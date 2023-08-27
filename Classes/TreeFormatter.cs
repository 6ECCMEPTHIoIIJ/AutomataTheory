using Lr1.Core.Classes;
using Lr1.Core.Interfaces;
using Lr1.Interfaces;

using System.Text;

namespace Lr1.Classes
{
    internal class TreeFormatter : IFormatter
    {
        private int _nestingLevel = 0;
        private readonly List<bool> _nestingLevelHasLinks = new() { true };

        private int NestingLevel
        {
            get => _nestingLevel;
            set
            {
                if (_nestingLevel == value) return;
                _nestingLevel = value;

                if (_nestingLevel >= _nestingLevelHasLinks.Count)
                    _nestingLevelHasLinks.Add(true);
            }
        }

        private string Nesting
        {
            get
            {
                var builder = new StringBuilder(NestingLevel * 4);
                for (int i = 0; i < NestingLevel; i++)
                    builder.Append(_nestingLevelHasLinks[i] ? "│   " : "    ");

                return builder.ToString();
            }
        }

        public string? ToString(IChainNonTerminalElement nonTerminalElement)
        {
            NestingLevel++;
            var str = nonTerminalElement.NonTerminal.ToString()
                + (nonTerminalElement.Child == null
                ? ""
                : "─┐\n" + ToString(nonTerminalElement.Child));
            NestingLevel--;

            return str;
        }

        public string? ToString(IChainTerminalElement terminalElement)
        {
            return terminalElement.Terminal.ToString();
        }

        public string? ToString(IChain chain)
        {
            string nesting = Nesting;
            var builder = new StringBuilder((nesting.Length + 3) * chain.Elements.Count);

            _nestingLevelHasLinks[NestingLevel] = true;
            for (int i = 0; i < chain.Elements.Count; i++)
                builder.AppendLine(nesting + "├─" + ToString(chain.Elements[i]));
            _nestingLevelHasLinks[NestingLevel] = false;

            builder.Append(nesting + "└─" + ToString(chain.Elements[^1]));

            return builder.ToString();
        }

        public string? ToString(IChainElement element)
        {
            if (element is IChainTerminalElement terminalElement)
                return ToString(terminalElement);
            if (element is IChainNonTerminalElement nonTerminalElement)
                return ToString(nonTerminalElement);
            if (element is IChain chain)
                return ToString(chain);
            return null;
        }
    }
}
