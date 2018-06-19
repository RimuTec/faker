# Faker
A C# port of the Ruby Faker gem (https://github.com/stympy/faker)

# Available Classes and Method
## Job
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

The source code and the library are available under the MIT license. This means you can use this libary and/or the source in your own projects, including closed-source and commercial projects for free. Terms and conditions are as per the MIT license in this project.

## Release Notes
Release notes are available at https://github.com/RimuTec/Faker/blob/master/releasenotes.md

# Credits
The project uses some portions of the source code of https://github.com/slashdotdash/faker-cs under the MIT license.

This project also uses the yaml files of the Ruby Faker gem at https://github.com/stympy/faker under the MIT license.
