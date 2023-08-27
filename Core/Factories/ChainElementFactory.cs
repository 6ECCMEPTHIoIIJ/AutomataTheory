using Lr1.Core.Classes;
using Lr1.Core.Interfaces;

namespace Lr1.Core.Factories
{
    internal class ChainElementFactory
    {
        private readonly TerminalBaseFactory _terminalBaseFactory;

        public ChainElementFactory(TerminalBaseFactory terminalBaseFactory)
        {
            _terminalBaseFactory = terminalBaseFactory;
        }

        public IChainElement GetChainElement(char letter)
        {
            return GetChainElement(_terminalBaseFactory.GetTerminalBase(letter));
        }

        public IChainElement GetChainElement(ITerminalBase terminalBase)
        {
            if (terminalBase is ITerminal terminal)
                return GetChainElement(terminal);
            if (terminalBase is INonTerminal nonTerminal)
                return GetChainElement(nonTerminal);
            throw new ArgumentOutOfRangeException();
        }

        public IChainElement GetChainElement(ITerminal terminal)
        {
            return new ChainTerminalElement(terminal);
        }

        public IChainElement GetChainElement(INonTerminal nonTerminal)
        {
            return new ChainNonTerminalElement(nonTerminal);
        }
    }
}
