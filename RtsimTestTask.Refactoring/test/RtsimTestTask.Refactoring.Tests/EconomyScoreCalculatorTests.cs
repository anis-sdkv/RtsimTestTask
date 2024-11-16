using RtsimTestTask.Refactoring.App.Calculators;
using RtsimTestTask.Refactoring.App.Models;

namespace RtsimRefactoring.Tests;

public class EconomyScoreCalculatorTests
{
    private const double Epsilon = 1e-6;

    [Fact]
    public void EconomyScoreCalculator_BaseTest_CalculatesCorrectResult()
    {
        // Arrange
        const int expectedResult = 31;
        var economyCalculator = new EconomyScoreCalculator(
            ParametersStore.TechnologicalModeParameters,
            ParametersStore.EconomyProfitParameters,
            ParametersStore.EconomyEfficiencyParameterNames
        );

        // Act
        var result = economyCalculator.CalculateEconomyScore();

        // Assert
        Assert.Equal(expectedResult, result, Epsilon);
    }
}