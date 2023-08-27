using Lr1.Core.Interfaces;

namespace Lr1.Core.Classes
{
    internal class ChainTerminalElement : IChainTerminalElement
    {
        public ChainTerminalElement(ITerminal terminal)
        {
            Terminal = terminal;
        }

        public ITerminal Terminal { get; }

        public bool IsTerminal => true;

        public object Clone()
        {
            return this;
        }

        public override string? ToString()
        {
            return Terminal.ToString();
        }
    }
}
