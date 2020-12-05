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
      /// Returns a random color name. Example: "yellow"
      /// </summary>
      /// <returns></returns>
      public static string ColorName()
      {
         return Fetch("color.name");
      }

      /// <summary>
      /// Returns a random hex color, e.g. "45AF55"
      /// </summary>
      /// <returns></returns>
      public static string HexColor()
      {
         var random = RandomNumber.Next(0, (int)Math.Pow(16, 6));
         return $"{random:X}".PadLeft(6, '0');
      }

      /// <summary>
      /// Returns an array with three elements, representing an RGB value.
      /// </summary>
      /// <returns></returns>
      public static byte[] RgbColor()
      {
         return new byte[] { Convert.ToByte(RandomNumber.Next(0, 256)), Convert.ToByte(RandomNumber.Next(0, 256)), Convert.ToByte(RandomNumber.Next(0, 256)) };
      }
   }
}
