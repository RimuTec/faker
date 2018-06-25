using NUnit.Framework;
using RimuTec.Faker.Extensions;
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

      [Test]
      public void Suffix_HappyDays() {
         // arrange

         // act
         var suffix = Company.Suffix();

         // assert
         var availableSuffixes = new List<string> { "Inc", "and Sons", "LLC", "Group" };
         Assert.IsTrue(availableSuffixes.Contains(suffix));
      }

      [Test]
      public void Industry_HappyDays() {
         // arrange

         // act
         var industry = Company.Industry();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(industry));
      }

      [Test]
      public void CatchPhrase_HappyDays() {
         // arrange

         // act
         var catchPhrase = Company.CatchPhrase();

         // assert
         var matches = Regex.Matches(catchPhrase, @"[0-9a-z]+");
         Assert.GreaterOrEqual(matches.Count, 3);
      }

      [Test]
      public void Buzzword_HappyDays() {
         // arrange

         for (int i = 0; i < 20; i++) {
            // act
            var buzzword = Company.Buzzword();

            // assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(buzzword));
            Assert.IsTrue(Regex.IsMatch(buzzword, @"[0-9a-z]+"));
         }
      }

      [Test]
      public void Bs_HappyDays() {
         // arrange

         // act
         var bs = Company.Bs();

         // assert
         var matches = Regex.Matches(bs, @"[0-9a-z]+");
         Assert.GreaterOrEqual(matches.Count, 3);
      }

      [Test]
      public void Logo_HappyDays() {
         // arrange

         // act
         var logo = Company.Logo();

         // assert
         var uri = new Uri(logo); // shouldn't throw indicating it's a valid URL
      }

      [Test]
      public void Type_HappyDays() {
         // arrange

         // act
         var type = Company.Type();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(type));
      }

      [Test]
      public void Ein_HappyDays() {
         // arrange

         // act
         var ein = Company.Ein();

         // assert
         Assert.AreEqual(10, ein.Length);
         Assert.IsFalse(ein.Contains("#"));
         Assert.AreEqual(1, Regex.Matches(ein, @"[\-]").Count);
         Assert.AreEqual(9, Regex.Matches(ein, @"[0-9]").Count);
      }

      [Test]
      public void DunsNumber_HappyDays() {
         // arrange

         // act
         var duns = Company.DunsNumber();

         // assert
         Assert.AreEqual(11, duns.Length);
         Assert.AreEqual(2, Regex.Matches(duns, @"[\-]").Count);
         Assert.AreEqual(9, Regex.Matches(duns, @"[0-9]").Count);
      }

      [Test]
      public void Profession_HappyDays() {
         // arrange

         // act
         var profession = Company.Profession();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(profession));
      }

      [Test]
      public void SwedishOrganizationNumber_HappyDays() {
         // arrange

         // act
         var son = Company.SwedishOrganizationNumber();

         // assert
         Assert.IsTrue(son.HasValidCheckDigit());
      }

      [Test]
      public void CzechOrganizationNumber_HappyDays() {
         // arrange

         // act
         var con = Company.CzechOrganizationNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(con));
         Assert.AreEqual(8, con.Length);
      }

      [Test]
      public void FrenchSirenNumber_HappyDays() {
         // arrange

         // act
         var siren = Company.FrenchSirenNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(siren));
         Assert.AreEqual(9, siren.Length);
      }

      [Test]
      public void FrenchSiretNumber_HappyDays() {
         // arrange

         // act
         var siret = Company.FrenchSiretNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(siret));
         Assert.AreEqual(14, siret.Length);
      }

      [Test]
      public void NorwegianOrganizationNumber_HappyDays() {
         // arrange

         // act
         var number = Company.NorwegianOrganizationNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, @"^[89]").Count);
         Assert.AreEqual(9, Regex.Matches(number, @"[0-9]").Count);
      }

      [Test]
      public void AustralianBusinessNumber_HappyDays() {
         // arrange

         // act
         var number = Company.AustralianBusinessNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(11, Regex.Matches(number, @"[0-9]").Count);
      }

      [Test]
      public void SpanishOrganizationNumber_HappyDays() {
         // arrange

         // act
         var number = Company.SpanishOrganizationNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, @"^[BCDEFGHJNPQRSUVW]").Count);
         Assert.AreEqual(7, Regex.Matches(number, @"[0-9]").Count);
      }

      [Test]
      public void PolishTaxpayerIdentificationNumber_HappyDays() {
         // arrange

         // act
         var number = Company.PolishTaxpayerIdentificationNumber();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, @"^[1-8][1-8][1-8]").Count);
         Assert.AreEqual(10, Regex.Matches(number, @"[0-9]").Count);
      }
   }
}
