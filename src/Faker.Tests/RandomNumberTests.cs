using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RimuTec.Faker.Tests
{
   public class RandomNumberTests
   {
      [Test]
      public void Next_With_MultipleThreads()
      {
         RunMultithreaded(RandomNumber.Next);
      }

      [Test]
      public void Next_MaxValue_WithMultipleThreads()
      {
         RunMultithreaded(() => RandomNumber.Next(42));
      }

      [Test]
      public void Next_MinValue_MaxValue_WithMultipleThreads()
      {
         RunMultithreaded(() => RandomNumber.Next(-34, 42));
      }

      [Test]
      public void NextDouble_Multithreaded()
      {
         RunMultithreaded(() => RandomNumber.NextDouble());
      }

      [Test]
      public void NextDouble_MaxValue_Multithreaded()
      {
         // Although it's an internal
         double d = 42.0;
         RunMultithreaded(() => RandomNumber.Next(d));
      }

      private static void RunMultithreaded<T>(Func<T> func)
      {
         var threads = new List<Task>()
         {
            Task.Run(func),
            Task.Run(func)
         }
         .ToArray();
         // We need to wait for all tasks to finish as otherwise the testing framework will not
         // report errors on other threads when execute without debugger and breakpoints:
         Task.WaitAll(threads);
      }
   }
}
