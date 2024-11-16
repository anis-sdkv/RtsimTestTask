namespace RtsimTestTask.Refactoring.App.Utils;

public static class DoubleExtensions
{
    public static bool IsBetween(this double x, double min, double max) => x > min && x < max;
}