using System;

namespace ByteBank.Portal.Infra.IoC
{
    public interface IContainer
    {
        void Register(Type origin, Type destiny);
        object Get(Type origin);
    }
}