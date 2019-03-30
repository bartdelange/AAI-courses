namespace AICore.FuzzyLogic.FuzzyHedges
{
    public class FzVery : IFuzzyTerm
    {
        private readonly IFuzzyTerm _fuzzySet;

        public FzVery(IFuzzyTerm fuzzySet)
        {
            _fuzzySet = fuzzySet;
        }

        public IFuzzyTerm Clone()
        {
            return new FzVery(_fuzzySet);
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