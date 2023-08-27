using Lr1.Classes;
using Lr1.Core.Classes;
using Lr1.Core.Factories;
using Lr1.Core.Interfaces;

namespace Lr1;

internal class Program
{
    private static void Main(string[] args)
    {
        UserExecutor executor = new UserExecutor();
        executor.Run();
    }
}