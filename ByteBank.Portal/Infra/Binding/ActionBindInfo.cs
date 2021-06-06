using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace ByteBank.Portal.Infra.Binding
{
    public class ActionBindInfo
    {
        public MethodInfo MethodInfo { get; private set; }
        public IReadOnlyCollection<KeyValueArgument> KeyValueTuple { get; private set; }

        public ActionBindInfo(MethodInfo methodInfo, IEnumerable<KeyValueArgument> keyValueArgument)
        {
            MethodInfo = methodInfo ?? throw new ArgumentNullException(nameof(methodInfo));

            if (keyValueArgument == null)
            {
                throw new ArgumentNullException(nameof(keyValueArgument));
            }

            KeyValueTuple = new ReadOnlyCollection<KeyValueArgument>(keyValueArgument.ToList());
        }
    }
}