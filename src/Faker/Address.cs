using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   public static class Address {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/address.yml
      // [Manfred, 21jun2018]

      static Address() {
         const string yamlFileName = "RimuTec.Faker.locales.en.address.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _address = locale.en.faker.address;
         _streetSuffix = _address.StreetSuffix;
      }

      /// <summary>
      /// Generates a building number. Example: "7304"
      /// </summary>
      /// <returns></returns>
      public static string BuildingNumber() {
         var template = _address.BuildingNumber.Random();
         return template.Bothify();
      }

      /// <summary>
      /// Generates a community name. Example: "University Crossing"
      /// </summary>
      /// <returns></returns>
      public static string Community() {
         var template = _address.Community.Random();
         template = template.Replace("#{community_prefix}", _address.CommunityPrefix.Random());
         template = template.Replace("#{community_suffix}", _address.CommunitySuffix.Random());
         return template;
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905"
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string Postcode(string stateAbbreviation = "") {
         return ZipCode(stateAbbreviation);
      }

      /// <summary>
      /// Generates a secondary address. Example: "Apt. 057"
      /// </summary>
      /// <returns></returns>
      public static string SecondaryAddress() {
         var template = _address.SecondaryAddress.Random();
         return template.Bothify();
      }

      /// <summary>
      /// Returns a street address, optionally with secondary address. Examples: "282 Kevin Brook" (no secondary) or "156 Margarita Pass Apt. 057" (including secondary).
      /// </summary>
      /// <param name="includeSecondary"></param>
      /// <returns></returns>
      public static string StreetAddress(bool includeSecondary = false) {
         var template = _address.StreetAddress.First() + (includeSecondary ? " " + SecondaryAddress() : string.Empty);
         template = template.Replace("#{building_number}", _address.BuildingNumber.Random());
         template = template.Replace("#{street_name}", StreetName());
         var result = template.Numerify();
         return result;
      }

      /// <summary>
      /// Returns a street name. Examples: "Kevin Brook" or "Margarita Pass".
      /// </summary>
      /// <returns></returns>
      public static string StreetName() {
         var template = _address.StreetName.Random();
         template = template.Replace("#{Name.first_name}", Name.FirstName());
         template = template.Replace("#{Name.last_name}", Name.LastName());
         template = template.Replace("#{street_suffix}", _address.StreetSuffix.Random());
         return template;
      }

      /// <summary>
      /// Returns a street suffix. Example: "Street"
      /// </summary>
      /// <returns></returns>
      public static string StreetSuffix() {
         return _address.StreetSuffix.Random();
      }

      /// <summary>
      /// Returns a random time zone. Example: "Asia/Yakutsk"
      /// </summary>
      /// <returns></returns>
      public static string TimeZone() {
         return _address.TimeZone.Random();
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905"
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string Zip(string stateAbbreviation = "") {
         return ZipCode(stateAbbreviation);
      }

      /// <summary>
      /// Generates a US ZIP code, optionally for a specific US state. Examples: "58517" or "23285-4905"
      /// </summary>
      /// <param name="stateAbbreviation">Abbreviation for one of the US states, e.g. "ME", or "". Default is "".</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">Parameter 'Abbreviation' has an invalid value.</exception>
      public static string ZipCode(string stateAbbreviation = "") {
         if(stateAbbreviation == "") {
            return _address.Postcode.Random().Letterify().Numerify();
         }
         if(stateAbbreviation == null || !_address.PostcodeByState.ContainsKey(stateAbbreviation)) {
            throw new ArgumentOutOfRangeException(nameof(stateAbbreviation), "Must be one of the US state abbreviations or empty.");
         }
         var template = _address.PostcodeByState[stateAbbreviation];
         return template.Letterify().Numerify();
      }

      internal static string[] _streetSuffix;

      private static address _address;

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
         public address address { get; set; }
      }

      internal class address {
         [YamlMember(Alias = "city_prefix", ApplyNamingConventions = false)]
         public string[] CityPrefix { get; set; }

         [YamlMember(Alias = "city_suffix", ApplyNamingConventions = false)]
         public string[] CitySuffix { get; set; }

         [YamlMember(Alias = "country", ApplyNamingConventions = false)]
         public string[] Country { get; set; }

         [YamlMember(Alias = "country_code", ApplyNamingConventions = false)]
         public string[] CountryCode { get; set; }

         [YamlMember(Alias = "country_code_long", ApplyNamingConventions = false)]
         public string[] CountryCodeLong { get; set; }

         [YamlMember(Alias = "building_number", ApplyNamingConventions = false)]
         public string[] BuildingNumber { get; set; }

         [YamlMember(Alias = "community_prefix", ApplyNamingConventions = false)]
         public string[] CommunityPrefix { get; set; }

         [YamlMember(Alias = "community_suffix", ApplyNamingConventions = false)]
         public string[] CommunitySuffix { get; set; }

         [YamlMember(Alias = "community", ApplyNamingConventions = false)]
         public List<string> Community { get; set; }

         [YamlMember(Alias = "street_suffix", ApplyNamingConventions = false)]
         public string[] StreetSuffix { get; set; }

         [YamlMember(Alias = "secondary_address", ApplyNamingConventions = false)]
         public string[] SecondaryAddress { get; set; }

         [YamlMember(Alias = "postcode", ApplyNamingConventions = false)]
         public string[] Postcode { get; set; }

         [YamlMember(Alias = "postcode_by_state", ApplyNamingConventions = false)]
         public Dictionary<string, string> PostcodeByState { get; set; }
         // Deserialize Dictionary<>: https://stackoverflow.com/questions/38339739/trying-to-convert-yaml-file-to-hashtable-using-yamldotnet

         [YamlMember(Alias = "state", ApplyNamingConventions = false)]
         public string[] State { get; set; }

         [YamlMember(Alias = "state_abbr", ApplyNamingConventions = false)]
         public string[] StateAbbr { get; set; }

         [YamlMember(Alias = "time_zone", ApplyNamingConventions = false)]
         public string[] TimeZone { get; set; }

         [YamlMember(Alias = "city", ApplyNamingConventions = false)]
         public List<string> City { get; set; }

         [YamlMember(Alias = "street_name", ApplyNamingConventions = false)]
         public List<string> StreetName { get; set; }

         [YamlMember(Alias = "street_address", ApplyNamingConventions = false)]
         public List<string> StreetAddress { get; set; }

         [YamlMember(Alias = "full_address", ApplyNamingConventions = false)]
         public List<string> FullAddress { get; set; }

         [YamlMember(Alias = "default_country", ApplyNamingConventions = false)]
         public string[] DefaultCountry { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
