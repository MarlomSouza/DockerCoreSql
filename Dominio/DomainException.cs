using System;

namespace docker_app_compose.Dominio
{

    public class DomainException : Exception
    {
        public DomainException(string error) : base(error) { }

        public static void Quando(bool haErro, string erro)
        {
            if (haErro)
                throw new DomainException(erro);
        }

    }
}