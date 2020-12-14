using NUnit.Framework;

namespace RimuTec.Faker.Tests.OtherTests
{
   public class OtherTests
   {
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
   }
}
