using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using docker_app_compose;
using docker_app_compose.Dominio;
using docker_app_compose.Infra;
using DockerCoreSql.Dominio;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace DockerCoreSql.Infra
{
    public class Fila : IFila<Pessoa>
    {
        private const string bootstrapServer = "bootstrap.servers";
        private readonly string uri;
        private readonly string topic;
        private readonly Consumidor _consumidor;

        public Fila(IOptions<MyConfiguration> myConfiguration, Consumidor consumidor)
        {
            _consumidor = consumidor;
            // uri = "myConfiguration.Value.EnderecoFila;"
            uri = "localhost:9092";
            topic = "myTopic";
        }

        public void Consumir()
        {
            _consumidor.ConsumirFila();
        }

        public void Enviar(Pessoa entity)
        {
            var config = new Dictionary<string, object> { { bootstrapServer, uri } };
            var valor = ConverterParaJson(entity);
            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                EnviarMensagem(producer, valor);
            }
        }

        private string ConverterParaJson(Entity entity)
        {
            return JsonConvert.SerializeObject(entity);
        }

        private void EnviarMensagem(Producer<Null, string> producer, string valor)
        {
            var dr = producer.ProduceAsync(topic, null, valor).Result;
            Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset} {dr.Partition}");
        }
    }


}