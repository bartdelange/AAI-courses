using System;
using System.Collections.Generic;
using System.Numerics;

namespace AICore.FuzzyLogic
{
    public class FuzzyModule
    {
        private Dictionary<string, FuzzyVariable> _fuzzyVars;
        private List<FuzzyRule> _rules;

        public enum DefuzzifyType
        {
            MaxAv, Centroid
        }
        
        private int _centroidSamplesToUse = 15;

        private void SetConfidencesOfConsequentsToZero()
        {
            
        }

        public FuzzyVariable CreatFLV(string VarName)
        {
            throw new NotImplementedException();
        }

        public void AddRule(FuzzyTerm antecedent, FuzzyTerm consequence)
        {
            
        }

        public void Fuzzify(string NameOfFLV, double val)
        {
            
        }

        public void DeFuzzify(string key, DefuzzifyType method)
        {
            
        }
    }
}