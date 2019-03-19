#region

using System;

#endregion

namespace AICore.FuzzyLogic.FuzzySets
{
    public class Singleton : FuzzySet
    {
        private readonly double _leftOffset;
        private readonly double _peakPoint;
        private readonly double _rightOffset;

        public Singleton(double leftOffset, double peak, double rightOffset) : base((peak + leftOffset + peak) / 2.0d)
        {
            _peakPoint = peak;
            _leftOffset = leftOffset;
            _rightOffset = rightOffset;
        }

        public override double CalculateDOM(double val)
        {
            if (Math.Abs(_rightOffset) < 0.000000001 && Math.Abs(val - _peakPoint) < 0.000000001) return 1.0;

            if (val >= _peakPoint - _leftOffset && val <= _peakPoint - _rightOffset) return 1.0;

            return 0;
        }
    }
}