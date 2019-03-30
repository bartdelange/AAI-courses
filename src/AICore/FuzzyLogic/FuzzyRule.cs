namespace AICore.FuzzyLogic
{
    public class FuzzyRule
    {
        private readonly IFuzzyTerm _antecedent;
        private readonly IFuzzyTerm _consequence;

        public FuzzyRule(IFuzzyTerm antecedent, IFuzzyTerm consequence)
        {
            _antecedent = antecedent.Clone();
            _consequence = consequence.Clone();
        }

        public void SetConfidenceOfConsequentToZero()
        {
            _consequence.ClearDom();
        }

        public void Calculate()
        {
            _consequence.OrWithDom(_antecedent.GetDom());
        }

        public override string ToString()
        {
            return $"{_antecedent.GetDom()}, {_consequence.GetDom()}";
        }
    }
}