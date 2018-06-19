# Faker
A C# port of the Ruby Faker gem (https://github.com/stympy/faker)

# Available Classes and Method
## Job
- KeySkill()
- Title()
## Lorem
- Words(int count)
- GetFirstWord()
- Sentence(int minWordCount)
- Sentences(int sentenceCount)
- Paragraph(int minSentenceCount)
- Paragraphs(int paragraphCount)
## Name
- FirstName()
- LastName()
## PhoneNumber
- CellPhone()
## RandomNumber
- ResetSeed(int seed)
- Next()
- Next(int maxValue)
- Next(int minValue, int maxValue)

# How To Install
This library is available as NuGet package at https://www.nuget.org/packages/RimuTec.Faker

The source code and the library are available under MIT license. This means you can use this libary and/or the source for free in your own projects, including closed-source and commercial projects. Terms and conditions are as per the MIT license in this project.

## Release Notes
Release notes are available at https://github.com/RimuTec/Faker/blob/master/releasenotes.md

# Credits
The project uses some portions of the source code of https://github.com/slashdotdash/faker-cs under a MIT license.

This project also uses the yaml files of the Ruby Faker gem from https://github.com/stympy/faker under a MIT license.
