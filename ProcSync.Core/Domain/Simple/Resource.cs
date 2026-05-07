using ProcSync.Core.Interfaces;

namespace ProcSync.Core.Domain.Simple;

public class Resource : IResource
{
    private string _value = "---";

    private const int _tempoEscritaParcialMs = 100; // intervalo entre as três etapas da escrita

    public string Read()
    {
        // A leitura não é atómica – pode ver estados intermédios da escrita
        return _value;
    }

    public void Write(string value)
    {
        throw new NotImplementedException();
    }

    public async Task WriteAsync(string value)
    {
        // Escrita não atómica – simula três passos com pausas
        _value = $"{value}---";
        await Task.Delay(_tempoEscritaParcialMs);
        _value = $"{value}***";
        await Task.Delay(_tempoEscritaParcialMs);
        _value = $"{value}###";
        await Task.Delay(_tempoEscritaParcialMs);
    }
}