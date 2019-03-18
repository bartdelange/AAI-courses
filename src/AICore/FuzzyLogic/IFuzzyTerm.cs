namespace AICore.FuzzyLogic
{
    public interface IFuzzyTerm
    {
        IFuzzyTerm Clone();
        double GetDOM();
        void ClearDOM();
        void ORWithDOM(double value);
    }
}