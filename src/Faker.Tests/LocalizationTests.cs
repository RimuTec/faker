using NUnit.Framework;
using System;
using System.Linq;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class LocalizationTests : FixtureBase
   {
      [TearDown]
      public void TearDown()
      {
         Config.Locale = "en";
      }

      [Test]
      public void German_City()
      {
         var germanSuffixes = new string[] { "stadt", "dorf", "land", "scheid", "burg", "berg", "heim", "hagen", "feld", "brunn", "grün" };
         Config.Locale = "de";
         var city = Address.City();
         Assert.IsTrue(germanSuffixes.Any(x => city.EndsWith(x)), $"{nameof(city)} was '{city}'");
         Config.Locale = "en";
      }

      [Test]
      public void German_FirstName()
      {
         Config.Locale = "de";
         var firstName = Name.FirstName();
         var firstNames = Fetch("name.first_name");
         Assert.Greater(firstNames.Count, 0);
         Assert.IsTrue(firstNames.Contains(firstName));
      }

      [Test]
      public void German_LastName()
      {
         Config.Locale = "de";
         var lastName = Name.LastName();
         var lastNames = Fetch("name.last_name");
         Assert.Greater(lastNames.Count, 0);
         Assert.IsTrue(lastNames.Contains(lastName));
      }

      [Test]
      public void Custom_Locale()
      {
         // RimuTec.Faker comes with all locale files included that are also part of Ruby's Faker gem.
         // In case you need some custom localization you can create a custom yaml file that provides
         // values for all or just a subset of the default locale (en). For a list of all available
         // locales see https://github.com/stympy/faker/tree/master/lib/locales/en

         // The custom local file needs to be in the same folder as RimuTec.Faker.dll, which is
         // usually the output directory of the project containing your unit tests.

         // Note that for this example we use the New Zealand Maori locale, "mi-NZ". First names and
         // last names are already in the embedded "en-NZ" locale (see en-nz.yml).
         // The sample local file mi-NZ.yml uses first names found at
         // https://www.dia.govt.nz/press.nsf/d77da9b523f12931cc256ac5000d19b6/18983b92b09bcf73cc257d1d00832439!OpenDocument
         // It uses last names found at https://en.wikipedia.org/wiki/Category:M%C4%81ori-language_surnames
         // and at https://en.wikipedia.org/wiki/Category:New_Zealand_M%C4%81ori_people
         // Should anyone feel offended by including or excluding a name in the file mi-NZ.yml, please create
         // an issue at https://github.com/RimuTec/Faker/issues to get it resolved. Thank you!

         // Maori New Zealand locale, for more details see 
         // https://teara.govt.nz/en/matauranga-hangarau-information-technology/page-3
         // http://homepages.ihug.co.nz/~trmusson/maori.html
         Config.Locale = "mi-NZ";
         var firstName = Name.FirstName();
         var firstNames = Fetch("name.first_name");
         Assert.Greater(firstNames.Count, 0);
         Assert.IsTrue(firstNames.Contains(firstName));
      }

      [Test]
      public void Fallback_Is_Locale_En()
      {
         Config.Locale = "mi-NZ";
         var citySuffix = Address.CitySuffix();
         Config.Locale = "en";
         var citySuffixes = Fetch("address.city_suffix");
         Assert.IsFalse(string.IsNullOrWhiteSpace(citySuffix));
         Assert.IsTrue(citySuffixes.Contains(citySuffix));
         Assert.Greater(citySuffixes.Count, 0);
      }

      [Test]
      public void TryLoadingInvalidLocaleFile()
      {
         const string locale = "no";
         Config.Locale = locale; // file no.yml contains locale 'no-NO' which is different
         var ex = Assert.Throws<FormatException>(() => Address.ZipCode());
         Assert.AreEqual($"File for locale '{locale}' has an invalid format. [Code 201213-1413]", ex.Message);
      }
   }
}
