using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class AddressTests : FixtureBase
   {
      [Test]
      public void BuildingNumber_HappyDays()
      {
         var buildingNumber = Address.BuildingNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(buildingNumber));
         Assert.IsFalse(buildingNumber.Contains("#"));
         Assert.IsFalse(buildingNumber.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(buildingNumber, @"[0-9]").Count, 3);
      }

      [Test]
      public void City_HappyDays()
      {
         var city = Address.City();
         Assert.IsFalse(string.IsNullOrWhiteSpace(city));
         Assert.IsFalse(city.Contains("#"), $"Incorrect value is: '{city}'");
         Assert.IsFalse(city.Contains("?"), $"Incorrect value is: '{city}'");
      }

      [Test]
      public void CityPrefix_HappyDays()
      {
         var cityPrefix = Address.CityPrefix();
         Assert.IsFalse(string.IsNullOrWhiteSpace(cityPrefix));
         Assert.IsFalse(cityPrefix.Contains("#"));
         Assert.IsFalse(cityPrefix.Contains("?"));
         var cityPrefixes = Fetch("address.city_prefix");
         Assert.IsTrue(cityPrefixes.Contains(cityPrefix));
      }

      [Test]
      public void CitySuffix_HappyDays()
      {
         var citySuffix = Address.CitySuffix();
         Assert.IsFalse(string.IsNullOrWhiteSpace(citySuffix));
         Assert.IsFalse(citySuffix.Contains("#"));
         Assert.IsFalse(citySuffix.Contains("?"));
         var citySuffixes = Fetch("address.city_suffix");
         Assert.IsTrue(citySuffixes.Contains(citySuffix));
      }

      [Test]
      public void Community_HappyDays()
      {
         var community = Address.Community();
         Assert.IsFalse(string.IsNullOrWhiteSpace(community));
         Assert.IsFalse(community.Contains("#"));
         Assert.IsFalse(community.Contains("?"));
         Assert.AreEqual(1, Regex.Matches(community, @" ").Count);
      }

      [Test]
      public void Country_HappyDays()
      {
         var country = Address.Country();
         Assert.IsFalse(string.IsNullOrWhiteSpace(country));
         Assert.IsFalse(country.Contains("#"));
         Assert.IsFalse(country.Contains("?"));
         var countries = Fetch("address.country");
         Assert.IsTrue(countries.Contains(country));
      }

      [Test]
      public void CountryCode_HappyDays()
      {
         var countryCode = Address.CountryCode();
         Assert.IsFalse(string.IsNullOrWhiteSpace(countryCode));
         Assert.IsFalse(countryCode.Contains("#"));
         Assert.IsFalse(countryCode.Contains("?"));
         var countryCodes = Fetch("address.country_code");
         Assert.IsTrue(countryCodes.Contains(countryCode));
      }

      [Test]
      public void CountryCodeLong_HappyDays()
      {
         var countryCodeLong = Address.CountryCodeLong();
         Assert.IsFalse(string.IsNullOrWhiteSpace(countryCodeLong));
         Assert.IsFalse(countryCodeLong.Contains("#"));
         Assert.IsFalse(countryCodeLong.Contains("?"));
         var countryCodesLong = Fetch("address.country_code_long");
         Assert.IsTrue(countryCodesLong.Contains(countryCodeLong));
      }

      [Test]
      public void FullAddress_HappyDays()
      {
         var fullAddress = Address.FullAddress();
         Assert.IsFalse(string.IsNullOrWhiteSpace(fullAddress));
         Assert.IsFalse(fullAddress.Contains("#"), $"Incorrect value is: '{fullAddress}'");
         Assert.IsFalse(fullAddress.Contains("?"), $"Incorrect value is: '{fullAddress}'");
      }

      [Test]
      public void Latitude_HappyDays()
      {
         var tries = RandomNumber.Next(5, 15);
         while (tries-- > 0)
         {
            var latitude = Address.Latitude();
            Assert.GreaterOrEqual(latitude, -90);
            Assert.Less(latitude, 90);
         }
      }

      [Test]
      public void Longitude_HappyDays()
      {
         var tries = RandomNumber.Next(5, 15);
         while (tries-- > 0)
         {
            var longitude = Address.Longitude();
            Assert.GreaterOrEqual(longitude, -180);
            Assert.Less(longitude, 180);
         }
      }

      [Test]
      public void Postcode_With_Default_Value()
      {
         var postcode = Address.Postcode();
         Assert.IsFalse(string.IsNullOrWhiteSpace(postcode));
         Assert.IsFalse(postcode.Contains("#"));
         Assert.IsFalse(postcode.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(postcode, @"[0-9]").Count, 3);
      }

      [Test]
      public void Postcode_For_Specific_US_State()
      {
         var postcode = Address.Postcode("ME");
         Assert.IsTrue(postcode.Length == 5 || postcode.Length == 10);
         Assert.IsTrue(postcode.StartsWith("042"));
      }

      [Test]
      public void Postcode_For_Invalid_State_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Postcode("ZZZ"));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }

      [Test]
      public void Postcode_With_Null_As_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Postcode(null));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }

      [Test]
      public void SecondaryAddress_HappyDays()
      {
         var secondaryAddress = Address.SecondaryAddress();
         Assert.IsFalse(string.IsNullOrWhiteSpace(secondaryAddress));
         Assert.IsFalse(secondaryAddress.Contains("#"));
         Assert.IsFalse(secondaryAddress.Contains("?"));
      }

      [Test]
      public void State_HappyDays()
      {
         var state = Address.State();
         Assert.IsFalse(string.IsNullOrWhiteSpace(state));
         Assert.IsFalse(state.Contains("#"));
         Assert.IsFalse(state.Contains("?"));
         var states = Fetch("address.state");
         Assert.IsTrue(states.Contains(state));
      }

      [Test]
      public void StateAbbr_HappyDays()
      {
         var stateAbbr = Address.StateAbbr();
         Assert.IsFalse(string.IsNullOrWhiteSpace(stateAbbr));
         Assert.IsFalse(stateAbbr.Contains("#"));
         Assert.IsFalse(stateAbbr.Contains("?"));
         var stateAbbrs = Fetch("address.state_abbr");
         Assert.IsTrue(stateAbbrs.Contains(stateAbbr));
      }

      [Test]
      public void StreetAddress_With_Default_Values()
      {
         var assembly = typeof(Address).Assembly;
         var streetAddress = Address.StreetAddress();
         Assert.IsFalse(string.IsNullOrWhiteSpace(streetAddress));
         Assert.IsFalse(streetAddress.Contains("#"));
         Assert.IsFalse(streetAddress.Contains("?"));
      }

      [Test]
      public void StreetAddress_With_SecondaryAddress()
      {
         var streetAddress = Address.StreetAddress(true);
         Assert.IsFalse(streetAddress.Contains("#"));
         Assert.IsFalse(streetAddress.Contains("?"));
         var words = streetAddress.Split(' ');
         var suffixes = Fetch("address.street_suffix");
         var intersect = words.Intersect(suffixes);
         Assert.Greater(intersect.Count(), 0);
      }

      [Test]
      public void StreetName_HappyDays()
      {
         var streetName = Address.StreetName();
         Assert.IsFalse(string.IsNullOrWhiteSpace(streetName));
         Assert.IsFalse(streetName.Contains("#"));
         Assert.IsFalse(streetName.Contains("?"));
      }

      [Test]
      public void StreetSuffix_HappyDays()
      {
         var suffix = Address.StreetSuffix();
         Assert.IsFalse(string.IsNullOrWhiteSpace(suffix));
         Assert.IsFalse(suffix.Contains("#"));
         Assert.IsFalse(suffix.Contains("?"));
         var suffixes = Fetch("address.street_suffix");
         Assert.IsTrue(suffixes.Contains(suffix));
      }

      [Test]
      public void TimeZone_HappyDays()
      {
         var timezone = Address.TimeZone();
         Assert.IsFalse(string.IsNullOrWhiteSpace(timezone));
         Assert.IsFalse(timezone.Contains("#"));
         Assert.IsFalse(timezone.Contains("?"));
      }

      [Test]
      public void Zip_With_Default_Value()
      {
         var zip = Address.Zip();
         Assert.IsFalse(string.IsNullOrWhiteSpace(zip));
         Assert.IsFalse(zip.Contains("#"));
         Assert.IsFalse(zip.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(zip, @"[0-9]").Count, 3);
      }

      [Test]
      public void Zip_For_Specific_US_State()
      {
         var zip = Address.Zip("ME");
         Assert.IsFalse(zip.Contains("#"));
         Assert.IsFalse(zip.Contains("?"));
         Assert.IsTrue(zip.StartsWith("042"));
      }

      [Test]
      public void Zip_For_Invalid_State_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Zip("ZZZ"));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }

      [Test]
      public void Zip_With_Null_As_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.Zip(null));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }

      [Test]
      public void ZipCode_With_Default_Value()
      {
         var zipCode = Address.ZipCode();
         Assert.IsFalse(string.IsNullOrWhiteSpace(zipCode));
         Assert.IsFalse(zipCode.Contains("#"));
         Assert.IsFalse(zipCode.Contains("?"));
         Assert.GreaterOrEqual(Regex.Matches(zipCode, @"[0-9]").Count, 3);
      }

      [Test]
      public void ZipCode_For_Specific_US_State()
      {
         var zipCode = Address.ZipCode("ME");
         Assert.IsFalse(zipCode.Contains("#"));
         Assert.IsFalse(zipCode.Contains("?"));
         Assert.IsTrue(zipCode.StartsWith("042"));
      }

      [Test]
      public void ZipCode_For_Invalid_State_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.ZipCode("ZZZ"));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }

      [Test]
      public void ZipCode_With_Null_As_Abbreviation()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Address.ZipCode(null));
         Assert.AreEqual("Must be one of the US state abbreviations or empty. (Parameter 'stateAbbreviation')", ex.Message);
      }
   }
}
