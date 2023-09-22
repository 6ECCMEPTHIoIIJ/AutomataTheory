using Lr1.Core.Interfaces;

using System.Collections.Generic;

namespace Lr1.Core.Classes
{
    internal class Chain : IChain
    {
        private readonly List<IChainElement> _elements;

        public Chain(IEnumerable<IChainElement> elements)
        {
            _elements = new List<IChainElement>(elements);
        }

        public object Clone()
        {
            return new Chain(_elements.Select(e => (IChainElement)e.Clone()));
        }

        public bool IsTerminal => _elements.All(e => e.IsTerminal);

        public IReadOnlyList<IChainElement> Elements => _elements;

        public IChainNonTerminalElement? LeftNonTerminal => GetNonTerminal(true);

        public IChainNonTerminalElement? RightNonTerminal => GetNonTerminal(false);

        public IList<IChainNonTerminalElement> NonTerminals
        {
            get
            {
                IList<IChainNonTerminalElement> nonTerminals = new List<IChainNonTerminalElement>();
                GetAllNonTerminals(ref nonTerminals);

                return nonTerminals;
            }
        }

        private void GetAllNonTerminals(ref IList<IChainNonTerminalElement> nonTerminals)
        {
            int index = _elements.FindIndex(e => !e.IsTerminal);
            while (index != -1)
            {
                var nonTerminal = (IChainNonTerminalElement)_elements[index];
                if (nonTerminal.Child == null)
                {
                    nonTerminals.Add(nonTerminal);
                }
                else
                {
                    var subChain = (Chain)nonTerminal.Child;
                    subChain.GetAllNonTerminals(ref nonTerminals);
                }

                index = _elements.FindIndex(index + 1, _elements.Count - index - 1, e => !e.IsTerminal);
            }
        }

        private IChainNonTerminalElement? GetNonTerminal(bool isLeft)
        {
            var index = isLeft
                ? _elements.FindIndex(e => !e.IsTerminal)
                : _elements.FindLastIndex(e => !e.IsTerminal);
            if (index == -1) return null;

            var nonTerminal = (IChainNonTerminalElement)_elements[index];
            if (nonTerminal.Child == null) return nonTerminal;

            var subChain = (Chain)nonTerminal.Child;
            return subChain.GetNonTerminal(isLeft);
        }

        public override string? ToString()
        {
            return string.Join("", Elements);
        }
    }
}
