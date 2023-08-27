using Lr1.Core.Interfaces;

namespace Lr1.Core.Classes
{
    internal class ChainNonTerminalElement : IChainNonTerminalElement
    {
        public ChainNonTerminalElement(INonTerminal nonTerminal)
        {
            NonTerminal = nonTerminal;
        }

        public INonTerminal NonTerminal { get; }

        public IChainElement? Child { get; set; } = null;

        public bool IsTerminal => Child != null && Child.IsTerminal;

        public object Clone()
        {
            return new ChainNonTerminalElement(NonTerminal);
        }

        public override string? ToString()
        {
            return Child == null ? NonTerminal.ToString() : Child.ToString();
        }
    }
}
