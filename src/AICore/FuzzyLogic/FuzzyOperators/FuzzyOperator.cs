using System.Collections.Generic;

namespace AICore.FuzzyLogic.FuzzyOperators
{
    public abstract class FuzzyOperator : IFuzzyTerm
    {
        public FuzzyOperator(params IFuzzyTerm[] terms)
        {
            FuzzyTerms = terms;
        }

        public IEnumerable<IFuzzyTerm> FuzzyTerms { get; set; }

        public abstract IFuzzyTerm Clone();
        public abstract double GetDom();
        public abstract void ClearDom();
        public abstract void OrWithDom(double value);
    }
}