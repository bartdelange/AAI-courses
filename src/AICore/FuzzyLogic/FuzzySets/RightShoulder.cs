#region

using System;

#endregion

namespace AICore.FuzzyLogic.FuzzySets
{
    public class RightShoulder : FuzzySet
    {
        private readonly double _leftOffset;
        private readonly double _peakPoint;
        private readonly double _rightOffset;

        public RightShoulder(double leftOffset, double peak, double rightOffset) : base(
            (peak + rightOffset + peak) / 2.0d)
        {
            _peakPoint = peak;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
        }

        public override double CalculateDOM(double val)
        {
            //test for the case where the left or right offsets are zero
            //(to prevent divide by zero errors below)
            if (Math.Abs(_rightOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001 ||
                Math.Abs(_leftOffset) < 0.000000001 && Math.Abs(_peakPoint - val) < 0.000000001)
                return 1.0;

            //find DOM if left of center
            if (val <= _peakPoint && val > _peakPoint - _leftOffset)
            {
                var grad = 1.0d / _leftOffset;
                return grad * (val - (_peakPoint - _leftOffset));
            }

            //find DOM if right of center and less than center + right offset
            if (val > _peakPoint && val <= _peakPoint + _rightOffset) return 1.0;
            return 0;
        }
    }
}