using System;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generates colors in different color models.
   /// </summary>
   public class Color : GeneratorBase<Color>
   {
      private Color() { }

      /// <summary>
      /// Returns a random hex color, e.g. "45AF55"
      /// </summary>
      /// <returns></returns>
      public static string HexColor()
      {
         var random = RandomNumber.Next(0, (int) Math.Pow(16, 6));
         return $"{random:X}".PadLeft(6, '0');
      }
   }
}
