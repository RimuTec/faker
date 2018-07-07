using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class InternetTests {
      [Test]
      public void DomainName_HappyDays() {
         // arrange

         // act
         var domainName = Internet.DomainName();

         // assert
         AllAssertions(domainName);
         Assert.AreEqual(1, RegexMatchesCount(domainName, @"\."));
      }

      [Test]
      public void DomainSuffix_HappyDays() {
         // arrange

         // act
         var domainSuffix = Internet.DomainSuffix();

         // assert
         AllAssertions(domainSuffix);
      }

      [Test]
      public void DomainWord_HappyDays() {
         // arrange

         // act
         var domainWord = Internet.DomainWord();

         // assert
         AllAssertions(domainWord);
      }

      [Test]
      public void Email_Default_Values() {
         // arrange

         // act
         var emailAddress = Internet.Email();

         // assert
         AllAssertions(emailAddress);
         Assert.AreEqual(1, RegexMatchesCount(emailAddress, @"@"));
      }

      [Test]
      public void Email_With_Specific_Name() {
         // arrange
         var firstName = Name.FirstName(); // can also be any other name

         // act
         var emailAddress = Internet.Email(firstName);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith($"{firstName.ToLower()}@"), $"Email address is: '{emailAddress}'");
      }

      [Test]
      public void Email_With_Separator_Only() {
         // arrange
         string emailAddress;

         // act
         var tries = 0;
         do {
            emailAddress = Internet.Email(separators: "+");
         } while (!emailAddress.Contains("+") && tries++ < 100);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.Contains("+"));
      }

      [Test]
      public void Email_With_No_Separator_Guaranteed() {
         // arrange

         // act
         var tries = 100;
         while(tries-- > 0) {
            var emailAddress = Internet.Email(separators: "");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"[-_+]"), $"{nameof(emailAddress)} was {emailAddress}");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"\0"));
         }

         // assert
      }

      [Test]
      public void Email_With_Name_And_Separator() {
         // arrange
         var name = "Janelle Santiago";
         var separators = "+";

         // act
         var emailAddress = Internet.Email(name, separators);

         // assert
         Assert.IsTrue(Regex.Match(emailAddress, @".+\+.+@.+\.\w+").Success);
      }

      [Test]
      public void FreeEmail_With_Default_Values() {
         // arrange

         // act
         var emailAddress = Internet.FreeEmail();

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Desired_Name() {
         // arrange
         const string desiredFirstName = "Nancy";

         // act
         var emailAddress = Internet.FreeEmail(desiredFirstName);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith(desiredFirstName.ToLower()));
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Empty_String() {
         // arrange

         // act
         var emailAddress = Internet.FreeEmail(string.Empty);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, @".+.+@.+\.\w+").Success, $"{nameof(emailAddress)} is '{emailAddress}'");
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void IPv4Address_HappyDays() {
         // arrange

         // act
         var ipv4address = Internet.IPv4Address();

         // assert
         var parts = ipv4address.Split('.');
         foreach(var part in parts) {
            var value = int.Parse(part);
            Assert.Greater(value, 1);
            Assert.Less(value, 255);
         }
      }

      [Test]
      public void IPv4CIDR_HappyDays() {
         // arrange

         // act
         var ipV4cidr = Internet.IPv4CIDR();

         // assert
         Assert.AreEqual(1, RegexMatchesCount(ipV4cidr, @"/"));
         Assert.AreEqual(3, RegexMatchesCount(ipV4cidr, @"\."));
      }

      [Test]
      public void IPv6Address_HappyDays() {
         // arrange

         // act
         var ipV6Address = Internet.IPv6Address();

         // assert
         Assert.IsTrue(Regex.Match(ipV6Address, @"[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}").Success, 
            $"{nameof(ipV6Address)} is '{ipV6Address}'");
      }

      [Test]
      public void IPv6CIDR_HappyDays() {
         // arrange

         // act
         var ipV6withMask = Internet.IPv6CIDR();

         // assert
         Assert.IsTrue(Regex.Match(ipV6withMask, @"[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}/[0-9]{1,3}").Success,
            $"{nameof(ipV6withMask)} is '{ipV6withMask}'");
      }

      [Test]
      public void MacAddress_DefaultValues() {
         // arrange

         // act
         var macAddress = Internet.MacAddress();

         // assert
         Assert.AreEqual(5, RegexMatchesCount(macAddress, $":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, $"[0-9a-f]"));
      }

      [Test]
      public void MacAddress_WithPrefix() {
         // arrange
         const string prefix = "55:44:33";

         // act
         var macAddress = Internet.MacAddress(prefix);

         // assert
         Assert.IsTrue(macAddress.StartsWith(prefix + ":"), $"{nameof(macAddress)} is '{macAddress}'");
         Assert.AreEqual(5, RegexMatchesCount(macAddress, $":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, $"[0-9a-f]"));
      }

      [Test]
      public void MacAddress_Null_Prefix() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentNullException>(() => Internet.MacAddress(null));

         // assert
         Assert.AreEqual("Must not be null.\r\nParameter name: prefix", ex.Message);
      }

      [Test]
      public void PrivateIPv4Address_HappyDays() {
         // arrange

         // act
         var privateIpAddress = Internet.PrivateIPv4Address();

         // assert
         Assert.IsTrue(Internet.IsInPrivateNet(privateIpAddress), $"{nameof(privateIpAddress)} is '{privateIpAddress}'");
      }

      [Test]
      public void PublicIPv4Address_HappyDays() {
         // arrange

         // act
         var publicIpAddress = Internet.PublicIPv4Address();

         // assert
         Assert.IsFalse(Internet.IsInPrivateNet(publicIpAddress), $"{nameof(publicIpAddress)} is '{publicIpAddress}'");
         Assert.IsFalse(Internet.IsInReservedNet(publicIpAddress), $"{nameof(publicIpAddress)} is '{publicIpAddress}'");
      }

      [Test]
      public void Password_With_Default_Values() {
         // arrange

         // act
         var password = Internet.Password();

         // assert
         Assert.GreaterOrEqual(password.Length, 8);
         Assert.LessOrEqual(password.Length, 15);
         Assert.Greater(RegexMatchesCount(password, @"[A-Z]"), 0);
         Assert.Greater(RegexMatchesCount(password, @"[a-z]"), 0);
         Assert.Greater(RegexMatchesCount(password, @"[!@#$%^&*]"), 0, $"{nameof(password)} is '{password}'");
      }

      [Test]
      public void Password_With_Invalid_MinLength() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(minLength: 0));

         // assert
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void Password_With_Invalid_MaxLength() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(maxLength: 0));

         // assert
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: maxLength", ex.Message);
      }

      [Test]
      public void Password_With_MaxLength_Less_Than_MinLength() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(minLength: 5, maxLength: 4));

         // assert
         Assert.AreEqual("Must be equal to or greater than minLength.\r\nParameter name: maxLength", ex.Message);
      }

      [Test]
      public void Password_With_UpperCase_Letters() {
         // arrange

         // act
         var password = Internet.Password(mixCase: true, specialChars: false);

         // assert
         Assert.Greater(RegexMatchesCount(password, @"[A-Z]"), 0, $"{nameof(password)} is '{password}'");
      }

      [Test]
      public void Password_Without_UpperCase_Letters() {
         // arrange

         // act
         var password = Internet.Password(mixCase: false, specialChars: false);

         // assert
         Assert.AreEqual(0, RegexMatchesCount(password, @"[A-Z]"));
      }

      [Test]
      public void Password_With_SpecialChars() {
         // arrange

         // act
         var password = Internet.Password(specialChars: true);

         // assert
         Assert.Greater(RegexMatchesCount(password, @"[!@#$%^&*]"), 0);
      }

      [Test]
      public void Password_Without_SpecialChars() {
         // arrange

         // act
         var password = Internet.Password(specialChars: false);

         // assert
         Assert.AreEqual(0, RegexMatchesCount(password, @"[!@#$%^&*]"));
      }

      [Test]
      public void SafeEmail_With_Default_Values() {
         // arrange
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };

         // act
         var emailAddress = Internet.SafeEmail();

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void SafeEmail_With_Desired_Name() {
         // arrange
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };
         const string desiredFirstName = "Nancy";

         // act
         var emailAddress = Internet.SafeEmail(desiredFirstName);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith(desiredFirstName.ToLower()));
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void SafeEmail_With_Empty_String() {
         // arrange
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };

         // act
         var emailAddress = Internet.SafeEmail(string.Empty);

         // assert
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, @".+.+@.+\.\w+").Success, $"{nameof(emailAddress)} is '{emailAddress}'");
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void Url_With_Default_Values() {
         // arrange
         var tries = 100;

         // act
         while(tries-- > 0) {
            var url = Internet.Url();

            // assert
            Assert.AreEqual(1, RegexMatchesCount(url, @"[a-z]{3,}://([a-z_]{1,}.){1,}[a-z_]{1,}/([a-z_]{0,}.)[a-z_]{1,}"), $"{nameof(url)} is '{url}'");
         }
      }

      [Test]
      public void Url_With_Host() {
         // arrange
         var desiredHost = "abdcefg.org";

         // act
         var url = Internet.Url(desiredHost);

         // assert
         Assert.IsTrue(url.Contains($"://{desiredHost}/"));
      }

      [Test]
      public void Url_With_Path() {
         // arrange
         var desiredPath = "/Home/Index.html";

         // act
         var url = Internet.Url(path: desiredPath);

         // assert
         Assert.IsTrue(url.EndsWith(desiredPath));
      }

      [Test]
      public void Url_With_Scheme() {
         // arrange
         var desiredScheme = "ftp";

         // act
         var url = Internet.Url(scheme: desiredScheme);

         // assert
         Assert.IsTrue(url.StartsWith($"{desiredScheme}://"));
      }

      [Test]
      public void Url_With_Empty_Scheme() {
         // arrange
         var desiredScheme = string.Empty;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));

         // assert
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: scheme", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Scheme() {
         // arrange
         var desiredScheme = " ";

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));

         // assert
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: scheme", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Host() {
         // arrange
         var desiredHost = string.Empty;

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>Internet.Url(host: desiredHost));

         // assert
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: host", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Host() {
         // arrange
         var desiredHost = " ";

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(host: desiredHost));

         // assert
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: host", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Path() {
         // arrange
         var desiredPath = string.Empty;

         // act
         var url = Internet.Url(scheme: "ftp", host: "abcdef.org", path: desiredPath);

         // assert
         Assert.AreEqual("ftp://abcdef.org", url);
      }

      [Test]
      public void UserName_With_Default_Values() {
         // arrange
         var tries = 100;

         // act
         while(tries-- > 0) {
            var userName = Internet.UserName();

            // assert
            AllAssertions(userName);
         }
      }

      [Test]
      public void UserName_With_Given_Name() {
         // arrange

         // act
         var userName = Internet.UserName("Nancy");

         // assert
         AllAssertions(userName);
         Assert.AreEqual("nancy", userName);
      }

      [Test]
      public void UserName_With_Given_Separators() {
         // arrange
         string separators = "-_+";

         // act
         var userName = Internet.UserName(separators: separators);

         // assert
         AllAssertions(userName);
         Assert.GreaterOrEqual(1, RegexMatchesCount(userName, $@"[{separators}]"));
      }

      [Test]
      public void UserName_With_No_Separator_Guaranteed() {
         // arrange

         // act
         var userName = Internet.UserName(null, "");

         // assert
         AllAssertions(userName);
         Assert.AreEqual(0, RegexMatchesCount(userName, @"[-_+]"));
      }

      [Test]
      public void UserName_With_String_Argument() {
         // arrange
         var firstName = "bo";
         var lastName = "peep";
         var tries = 10;

         // act
         while (tries-- > 0) {
            var userName = Internet.UserName($"{firstName} {lastName}");

            // assert
            AllAssertions(userName);
            Assert.IsTrue(Regex.Match(userName, $@"({firstName}(_|\.){lastName}|{lastName}(_|\.){firstName})").Success, $"userName was '{userName}'");
         }
      }

      [Test]
      public void UserName_With_Min_Length() {
         // arrange
         const int minLength = 7;
         var tries = 10;

         // act
         while (tries-- > 0) {
            var userName = Internet.UserName(minLength);

            // assert
            Assert.GreaterOrEqual(userName.Length, minLength);
         }
      }

      [Test]
      public void UserName_With_Ridiculous_Value() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName((int)Math.Pow(10, 6) + 1));

         // assert
         Assert.AreEqual("Must be equal to or less than 10^6.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void UserName_With_Invalid_Value() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName(0));

         // assert
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void UserName_With_Min_Max_Length() {
         // arrange
         const int minLength = 5;
         const int maxLength = 8;

         // act
         var userName = Internet.UserName(minLength, maxLength);

         // assert
         int actualLength = userName.Length;
         Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
      }

      [Test]
      public void UserName_With_Common_Lengths() {
         for(int i = 1; i < 32; i++ ) {
            for(int j = i; j < i + 32; j++) {
               var minLength = i;
               var maxLength = j;
               var userName = Internet.UserName(minLength, maxLength);
               int actualLength = userName.Length;
               Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
            }
         }
      }

      [Test]
      public void UserName_With_Min_Greater_Than_Min() {
         // arrange

         // act
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName(7, 6));

         // assert
         Assert.AreEqual("maxLength must be equal to or greater than minLength.", ex.Message);
      }

      private static void AllAssertions(string candidate) {
         Assert.IsFalse(string.IsNullOrWhiteSpace(candidate));
         Assert.IsFalse(candidate.Contains("#"));
         Assert.IsFalse(candidate.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(candidate, @"[\s]").Count, $"Candidate was {candidate}");
         Assert.AreEqual(0, Regex.Matches(candidate, @"[A-Z]").Count, $"Candidate was {candidate}");
      }

      protected static int RegexMatchesCount(string input, string pattern) {
         return Regex.Matches(input, pattern, RegexOptions.Compiled).Count;
      }
   }
}
