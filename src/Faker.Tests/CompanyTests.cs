using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class CompanyTests {
      [Test]
      public void Name_HappyDays() {
         // arrange

         // act
         var companyName = Company.Name();

         // assert
         Assert.IsTrue(new List<Func<bool>> {
                                  () => Regex.IsMatch(companyName, @"\w+ \w+"),
                                  () => Regex.IsMatch(companyName, @"\w+-\w+"),
                                  () => Regex.IsMatch(companyName, @"\w+, \w+ and \w+")
                              }.Any(x => x.Invoke()));
      }
   }
}

