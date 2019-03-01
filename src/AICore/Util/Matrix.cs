using System;
using System.Numerics;

namespace AICore.Util
{
    public class Matrix
    {
        private MatrixStructure _matrix = new MatrixStructure();

        private void MatrixMultiply(MatrixStructure matrix)
        {
            var tempMatrix = new MatrixStructure
            {
                // First row	
                P11 = _matrix.P11 * matrix.P11 + _matrix.P12 * matrix.P21 + _matrix.P13 * matrix.P31,
                P12 = _matrix.P11 * matrix.P12 + _matrix.P12 * matrix.P22 + _matrix.P13 * matrix.P32,
                P13 = _matrix.P11 * matrix.P13 + _matrix.P12 * matrix.P23 + _matrix.P13 * matrix.P33,

                // Second row	
                P21 = _matrix.P21 * matrix.P11 + _matrix.P22 * matrix.P21 + _matrix.P23 * matrix.P31,
                P22 = _matrix.P21 * matrix.P12 + _matrix.P22 * matrix.P22 + _matrix.P23 * matrix.P32,
                P23 = _matrix.P21 * matrix.P13 + _matrix.P22 * matrix.P23 + _matrix.P23 * matrix.P33,

                // Third row	
                P31 = _matrix.P31 * matrix.P11 + _matrix.P32 * matrix.P21 + _matrix.P33 * matrix.P31,
                P32 = _matrix.P31 * matrix.P12 + _matrix.P32 * matrix.P22 + _matrix.P33 * matrix.P32,
                P33 = _matrix.P31 * matrix.P13 + _matrix.P32 * matrix.P23 + _matrix.P33 * matrix.P33
            };

            _matrix = tempMatrix;
        }

        /// <summary>
        ///     Rotates matrix by using the given vectors
        /// </summary>
        public void Rotate(Vector2 heading, Vector2 side)
        {
            var matrix = new MatrixStructure
            {
                P11 = heading.X,
                P12 = heading.Y,
                P13 = 0,
                P21 = side.X,
                P22 = side.Y,
                P23 = 0,
                P31 = 0,
                P32 = 0,
                P33 = 1
            };

            // Console.WriteLine("Rotate\n" + matrix);	

            MatrixMultiply(matrix);
        }

        /// <summary>
        ///     Rotates matrix by given angle
        /// </summary>
        /// <param name="angle">
        /// </param>
        public void Rotate(float angle)
        {
            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);

            var matrix = new MatrixStructure
            {
                P11 = (float) cos,
                P12 = (float) sin,
                P13 = 0,
                P21 = (float) -sin,
                P22 = (float) cos,
                P23 = 0,
                P31 = 0,
                P32 = 0,
                P33 = 1
            };

            MatrixMultiply(matrix);
        }

        /// <summary>
        ///     Translate the matrix origin
        /// </summary>
        /// <param name="x">
        /// </param>
        /// <param name="y">
        /// </param>
        public void Translate(float x, float y)
        {
            var matrix = new MatrixStructure
            {
                P11 = 1,
                P12 = 0,
                P13 = 0,
                P21 = 0,
                P22 = 1,
                P23 = 0,
                P31 = x,
                P32 = y,
                P33 = 1
            };

            // Console.WriteLine("Translate\n" + matrix);	

            MatrixMultiply(matrix);
        }

        public Vector2 TransformVector2s(Vector2 localVector)
        {
            var x = _matrix.P11 * localVector.X + _matrix.P21 * localVector.Y + _matrix.P31;
            var y = _matrix.P12 * localVector.X + _matrix.P22 * localVector.Y + _matrix.P32;

            return new Vector2(x, y);
        }

        public override string ToString()
        {
            return _matrix.ToString();
        }

        private class MatrixStructure
        {
            public float P11 = 1;
            public float P12;
            public float P13;
            public float P21;
            public float P22 = 1;
            public float P23;
            public float P31;
            public float P32;
            public float P33 = 1;

            public override string ToString()
            {
                return $"[{P11},{P12},{P13}]\n[{P21},{P22},{P23}]\n[{P31},{P32},{P33}]";
            }
        }
    }
}