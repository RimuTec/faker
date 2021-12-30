using NUnit.Framework;
using System.Linq;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(DefaultFixtureData), nameof(DefaultFixtureData.FixtureParams))]
   public class JobTests
   {
      public JobTests(string locale)
      {
         Locale = locale;
      }

      [SetUp]
      public void SetUp()
      {
         RandomNumber.ResetSeed(42);
         Config.Locale = Locale;
      }

      private string Locale { get; }

      [Test]
      public void Title_HappyDays()
      {
         var title = Job.Title();
         Assert.IsFalse(string.IsNullOrWhiteSpace(title));
         Assert.IsTrue(title.Contains(" "));
      }

      [Test]
      public void Title_Twice_NotEqual()
      {
         var title1 = Job.Title();
         var title2 = Job.Title();
         Assert.AreNotEqual(title1, title2);
      }

      [Test]
      public void Field_HappyDays()
      {
         var field = Job.Field();
         Assert.IsFalse(string.IsNullOrWhiteSpace(field));
         Assert.AreEqual(0, field.Count(c => c == ' '));
      }

      [Test]
      public void Field_Twice_NotEqual()
      {
         var field1 = Job.Field();
         var field2 = Job.Field();
         Assert.AreNotEqual(field1, field2);
      }

      [Test]
      public void Seniority_HappyDays()
      {
         var seniority = Job.Seniority();
         Assert.IsFalse(string.IsNullOrWhiteSpace(seniority));
         Assert.AreEqual(0, seniority.Count(c => c == ' '));
      }

      [Test]
      public void Seniority_Twice_NotEqual()
      {
         var seniority1 = Job.Seniority();
         var seniority2 = Job.Seniority();
         Assert.AreNotEqual(seniority1, seniority2);
      }

      [Test]
      public void Position_HappyDays()
      {
         var position = Job.Position();
         Assert.IsFalse(string.IsNullOrWhiteSpace(position));
         Assert.AreEqual(0, position.Count(c => c == ' '));
      }

      [Test]
      public void Position_Twice_NotEqual()
      {
         var position1 = Job.Position();
         var position2 = Job.Position();
         Assert.AreNotEqual(position1, position2);
      }

      [Test]
      public void KeySkill_HappyDays()
      {
         var keySkill = Job.KeySkill();
         // As we seed the random number for each test, this should give us "Networking skills":
         Assert.AreEqual("Networking skills", keySkill,
            $"Locale '{Locale}'. Invalid value: '{keySkill}'"
         );
      }

      [Test]
      public void KeySkill_Twice_NotEqual()
      {
         var keySkill1 = Job.KeySkill();
         var keySkill2 = Job.KeySkill();
         Assert.AreNotEqual(keySkill1, keySkill2);
      }

      [Test]
      public void EmploymentType_HappyDays()
      {
         var employmentType = Job.EmploymentType();
         Assert.IsFalse(string.IsNullOrWhiteSpace(employmentType));
         Assert.AreEqual(0, employmentType.Count(c => c == ' '));
      }

      [Test]
      public void EmploymentType_Twice_NotEqual()
      {
         // const string locale = "zh-TW";
         // RandomNumber.ResetSeed(42);
         // Config.Locale = locale;
         var employmentType1 = Job.EmploymentType();
         var employmentType2 = Job.EmploymentType();
         Assert.AreNotEqual(employmentType1, employmentType2,
            $"Locale '{Locale}'. employmentType1 '{employmentType1}'. employmentType2 '{employmentType2}'."
         );
      }

      [Test]
      public void EducationLevel_HappyDays()
      {
         var educationLevel = Job.EducationLevel();
         Assert.IsFalse(string.IsNullOrWhiteSpace(educationLevel));
         Assert.AreEqual(0, educationLevel.Count(c => c == ' '));
      }

      [Test]
      public void EducationLevel_Twice_NotEqual()
      {
         var educationLevel1 = Job.EducationLevel();
         var educationLevel2 = Job.EducationLevel();
         Assert.AreNotEqual(educationLevel1, educationLevel2);
      }
   }
}
