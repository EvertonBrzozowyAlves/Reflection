namespace ByteBank.Portal.Infra.Filter
{
    public class FilterResult
    {
        public bool CanContinue { get; private set; }
        public FilterResult(bool canContinue)
        {
            CanContinue = canContinue;
        }
    }
}