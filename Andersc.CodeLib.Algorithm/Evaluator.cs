using System;
using System.Linq;
using System.Text;
using Generic = System.Collections.Generic;

using Andersc.CodeLib.Common;
using Andersc.CodeLib.Common.Collections;

namespace Andersc.CodeLib.Algorithm
{
    // TODO: Impl by F#
    public class Evaluator
    {
        private const int EOL = 0;
        private const int VALUE = 1;
        private const int OPAREN = 2;
        private const int CPAREN = 3;
        private const int EXP = 4;
        private const int MULT = 5;
        private const int DIV = 6;
        private const int PLUS = 7;
        private const int MINUS = 8;

        private Stack<int> opStack = new Stack<int>();
        private Stack<long> postfixStack = new Stack<long>();
        private char[] separators = { '+', '-', '*', '/', '^', '(', ')' };
        private string[] tokens = null;

        private static Precedence[] precTable = 
        {
            new Precedence(0, -1), // EOL
            new Precedence(0, 0), // VALUE
            new Precedence(100, 0), // OPAREN
            new Precedence(0, 99), // CPAREN
            new Precedence(6, 5), // EXP
            new Precedence(3, 4), // MULT
            new Precedence(3, 4), // DIV
            new Precedence(1, 2), // PLUS
            new Precedence(1, 2) // MINUS
        };

        public Evaluator(string s)
        {
            opStack.Push(EOL);
            ResolveTokens(s);
        }

        private void ResolveTokens(string s)
        {
            int index = -1;
            int startIndex = 0;
            Generic.List<string> tokenParts = new Generic.List<string>();

            //index = s.IndexOfAny(separators, startIndex);
            while((index = s.IndexOfAny(separators, startIndex)) > 0)
            {
                int offset = index - startIndex;
                if (offset > 0)
                {
                    tokenParts.Add(s.Substring(startIndex, index - startIndex));
                }
                tokenParts.Add(s.Substring(index, 1));

                startIndex = index + 1;
            }

            tokenParts.Add(s.Substring(startIndex));
            tokenParts.Add(string.Empty); // TODO: Simulate EOL
            tokens = tokenParts.ToArray();
        }

        public long Evaluate()
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                Token token = GetToken(tokens[i]);
                ProcessToken(token);

                if (token.Type == EOL)
                {
                    break;
                }
            }

            if (postfixStack.IsEmpty())
            {
                Console.WriteLine("Missing operand.");
                return 0;
            }

            long result = postfixStack.Pop();
            if (postfixStack.IsNotEmpty())
            {
                throw new Exception("Missing operators.");
            }

            return result;
        }

        private Token GetToken(string s)
        {
            if (s.IsBlank()) { return new Token(); } // TODO: Sure?
            if (s == "^") { return new Token(EXP); }
            if (s == "/") { return new Token(DIV); }
            if (s == "*") { return new Token(MULT); }
            if (s == "(") { return new Token(OPAREN); }
            if (s == ")") { return new Token(CPAREN); }
            if (s == "+") { return new Token(PLUS); }
            if (s == "-") { return new Token(MINUS); }

            // Otherwise, it is a value
            long value = long.Parse(s);
            return new Token(VALUE, value);
        }

        private void ProcessToken(Token lastToken)
        {
            int topOp;
            int lastType = lastToken.Type;

            switch (lastType)
            {
                case VALUE:
                    postfixStack.Push(lastToken.Value);
                    break;
                case CPAREN:
                    while ((topOp = opStack.Top) != OPAREN && topOp != EOL)
                    {
                        BinaryOp(topOp);
                    }
                    if (topOp == OPAREN)
                    {
                        opStack.Pop(); // Get rid of opening parentheseis
                    }
                    else
                    {
                        throw new Exception("Missing opening parenthesis.");
                    }
                    break;
                default:
                    while (precTable[lastType].InputSymbol <= precTable[topOp = opStack.Top].TopOfStack)
                    {
                        BinaryOp(topOp);
                    }
                    if (lastType != EOL)
                    {
                        opStack.Push(lastType);
                    }
                    break;
            }
        }

        private long GetTop()
        {
            throw new NotImplementedException();
        }

        private void BinaryOp(int topOp)
        {
            if (topOp == OPAREN)
            {
                throw new InvalidOperationException("Unbalanced parenthesis.");
                //opStack.Pop();
                //return;
            }

            long rhs = PopPostfix();
            long lhs = PopPostfix();

            if (topOp == EXP)
            {
                postfixStack.Push((long)(Math.Pow(lhs, rhs)));
            }
            else if (topOp == PLUS)
            {
                postfixStack.Push(lhs + rhs);
            }
            else if (topOp == MINUS)
            {
                postfixStack.Push(lhs - rhs);
            }
            else if (topOp == MULT)
            {
                postfixStack.Push(lhs * rhs);
            }
            else if (topOp == DIV)
            {
                postfixStack.Push(lhs / rhs);
            }

            opStack.Pop();
        }

        private long PopPostfix()
        {
            if (postfixStack.IsEmpty())
            {
                throw new InvalidOperationException("Missing operand.");
            }

            return postfixStack.Pop();
        }

        private class Token
        {
            private int type = EOL;
            private long value = 0;

            public int Type
            {
                get { return type; }
            }

            public long Value
            {
                get { return value; }
            }

            public Token() : this(EOL) { }

            public Token(int t) : this(t, 0) { }

            public Token(int t, long v)
            {
                type = t;
                value = v;
            }
        }

        private class Precedence
        {
            public int InputSymbol { get; set; }
            public int TopOfStack { get; set; }

            public Precedence(int inSymbol, int topSymbol)
            {
                InputSymbol = inSymbol;
                TopOfStack = topSymbol;
            }
        }


    }
}
