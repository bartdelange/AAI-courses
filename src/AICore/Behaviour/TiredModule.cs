using AICore.FuzzyLogic;

namespace AICore.Behaviour
{
    public class TiredModule
    {
        private FuzzyModule _tiredFM = new FuzzyModule();
        public bool CanMove { get; private set; }

        public TiredModule()
        {
            // Distance module
            var tired = _tiredFM.CreateFLV("Tired");
            var notTired = tired.AddLeftShoulderSet("NotTired", 0, 25, 50);
            var kindaTired = tired.AddTriangularSet("KindaTired", 25, 50, 75);
            var veryTired = tired.AddRightShoulderSet("VeryTired", 50, 75, 100);

            var moved = _tiredFM.CreateFLV("Moved");
            var notMoved = moved.AddRightShoulderSet("NotMoved", 0, 25, 50);
            var movedABit = moved.AddTriangularSet("MovedABit", 25, 50, 75);
            var movedALot = moved.AddLeftShoulderSet("MovedALot", 50, 75, 100);

            _tiredFM.AddRule("goalClose -> veryDesirable", notMoved, notTired);
            _tiredFM.AddRule("goalMedium -> undesirable", movedABit, kindaTired);
            _tiredFM.AddRule("goalFar -> undesirable", movedALot, veryTired);
        }

        public double CheckTiredness(double movement)
        {
            _tiredFM.Fuzzify("Moved", movement);
            return _tiredFM.DeFuzzify("Tired", FuzzyModule.DefuzzifyType.MaxAv);
        } 
    }
}