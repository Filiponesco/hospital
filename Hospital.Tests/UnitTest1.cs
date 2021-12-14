using System;
using hospital.Helpers;
using Xunit;

namespace Hospital.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void RandomString_ForLength7_ReturnsRandomStringWithLength7()
        {
            // arange
            int length = 7;

            // act
            string result = RandomStringGenerator.RandomString(length);

            // assert
            Assert.Equal(length, result.Length);
        }
    }
}
