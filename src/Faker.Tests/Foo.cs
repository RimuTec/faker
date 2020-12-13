using System.Collections;
using NUnit.Framework;

namespace RimuTec.Faker.Tests
{
   [TestFixture]
   [TestFixtureSource(typeof(FooFixtureData), nameof(FooFixtureData.Blahs))]
   public class Foo
   {
      public Foo(int someValue)
      {
         SomeValue = someValue;
      }

      [SetUp]
      public void SetUp()
      {
         // do something with this.SomeValue
      }

      [Test]
      public void Test1()
      {
         Assert.True(true);
      }

      private int SomeValue { get; }
   }

   public static class FooFixtureData
   {
      public static IEnumerable Blahs
      {
         get
         {
            yield return new TestFixtureData((int) 1);
            yield return new TestFixtureData((int) 2);
         }
      }
   }
}
