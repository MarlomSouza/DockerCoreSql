using docker_app_compose.Dominio;

namespace DockerCoreSql.Dominio
{
    public interface IFila<TEntity> where TEntity: Entity
    {
        void Enviar(TEntity entity);
        void Consumir();
    }
}