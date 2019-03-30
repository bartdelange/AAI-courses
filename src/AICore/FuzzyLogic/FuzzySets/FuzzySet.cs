namespace AICore.FuzzyLogic.FuzzySets
{
    public abstract class FuzzySet
    {
        protected FuzzySet(double repVal)
        {
            Dom = 0.0d;
            RepresentativeValue = repVal;
        }

        private double Dom { get; set; }
        public double RepresentativeValue { get; }

        public double GetDom()
        {
            return Dom;
        }

        public void SetDom(double val)
        {
            Dom = val;
        }

        public void ClearDom()
        {
            Dom = 0.0d;
        }

        public void OrWithDom(double value)
        {
            if (value > Dom)
                Dom = value;
        }

        public abstract double CalculateDom(double val);
    }
}