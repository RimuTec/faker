using RimuTec.Faker.Extensions;
using RimuTec.Faker.Helper;
using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace RimuTec.Faker {
   public static class Job {
      // Resources used by this class from https://github.com/stympy/faker/blob/master/lib/locales/en/job.yml
      // [Manfred, 03jun2018]

      static Job() {
         const string yamlFileName = "RimuTec.Faker.locales.en.job.yml";
         locale locale = YamlLoader.Read<locale>(yamlFileName);
         _job = locale.en.faker.job;
      }

      public static string KeySkill() {
         return _job.KeySkills.Random().Trim();
      }

      public static string Title() {
         var titleTemplate = _job.Title.Random().Trim();
         var seniority = _job.Seniority.Random().Trim();
         var field = _job.Field.Random().Trim();
         var position = _job.Position.Random().Trim();

         var result = titleTemplate.Replace("#{seniority}", seniority);
         result = result.Replace("#{field}", field);
         result = result.Replace("#{position}", position);

         return result;
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
