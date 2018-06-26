# Release Notes

## Version 0.12.0
### New Features
- Address.BuildingNumber()

## Version 0.11.0
### New Features
- Address.SecondaryAddress()
- Address.StreetAddress(bool includeSecondary = false)
- Address.StreetName()

## Version 0.10.0
Company is now on pare with Ruby Faker for default local ("en").

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
