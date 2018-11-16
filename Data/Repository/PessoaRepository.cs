using System.Collections;
using System.Collections.Generic;
using System.Linq;
using docker_app_compose.Data.Context;
using docker_app_compose.Dominio;

namespace docker_app_compose.Data.Repository
{
    public class PessoaRepository : Repository<Pessoa>
    {
        public PessoaRepository(ApplicationDbContext appDBContext) : base(appDBContext) { }
        public Pessoa ObterPorCpf(string cpf) => context.Pessoas.SingleOrDefault(pessoa => pessoa.Cpf == cpf);
        public IEnumerable<Pessoa> ObterPorNome(string nome) => context.Pessoas.Where(pessoa => pessoa.Nome.Contains(nome));
    }
}