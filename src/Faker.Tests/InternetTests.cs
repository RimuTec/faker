using NUnit.Framework;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class InternetTests : FixtureBase
   {
      public InternetTests(string locale)
      {
         Locale = locale;
      }

      [SetUp]
      public void SetUp()
      {
         Config.Locale = Locale;
      }

      private string Locale { get; }

      [Test]
      public void DomainName_HappyDays()
      {
         var domainName = Internet.DomainName();
         AllAssertions(domainName);
         Assert.GreaterOrEqual(RegexMatchesCount(domainName, @"\."), 1,
            $"Locale {Locale}. Invalid value '{domainName}'"
         );
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
         Assert.AreEqual(1, RegexMatchesCount(emailAddress, "@"));
      }

      [Test]
      public void Email_With_Specific_Name()
      {
         var firstName = Name.FirstName(); // can also be any other name
         var pattern = MakePattern(firstName);
         var emailAddress = Internet.Email(firstName);
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, pattern).Success,
            $"Locale {Locale}. First name is '{firstName}' Email address is: '{emailAddress}'. Pattern is '{pattern}'"
         );
      }

      private static string MakePattern(string firstNames) {
         // Example: John Patrick => ^(john[._]{1}patrick|patrick[._]{1}john)
         var firstNamesAsArray = firstNames.Split(' ', '-', '\'').Select(s => s.ToLower()).ToArray();
         if(firstNamesAsArray.Length == 1) {
            return firstNamesAsArray[0];
         }
         var firstFirstName = firstNamesAsArray[0];
         var secondFirstName = firstNamesAsArray[1];
         return $"^({firstFirstName}[._]{{1}}{secondFirstName}|{secondFirstName}[._]{{1}}{firstFirstName})";
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
      public void Email_With_No_Separator_Guaranteed_LocaleEn()
      {
         Config.Locale = "en";
         var tries = 100;
         while (tries-- > 0)
         {
            var emailAddress = Internet.Email(separators: string.Empty);
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, "[-_+]"), $"{nameof(emailAddress)} was {emailAddress}");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"\0"));
         }
      }

      [Test]
      public void Email_With_No_Separator_Guaranteed_LocaleRu()
      {
         Config.Locale = "ru";
         var tries = 100;
         while (tries-- > 0)
         {
            var emailAddress = Internet.Email(separators: string.Empty);
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, "[-_+]"), $"{nameof(emailAddress)} was {emailAddress}");
            Assert.AreEqual(0, RegexMatchesCount(emailAddress, @"\0"));
         }
      }
      [Test]
      public void Email_With_Name_And_Separator()
      {
         const string name = "Janelle Santiago";
         const string separators = "+";
         var emailAddress = Internet.Email(name, separators);
         Assert.IsTrue(Regex.Match(emailAddress, @".+\+.+@.+\.\w+").Success);
      }

      [Test]
      public void FreeEmail_With_Default_Values()
      {
         var emailAddress = Internet.FreeEmail();
         AllAssertions(emailAddress);
         var freeEmail = Fetch("internet.free_email");
         Assert.IsTrue(freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Desired_Name()
      {
         const string desiredFirstName = "Nancy";
         var emailAddress = Internet.FreeEmail(desiredFirstName);
         AllAssertions(emailAddress);
         Assert.IsTrue(emailAddress.StartsWith(desiredFirstName.ToLower()));
         var freeEmail = Fetch("internet.free_email");
         Assert.IsTrue(freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void FreeEmail_With_Empty_String()
      {
         var emailAddress = Internet.FreeEmail(string.Empty);
         AllAssertions(emailAddress);
         Assert.IsTrue(Regex.Match(emailAddress, EmailRegex).Success,
            $"Locale '{Locale}'. Invalid value is '{emailAddress}'"
         );
         var freeEmail = Fetch("internet.free_email");
         Assert.IsTrue(freeEmail.Any(x => emailAddress.EndsWith(x)));
      }

      [Test]
      public void IPv4Address_HappyDays()
      {
         var tries = 100;
         while (tries-- > 0)
         {
            var ipv4address = Internet.IPv4Address();
            foreach (var part in ipv4address.Split('.'))
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
         Assert.AreEqual(1, RegexMatchesCount(ipV4cidr, "/"));
         Assert.AreEqual(3, RegexMatchesCount(ipV4cidr, @"\."));
      }

      [Test]
      public void IPv6Address_HappyDays()
      {
         var ipV6Address = Internet.IPv6Address();
         Assert.IsTrue(Regex.Match(ipV6Address, "[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}").Success,
            $"{nameof(ipV6Address)} is '{ipV6Address}'");
      }

      [Test]
      public void IPv6CIDR_HappyDays()
      {
         var ipV6withMask = Internet.IPv6CIDR();
         Assert.IsTrue(Regex.Match(ipV6withMask, "[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}:[0-9abcdef]{4}/[0-9]{1,3}").Success,
            $"{nameof(ipV6withMask)} is '{ipV6withMask}'");
      }

      [Test]
      public void MacAddress_DefaultValues()
      {
         var macAddress = Internet.MacAddress();
         Assert.AreEqual(5, RegexMatchesCount(macAddress, ":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, "[0-9a-f]"));
      }

      [Test]
      public void MacAddress_WithPrefix()
      {
         const string prefix = "55:44:33";
         var macAddress = Internet.MacAddress(prefix);
         Assert.IsTrue(macAddress.StartsWith(prefix + ":"), $"{nameof(macAddress)} is '{macAddress}'");
         Assert.AreEqual(5, RegexMatchesCount(macAddress, ":"));
         Assert.AreEqual(12, RegexMatchesCount(macAddress, "[0-9a-f]"));
      }

      [Test]
      public void MacAddress_Null_Prefix()
      {
         var ex = Assert.Throws<ArgumentNullException>(() => Internet.MacAddress(null));
         Assert.AreEqual("Must not be null. (Parameter 'prefix')", ex.Message);
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
            if (RegexMatchesCount(password, "[A-Z]") > 0)
            {
               usesUpperCase = true;
            }
            if (RegexMatchesCount(password, "[a-z]") > 0)
            {
               usesLowerCase = true;
            }
            if (RegexMatchesCount(password, "[!@#$%^&*]") > 0)
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
         Assert.AreEqual("Must be greater than zero. (Parameter 'minLength')", ex.Message);
      }

      [Test]
      public void Password_With_Invalid_MaxLength()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(maxLength: 0));
         Assert.AreEqual("Must be greater than zero. (Parameter 'maxLength')", ex.Message);
      }

      [Test]
      public void Password_With_MaxLength_Less_Than_MinLength()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Password(minLength: 5, maxLength: 4));
         Assert.AreEqual("Must be equal to or greater than minLength. (Parameter 'maxLength')", ex.Message);
      }

      [Test]
      public void Password_With_UpperCase_Letters()
      {
         var password = Internet.Password(mixCase: true, specialChars: false);
         Assert.Greater(RegexMatchesCount(password, "[A-Z]"), 0,
            $"Locale '{Locale}'. Invalid value is '{password}'"
         );
      }

      [Test]
      public void Password_Without_UpperCase_Letters()
      {
         var password = Internet.Password(mixCase: false, specialChars: false);
         Assert.AreEqual(0, RegexMatchesCount(password, "[A-Z]"));
      }


      [Test]
      public void Password_With_SpecialChars()
      {
         var password = Internet.Password(specialChars: true);
         Assert.Greater(RegexMatchesCount(password, "[!@#$%^&*]"), 0);
      }

      [Test]
      public void Password_Without_SpecialChars()
      {
         var password = Internet.Password(specialChars: false);
         Assert.AreEqual(0, RegexMatchesCount(password, "[!@#$%^&*]"));
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
         Assert.IsTrue(Regex.Match(emailAddress, EmailRegex).Success,
            $"Locale {Locale}. Invalid value is '{emailAddress}'.");
         Assert.IsTrue(expected.Any(x => emailAddress.EndsWith(x)), $"{nameof(emailAddress)} is '{emailAddress}'");
      }

      [Test]
      public void Slug_With_Default_Values()
      {
         var slug = Internet.Slug();
         Assert.IsFalse(string.IsNullOrWhiteSpace(slug));
         Assert.AreEqual(0, RegexMatchesCount(slug, "[A-Z]"));
         Assert.AreEqual(0, RegexMatchesCount(slug, @"\s"));
         Assert.GreaterOrEqual(RegexMatchesCount(slug, "[_.-]"), 1,
            $"Locale {Locale}. Invalid value '{slug}'"
         );
      }

      [Test]
      public void Slug_With_Words()
      {
         const string desiredWords = "the answer is fourty two";
         var slug = Internet.Slug(words: desiredWords);
         var words = desiredWords.Split(' ');
         Assert.AreEqual(5, words.Count(x => slug.Contains(x)));
         Assert.AreEqual(4, RegexMatchesCount(slug, "[_.-]"));
      }

      [Test]
      public void Slug_With_Glue()
      {
         const string desiredGlue = "+";
         var slug = Internet.Slug(glue: desiredGlue);
         var words = slug.Split(new string[] { desiredGlue }, StringSplitOptions.None);
         Assert.AreEqual(2, words.Length);
         Assert.AreEqual(1, RegexMatchesCount(slug, @"\+"));
      }

      [Test]
      public void Url_With_Default_Values_LocaleEn()
      {
         Config.Locale = "en";
         var tries = 100;
         while (tries-- > 0)
         {
            var url = Internet.Url();
            Assert.AreEqual(1, RegexMatchesCount(url, "[a-z]{3,}://([a-z_]{1,}.){1,}[a-z_]{1,}/([a-z_]{0,}.)[a-z_]{1,}"), $"{nameof(url)} is '{url}'");
         }
      }

      [Test]
      public void Url_With_Default_Values_LocalRu()
      {
         Config.Locale = "ru";
         var tries = 100;
         while (tries-- > 0)
         {
            var url = Internet.Url();
            Assert.AreEqual(1, RegexMatchesCount(url, "[a-z]{3,}://([a-z][-a-z0-9_]{1,}.){1,}[a-z_]{1,}/((%([A-Z0-9]){2,2}){0,}|([a-z_]{0,}.)[a-z_]{1,})"), $"{nameof(url)} is '{url}'");
         }
      }
      [Test]
      public void Url_With_Host()
      {
         const string desiredHost = "abdcefg.org";
         var url = Internet.Url(desiredHost);
         Assert.IsTrue(url.Contains($"://{desiredHost}/"));
      }

      [Test]
      public void Url_With_Path()
      {
         const string desiredPath = "/Home/Index.html";
         var url = Internet.Url(path: desiredPath);
         Assert.IsTrue(url.EndsWith(desiredPath));
      }

      [Test]
      public void Url_With_Scheme()
      {
         const string desiredScheme = "ftp";
         var url = Internet.Url(scheme: desiredScheme);
         Assert.IsTrue(url.StartsWith($"{desiredScheme}://"));
      }

      [Test]
      public void Url_With_Empty_Scheme()
      {
         var desiredScheme = string.Empty;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));
         Assert.AreEqual("Must not be empty string or white spaces only. (Parameter 'scheme')", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Scheme()
      {
         const string desiredScheme = " ";
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(scheme: desiredScheme));
         Assert.AreEqual("Must not be empty string or white spaces only. (Parameter 'scheme')", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Host()
      {
         var desiredHost = string.Empty;
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(host: desiredHost));
         Assert.AreEqual("Must not be empty string or white spaces only. (Parameter 'host')", ex.Message);
      }

      [Test]
      public void Url_With_WhiteSpace_Host()
      {
         const string desiredHost = " ";
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.Url(host: desiredHost));
         Assert.AreEqual("Must not be empty string or white spaces only. (Parameter 'host')", ex.Message);
      }

      [Test]
      public void Url_With_Empty_Path()
      {
         var desiredPath = string.Empty;
         var url = Internet.Url(host: "abcdef.org", path: desiredPath, scheme: "ftp");
         Assert.AreEqual("ftp://abcdef.org", url);
      }

      [Test]
      public void UserAgent_With_Default_Values()
      {
         var agent = Internet.UserAgent();
         Assert.Greater(RegexMatchesCount(agent, "Mozilla|Opera"), 0);
      }

      [Test]
      public void UserAgent_With_Vendor()
      {
         const string desiredVendor = "opera";
         var agent = Internet.UserAgent(desiredVendor);
         Assert.Greater(RegexMatchesCount(agent, "Opera"), 0, $"{nameof(agent)} is '{agent}'");
      }

      [Test]
      public void UserAgent_With_Invalid_Vendor()
      {
         const string desiredVendor = "ie";
         var agent = Internet.UserAgent(desiredVendor);
         Assert.Greater(RegexMatchesCount(agent, "Mozilla|Opera"), 0);
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
         const string separators = "-_+";
         var userName = Internet.UserName(separators: separators);
         AllAssertions(userName);
         Assert.GreaterOrEqual(1, RegexMatchesCount(userName, $"[{separators}]"));
      }

      [Test]
      public void UserName_With_No_Separator_Guaranteed()
      {
         var userName = Internet.UserName(null, string.Empty);
         AllAssertions(userName);
         Assert.AreEqual(0, RegexMatchesCount(userName, "[-_+]"), $"userName was '{userName}'");
      }

      [Test]
      public void UserName_With_String_Argument()
      {
         const string firstName = "bo";
         const string lastName = "peep";
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
         Assert.AreEqual("Must be equal to or less than 10^6. (Parameter 'minLength')", ex.Message);
      }

      [Test]
      public void UserName_With_Invalid_Value()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Internet.UserName(0));
         Assert.AreEqual("Must be greater than zero. (Parameter 'minLength')", ex.Message);
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
      public void UserName_With_ShortLength()
      {
         const int minLength = 1;
         const int maxLength = 2;
         var userName = Internet.UserName(minLength, maxLength);
         int actualLength = userName.Length;
         Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
      }

      [Test]
      public void UserName_With_LongLength()
      {
         const int minLength = 32;
         const int maxLength = 33;
         var userName = Internet.UserName(minLength, maxLength);
         int actualLength = userName.Length;
         Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
      }

      [Test]
      public void UserName_With_ReasonableLength()
      {
         const int minLength = 8;
         const int maxLength = 12;
         var userName = Internet.UserName(minLength, maxLength);
         int actualLength = userName.Length;
         Assert.IsTrue(actualLength >= minLength && actualLength <= maxLength);
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
         Assert.AreEqual(0, Regex.Matches(candidate, "[A-Z]").Count, $"Candidate was {candidate}");
      }

      // Source for regex: https://stackoverflow.com/a/37320735/411428
      private const string EmailRegex = "^(([^<>()\\[\\]\\.,;:\\s@\"]+(\\.[^<>()\\[\\]\\.,;:\\s@\"]+)*)|(\".+\"))@(([^<>()[\\]\\.,;:\\s@\"]+\\.)+[^<>()[\\]\\.,;:\\s@\"]{2,})";
   }
}
