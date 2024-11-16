namespace RtsimTestTask.Refactoring.App.Models;

public class EconomyProfitCalculationParametersModel(
    string parameterName,
    double minAllowedValue,
    double maxScore,
    double penaltyStep,
    double penaltyStepAmount)
{
    /// <summary>Максимально возможный балл.</summary>
    public double MaxScore { get; set; } = maxScore;

    /// <summary>Шаг отнимания баллов за каждое несоответствие параметра.</summary>
    public double PenaltyStep { get; set; } = penaltyStep;

    /// <summary>Шаг для понижения баллов, если реальное значение меньше минимально допустимого.</summary>
    public double PenaltyStepAmount { get; set; } = penaltyStepAmount;

    /// <summary>Минимально допустимое значение параметра.</summary>
    public double MinAllowedValue { get; set; } = minAllowedValue;

    /// <summary>Название параметра.</summary>
    public string ParameterName { get; set; } = parameterName;
}