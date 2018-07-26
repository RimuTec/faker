namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for education related data, e.g. university, secondary school, course and campus.
   /// </summary>
   public class Educator : GeneratorBase
   {
      private Educator() { }

      /// <summary>
      /// Generates a campus name. Example: "Vertapple Campus"
      /// </summary>
      /// <returns></returns>
      public static string Campus()
      {
         return $"{Parse("educator.name")} Campus";
      }

      /// <summary>
      /// Generates the name for a course. Example: "Associate Degree in Criminology"
      /// </summary>
      /// <returns></returns>
      public static string Course()
      {
         return $"{Fetch("educator.tertiary.course.type")} {Fetch("educator.tertiary.course.subject")}";
      }

      /// <summary>
      /// Generates a name for a secondary school. Example: "Iceborough Secondary College"
      /// </summary>
      /// <returns></returns>
      public static string SecondarySchool()
      {
         return $"{Parse("educator.name")} {Fetch("educator.secondary")}";
      }

      /// <summary>
      /// Generates a name for a tertiary education provider. Example: "Mallowtown Technical College"
      /// </summary>
      /// <returns></returns>
      public static string University()
      {
         return $"{Parse("educator.name")} {Fetch("educator.tertiary.type")}";
      }
   }
}
