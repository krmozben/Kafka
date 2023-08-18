using Confluent.Kafka;

namespace Producer
{

    public class KafkaProducer<TKey, TValue> : IDisposable
    {
        private readonly IProducer<TKey, TValue> producer;

        public KafkaProducer(string bootstrapServers)
        {
            var config = new ProducerConfig { BootstrapServers = bootstrapServers };
            producer = new ProducerBuilder<TKey, TValue>(config).Build();
        }

        public async Task ProduceAsync(string topic, TKey key, TValue value)
        {
            var message = new Message<TKey, TValue> { Key = key, Value = value };
            var deliveryReport = await producer.ProduceAsync(topic, message);
            Console.WriteLine($"Produced message to: {deliveryReport.TopicPartitionOffset}");
        }

        public void Dispose()
        {
            producer.Dispose();
        }
    }
}
