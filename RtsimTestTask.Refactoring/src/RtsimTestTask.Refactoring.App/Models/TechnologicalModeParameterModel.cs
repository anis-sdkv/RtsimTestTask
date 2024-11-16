namespace RtsimTestTask.Refactoring.App.Models;

/// <summary>Параметр технологического режима</summary>
public class TechnologicalModeParameterModel(
    int id,
    string serialNumber,
    string deviceParameterName,
    double value,
    double minNorm,
    double maxNorm)
{
    /// <summary>Идентификатор параметра.</summary>
    public int Id { get; set; } = id;

    /// <summary>Порядковый номер параметра.</summary>
    public string SerialNumber { get; set; } = serialNumber;

    /// <summary>Имя параметра прибора.</summary>
    public string DeviceParameterName { get; set; } = deviceParameterName;

    /// <summary>Значение параметра.</summary>
    public double Value { get; set; } = value;

    /// <summary>Минимально допустимая норма параметра.</summary>
    public double MinNorm { get; set; } = minNorm;

    /// <summary>Максимально допустимая норма параметра.</summary>
    public double MaxNorm { get; set; } = maxNorm;
}