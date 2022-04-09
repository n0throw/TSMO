using System.Diagnostics.CodeAnalysis;

namespace TSMO;


/// <summary>
/// 
/// </summary>
/// <param name="CountSetup">Количество установок</param>
/// <param name="ProbabilityDefeat">Вероятность поражения</param>
/// <param name="RateSetup">Скорострельность каждой установки</param>
public record struct Channel
{
    public int CountSetup { get; init; }
    public double ProbabilityDefeat { get; init; }
    public double RateSetup { get; init; }
    public double ProcessingTime { get; init; }

    public bool IsActive { get; set; }
    public double? StartTime { get; set; }
    public int? IndexRequest { get; set; }

    /// <summary>
    /// Считает скорострельность всего канала
    /// </summary>
    /// <returns>Скорострельность всего канала</returns>
    public double GetRateChannel() => CountSetup * ProbabilityDefeat * RateSetup;
}

public class ChannelComparerWithIndex : IEqualityComparer<Channel>
{
    public bool Equals(Channel x, Channel y) => x.IndexRequest == y.IndexRequest;

    public int GetHashCode([DisallowNull] Channel obj) => obj.IndexRequest.GetHashCode();
}