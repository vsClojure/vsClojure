using System;
using System.Collections.Generic;
using System.Text;

namespace ClojureExtension.Parsing
{
	public class Lexer
	{
		private static readonly List<string> BuiltInFunctions = new List<string>()
		{
			"def", "if", "do", "let", "quote", "var", "fn", "loop",
			"recur", "throw", "try", "monitor-enter", "monitor-exit",
			"new", "set!", "."
		};

		private static readonly List<char> ValidNonLetterSymbolPrefixes = new List<char>()
		{
			'*', '+', '!', '-', '_', '?', '>', '<', '=', '$', '&'
		};

		private readonly PushBackCharacterStream _source;

		public Lexer(PushBackCharacterStream inputText)
		{
			_source = inputText;
		}

		public Token Next()
		{
			if (!_source.HasMore) return null;

			char currentChar = _source.Next();
			Token nextToken = null;

			if (currentChar == '(')
			{
				nextToken = new Token(TokenType.ListStart, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == ')')
			{
				nextToken = new Token(TokenType.ListEnd, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == '[')
			{
				nextToken = new Token(TokenType.VectorStart, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == ']')
			{
				nextToken = new Token(TokenType.VectorEnd, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == '{')
			{
				nextToken = new Token(TokenType.MapStart, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == '}')
			{
				nextToken = new Token(TokenType.MapEnd, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}
			else if (currentChar == ':')
			{
				_source.Push(currentChar);
				string keyword = ReadKeyword();
				nextToken = new Token(TokenType.Keyword, keyword, _source.CurrentIndex - keyword.Length, keyword.Length);
			}
			else if (IsString(currentChar, "#_"))
			{
				ReadChars(1);
				nextToken = new Token(TokenType.IgnoreReaderMacro, "#_", _source.CurrentIndex - 2, 2);
			}
			else if (BuiltInFunctions.Find(f => IsString(currentChar, f)) != null)
			{
				string match = BuiltInFunctions.Find(f => IsString(currentChar, f));
				ReadChars(match.Length-1);
				nextToken = new Token(TokenType.BuiltIn, match, _source.CurrentIndex - match.Length, match.Length);
			}
			else if (currentChar == '\\' && !IsNextCharWhitespace())
			{
				string character = ReadCharacter(currentChar);
				nextToken = new Token(TokenType.Character, character, _source.CurrentIndex - character.Length, character.Length);
			}
			else if (IsPrefix(currentChar, "0x"))
			{
				ReadChars(1);
				string number = "0x" + ReadNumber();
				nextToken = new Token(TokenType.HexNumber, number, _source.CurrentIndex - number.Length, number.Length);
			}
			else if (Char.IsNumber(currentChar))
			{
				_source.Push(currentChar);
				string number = ReadNumber();
				nextToken = new Token(TokenType.Number, number, _source.CurrentIndex - number.Length, number.Length);
			}
			else if (currentChar == '"')
			{
				_source.Push(currentChar);
				string str = ReadString();
				nextToken = new Token(TokenType.String, str, _source.CurrentIndex - str.Length, str.Length);
			}
			else if (IsWhitespace(currentChar))
			{
				_source.Push(currentChar);
				string str = ReadWhitespace();
				nextToken = new Token(TokenType.Whitespace, str, _source.CurrentIndex - str.Length, str.Length);
			}
			else if (currentChar == ';')
			{
				_source.Push(currentChar);
				string str = ReadComment();
				nextToken = new Token(TokenType.Comment, str, _source.CurrentIndex - str.Length, str.Length);
			}
			else if (IsString(currentChar, "true"))
			{
				ReadChars(3);
				nextToken = new Token(TokenType.Boolean, "true", _source.CurrentIndex - 4, 4);
			}
			else if (IsString(currentChar, "false"))
			{
				ReadChars(4);
				nextToken = new Token(TokenType.Boolean, "false", _source.CurrentIndex - 5, 5);
			}
			else if (IsString(currentChar, "nil"))
			{
				ReadChars(2);
				nextToken = new Token(TokenType.Nil, "nil", _source.CurrentIndex - 3, 3);
			}
			else if (IsSymbolPrefix(currentChar))
			{
				_source.Push(currentChar);
				string str = ReadSymbol();
				nextToken = new Token(TokenType.Symbol, str, _source.CurrentIndex - str.Length, str.Length);
			}
			else
			{
				nextToken = new Token(TokenType.Unknown, currentChar.ToString(), _source.CurrentIndex - 1, 1);
			}

			return nextToken;
		}

		private string ReadCharacter(char currentChar)
		{
			if (IsString(currentChar, "\\newline")) return currentChar + ReadChars(7);
			else if (IsString(currentChar, "\\space")) return currentChar + ReadChars(5);
			else if (IsString(currentChar, "\\tab")) return currentChar + ReadChars(3);

			string firstCharacter = ReadChars(1);

			if (firstCharacter == "u")
			{
				string nextFourCharacters = ReadChars(4);
				if (IsCharacterDefinedByHexDigits(nextFourCharacters)) return currentChar + firstCharacter + nextFourCharacters;
				_source.Push(nextFourCharacters);
			}

			return currentChar + firstCharacter;
		}

		private static bool IsCharacterDefinedByHexDigits(string str)
		{
			if (str.Length != 4) return false;
			string hexChars = "abcdefABCDEF0123456789";

			foreach (char c in str)
				if (!hexChars.Contains(c.ToString()))
					return false;

			return true;
		}

		private bool IsNextCharWhitespace()
		{
			string nextChar = ReadChars(1);
			bool nextCharIsWhitespace = string.IsNullOrEmpty(nextChar) && IsWhitespace(nextChar[0]);
			_source.Push(nextChar);
			return nextCharIsWhitespace;
		}

		private bool IsPrefix(char currentChar, string stringToMatch)
		{
			StringBuilder str = new StringBuilder(currentChar.ToString());
			str.Append(ReadChars(stringToMatch.Length - 1));
			string nextChar = ReadChars(1);

			if (nextChar.Length > 0 && !IsDataStructureStart(nextChar[0]) && !IsTerminatingChar(nextChar[0]))
			{
				_source.Push(nextChar);
				bool isMatch = str.ToString() == stringToMatch;
				_source.Push(str.ToString().Substring(1));
				return isMatch;
			}

			_source.Push(nextChar);
			_source.Push(str.ToString().Substring(1));
			return false;
		}

		private bool IsString(char currentChar, string stringToMatch)
		{
			StringBuilder str = new StringBuilder(currentChar.ToString());
			str.Append(ReadChars(stringToMatch.Length - 1));
			string nextChar = ReadChars(1);

			if (nextChar.Length == 0 || IsDataStructureStart(nextChar[0]) || IsTerminatingChar(nextChar[0]))
			{
				_source.Push(nextChar);
				bool isMatch = str.ToString() == stringToMatch;
				_source.Push(str.ToString().Substring(1));
				return isMatch;
			}

			_source.Push(nextChar);
			_source.Push(str.ToString().Substring(1));
			return false;
		}

		private string ReadChars(int charCount)
		{
			var chars = new StringBuilder();

			for (int i = 0; i < charCount; i++)
			{
				if (_source.HasMore) chars.Append(_source.Next());
				else return chars.ToString();
			}

			return chars.ToString();
		}

		private string ReadKeyword()
		{
			var parsedKeyword = new StringBuilder();
			char currentChar = _source.Next();

			while (!IsTerminatingChar(currentChar) && !IsDataStructureStart(currentChar))
			{
				parsedKeyword.Append(currentChar);
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedKeyword.ToString();
			}

			_source.Push(currentChar);
			return parsedKeyword.ToString();
		}

		private string ReadSymbol()
		{
			var parsedSymbol = new StringBuilder();
			char currentChar = _source.Next();

			while (!IsTerminatingChar(currentChar))
			{
				parsedSymbol.Append(currentChar);
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedSymbol.ToString();
			}

			_source.Push(currentChar);
			return parsedSymbol.ToString();
		}

		private string ReadComment()
		{
			return PutBackTrailingReturnCharacters(ReadToEndOfLineIncludingReturnCharacters());
		}

		private string PutBackTrailingReturnCharacters(string text)
		{
			for (int i=text.Length-1; i>=0; i--)
			{
				if (text[i] == '\r' || text[i] == '\n')
				{
					_source.Push(text[i]);
				}
				else
				{
					return text.Substring(0, i + 1);
				}
			}

			return string.Empty;
		}

		private string ReadToEndOfLineIncludingReturnCharacters()
		{
			var parsedLine = new StringBuilder();
			char currentChar = _source.Next();

			while (currentChar != '\r' && currentChar != '\n')
			{
				parsedLine.Append(currentChar);
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedLine.ToString();
			}

			parsedLine.Append(currentChar);

			if (currentChar == '\r')
			{
				currentChar = _source.Next();
				if (currentChar == '\n') parsedLine.Append(currentChar);
				else _source.Push(currentChar);
			}

			return parsedLine.ToString();
		}

		private string ReadWhitespace()
		{
			var parsedWhitespace = new StringBuilder();
			char currentChar = _source.Next();

			while (IsWhitespace(currentChar))
			{
				parsedWhitespace.Append(currentChar);
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedWhitespace.ToString();
			}

			_source.Push(currentChar);
			return parsedWhitespace.ToString();
		}

		private string ReadNumber()
		{
			var parsedNumber = new StringBuilder();
			char currentChar = _source.Next();

			while (!IsTerminatingChar(currentChar))
			{
				parsedNumber.Append(currentChar);
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedNumber.ToString();
			}

			_source.Push(currentChar);
			return parsedNumber.ToString();
		}

		private string ReadString()
		{
			var parsedString = new StringBuilder();
			char currentChar = _source.Next();
			parsedString.Append(currentChar);
			if (!_source.HasMore) return parsedString.ToString();
			currentChar = _source.Next();
			bool previousCharWasBackslash = false;

			while (currentChar != '"' || (currentChar == '"' && previousCharWasBackslash))
			{
				parsedString.Append(currentChar);
				previousCharWasBackslash = currentChar == '\\' && !previousCharWasBackslash;
				if (_source.HasMore) currentChar = _source.Next();
				else return parsedString.ToString();
			}

			parsedString.Append(currentChar);
			return parsedString.ToString();
		}

		private static bool IsWhitespace(char c)
		{
			return Char.IsWhiteSpace(c) || c == ',';
		}

		private static bool IsTerminatingChar(char c)
		{
			return c == ')' || c == '}' || c == ']' || IsWhitespace(c) || c == ';' || c == '"' || c == '\\';
		}

		private static bool IsDataStructureStart(char c)
		{
			return c == '(' || c == '{' || c == '[';
		}

		private static bool IsSymbolPrefix(char c)
		{
			return Char.IsLetter(c) || ValidNonLetterSymbolPrefixes.Contains(c);
		}
	}
}