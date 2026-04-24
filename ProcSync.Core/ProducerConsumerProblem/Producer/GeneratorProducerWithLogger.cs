
using ProcSync.Core.ProducerConsumerProblem.CircularBuffer;

namespace ProcSync.Core.ProducerConsumerProblem.Producer;

public class GeneratorProducerWithLogger<TItem> : IProducer<TItem>
{
    private readonly int _delayInMiliseconds;
    private readonly IGenerator<TItem> _generator;

    public GeneratorProducerWithLogger(IGenerator<TItem> generator, int delayInMiliseconds)
    {
        _delayInMiliseconds = delayInMiliseconds;
        _generator = generator;
    }

    public void Produce(IBuffer<TItem> buffer)
    {
        while (buffer.IsEmpty)
        {
            // FIXME: Tirar daqui
            Console.WriteLine("\rProducer esperando...");
            Thread.Yield();
        }

        // Mudar para sleep no while?
        // Mudar para async?
        // Como aguardar?
        // Como fazer lock?
        Thread.Sleep(_delayInMiliseconds);
        TItem item = _generator.GenerateNext();
        buffer.Put(item);
        Console.WriteLine($"Produziu: {item}");
    }
}
