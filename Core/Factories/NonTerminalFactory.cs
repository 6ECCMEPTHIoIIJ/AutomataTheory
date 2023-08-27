using Lr1.Core.Classes;
using Lr1.Core.Interfaces;

namespace Lr1.Core.Factories
{
    internal class NonTerminalFactory
    {
        private readonly IDictionary<char, INonTerminal> _nonTerminals;

        public NonTerminalFactory(IDictionary<char, INonTerminal> nonTerminals)
        {
            _nonTerminals = nonTerminals;
        }

        public INonTerminal GetNonTerminal(char letter)
        {
            if (!char.IsUpper(letter)) throw new ArgumentException($"Invalid character \'{letter}\' for {nameof(NonTerminal)} representation. Character must be uppercase.");

            if (!_nonTerminals.ContainsKey(letter))
                _nonTerminals.Add(letter, new NonTerminal(letter));

            return _nonTerminals[letter];
        }
    }
}
