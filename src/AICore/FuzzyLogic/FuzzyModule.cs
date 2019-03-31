using System.Collections.Generic;
using AICore.Util;

namespace AICore.FuzzyLogic
{
    public class FuzzyModule
    {
        public enum DefuzzifyType
        {
            MaxAv,
            Centroid
        }

        private const int CentroidSamplesToUse = 15;
        private readonly Dictionary<string, FuzzyVariable> _fuzzyVars;
        private readonly Dictionary<string, FuzzyRule> _rules;

        public FuzzyModule()
        {
            _fuzzyVars = new Dictionary<string, FuzzyVariable>();
            _rules = new Dictionary<string, FuzzyRule>();
        }

        private void SetConfidencesOfConsequentsToZero()
        {
            foreach (var rule in _rules) rule.Value.SetConfidenceOfConsequentToZero();
        }

        public FuzzyVariable CreateFlv(string varName)
        {
            return _fuzzyVars.GetOrCreate(varName, new FuzzyVariable());
        }

        public void AddRule(string ruleName, IFuzzyTerm antecedent, IFuzzyTerm consequence)
        {
            _rules.Add(ruleName, new FuzzyRule(antecedent, consequence));
        }

        public void Fuzzify(string nameOfFlv, double val)
        {
            if (!_fuzzyVars.TryGetValue(nameOfFlv, out var fuzzyVar))
                throw new KeyNotFoundException("That FLV was not found in any set");

            fuzzyVar.Fuzzify(val);
        }

        public double DeFuzzify(string key, DefuzzifyType method)
        {
            //first make sure the named FLV exists in this module
            if (!_fuzzyVars.TryGetValue(key, out var flv))
                throw new KeyNotFoundException("That FLV was not found in any set");

            //clear the DOMs of all the consequences
            SetConfidencesOfConsequentsToZero();

            //process the rules            
            foreach (var rule in _rules) rule.Value.Calculate();

            //now defuzzify the resultant conclusion using the specified method
            switch (method)
            {
                case DefuzzifyType.Centroid: return flv.DeFuzzifyCentroid(CentroidSamplesToUse);
                case DefuzzifyType.MaxAv: return flv.DeFuzzifyMaxAv();
            }

            return 0;
        }
    }
}