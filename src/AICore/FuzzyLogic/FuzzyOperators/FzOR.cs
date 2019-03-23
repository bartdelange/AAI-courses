using System.Linq;

namespace AICore.FuzzyLogic.FuzzyOperators
{
    public class FzOR : FuzzyOperator
    {
        public FzOR(params IFuzzyTerm[] terms) : base(terms)
        {
        }

        public override IFuzzyTerm Clone()
        {
            return new FzOR(FuzzyTerms.Select(item => item.Clone()).ToArray());
        }

        public override double GetDOM()
        {
            return FuzzyTerms.Select(fuzzyTerm => fuzzyTerm.GetDOM()).Concat(new[] {double.MinValue}).Max();
        }

        public override void ClearDOM()
        {
            foreach (var fuzzyTerm in FuzzyTerms) fuzzyTerm.ClearDOM();
        }

        public override void ORWithDOM(double value)
        {
            foreach (var fuzzyTerm in FuzzyTerms) fuzzyTerm.ORWithDOM(value);
        }
    }
}