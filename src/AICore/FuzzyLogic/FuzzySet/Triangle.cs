using System;

namespace AICore.FuzzyLogic.FuzzySet
{
    public class Triangle : FuzzySet
    {
        private double _peakPoint;
        private double _leftOffset;
        private double _rightOffset;
        
        public Triangle(double mid, double left, double right) : base(mid)
        {
            _peakPoint = mid;
            _leftOffset = left;
            _rightOffset = right;
        }

        public override double CalculateDOM(double val)
        {
            double grad;

            // Test for the case where the triangle's left or right offsets are zero
            // (to prevent divide by zero errors below)
            if (Math.Abs(_rightOffset) < 0.000001 && Math.Abs(_peakPoint - val) < 0.000001 
                || Math.Abs(_leftOffset) < 0.000001 && Math.Abs(_peakPoint - val) < 0.000001)
            {
                return 1.0;
            }
            
            // Find DOM if left of center
            if ( val <= _peakPoint && val >= _peakPoint - _leftOffset )
            {
                grad = 1.0 / _leftOffset;
                return grad * (val - (_peakPoint - _leftOffset));
            }
            
            // Find DOM if right of center else
            if (!(val > _peakPoint) || !(val < _peakPoint + _rightOffset))
            {
                grad = 1.0 / -_rightOffset;
                return grad * (val - _peakPoint) + 1.0;
            }
            
            // Out of range of this FLV, return zero
            return 0.0;
        }
    }
}