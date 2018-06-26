using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class AddressTests {
      [Test]
      public void SecondaryAddress_HappyDays() {
         // arrange

         // act
         var secondaryAddress = Address.SecondaryAddress();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(secondaryAddress));
         Assert.IsFalse(secondaryAddress.Contains("#"));
         Assert.IsFalse(secondaryAddress.Contains("?"));
      }

      [Test]
      public void StreetAddress_With_Default_Values() {
         // arrange
         var assembly = typeof(Address).Assembly;

         // act
         var streetAddress = Address.StreetAddress();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(streetAddress));
         Assert.IsFalse(streetAddress.Contains("#"));
         Assert.IsFalse(streetAddress.Contains("?"));
      }

      [Test]
      public void StreetAddress_With_SecondaryAddress() {
         // arrange

         // act
         var streetAddress = Address.StreetAddress(true);

         // assert
         Assert.IsFalse(streetAddress.Contains("#"));
         Assert.IsFalse(streetAddress.Contains("?"));
         var words = streetAddress.Split(' ');
         var intersect = words.Intersect(Address._streetSuffix);
         Assert.Greater(intersect.Count(), 0);
      }

      [Test]
      public void StreetName_HappyDays() {
         // arrange

         // act
         var streetName = Address.StreetName();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(streetName));
         Assert.IsFalse(streetName.Contains("#"));
         Assert.IsFalse(streetName.Contains("?"));
      }

      [Test]
      public void BuildingNumber_HappyDays() {
         // arrange

         // act
         var buildingNumber = Address.BuildingNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(buildingNumber));
         Assert.IsFalse(buildingNumber.Contains("#"));
         Assert.IsFalse(buildingNumber.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(buildingNumber, @"[0-9]").Count, 3);
      }
   }
}
