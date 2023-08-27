namespace Lr1.Core.Interfaces;

internal interface IChainNonTerminalElement : IChainElement
{
    INonTerminal NonTerminal { get; }
    IChainElement? Child { get; set; }
}