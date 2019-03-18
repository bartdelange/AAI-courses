namespace AICore.FuzzyLogic.FuzzySets
{
    public abstract class FuzzySet
    {
        protected FuzzySet(double RepVal)
        {
            DOM = 0.0d;
            RepresentativeValue = RepVal;
        }

        private double DOM { get; set; }
        public double RepresentativeValue { get; }

        public double GetDOM()
        {
            return DOM;
        }

        public void SetDOM(double val)
        {
            DOM = val;
        }

        public void ClearDOM()
        {
            DOM = 0.0d;
        }

        public void ORWithDOM(double value)
        {
            if (value > DOM)
                DOM = value;
        }

        public abstract double CalculateDOM(double val);
    }
}