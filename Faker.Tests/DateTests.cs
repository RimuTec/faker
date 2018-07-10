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
         AssertIsDateOnly(date);
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
         Assert.AreEqual("Must be equal to or greater than from.\r\nParameter name: to", ex.Message);
      }

      [Test]
      public void Between_With_Same_Dates() {
         // arrange
         var requested = DateTime.Today;

         // act
         var date = Date.Between(requested, requested);

         // assert
         AssertIsDateOnly(date);
         Assert.AreEqual(requested, date);
      }

      [Test]
      public void BetweenExcept_With_Default_Values() {
         // arrange
         var minDate = 2.Days().Ago;
         var maxDate = 1.Years().FromNow;

         // act
         var date = Date.BetweenExcept(minDate, maxDate, DateTime.Today);

         // assert
         AssertIsDateOnly(date);
         Assert.Greater(date, minDate);
         Assert.Less(date, maxDate);
      }

      [Test]
      public void BetweenExcept_With_All_Parameters_Equal() {
         // arrange
         var minDate = 2.Days().Ago;
         var maxDate = minDate;
         var excepted = minDate;

         // act
         var ex = Assert.Throws<ArgumentException>(() => Date.BetweenExcept(minDate, maxDate, excepted));

         // assert
         Assert.AreEqual("From date, to date and excepted date must not be the same.", ex.Message);
      }

      [Test]
      public void BetweenExcept_Invalid_Range() {
         // arrange
         var minDate = 2.Days().Ago;
         var maxDate = 5.Days().Ago;
         var excepted = 3.Days().Ago;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.BetweenExcept(minDate, maxDate, excepted));

         // assert
         Assert.AreEqual("Must be equal to or greater than from.\r\nParameter name: to", ex.Message);
      }

      [Test]
      public void BetweenExcepted_Excepted_Outside_Range() {
         // arrange
         var minDate = 14.Days().Ago;
         var maxDate = 2.Days().Ago;
         var excepted = 2.Years().FromNow;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.BetweenExcept(minDate, maxDate, excepted));

         // assert
         Assert.AreEqual("Must be between from and to date.\r\nParameter name: excepted", ex.Message);
      }

      [Test]
      public void Forward_With_Default_Value() {
         // arrange

         // act
         var date = Date.Forward();

         // assert
         var minDate = DateTime.Today.AddDays(1);
         var maxDate = DateTime.Today.AddYears(1);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void Forward_With_Days() {
         // arrange
         var tries = 100;
         var days = 1000;

         // act
         DateTime date = DateTime.MinValue;
         while (tries-- > 0) {
            date = Date.Forward(days);
            if (date > DateTime.Today.AddYears(1))
               break;
         }

         // assert
         var minDate = DateTime.Today.AddDays(1);
         var maxDate = DateTime.Today.AddDays(days);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void Forward_With_Invalid_Days() {
         // arrange
         var days = 0;

         // Act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Forward(days));

         // assert
         Assert.AreEqual($"Must be greater than zero.\r\nParameter name: days", ex.Message);
      }

      private static void AssertIsDateOnly(DateTime date) {
         Assert.AreNotEqual(0, date.Year);
         Assert.AreNotEqual(0, date.Month);
         Assert.AreNotEqual(0, date.Day);
         Assert.AreEqual(0, date.Hour);
         Assert.AreEqual(0, date.Minute);
         Assert.AreEqual(0, date.Second);
         Assert.AreEqual(0, date.Millisecond);
      }
   }

   public static class IntExtensions {
      public static Years Years(this int i) {
         return new Years(i);
      }

      public static Days Days(this int i) {
         return new Days(i);
      }
   }

   public class Years {
      public Years(int i) {
         _years = i;
      }

      public DateTime FromNow {
         get {
            return DateTime.Today.AddYears(_years);
         }
      }

      private readonly int _years;
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
