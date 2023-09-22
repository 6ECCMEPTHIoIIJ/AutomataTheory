using Lr1.Core.Interfaces;
using Lr1.Interfaces;

namespace Lr1.Classes
{
    internal class LatexFormatter : IFormatter
    {
        public string? ToString(IChainNonTerminalElement nonTerminalElement)
        {
            return $".{nonTerminalElement.NonTerminal.Letter} {(nonTerminalElement.Child == null ? "" : $"[{ToString(nonTerminalElement.Child)}]")}";
        }

        public string? ToString(IChainTerminalElement terminalElement)
        {
            return $".{terminalElement.Terminal.Letter} ";
        }

        public string? ToString(IChain chain)
        {
            return string.Join("", chain.Elements.Select(e => $"[{ToString(e)}]"));
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
