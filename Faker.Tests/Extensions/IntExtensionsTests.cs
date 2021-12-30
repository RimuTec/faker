using NUnit.Framework;
using RimuTec.Faker.Extensions;
using System;
using System.Linq;

namespace RimuTec.Faker.Tests.Extensions
{
    [TestFixture]
    public class IntExtensionsTests
    {
        [Test]
        public void Times_With_TypesMissing()
        {
            const int paramCount = 3;
            var parameters = new object[paramCount];
            for (var i = 0; i < paramCount; i++)
            {
                parameters[i] = new object();
            }
            paramCount.TimesDo(index => parameters[index] = Type.Missing);
            Assert.AreEqual(paramCount, parameters.Length);
            for (var i = 0; i < paramCount; i++)
            {
                Assert.AreEqual(Type.Missing, parameters[i]);
            }
        }

        [Test]
        public void Times()
        {
            const int paramCount = 3;
            var parameters = new object[paramCount];
            paramCount.TimesDo(index => parameters[index] = Type.Missing);
            var usingEnum = Enumerable.Repeat(Type.Missing, paramCount).ToArray();

            Assert.AreEqual(paramCount, parameters.Length);
            for (var i = 0; i < paramCount; i++)
            {
                Assert.AreEqual(parameters[i], usingEnum[i]);
            }
        }
    }
}
