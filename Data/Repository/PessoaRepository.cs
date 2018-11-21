using System.Collections;
using System.Collections.Generic;
using System.Linq;
using docker_app_compose.Data.Context;
using docker_app_compose.Dominio;
using DockerCoreSql.Dominio;

namespace docker_app_compose.Data.Repository
{
    public class PessoaRepository : Repository<Pessoa>
    {
        private readonly IFila fila;

        public PessoaRepository(ApplicationDbContext appDBContext, IFila fila) : base(appDBContext)
        {
            this.fila = fila;
        }
        public Pessoa ObterPorCpf(string cpf) => context.Pessoas.SingleOrDefault(pessoa => pessoa.Cpf == cpf);
        public IEnumerable<Pessoa> ObterPorNome(string nome) => context.Pessoas.Where(pessoa => pessoa.Nome.Contains(nome));

        public override void Save(Pessoa entity)
        {
            base.Save(entity);
            fila.Enviar(entity.Nome);
        }
    }
}