# Faker
A C# port of the Ruby Faker gem [https://github.com/stympy/faker](https://github.com/stympy/faker). It uses the latest version of their yaml files.

RimuTec.Faker targets .NET Standard 2.0 (netstandard2.0) and .NET Framework 4.6.2 (net462). 

# Available Fake Data Generators
Because Ruby Faker has a large number of generators, we had to start with a small set of classes. Our aim is to add the remaining classes and method over time. If you have preferences please file suggestions as issues on Github (see below). Thank you!
## Company
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
- Profession()
- Suffix()
- SwedishOrganizationNumber()
- Type()

## Job
- EmploymentType()
- EducationLevel()
- Field()
- KeySkill()
- Position()
- Seniority()
- Title()

## Lorem
This class is on par with Ruby Faker.
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
- Next(int maxValue)
- Next(int minValue, int maxValue)
- ResetSeed(int seed)

# Usage
## Installation
This library is available as NuGet package at [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker)

The source code and the library are available under a MIT license. This means you can use this libary and/or the source for free in your own projects, including closed-source and commercial projects. Terms and conditions are as per the MIT license in this project.

## Generating Fake Data
1. Install NuGet package. See [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker) for instructions
1. Add `using RimuTec.Faker;` at the beginning of your C# source file (or the equivalent for your preferred .NET language)
1. Generate fake data, e.g. `var firstName = Name.NameWithMiddle();`

In case of name clashes with other classes in your code base, use one of the following techniques in C# source files:
1. use fully qualify names, e.g. `var firstName = RimuTec.Faker.Name.NameWithMiddle();`. Use this if there are only a couple of occurances.
1. add `using NameFaker = RimuTec.Faker.Name;` at the beginning of the file and then `var firstName = NameFaker.NameWithMiddle();`. Use this option if you have many usages within the source file.

## Release Notes
Release notes are available at [https://github.com/RimuTec/Faker/blob/master/releasenotes.md](https://github.com/RimuTec/Faker/blob/master/releasenotes.md)

## Reporting Issues
Although RimuTec.Faker has about 110 unit tests, that is no proof of absence of bugs. To help making this library more valuable for other users please report bugs at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues) ideally including steps to reproduce. We also consider pull requests (PR). Thank you!

## Support & Suggestions
If you have a support question please file an issue at [https://github.com/RimuTec/Faker/issues](https://github.com/RimuTec/Faker/issues). Thank you!

If you have a suggestion for improvement please file an issue as well. Thank you!

# Credits
Thank you to all contributors of the following projects. Their work is much appreciated.
## faker-cs
This project uses some code portions from [faker-cs](https://github.com/slashdotdash/faker-cs) under a MIT license.
## Ruby Faker gem
This project uses the yaml files from the [Ruby Faker gem](https://github.com/stympy/faker) under a MIT license.
