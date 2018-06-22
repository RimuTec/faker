# Release Notes

## Version 0.8.0
### New Features
- Sentence(int wordCount = 4, bool supplemental = false, int randomWordsToAdd = 6): Optional parameters and default values added.

## Version 0.7.0
### New Features
- Lorem.Character()
- Lorem.Characters(int charCount = 255)
- Lorem.Multibyte()

## Version 0.6.0
### New Features
- Company.Name()
- Lorem.Word()
- Lorem.Words(int count = 3, bool supplemental = false): Optional parameters and default values added.

### Breaking Changes
- Removed Lorem.GetFirstWord(). If you need to generate a defined word, use `RandomNumber.ResetSeed(int)` and then `Lorem.Word()` instead. We believe the impact of this change is minimal as the method was used for testing only. It does not exist in the Roby gem.

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
