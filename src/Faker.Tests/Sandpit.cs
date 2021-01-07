using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using RimuTec.Faker.Extensions;
using RimuTec.Faker.Tests.Extensions;

namespace RimuTec.Faker.Tests.OtherTests
{
   /// <summary>
   /// The sandpit class contains random code for experiments. Although written like tests,
   /// these are not considered a part of the test suite for RimuTec.Faker.
   /// </summary>
   [TestFixture]
   public class Sandpit
   {
      [TearDown]
      public void TearDown()
      {
         Config.Locale = "en";
      }

      [Test]
      public void NameWithMiddle_NullReferenceException_it()
      {
         Config.Locale = "it";
         RandomNumber.ResetSeed(42);
         try {
            var nameWithMiddle1 = Name.NameWithMiddle();
            var nameWithMiddle2 = Name.NameWithMiddle();
            Assert.AreNotEqual(nameWithMiddle1, nameWithMiddle2);
         }
         catch(Exception ex){
            Assert.Fail($"Locale {Config.Locale}. Exception '{ex}'");
         }
      }

      [Test]
      public void EmploymentType_Twice_NotEqual()
      {
         const string locale = "zh-TW";
         RandomNumber.ResetSeed(42);
         Config.Locale = locale;
         var employmentType1 = Job.EmploymentType();
         var employmentType2 = Job.EmploymentType();
         Assert.AreNotEqual(employmentType1, employmentType2,
            $"Locale '{locale}'. employmentType1 '{employmentType1}'. employmentType2 '{employmentType2}'."
         );
      }

      [Test]
      public void SsnValid()
      {
         var ssn = IdNumber.SsnValid();
         Assert.IsFalse(Regex.Match(ssn, "000-[0-9]{2}-[0-9]{4}|[0-9]{3}-00-[0-9]{4}|[0-9]{3}-[0-9]{2}-0000|666-[0-9]{2}-[0-9]{4}|9[0-9]{2}-[0-9]{2}-[0-9]{4}").Success);
      }

      [Test]
      public void Split()
      {
         var cultureToRestore = Thread.CurrentThread.CurrentCulture;
         Thread.CurrentThread.CurrentCulture = new CultureInfo("fa");
         Config.Locale = "fa";

         const string foo = "abc def";
         const string firstNames = "گل آرا‌";

         var fooAsChars = foo.ToCharArray();
         var firstNamesAsChars = firstNames.ToCharArray();

         var firstNamesAsArray = firstNames.Split(' ', '-', '\'').Select(s => s.Trim('\u200C').ToLower()).ToArray();
         Assert.AreEqual(2, firstNamesAsArray.Length);

         var firstFirstName = firstNamesAsArray[0];
         var secondFirstName = firstNamesAsArray[1];

         var firstFirstNameAsChars = firstFirstName.ToCharArray();
         var secondFirstNameAsChars = secondFirstName.ToCharArray();

         string pattern = $"^({firstFirstName}[._]{{1}}{secondFirstName}|{secondFirstName}[._]{{1}}{firstFirstName})";

         IEnumerable<string> parts = firstNames.Scan(@"\w+").Shuffle();
         var userName = string.Join("._".Sample(), parts).ToLower();

         Assert.AreEqual(1, Regex.Matches(userName, pattern).Count,
            $"userName is '{userName}'. Pattern is '{pattern}'"
         );

         Thread.CurrentThread.CurrentCulture = cultureToRestore;
      }

      [Test]
      public void German_City()
      {
         var germanSuffixes = new string[] { "stadt", "dorf", "land", "scheid", "burg", "berg", "heim", "hagen", "feld", "brunn", "grün" };
         Config.Locale = "de";
         var city = Address.City();
         Assert.IsTrue(germanSuffixes.Any(x => city.EndsWith(x)), $"{nameof(city)} was '{city}'");
         Config.Locale = "en";
      }

      [Test]
      public void German_FirstName()
      {
         Config.Locale = "de";
         var firstName = Name.FirstName();
         var firstNames = Name.FetchList("name.first_name");
         Assert.Greater(firstNames.Count, 0);
         Assert.IsTrue(firstNames.Contains(firstName));
      }

      [Test]
      public void German_LastName()
      {
         Config.Locale = "de";
         var lastName = Name.LastName();
         var lastNames = Name.FetchList("name.last_name");
         Assert.Greater(lastNames.Count, 0);
         Assert.IsTrue(lastNames.Contains(lastName));
      }

      [Test]
      public void Custom_Locale()
      {
         // RimuTec.Faker comes with all locale files included that are also part of Ruby's Faker gem.
         // In case you need some custom localization you can create a custom yaml file that provides
         // values for all or just a subset of the default locale (en). For a list of all available
         // locales see https://github.com/stympy/faker/tree/master/lib/locales/en

         // The custom local file needs to be in the same folder as RimuTec.Faker.dll, which is
         // usually the output directory of the project containing your unit tests.

         // Note that for this example we use the New Zealand Maori locale, "mi-NZ". First names and
         // last names are already in the embedded "en-NZ" locale (see en-nz.yml).
         // The sample local file mi-NZ.yml uses first names found at
         // https://www.dia.govt.nz/press.nsf/d77da9b523f12931cc256ac5000d19b6/18983b92b09bcf73cc257d1d00832439!OpenDocument
         // It uses last names found at https://en.wikipedia.org/wiki/Category:M%C4%81ori-language_surnames
         // and at https://en.wikipedia.org/wiki/Category:New_Zealand_M%C4%81ori_people
         // Should anyone feel offended by including or excluding a name in the file mi-NZ.yml, please create
         // an issue at https://github.com/RimuTec/Faker/issues to get it resolved. Thank you!

         // Maori New Zealand locale, for more details see 
         // https://teara.govt.nz/en/matauranga-hangarau-information-technology/page-3
         // http://homepages.ihug.co.nz/~trmusson/maori.html
         Config.Locale = "mi-NZ";
         var firstName = Name.FirstName();
         var firstNames = Name.FetchList("name.first_name");
         Assert.Greater(firstNames.Count, 0);
         Assert.IsTrue(firstNames.Contains(firstName));
      }

      [Test]
      public void Fallback_Is_Locale_En()
      {
         Config.Locale = "mi-NZ";
         var citySuffix = Address.CitySuffix();
         Config.Locale = "en";
         var citySuffixes = Address.FetchList("address.city_suffix");
         Assert.IsFalse(string.IsNullOrWhiteSpace(citySuffix));
         Assert.IsTrue(citySuffixes.Contains(citySuffix));
         Assert.Greater(citySuffixes.Count, 0);
      }

      [Test]
      public void TryLoadingInvalidLocaleFile()
      {
         const string locale = "no";
         Config.Locale = locale; // file no.yml contains locale 'no-NO' which is different to what the filename should be.
         var ex = Assert.Throws<FormatException>(() => Address.ZipCode());
         Assert.AreEqual($"File for locale '{locale}' has an invalid format. [Code 201213-1413]", ex.Message);
      }

      [Test]
      public void Lorem_Words_Locale_Ja()
      {
         Config.Locale = "ja";
         var tries = 1000;
         while (--tries > 0)
         {
            var word = Lorem.Word();
            Assert.False(word.Contains(' '));
            Assert.False(word.Contains(','));
         }
      }

      [Test]
      public void Words_Locale_Ko()
      {
         Config.Locale = "ko";
         var someWords = new[] { "아니하며", "보호한다" };
         foreach (var word in someWords)
         {
            Assert.IsTrue(WordList.Contains(word),
               $"Missing word is '{word}'. {WordList} is '{string.Join('|', WordList.ToArray())}'"
            );
         }

         var tries = 100;
         while (--tries > 0)
         {
            var someOtherWord = Lorem.Word();
            Assert.IsTrue(WordList.Contains(someOtherWord),
               $"Missing word is '{someOtherWord}'"
            );
         }
      }

      [Test]
      public void ParagraphByChars()
      {
         Config.Locale = "zh-TW";
         const int defaultChars = 256 + 1; // +1 for period
         var paragraph = Lorem.ParagraphByChars();
         Assert.AreEqual(defaultChars, paragraph.Length);
         var checkCount = 0;
         Assert.IsTrue(SupplementalWordList.Contains("abbas"));
         Assert.IsTrue(SupplementalWordList.Contains("cum"));
         var wordListAsLower = WordList.Select(w => w.ToLower());
         foreach (var word in paragraph.ToWordList().Take(paragraph.Length - 1)) // last word may be truncated or padded
         {
            if (paragraph.ToLower().EndsWith(word + Lorem.PunctuationPeriod()))
            {
               // don't check last word as it might be truncated or padded
               return;
            }
            // but check all others
            checkCount++;
            Assert.IsTrue(wordListAsLower.Contains(word.ToLower()),
               $"{nameof(Config.Locale)} '{Config.Locale}'. Missing word is '{word}'. {nameof(paragraph)} is '{paragraph}'. {nameof(wordListAsLower)} is '{string.Join('|', wordListAsLower.ToArray())}'"
            );
         }
         Assert.Greater(checkCount, 10);
      }

      [Test]
      public void Foo()
      {
         Config.Locale = "ja";
         const int wordCount = 3;
         var combined = wordCount.Times(_ => Lorem.Word()).Concat(wordCount.Times(_ => Lorem.SupplementalWord())).ToArray();
         //result.AddRange(combined.Shuffle().Take(wordCount));
      }

      [Test]
      public void StringSplitWithLocalJa()
      {
         Config.Locale = "ja";
         var japaneseSpace = Lorem.PunctuationSpace();
         const string sentence = "雇用　弥生　軒　ふそく　輸出　フランス語　はちまき　縛る　しばふ　弱虫　助手　がいよう　うえる　しばふ　輸出　みなと　零す　むこう　きょうかい　せんりゅう　特殊　せいぞう　のき　しょうゆ　避ける　擬装　電話　廃墟　とふ　ほうげん　こうせい　とうさん　下さい。";
         var words = sentence.Split(japaneseSpace.ToCharArray());
         Assert.AreEqual(33, words.Length);
      }

      [Test]
      public void InternetFreeEmail_en()
      {
         Config.Locale = "en";
         _ = Internet.FetchList("internet.free_email");
         // shouldn't throw exception
      }

      [Test]
      public void InternetFreeEmail_zh_CN()
      {
         Config.Locale = "zh-CN";
         _ = Internet.FetchList("internet.free_email");
         // shouldn't throw exception
      }

      [Test]
      public void StreetAddress_With_SecondaryAddress()
      {
         Config.Locale = "ja";
         var seps = new List<char>();
         seps.AddRange(Lorem.PunctuationSpace().ToCharArray());
         seps.Add(' ');
         var secondaryAddressPatternParts = string.Join(Lorem.PunctuationSpace(), Address.FetchList("address.secondary_address")).Split(seps.ToArray());
         var streetAddress = Address.StreetAddress(true);
         Assert.IsFalse(streetAddress.Contains("#"));
         Assert.IsFalse(streetAddress.Contains("?"));
         Assert.IsTrue(secondaryAddressPatternParts.Any(p => streetAddress.Contains(p)),
            $"Locale {Config.Locale}. streetAddress is '{streetAddress}'. parts are '{string.Join("|", secondaryAddressPatternParts)}'"
         );
      }

      [Test]
      public void StreetName()
      {
         Config.Locale = "he";
         var streetName = Address.StreetName();
         Assert.IsFalse(string.IsNullOrWhiteSpace(streetName));
         Assert.IsFalse(streetName.Contains("#"),
            $"Locale '{Config.Locale}'. streetName is '{streetName}'"
         );
         Assert.IsFalse(streetName.Contains("?"));
      }

      [Test]
      public void NamePrefix_de()
      {
         Config.Locale = "de";
         var prefix = Name.Prefix();
         var prefixes = Name.FetchList("name.prefix");
         Assert.IsTrue(prefixes.Contains("Dipl.-Ing."));
         Assert.IsTrue(prefixes.Contains(prefix));
      }

      /// <summary>
      /// Words in the standard list. Duplicates removed.
      /// </summary>
      public string[] WordList
      {
         get
         {
            // No caching here as we use different locales in this file.
            var temp = new List<string>();
            var toTrim = Lorem.PunctuationSpace() + Lorem.PunctuationPeriod() + "., ";
            foreach (var word in Lorem.FetchList("lorem.words"))
            {
               temp.AddRange(word.Split(',').Select(w => w.Trim(toTrim.ToCharArray())));
            }
            _wordList = temp.ToArray();
            return _wordList;
         }
      }

      /// <summary>
      /// Words in the supplementary list. Duplicates are removed.
      /// </summary>
      public string[] SupplementalWordList
      {
         get
         {
            // No caching here as we use different locales in this file.
            var temp = new List<string>();
            var toTrim = Lorem.PunctuationSpace() + Lorem.PunctuationPeriod() + "., ";
            foreach (var word in Lorem.FetchList("lorem.supplemental"))
            {
               temp.AddRange(word.Split(',').Select(w => w.Trim(toTrim.ToCharArray())));
            }
            _supplementalWordList = temp.ToArray();
            return _supplementalWordList;
         }
      }

      /// <summary>
      /// Words that are in both, the base list and the supplementary list.
      /// </summary>
      public string[] JointWords
      {
         get
         {
            // No caching here as we use different locales in this file.
            var intersection = WordList.Intersect(SupplementalWordList);
            _jointWords = intersection.Distinct().ToArray();
            return _jointWords;
         }
      }

      private string[] _supplementalWordList;
      private string[] _wordList;
      private string[] _jointWords;
   }
}
