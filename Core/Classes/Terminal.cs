using Lr1.Core.Interfaces;

namespace Lr1.Core.Classes
{
    internal class Terminal : ITerminal
    {
        public Terminal(char letter)
        {
            Letter = letter;
        }

        public char Letter { get; }

        public override string? ToString()
        {
            return Letter.ToString();
        }
    }
}
