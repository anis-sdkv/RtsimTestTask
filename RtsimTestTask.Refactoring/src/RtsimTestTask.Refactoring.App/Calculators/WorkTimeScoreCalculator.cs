using RtsimTestTask.Refactoring.App.Models;

namespace RtsimTestTask.Refactoring.App.Calculators;

//Задание 2
//Сделать аналогичную функцию для расчета времени работы
//общее время работы за каждые шаг по времени превышающие максимально допустимое время на работу вычитается шаг по баллам из максимального балла
// WorkTime за каждые StepTime превышающие MaxMinutesWorkTime вычитается StepScore из MaxScoreWorkTime
// пример: WorkTime =16 секунд, StepTime=2 секунды, MaxMinutesWorkTime= 10 секунд, StepScore = 1 балл, MaxScoreWorkTime = 5 баллов. Результат = 2 балла
//входные значения: время начала работы, время окончания работы, максимально допустимое время на работу, максимальный балл, шаг по времени, шаг по баллам
//выходное значение: полученное количество баллов

public class WorkTimeScoreCalculator(WorkTimeCalculationModel model)
{
    public double CalculateWorkTimeScore()
    {
        var workTime = model.EndTime - model.StartTime;

        if (workTime <= model.MaxWorkTime) return model.MaxScoreWorkTime;

        var exceededTime = workTime - model.MaxWorkTime;
        var stepCount = Math.Truncate(exceededTime / model.StepTime);
        var totalScoreDeduction = stepCount * model.StepScore;

        var finalScore = model.MaxScoreWorkTime - totalScoreDeduction;
        return finalScore < 0 ? 0 : finalScore;
    }
}