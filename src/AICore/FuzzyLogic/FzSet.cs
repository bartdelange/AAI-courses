using AICore.FuzzyLogic.FuzzySets;

namespace AICore.FuzzyLogic
{
    public class FzSet : IFuzzyTerm
    {
        public FzSet(FuzzySet fuzzySet)
        {
            FZ = fuzzySet;
        }

        private FuzzySet FZ { get; }

        public IFuzzyTerm Clone()
        {
            return new FzSet(FZ);
        }

        public double GetDom()
        {
            return FZ.GetDom();
        }

        public void ClearDom()
        {
            FZ.ClearDom();
        }

        public void OrWithDom(double value)
        {
            FZ.OrWithDom(value);
        }
    }
}