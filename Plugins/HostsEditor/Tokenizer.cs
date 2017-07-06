using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

//thank https://codereview.stackexchange.com/questions/129663/simple-tokenizer-parser

namespace HostsEditor
{
    public class Token
    {
        public Token(string type, string token, int index)
        {
            Value = token;
            Type = type;
            Index = index;
        }
        public string Value { get; private set; }
        public string Type { get; private set; }
        public int Index { get; private set; }
    }
    public class Tokenizer
    {
        private class TokenDefinition
        {
            private readonly Regex myRegex;
            public TokenDefinition(string type, string regex)
            {
                myRegex = new Regex(regex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                Type = type;
            }
            public string Type { get; set; }
            public MatchCollection Matches(string input)
            {
                return myRegex.Matches(input);
            }
        }

        private readonly List<TokenDefinition> myTokenDefinitions = new List<TokenDefinition>();

        public Tokenizer WithToken(string type, params string[] regexes)
        {
            foreach (var regex in regexes)
                myTokenDefinitions.Add(new TokenDefinition(type, regex));
            return this;
        }

        public Token[] Tokenize(string input)
        {
            if (input == null)
                input = string.Empty;

            var occupied = new bool[input.Length];

            return CollectTokens(input, occupied);
        }

        private Token[] CollectTokens(string input, bool[] occupied)
        {
            var tokens = new List<Token>();

            foreach (var tokenDefinition in myTokenDefinitions)
                foreach (var token in TokenizeInternal(input, occupied, tokenDefinition))
                    tokens.Add(token);

            return tokens.OrderBy(t => t.Index).ToArray();
        }

        private static IEnumerable<Token> TokenizeInternal(string input, bool[] occupied, TokenDefinition tokenDefinition)
        {
            foreach (Match match in tokenDefinition.Matches(input))
            {
                if (!match.Success)
                    continue;

                var indexRange = Enumerable.Range(match.Index, match.Length).ToList();
                if (indexRange.Any(idx => occupied[idx]))
                    continue;

                indexRange.ForEach(idx => occupied[idx] = true);

                yield return new Token(tokenDefinition.Type, match.Value, match.Index);
            }
        }

        public static Tokenizer Empty
        {
            get { return new Tokenizer(); }
        }
    }
}
