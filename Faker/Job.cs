namespace RimuTec.Faker
{
   /// <summary>
   /// Generator for Job related fake data.
   /// </summary>
   public class Job : GeneratorBase
   {
      private Job() { }

      /// <summary>
      /// Generates a job title, e.g. "Lead Accounting Associate".
      /// </summary>
      /// <returns></returns>
      public static string Title()
      {
         return Parse(Fetch("job.title"));
      }

      /// <summary>
      /// Generates a field of work, e.g. "Manufacturing".
      /// </summary>
      /// <returns></returns>
      public static string Field()
      {
         return Fetch("job.field");
      }

      /// <summary>
      /// Generates a seniority, e.g. "Lead".
      /// </summary>
      /// <returns></returns>
      public static string Seniority()
      {
         return Fetch("job.seniority");
      }

      /// <summary>
      /// Generates a position, e.g. "Supervisor".
      /// </summary>
      /// <returns></returns>
      public static string Position()
      {
         return Fetch("job.position");
      }

      /// <summary>
      /// Generates a key skill, e.g. "Teamwork".
      /// </summary>
      /// <returns></returns>
      public static string KeySkill()
      {
         return Fetch("job.key_skills");
      }

      /// <summary>
      /// Generates an employment type, e.g. "Full-time".
      /// </summary>
      /// <returns></returns>
      public static string EmploymentType()
      {
         return Fetch("job.employment_type");
      }

      /// <summary>
      /// Generates an education level, e.g. "Bachelor".
      /// </summary>
      /// <returns></returns>
      public static string EducationLevel()
      {
         return Fetch("job.education_level");
      }
   }
}
