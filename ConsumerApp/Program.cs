using Confluent.Kafka;
using System;

class Program
{
    static void Main(string[] args)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = "localhost:9092", // Kafka broker(s) address
            GroupId = "my-consumer-group", // Consumer group ID
            AutoOffsetReset = AutoOffsetReset.Earliest // Reset offset to the earliest message in case no offset is committed
        };

        using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            consumer.Subscribe("Registration-topic"); // Subscribe to Kafka topic

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume(); // Consume message
                    Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occurred: {e.Error.Reason}");
                }
            }
        }
    }
}
