# Release Notes

## Version 0.40.0
### New Features
- Color.ColorName()
- Color.HexColor()
- Color.RgbColor()

## Version 0.39.0
### Breaking Change
- Class IDNumber has been renamed to IdNumber(). A find-replace for something like "IDNumber." should help with the transition.
### New Features
- Added instructions to README.md describing how to build Faker from sources.
- Class Job supports all 51 built-in as well as custom locales
- Class Lorem supports all 51 built-in as well as custom locales
- Class PhoneNumber supports all 51 built-in as well as custom locales

## Version 0.38.0
### Breaking Change
- Method IDNumber.SSN_Valid() has been renamed to IDNumber.SsnValid(). A fairly straight forward find-raplace should help with the transition, e.g. you could do a find-all for ".SSN_Valid("
### New Features
- Class IDNumber supports all 51 built-in as well as custom locales
- Class Internet supports all 51 built-in as well as custom locales

## Version 0.37.0
### New Features
- Class Educator supports all 51 built-in as well as custom locales
- Class Finance supports all 51 built-in as well as custom locales
### Bug Fixes
- FIX: Finance.Finance.CreditCard(CreditCardType.Mastercard) did not replace "[1-5]" with single digit in this range

## Version 0.36.0
### New Features
- Class Business supports all 51 built-in as well as custom locales
- Class Code supports all 51 built-in as well as custom locales
- Class Company supports all 51 built-in as well as custom locales

## Version 0.35.0
### New Features
- Class Address supports all 51 built-in as well as custom locales

## Version 0.34.0
### Summary
Class Name is now on par with Faker Gem including support for localization, 51 locales out of the box plus support for custom locales.
### New Features
- Name.Prefix() supports localization
- Name.Suffix() supports localization
- Name.FullName() supports localization
- Name.MiddleName() supports localization
- Name.NameWithMiddle() supports localization

## Version 0.33.0
### New Features
- Enabled SourceLink. For more details regarding SourceLink see https://github.com/dotnet/sourcelink
- Now supports 51 locales out of the box
- Address.City() can be localized
- Address.CitySuffix() can be localized
- Address.CityPrefix() can be localized
- Name.FirstName() can be localized
- Name.LastName() can be localized
- Added basic infrastructure for custom locale files (yml)

## Version 0.32.0
### Summary
Class Code is now on par with Ruby Faker for default locale ("en").
### New Features
- Code.Asin()
- Code.Sin()

## Version 0.31.0
### New Features
- Code.Imei()

## Version 0.30.0
### New Features
- Code.Nric(int minAge = 18, int maxAge = 65)

## Version 0.29.0
### New Features
- Code.Ean(int @base = 13)
- Code.Rut()

## Version 0.28.0
### New Features
- Code.Isbn(int @base = 10)
- Code.Npi()

## Version 0.27.0
### Summary
Date and Business are now on par with Ruby Faker for default locale ("en").
### New Features
- Business.CreditCardExpiryDate()
- Business.CreditCardNumber()
- Business.CreditCardType()
- Date.Birthday(int minAge = 18, int maxAge = 65)

## Version 0.26.0
### New Features
- Date.Backward(int days = 365)
- Date.Forward(int days = 365)

## Version 0.25.0
### New Features
- Date.Between(DateTime minDate, DateTime maxDate)
- Date.BetweenExcept(DateTime from, DateTime to, DateTime excepted)

## Version 0.24.0
### Summary
Educator is now on par with Ruby Faker for default locale ("en").
### New Features
- Educator.Campus()
- Educator.Course()
- Educator.SecondarySchool()
- Educator.University()

## Version 0.23.0
### Summary
Finance is now on par with Ruby Faker for default locale ("en").
### New Features
- Finance.CreditCard(params CreditCardType[] types)

## Version 0.22.0
### Summary
Internet is now on par with Ruby Faker for default locale ("en").
### New Features
- Internet.MacAddress(string prefix = "")
- Internet.Slug(string words = null, string glue = null)
- Internet.Url(string host = null, string path = null, string scheme = "http")
- Internet.UserAgent(string vendor = null)

## Version 0.21.0
### New Features
- Internet.IPv4CIDR()
- Internet.IPv6Address()
- Internet.IPv6CIDR()

## Version 0.20.0
### New Features
- Internet.IPv4Address()
- Internet.PrivateIPv4Address()
- Internet.PublicIPv4Address()

## Version 0.19.0
### New Features
- Internet.FreeEmail(string name = null)
- Internet.Password(int minLength = 8, int maxLength = 15, bool mixCase = true, bool specialChars = true)
- Internet.SafeEmail(string name = null)

## Version 0.18.0
### New Features
- Internet.Email(string name = null, string separators = null)
- Internet.UserName(int minLength, int maxLength = int.MaxValue)
- Internet.UserName(string name = null, string separators = null)

## Version 0.17.0
### New Features
- Internet.DomainName()
- Internet.DomainSuffix()
- Internet.DomainWord()
- Internet.UserName()

## Version 0.16.0
### Summary
IDNumber is now on par with Ruby Faker for default locale ("en").
### New Features
- IDNumber.SpanishForeignCitizenNumber()
- IDNumber.Invalid()
- IDNumber.SpanishCitizenNumber()
- IDNumber.Valid()

## Version 0.15.0
### Summary
Address is now on par with Ruby Faker for default locale ("en").
### New Features
- Address.City()
- Address.CountryCodeLong()
- Address.FullAddress()
- Address.Latitude()
- Address.Longitude()
- RandomNumber.NextDouble()

## Version 0.14.0
### New Features
- Address.CityPrefix()
- Address.Country()
- Address.CountryCode()
- Address.State()
- Address.StateAbbr()

## Version 0.13.0
### New Features
- NuGet package now also deploys XML documentation file for RimuTec.Faker.
- Address.CitySuffix()
- Address.Postcode(string stateAbbreviation = "")
- Address.StreetSuffix()
- Address.TimeZone()
- Address.Zip(string stateAbbreviation = "")

## Version 0.12.0
### New Features
- Address.BuildingNumber()
- Address.Community()
- Address.ZipCode(string stateAbbreviation = "")

## Version 0.11.0
### New Features
- Address.SecondaryAddress()
- Address.StreetAddress(bool includeSecondary = false)
- Address.StreetName()

## Version 0.10.0
### Summary
Company is now on par with Ruby Faker for default local ("en").
### New Features
- Company.AustralianBusinessNumber()
- Company.CzechOrganizationNumber()
- Company.FrenchSirenNumber()
- Company.FrenchSiretNumber()
- Company.NorwegianOrganizationNumber()
- Company.PolishRegisterOfNationalEconomy(int length = 9)
- Company.PolishTaxpayerIdentificationNumber()
- Company.SpanishOrganizationNumber()

## Version 0.9.0
### Summary
Lorem is now on par with Ruby Faker for default locale ("en").
### New Features
- Lorem.Paragraph(int sentenceCount = 3, bool supplemental = false, int randomSentencesToAdd = 0): Optional parameters and default values added.
- Lorem.ParagraphByChars(int chars = 256, bool supplemental = false)
- Lorem.Paragraphs(int paragraphCount = 3, bool supplemental = false): Optional parameters and default values added.
- Lorem.Question(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0)
- Lorem.Questions(int questionCount = 3, bool supplemental = false)
- PhoneNumber.LandLine(): Equivalent to Ruby Faker::PhoneNumber.phone_number
- Company.Bs()
- Company.Buzzword()
- Company.CatchPhrase()
- Company.DunsNumber()
- Company.Ein()
- Company.Industry()
- Company.Logo()
- Company.Profession()
- Company.Suffix()
- Company.SwedishOrganizationNumber()
- Company.Type()

## Version 0.8.0
### New Features
- Lorem.Sentence(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0): Optional parameters and default values added.
- Lorem.Sentences(int sentenceCount = 3, bool supplemental = false): Optional parameters and default values added.
### Bug Fixes
- FIX: Lorem.Words() does not consider list of supplementary words.
### Other Changes
- Updated package YamlDotNet to version 5.0.1

## Version 0.7.0
### New Features
- Lorem.Character()
- Lorem.Characters(int charCount = 255)
- Lorem.Multibyte()

## Version 0.6.0
### Breaking Changes
- Removed Lorem.GetFirstWord(). If you need to generate a defined word, use `RandomNumber.ResetSeed(int)` and then `Lorem.Word()` instead. We believe the impact of this change is minimal as the method was used for testing only. It does not exist in the Roby gem.

### New Features
- Company.Name()
- Lorem.Word()
- Lorem.Words(int count = 3, bool supplemental = false): Optional parameters and default values added.

## Version 0.5.0
### New Features
- Job.Field()
- Job.Seniority()
- Job.Position()
- Job.EmploymentType()
- Job.EducationLevel()

## Version 0.4.0
### New Features
- Name.FullName() (equivalent to Ruby's Faker::Name.name)
- Name.NameWithMiddle()
- Name.MiddleName()
- Name.Prefix()
- Name.Suffix()
- Name.Initials(int characterCount)

## Version 0.3.0
### New Features
- Job.KeySkill()

## Version 0.2.0
### New features
- Job.Title()
- Lorem.Words(int count)
- Lorem.GetFirstWord()
- Lorem.Sentence(int minWordCount)
- Lorem.Sentences(int sentenceCount)
- Lorem.Paragraph(int minSentenceCount)
- Lorem.Paragraphs(int paragraphCount)
- PhoneNumber.CellPhone()

## Version 0.1.0
### New features
- RandomNumber.Reset(int seed)
- RandomNumber.Next()
- RandomNumber.Next(int maxValue)
- RandomNumber.Next(int minValue, int maxValue)
- Name.First()
- Name.Last()
