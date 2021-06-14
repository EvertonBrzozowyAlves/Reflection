using System;

namespace ByteBank.Portal.Infra.IoC
{
    public interface IContainer
    {
        void Register(Type origin, Type destiny);
        void Register<TOrigyn, TDestiny>() where TDestiny : TOrigyn;
        object Get(Type origin);
    }
}