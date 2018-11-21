using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace docker_app_compose.Infra
{
    public class Consumidor
    {
        static Dictionary<string, object> conf;
        private static string topic = "myTopic";

        public Consumidor()
        {
            conf = new Dictionary<string, object>
            {
            { "group.id", "Grupo" },
            { "bootstrap.servers", "localhost:9092" },
            { "enable.auto.commit", true},
            { "auto.offset.reset", "smallest" }
            };
        }

        private static Consumer<Ignore, string> GetConsumer() => new Consumer<Ignore, string>(conf, null, new StringDeserializer(Encoding.UTF8));
        public void ConsumirFila()
        {
            using (var consumer = GetConsumer())
            {
                consumer.OnMessage += (_, msg) =>
                {
                    var err = consumer.CommitAsync().Result.Error;
                    if (!err)
                        ImprimirMensagem(msg);
                };

                consumer.OnError += (_, error)
                  => Console.WriteLine($"Error: {error}");

                consumer.OnConsumeError += (_, msg)
                  => Console.WriteLine($"Consume error ({msg.TopicPartitionOffset}): {msg.Error}");

                consumer.Subscribe(topic);
                while (true)
                    BuscarNovaMensagem(consumer);
            }
        }

        private static void BuscarNovaMensagem(Consumer<Ignore, string> consumer) =>
           consumer.Poll(TimeSpan.FromMilliseconds(10));
        private static void ImprimirMensagem(Message<Ignore, string> msg) =>
           Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value}");
    }

}