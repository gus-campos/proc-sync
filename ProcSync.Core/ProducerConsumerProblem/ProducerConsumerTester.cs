
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

namespace ProcSync.Core.ProducerConsumerProblem;

public static class ProducerConsumerTester
{
    public static void RunTest(ICircularBuffer<double> buffer, int totalItemsAmount,
        int productionDelayInMs = 0, int consumptionDelayInMs = 0
    )
    {
        var consuptionTask = Task.Run(
            () => ConsumeMany(buffer, totalItemsAmount, consumptionDelayInMs)
        );

        var producionTask = Task.Run(
            () => ProduceMany(buffer, totalItemsAmount, productionDelayInMs)
        );

        Task.WaitAll([consuptionTask, producionTask]);
    }

    async private static Task ConsumeMany(
        ICircularBuffer<double> buffer,
        int totalItemsAmount,
        int productionDelayInMs
    )
    {
        for (int index = 0; index < totalItemsAmount; index++)
        {
            while (buffer.IsEmpty)
            {
                Console.WriteLine("Consumer esperando");
                await Task.Yield();
            }

            double item = buffer.Get();
            Console.WriteLine($"Consumiu: {item}");
            await Task.Delay(productionDelayInMs);
        }
    }

    async private static Task ProduceMany(
        ICircularBuffer<double> buffer,
        int totalItemsAmount,
        int consumptionDelayInMs
    )
    {
        for (int index = 0; index < totalItemsAmount; index++)
        {
            while (buffer.IsFull)
            {
                Console.WriteLine("Producer esperando");
                await Task.Yield();
            }

            double item = index;
            buffer.Put(item);
            Console.WriteLine($"Produziu: {item}");
            await Task.Delay(consumptionDelayInMs);
        }
    }
}
