using System;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   public class AllLocalizationTests
   {
      private static readonly string[] _localizations =
      {
         "bg",
         "ca",
         "ca-CAT",
         "da-DK",
         "de",
         "de-AT",
         "de-CH",
         "ee",
         "en",
         "en-AU",
         "en-au-ocker",
         "en-BORK",
         "en-CA",
         "en-GB",
         "en-IND",
         "en-MS",
         "en-NEP",
         "en-NG",
         "en-NZ",
         "en-PAK",
         "en-SG",
         "en-UG",
         "en-US",
         "en-ZA",
         "es",
         "es-MX",
         "fa",
         "fi-FI",
         "fr",
         "fr-CA",
         "fr-CH",
         "he",
         "id",
         "it",
         "ja",
         "ko",
         "lv",
         "nb-NO",
         "nl",
         //"no",
         "pl",
         "pt",
         "pt-BR",
         "ru",
         "sk",
         "sv",
         "tr",
         "uk",
         "vi",
         "zh-CN",
         "zh-TW"
      };

      [Test]
      public void Address_Methods()
      {
         Func<object>[] addressActions = new[]
         {
            Address.BuildingNumber,
            new Func<object>(() => Address.City()),
            Address.CityPrefix,
            Address.CitySuffix,
            Address.Community,
            Address.Country,
            Address.CountryCode,
            Address.CountryCodeLong,
            Address.FullAddress,
            () => Address.Latitude(),
            () => Address.Longitude(),
            () => Address.Postcode(),
            Address.SecondaryAddress,
            Address.State,
            Address.StateAbbr,
            () => Address.StreetAddress(),
            () => Address.StreetAddress(true),
            Address.StreetName,
            Address.StreetSuffix,
            Address.TimeZone,
            () => Address.Zip(),
            () => Address.ZipCode()
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> addressAction in addressActions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(addressAction, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Business_Methods()
      {
         Func<object>[] businessActions = new[]
         {
            () => Business.CreditCardExpiryDate(), 
            Business.CreditCardNumber, 
            new Func<object>(Business.CreditCardType)
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> businessAction in businessActions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(businessAction, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Code_Methods()
      {
         Func<object>[] codeActions = new[]
         {
            new Func<object>(() => Code.Asin()),
            new Func<object>(() => Code.Ean()),
            new Func<object>(() => Code.Imei()),
            new Func<object>(() => Code.Isbn()),
            new Func<object>(() => Code.Npi()),
            new Func<object>(() => Code.Nric()),
            new Func<object>(() => Code.Rut()),
            new Func<object>(() => Code.Sin())
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> codeAction in codeActions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(codeAction, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Company_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Company.AustralianBusinessNumber()),
            new Func<object>(() => Company.Bs()),
            new Func<object>(() => Company.Buzzword()),
            new Func<object>(() => Company.CatchPhrase()),
            new Func<object>(() => Company.CzechOrganizationNumber()),
            new Func<object>(() => Company.DunsNumber()),
            new Func<object>(() => Company.Ein()),
            new Func<object>(() => Company.FrenchSirenNumber()),
            new Func<object>(() => Company.FrenchSiretNumber()),
            new Func<object>(() => Company.Industry()),
            new Func<object>(() => Company.Logo()),
            new Func<object>(() => Company.Name()),
            new Func<object>(() => Company.NorwegianOrganizationNumber()),
            new Func<object>(() => Company.PolishRegisterOfNationalEconomy()),
            new Func<object>(() => Company.PolishTaxpayerIdentificationNumber()),
            new Func<object>(() => Company.Profession()),
            new Func<object>(() => Company.SpanishOrganizationNumber()),
            new Func<object>(() => Company.Suffix()),
            new Func<object>(() => Company.SwedishOrganizationNumber()),
            new Func<object>(() => Company.Type()),
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Educator_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Educator.Campus()),
            new Func<object>(() => Educator.Course()),
            new Func<object>(() => Educator.SecondarySchool()),
            new Func<object>(() => Educator.University())
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void IdNumber_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => IdNumber.Invalid()),
            new Func<object>(() => IdNumber.SpanishCitizenNumber()),
            new Func<object>(() => IdNumber.SpanishForeignCitizenNumber()),
            new Func<object>(() => IdNumber.Valid()),
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Internet_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Internet.DomainName()),
            new Func<object>(() => Internet.DomainSuffix()),
            new Func<object>(() => Internet.DomainWord()),
            new Func<object>(() => Internet.Email()),
            new Func<object>(() => Internet.FreeEmail()),
            new Func<object>(() => Internet.IPv4Address()),
            new Func<object>(() => Internet.IPv4CIDR()),
            new Func<object>(() => Internet.IPv6Address()),
            new Func<object>(() => Internet.IPv6CIDR()),
            new Func<object>(() => Internet.MacAddress()),
            new Func<object>(() => Internet.Password()),
            new Func<object>(() => Internet.PrivateIPv4Address()),
            new Func<object>(() => Internet.PublicIPv4Address()),
            new Func<object>(() => Internet.SafeEmail()),
            new Func<object>(() => Internet.Slug()),
            new Func<object>(() => Internet.Url()),
            new Func<object>(() => Internet.UserAgent()),
            new Func<object>(() => Internet.UserName()),
            new Func<object>(() => Internet.UserName()),
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Job_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Job.EmploymentType()),
            new Func<object>(() => Job.EducationLevel()),
            new Func<object>(() => Job.Field()),
            new Func<object>(() => Job.KeySkill()),
            new Func<object>(() => Job.Position()),
            new Func<object>(() => Job.Seniority()),
            new Func<object>(() => Job.Title()),
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Lorem_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Lorem.Character()),
            new Func<object>(() => Lorem.Characters()),
            new Func<object>(() => Lorem.Multibyte()),
            new Func<object>(() => Lorem.Paragraph()),
            new Func<object>(() => Lorem.ParagraphByChars()),
            new Func<object>(() => Lorem.Paragraphs()),
            new Func<object>(() => Lorem.Question()),
            new Func<object>(() => Lorem.Questions()),
            new Func<object>(() => Lorem.Sentence()),
            new Func<object>(() => Lorem.Sentences()),
            new Func<object>(() => Lorem.Word()),
            new Func<object>(() => Lorem.Words()),
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void Name_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => Name.FirstName()),
            new Func<object>(() => Name.FullName()),
            new Func<object>(() => Name.Initials(3)),
            new Func<object>(() => Name.LastName()),
            new Func<object>(() => Name.MiddleName()),
            new Func<object>(() => Name.NameWithMiddle()),
            new Func<object>(() => Name.Prefix()),
            new Func<object>(() => Name.Suffix())
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      [Test]
      public void PhoneNumber_Methods()
      {
         Func<object>[] actions = new[]
         {
            new Func<object>(() => PhoneNumber.CellPhone()),
            new Func<object>(() => PhoneNumber.LandLine())
         };

         bool success = true;
         int totalExecutions = 0;
         int successExecutions = 0;
         int failedExecutions = 0;
         foreach (Func<object> action in actions)
         {
            foreach (string loc in _localizations)
            {
               ++totalExecutions;

               if (!TryCatch(action, loc))
               {
                  success = false;
                  ++failedExecutions;
               }
               else
               {
                  ++successExecutions;
               }
            }
         }

         Console.WriteLine($"{failedExecutions} of {totalExecutions} executions failed");

         Assert.IsTrue(success);
         Assert.AreEqual(totalExecutions, successExecutions);
      }

      private static bool TryCatch(Func<object> action, string locale)
      {
         Config.Locale = locale;

         try
         {
            action();
            return true;
         }
         catch(Exception ex)
         {
            Assert.Fail($"Error while using Locale '{locale}': {ex.Message} => {ex.InnerException?.Message}");
            //Console.WriteLine($"Error while using Locale '{locale}': {ex.Message} => {ex.InnerException?.Message}");
            return false;
         }
      }
   }
}
