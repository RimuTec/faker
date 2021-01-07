using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using YamlDotNet.RepresentationModel;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(CompanyTestsFixtureData), nameof(CompanyTestsFixtureData.FixtureParams))]
   public class CompanyTests
   {
      public CompanyTests(string locale, Dictionary<string, string> localeSpecificRegex)
      {
         Locale = locale;
         // Set defaults:
         DefaultRegex[nameof(Name_HappyDays)] = @"\w+ \w+|\w+-\w+|\w+, \w+ and \w+";
         // locale specific
         if (localeSpecificRegex != null)
         {
            foreach (var key in localeSpecificRegex.Keys)
            {
               DefaultRegex[key] = localeSpecificRegex[key];
            }
         }
      }

      [SetUp]
      public void SetUp()
      {
         Config.Locale = Locale;
      }

      private readonly Dictionary<string, string> DefaultRegex = new();

      private string Locale { get; }

      [Test]
      public void Name_HappyDays()
      {
         var companyName = Company.Name();
         Assert.AreEqual(Regex.Matches(companyName, DefaultRegex[nameof(Name_HappyDays)]).Count, 1,
            $"Locale {Locale}: Incorrect value is: '{companyName}'. Regex used: '{DefaultRegex[nameof(Name_HappyDays)]}'");
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
         using var input = new StringReader(Document);
         var yaml = new YamlStream();
         yaml.Load(input);
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

   public static class CompanyTestsFixtureData
   {
      public static IEnumerable FixtureParams
      {
         get
         {
            yield return new TestFixtureData("bg", null);
            yield return new TestFixtureData("ca", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-ZÀa-zàèéíïòó, ]+$" },
            });
            yield return new TestFixtureData("ca-CAT", null);
            yield return new TestFixtureData("da-DK", null);
            yield return new TestFixtureData("de", null);
            yield return new TestFixtureData("de-AT", null);
            yield return new TestFixtureData("de-CH", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z,' ]+(AG|GmbH|und Söhne|und Partner|& Co.|Gruppe|LLC|Inc.)?$" },
            });
            yield return new TestFixtureData("ee", null);
            yield return new TestFixtureData("en", null);
            yield return new TestFixtureData("en-AU", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z,' ]+(Pty Ltd|and Sons|Corp|Group|Brothers|Partners)?$" }
            });
            yield return new TestFixtureData("en-au-ocker", null);
            yield return new TestFixtureData("en-BORK", null);
            yield return new TestFixtureData("en-CA", null);
            yield return new TestFixtureData("en-GB", null);
            yield return new TestFixtureData("en-IND", null);
            yield return new TestFixtureData("en-MS", null);
            yield return new TestFixtureData("en-NEP", null);
            yield return new TestFixtureData("en-NG", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z,' ]+(Inc|and Sons|LLC|Group)?$" }
            });
            yield return new TestFixtureData("en-NZ", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z,' ]+(Ltd|Ltc|and Sons|Group|Brothers|Partners)?$" }
            });
            yield return new TestFixtureData("en-PAK", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z,' ]+(Inc|and Sons|LLC|Group)?$" }
            });
            yield return new TestFixtureData("en-SG", null);
            yield return new TestFixtureData("en-UG", null);
            yield return new TestFixtureData("en-US", null);
            yield return new TestFixtureData("en-ZA", null);
            yield return new TestFixtureData("es", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[A-ZÁa-záéíóúñ, ]+(S.L.|e Hijos|S.A.|Hermanos)?$" }
            });
            yield return new TestFixtureData("es-MX", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^(Grupo|Sociedad|Grupo Financiero|Colegio|Fondo)?[A-ZÁa-záéíóúñ, ]+(S.A.|S.A. de C.V.|S.R.L|S.A.B.|S.C.)?$" },
            });
            yield return new TestFixtureData("fa", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), @"^[-\p{IsArabic}\u200CA-Za-z\(\), ]+$" }
            });
            yield return new TestFixtureData("fi-FI", null);
            yield return new TestFixtureData("fr", null);
            yield return new TestFixtureData("fr-CA", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-zé, ]+(SARL|SA|EURL|SAS|SEM|SCOP|GIE|EI)?$" },
            });
            yield return new TestFixtureData("fr-CH", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-zéê, ]+(AG|GmbH|et associés|& Co.|Groupe|LLC|Inc.)?$" },
            });
            yield return new TestFixtureData("he", null);
            yield return new TestFixtureData("id", null);
            yield return new TestFixtureData("it", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-Za-z', ]+(SPA|e figli|Group|s.r.l.)$" },
            });
            yield return new TestFixtureData("ja", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), @"^[\u3041-\u3096\u30A0-\u30FF\u3400-\u4DB5\u4E00-\u9FCB\uF900-\uFA6A]+$" },
               // .NET Docs: Supported named blocks: https://docs.microsoft.com/en-us/dotnet/standard/base-types/character-classes-in-regular-expressions#supported-named-blocks
               // However.... I couldn't find a way to make '\p{IsHirigana}' or '\p{IsKatakana}' work, therefore am using code
               // point ranges found here: https://www.localizingjapan.com/blog/2012/01/20/regular-expressions-for-japanese-text/
            });
            yield return new TestFixtureData("ko", null);
            yield return new TestFixtureData("lv", null);
            yield return new TestFixtureData("nb-NO", null);
            yield return new TestFixtureData("nl", null);

            // Not testing locale 'no' since the file no.yml has an incorrect format at the time of writing.
            // Note that we won't fix the content as we take the file 'as-is' from https://github.com/faker-ruby/faker
            //yield return new TestFixtureData("no", null);

            yield return new TestFixtureData("pl", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-ZŚŻa-ząćęłńóśżź, ]+(S.A.|sp. z o.o.|sp. j.|sp.p.|sp. k.|S.K.A.)?$" },
            });
            yield return new TestFixtureData("pt", null);
            yield return new TestFixtureData("pt-BR", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[-A-ZÁa-záãêçóõú ]+(-| |, )+([A-ZÁa-záãêçóõú ]+)?( e | e de | e da )?([A-ZÁa-záãêçóõú]+)?(S.A.|LTDA|e Associados|Comércio|EIRELI)?$" },
            });
            yield return new TestFixtureData("ru", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), @"^(ИП|ООО|ЗАО|ОАО|НКО|ТСЖ|ОП) [-\u0400-\u04FF ]+(Снаб|Торг|Пром|Трейд|Сбыт)?$" },
            });
            yield return new TestFixtureData("sk", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), "^[A-Za-zČŠáčéďíľňšúýž' ]+(s.r.o.|a.s.|v.o.s.)$" },
            });
            yield return new TestFixtureData("sv", null);
            yield return new TestFixtureData("tr", null);
            yield return new TestFixtureData("uk", new Dictionary<string, string>{
               { nameof(CompanyTests.Name_HappyDays), @"^(ТОВ|ПАТ|ПрАТ|ТДВ|КТ|ПТ|ДП|ФОП) [-’\u0400-\u04FF ]+(постач|торг|пром|трейд|збут)?$" },
            });
            yield return new TestFixtureData("vi", null);
            yield return new TestFixtureData("zh-CN", null);
            yield return new TestFixtureData("zh-TW", null);
         }
      }
   }
}
