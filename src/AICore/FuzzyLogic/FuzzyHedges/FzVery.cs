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