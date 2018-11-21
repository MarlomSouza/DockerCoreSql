namespace DockerCoreSql.Dominio
{
    public interface IFila
    {
        void Enviar(string valor);
        string Consumir();
    }
}