namespace docker_app_compose.Dominio
{
    public class Pessoa : Entity
    {
        public string Nome { get; protected set; }
        public string Cpf { get; protected set; }

        public Pessoa(string nome, string cpf)
        {
            ValidarPessoa(nome, cpf);

            Nome = nome;
            Cpf = cpf;
        }

        private static void ValidarPessoa(string nome, string cpf)
        {
            DomainException.Quando(string.IsNullOrEmpty(nome), "Informar nome");
            DomainException.Quando(string.IsNullOrEmpty(cpf), "Informar CPF");
        }
    }
}