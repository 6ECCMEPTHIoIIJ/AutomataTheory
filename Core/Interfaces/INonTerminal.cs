namespace Lr1.Core.Interfaces;

internal interface INonTerminal : ITerminalBase
{
    IDictionary<int, IRule> Rules { get; }
}