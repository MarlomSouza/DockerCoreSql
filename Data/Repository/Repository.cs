using System.Collections.Generic;
using System.Linq;
using docker_app_compose.Data.Context;
using docker_app_compose.Dominio;
using docker_app_compose.Dominio.Repository;

namespace docker_app_compose.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly ApplicationDbContext context;

        public Repository(ApplicationDbContext applicationDbContext) => context = applicationDbContext;

        public TEntity Get(int id) => context.Set<TEntity>().SingleOrDefault(entity => entity.Id == id);

        public IEnumerable<TEntity> Get() => context.Set<TEntity>().AsEnumerable();

        public virtual void Save(TEntity entity)
        {
            context.Add<TEntity>(entity);
            context.SaveChanges();
        }
    }
}