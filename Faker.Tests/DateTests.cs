using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class DateTests {
      [Test]
      public void Between_With_Default_Values() {
         // arrange

         // act
         var date = Date.Between(2.Days().Ago, DateTime.Today);

         // assert
         Assert.GreaterOrEqual(date, 2.Days().Ago);
         Assert.LessOrEqual(date, DateTime.Today);
      }

      [Test]
      public void Between_With_Invalid_Range() {
         // arrange
         var minDate = DateTime.Today;
         var maxDate = minDate.AddDays(-1);

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Between(minDate, maxDate));

         // assert
         Assert.AreEqual("Must be equal to or greater than minDate.\r\nParameter name: maxDate", ex.Message);
      }

      [Test]
      public void Between_With_Same_Dates() {
         // arrange
         var requested = DateTime.Today;

         // act
         var date = Date.Between(requested, requested);

         // assert
         Assert.AreEqual(requested, date);
      }
   }

   public static class IntExtensions {
      public static Days Days(this int i) {
         return new Days(i);
      }
   }

   public class Days {
      public Days(int i) {
         _days = i;
      }

      public DateTime Ago {
         get {
            return DateTime.Today.AddDays(-_days);
         }
      }

      private readonly int _days;
   }
}
