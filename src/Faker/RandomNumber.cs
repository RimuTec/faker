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
      /// Returns a random number not exceeding a given maximum value.
      /// </summary>
      /// <param name="max">Value that the returned value must not exceed.</param>
      /// <returns>A random number not exceeding a given maximum value.</returns>
      public static int Next(int max) {
         return _random.Next(max);
      }

      /// <summary>
      /// Returns a random number that is within a specified range.
      /// </summary>
      /// <param name="min">Minimum value of the random number</param>
      /// <param name="max">Maximum value of the random number</param>
      /// <returns>A random number within the specified range.</returns>
      public static int Next(int min, int max) {
         return _random.Next(min, max);
      }

      private static Random _random = new Random();
   }
}
