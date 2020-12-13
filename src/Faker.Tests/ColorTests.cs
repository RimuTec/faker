using System;
using System.Collections;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(ColorTestsFixtureData), nameof(ColorTestsFixtureData.FixtureParams))]
   public class ColorTests : FixtureBase
   {
      public ColorTests(string locale)
      {
         Locale = locale;
      }

      [SetUp]
      public void SetUp()
      {
         Config.Locale = Locale;
      }

      private string Locale { get; }

      [Test]
      public void ColorName_With_LocaleEn()
      {
         Config.Locale = "en";
         Console.Write($"#################### Locale is {Config.Locale} ####################");
         var colorName = Color.ColorName();
         var colors = Fetch("color.name");
         Assert.IsTrue(colors.Contains(colorName));
      }

      [Test]
      public void ColorName_With_LocaleRu()
      {
         Config.Locale = "ru";
         var colorName = Color.ColorName();
         var colors = Fetch("color.name");
         Assert.IsTrue(colors.Contains(colorName));
      }

      [Test]
      public void Fetch_ColorName_LocalRu()
      {
         Config.Locale = "ru";
         Color.LoadLocale("ru");
         Fetch("color.name");
      }

      [Test]
      public void HexColor()
      {
         var hex = Color.HexColor();
         Assert.AreEqual(6, hex.Length);
         Assert.AreEqual(6, RegexMatchesCount(hex, "[0-9A-F]"), $"{nameof(hex)} is {hex}");
      }

      [Test]
      public void RgbColor()
      {
         var rgb = Color.RgbColor();
         Assert.AreEqual(3, rgb.Length);
      }
   }

   public static class ColorTestsFixtureData
   {
      public static IEnumerable FixtureParams
      {
         get
         {
            yield return new TestFixtureData("bg");
            yield return new TestFixtureData("ca");
            yield return new TestFixtureData("ca-CAT");
            yield return new TestFixtureData("da-DK");
            yield return new TestFixtureData("de");
            yield return new TestFixtureData("de-AT");
            yield return new TestFixtureData("de-CH");
            yield return new TestFixtureData("ee");
            yield return new TestFixtureData("en");
            yield return new TestFixtureData("en-AU");
            yield return new TestFixtureData("en-au-ocker");
            yield return new TestFixtureData("en-BORK");
            yield return new TestFixtureData("en-CA");
            yield return new TestFixtureData("en-GB");
            yield return new TestFixtureData("en-IND");
            yield return new TestFixtureData("en-MS");
            yield return new TestFixtureData("en-NEP");
            yield return new TestFixtureData("en-NG");
            yield return new TestFixtureData("en-NZ");
            yield return new TestFixtureData("en-PAK");
            yield return new TestFixtureData("en-SG");
            yield return new TestFixtureData("en-UG");
            yield return new TestFixtureData("en-US");
            yield return new TestFixtureData("en-ZA");
            yield return new TestFixtureData("es");
            yield return new TestFixtureData("es-MX");
            yield return new TestFixtureData("fa");
            yield return new TestFixtureData("fi-FI");
            yield return new TestFixtureData("fr");
            yield return new TestFixtureData("fr-CA");
            yield return new TestFixtureData("fr-CH");
            yield return new TestFixtureData("he");
            yield return new TestFixtureData("id");
            yield return new TestFixtureData("it");
            yield return new TestFixtureData("ja");
            yield return new TestFixtureData("ko");
            yield return new TestFixtureData("lv");
            yield return new TestFixtureData("nb-NO");
            yield return new TestFixtureData("nl");

            // Not testing locale 'no' since the file no.yml has an incorrect format at the time of writing.
            // Note that we won't fix the content as we take the file 'as-is' from https://github.com/faker-ruby/faker
            //yield return new TestFixtureData("no", null);

            yield return new TestFixtureData("pl");
            yield return new TestFixtureData("pt");
            yield return new TestFixtureData("pt-BR");
            yield return new TestFixtureData("ru");
            yield return new TestFixtureData("sk");
            yield return new TestFixtureData("sv");
            yield return new TestFixtureData("tr");
            yield return new TestFixtureData("uk");
            yield return new TestFixtureData("vi");
            yield return new TestFixtureData("zh-CN");
            yield return new TestFixtureData("zh-TW");
         }
      }
   }
}
