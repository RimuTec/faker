using RimuTec.Faker.Extensions;

namespace RimuTec.Faker
{
   /// <summary>
   /// Generators for phone numbers
   /// </summary>
   public class PhoneNumber : GeneratorBase<PhoneNumber>
   {
      private PhoneNumber() { }

      /// <summary>
      /// Generates a land line number in one of several formats.
      /// </summary>
      /// <returns></returns>
      /// <remarks>This method is the equivalent to Ruby's Faker::PhoneNumber.phone_number.</remarks>
      public static string LandLine()
      {
         return Fetch("phone_number.formats").Numerify();
      }

      /// <summary>
      /// Generates a cell phone number in one of several formats.
      /// </summary>
      /// <returns></returns>
      public static string CellPhone()
      {
         return Fetch("cell_phone.formats").Numerify();
      }
   }
}
