namespace Lr1.Core.Interfaces;

internal interface IChain : IChainElement
{
    IReadOnlyList<IChainElement> Elements { get; }
    IChainNonTerminalElement? LeftNonTerminal { get; }
    IChainNonTerminalElement? RightNonTerminal { get; }
}