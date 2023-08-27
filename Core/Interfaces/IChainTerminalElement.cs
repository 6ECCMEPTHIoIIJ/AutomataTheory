namespace Lr1.Core.Interfaces;

internal interface IChainTerminalElement : IChainElement
{
    ITerminal Terminal { get; }
}