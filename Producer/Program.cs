
using Producer;

string bootstrapServers = "localhost:9092";
string topic1 = "test-topic-1";
string topic2 = "test-topic-2";

using (var producer = new KafkaProducer<string, string>(bootstrapServers))
{
    for (int i = 0; i < 100; i++)
    {
        Task.Run(() => producer.ProduceAsync(topic1, "topic-1", "topic-1 Hello Kafka Message = " + i));
        Task.Run(() => producer.ProduceAsync(topic2, "topic-2", "topic-2 Hello Kafka Message = " + i));


        Thread.Sleep(1000);
    }

    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
}