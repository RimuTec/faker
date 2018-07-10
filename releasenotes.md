# Release Notes

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
