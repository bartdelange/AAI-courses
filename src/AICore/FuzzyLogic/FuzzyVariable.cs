#region

using System;
using System.Collections.Generic;
using AICore.FuzzyLogic.FuzzySets;
using AICore.Util;

#endregion

namespace AICore.FuzzyLogic
{
    public class FuzzyVariable
    {
        private double _maxRange;
        private readonly Dictionary<string, FuzzySet> _members = new Dictionary<string, FuzzySet>();
        private double _minRange;

        public FuzzyVariable()
        {
            _maxRange = 0.0;
        }

        private void AdjustRangeToFit(double min, double max)
        {
            if (min < _minRange)
                _minRange = min;

            if (max > _maxRange)
                _maxRange = max;
        }

        public FzSet AddLeftShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_members.GetOrCreate(name, new LeftShoulder(minBound, peak, maxBound)));
        }

        public FzSet AddRightShoulderSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_members.GetOrCreate(name, new RightShoulder(minBound, peak, maxBound)));
        }

        public FzSet AddTriangularSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_members.GetOrCreate(name, new Triangle(minBound, peak, maxBound)));
        }

        public FzSet AddSingletonSet(string name, double minBound, double peak, double maxBound)
        {
            AdjustRangeToFit(minBound, maxBound);

            return new FzSet(_members.GetOrCreate(name, new Singleton(minBound, peak, maxBound)));
        }

        public void Fuzzify(double val)
        {
            //make sure the value is within the bounds of this variable
            if (!(val >= _minRange && val <= _maxRange))
                throw new ArgumentOutOfRangeException(nameof(val), "Value out of range");

            //for each set in the flv calculate the DOM for the given value
            foreach (var member in _members) member.Value.SetDOM(member.Value.CalculateDOM(val));
        }

        public double DeFuzzifyMaxAv()
        {
            var bottom = 0.0;
            var top = 0.0;

            foreach (var member in _members)
            {
                bottom += member.Value.GetDOM();
                top += member.Value.RepresentativeValue * member.Value.GetDOM();
            }

            //make sure bottom is not equal to zero
            if (Math.Abs(bottom) < 0.000000001) return 0.0;

            return top / bottom;
        }

        public double DeFuzzifyCentroid(int numSamples)
        {
            //calculate the step size
            var stepSize = (_maxRange - _minRange) / numSamples;
            var totalArea = 0.0;
            var sumOfMoments = 0.0;

            for (var samp = 1; samp <= numSamples; ++samp)
                foreach (var member in _members)
                {
                    var contribution = Math.Min(member.Value.CalculateDOM(_minRange + samp * stepSize),
                        member.Value.GetDOM());

                    totalArea += contribution;
                    sumOfMoments += (_minRange + samp * stepSize) * contribution;
                }

            //make sure total area is not equal to zero
            if (Math.Abs(totalArea) < 0.000000001) return 0.0;

            return sumOfMoments / totalArea;
        }
    }
}