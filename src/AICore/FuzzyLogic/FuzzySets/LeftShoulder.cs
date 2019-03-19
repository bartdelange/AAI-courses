#region

using System;

#endregion

namespace AICore.FuzzyLogic.FuzzySets
{
    public class LeftShoulder : FuzzySet
    {
        private readonly double _leftOffset;
        private readonly double _peakPoint;
        private readonly double _rightOffset;

        public LeftShoulder(double leftOffset, double peak, double rightOffset) : base(
            (peak + leftOffset + peak) / 2.0d)
        {
            _peakPoint = peak;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if (Math.Abs(_rightOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001
                || Math.Abs(_leftOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001)
                return 1.0;

            if (val >= _peakPoint && val < _peakPoint + _rightOffset)
            {
                var grad = 1.0d / -_rightOffset;
                return grad * (val - _peakPoint) + 1.0;
            }

            //find DOM if left of center
            if (val < _peakPoint && val >= _peakPoint - _leftOffset) return 1.0;

            //out of range of this FLV, return zero
            return 0.0;
        }
    }
}