using System;
using ByteBank.Portal.Filter;
using ByteBank.Portal.Infra.Binding;

namespace ByteBank.Portal.Infra.Filter
{
    public class FilterResolver
    {
        public FilterResult VerifyFilters(ActionBindInfo actionBindInfo)
        {
            var methodInfo = actionBindInfo.MethodInfo;

            // var attributeType = (new FilterAttribute()).GetType(); //envolve not used classes with parenthesis => can't instanciate abstract class

            var attributes = methodInfo.GetCustomAttributes(typeof(FilterAttribute), false); //not getting methods that inherit this attribute

            foreach (FilterAttribute attribute in attributes) //cast
                if (!attribute.CanContinue())
                    return new FilterResult(false);

            return new FilterResult(true);
        }
    }
}