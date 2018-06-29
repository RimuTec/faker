# Faker
A C# port of the Ruby Faker gem [https://github.com/stympy/faker](https://github.com/stympy/faker). It uses the latest version of their yaml files.

RimuTec.Faker targets .NET Standard 2.0 (netstandard2.0) and .NET Framework 4.6.2 (net462). 

The library including its source code are licensed under the MIT license.

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

Basic methods:
- Bs()
- Buzzword()
- CatchPhrase()
- DunsNumber()
- Ein()
- Industry()
- Logo()
- Name()
- Profession()
- Suffix()
- Type()

Country specific methods:
- AustralianBusinessNumber()
- CzechOrganizationNumber()
- FrenchSirenNumber()
- FrenchSiretNumber()
- NorwegianOrganizationNumber()
- PolishRegisterOfNationalEconomy(int length = 9)
- PolishTaxpayerIdentificationNumber()
- SpanishOrganizationNumber()
- SwedishOrganizationNumber()

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
RimuTec.Faker has a test suite with about 150 unit tests. This does not guarantee absence of bugs. Please report all bugs at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) ideally including steps to reproduce. We also consider pull requests (PR). All your feedback will help make the library more valuable for other users as well. Thank you!

# Credits
This project uses the yaml files from the [Ruby Faker gem](https://github.com/stympy/faker), licensed under the MIT license. Thank you to all their contributors!
