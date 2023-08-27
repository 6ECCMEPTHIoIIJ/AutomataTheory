namespace Lr1.Core.Interfaces;

internal interface IRule
{
    INonTerminal Origin { get; }
    IChain Target { get; }
    bool ApplyLeft(IChain chain);
    bool ApplyRight(IChain chain);
    bool Apply(IChain chain);
}