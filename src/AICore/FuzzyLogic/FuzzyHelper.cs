using System.Diagnostics;

namespace AICore.FuzzyLogic
{
    public class FuzzyHelper
    {
        public FuzzyModule InitSampleModule()
        {
            var fm = new FuzzyModule();
            var distToTarget = fm.CreateFLV("DistToTarget");

            var targetClose = distToTarget.AddLeftShoulderSet("TargetClose", 0, 25, 150);
            var targetMedium = distToTarget.AddTriangularSet("TargetMedium", 25, 150, 300);
            var targetFar = distToTarget.AddRightShoulderSet("TargetFar", 150, 300, 500);

            var ammoStatus = fm.CreateFLV("AmmoStatus");

            var ammoLoads = ammoStatus.AddRightShoulderSet("AmmoLoads", 10, 30, 100);
            var ammoOkay = ammoStatus.AddTriangularSet("AmmoOkay", 0, 10, 30);
            var ammoLow = ammoStatus.AddLeftShoulderSet("AmmoLow", 0, 0, 10);

            var desirability = fm.CreateFLV("Desirability");

            var veryDesirable = desirability.AddRightShoulderSet("VeryDesirable", 50, 75, 100);
            var desirable = desirability.AddTriangularSet("Desirable", 25, 50, 75);
            var undesirable = desirability.AddLeftShoulderSet("Undesirable", 0, 25, 50);

            fm.AddRule("targetClose -> undesirable", targetClose, undesirable);
            fm.AddRule("targetMedium -> veryDesirable", targetMedium, veryDesirable);
            fm.AddRule("targetFar -> undesirable", targetFar, undesirable);
            fm.AddRule("ammoLow -> undesirable", ammoLow, undesirable);
            fm.AddRule("ammoOkay -> desirable", ammoOkay, desirable);
            fm.AddRule("ammoLoads -> veryDesirable", ammoLoads, veryDesirable);

            // Fuzzify the inputs
            fm.Fuzzify("DistToTarget", 200);
            fm.Fuzzify("AmmoStatus", 8);

            // The inferred conclusion
            var desStatus = fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);

            Debug.WriteLine(desStatus);

            return fm;
        }

        public double CalculateDesirability(FuzzyModule fm, double dist, double ammo)
        {
            fm.Fuzzify("DistToTarget", dist);
            fm.Fuzzify("AmmoStatus", ammo);
//this method automatically processes the rules and defuzzifies //the inferred conclusion
            return fm.DeFuzzify("Desirability", FuzzyModule.DefuzzifyType.MaxAv);
        }
    }
}