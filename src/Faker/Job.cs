using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace RimuTec.Faker {
   public static class Job {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/job.yml
      // [Manfred, 03jun2018]

      static Job() {
         var executingAssembly = Assembly.GetExecutingAssembly();
         var resourceNames = executingAssembly.GetManifestResourceNames();
         using (var resourceStream = executingAssembly.GetManifestResourceStream("RimuTec.Faker.locales.en.job.yml")) {
            using (var textReader = new StreamReader(resourceStream)) {
               var deserializer = new DeserializerBuilder()
                  .WithNamingConvention(new CamelCaseNamingConvention())
                  .Build()
                  ;
               _locale = deserializer.Deserialize<locale>(textReader.ReadToEnd());
            }
         }
      }

      public static string KeySkill() {
         return _locale.en.faker.job.KeySkills.Random().Trim();
      }

      public static string Title() {
         var titleTemplate = _locale.en.faker.job.Title.Random().Trim();
         var seniority = _locale.en.faker.job.Seniority.Random().Trim();
         var field = _locale.en.faker.job.Field.Random().Trim();
         var position = _locale.en.faker.job.Position.Random().Trim();

         var result = titleTemplate.Replace("#{seniority}", seniority);
         result = result.Replace("#{field}", field);
         result = result.Replace("#{position}", position);

         return result;
      }

      private static locale _locale;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale {
         public en en { get; set; }
      }

      internal class en {
         public faker faker { get; set; }
      }

      internal class faker {
         public job job { get; set; }
      }

      internal class job {
         [YamlMember(Alias = "field", ApplyNamingConventions = false)]
         public string[] Field { get; set; }

         [YamlMember(Alias = "seniority", ApplyNamingConventions = false)]
         public string[] Seniority { get; set; }

         [YamlMember(Alias = "position", ApplyNamingConventions = false)]
         public string[] Position { get; set; }

         [YamlMember(Alias = "key_skills", ApplyNamingConventions = false)]
         public string[] KeySkills { get; set; }

         [YamlMember(Alias = "employment_type", ApplyNamingConventions = false)]
         public string[] EmploymentType { get; set; }

         [YamlMember(Alias = "education_level", ApplyNamingConventions = false)]
         public string[] EducationLevel { get; set; }

         public List<string> Title { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
