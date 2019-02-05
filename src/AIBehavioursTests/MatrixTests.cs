using AIBehaviours.util;
using Xunit;

namespace AIBehavioursTests
{
    public class MatrixTests
    {
        [Fact]
        public void ShouldCreateMatrixWithIdentity()
        {
            MatrixTransformations matrix = new MatrixTransformations();
            string expected = "[1,0]\n[0,1]\n[0,0]";

            Assert.Equal(expected, matrix.ToString());
        }

        [Fact]
        public void ShouldTranslateMatrix()
        {
            MatrixTransformations matrix = new MatrixTransformations();
            string expected = "[1,0]\n[0,1]\n[0,0]";

            matrix.Translate(2, 2);

            Assert.Equal(expected, matrix.ToString());
        }
    }
}
