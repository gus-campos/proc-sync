namespace ProcSync.Core.Utils;

public record Result<TItem>(bool Succes, TItem? Item);
