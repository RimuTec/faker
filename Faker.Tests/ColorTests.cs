using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class ColorTests : FixtureBase
   {
      [Test]
      public void HexColor()
      {
         var hex = Color.HexColor();
         Assert.AreEqual(6, hex.Length);
         Assert.AreEqual(6, RegexMatchesCount(hex, @"[0-9A-F]"), $"{nameof(hex)} is {hex}");
      }
   }
}
