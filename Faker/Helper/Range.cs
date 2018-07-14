using RimuTec.Faker.Extensions;

namespace RimuTec.Faker.Helper
{
   /// <summary>
   /// Range of integers or chars, e.g. 1-4 or a-f
   /// </summary>
   /// <typeparam name="T"></typeparam>
   internal class Range2<T> where T : new()
   {
      public Range2(T minValue, T maxValue)
      {
         MinValue = minValue;
         MaxValue = maxValue;
      }

      public Range2(T[] boundaries) : this(boundaries[0], boundaries[1]) { }

      public T[] AsArray()
      {
         var min = (dynamic)MinValue;
         var max = (dynamic)MaxValue;
         var arr = new T[max - min + 1];
         var currentValue = min;
         for (var i = 0; i <= max - min; i++)
         {
            arr[i] = currentValue;
            currentValue++;
         }
         return arr;
      }

      public T Sample()
      {
         return AsArray().Sample();
      }

      public int Length()
      {
         return AsArray().Length;
      }

      public T MinValue { get; }
      public T MaxValue { get; }
   }
}
