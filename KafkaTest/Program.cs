using Confluent.Kafka;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using System;
using System.Threading.Tasks;

namespace KafkaTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ProducerConfig { BootstrapServers = "192.168.50.207:9092" };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync("quickstart-events", new Message<Null, string> { Value = "test" });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }



            //var options = new KafkaOptions(new Uri("http://172.31.32.1:9092"));
            //var router = new BrokerRouter(options);
            //var client = new Producer(router);

            //await client.SendMessageAsync("quickstart-events", new[] { new Message("hello world") });

            //using (client) { }
        }
    }
}
