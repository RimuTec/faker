using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class InternetTests : FixtureBase
   {
      [Test]
      public void DomainName_HappyDays()
      {
         var domainName = Internet.DomainName();
         AllAssertions(domainName);
         Assert.AreEqual(1, RegexMatchesCount(domainName, @"\."));
      }

      [Test]
      public void DomainSuffix_HappyDays()
      {
         var domainSuffix = Internet.DomainSuffix();
         AllAssertions(domainSuffix);
      }

      [Test]
      public void DomainWord_HappyDays()
      {
         var domainWord = Internet.DomainWord();
         AllAssertions(domainWord);
      }

      [Test]
      public void Email_Default_Values()
      {
         var emailAddress = Internet.Email();
         AllAssertions(emailAddress);
         Assert.AreEqual(1, RegexMatchesCount(emailAddress, @"@"));
      }

      [Test]
      public void Email_With_Specific_Name()
      {
         var firstName = Name.FirstName(); // can also be any other name
         var emailAddress = Internet.Email(firstName);
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith($"{firstName.ToLower()}@"), $"Email address is: '{emailAddress}'");
      }

      [Test]
      public void Email_With_Separator_Only()
      {
         string emailAddress;
         var tries = 0;
         do
         {
            emailAddress = Internet.Email(separators: "+");
         } while (!emailAddress.Contains("+") && tries++ < 100);
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.Contains("+"));
      }

      [Test]
      public void Email_With_No_Separator_Guaranteed()
      {
         var tries = 100;
         while (tries-- > 0)
         {
            var emailAddress = Internet.Email(separators: "");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"[-_+]"), $"{nameof(emailAddress)} was {emailAddress}");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"\0"));
         }
      }

      [Test]
      public void Email_With_Name_And_Separator()
      {
         var name = "Janelle Santiago";
         var separators = "+";
         var emailAddress = Internet.Email(name, separators);
         Assert.IsTrue(Regex.Match(emailAddress, @".+\+.+@.+\.\w+").Success);
      }

      [Test]
      public void FreeEmail_With_Default_Values()
      {
         var emailAddress = Internet.FreeEmail();
         AllAssertions(emailAddress);
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Desired_Name()
      {
         const string desiredFirstName = "Nancy";
         var emailAddress = Internet.FreeEmail(desiredFirstName);
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith(desiredFirstName.ToLower()));
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Empty_String()
      {
         var emailAddress = Internet.FreeEmail(string.Empty);
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, @".+.+@.+\.\w+").Success, $"{nameof(emailAddress)} is '{emailAddress}'");
         Assert.IsTrue(Internet._freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void IPv4Address_HappyDays()
      {
         var tries = 100;
         while (tries-- > 0)
         {
            var ipv4address = Internet.IPv4Address();
            var parts = ipv4address.Split('.');
            foreach (var part in parts)
            {
               var value = int.Parse(part);
               Assert.GreaterOrEqual(value, 1, $"{nameof(ipv4address)} is '{ipv4address}'");
               Assert.LessOrEqual(value, 254, $"{nameof(ipv4address)} is '{ipv4address}'");
            }
         }
      }

      [Test]
      public void IPv4CIDR_HappyDays()
      {
         var ipV4cidr = Internet.IPv4CIDR();
         Assert.AreEqual(1, RegexMatchesCount(ipV4cidr, @"/"));
         Assert.AreEqual(3, RegexMatchesCount(ipV4cidr, @"\."));
      }

      [Test]
      public void IPv6Address_HappyDays()
      {
         var ipV6Address = Internet.IPv6Address();
         Assert.IsTrue(Regex.Match(ipV6Address, @"[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}").Success,
            $"{nameof(ipV6Address)} is '{ipV6Address}'");
      }

      [Test]
      public void IPv6CIDR_HappyDays()
      {
         var ipV6withMask = Internet.IPv6CIDR();
         Assert.IsTrue(Regex.Match(ipV6withMask, @"[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}/[0-9]{1,3}").Success,
            $"{nameof(ipV6withMask)} is '{ipV6withMask}'");
      }

      [Test]
      public void MacAddress_DefaultValues()
      {
         var macAddress = Internet.MacAddress();
         Assert.AreEqual(5, RegexMatchesCount(macAddress, $":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, $"[0-9a-f]"));
      }

      [Test]
      public void MacAddress_WithPrefix()
      {
         const string prefix = "55:44:33";
         var macAddress = Internet.MacAddress(prefix);
         Assert.IsTrue(macAddress.StartsWith(prefix + ":"), $"{nameof(macAddress)} is '{macAddress}'");
         Assert.AreEqual(5, RegexMatchesCount(macAddress, $":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, $"[0-9a-f]"));
      }

      [Test]
      public void MacAddress_Null_Prefix()
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Internet.MacAddress(null));
         Assert.AreEqual("Must not be null.\r\nParameter name: prefix", ex.Message);
      }

      [Test]
      public void PrivateIPv4Address_HappyDays()
      {
         var privateIpAddress = Internet.PrivateIPv4Address();
         Assert.IsTrue(Internet.IsInPrivateNet(privateIpAddress), $"{nameof(privateIpAddress)} is '{privateIpAddress}'");
      }

      [Test]
      public void PublicIPv4Address_HappyDays()
      {
         var publicIpAddress = Internet.PublicIPv4Address();
         Assert.IsFalse(Internet.IsInPrivateNet(publicIpAddress), $"{nameof(publicIpAddress)} is '{publicIpAddress}'");
         Assert.IsFalse(Internet.IsInReservedNet(publicIpAddress), $"{nameof(publicIpAddress)} is '{publicIpAddress}'");
      }

      [Test]
      public void Password_With_Default_Values()
      {
         bool usesLowerCase = false;
         bool usesUpperCase = false;
         bool usesSpecialChars = false;
         var tries = 100;
         while (tries-- > 0)
         {
            var password = Internet.Password();
            Assert.GreaterOrEqual(password.Length, 8);
            Assert.LessOrEqual(password.Length, 15);
            if (RegexMatchesCount(password, @"[A-Z]") > 0)
            {
               usesUpperCase = true;
            }
            if (RegexMatchesCount(password, @"[a-z]") > 0)
            {
               usesLowerCase = true;
            }
            if (RegexMatchesCount(password, @"[!@#$%^&*]") > 0)
            {
               usesSpecialChars = true;
            }
            if (usesLowerCase && usesUpperCase && usesSpecialChars)
            {
               break;
            }
         }
         Assert.IsTrue(usesLowerCase && usesUpperCase && usesSpecialChars);
      }

      [Test]
      public void Password_With_Invalid_MinLength()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(minLength: 0));
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void Password_With_Invalid_MaxLength()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(maxLength: 0));
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: maxLength", ex.Message);
      }

      [Test]
      public void Password_With_MaxLength_Less_Than_MinLength()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(minLength: 5, maxLength: 4));
         Assert.AreEqual("Must be equal to or greater than minLength.\r\nParameter name: maxLength", ex.Message);
      }

      [Test]
      public void Password_With_UpperCase_Letters()
      {
         var password = Internet.Password(mixCase: true, specialChars: false);
         Assert.Greater(RegexMatchesCount(password, @"[A-Z]"), 0, $"{nameof(password)} is '{password}'");
      }

      [Test]
      public void Password_Without_UpperCase_Letters()
      {
         var password = Internet.Password(mixCase: false, specialChars: false);
         Assert.AreEqual(0, RegexMatchesCount(password, @"[A-Z]"));
      }

      [Test]
      public void Password_With_SpecialChars()
      {
         var password = Internet.Password(specialChars: true);
         Assert.Greater(RegexMatchesCount(password, @"[!@#$%^&*]"), 0);
      }

      [Test]
      public void Password_Without_SpecialChars()
      {
         var password = Internet.Password(specialChars: false);
         Assert.AreEqual(0, RegexMatchesCount(password, @"[!@#$%^&*]"));
      }

      [Test]
      public void SafeEmail_With_Default_Values()
      {
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };
         var emailAddress = Internet.SafeEmail();
         AllAssertions(emailAddress);
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void SafeEmail_With_Desired_Name()
      {
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };
         const string desiredFirstName = "Nancy";
         var emailAddress = Internet.SafeEmail(desiredFirstName);
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith(desiredFirstName.ToLower()));
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void SafeEmail_With_Empty_String()
      {
         var expected = new string[] { "@example.org", "@example.net", "@example.com" };
         var emailAddress = Internet.SafeEmail(string.Empty);
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, @".+.+@.+\.\w+").Success, $"{nameof(emailAddress)} is '{emailAddress}'");
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void Slug_With_Default_Values()
      {
         var slug = Internet.Slug();
         Assert.IsFalse(string.IsNullOrWhiteSpace(slug));
         Assert.AreEqual(0, RegexMatchesCount(slug, @"[A-Z]"));
         Assert.AreEqual(0, RegexMatchesCount(slug, @"\s"));
         Assert.AreEqual(1, RegexMatchesCount(slug, @"[_.-]"));
      }

      [Test]
      public void Slug_With_Words()
      {
         var desiredWords = "the answer is fourty two";
         var slug = Internet.Slug(words: desiredWords);
         var words = desiredWords.Split(' ');
         Assert.AreEqual(5, words.Count(x => slug.Contains(x)));
         Assert.AreEqual(4, RegexMatchesCount(slug, @"[_.-]"));
      }

      [Test]
      public void Slug_With_Glue()
      {
         var desiredGlue = "+";
         var slug = Internet.Slug(glue: desiredGlue);
         var words = slug.Split(new string[] { desiredGlue }, StringSplitOptions.None);
         Assert.AreEqual(2, words.Count());
         Assert.AreEqual(1, RegexMatchesCount(slug, @"\+"));
      }

      [Test]
      public void Url_With_Default_Values()
      {
         var tries = 100;
         while (tries-- > 0)
         {
            var url = Internet.Url();
            Assert.AreEqual(1, RegexMatchesCount(url, @"[a-z]{3,}://([a-z_]{1,}.){1,}[a-z_]{1,}/([a-z_]{0,}.)[a-z_]{1,}"), $"{nameof(url)} is '{url}'");
         }
      }

      [Test]
      public void Url_With_Host()
      {
         var desiredHost = "abdcefg.org";
         var url = Internet.Url(desiredHost);
         Assert.IsTrue(url.Contains($"://{desiredHost}/"));
      }

      [Test]
      public void Url_With_Path()
      {
         var desiredPath = "/Home/Index.html";
         var url = Internet.Url(path: desiredPath);
         Assert.IsTrue(url.EndsWith(desiredPath));
      }

      [Test]
      public void Url_With_Scheme()
      {
         var desiredScheme = "ftp";
         var url = Internet.Url(scheme: desiredScheme);
         Assert.IsTrue(url.StartsWith($"{desiredScheme}://"));
      }

      [Test]
      public void Url_With_Empty_Scheme()
      {
         var desiredScheme = string.Empty;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: scheme", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Scheme()
      {
         var desiredScheme = " ";
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: scheme", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Host()
      {
         var desiredHost = string.Empty;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(host: desiredHost));
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: host", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Host()
      {
         var desiredHost = " ";
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(host: desiredHost));
         Assert.AreEqual("Must not be empty string or white spaces only.\r\nParameter name: host", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Path()
      {
         var desiredPath = string.Empty;
         var url = Internet.Url(scheme: "ftp", host: "abcdef.org", path: desiredPath);
         Assert.AreEqual("ftp://abcdef.org", url);
      }

      [Test]
      public void UserAgent_With_Default_Values()
      {
         var agent = Internet.UserAgent();
         Assert.Greater(RegexMatchesCount(agent, @"Mozilla|Opera"), 0);
      }

      [Test]
      public void UserAgent_With_Vendor()
      {
         var desiredVendor = "opera";
         var agent = Internet.UserAgent(desiredVendor);
         Assert.Greater(RegexMatchesCount(agent, @"Opera"), 0, $"{nameof(agent)} is '{agent}'");
      }

      [Test]
      public void UserAgent_With_Invalid_Vendor()
      {
         var desiredVendor = "ie";
         var agent = Internet.UserAgent(desiredVendor);
         Assert.Greater(RegexMatchesCount(agent, @"Mozilla|Opera"), 0);
      }

      [Test]
      public void UserName_With_Default_Values()
      {
         var tries = 100;
         while (tries-- > 0)
         {
            var userName = Internet.UserName();
            AllAssertions(userName);
         }
      }

      [Test]
      public void UserName_With_Given_Name()
      {
         var userName = Internet.UserName("Nancy");
         AllAssertions(userName);
         Assert.AreEqual("nancy", userName);
      }

      [Test]
      public void UserName_With_Given_Separators()
      {
         string separators = "-_+";
         var userName = Internet.UserName(separators: separators);
         AllAssertions(userName);
         Assert.GreaterOrEqual(1, RegexMatchesCount(userName, $@"[{separators}]"));
      }

      [Test]
      public void UserName_With_No_Separator_Guaranteed()
      {
         var userName = Internet.UserName(null, "");
         AllAssertions(userName);
         Assert.AreEqual(0, RegexMatchesCount(userName, @"[-_+]"));
      }

      [Test]
      public void UserName_With_String_Argument()
      {
         var firstName = "bo";
         var lastName = "peep";
         var tries = 10;
         while (tries-- > 0)
         {
            var userName = Internet.UserName($"{firstName} {lastName}");
            AllAssertions(userName);
            Assert.IsTrue(Regex.Match(userName, $@"({firstName}(_|\.){lastName}|{lastName}(_|\.){firstName})").Success, $"userName was '{userName}'");
         }
      }

      [Test]
      public void UserName_With_Min_Length()
      {
         const int minLength = 7;
         var tries = 10;
         while (tries-- > 0)
         {
            var userName = Internet.UserName(minLength);
            Assert.GreaterOrEqual(userName.Length, minLength);
         }
      }

      [Test]
      public void UserName_With_Ridiculous_Value()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName((int)Math.Pow(10, 6) + 1));
         Assert.AreEqual("Must be equal to or less than 10^6.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void UserName_With_Invalid_Value()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName(0));
         Assert.AreEqual("Must be greater than zero.\r\nParameter name: minLength", ex.Message);
      }

      [Test]
      public void UserName_With_Min_Max_Length()
      {
         const int minLength = 5;
         const int maxLength = 8;
         var userName = Internet.UserName(minLength, maxLength);
         int actualLength = userName.Length;
         Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
      }

      [Test]
      public void UserName_With_Common_Lengths()
      {
         for (int i = 1; i < 32; i++)
         {
            for (int j = i; j < i + 32; j++)
            {
               var minLength = i;
               var maxLength = j;
               var userName = Internet.UserName(minLength, maxLength);
               int actualLength = userName.Length;
               Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
            }
         }
      }

      [Test]
      public void UserName_With_Min_Greater_Than_Min()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName(7, 6));
         Assert.AreEqual("maxLength must be equal to or greater than minLength.", ex.Message);
      }

      private static void AllAssertions(string candidate)
      {
         Assert.IsFalse(string.IsNullOrWhiteSpace(candidate));
         Assert.IsFalse(candidate.Contains("#"));
         Assert.IsFalse(candidate.Contains("?"));
         Assert.AreEqual(0, Regex.Matches(candidate, @"[\s]").Count, $"Candidate was {candidate}");
         Assert.AreEqual(0, Regex.Matches(candidate, @"[A-Z]").Count, $"Candidate was {candidate}");
      }
   }
}
