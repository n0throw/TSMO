namespace TSMO;

public readonly record struct Channel(int CountSetup, double ProbabilityDefeat, double RateSetup)
{
    public double GetRateChannel() => CountSetup * ProbabilityDefeat * RateSetup;
}
