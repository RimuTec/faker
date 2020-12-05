using System;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class ColorTests : FixtureBase
   {
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
         Assert.AreEqual(6, RegexMatchesCount(hex, @"[0-9A-F]"), $"{nameof(hex)} is {hex}");
      }

      [Test]
      public void RgbColor()
      {
         var rgb = Color.RgbColor();
         Assert.AreEqual(3, rgb.Length);
      }
   }
}
