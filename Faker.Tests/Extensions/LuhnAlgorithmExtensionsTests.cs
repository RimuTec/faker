using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;

namespace RimuTec.Faker.Tests.Helper {
   [TestFixture]
   public class LuhnAlgorithmExtensionsTests {
      // Migrated from XUnit tests at https://stackoverflow.com/a/23640453/411428

      [Test]
      public void ComputeCheckDigits() {
         Assert.AreEqual(0, (new List<int> { 0 }).CheckDigit());
         Assert.AreEqual(8, (new List<int> { 1 }).CheckDigit());
         Assert.AreEqual(6, (new List<int> { 2 }).CheckDigit());
         Assert.AreEqual(0, (new List<int> { 3, 6, 1, 5, 5 }).CheckDigit());
         Assert.AreEqual(0, 36155.CheckDigit());
         Assert.AreEqual(8, (new List<int> { 3, 6, 1, 5, 6 }).CheckDigit());
         Assert.AreEqual(8, 36156.CheckDigit());
         Assert.AreEqual(6, 36157.CheckDigit());
         Assert.AreEqual("6", "36157".CheckDigit());
         Assert.AreEqual("3", "7992739871".CheckDigit());
      }

      [Test]
      public void ValidateCheckDigits() {
         Assert.IsTrue((new List<int> { 3, 6, 1, 5, 6, 8 }).HasValidCheckDigit());
         Assert.IsTrue(361568.HasValidCheckDigit());
         Assert.IsTrue("361568".HasValidCheckDigit());
         Assert.IsTrue("79927398713".HasValidCheckDigit());
      }

      [Test]
      public void AppendCheckDigits() {
         Console.WriteLine("36156".CheckDigit());
         Console.WriteLine("36156".AppendCheckDigit());
         Assert.AreEqual("361568", "36156".AppendCheckDigit());
         Assert.AreEqual("79927398713", "7992739871".AppendCheckDigit());
      }
   }
}
