using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for address related data.
   /// </summary>
   public static class Address
   {
      private static string Parse(string template)
      {
         var matches = Regex.Matches(template, @"#{([a-zA-Z._]{1,})}");
         for (var i = 0; i < matches.Count; i++)
         {
            string placeHolder = matches[i].Value;
            var token = matches[i].Groups[1].Value;
            if (!token.Contains("."))
            {
               // Prepend class name before fetching
               token = $"address.{token}";
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
            if(string.IsNullOrWhiteSpace(replacement))
            {
               replacement = YamlLoader.Fetch(token);
            }

            template = template.Replace(placeHolder, replacement);
         }
         return template;
      }

      /// <summary>
      /// Generates a building number. Example: "7304".
      /// </summary>
      /// <returns></returns>
      public static string BuildingNumber()
      {
         return YamlLoader.Fetch("address.building_number").Bothify();
      }

      /// <summary>
      /// Returns a city. Example: "Imogeneborough"
      /// </summary>
      /// <param name="withState">If 'true', returns city with state. Default is 'false'.</param>
      /// <returns></returns>
      public static string City(bool withState = false)
      {
         return Parse(YamlLoader.Fetch("address.city"));
      }

      /// <summary>
      /// Resturns a city prefix. Example: "Lake".
      /// </summary>
      /// <returns></returns>
      public static string CityPrefix()
      {
         return YamlLoader.Fetch("address.city_prefix");
      }

      /// <summary>
      /// Returns a city suffix. Example: "fort".
      /// </summary>
      /// <returns></returns>
      public static string CitySuffix()
      {

         return YamlLoader.Fetch("address.city_suffix");
      }

      /// <summary>
      /// Generates a community name. Example: "University Crossing".
      /// </summary>
      /// <returns></returns>
      public static string Community()
      {
         return Parse(YamlLoader.Fetch("address.community"));
      }

      /// <summary>
      /// Returns a country name. Exmple: "French Guiana".
      /// </summary>
      /// <returns></returns>
      public static string Country()
      {
         return YamlLoader.Fetch("address.country");
      }

      /// <summary>
      /// Returns a country code. Example: "IT"
      /// </summary>
      /// <returns></returns>
      public static string CountryCode()
      {
         return YamlLoader.Fetch("address.country_code");
      }

      /// <summary>
      /// Returns a long country code. Example: "ITA"
      /// </summary>
      /// <returns></returns>
      public static string CountryCodeLong()
      {
         return YamlLoader.Fetch("address.country_code_long");
      }

      /// <summary>
      /// Returns a full address. Example: "282 Kevin Brook, Imogeneborough, CA 58517"
      /// </summary>
      /// <returns></returns>
      public static string FullAddress()
      {
         return Parse(YamlLoader.Fetch("address.full_address"));
      }

      /// <summary>
      /// Returns a latitude. Example: -58.17256227443719
      /// </summary>
      /// <returns></returns>
      public static double Latitude()
      {
         return (RandomNumber.NextDouble() * 180) - 90;
      }

      /// <summary>
      /// Returns a longitude. Example: -156.65548382095133
      /// </summary>
      /// <returns></returns>
      public static double Longitude()
      {
         return (RandomNumber.NextDouble() * 360) - 180;
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905".
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string Postcode(string stateAbbreviation = "")
      {
         return ZipCode(stateAbbreviation);
      }

      /// <summary>
      /// Generates a secondary address. Example: "Apt. 057".
      /// </summary>
      /// <returns></returns>
      public static string SecondaryAddress()
      {
         return YamlLoader.Fetch("address.secondary_address").Bothify();
      }

      /// <summary>
      /// Returns a state. Example: "California".
      /// </summary>
      /// <returns></returns>
      public static string State()
      {
         return YamlLoader.Fetch("address.state");
      }

      /// <summary>
      /// Returns a state abbreviation. Examples: "AP" (Armed Forces Pacific, in case you wondered) or "ME" (Maine).
      /// </summary>
      /// <returns></returns>
      public static string StateAbbr()
      {
         return YamlLoader.Fetch("address.state_abbr");
      }

      /// <summary>
      /// Returns a street address, optionally with secondary address. Examples: "282 Kevin Brook" (no secondary) 
      /// or "156 Margarita Pass Apt. 057" (including secondary).
      /// </summary>
      /// <param name="includeSecondary">'true' to include, 'false' to exclude sencondary. Default value is 'false'.</param>
      /// <returns></returns>
      public static string StreetAddress(bool includeSecondary = false)
      {
         return (Parse(YamlLoader.Fetch("address.street_address")) + (includeSecondary ? " " + SecondaryAddress() : string.Empty)).Numerify();
      }

      /// <summary>
      /// Returns a street name. Examples: "Kevin Brook" or "Margarita Pass".
      /// </summary>
      /// <returns></returns>
      public static string StreetName()
      {
         return Parse(YamlLoader.Fetch("address.street_name"));
      }

      /// <summary>
      /// Returns a street suffix. Example: "Street".
      /// </summary>
      /// <returns></returns>
      public static string StreetSuffix()
      {
         return YamlLoader.Fetch("address.street_suffix");
      }

      /// <summary>
      /// Returns a random time zone. Example: "Asia/Yakutsk".
      /// </summary>
      /// <returns></returns>
      public static string TimeZone()
      {
         return YamlLoader.Fetch("address.time_zone");
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905".
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string Zip(string stateAbbreviation = "")
      {
         return ZipCode(stateAbbreviation);
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905".
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string ZipCode(string stateAbbreviation = "")
      {
         if (stateAbbreviation == "")
         {
            return YamlLoader.Fetch("address.postcode").Bothify();
         }
         try
         {
            return YamlLoader.Fetch($"address.postcode_by_state.{stateAbbreviation}");
         }
         catch(KeyNotFoundException)
         {
            throw new ArgumentOutOfRangeException(nameof(stateAbbreviation), "Must be one of the US state abbreviations or empty.");
         }
      }
   }
}
