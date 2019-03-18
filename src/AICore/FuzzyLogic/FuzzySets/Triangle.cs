#region

using System;

#endregion

namespace AICore.FuzzyLogic.FuzzySets
{
    public class Triangle : FuzzySet
    {
        private readonly double _leftOffset;
        private readonly double _peakPoint;
        private readonly double _rightOffset;

        public Triangle(double leftOffset, double peak, double rightOffset) : base(peak)
        {
            _peakPoint = peak;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
        }
        
        public override double CalculateDOM(double val)
        {
            double grad;

            // Test for the case where the triangle's left or right offsets are zero
            // (to prevent divide by zero errors below)
            if ((Math.Abs(_rightOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001)
                || (Math.Abs(_leftOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001))
                return 1.0;

            // Find DOM if left of center
            if (val <= _peakPoint && val >= _peakPoint - _leftOffset)
            {
                grad = 1.0 / _leftOffset;
                return grad * (val - (_peakPoint - _leftOffset));
            }

            // Find DOM if right of center
            if (val > _peakPoint && val < _peakPoint + _rightOffset)
            {
                grad = 1.0 / -_rightOffset;
                return grad * (val - _peakPoint) + 1.0;
            }

            // Out of range of this FLV, return zero
            return 0.0;
        }
    }
}