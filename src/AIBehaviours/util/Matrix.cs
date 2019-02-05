using System;

namespace AIBehaviours.util
{
    public class MatrixTransformations
    {
        private class Matrix
        {
            public double _11 = 1;
            public double _12;
            public double _13;
            public double _21;
            public double _22 = 1;
            public double _23;
            public double _31;
            public double _32;
            public double _33 = 1;

            public override string ToString()
            {
                return $"[{_11},{_12},{_13}]\n[{_21},{_22},{_23}]\n[{_31},{_32},{_33}]";
            }
        }

        private Matrix _matrix = new Matrix();

        private void MatrixMultiply(Matrix matrix)
        {
            var tempMatrix = new Matrix
            {
                // First row
                _11 = (_matrix._11 * matrix._11) + (_matrix._12 * matrix._21) + (_matrix._13 * matrix._31),
                _12 = (_matrix._11 * matrix._12) + (_matrix._12 * matrix._22) + (_matrix._13 * matrix._32),
                _13 = (_matrix._11 * matrix._13) + (_matrix._12 * matrix._23) + (_matrix._13 * matrix._33),

                // Second row
                _21 = (_matrix._21 * matrix._11) + (_matrix._22 * matrix._21) + (_matrix._23 * matrix._31),
                _22 = (_matrix._21 * matrix._12) + (_matrix._22 * matrix._22) + (_matrix._23 * matrix._32),
                _23 = (_matrix._21 * matrix._13) + (_matrix._22 * matrix._23) + (_matrix._23 * matrix._33),

                // Third row
                _31 = (_matrix._31 * matrix._11) + (_matrix._32 * matrix._21) + (_matrix._33 * matrix._31),
                _32 = (_matrix._31 * matrix._12) + (_matrix._32 * matrix._22) + (_matrix._33 * matrix._32),
                _33 = (_matrix._31 * matrix._13) + (_matrix._32 * matrix._23) + (_matrix._33 * matrix._33),
            };

            _matrix = tempMatrix;
        }

        /// <summary>
        /// Rotates matrix by using the given vectors
        /// </summary>
        public void Rotate(Vector2D heading, Vector2D side)
        {
            var matrix = new Matrix
            {
                _11 = heading.X,
                _12 = heading.Y,
                _13 = 0,
                _21 = side.X,
                _22 = side.Y,
                _23 = 0,
                _31 = 0,
                _32 = 0,
                _33 = 1,
            };

            // Console.WriteLine("Rotate\n" + matrix);

            MatrixMultiply(matrix);
        }

        /// <summary>
        /// Rotates matrix by given angle
        /// </summary>
        /// <param name="angle">
        /// </param>
        public void Rotate(double angle)
        {
            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);

            var matrix = new Matrix
            {
                _11 = cos,
                _12 = sin,
                _13 = 0,
                _21 = -sin,
                _22 = cos,
                _23 = 0,
                _31 = 0,
                _32 = 0,
                _33 = 1,
            };

            MatrixMultiply(matrix);
        }

        /// <summary>
        /// Translate the matrix origin
        /// </summary>
        /// <param name="x">
        /// </param>
        /// <param name="y">
        /// </param>
        public void Translate(double x, double y)
        {
            var matrix = new Matrix
            {
                _11 = 1,
                _12 = 0,
                _13 = 0,
                _21 = 0,
                _22 = 1,
                _23 = 0,
                _31 = x,
                _32 = y,
                _33 = 1,
            };

            // Console.WriteLine("Translate\n" + matrix);

            MatrixMultiply(matrix);
        }

        public Vector2D TransformVector2Ds(Vector2D localVector)
        {
            var x = (_matrix._11 * localVector.X) + (_matrix._21 * localVector.Y) + (_matrix._31);
            var y = (_matrix._12 * localVector.X) + (_matrix._22 * localVector.Y) + (_matrix._32);

            return new Vector2D(x, y);
        }

        public override string ToString()
        {
            return _matrix.ToString();
        }
    }
}