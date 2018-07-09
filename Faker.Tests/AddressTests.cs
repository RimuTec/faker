using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class AddressTests {
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
      public void City_HappyDays() {
         // arrange

         // act
         var city = Address.City();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(city));
         Assert.IsFalse(city.Contains("#"), $"Incorrect value is: '{city}'");
         Assert.IsFalse(city.Contains("?"), $"Incorrect value is: '{city}'");
      }

      [Test]
      public void CityPrefix_HappyDays() {
         // arrange

         // act
         var cityPrefix = Address.CityPrefix();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(cityPrefix));
         Assert.IsFalse(cityPrefix.Contains("#"));
         Assert.IsFalse(cityPrefix.Contains("?"));
         Assert.IsTrue(Address._cityPrefix.Contains(cityPrefix));
      }

      [Test]
      public void CitySuffix_HappyDays() {
         // arrange

         // act
         var citySuffix = Address.CitySuffix();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(citySuffix));
         Assert.IsFalse(citySuffix.Contains("#"));
         Assert.IsFalse(citySuffix.Contains("?"));
         Assert.IsTrue(Address._citySuffix.Contains(citySuffix));
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
      public void Country_HappyDays() {
         // arrange

         // act
         var country = Address.Country();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(country));
         Assert.IsFalse(country.Contains("#"));
         Assert.IsFalse(country.Contains("?"));
         Assert.IsTrue(Address._country.Contains(country));
      }

      [Test]
      public void CountryCode_HappyDays() {
         // arrange

         // act
         var countryCode = Address.CountryCode();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(countryCode));
         Assert.IsFalse(countryCode.Contains("#"));
         Assert.IsFalse(countryCode.Contains("?"));
         Assert.IsTrue(Address._countryCode.Contains(countryCode));
      }

      [Test]
      public void CountryCodeLong_HappyDays() {
         // arrange

         // act
         var countryCodeLong = Address.CountryCodeLong();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(countryCodeLong));
         Assert.IsFalse(countryCodeLong.Contains("#"));
         Assert.IsFalse(countryCodeLong.Contains("?"));
         Assert.IsTrue(Address._countryCodeLong.Contains(countryCodeLong));
      }

      [Test]
      public void FullAddress_HappyDays() {
         // arrange

         // act
         var fullAddress = Address.FullAddress();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(fullAddress));
         Assert.IsFalse(fullAddress.Contains("#"), $"Incorrect value is: '{fullAddress}'");
         Assert.IsFalse(fullAddress.Contains("?"), $"Incorrect value is: '{fullAddress}'");
      }

      [Test]
      public void Latitude_HappyDays() {
         // arrange
         var tries = RandomNumber.Next(5, 15);
         while (tries-- > 0) {

            // act
            var latitude = Address.Latitude();

            // assert
            Assert.GreaterOrEqual(latitude, -90);
            Assert.Less(latitude, 90);
         }
      }

      [Test]
      public void Longitude_HappyDays() {
         // arrange
         var tries = RandomNumber.Next(5, 15);
         while (tries-- > 0) {

            // act
            var longitude = Address.Longitude();

            // assert
            Assert.GreaterOrEqual(longitude, -180);
            Assert.Less(longitude, 180);
         }
      }

      [Test]
      public void Postcode_With_Default_Value() {
         // arrange

         // act
         var postcode = Address.Postcode();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(postcode));
         Assert.IsFalse(postcode.Contains("#"));
         Assert.IsFalse(postcode.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(postcode, @"[0-9]").Count, 3);
      }

      [Test]
      public void Postcode_For_Specific_US_State() {
         // arrange

         // act
         var postcode = Address.Postcode("ME");

         // assert
         Assert.IsTrue(postcode.StartsWith("042"));
      }

      [Test]
      public void Postcode_For_Invalid_State_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Postcode("ZZZ"));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
      }

      [Test]
      public void Postcode_With_Null_As_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Postcode(null));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
      }

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
      public void State_HappyDays() {
         // arrange

         // act
         var state = Address.State();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(state));
         Assert.IsFalse(state.Contains("#"));
         Assert.IsFalse(state.Contains("?"));
         Assert.IsTrue(Address._state.Contains(state));
      }

      [Test]
      public void StateAbbr_HappyDays() {
         // arrange

         // act
         var stateAbbr = Address.StateAbbr();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(stateAbbr));
         Assert.IsFalse(stateAbbr.Contains("#"));
         Assert.IsFalse(stateAbbr.Contains("?"));
         Assert.IsTrue(Address._stateAbbr.Contains(stateAbbr));
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
      public void StreetSuffix_HappyDays() {
         // arrange

         // act
         var suffix = Address.StreetSuffix();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(suffix));
         Assert.IsFalse(suffix.Contains("#"));
         Assert.IsFalse(suffix.Contains("?"));
         Assert.IsTrue(Address._streetSuffix.Contains(suffix));
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
      public void Zip_With_Default_Value() {
         // arrange

         // act
         var zip = Address.Zip();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(zip));
         Assert.IsFalse(zip.Contains("#"));
         Assert.IsFalse(zip.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(zip, @"[0-9]").Count, 3);
      }

      [Test]
      public void Zip_For_Specific_US_State() {
         // arrange

         // act
         var zip = Address.Zip("ME");

         // assert
         Assert.IsTrue(zip.StartsWith("042"));
      }

      [Test]
      public void Zip_For_Invalid_State_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Zip("ZZZ"));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
      }

      [Test]
      public void Zip_With_Null_As_Abbreviation() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Zip(null));

         // assert
         Assert.AreEqual("Must be one of the US state abbreviations or empty.\r\nParameter name: stateAbbreviation", ex.Message);
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
