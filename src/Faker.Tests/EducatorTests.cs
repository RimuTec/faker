using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class EducatorTests {
      [Test]
      public void University_Happy_Days() {
         // arrange

         // act
         var university = Educator.University();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(university));
      }

      [Test]
      public void SecondarySchool_Happy_Days() {
         // arrange

         // act
         var secondarySchool = Educator.SecondarySchool();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(secondarySchool));
      }
   }
}
