using Lr1.Core.Classes;
using Lr1.Core.Interfaces;

namespace Lr1.Core.Factories
{
    internal class TerminalBaseFactory
    {
        private readonly TerminalFactory _terminalFactory;
        private readonly NonTerminalFactory _nonTerminalFactory;

        public TerminalBaseFactory(TerminalFactory terminalFactory, NonTerminalFactory nonTerminalFactory)
        {
            _terminalFactory = terminalFactory;
            _nonTerminalFactory = nonTerminalFactory;
        }

        public ITerminalBase GetTerminalBase(char letter)
        {
            if (!char.IsLetter(letter)) throw new ArgumentException($"Invalid character \'{letter}\' for {nameof(Terminal)} and {nameof(NonTerminal)} representation. Character must be letter.");

            return char.IsUpper(letter)
                ? _nonTerminalFactory.GetNonTerminal(letter)
                : _terminalFactory.GetTerminal(letter);
        }
    }
}
