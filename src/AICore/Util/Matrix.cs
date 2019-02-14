using System;

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
                _P11 = _matrix._P11 * matrix._P11 + _matrix._P12 * matrix._P21 + _matrix._P13 * matrix._P31,
                _P12 = _matrix._P11 * matrix._P12 + _matrix._P12 * matrix._P22 + _matrix._P13 * matrix._P32,
                _P13 = _matrix._P11 * matrix._P13 + _matrix._P12 * matrix._P23 + _matrix._P13 * matrix._P33,

                // Second row	
                _P21 = _matrix._P21 * matrix._P11 + _matrix._P22 * matrix._P21 + _matrix._P23 * matrix._P31,
                _P22 = _matrix._P21 * matrix._P12 + _matrix._P22 * matrix._P22 + _matrix._P23 * matrix._P32,
                _P23 = _matrix._P21 * matrix._P13 + _matrix._P22 * matrix._P23 + _matrix._P23 * matrix._P33,

                // Third row	
                _P31 = _matrix._P31 * matrix._P11 + _matrix._P32 * matrix._P21 + _matrix._P33 * matrix._P31,
                _P32 = _matrix._P31 * matrix._P12 + _matrix._P32 * matrix._P22 + _matrix._P33 * matrix._P32,
                _P33 = _matrix._P31 * matrix._P13 + _matrix._P32 * matrix._P23 + _matrix._P33 * matrix._P33
            };

            _matrix = tempMatrix;
        }

        /// <summary>
        ///     Rotates matrix by using the given vectors
        /// </summary>
        public void Rotate(Vector2D heading, Vector2D side)
        {
            var matrix = new MatrixStructure
            {
                _P11 = heading._X,
                _P12 = heading._Y,
                _P13 = 0,
                _P21 = side._X,
                _P22 = side._Y,
                _P23 = 0,
                _P31 = 0,
                _P32 = 0,
                _P33 = 1
            };

            // Console.WriteLine("Rotate\n" + matrix);	

            MatrixMultiply(matrix);
        }

        /// <summary>
        ///     Rotates matrix by given angle
        /// </summary>
        /// <param name="angle">
        /// </param>
        public void Rotate(double angle)
        {
            var sin = Math.Sin(angle);
            var cos = Math.Cos(angle);

            var matrix = new MatrixStructure
            {
                _P11 = cos,
                _P12 = sin,
                _P13 = 0,
                _P21 = -sin,
                _P22 = cos,
                _P23 = 0,
                _P31 = 0,
                _P32 = 0,
                _P33 = 1
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
        public void Translate(double x, double y)
        {
            var matrix = new MatrixStructure
            {
                _P11 = 1,
                _P12 = 0,
                _P13 = 0,
                _P21 = 0,
                _P22 = 1,
                _P23 = 0,
                _P31 = x,
                _P32 = y,
                _P33 = 1
            };

            // Console.WriteLine("Translate\n" + matrix);	

            MatrixMultiply(matrix);
        }

        public Vector2D TransformVector2Ds(Vector2D localVector)
        {
            var x = _matrix._P11 * localVector._X + _matrix._P21 * localVector._Y + _matrix._P31;
            var y = _matrix._P12 * localVector._X + _matrix._P22 * localVector._Y + _matrix._P32;

            return new Vector2D(x, y);
        }

        public override string ToString()
        {
            return _matrix.ToString();
        }

        private class MatrixStructure
        {
            public double _P11 = 1;
            public double _P12;
            public double _P13;
            public double _P21;
            public double _P22 = 1;
            public double _P23;
            public double _P31;
            public double _P32;
            public double _P33 = 1;

            public override string ToString()
            {
                return $"[{_P11},{_P12},{_P13}]\n[{_P21},{_P22},{_P23}]\n[{_P31},{_P32},{_P33}]";
            }
        }
    }
}