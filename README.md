# Faker
RimuTec.Faker provides generators for fake, yet realistically looking data. Use it for testing, for creating screenshots to show off your cool software, and similar more. Generators include Lorem, Name, Address, Date, Company, Business, and many more.

RimuTec.Faker is a C# port of the Ruby Faker gem [https://github.com/stympy/faker](https://github.com/stympy/faker). The port is not complete yet but we are continuously adding classes and methods.

RimuTec.Faker targets .NET Standard 2.0 (netstandard2.0) and .NET Framework 4.6.2 (net462). The library including its source code are licensed under the MIT license. It supports 51 locales out of the box. And you can extend it with your own custom locales using yaml files.

| Metric      | Status      |
| ----- | ----- |
| Nuget       | [![NuGet Badge](https://buildstats.info/nuget/RimuTec.Faker)](https://www.nuget.org/packages/RimuTec.Faker/) |

# Installation

RimuTec.Faker is available as a NuGet package. To install follow the instructions at [https://www.nuget.org/packages/RimuTec.Faker/](https://www.nuget.org/packages/RimuTec.Faker/).

# Available Fake Data Generators
Because Ruby Faker has a large number of generators, we had to start with a small set of classes. Our aim is to add the remaining classes and method over time. If you have preferences please file suggestions as issues on Github (see below). Thank you!

## Supported Locales and Customization
The following 51 locales are supported out of the box (no extra files needed):

bg, ca, ca-CAT, da-DK, de, de-AT, de-CH, ee, en, en-AU, en-au-ocker, en-BORK, en-CA, en-GB, en-IND, en-MS, en-NEP, en-NG, en-NZ, en-PAK, en-SG, en-UG, en-US, en-ZA, es, es-MX, fa, fi-FI, fr, fr-CA, fr-CH, he, id, it, ja, ko, lv, nb-NO, nl, no, pl, pt, pt-BR, ru, sk, sv, tr, uk, vi, zh-CN, zh-TW

Classes that already make use of locales are marked in the list below. To set the locale use something like `Config.Locale = "de";`.

In addition you can use custom locale files for methods that are marked with an asterisk. Ensure that the custom locale file (yml) is copied to the directory that also contains RimuTec.Faker.dll, usually the output directory of your test project.

## Address
This class supports all 51 built-in and custom locales.
- BuildingNumber()
- City()
- CityPrefix()
- CitySuffix()
- Community()
- Country()
- CountryCode()
- CountryCodeLong()
- FullAddress()
- Latitude()
- Longitude()
- Postcode(string stateAbbreviation = "")
- SecondaryAddress()
- State()
- StateAbbr()
- StreetAddress(bool includeSecondary = false)
- StreetName()
- StreetSuffix()
- TimeZone()
- Zip(string stateAbbreviation = "")
- ZipCode(string stateAbbreviation = "")

## Business
This class supports all 51 built-in and custom locales.
- CreditCardExpiryDate()
- CreditCardNumber()
- CreditCardType()

## Code
This class supports all 51 built-in and custom locales.
- Asin()
- Ean(int @base = 13)
- Imei()
- Isbn(int @base = 10)
- Npi()
- Nric(int minAge = 18, int maxAge = 65)
- Rut()
- Sin()

## Company
This class supports all 51 built-in and custom locales.
- AustralianBusinessNumber()
- Bs()
- Buzzword()
- CatchPhrase()
- CzechOrganizationNumber()
- DunsNumber()
- Ein()
- FrenchSirenNumber()
- FrenchSiretNumber()
- Industry()
- Logo()
- Name()
- NorwegianOrganizationNumber()
- PolishRegisterOfNationalEconomy(int length = 9)
- PolishTaxpayerIdentificationNumber()
- Profession()
- SpanishOrganizationNumber()
- Suffix()
- SwedishOrganizationNumber()
- Type()

## Date
This class supports all 51 built-in and custom locales.
- Backward(int days = 365)
- Between(DateTime minDate, DateTime maxDate)
- BetweenExcept(DateTime from, DateTime to, DateTime excepted)
- Birthday(int minAge = 18, int maxAge = 65)
- Forward(int days = 365)

## Educator
This class supports all 51 built-in and custom locales.
- Campus()
- Course()
- SecondarySchool()
- University()

## Finance
This class supports all 51 built-in and custom locales.
- CreditCard(params CreditCardType[] types)

## IDNumber
This class supports all 51 built-in and custom locales.
- SpanishForeignCitizenNumber()
- Invalid()
- SpanishCitizenNumber()
- Valid()

## Internet
This class supports all 51 built-in and custom locales.
- DomainName()
- DomainSuffix()
- DomainWord()
- Email(string name = null, string separators = null)
- FreeEmail(string name = null)
- IPv4Address()
- IPv4CIDR()
- IPv6Address()
- IPv6CIDR()
- MacAddress(string prefix = "")
- Password(int minLength = 8, int maxLength = 15, bool mixCase = true, bool specialChars = true)
- PrivateIPv4Address()
- PublicIPv4Address()
- SafeEmail(string name = null)
- Slug(string words = null, string glue = null)
- Url(string host = null, string path = null, string scheme = "http")
- UserAgent(string vendor = null)
- UserName(int minLength, int maxLength = int.MaxValue)
- UserName(string name = null, string separators = null)

## Job
- EmploymentType()
- EducationLevel()
- Field()
- KeySkill()
- Position()
- Seniority()
- Title()

## Lorem
This class is on par with Ruby Faker for default locale ("en").
- Character()
- Characters(int charCount = 255)
- Multibyte()
- Paragraph(int sentenceCount = 3, bool supplemental = false, int randomSentencesToAdd = 0)
- ParagraphByChars(int chars = 256, bool supplemental = false)
- Paragraphs(int paragraphCount = 3, bool supplemental = false)
- Question(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0)
- Questions(int questionCount = 3, bool supplemental = false)
- Sentence(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 0)
- Sentences(int sentenceCount = 3, bool supplemental = false)
- Word()
- Words(int wordCount = 3, bool supplemental = false)

## Name *
This class is on par with Ruby Faker for all 51 built-in and custom locales.
- FirstName()
- FullName() (equivalent to Ruby's Faker::Name.name)
- Initials(int characterCount)
- LastName()
- MiddleName()
- NameWithMiddle()
- Prefix()
- Suffix()

## PhoneNumber
- CellPhone()
- LandLine(): Equivalent to Ruby Faker::PhoneNumber.phone_number

## RandomNumber
- Next()
- NextDouble()
- Next(int maxValue)
- Next(int minValue, int maxValue)
- ResetSeed(int seed)

# Usage
## Quick Start
1. Install NuGet package. See [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker) for instructions
1. Add `using RimuTec.Faker;` at the beginning of your C# source file (or the equivalent for your preferred .NET language)
1. Generate fake data, e.g. `var firstName = Name.NameWithMiddle();`

In case of name clashes with other classes in your code base, use one of the following techniques in C# source files:
1. use fully qualify names, e.g. `var firstName = RimuTec.Faker.Name.NameWithMiddle();`. Use this if there are only a couple of occurances.
1. add `using NameFaker = RimuTec.Faker.Name;` at the beginning of the file and then `var firstName = NameFaker.NameWithMiddle();`. Use this option if you have many usages within the source file.

## Release Notes
Release notes are available at [https://github.com/RimuTec/Faker/blob/master/releasenotes.md](https://github.com/RimuTec/Faker/blob/master/releasenotes.md)

## Support & Suggestions
If you need support or have a suggestion for improvement please file an issue at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues). Thank you!

## Reporting Bugs
RimuTec.Faker has a test suite with about 270 unit tests. This does not guarantee absence of bugs. Please report all bugs at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) ideally including steps to reproduce. We also consider pull requests (PR). All your feedback will help make the library more valuable for other users as well. Thank you!

# How To Build
## Visual Studio 2017
Open Faker.sln in Visual Studio, select the desired configuration ("DEBUG" or "RELEASE") and then build the solution.

## Command Line
1. Open Powershell and navigate to directory containing Faker.sln
1. Execute the command `dotnet build --configuration RELEASE Faker.sln`. Replace RELEASE with DEBUG if you want build the DEBUG configuration

## Issues With Building
If you encounter issues with building the library please file an issue on GitHub, ideally with what you tried, what the expected and what the actual outcome was. Thank you!

# Credits
This project uses the yaml files from the [Ruby Faker gem](https://github.com/stympy/faker), licensed under the MIT license. Thank you to all their contributors!
