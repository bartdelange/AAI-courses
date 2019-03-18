#region

using System.Linq;

#endregion

namespace AICore.FuzzyLogic.FuzzyOperators
{
    public class FzAND : FuzzyOperator
    {
        public FzAND(params IFuzzyTerm[] terms) : base(terms)
        {
        }

        public override IFuzzyTerm Clone()
        {
            return new FzAND(FuzzyTerms.Select(item => item.Clone()).ToArray());
        }

        public override double GetDOM()
        {
            return FuzzyTerms.Select(fuzzyTerm => fuzzyTerm.GetDOM()).Concat(new[] {double.MaxValue}).Min();
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