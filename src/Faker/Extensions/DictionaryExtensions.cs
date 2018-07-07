using System.Collections.Generic;

namespace RimuTec.Faker.Extensions {
   internal static class DictionaryExtensions {
      public static KeyValuePair<TKey, TValue> Sample<TKey, TValue>(this Dictionary<TKey, TValue> dictionary) {
         var index = RandomNumber.Next(0, dictionary.Count);
         var keys = new List<TKey>(dictionary.Keys);
         var key = keys[index];
         var pair = new KeyValuePair<TKey, TValue>(key, dictionary[key]);
         return pair;
      }
   }
}
