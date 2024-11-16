using RtsimTestTask.Refactoring.App.Models;
using RtsimTestTask.Refactoring.App.Utils;

namespace RtsimTestTask.Refactoring.App.Calculators;

public class EconomyScoreCalculator(
    IReadOnlyCollection<TechnologicalModeParameterModel> technologicalModeParameters,
    IReadOnlyCollection<EconomyProfitCalculationParametersModel> economyProfitCalculationParameters,
    IEnumerable<string> economicEfficiencyIndicators)
{
    private const double EfficiencyMax = 10;
    private const double EfficiencyStep = 2;

    public double CalculateEconomyScore()
    {
        var economy = economyProfitCalculationParameters.Sum(CalculateProfitForParameter);
        var efficiency = CalculateEfficiency();
        return economy + efficiency;
    }

    private double CalculateProfitForParameter(EconomyProfitCalculationParametersModel profitParameters)
    {
        var technologicalParameter = technologicalModeParameters
            .FirstOrDefault(b => b.DeviceParameterName == profitParameters.ParameterName);

        if (technologicalParameter == null || technologicalParameter.Value >= profitParameters.MinAllowedValue)
            return profitParameters.MaxScore;

        var difference = profitParameters.MinAllowedValue - technologicalParameter.Value;
        var stepsCount = Math.Truncate(difference / profitParameters.PenaltyStepAmount);

        return profitParameters.MaxScore - stepsCount * profitParameters.PenaltyStep;
    }

    private double CalculateEfficiency()
    {
        var score = EfficiencyMax;

        foreach (var economyParam in economicEfficiencyIndicators)
        {
            var param = technologicalModeParameters.FirstOrDefault(p => p.DeviceParameterName == economyParam);
            if (param == null) continue;
            if (!param.Value.IsBetween(param.MinNorm, param.MaxNorm)) score -= EfficiencyStep;
        }

        return score < 0 ? 0 : score;
    }
}