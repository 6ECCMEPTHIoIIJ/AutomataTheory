using Lr1.Core.Classes;
using Lr1.Core.Factories;
using Lr1.Core.Interfaces;
using Lr1.Interfaces;

namespace Lr1.Classes
{
    internal class ExecutionChecker : IExecutor
    {
        private class WrongRuleException : ArgumentException { }

        private readonly SimpleFormatter _simpleFormatter = new();
        private readonly TreeFormatter _treeFormatter = new();
        private readonly LbfTreeFormatter _lbfTreeFormatter = new();

        private readonly TerminalFactory _terminalFactory;
        private readonly NonTerminalFactory _nonTerminalFactory;
        private readonly TerminalBaseFactory _terminalBaseFactory;
        private readonly ChainElementFactory _chainElementFactory;
        private readonly ChainFactory _chainFactory;
        private readonly RuleFactory _ruleFactory;

        private readonly Dictionary<char, INonTerminal> _nonTerminals = new();
        private readonly Dictionary<char, ITerminal> _terminals = new();
        private readonly List<IRule> _rules = new();

        private readonly List<int> _rulesIndeces = new();
        private IChain _mainChain = new Chain(Array.Empty<IChainElement>());
        private int _deductionType;
        private int _stepCount = 1;
        private List<List<int>> _appliedRulesIndeces = new()
        {
            new (){1, 1, 3, 4, 6, 3, 5, 7, 8, 6, 2, 3, 6},
            new (){1, 1, 2, 3, 3, 3, 4, 5, 6, 6, 7, 8, 6},
            new (){1, 3, 3, 5, 7, 4, 6, 8, 6, 2, 3, 6, 6},
            new (){1, 2, 3, 6, 1, 3, 5, 7, 8, 6, 3, 4, 6},
        };

        public ExecutionChecker()
        {
            _terminalFactory = new TerminalFactory(_terminals);
            _nonTerminalFactory = new NonTerminalFactory(_nonTerminals);
            _terminalBaseFactory = new TerminalBaseFactory(_terminalFactory, _nonTerminalFactory);
            _chainElementFactory = new ChainElementFactory(_terminalBaseFactory);
            _chainFactory = new ChainFactory(_chainElementFactory);
            _ruleFactory = new RuleFactory(_nonTerminalFactory, _chainFactory, _rules);
        }

        public void Run()
        {
            GetGrammatic();
            GetDeductionType();

            foreach (var ruleIndeces in _appliedRulesIndeces)
            {
                _stepCount = 0;
                GetMainChain();
                try
                {
                    if (_deductionType == 1)
                        GetLeftRule(ruleIndeces);
                    else if (_deductionType == 2)
                        GetRightRule(ruleIndeces);
                    else if (_deductionType == 3)
                        GetBothRules(ruleIndeces);

                    if (_mainChain.IsTerminal)
                        WriteResult();
                }
                catch (WrongRuleException)
                {
                    Utility.WriteError("Невозможно применить последовательность правил");
                }
            }
        }

        private void GetGrammatic()
        {
            bool isInputCorrect;
            do
            {
                _nonTerminals.Clear();
                _terminals.Clear();
                _rules.Clear();
                _rulesIndeces.Clear();
                isInputCorrect = true;

                Console.Write("Число правил в КС-грамматике: ");
                int rulesCount = Utility.ReadInt();

                if (rulesCount <= 0)
                {
                    Utility.WriteError($"[ОШИБКА]: Недопустимое число \'{rulesCount}\' правил КС-грамматики. Число должно быть > 0.");
                    isInputCorrect = false;
                    continue;
                }

                Console.WriteLine("КС-грамматики:");
                for (int i = 0; i < rulesCount; i++)
                {
                    ReadRule(i);
                }


                foreach (var nonTerminal in _nonTerminals.Values)
                {
                    if (nonTerminal.Rules.Count == 0)
                    {
                        Utility.WriteError($"[ОШИБКА]: Нет правил вывода для нетерминала \'{nonTerminal}\'.");
                        isInputCorrect = false;
                        continue;
                    }
                }

            }
            while (!isInputCorrect);
        }

        private void GetMainChain()
        {
            Console.Write("Начальная цепочка: ");
            _mainChain = _chainFactory.GetChain(new List<IChainElement> { _chainElementFactory.GetChainElement(_rules[0].Origin) });
            Console.WriteLine(_simpleFormatter.ToString(_mainChain));
        }

        private void GetDeductionType()
        {
            Console.WriteLine("Способы вывода:\n.1 Левый\n.2 Правый\n.3 Смешанный");

            do
            {
                Console.Write("Вывод: ");
                _deductionType = Utility.ReadInt();
                if (_deductionType < 1 || _deductionType > 3)
                    Utility.WriteError($"[ОШИБКА]: Неизвестный способ вывода \'{_deductionType}\'. Способ вывода должен находиться в диапазоне [1..3].");
            }
            while (_deductionType < 1 || _deductionType > 3);

            Console.WriteLine(_deductionType == 1
                ? "Левый"
                : _deductionType == 2
                ? "Правый"
                : "Cмешанный");
        }

        private void GetLeftRule(IList<int> ruleIndeces)
        {
            GetRule(true, false, ruleIndeces);
        }

        private void GetRightRule(IList<int> ruleIndeces)
        {
            GetRule(false, true, ruleIndeces);
        }

        private void GetBothRules(IList<int> ruleIndeces)
        {
            GetRule(true, true, ruleIndeces);
        }

        private void GetRule(bool isLeft, bool isRight, IList<int> ruleIndeces)
        {
            foreach (var rule in ruleIndeces)
            {
                Console.WriteLine($"Шаг {_stepCount}.");
                _stepCount++;
                Console.WriteLine($"Промежуточная цепочка: {_simpleFormatter.ToString(_mainChain)}");
                Console.WriteLine("Доступные правила: ");

                IChainNonTerminalElement? leftNonTerminal = isLeft ? _mainChain.LeftNonTerminal : null;
                if (isLeft && leftNonTerminal == null) return;

                IChainNonTerminalElement? rightNonTerminal = isRight ? _mainChain.RightNonTerminal : null;
                if (isRight && rightNonTerminal == null) return;

                for (int i = 0; i < _rules.Count; i++)
                    if (isLeft && HasRule(leftNonTerminal, i + 1))
                        Console.WriteLine("\t" + _ruleFactory.GetRule(i + 1));
                    else if (isRight && HasRule(rightNonTerminal, i + 1))
                        Console.WriteLine("\t" + _ruleFactory.GetRule(i + 1));

                int ruleIndex = GetRuleIndex();
                _rulesIndeces.Add(ruleIndex);

                if (isLeft && isRight)
                    _ruleFactory.GetRule(ruleIndex).Apply(_mainChain);
                else if (isLeft)
                    _ruleFactory.GetRule(ruleIndex).ApplyLeft(_mainChain);
                else if (isRight)
                    _ruleFactory.GetRule(ruleIndex).ApplyRight(_mainChain);

                bool HasRule(IChainNonTerminalElement? nonTerminalElement, int ruleIndex)
                {
                    return nonTerminalElement != null && nonTerminalElement.NonTerminal.Rules.ContainsKey(ruleIndex);
                }

                int GetRuleIndex()
                {
                    int ruleIndex = rule;
                    bool isLeftRule;
                    bool isRightRule;

                    Console.WriteLine($"Применим правило: {ruleIndex}");

                    isLeftRule = HasRule(leftNonTerminal, ruleIndex);
                    isRightRule = HasRule(rightNonTerminal, ruleIndex);
                    if (!isLeftRule && !isRightRule)
                    {
                        Utility.WriteError($"[ОШИБКА]: Нетерминал \'{(isLeftRule ? rightNonTerminal : leftNonTerminal)}\' не содержит правила #{ruleIndex}");
                        throw new WrongRuleException();
                    }

                    return ruleIndex;
                }
            }
        }

        private IRule ReadRule(int ruleIndex)
        {
            Console.Write($".{ruleIndex + 1} ");
            char originLetter = Utility.ReadLetter();
            string targetString = Utility.ReadString();

            return _ruleFactory.GetRule(originLetter, targetString);
        }

        private void WriteResult()
        {
            Console.WriteLine($"Шаг {_stepCount}.");
            Console.WriteLine($"Терминальная цепочка: {_simpleFormatter.ToString(_mainChain)}");
            Console.WriteLine($"Последовательность правил: {string.Join("->", _rulesIndeces)}");
            Console.WriteLine($"ЛСФ ДВ: {_lbfTreeFormatter.ToString(_mainChain)}");
            Console.WriteLine($"Примечание. Дерево вывода имеет вид:\n{_treeFormatter.ToString(_mainChain)}");
        }
    }
}
