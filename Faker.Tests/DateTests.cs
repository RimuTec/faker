using NUnit.Framework;
using System;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class DateTests
   {
      public DateTests(string locale)
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
      public void Backward_With_Days()
      {
         var tries = 100;
         const int days = 1000;
         var date = DateTime.MaxValue;
         while (tries-- > 0)
         {
            date = Date.Backward(days);
            if (date < DateTime.Today.AddYears(-1))
               break;
         }
         var minDate = DateTime.Today.AddDays(-days);
         var maxDate = DateTime.Today.AddDays(-1);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void Backward_With_Invalid_Days()
      {
         const int days = 0;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Backward(days));
         Assert.AreEqual("Must be greater than zero. (Parameter 'days')", ex.Message);
      }

      [Test]
      public void Between_With_Default_Values()
      {
         var date = Date.Between(2.Days().Ago, DateTime.Today);
         AssertIsDateOnly(date);
         Assert.GreaterOrEqual(date, 2.Days().Ago);
         Assert.LessOrEqual(date, DateTime.Today);
      }

      [Test]
      public void Between_With_Invalid_Range()
      {
         var minDate = DateTime.Today;
         var maxDate = minDate.AddDays(-1);
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Between(minDate, maxDate));
         Assert.AreEqual("Must be equal to or greater than from. (Parameter 'to')", ex.Message);
      }

      [Test]
      public void Between_With_Same_Dates()
      {
         var requested = DateTime.Today;
         var date = Date.Between(requested, requested);
         AssertIsDateOnly(date);
         Assert.AreEqual(requested, date);
      }

      [Test]
      public void BetweenExcept_With_Default_Values()
      {
         var minDate = 2.Days().Ago;
         var maxDate = 1.Years().FromNow;
         var date = Date.BetweenExcept(minDate, maxDate, DateTime.Today);
         AssertIsDateOnly(date);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void BetweenExcept_With_All_Parameters_Equal()
      {
         var minDate = 2.Days().Ago;
         var maxDate = minDate;
         var excepted = minDate;
         var ex = Assert.Throws<ArgumentException>(() => Date.BetweenExcept(minDate, maxDate, excepted));
         Assert.AreEqual("From date, to date and excepted date must not be the same.", ex.Message);
      }

      [Test]
      public void BetweenExcept_Invalid_Range()
      {
         var minDate = 2.Days().Ago;
         var maxDate = 5.Days().Ago;
         var excepted = 3.Days().Ago;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.BetweenExcept(minDate, maxDate, excepted));
         Assert.AreEqual("Must be equal to or greater than from. (Parameter 'to')", ex.Message);
      }

      [Test]
      public void BetweenExcepted_Excepted_Outside_Range()
      {
         var minDate = 14.Days().Ago;
         var maxDate = 2.Days().Ago;
         var excepted = 2.Years().FromNow;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.BetweenExcept(minDate, maxDate, excepted));
         Assert.AreEqual("Must be between from and to date. (Parameter 'excepted')", ex.Message);
      }

      [Test]
      public void Birthday_With_Default_Values()
      {
         var birthday = Date.Birthday();
         Assert.GreaterOrEqual(birthday, DateTime.Today.Date.AddYears(-65));
         Assert.LessOrEqual(birthday, DateTime.Today.Date.AddYears(-18));
      }

      [Test]
      public void Birthday_With_Negative_MinAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Birthday(minAge: -1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'minAge')", ex.Message);
      }

      [Test]
      public void Birthday_With_Negative_MaxAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Birthday(maxAge: -1));
         Assert.AreEqual("Must be equal to or greater than zero. (Parameter 'maxAge')", ex.Message);
      }

      [Test]
      public void Birthday_With_MinAge_Greater_MaxAge()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Birthday(minAge: 42, maxAge: 17));
         Assert.AreEqual("Must be equal to or greater than minAge. (Parameter 'maxAge')", ex.Message);
      }

      [Test]
      public void Birthday_With_MinAge_Equal_MaxAge()
      {
         var birthday = Date.Birthday(42, 42);
         Assert.AreEqual(birthday, DateTime.Today.Date.AddYears(-42));
         Assert.AreEqual(birthday, DateTime.Today.Date.AddYears(-42));
      }

      [Test]
      public void Forward_With_Default_Value()
      {
         var date = Date.Forward();
         var minDate = DateTime.Today.AddDays(1);
         var maxDate = DateTime.Today.AddYears(1);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void Forward_With_Days()
      {
         var tries = 100;
         const int days = 1000;
         DateTime date = DateTime.MinValue;
         while (tries-- > 0)
         {
            date = Date.Forward(days);
            if (date > DateTime.Today.AddYears(1))
               break;
         }
         var minDate = DateTime.Today.AddDays(1);
         var maxDate = DateTime.Today.AddDays(days);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      [Test]
      public void Forward_With_Invalid_Days()
      {
         const int days = 0;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Date.Forward(days));
         Assert.AreEqual("Must be greater than zero. (Parameter 'days')", ex.Message);
      }

      [Test]
      public void Backward_With_DefaultValue()
      {
         var date = Date.Backward();
         var minDate = DateTime.Today.AddYears(-1);
         var maxDate = DateTime.Today.AddDays(-1);
         Assert.GreaterOrEqual(date, minDate);
         Assert.LessOrEqual(date, maxDate);
      }

      private static void AssertIsDateOnly(DateTime date)
      {
         Assert.AreNotEqual(0, date.Year);
         Assert.AreNotEqual(0, date.Month);
         Assert.AreNotEqual(0, date.Day);
         Assert.AreEqual(0, date.Hour);
         Assert.AreEqual(0, date.Minute);
         Assert.AreEqual(0, date.Second);
         Assert.AreEqual(0, date.Millisecond);
      }
   }

   public static class IntExtensions
   {
      public static Years Years(this int i)
      {
         return new Years(i);
      }

      public static Days Days(this int i)
      {
         return new Days(i);
      }
   }

   public class Years
   {
      public Years(int i)
      {
         _years = i;
      }

      public DateTime FromNow
      {
         get
         {
            return DateTime.Today.AddYears(_years);
         }
      }

      private readonly int _years;
   }

   public class Days
   {
      public Days(int i)
      {
         _days = i;
      }

      public DateTime Ago
      {
         get
         {
            return DateTime.Today.AddDays(-_days);
         }
      }

      private readonly int _days;
   }
}
