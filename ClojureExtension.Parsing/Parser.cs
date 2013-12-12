using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClojureExtension.Parsing
{
	public class Parser
	{
		private readonly Lexer _lexer;
		private readonly Stack<List<object>> _listStack;

		public Parser(Lexer lexer)
		{
			_lexer = lexer;
			_listStack = new Stack<List<object>>();
			_listStack.Push(new List<object>());
		}

		public List<object> Parse()
		{
			Token token = _lexer.Next();

			while (token != null)
			{
				if (token.Type == TokenType.ListStart)
				{
					_listStack.Push(new List<object>());
				}
				else if (token.Type == TokenType.ListEnd)
				{
					if (_listStack.Count == 1) throw new Exception("Unexpected token: " + token.Text);
					var completedList = new List<object>(_listStack.Pop());
					_listStack.Peek().Add(completedList);
				}
				else if (token.Type == TokenType.Nil)
				{
					_listStack.Peek().Add(null);
				}
				else if (token.Type == TokenType.String)
				{
					_listStack.Peek().Add(token.Text.Trim(new[] {'\"'}));
				}
				else if (token.Type == TokenType.Number)
				{
					_listStack.Peek().Add(double.Parse(token.Text));
				}
				else if (token.Type == TokenType.Boolean)
				{
					_listStack.Peek().Add(bool.Parse(token.Text));
				}
				else if (token.Type == TokenType.HexNumber)
				{
					int num = Int32.Parse(token.Text.Replace("0x", ""), NumberStyles.HexNumber);
					_listStack.Peek().Add((double) num);
				}
				else if (token.Type == TokenType.Symbol || token.Type == TokenType.BuiltIn)
				{
					_listStack.Peek().Add(token.Text);
				}
				else if (token.Type == TokenType.Keyword)
				{
					_listStack.Peek().Add(token.Text);
				}
				else if (token.Type == TokenType.Unknown)
				{
					throw new Exception("Unknown token: " + token.Text + " at " + token.StartIndex);
				}

				token = _lexer.Next();
			}

			if (_listStack.Count > 1) throw new Exception("Expected )");
			return _listStack.Peek();
		}
	}
}