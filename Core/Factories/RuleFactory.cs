using Lr1.Core.Classes;
using Lr1.Core.Interfaces;


namespace Lr1.Core.Factories
{
    internal class RuleFactory
    {
        private readonly NonTerminalFactory _nonTerminalFactory;
        private readonly ChainFactory _chainFactory;

        private readonly IList<IRule> _rules;

        public RuleFactory(NonTerminalFactory nonTerminalFactory, ChainFactory chainFactory, IList<IRule> rules)
        {
            _nonTerminalFactory = nonTerminalFactory;
            _chainFactory = chainFactory;
            _rules = rules;
        }

        public IRule GetRule(char originLetter, string targetString)
        {
            return GetRule(_nonTerminalFactory.GetNonTerminal(originLetter), _chainFactory.GetChain(targetString));
        }

        public IRule GetRule(INonTerminal origin, IChain target)
        {
            _rules.Add(new Rule(origin, target, _rules.Count + 1));
            _nonTerminalFactory.GetNonTerminal(origin.Letter).Rules.Add(_rules.Count, _rules.Last());
            return _rules.Last();
        }

        public IRule GetRule(int index)
        {
            return _rules[index - 1];
        }
    }
}
