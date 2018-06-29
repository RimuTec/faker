using System;

namespace RimuTec.Faker {
   /// <summary>
   /// Provide access to random number generator.
   /// </summary>
   public static class RandomNumber {
      /// <summary>
      /// Resets the random number generator and intializes it with a seed.
      /// </summary>
      /// <param name="seed"></param>
      public static void ResetSeed(int seed) {
         _random = new Random(seed);
      }

      /// <summary>
      /// Returns the next random number.
      /// </summary>
      /// <returns>An integer</returns>
      public static int Next() {
         return _random.Next();
      }

      /// <summary>
      /// Returns a random integer that is less than the specified maximum.
      /// </summary>
      /// <param name="maxValue">Upper limit of random integer to return excluding this value.</param>
      /// <returns>A random number not exceeding a given maximum value.</returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static int Next(int maxValue) {
         return _random.Next(maxValue);
      }

      /// <summary>
      /// Returns a random integer that is within a specified range. Includes the lower boundary and excludes the upper boundary.
      /// </summary>
      /// <param name="minValue">Lower limit of random integer to return including this value.</param>
      /// <param name="maxValue">Upper limit of random integer to return excluding this value.</param>
      /// <returns>A random number within the specified range.</returns>
      /// <exception cref="ArgumentOutOfRangeException"></exception>
      public static int Next(int minValue, int maxValue) {
         return _random.Next(minValue, maxValue);
      }

      /// <summary>
      /// Returns a random double that is greater than or equal to 0.0 and less than 1.0.
      /// </summary>
      /// <returns></returns>
      public static double NextDouble() {
         return _random.NextDouble();
      }

      // ThreadStatic added, ref: https://stackoverflow.com/a/1262619/411428
      [ThreadStatic] private static Random _random = new Random();
   }
}
