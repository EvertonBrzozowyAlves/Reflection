using System;
using ByteBank.Portal.Filter;
using ByteBank.Portal.Infra.Binding;

namespace ByteBank.Portal.Infra.Filter
{
    public class FilterResolver
    {
        public object VerifyFilters(ActionBindInfo actionBindInfo)
        {
            var methodInfo = actionBindInfo.MethodInfo;

            var attributeType = (new OnlyBusinessHoursAttribute()).GetType(); //envolve not used classes with parenthesis

            var attributes = methodInfo.GetCustomAttributes(attributeType, false);

            foreach (var attribute in attributes)
            {
                //TODO: implement
            }

            return new { }; //TODO: implement

        }
    }
}