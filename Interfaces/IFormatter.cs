using Lr1.Core.Interfaces;

namespace Lr1.Interfaces
{
    internal interface IFormatter
    {
        string? ToString(IChainNonTerminalElement nonTerminalElement);
        string? ToString(IChainTerminalElement terminalElement);
        string? ToString(IChain chain);
        string? ToString(IChainElement element);
    }
}
