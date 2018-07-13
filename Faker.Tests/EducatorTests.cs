using NUnit.Framework;

namespace RimuTec.Faker.Tests {
   [TestFixture]
   public class EducatorTests : FixtureBase {
      [Test]
      public void Campus_Happy_Days() {
         var campus = Educator.Campus();
         Assert.IsFalse(string.IsNullOrWhiteSpace(campus));
         Assert.Greater(RegexMatchesCount(campus, @" "), 0);
      }

      [Test]
      public void Course_Happy_Days() {
         var course = Educator.Course();
         Assert.IsFalse(string.IsNullOrWhiteSpace(course));
         Assert.Greater(RegexMatchesCount(course, @" "), 0);
      }

      [Test]
      public void SecondarySchool_Happy_Days() {
         var secondarySchool = Educator.SecondarySchool();
         Assert.IsFalse(string.IsNullOrWhiteSpace(secondarySchool));
         Assert.Greater(RegexMatchesCount(secondarySchool, @" "), 0);
      }

      [Test]
      public void University_Happy_Days() {
         var university = Educator.University();
         Assert.IsFalse(string.IsNullOrWhiteSpace(university));
         Assert.Greater(RegexMatchesCount(university, @" "), 0);
      }
   }
}
