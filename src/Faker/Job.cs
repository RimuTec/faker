using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   /// <summary>
   /// Generator for Job related fake data.
   /// </summary>
   public static class Job {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/job.yml
      // [Manfred, 03jun2018]

      static Job() {
         const string yamlFileName = "RimuTec.Faker.locales.en.job.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _job = locale.en.faker.job;
      }

      /// <summary>
      /// Generates a job title, e.g. "Lead Accounting Associate".
      /// </summary>
      /// <returns></returns>
      public static string Title() {
         var titleTemplate = _job.Title.Random();
         var seniority = _job.Seniority.Random();
         var field = _job.Field.Random();
         var position = _job.Position.Random();

         var result = titleTemplate.Replace("#{seniority}", seniority);
         result = result.Replace("#{field}", field);
         result = result.Replace("#{position}", position);

         return result;
      }

      /// <summary>
      /// Generates a field of work, e.g. "Manufacturing".
      /// </summary>
      /// <returns></returns>
      public static string Field() {
         return _job.Field.Random();
      }

      /// <summary>
      /// Generates a seniority, e.g. "Lead".
      /// </summary>
      /// <returns></returns>
      public static string Seniority() {
         return _job.Seniority.Random();
      }

      /// <summary>
      /// Generates a position, e.g. "Supervisor".
      /// </summary>
      /// <returns></returns>
      public static string Position() {
         return _job.Position.Random();
      }

      /// <summary>
      /// Generates a key skill, e.g. "Teamwork".
      /// </summary>
      /// <returns></returns>
      public static string KeySkill() {
         return _job.KeySkills.Random();
      }

      /// <summary>
      /// Generates an employment type, e.g. "Full-time".
      /// </summary>
      /// <returns></returns>
      public static string EmploymentType() {
         return _job.EmploymentType.Random();
      }

      /// <summary>
      /// Generates an education level, e.g. "Bachelor".
      /// </summary>
      /// <returns></returns>
      public static string EducationLevel() {
         return _job.EducationLevel.Random();
      }

      private static job _job;

#pragma warning disable IDE1006 // Naming Styles
      // Helper classes for reading the yaml file. Note that the class names are
      // intentionally lower case.

      internal class locale {
         public en en { get; set; }
      }

      internal class en {
         public faker faker { get; set; }
      }

      internal class faker {
         public job job { get; set; }
      }

      internal class job {
         [YamlMember(Alias = "field", ApplyNamingConventions = false)]
         public string[] Field { get; set; }

         [YamlMember(Alias = "seniority", ApplyNamingConventions = false)]
         public string[] Seniority { get; set; }

         [YamlMember(Alias = "position", ApplyNamingConventions = false)]
         public string[] Position { get; set; }

         [YamlMember(Alias = "key_skills", ApplyNamingConventions = false)]
         public string[] KeySkills { get; set; }

         [YamlMember(Alias = "employment_type", ApplyNamingConventions = false)]
         public string[] EmploymentType { get; set; }

         [YamlMember(Alias = "education_level", ApplyNamingConventions = false)]
         public string[] EducationLevel { get; set; }

         public List<string> Title { get; set; }
      }
#pragma warning restore IDE1006 // Naming Styles
   }
}
