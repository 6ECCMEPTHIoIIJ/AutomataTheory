using Lr1.Core.Classes;
using Lr1.Core.Interfaces;

namespace Lr1.Core.Factories
{
    internal class ChainFactory
    {
        private readonly ChainElementFactory _elementFactory;

        public ChainFactory(ChainElementFactory elementFactory)
        {
            _elementFactory = elementFactory;
        }

        public IChain GetChain(string chainString)
        {
            return GetChain(chainString.Select(_elementFactory.GetChainElement));
        }

        public IChain GetChain(IEnumerable<IChainElement> elements)
        {
            return new Chain(elements);
        }
    }
}
