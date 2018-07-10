using System;
using System.Collections.Generic;
using System.Text;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for dates in the past, in the future, birthdays, etc.
   /// </summary>
   public static class Date {
      /// <summary>
      /// Random date between dates.
      /// </summary>
      /// <param name="from">Minimum date.</param>
      /// <param name="to">Maximum date. Must be equal or greater than 'minDate'.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">When <paramref name="to"/> is less than <paramref name="from"/>.</exception>
      public static DateTime Between(DateTime from, DateTime to) {
         var fromDate = from.Date;
         var toDate = to.Date;
         if (toDate < fromDate) {
            throw new ArgumentOutOfRangeException(nameof(to), $"Must be equal to or greater than {nameof(from)}.");
         }
         var timespan = toDate - fromDate;
         return fromDate.AddDays(RandomNumber.Next((int)timespan.TotalDays)).Date;
      }

      /// <summary>
      /// Random date between dates, except for a specific date.
      /// </summary>
      /// <param name="from">Minimum date.</param>
      /// <param name="to">Maximum date. Must be greater than minimum and excepted date.</param>
      /// <param name="excepted">Excepted date.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentException">If <paramref name="from"/>, <paramref name="to"/> and <paramref name="excepted"/> are equal.</exception>
      /// <exception cref="ArgumentOutOfRangeException">If <paramref name="to"/> is less than <paramref name="from"/> or if <paramref name="excepted"/> outside of date range.</exception>
      public static DateTime BetweenExcept(DateTime from, DateTime to, DateTime excepted) {
         DateTime fromDate = from.Date;
         DateTime toDate = to.Date;
         DateTime exceptedDate = excepted.Date;
         if (fromDate == toDate && toDate == exceptedDate) {
            throw new ArgumentException("From date, to date and excepted date must not be the same.");
         }
         if (toDate < fromDate) {
            throw new ArgumentOutOfRangeException(nameof(to), $"Must be equal to or greater than {nameof(from)}.");
         }
         if(exceptedDate < from || excepted > to) {
            throw new ArgumentOutOfRangeException(nameof(excepted), $"Must be between {nameof(from)} and {nameof(to)} date.");
         }
         var timespan = toDate - fromDate;
         DateTime result;
         do {
            result = from.Date.AddDays(RandomNumber.Next((int)timespan.TotalDays)).Date;
         } while (result == excepted);
         return result;
      }

      /// <summary>
      /// Generates a random date in the future (up to a maximum of N days).
      /// </summary>
      /// <param name="days">Maximum number of days into the future.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">If <paramref name="days"/> is zero or less.</exception>
      public static DateTime Forward(int days = 365) {
         if(days <= 0) {
            throw new ArgumentOutOfRangeException(nameof(days), "Must be greater than zero.");
         }
         var fromDate = DateTime.Today.AddDays(1);
         var toDate = DateTime.Today.AddDays(days);
         return Between(fromDate, toDate);
      }
   }
}
