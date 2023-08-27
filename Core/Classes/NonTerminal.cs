using Lr1.Core.Interfaces;

namespace Lr1.Core.Classes;

internal class NonTerminal : INonTerminal
{
    public NonTerminal(char letter)
    {
        Letter = letter;
    }

    public char Letter { get; }

    public IDictionary<int, IRule> Rules { get; } = new Dictionary<int, IRule>();

    public override string? ToString()
    {
        return Letter.ToString();
    }
}