using Lr1.Core.Classes;
using Lr1.Core.Interfaces;

namespace Lr1.Core.Factories
{
    internal class TerminalFactory
    {
        private readonly IDictionary<char, ITerminal> _terminals;

        public TerminalFactory(IDictionary<char, ITerminal> terminals)
        {
            _terminals = terminals;
        }

        public ITerminal GetTerminal(char letter)
        {
            if (!char.IsLower(letter)) throw new ArgumentException($"Invalid character \'{letter}\' for {nameof(Terminal)} representation. Character must be lowercase.");

            if (!_terminals.ContainsKey(letter))
                _terminals.Add(letter, new Terminal(letter));

            return _terminals[letter];
        }
    }
}
