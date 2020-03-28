using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System;
using System.Collections.Generic;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generator for weather related data.
   /// </summary>
   public class Weather : GeneratorBase<Weather>
   {
      /// <summary>
      /// Generates a weather summary, e.g. 'Sweltering'
      /// </summary>
      /// <returns></returns>
      public static string Summary()
      {
         return Fetch("weather.summary");
      }

      /// <summary>
      /// Generates a random weather forecast for 5 days.
      /// </summary>
      /// <returns></returns>
      public static IEnumerable<WeatherForecast> Forecast(int numberOfDays = 5)
      {
         if(numberOfDays < 0)
         {
            throw new ArgumentOutOfRangeException(nameof(numberOfDays), "Must not be negative.");
         }
         var temperatureRangeC = new Range2<int>(-20, 50);
         var result = new List<WeatherForecast>();
         result.AddRange(numberOfDays.Times(x => new WeatherForecast
         {
            Date = DateTime.UtcNow.AddDays(x),
            TemperatureC = temperatureRangeC.Sample(),
            Summary = Summary()
         })); ;
         return result;
      }
   }

   /// <summary>
   /// Each instance represents the forecast for one day.
   /// </summary>
   public class WeatherForecast
   {
      /// <summary>
      /// Date in UTC
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// Temperature in Celsius
      /// </summary>
      public int TemperatureC { get; set; }

      /// <summary>
      /// Temperature in Fahrenheit
      /// </summary>
      public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

      /// <summary>
      /// Summary, e.g. "Freezing", "Warm", etc.
      /// </summary>
      public string Summary { get; set; }
   }
}
