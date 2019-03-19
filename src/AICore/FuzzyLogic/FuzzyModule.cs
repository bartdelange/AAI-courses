#region

using System.Collections.Generic;
using AICore.Util;

#endregion

namespace AICore.FuzzyLogic
{
    public class FuzzyModule
    {
        public enum DefuzzifyType
        {
            MaxAv,
            Centroid
        }

        private const int _centroidSamplesToUse = 15;
        private readonly Dictionary<string, FuzzyVariable> _fuzzyVars = new Dictionary<string, FuzzyVariable>();
        private readonly Dictionary<string, FuzzyRule> _rules = new Dictionary<string, FuzzyRule>();

        private void SetConfidencesOfConsequentsToZero()
        {
            foreach (var rule in _rules) rule.Value.SetConfidenceOfConsequentToZero();
        }

        public FuzzyVariable CreateFLV(string VarName)
        {
            return _fuzzyVars.GetOrCreate(VarName, new FuzzyVariable());
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

            //clear the DOMs of all the consequents
            SetConfidencesOfConsequentsToZero();

            //process the rules            
            foreach (var rule in _rules) rule.Value.Calculate();

            //now defuzzify the resultant conclusion using the specified method
            switch (method)
            {
                case DefuzzifyType.Centroid: return flv.DeFuzzifyCentroid(_centroidSamplesToUse);
                case DefuzzifyType.MaxAv: return flv.DeFuzzifyMaxAv();
            }

            return 0;
        }
    }
}