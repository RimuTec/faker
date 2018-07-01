using System.Collections.Generic;
using System.Linq;

namespace RimuTec.Faker.Helper {
   internal class Range {
      public Range(string boundaries) {
         var parts = boundaries.ToCharArray().Except(",-").ToArray();
         _minValue = parts[0];
         _maxValue = parts[1];
      }

      public Range(string[] boundaries) {
         _minValue = boundaries[0].ToCharArray()[0];
         _maxValue = boundaries[1].ToCharArray()[0];
      }

      public Range(string min, string max) {
         _minValue = min.ToCharArray()[0];
         _maxValue = max.ToCharArray()[0];
      }

      public string[] ToArray() {
         var list = new List<string>();
         for (var c = _minValue; c <= _maxValue; c++) {
            list.Add(c.ToString());
         }
         return list.ToArray();
      }

      public int[] AsIntArray() {
         return ToArray().Select(x => int.Parse(x)).ToArray();
      }

      public string MinValue => _minValue.ToString();
      public string MaxValue => _maxValue.ToString();

      private char _minValue;
      private char _maxValue;
   }
}
