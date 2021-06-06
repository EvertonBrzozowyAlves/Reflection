using System;

namespace ByteBank.Portal.Infra.Binding
{
    public class KeyValueArgument
    {
        public KeyValueArgument(string key, string value)
        {
            Key = key ?? throw new ArgumentNullException(nameof(key));
            Value = value ?? throw new ArgumentNullException(nameof(key));
        }

        public string Key { get; private set; }
        public string Value { get; private set; }

    }
}