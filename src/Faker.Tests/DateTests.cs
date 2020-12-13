using NUnit.Framework;
using System;
using System.Collections;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(ColorTestsFixtureData), nameof(ColorTestsFixtureData.FixtureParams))]
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
         Assert.Greater(date, minDate);
         Assert.Less(date, maxDate);
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

   public static class DateTestsFixtureData
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
