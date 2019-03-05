using System.Numerics;
using AICore.Util;
using Xunit;

namespace AAITests.Util
{
    public class Matrix3Tests
    {

        [Fact]
        public void Should_CheckEquality()
        {
            var m1 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var m2 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            Assert.True(m1.Equals(m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
        }

        [Fact]
        public void Should_AddMatrixes()
        {
            var m1 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var m2 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var expected = new Matrix3(
                2, 2, 2,
                2, 2, 2,
                2, 2, 2
            );

            Assert.Equal(expected, m1 + m2);
        }

        [Fact]
        public void Should_SubtractMatrixes()
        {
            var m1 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var m2 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var expected = new Matrix3(
                0, 0, 0,
                0, 0, 0,
                0, 0, 0
            );

            Assert.Equal(expected, m1 - m2);
        }

        [Fact]
        public void Should_MultiplyMatrixWithScalar()
        {
            var m1 = new Matrix3(
                1, 1, 1,
                1, 1, 1,
                1, 1, 1
            );

            var scalar = 2;

            var expected = new Matrix3(
                2, 2, 2,
                2, 2, 2,
                2, 2, 2
            );

            Assert.Equal(expected, m1 * scalar);
        }

        [Fact]
        public void Should_MultiplyMatrixes()
        {
            var m1 = new Matrix3(
                1, 2, 3,
                2, 2, 3,
                3, 3, 3
            );

            var m2 = new Matrix3(
                2, 2, 2,
                2, 2, 2,
                2, 2, 2
            );

            var expected = new Matrix3(
                12, 12, 12,
                14, 14, 14,
                18, 18, 18
            );

            Assert.Equal(expected, m1 * m2);
        }

        [Fact]
        public void Should_MultiplyMatrixWithVector()
        {
            var m1 = new Matrix3(
                1, 0, 1,
                0, 1, 2,
                0, 0, 1
            );

            var expected = new Vector2(1, 2);

            Assert.Equal(expected, m1 * new Vector2());
        }

        [Fact]
        public void Should_AddTranslationToMatrix()
        {
            var m1 = new Matrix3().Translate(new Vector2(5, 5));

            var expected = new Matrix3(
                1, 0, 5,
                0, 1, 5,
                0, 0, 1
            );

            Assert.Equal(expected, m1);
        }

        [Fact]
        public void Should_Rotate()
        {
            var m1 = new Matrix3().Rotate(45);

            var expected = new Matrix3(
                0.707106769F, 0.707106769F, 0,
                -0.707106769F, 0.707106769F, 0,
                0, 0, 1
            );

            Assert.Equal(expected, m1);
        }
    }
}
