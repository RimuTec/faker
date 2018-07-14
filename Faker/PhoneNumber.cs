using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using YamlDotNet.Serialization;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for phone numbers
   /// </summary>
   public static class PhoneNumber
   {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/phone_number.yml

      static PhoneNumber()
      {
         const string yamlFileName = "RimuTec.Faker.locales.en.phone_number.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _faker = locale.en.faker;
      }

      /// <summary>
      /// Generates a land line number in one of several formats.
      /// </summary>
      /// <returns></returns>
      /// <remarks>This method is the equivalent to Ruby's Faker::PhoneNumber.phone_number.</remarks>
      public static string LandLine()
      {
         var numberTemplate = _faker.PhoneNumber.Formats.Sample();
         return numberTemplate.Numerify();
      }

      /// <summary>
      /// Generates a cell phone number in one of several formats.
      /// </summary>
      /// <returns></returns>
      public static string CellPhone()
      {
         var numberTemplate = _faker.CellPhone.Formats.Sample();
         return numberTemplate.Numerify();
      }

      private static faker _faker;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale
      {
         public en en { get; set; }
      }

      internal class en
      {
         public faker faker { get; set; }
      }

      internal class faker
      {
         [YamlMember(Alias = "phone_number", ApplyNamingConventions = false)]
         public phone_number PhoneNumber { get; set; }

         [YamlMember(Alias = "cell_phone", ApplyNamingConventions = false)]
         public cell_phone CellPhone { get; set; }
      }

      internal class phone_number
      {
         [YamlMember(Alias = "formats", ApplyNamingConventions = false)]
         public string[] Formats { get; set; }
      }

      internal class cell_phone
      {
         [YamlMember(Alias = "formats", ApplyNamingConventions = false)]
         public string[] Formats { get; set; }
      }

#pragma warning restore IDE1006 // Naming Styles
   }
}
