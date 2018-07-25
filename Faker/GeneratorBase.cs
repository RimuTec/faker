using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace RimuTec.Faker
{
   /// <summary>
   /// Base class for all fake data generators.
   /// </summary>
   public class GeneratorBase
   {
      internal GeneratorBase() { }

      /// <summary>
      /// Parses a template that may contains tokens like "#{address.full_address}" and
      /// either invokes generator methods or load content from yaml files as replacement
      /// for the token
      /// </summary>
      /// <param name="template"></param>
      /// <returns></returns>
      protected internal static string Parse(string template)
      {
         var clazz = new StackTrace().GetFrame(1).GetMethod().DeclaringType;
         var matches = Regex.Matches(template, @"#{([a-zA-Z._]{1,})}");
         for (var i = 0; i < matches.Count; i++)
         {
            string placeHolder = matches[i].Value;
            var token = matches[i].Groups[1].Value;
            if (!token.Contains("."))
            {
               // Prepend class name before fetching
               token = $"{clazz.Name.ToLower()}.{token}";
            }

            var className = token.Split('.')[0].ToPascalCasing();
            var method = token.Split('.')[1].ToPascalCasing();

            string replacement = null;
            var type = typeof(YamlLoader).Assembly.GetTypes().FirstOrDefault(t => t.Name == className);
            if (type != null)
            {
               var methodInfo = type.GetMethod(method, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
               if (methodInfo != null)
               {
                  // invoke statics method, if needed with default parameter values
                  // Ref: https://stackoverflow.com/a/9916197/411428
                  var paramCount = methodInfo.GetParameters().Count();
                  object[] parameters = Enumerable.Repeat(Type.Missing, paramCount).ToArray();
                  replacement = methodInfo.Invoke(null, parameters).ToString();
               }
            }
            if (string.IsNullOrWhiteSpace(replacement))
            {
               replacement = YamlLoader.Fetch(token);
            }

            template = template.Replace(placeHolder, replacement);
         }
         return template;
      }
   }
}
