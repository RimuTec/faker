using NUnit.Framework;
using RimuTec.Faker.Helper;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class RangeTests {
      [Test]
      public void Range_Default() {
         // arrange
         var boundaries = "0,9";

         // act
         var range = new Range(boundaries);

         // assert
         Assert.AreEqual("0", range.MinValue);
         Assert.AreEqual("9", range.MaxValue);
      }

      [Test]
      public void Array_From_Range() {
         // arrange
         var range = new Range("A-B".Split('-'));

         // act
         var array = range.ToArray();

         // assert
         Assert.AreEqual(2, array.Length);
         Assert.AreEqual("A", array[0]);
         Assert.AreEqual("B", array[1]);
      }
   }
}
