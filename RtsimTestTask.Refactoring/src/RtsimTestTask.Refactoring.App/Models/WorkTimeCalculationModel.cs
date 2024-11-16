namespace RtsimTestTask.Refactoring.App.Models;

public record WorkTimeCalculationModel(
    DateTime StartTime,
    DateTime EndTime,
    TimeSpan MaxWorkTime,
    TimeSpan StepTime,
    double MaxScoreWorkTime,
    double StepScore
);