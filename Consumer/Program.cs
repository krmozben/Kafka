using Consumer;

string bootstrapServers = "localhost:9092";
string topic1 = "test-topic-1";
string groupId1 = "group-id-1";

string topic2 = "test-topic-2";
string groupId2 = "group-id-2";

using (var consumer = new KafkaConsumer<string, string>())
{
    var cts = new CancellationTokenSource();
    Task.Run(() => consumer.Consume(bootstrapServers, groupId1, topic1, cts.Token));

    Task.Run(() => consumer.Consume(bootstrapServers, groupId2, topic2, cts.Token));


    Console.WriteLine("Press Enter to exit...");
    Console.ReadLine();
}