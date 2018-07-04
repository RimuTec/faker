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
      public void UserName_With_Default_Values() {
         // arrange

         // act
         var userName = Internet.UserName();

         // assert
         AllAssertions(userName);
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
