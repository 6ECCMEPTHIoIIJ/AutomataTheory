namespace Lr1.Core.Interfaces;

internal interface IChainElement : ICloneable
{
    bool IsTerminal { get; }
}