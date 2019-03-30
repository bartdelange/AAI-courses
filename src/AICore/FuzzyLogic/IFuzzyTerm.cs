namespace AICore.FuzzyLogic
{
    public interface IFuzzyTerm
    {
        IFuzzyTerm Clone();
        double GetDom();
        void ClearDom();
        void OrWithDom(double value);
    }
}