using RimuTec.Faker.Extensions;
using System;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for names for humans.
   /// </summary>
   public class Name : GeneratorBase<Name>
   {
      private Name() { }

      /// <summary>
      /// Generates a random name with first name, last name and potentially a prefix and/or a suffix. The name will not 
      /// include a middle name. Example: "Tyshawn Johns Sr.". Note: This is the equivalent to Ruby's 'Faker::Name.name'.
      /// </summary>
      /// <returns></returns>
      public static string FullName()
      {
         return Parse(Fetch("name.name"));
      }

      /// <summary>
      /// Generates a random name including first name, middle name and potentially a prefix and/or suffix. Example:
      /// "Aditya Elton Douglas".
      /// </summary>
      /// <returns></returns>
      public static string NameWithMiddle()
      {
         return Parse(Fetch("name.name_with_middle"));
      }

      /// <summary>
      /// Generates a random first name. Example: "Kaci"
      /// </summary>
      /// <returns>A string containing a first name</returns>
      public static string FirstName()
      {
         return Parse(Fetch("name.first_name"));
      }

      /// <summary>
      /// Generates a random middle name. Example: "Abraham"
      /// </summary>
      /// <returns></returns>
      public static string MiddleName()
      {
         return Fetch("name.middle_name");
      }

      /// <summary>
      /// Generates a random last name. Example: "Ernser"
      /// </summary>
      /// <returns>A string containing a last name</returns>
      public static string LastName()
      {
         return Parse(Fetch("name.last_name"));
      }

      /// <summary>
      /// Generates a random prefix. Example: "Mr."
      /// </summary>
      /// <returns></returns>
      public static string Prefix()
      {
         return Fetch("name.prefix");
      }

      /// <summary>
      /// Generates a random suffix. Example: "IV"
      /// </summary>
      /// <returns></returns>
      public static string Suffix()
      {
         return Fetch("name.suffix");
      }

      /// <summary>
      /// Generates name initials. Example: NJM.
      /// </summary>
      /// <param name="characterCount">Number of characters in initials.</param>
      /// <returns></returns>
      public static string Initials(int characterCount = 3)
      {
         if (!(characterCount > 0))
         {
            throw new ArgumentOutOfRangeException(nameof(characterCount), "Must be greater than 0.");
         }
         return string.Concat(characterCount.Times(x => _alphabetUpper.Sample()));
      }

      private static readonly string[] _alphabet = "a b c d e f g h i j k l m n o p q r s t u v w x y z".Split(' ');
      private static readonly string[] _alphabetUpper = "a b c d e f g h i j k l m n o p q r s t u v w x y z".ToUpper().Split(' ');
   }
}
