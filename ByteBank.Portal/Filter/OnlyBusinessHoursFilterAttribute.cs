using System;
using ByteBank.Portal.Infra.Filter;

namespace ByteBank.Portal.Filter
{
    public class OnlyBusinessHoursFilterAttribute : FilterAttribute //good practice
    {
        public override bool CanContinue()
        {
            var currentHour = DateTime.Now.Hour;
            return currentHour >= 9 && currentHour <= 16;
        }
    }
}