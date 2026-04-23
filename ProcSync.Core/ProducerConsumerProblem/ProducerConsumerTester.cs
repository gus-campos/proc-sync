
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

namespace ProcSync.Core.ProducerConsumerProblem;

public static class ProducerConsumerTester
{
    public static void RunTest(
        ICircularBuffer<double> buffer,
        int totalItemsAmount,
        int productionDelayInMs = 0,
        int consumptionDelayInMs = 0
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

    private static void ConsumeMany(
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
                Thread.Yield();
            }

            double item = buffer.Get();
            Console.WriteLine($"Consumiu: {item}");
            Thread.Sleep(productionDelayInMs);
        }
    }

    private static void ProduceMany(
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
                Thread.Yield();
            }

            double item = index;
            buffer.Put(item);
            Console.WriteLine($"Produziu: {item}");
            Thread.Sleep(consumptionDelayInMs);
        }
    }
}
