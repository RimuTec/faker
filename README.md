# Faker
A C# port of the Ruby Faker gem [https://github.com/stympy/faker](https://github.com/stympy/faker). 

RimuTec.Faker aims at achieving parity with Ruby Faker. Classes that are on par with Ruby Faker are marked accordingly (see below).

RimuTec.Faker targets .NET Standard 2.0 (netstandard2.0) and .NET Framework 4.6.2 (net462). 

The library including its source code are licensed under the MIT license.

# Installation

RimuTec.Faker is available as a NuGet package. To install follow the instructions at [https://www.nuget.org/packages/RimuTec.Faker/](https://www.nuget.org/packages/RimuTec.Faker/).

| Metric      | Status      |
| ----- | ----- |
| Nuget             | [![NuGet Badge](https://buildstats.info/nuget/RimuTec.Faker)](https://www.nuget.org/packages/RimuTec.Faker/) |

# Available Fake Data Generators
Because Ruby Faker has a large number of generators, we had to start with a small set of classes. Our aim is to add the remaining classes and method over time. If you have preferences please file suggestions as issues on Github (see below). Thank you!

## Address
This class is on par with Ruby Faker for default locale ("en").
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

## Company
This class is on par with Ruby Faker for default locale ("en").
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

## Educator
This class is on par with Ruby Faker for default locale ("en").
- Campus()
- Course()
- SecondarySchool()
- University()

## Finance
This class is on par with Ruby Faker for default locale ("en").
- CreditCard(params CreditCardType[] types)

## IDNumber
This class is on par with Ruby Faker for default locale ("en").
- SpanishForeignCitizenNumber()
- Invalid()
- SpanishCitizenNumber()
- Valid()

## Internet
This class is on par with Ruby Faker for default locale ("en").
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

## Name
This class is on par with Ruby Faker for default locale ("en").
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
RimuTec.Faker has a test suite with about 220 unit tests. This does not guarantee absence of bugs. Please report all bugs at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) ideally including steps to reproduce. We also consider pull requests (PR). All your feedback will help make the library more valuable for other users as well. Thank you!

# Credits
This project uses the yaml files from the [Ruby Faker gem](https://github.com/stympy/faker), licensed under the MIT license. Thank you to all their contributors!
