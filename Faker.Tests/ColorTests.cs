using System;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
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
         var colors = Color.FetchList("color.name");
         Assert.IsTrue(colors.Contains(colorName));
      }

      [Test]
      public void ColorName_With_LocaleRu()
      {
         Config.Locale = "ru";
         var colorName = Color.ColorName();
         var colors = Color.FetchList("color.name");
         Assert.IsTrue(colors.Contains(colorName));
      }

      [Test]
      public void Fetch_ColorName_LocalRu()
      {
         Config.Locale = "ru";
         Color.LoadLocale("ru");
         Color.FetchList("color.name");
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
}
