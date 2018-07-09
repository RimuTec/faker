using System;
using System.Collections.Generic;
using System.Text;

namespace RimuTec.Faker {
   /// <summary>
   /// Generators for dates in the past, in the future, birthdays, etc.
   /// </summary>
   public static class Date {
      /// <summary>
      /// Random date between dates. Example: "Wed, 24 Sep 2014"
      /// </summary>
      /// <param name="minDate">Minimum date.</param>
      /// <param name="maxDate">Maximum date. Must be equal or greater than 'minDate'.</param>
      /// <returns></returns>
      /// <exception cref="ArgumentOutOfRangeException">When maxDate is less than minDate.</exception>
      public static DateTime Between(DateTime minDate, DateTime maxDate) {
         if(maxDate < minDate) {
            throw new ArgumentOutOfRangeException(nameof(maxDate), "Must be equal to or greater than minDate.");
         }
         var timespan = maxDate - minDate;
         return minDate.AddDays(RandomNumber.Next((int)timespan.TotalDays));
      }
   }
}
