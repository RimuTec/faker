using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class ColorTests : FixtureBase
   {
      [Test]
      public void ColorName()
      {
         var colorName = Color.ColorName();
         var colors = Fetch("color.name");
         Assert.IsTrue(colors.Contains(colorName));
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
