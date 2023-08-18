using Confluent.Kafka;

namespace Consumer
{
    public class KafkaConsumer<TKey, TValue> : IDisposable
    {

        public KafkaConsumer()
        {
        }

        public async Task Consume(string bootstrapServers, string groupId, string topic, CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = bootstrapServers,
                GroupId = groupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            var consumer = new ConsumerBuilder<TKey, TValue>(config).Build();
            consumer.Subscribe(topic);
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = consumer.Consume(cancellationToken);
                    Console.WriteLine($"Consumer {consumer.MemberId} - Consumed message: {consumeResult.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming: {e.Error.Reason}");
                }
            }
        }

        public void Dispose()
        {

        }
    }
}
