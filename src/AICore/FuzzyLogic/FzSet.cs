using AICore.FuzzyLogic.FuzzySets;

namespace AICore.FuzzyLogic
{
    public class FzSet : IFuzzyTerm
    {
        public FzSet(FuzzySet fuzzySet)
        {
            fZ = fuzzySet;
        }

        private FuzzySet fZ { get; set; }

        public IFuzzyTerm Clone()
        {
            return new FzSet(fZ);
        }

        public double GetDOM()
        {
            return fZ.GetDOM();
        }

        public void ClearDOM()
        {
            fZ.ClearDOM();
        }

        public void ORWithDOM(double value)
        {
            fZ.ORWithDOM(value);
        }
    }
}