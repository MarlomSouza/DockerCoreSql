using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using DockerCoreSql.Dominio;


namespace DockerCoreSql.Infra
{
    public class Fila : IFila
    {
        private const string bootstrapServer = "bootstrap.servers";
        private readonly string uri;
        private readonly string topic;

        public Fila()
        {
            // uri = ConfigurationManager.
            topic = "myTopic";

        }
        public string Consumir()
        {
            throw new System.NotImplementedException();
        }

        public void Enviar(string valor)
        {
            var config = new Dictionary<string, object> { { bootstrapServer, uri } };

            using (var producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8)))
            {
                EnviarMensagem(producer, valor);
            }
        }

        private void EnviarMensagem(Producer<Null, string> producer, string link)
        {
            var dr = producer.ProduceAsync(topic, null, link).Result;
            Console.WriteLine($"Delivered '{dr.Value}' to: {dr.TopicPartitionOffset} {dr.Partition}");
        }
    }


}