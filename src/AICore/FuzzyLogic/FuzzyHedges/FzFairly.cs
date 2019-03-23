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

        public double GetDOM()
        {
            return _fuzzySet.GetDOM() * _fuzzySet.GetDOM();
        }

        public void ClearDOM()
        {
            _fuzzySet.ClearDOM();
        }

        public void ORWithDOM(double value)
        {
            _fuzzySet.ORWithDOM(value);
        }
    }
}