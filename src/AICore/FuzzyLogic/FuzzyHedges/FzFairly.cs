using AICore.FuzzyLogic.FuzzySets;

namespace AICore.FuzzyLogic.FuzzyHedges
{
    public class FzFairly : IFuzzyTerm
    {
        private readonly FuzzySet _fuzzySet;

        public FzFairly(FuzzySet fuzzySet)
        {
            _fuzzySet = fuzzySet;
        }

        public IFuzzyTerm Clone()
        {
            return new FzFairly(_fuzzySet);
        }

        public double GetDom()
        {
            return _fuzzySet.GetDom() * _fuzzySet.GetDom();
        }

        public void ClearDom()
        {
            _fuzzySet.ClearDom();
        }

        public void OrWithDom(double value)
        {
            _fuzzySet.OrWithDom(value);
        }
    }
}