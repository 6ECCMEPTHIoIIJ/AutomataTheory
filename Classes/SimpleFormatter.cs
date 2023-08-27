using Lr1.Core.Interfaces;
using Lr1.Interfaces;

namespace Lr1.Classes
{
    internal class SimpleFormatter : IFormatter
    {
        public string? ToString(IChainNonTerminalElement nonTerminalElement)
        {
            return nonTerminalElement.Child == null 
                ? nonTerminalElement.NonTerminal.ToString() 
                : ToString(nonTerminalElement.Child);
        }

        public string? ToString(IChainTerminalElement terminalElement)
        {
            return terminalElement.Terminal.ToString();
        }

        public string? ToString(IChain chain)
        {
            return string.Join("", chain.Elements.Select(ToString));
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
