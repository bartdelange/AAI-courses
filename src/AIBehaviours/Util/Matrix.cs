using System;

namespace AIBehaviours.Util
{
    public class Matrix
    {
        private class MatrixStructure
        {
            public double P11 = 1;
            public double P12;
            public double P13;
            public double P21;
            public double P22 = 1;
            public double P23;
            public double P31;
            public double P32;
            public double P33 = 1;

            public override string ToString()
            {
                return $"[{P11},{P12},{P13}]\n[{P21},{P22},{P23}]\n[{P31},{P32},{P33}]";
            }
        }

        private MatrixStructure _matrix = new MatrixStructure();

        private void MatrixMultiply(MatrixStructure matrix)
        {
            var tempMatrix = new MatrixStructure
            {
                // First row	
                P11 = (_matrix.P11 * matrix.P11) + (_matrix.P12 * matrix.P21) + (_matrix.P13 * matrix.P31),
                P12 = (_matrix.P11 * matrix.P12) + (_matrix.P12 * matrix.P22) + (_matrix.P13 * matrix.P32),
                P13 = (_matrix.P11 * matrix.P13) + (_matrix.P12 * matrix.P23) + (_matrix.P13 * matrix.P33),

                // Second row	
                P21 = (_matrix.P21 * matrix.P11) + (_matrix.P22 * matrix.P21) + (_matrix.P23 * matrix.P31),
                P22 = (_matrix.P21 * matrix.P12) + (_matrix.P22 * matrix.P22) + (_matrix.P23 * matrix.P32),
                P23 = (_matrix.P21 * matrix.P13) + (_matrix.P22 * matrix.P23) + (_matrix.P23 * matrix.P33),

                // Third row	
                P31 = (_matrix.P31 * matrix.P11) + (_matrix.P32 * matrix.P21) + (_matrix.P33 * matrix.P31),
                P32 = (_matrix.P31 * matrix.P12) + (_matrix.P32 * matrix.P22) + (_matrix.P33 * matrix.P32),
                P33 = (_matrix.P31 * matrix.P13) + (_matrix.P32 * matrix.P23) + (_matrix.P33 * matrix.P33),
            };

            _matrix = tempMatrix;
        }

        /// <summary>	
        /// Rotates matrix by using the given vectors	
        /// </summary>	
        public void Rotate(Vector2D heading, Vector2D side)
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
                P33 = 1,
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

            var matrix = new MatrixStructure
            {
                P11 = cos,
                P12 = sin,
                P13 = 0,
                P21 = -sin,
                P22 = cos,
                P23 = 0,
                P31 = 0,
                P32 = 0,
                P33 = 1,
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
                P33 = 1,
            };

            // Console.WriteLine("Translate\n" + matrix);	

            MatrixMultiply(matrix);
        }

        public Vector2D TransformVector2Ds(Vector2D localVector)
        {
            var x = (_matrix.P11 * localVector.X) + (_matrix.P21 * localVector.Y) + (_matrix.P31);
            var y = (_matrix.P12 * localVector.X) + (_matrix.P22 * localVector.Y) + (_matrix.P32);

            return new Vector2D(x, y);
        }

        public override string ToString()
        {
            return _matrix.ToString();
        }
    }
}