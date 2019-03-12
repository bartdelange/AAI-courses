using System;

namespace AICore.FuzzyLogic.FuzzySet
{
    public abstract class FuzzySet
    {
        protected double DOM { get; set; }
        protected double RepresentativeValue { get; private set; }

        public FuzzySet(double RepVal)
        {
            DOM = 0.0d;
            RepresentativeValue = RepVal;
        }

        public abstract double CalculateDOM(double val);

        public void ClearDOM()
        {
            DOM = 0.0d;
        }
    }
}