using RtsimTestTask.Refactoring.App.Calculators;
using RtsimTestTask.Refactoring.App.Models;

namespace RtsimRefactoring.Tests;

public class WorkTimeScoreCalculatorTests
{
    private const double Epsilon = 1e-6;

    [Fact]
    public void WorkTimeScoreCalculator_BaseTest_CalculatesCorrectResult()
    {
        // Arrange
        var startTime = DateTime.Now;
        var model = new WorkTimeCalculationModel(
            startTime,
            startTime + TimeSpan.FromSeconds(16),
            TimeSpan.FromSeconds(10),
            TimeSpan.FromSeconds(2),
            5f,
            1f
        );

        var scoreCalculator = new WorkTimeScoreCalculator(model);
        var expectedResult = model.MaxScoreWorkTime - 3 * model.StepScore;

        // Act
        var result = scoreCalculator.CalculateWorkTimeScore();

        // Assert
        Assert.Equal(expectedResult, result, Epsilon);
    }

    [Fact]
    public void CalculateWorkTimeScore_WhenWorkTimeIsWithinLimit_ShouldReturnMaxScore()
    {
        // Arrange
        var model = new WorkTimeCalculationModel(
            DateTime.Now,
            DateTime.Now + TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(10),
            100,
            10
        );
        var calculator = new WorkTimeScoreCalculator(model);

        // Act
        var result = calculator.CalculateWorkTimeScore();

        // Assert
        Assert.Equal(model.MaxScoreWorkTime, result, Epsilon);
    }

    [Fact]
    public void CalculateWorkTimeScore_WhenFinalScoreIsNegative_ShouldReturnZero()
    {
        // Arrange
        var model = new WorkTimeCalculationModel(
            DateTime.Now,
            DateTime.Now + TimeSpan.FromMinutes(60),
            TimeSpan.FromMinutes(30),
            TimeSpan.FromMinutes(10),
            10,
            10
        );
        var calculator = new WorkTimeScoreCalculator(model);

        // Act
        var result = calculator.CalculateWorkTimeScore();

        // Assert
        Assert.Equal(0, result, Epsilon);
    }

    [Fact]
    public void CalculateWorkTimeScore_WhenWorkTimeIsExactlyMaxWorkTime_ShouldReturnMaxScore()
    {
        // Arrange
        var model = new WorkTimeCalculationModel(
            DateTime.Now,
            DateTime.Now + TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(40),
            TimeSpan.FromMinutes(10),
            100,
            10
        );
        var calculator = new WorkTimeScoreCalculator(model);

        // Act
        var result = calculator.CalculateWorkTimeScore();

        // Assert
        Assert.Equal(model.MaxScoreWorkTime, result, Epsilon);
    }
}