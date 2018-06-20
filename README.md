# Faker
A C# port of the Ruby Faker gem [https://github.com/stympy/faker](https://github.com/stympy/faker).

The RimuTec.Faker targets .NET Standard 2.0 (netstandard2.0) and .NET Framework 4.6.2 (net462).

# Available Fake Data Generators
## Job
- Title()
- Field()
- Seniority()
- Position()
- KeySkill()
- EmploymentType()
- EducationLevel()
## Lorem
- Words(int count)
- GetFirstWord()
- Sentence(int minWordCount)
- Sentences(int sentenceCount)
- Paragraph(int minSentenceCount)
- Paragraphs(int paragraphCount)
## Name
- FullName() (equivalent to Ruby's Faker::Name.name)
- NameWithMiddle()
- FirstName()
- MiddleName()
- LastName()
- Prefix()
- Suffix()
- Initials(int characterCount)
## PhoneNumber
- CellPhone()
## RandomNumber
- ResetSeed(int seed)
- Next()
- Next(int maxValue)
- Next(int minValue, int maxValue)

# Usage
## Installation
This library is available as NuGet package at [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker)

The source code and the library are available under MIT license. This means you can use this libary and/or the source for free in your own projects, including closed-source and commercial projects. Terms and conditions are as per the MIT license in this project.

## Generating Fake Data
1. Install NuGet package. See [https://www.nuget.org/packages/RimuTec.Faker](https://www.nuget.org/packages/RimuTec.Faker) for instructions
1. Add `using RimuTec.Faker;` at the beginning of your C# source file (or the equivalent for your preferred .NET language)
1. Generate fake data, e.g. `var firstName = Name.NameWithMiddle();`

In case of name clashes with other classes in your code base, use one of the following techniques in C# source files:
1. use fully qualify names, e.g. `var firstName = RimuTec.Faker.Name.NameWithMiddle();`. Use this if there are only a couple of occurances.
1. add `using NameFaker = RimuTec.Faker.Name;` at the beginning of the file and then `var firstName = NameFaker.NameWithMiddle();`. Use this option if you have many usages within the source file.

## Release Notes
Release notes are available at [https://github.com/RimuTec/Faker/blob/master/releasenotes.md](https://github.com/RimuTec/Faker/blob/master/releasenotes.md)

# Credits
The project uses some portions of the source code of [https://github.com/slashdotdash/faker-cs](https://github.com/slashdotdash/faker-cs) under a MIT license.

This project also uses the yaml files of the Ruby Faker gem from [https://github.com/stympy/faker](https://github.com/stympy/faker) under a MIT license.
