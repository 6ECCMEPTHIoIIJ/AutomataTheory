using Lr1.Classes;

namespace Lr1;

internal class Program
{
    private static void Main(string[] args)
    {
        ExecutionChecker checker = new ExecutionChecker();
        UserExecutor executor = new UserExecutor();
        Console.WriteLine("Выберите режим:\n1. Пользовательский ввод\n2. Автоматическая обработка");
        int input;
        do {

            Console.Write("Режим: ");
            input = Utility.ReadInt();
            if (input < 1 || input > 2)
                Utility.WriteError($"[ОШИБКА]: Неизвестный режим работы \'{input}\'. Режим работы должен находиться в диапазоне [1..2].");
        } while (input < 1 || input > 2);



        if (input == 1)
        {
            Console.WriteLine("Пользоательский ввод");
            executor.Run();
        }
        else
        {
            Console.WriteLine("Автоматическая обработка");
            checker.Run();
        }
    }
}