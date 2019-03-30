using AICore.FuzzyLogic;

namespace AICore.Behaviour
{
    public class TiredModule
    {
        private FuzzyModule _tiredFm = new FuzzyModule();

        public TiredModule()
        {
            // Distance module
            var tired = _tiredFm.CreateFlv("Tired");
            var notTiredNotTired = tired.AddLeftShoulderSet("NotTired", 0, 25, 50);
            var kindaTired = tired.AddTriangularSet("KindaTired", 25, 50, 75);
            var veryTired = tired.AddRightShoulderSet("VeryTired", 50, 75, 100);

            var energy = _tiredFm.CreateFlv("Energy");
            var noEnergy = energy.AddRightShoulderSet("NoEnergy", 0, 25, 50);
            var energyABit = energy.AddTriangularSet("EnergyABit", 25, 50, 75);
            var energyALot = energy.AddLeftShoulderSet("EnergyALot", 50, 75, 100);

            _tiredFm.AddRule("noEnergy -> veryTired", noEnergy, veryTired);
            _tiredFm.AddRule("energyABit -> kindaTired", energyABit, kindaTired);
            _tiredFm.AddRule("energyALot -> veryTired", energyALot, veryTired);
        }

        public double CheckTiredness(double movement)
        {
            _tiredFm.Fuzzify("Moved", movement);
            return _tiredFm.DeFuzzify("Tired", FuzzyModule.DefuzzifyType.MaxAv);
        } 
    }
}