using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class CompanyTests
   {
      [Test]
      public void Name_HappyDays()
      {
         var companyName = Company.Name();
         Assert.IsTrue(new List<Func<bool>> {
                                  () => Regex.IsMatch(companyName, @"\w+ \w+"),
                                  () => Regex.IsMatch(companyName, @"\w+-\w+"),
                                  () => Regex.IsMatch(companyName, @"\w+, \w+ and \w+")
                              }.Any(x => x.Invoke()));
      }

      [Test]
      public void Suffix_With_LocaleEn()
      {
         Config.Locale = "en";
         var suffix = Company.Suffix();
         var availableSuffixes = new List<string> { "Inc", "and Sons", "LLC", "Group" };
         Assert.IsTrue(availableSuffixes.Contains(suffix));
      }

      [Test]
      public void Suffix_With_LocaleRu()
      {
         Config.Locale = "ru";
         var suffix = Company.Suffix();
         var availableSuffixes = new List<string> { "Снаб", "Торг", "Пром", "Трейд", "Сбыт" };
         Assert.IsTrue(availableSuffixes.Contains(suffix));
      }

      [Test]
      public void Industry_HappyDays()
      {
         var industry = Company.Industry();
         Assert.IsFalse(string.IsNullOrWhiteSpace(industry));
      }

      [Test]
      public void CatchPhrase_HappyDays()
      {
         var catchPhrase = Company.CatchPhrase();
         var matches = Regex.Matches(catchPhrase, "[0-9a-z]+");
         Assert.GreaterOrEqual(matches.Count, 3);
      }

      [Test]
      public void Buzzword_HappyDays()
      {
         for (int i = 0; i < 20; i++)
         {
            var buzzword = Company.Buzzword();
            Assert.IsFalse(string.IsNullOrWhiteSpace(buzzword));
            Assert.IsTrue(Regex.IsMatch(buzzword, "[0-9a-z]+"));
         }
      }

      [Test]
      public void Bs_HappyDays()
      {
         var bs = Company.Bs();
         var matches = Regex.Matches(bs, "[0-9a-zA-Z]+");
         Assert.GreaterOrEqual(matches.Count, 3, $"Failed BS was '{bs}'");
      }

      [Test]
      public void Logo_HappyDays()
      {
         var logo = Company.Logo();
         _ = new Uri(logo); // shouldn't throw indicating it's a valid URL
      }

      [Test]
      public void Type_HappyDays()
      {
         var type = Company.Type();
         Assert.IsFalse(string.IsNullOrWhiteSpace(type));
      }

      [Test]
      public void Ein_HappyDays()
      {
         var ein = Company.Ein();
         Assert.AreEqual(10, ein.Length);
         Assert.IsFalse(ein.Contains("#"));
         Assert.AreEqual(1, Regex.Matches(ein, @"[\-]").Count);
         Assert.AreEqual(9, Regex.Matches(ein, "[0-9]").Count);
      }

      [Test]
      public void DunsNumber_HappyDays()
      {
         var duns = Company.DunsNumber();
         Assert.AreEqual(11, duns.Length);
         Assert.AreEqual(2, Regex.Matches(duns, @"[\-]").Count);
         Assert.AreEqual(9, Regex.Matches(duns, "[0-9]").Count);
      }

      [Test]
      public void Profession_HappyDays()
      {
         var profession = Company.Profession();
         Assert.IsFalse(string.IsNullOrWhiteSpace(profession));
      }

      [Test]
      public void SwedishOrganizationNumber_HappyDays()
      {
         var son = Company.SwedishOrganizationNumber();
         Assert.IsTrue(son.HasValidCheckDigit());
      }

      [Test]
      public void CzechOrganizationNumber_HappyDays()
      {
         var con = Company.CzechOrganizationNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(con));
         Assert.AreEqual(8, con.Length);
      }

      [Test]
      public void FrenchSirenNumber_HappyDays()
      {
         var siren = Company.FrenchSirenNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(siren));
         Assert.AreEqual(9, siren.Length);
      }

      [Test]
      public void FrenchSiretNumber_HappyDays()
      {
         var siret = Company.FrenchSiretNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(siret));
         Assert.AreEqual(14, siret.Length);
      }

      [Test]
      public void NorwegianOrganizationNumber_HappyDays()
      {
         var number = Company.NorwegianOrganizationNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, "^[89]").Count);
         Assert.AreEqual(9, Regex.Matches(number, "[0-9]").Count);
      }

      [Test]
      public void AustralianBusinessNumber_HappyDays()
      {
         var number = Company.AustralianBusinessNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(11, Regex.Matches(number, "[0-9]").Count);
      }

      [Test]
      public void SpanishOrganizationNumber_HappyDays()
      {
         var number = Company.SpanishOrganizationNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, "^[ABCDEFGHJNPQRSUVW]").Count, $"Invalid number is '{number}'.");
         Assert.AreEqual(7, Regex.Matches(number, "[0-9]").Count);
      }

      [Test]
      public void PolishTaxpayerIdentificationNumber_HappyDays()
      {
         var number = Company.PolishTaxpayerIdentificationNumber();
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(1, Regex.Matches(number, "^[1-8][1-8][1-8]").Count);
         Assert.AreEqual(10, Regex.Matches(number, "[0-9]").Count);
      }

      [Test]
      public void PolishRegisterOfNationalEconomy_DefaultValue()
      {
         var number = Company.PolishRegisterOfNationalEconomy();
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(9, number.Length);
      }

      [Test]
      public void PolishRegisterOfNationalEconomy_With_Length_9()
      {
         var number = Company.PolishRegisterOfNationalEconomy(9);
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(9, number.Length);
      }

      [Test]
      public void PolishRegisterOfNationalEconomy_With_Length_14()
      {
         var number = Company.PolishRegisterOfNationalEconomy(14);
         Assert.IsFalse(string.IsNullOrWhiteSpace(number));
         Assert.AreEqual(14, number.Length);
      }

      [Test]
      public void PolishRegisterOfNationalEconomy_With_Invalid_Length()
      {
         var ex = Assert.Throws<ArgumentOutOfRangeException>(() => Company.PolishRegisterOfNationalEconomy(11));
         Assert.AreEqual("Must be either 9 or 14. (Parameter 'length')", ex.Message);
      }

      [Test]
      public void Foo()
      {
         // This test fails with YamlDotNet 9.1.0 but passes with YamlDotNet 8.1.1.
         // Issue filed at: https://github.com/aaubry/YamlDotNet/issues/548
         // [Manfred, 05dec2020]
         using(var input = new StringReader(Document))
         {
            var yaml = new YamlStream();
            yaml.Load(input);
         }
      }
      private const string Document =
         "en:" + "\n" +
         "  faker:" + "\n" +
         "    company:" + "\n" +
         "      suffix: [Inc, and Sons, LLC, Group]" + "\n" +
         "      # Buzzword wordlist from http://www.1728.com/buzzword.htm" + "\n" +
         "      buzzwords:" + "\n" +
         "        - [\"foo\", \"Facetoface\", \"Focused\", \"Frontline\", \"Fullyconfigurable\", \"Functionbased\", \"Fundamental\", \"Futureproofed\", \"Grassroots\", \"Horizontal\", \"Implemented\", \"Innovative\", \"Integrated\", \"Intuitive\", \"Inverse\", \"Managed\", \"Mandatory\", \"Monitored\", \"Multichannelled\", \"Multilateral\", \"Multilayered\", \"Multitiered\", \"Networked\", \"Objectbased\", \"Openarchitected\", \"Opensource\", \"Operative\", \"Optimized\", \"Optional\", \"Organic\", \"Organized\", \"Persevering\", \"Persistent\", \"Phased\", \"Polarised\", \"Pre-emptive\", \"Proactive\", \"Profit-focused\", \"Profound\", \"Programmable\", \"Progressive\", \"Public-key\", \"Quality-focused\", \"Reactive\", \"Realigned\", \"Re-contextualized\", \"Reengineered\", \"Reduced\", \"Reverseengineered\", \"Rightsized\", \"Robust\", \"Seamless\", \"Secured\", \"Selfenabling\", \"Sharable\", \"Standalone\", \"Streamlined\", \"Switchable\", \"Synchronised\", \"Synergistic\", \"Synergized\", \"Teamoriented\", \"Total\", \"Triplebuffered\" ]" + "\n" +
         "        - [\"24 hour\", \"24/7\", \"3rd generation\", \"4th generation\", \"5th generation\"]" + "\n" +
         "        - [\"ability\", \"access\", \"adapter\", \"algorithm\", \"alliance\"]";
   }
}
