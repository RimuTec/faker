using NUnit.Framework;
using System.Linq;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class JobTests {
      [SetUp]
      public void SetUp() {
         RandomNumber.ResetSeed(42);
      }

      [Test]
      public void Title_HappyDays() {
         // arrange

         // act
         var title = Job.Title();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(title));
         Assert.IsTrue(title.Contains(" "));
      }

      [Test]
      public void Title_Twice_NotEqual() {
         // arrange

         // act
         var title1 = Job.Title();
         var title2 = Job.Title();

         // assert
         Assert.AreNotEqual(title1, title2);
      }

      [Test]
      public void Field_HappyDays() {
         // arrange

         // act
         var field = Job.Field();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(field));
         Assert.AreEqual(0, field.Count(c => c == ' '));
      }

      [Test]
      public void Field_Twice_NotEqual() {
         // arrange

         // act
         var field1 = Job.Field();
         var field2 = Job.Field();

         // assert
         Assert.AreNotEqual(field1, field2);
      }

      [Test]
      public void Seniority_HappyDays() {
         // arrange

         // act
         var seniority = Job.Seniority();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(seniority));
         Assert.AreEqual(0, seniority.Count(c => c == ' '));
      }

      [Test]
      public void Seniority_Twice_NotEqual() {
         // arrange

         // act
         var seniority1 = Job.Seniority();
         var seniority2 = Job.Seniority();

         // assert
         Assert.AreNotEqual(seniority1, seniority2);
      }

      [Test]
      public void Position_HappyDays() {
         // arrange

         // act
         var position = Job.Position();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(position));
         Assert.AreEqual(0, position.Count(c => c == ' '));
      }

      [Test]
      public void Position_Twice_NotEqual() {
         // arrange

         // act
         var position1 = Job.Position();
         var position2 = Job.Position();

         // assert
         Assert.AreNotEqual(position1, position2);
      }

      [Test]
      public void KeySkill_HappyDays() {
         // arrange

         // act
         var keySkill = Job.KeySkill();

         // assert
         // As we seed the random number for each test, this gives us "Networking skills":
         Assert.AreEqual("Networking skills", keySkill);
      }

      [Test]
      public void KeySkill_Twice_NotEqual() {
         // arrange

         // act
         var keySkill1 = Job.KeySkill();
         var keySkill2 = Job.KeySkill();

         // assert
         Assert.AreNotEqual(keySkill1, keySkill2);
      }

      [Test]
      public void EmploymentType_HappyDays() {
         // arrange

         // act
         var employmentType = Job.EmploymentType();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(employmentType));
         Assert.AreEqual(0, employmentType.Count(c => c == ' '));
      }

      [Test]
      public void EmploymentType_Twice_NotEqual() {
         // arrange

         // act
         var employmentType1 = Job.EmploymentType();
         var employmentType2 = Job.EmploymentType();

         // assert
         Assert.AreNotEqual(employmentType1, employmentType2);
      }

      [Test]
      public void EducationLevel_HappyDays() {
         // arrange

         // act
         var educationLevel = Job.EducationLevel();

         // assert
         Assert.IsFalse(string.IsNullOrWhiteSpace(educationLevel));
         Assert.AreEqual(0, educationLevel.Count(c => c == ' '));
      }

      [Test]
      public void EducationLevel_Twice_NotEqual() {
         // arrange

         // act
         var educationLevel1 = Job.EducationLevel();
         var educationLevel2 = Job.EducationLevel();

         // assert
         Assert.AreNotEqual(educationLevel1, educationLevel2);
      }
   }
}
