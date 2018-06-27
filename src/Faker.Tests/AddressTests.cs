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

      [Test]
      public void Community_HappyDays() {
         // arrange

         // act
         var community = Address.Community();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(community));
         Assert.IsFalse(community.Contains("#"));
         Assert.IsFalse(community.Contains("?"));
         Assert.AreEqual(1, Regex.Matches(community, @" ").Count);
      }

      [Test]
      public void TimeZone_HappyDays() {
         // arrange

         // act
         var timezone = Address.TimeZone();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(timezone));
         Assert.IsFalse(timezone.Contains("#"));
         Assert.IsFalse(timezone.Contains("?"));
      }

      [Test]
      public void ZipCode_With_Default_Value() {
         // arrange

         // act
         var zipCode = Address.ZipCode();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(zipCode));
         Assert.IsFalse(zipCode.Contains("#"));
         Assert.IsFalse(zipCode.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(zipCode, @"[0-9]").Count, 3);
      }

      [Test]
      public void ZipCode_For_Specific_US_State() {
         // arrange

         // act
         var zipCode = Address.ZipCode("ME");

         // assert
         Assert.IsTrue(zipCode.StartsWith("042"));
      }

      [Test]
      public void ZipCode_For_Invalid_State_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.ZipCode("ZZZ"));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
      }

      [Test]
      public void ZipCode_With_Null_As_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.ZipCode(null));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
      }
   }
}
