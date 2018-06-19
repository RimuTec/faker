﻿using NUnit.Framework;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class JobTests {
      [Test]
      public void Title_HappyDays() {
         // arrange

         // act
         var title = Job.Title();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(title));
         Assert.IsTrue(title.Contains(" "));
      }

      [Test]
      public void Title_Twice_NotEqual() {
         // arrange

         // act
         var title1 = Job.Title();
         var title2 = Job.Title();

         // assert
         Assert.AreNotEqual(title1, title2);
      }
   }
}