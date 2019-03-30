using System.Linq;

namespace AICore.FuzzyLogic.FuzzyOperators
{
    public class FzOr : FuzzyOperator
    {
        public FzOr(params IFuzzyTerm[] terms) : base(terms)
        {
        }

        public override IFuzzyTerm Clone()
        {
            return new FzOr(FuzzyTerms.Select(item => item.Clone()).ToArray());
        }

        public override double GetDom()
        {
            return FuzzyTerms.Select(fuzzyTerm => fuzzyTerm.GetDom()).Concat(new[] {double.MinValue}).Max();
        }

        public override void ClearDom()
        {
            foreach (var fuzzyTerm in FuzzyTerms) fuzzyTerm.ClearDom();
        }

        public override void OrWithDom(double value)
        {
            foreach (var fuzzyTerm in FuzzyTerms) fuzzyTerm.OrWithDom(value);
        }
    }
}