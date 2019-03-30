using System.Linq;

namespace AICore.FuzzyLogic.FuzzyOperators
{
    public class FzAnd : FuzzyOperator
    {
        public FzAnd(params IFuzzyTerm[] terms) : base(terms)
        {
        }

        public override IFuzzyTerm Clone()
        {
            return new FzAnd(FuzzyTerms.Select(item => item.Clone()).ToArray());
        }

        public override double GetDom()
        {
            return FuzzyTerms.Select(fuzzyTerm => fuzzyTerm.GetDom()).Concat(new[] {double.MaxValue}).Min();
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