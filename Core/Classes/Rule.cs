using Lr1.Core.Interfaces;

namespace Lr1.Core.Classes;

internal class Rule : IRule
{
    private readonly int _index;
    private readonly INonTerminal _origin;
    private readonly IChain _target;

    public Rule(INonTerminal origin, IChain target, int index)
    {
        _origin = origin;
        _target = target;
        _index = index;
    }

    public IChain Target => (IChain)_target.Clone();

    private char OriginLetter => _origin.Letter;

    public INonTerminal Origin => _origin;

    public bool ApplyLeft(IChain chain)
    {
        return Apply(chain, true);
    }

    public bool ApplyRight(IChain chain)
    {
        return Apply(chain, false);
    }

    public bool Apply(IChain chain)
    {
        return ApplyLeft(chain) || ApplyRight(chain);
    }

    private bool Apply(IChain chain, bool isLeft)
    {
        IChainNonTerminalElement? nonTerminal = isLeft
            ? chain.LeftNonTerminal
            : chain.RightNonTerminal;
        if (nonTerminal == null
            || OriginLetter != nonTerminal.NonTerminal.Letter)
            return false;

        nonTerminal.Child = Target;
        return true;
    }

    public override string? ToString()
    {
        return $"#{_index} {_origin} -> {_target}";
    }
}