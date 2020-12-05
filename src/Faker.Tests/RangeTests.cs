using NUnit.Framework;
using RimuTec.Faker.Helper;
using System.Linq;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class RangeTests
   {
      [Test]
      public void Range_With_Integers()
      {
         var range = new Range2<int>(4, 7);
         Assert.AreEqual(4, range.Length());
      }

      [Test]
      public void Range_Default()
      {
         var range = new Range2<char>('0', '9');
         Assert.AreEqual('0', range.MinValue);
         Assert.AreEqual('9', range.MaxValue);
      }

      [Test]
      public void Array_From_Range()
      {
         var range = new Range2<char>("A-B".Split('-').Select(x => x[0]).ToArray());
         var array = range.AsArray();
         Assert.AreEqual(2, array.Length);
         Assert.AreEqual('A', array[0]);
         Assert.AreEqual('B', array[1]);
      }


      [Test]
      public void Range2_With_Integers()
      {
         var range = new Range2<int>(1, 3);
         Assert.AreEqual(1, range.MinValue);
         Assert.AreEqual(3, range.MaxValue);
      }

      [Test]
      public void Range2_With_Chars()
      {
         var range = new Range2<char>('a', 'b');
         Assert.AreEqual('a', range.MinValue);
         Assert.AreEqual('b', range.MaxValue);
         var expected = new char[] { 'a', 'b' };
         var actuals = range.AsArray();
         for (var i = 0; i < expected.Length; i++)
         {
            Assert.AreEqual(expected[i], actuals[i]);
         }
      }
   }
}
