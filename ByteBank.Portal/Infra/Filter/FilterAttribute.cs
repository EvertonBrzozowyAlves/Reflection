using System;

namespace ByteBank.Portal.Infra.Filter
{
    public abstract class FilterAttribute : Attribute
    {
        public abstract bool CanContinue();
    }
}