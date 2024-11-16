using RtsimTestTask.Refactoring.App.Models;

namespace RtsimRefactoring.Tests;

public static class ParametersStore
{
    public static List<TechnologicalModeParameterModel> TechnologicalModeParameters =>
    [
        new(0, "0", "Норма расхода сырья", 87.43, 24.27, 76.99),
        new(1, "1", "Норма расхода теплоносителя", 100.17, 7.55, 520.19),
        new(2, "2", "Норма расхода пара СД", 68.34, 17.57, 65.7),
        new(3, "3", "Норма расхода воды", 75.89, 27.1, 80.3),
        new(4, "4", "Себестоимость", 84.60, 65.35, 90.6),
        new(5, "5", "Param5", 94.27, 18.29, 63.49),
        new(6, "6", "Param6", 94.43, 10.1, 30.18),
        new(7, "7", "Прибыль", 948065, 22.11, 67.62),
        new(8, "8", "Премия участника", 86815, 26.87, 77.50),
        new(9, "9", "ВПР", 30610543, 28.33, 100.28)
    ];

    public static string[] EconomyEfficiencyParameterNames =>
    [
        "Норма расхода сырья",
        "Норма расхода теплоносителя",
        "Норма расхода пара СД",
        "Норма расхода воды",
        "Себестоимость"
    ];

    public static List<EconomyProfitCalculationParametersModel> EconomyProfitParameters =>
    [
        new("ВПР", 5500000, 10, 1, 500000),
        new("Премия участника", 150000, 10, 1, 20000),
        new("Прибыль", 1200000, 10, 1, 100000)
    ];
}