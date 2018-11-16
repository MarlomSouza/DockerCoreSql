using System.Collections.Generic;

namespace docker_app_compose.Dominio.Repository
{
    public interface IRepository<TEntity>
    {
        void Save(TEntity entity);
        TEntity Get(int id);
        IEnumerable<TEntity> Get();
    }
}