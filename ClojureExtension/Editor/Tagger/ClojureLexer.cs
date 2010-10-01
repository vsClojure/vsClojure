// $ANTLR 3.2 Sep 23, 2009 12:02:23 C:\\Users\\Jon\\Desktop\\Clojure.g 2010-10-01 12:57:09

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


using System;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class ClojureLexer : Lexer {
    public const int SYNTAX_QUOTE = 33;
    public const int KEYWORD = 32;
    public const int SYMBOL = 28;
    public const int METADATA_TYPEHINT = 29;
    public const int SYMBOL_HEAD = 30;
    public const int NUMBER = 23;
    public const int AMPERSAND = 6;
    public const int OPEN_PAREN = 4;
    public const int COMMERCIAL_AT = 13;
    public const int EOF = -1;
    public const int SPACE = 37;
    public const int CHARACTER = 24;
    public const int RIGHT_CURLY_BRACKET = 10;
    public const int LEFT_SQUARE_BRACKET = 7;
    public const int RIGHT_SQUARE_BRACKET = 8;
    public const int LEFT_CURLY_BRACKET = 9;
    public const int NAME = 27;
    public const int BOOLEAN = 26;
    public const int NIL = 25;
    public const int UNQUOTE = 35;
    public const int UnicodeEscape = 20;
    public const int LAMBDA_ARG = 38;
    public const int NUMBER_SIGN = 14;
    public const int SPECIAL_FORM = 16;
    public const int CLOSE_PAREN = 5;
    public const int APOSTROPHE = 15;
    public const int SYMBOL_REST = 31;
    public const int REGEX_LITERAL = 19;
    public const int COMMENT = 36;
    public const int EscapeSequence = 17;
    public const int OctalEscape = 21;
    public const int CIRCUMFLEX = 12;
    public const int UNQUOTE_SPLICING = 34;
    public const int STRING = 18;
    public const int BACKSLASH = 11;
    public const int HEXDIGIT = 22;

    // delegates
    // delegators

    public ClojureLexer() 
    {
		InitializeCyclicDFAs();
    }
    public ClojureLexer(ICharStream input)
		: this(input, null) {
    }
    public ClojureLexer(ICharStream input, RecognizerSharedState state)
		: base(input, state) {
		InitializeCyclicDFAs(); 

    }
    
    override public string GrammarFileName
    {
    	get { return "C:\\Users\\Jon\\Desktop\\Clojure.g";} 
    }

    // $ANTLR start "OPEN_PAREN"
    public void mOPEN_PAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = OPEN_PAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:27:11: ( '(' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:27:13: '('
            {
            	Match('('); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OPEN_PAREN"

    // $ANTLR start "CLOSE_PAREN"
    public void mCLOSE_PAREN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CLOSE_PAREN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:29:12: ( ')' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:29:14: ')'
            {
            	Match(')'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CLOSE_PAREN"

    // $ANTLR start "AMPERSAND"
    public void mAMPERSAND() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = AMPERSAND;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:31:10: ( '&' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:31:12: '&'
            {
            	Match('&'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "AMPERSAND"

    // $ANTLR start "LEFT_SQUARE_BRACKET"
    public void mLEFT_SQUARE_BRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LEFT_SQUARE_BRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:33:20: ( '[' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:33:22: '['
            {
            	Match('['); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LEFT_SQUARE_BRACKET"

    // $ANTLR start "RIGHT_SQUARE_BRACKET"
    public void mRIGHT_SQUARE_BRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RIGHT_SQUARE_BRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:35:21: ( ']' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:35:23: ']'
            {
            	Match(']'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RIGHT_SQUARE_BRACKET"

    // $ANTLR start "LEFT_CURLY_BRACKET"
    public void mLEFT_CURLY_BRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LEFT_CURLY_BRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:37:19: ( '{' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:37:21: '{'
            {
            	Match('{'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LEFT_CURLY_BRACKET"

    // $ANTLR start "RIGHT_CURLY_BRACKET"
    public void mRIGHT_CURLY_BRACKET() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = RIGHT_CURLY_BRACKET;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:39:20: ( '}' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:39:22: '}'
            {
            	Match('}'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "RIGHT_CURLY_BRACKET"

    // $ANTLR start "BACKSLASH"
    public void mBACKSLASH() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BACKSLASH;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:41:10: ( '\\\\' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:41:12: '\\\\'
            {
            	Match('\\'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BACKSLASH"

    // $ANTLR start "CIRCUMFLEX"
    public void mCIRCUMFLEX() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CIRCUMFLEX;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:43:11: ( '^' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:43:13: '^'
            {
            	Match('^'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CIRCUMFLEX"

    // $ANTLR start "COMMERCIAL_AT"
    public void mCOMMERCIAL_AT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMERCIAL_AT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:45:14: ( '@' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:45:16: '@'
            {
            	Match('@'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMERCIAL_AT"

    // $ANTLR start "NUMBER_SIGN"
    public void mNUMBER_SIGN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NUMBER_SIGN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:47:12: ( '#' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:47:14: '#'
            {
            	Match('#'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NUMBER_SIGN"

    // $ANTLR start "APOSTROPHE"
    public void mAPOSTROPHE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = APOSTROPHE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:49:11: ( '\\'' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:49:13: '\\''
            {
            	Match('\''); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "APOSTROPHE"

    // $ANTLR start "SPECIAL_FORM"
    public void mSPECIAL_FORM() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SPECIAL_FORM;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:53:13: ( 'def' | 'if' | 'do' | 'let' | 'quote' | 'var' | 'fn' | 'loop' | 'recur' | 'throw' | 'try' | 'monitor-enter' | 'monitor-exit' | 'new' | 'set!' | '.' )
            int alt1 = 16;
            alt1 = dfa1.Predict(input);
            switch (alt1) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:15: 'def'
                    {
                    	Match("def"); 


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:23: 'if'
                    {
                    	Match("if"); 


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:30: 'do'
                    {
                    	Match("do"); 


                    }
                    break;
                case 4 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:37: 'let'
                    {
                    	Match("let"); 


                    }
                    break;
                case 5 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:45: 'quote'
                    {
                    	Match("quote"); 


                    }
                    break;
                case 6 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:55: 'var'
                    {
                    	Match("var"); 


                    }
                    break;
                case 7 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:63: 'fn'
                    {
                    	Match("fn"); 


                    }
                    break;
                case 8 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:53:70: 'loop'
                    {
                    	Match("loop"); 


                    }
                    break;
                case 9 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:54:13: 'recur'
                    {
                    	Match("recur"); 


                    }
                    break;
                case 10 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:54:23: 'throw'
                    {
                    	Match("throw"); 


                    }
                    break;
                case 11 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:54:33: 'try'
                    {
                    	Match("try"); 


                    }
                    break;
                case 12 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:54:41: 'monitor-enter'
                    {
                    	Match("monitor-enter"); 


                    }
                    break;
                case 13 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:54:59: 'monitor-exit'
                    {
                    	Match("monitor-exit"); 


                    }
                    break;
                case 14 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:55:13: 'new'
                    {
                    	Match("new"); 


                    }
                    break;
                case 15 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:55:21: 'set!'
                    {
                    	Match("set!"); 


                    }
                    break;
                case 16 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:55:30: '.'
                    {
                    	Match('.'); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SPECIAL_FORM"

    // $ANTLR start "STRING"
    public void mSTRING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = STRING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:60:5: ( '\"' ( EscapeSequence | ~ ( '\\\\' | '\"' ) )* '\"' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:60:8: '\"' ( EscapeSequence | ~ ( '\\\\' | '\"' ) )* '\"'
            {
            	Match('\"'); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:60:12: ( EscapeSequence | ~ ( '\\\\' | '\"' ) )*
            	do 
            	{
            	    int alt2 = 3;
            	    int LA2_0 = input.LA(1);

            	    if ( (LA2_0 == '\\') )
            	    {
            	        alt2 = 1;
            	    }
            	    else if ( ((LA2_0 >= '\u0000' && LA2_0 <= '!') || (LA2_0 >= '#' && LA2_0 <= '[') || (LA2_0 >= ']' && LA2_0 <= '\uFFFF')) )
            	    {
            	        alt2 = 2;
            	    }


            	    switch (alt2) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:60:14: EscapeSequence
            			    {
            			    	mEscapeSequence(); 

            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:60:31: ~ ( '\\\\' | '\"' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop2;
            	    }
            	} while (true);

            	loop2:
            		;	// Stops C# compiler whining that label 'loop2' has no statements

            	Match('\"'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "STRING"

    // $ANTLR start "REGEX_LITERAL"
    public void mREGEX_LITERAL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = REGEX_LITERAL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:64:5: ( NUMBER_SIGN '\"' (~ ( '\\\\' | '\"' ) | '\\\\' . )* '\"' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:64:7: NUMBER_SIGN '\"' (~ ( '\\\\' | '\"' ) | '\\\\' . )* '\"'
            {
            	mNUMBER_SIGN(); 
            	Match('\"'); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:64:23: (~ ( '\\\\' | '\"' ) | '\\\\' . )*
            	do 
            	{
            	    int alt3 = 3;
            	    int LA3_0 = input.LA(1);

            	    if ( ((LA3_0 >= '\u0000' && LA3_0 <= '!') || (LA3_0 >= '#' && LA3_0 <= '[') || (LA3_0 >= ']' && LA3_0 <= '\uFFFF')) )
            	    {
            	        alt3 = 1;
            	    }
            	    else if ( (LA3_0 == '\\') )
            	    {
            	        alt3 = 2;
            	    }


            	    switch (alt3) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:64:25: ~ ( '\\\\' | '\"' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '!') || (input.LA(1) >= '#' && input.LA(1) <= '[') || (input.LA(1) >= ']' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:64:41: '\\\\' .
            			    {
            			    	Match('\\'); 
            			    	MatchAny(); 

            			    }
            			    break;

            			default:
            			    goto loop3;
            	    }
            	} while (true);

            	loop3:
            		;	// Stops C# compiler whining that label 'loop3' has no statements

            	Match('\"'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "REGEX_LITERAL"

    // $ANTLR start "EscapeSequence"
    public void mEscapeSequence() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:70:5: ( '\\\\' ( 'b' | 't' | 'n' | 'f' | 'r' | '\\\"' | '\\'' | '\\\\' ) | UnicodeEscape | OctalEscape )
            int alt4 = 3;
            int LA4_0 = input.LA(1);

            if ( (LA4_0 == '\\') )
            {
                switch ( input.LA(2) ) 
                {
                case '\"':
                case '\'':
                case '\\':
                case 'b':
                case 'f':
                case 'n':
                case 'r':
                case 't':
                	{
                    alt4 = 1;
                    }
                    break;
                case 'u':
                	{
                    alt4 = 2;
                    }
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                	{
                    alt4 = 3;
                    }
                    break;
                	default:
                	    NoViableAltException nvae_d4s1 =
                	        new NoViableAltException("", 4, 1, input);

                	    throw nvae_d4s1;
                }

            }
            else 
            {
                NoViableAltException nvae_d4s0 =
                    new NoViableAltException("", 4, 0, input);

                throw nvae_d4s0;
            }
            switch (alt4) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:70:9: '\\\\' ( 'b' | 't' | 'n' | 'f' | 'r' | '\\\"' | '\\'' | '\\\\' )
                    {
                    	Match('\\'); 
                    	if ( input.LA(1) == '\"' || input.LA(1) == '\'' || input.LA(1) == '\\' || input.LA(1) == 'b' || input.LA(1) == 'f' || input.LA(1) == 'n' || input.LA(1) == 'r' || input.LA(1) == 't' ) 
                    	{
                    	    input.Consume();

                    	}
                    	else 
                    	{
                    	    MismatchedSetException mse = new MismatchedSetException(null,input);
                    	    Recover(mse);
                    	    throw mse;}


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:71:9: UnicodeEscape
                    {
                    	mUnicodeEscape(); 

                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:72:9: OctalEscape
                    {
                    	mOctalEscape(); 

                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "EscapeSequence"

    // $ANTLR start "UnicodeEscape"
    public void mUnicodeEscape() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:78:5: ( '\\\\' 'u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:78:9: '\\\\' 'u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT
            {
            	Match('\\'); 
            	Match('u'); 
            	mHEXDIGIT(); 
            	mHEXDIGIT(); 
            	mHEXDIGIT(); 
            	mHEXDIGIT(); 

            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "UnicodeEscape"

    // $ANTLR start "OctalEscape"
    public void mOctalEscape() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:84:5: ( '\\\\' ( '0' .. '3' ) ( '0' .. '7' ) ( '0' .. '7' ) | '\\\\' ( '0' .. '7' ) ( '0' .. '7' ) | '\\\\' ( '0' .. '7' ) )
            int alt5 = 3;
            int LA5_0 = input.LA(1);

            if ( (LA5_0 == '\\') )
            {
                int LA5_1 = input.LA(2);

                if ( ((LA5_1 >= '0' && LA5_1 <= '3')) )
                {
                    int LA5_2 = input.LA(3);

                    if ( ((LA5_2 >= '0' && LA5_2 <= '7')) )
                    {
                        int LA5_4 = input.LA(4);

                        if ( ((LA5_4 >= '0' && LA5_4 <= '7')) )
                        {
                            alt5 = 1;
                        }
                        else 
                        {
                            alt5 = 2;}
                    }
                    else 
                    {
                        alt5 = 3;}
                }
                else if ( ((LA5_1 >= '4' && LA5_1 <= '7')) )
                {
                    int LA5_3 = input.LA(3);

                    if ( ((LA5_3 >= '0' && LA5_3 <= '7')) )
                    {
                        alt5 = 2;
                    }
                    else 
                    {
                        alt5 = 3;}
                }
                else 
                {
                    NoViableAltException nvae_d5s1 =
                        new NoViableAltException("", 5, 1, input);

                    throw nvae_d5s1;
                }
            }
            else 
            {
                NoViableAltException nvae_d5s0 =
                    new NoViableAltException("", 5, 0, input);

                throw nvae_d5s0;
            }
            switch (alt5) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:84:9: '\\\\' ( '0' .. '3' ) ( '0' .. '7' ) ( '0' .. '7' )
                    {
                    	Match('\\'); 
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:14: ( '0' .. '3' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:15: '0' .. '3'
                    	{
                    		MatchRange('0','3'); 

                    	}

                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:25: ( '0' .. '7' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:26: '0' .. '7'
                    	{
                    		MatchRange('0','7'); 

                    	}

                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:36: ( '0' .. '7' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:84:37: '0' .. '7'
                    	{
                    		MatchRange('0','7'); 

                    	}


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:85:9: '\\\\' ( '0' .. '7' ) ( '0' .. '7' )
                    {
                    	Match('\\'); 
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:85:14: ( '0' .. '7' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:85:15: '0' .. '7'
                    	{
                    		MatchRange('0','7'); 

                    	}

                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:85:25: ( '0' .. '7' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:85:26: '0' .. '7'
                    	{
                    		MatchRange('0','7'); 

                    	}


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:86:9: '\\\\' ( '0' .. '7' )
                    {
                    	Match('\\'); 
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:86:14: ( '0' .. '7' )
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:86:15: '0' .. '7'
                    	{
                    		MatchRange('0','7'); 

                    	}


                    }
                    break;

            }
        }
        finally 
    	{
        }
    }
    // $ANTLR end "OctalEscape"

    // $ANTLR start "NUMBER"
    public void mNUMBER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NUMBER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:91:7: ( ( '-' )? ( '0' .. '9' )+ ( '.' ( '0' .. '9' )+ )? ( ( 'e' | 'E' ) ( '-' )? ( '0' .. '9' )+ )? )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:91:9: ( '-' )? ( '0' .. '9' )+ ( '.' ( '0' .. '9' )+ )? ( ( 'e' | 'E' ) ( '-' )? ( '0' .. '9' )+ )?
            {
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:9: ( '-' )?
            	int alt6 = 2;
            	int LA6_0 = input.LA(1);

            	if ( (LA6_0 == '-') )
            	{
            	    alt6 = 1;
            	}
            	switch (alt6) 
            	{
            	    case 1 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:91:9: '-'
            	        {
            	        	Match('-'); 

            	        }
            	        break;

            	}

            	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:14: ( '0' .. '9' )+
            	int cnt7 = 0;
            	do 
            	{
            	    int alt7 = 2;
            	    int LA7_0 = input.LA(1);

            	    if ( ((LA7_0 >= '0' && LA7_0 <= '9')) )
            	    {
            	        alt7 = 1;
            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:91:14: '0' .. '9'
            			    {
            			    	MatchRange('0','9'); 

            			    }
            			    break;

            			default:
            			    if ( cnt7 >= 1 ) goto loop7;
            		            EarlyExitException eee7 =
            		                new EarlyExitException(7, input);
            		            throw eee7;
            	    }
            	    cnt7++;
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whining that label 'loop7' has no statements

            	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:24: ( '.' ( '0' .. '9' )+ )?
            	int alt9 = 2;
            	int LA9_0 = input.LA(1);

            	if ( (LA9_0 == '.') )
            	{
            	    alt9 = 1;
            	}
            	switch (alt9) 
            	{
            	    case 1 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:91:25: '.' ( '0' .. '9' )+
            	        {
            	        	Match('.'); 
            	        	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:29: ( '0' .. '9' )+
            	        	int cnt8 = 0;
            	        	do 
            	        	{
            	        	    int alt8 = 2;
            	        	    int LA8_0 = input.LA(1);

            	        	    if ( ((LA8_0 >= '0' && LA8_0 <= '9')) )
            	        	    {
            	        	        alt8 = 1;
            	        	    }


            	        	    switch (alt8) 
            	        		{
            	        			case 1 :
            	        			    // C:\\Users\\Jon\\Desktop\\Clojure.g:91:29: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    if ( cnt8 >= 1 ) goto loop8;
            	        		            EarlyExitException eee8 =
            	        		                new EarlyExitException(8, input);
            	        		            throw eee8;
            	        	    }
            	        	    cnt8++;
            	        	} while (true);

            	        	loop8:
            	        		;	// Stops C# compiler whining that label 'loop8' has no statements


            	        }
            	        break;

            	}

            	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:41: ( ( 'e' | 'E' ) ( '-' )? ( '0' .. '9' )+ )?
            	int alt12 = 2;
            	int LA12_0 = input.LA(1);

            	if ( (LA12_0 == 'E' || LA12_0 == 'e') )
            	{
            	    alt12 = 1;
            	}
            	switch (alt12) 
            	{
            	    case 1 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:91:42: ( 'e' | 'E' ) ( '-' )? ( '0' .. '9' )+
            	        {
            	        	if ( input.LA(1) == 'E' || input.LA(1) == 'e' ) 
            	        	{
            	        	    input.Consume();

            	        	}
            	        	else 
            	        	{
            	        	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	        	    Recover(mse);
            	        	    throw mse;}

            	        	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:52: ( '-' )?
            	        	int alt10 = 2;
            	        	int LA10_0 = input.LA(1);

            	        	if ( (LA10_0 == '-') )
            	        	{
            	        	    alt10 = 1;
            	        	}
            	        	switch (alt10) 
            	        	{
            	        	    case 1 :
            	        	        // C:\\Users\\Jon\\Desktop\\Clojure.g:91:52: '-'
            	        	        {
            	        	        	Match('-'); 

            	        	        }
            	        	        break;

            	        	}

            	        	// C:\\Users\\Jon\\Desktop\\Clojure.g:91:57: ( '0' .. '9' )+
            	        	int cnt11 = 0;
            	        	do 
            	        	{
            	        	    int alt11 = 2;
            	        	    int LA11_0 = input.LA(1);

            	        	    if ( ((LA11_0 >= '0' && LA11_0 <= '9')) )
            	        	    {
            	        	        alt11 = 1;
            	        	    }


            	        	    switch (alt11) 
            	        		{
            	        			case 1 :
            	        			    // C:\\Users\\Jon\\Desktop\\Clojure.g:91:57: '0' .. '9'
            	        			    {
            	        			    	MatchRange('0','9'); 

            	        			    }
            	        			    break;

            	        			default:
            	        			    if ( cnt11 >= 1 ) goto loop11;
            	        		            EarlyExitException eee11 =
            	        		                new EarlyExitException(11, input);
            	        		            throw eee11;
            	        	    }
            	        	    cnt11++;
            	        	} while (true);

            	        	loop11:
            	        		;	// Stops C# compiler whining that label 'loop11' has no statements


            	        }
            	        break;

            	}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NUMBER"

    // $ANTLR start "CHARACTER"
    public void mCHARACTER() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = CHARACTER;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:94:10: ( '\\\\newline' | '\\\\space' | '\\\\tab' | '\\\\u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT | BACKSLASH . )
            int alt13 = 5;
            alt13 = dfa13.Predict(input);
            switch (alt13) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:95:9: '\\\\newline'
                    {
                    	Match("\\newline"); 


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:96:9: '\\\\space'
                    {
                    	Match("\\space"); 


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:97:9: '\\\\tab'
                    {
                    	Match("\\tab"); 


                    }
                    break;
                case 4 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:98:9: '\\\\u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT
                    {
                    	Match("\\u"); 

                    	mHEXDIGIT(); 
                    	mHEXDIGIT(); 
                    	mHEXDIGIT(); 
                    	mHEXDIGIT(); 

                    }
                    break;
                case 5 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:99:9: BACKSLASH .
                    {
                    	mBACKSLASH(); 
                    	MatchAny(); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "CHARACTER"

    // $ANTLR start "HEXDIGIT"
    public void mHEXDIGIT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = HEXDIGIT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:102:9: ( '0' .. '9' | 'a' .. 'f' | 'A' .. 'F' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:
            {
            	if ( (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= 'A' && input.LA(1) <= 'F') || (input.LA(1) >= 'a' && input.LA(1) <= 'f') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "HEXDIGIT"

    // $ANTLR start "NIL"
    public void mNIL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = NIL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:105:4: ( 'nil' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:105:9: 'nil'
            {
            	Match("nil"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "NIL"

    // $ANTLR start "BOOLEAN"
    public void mBOOLEAN() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = BOOLEAN;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:108:8: ( 'true' | 'false' )
            int alt14 = 2;
            int LA14_0 = input.LA(1);

            if ( (LA14_0 == 't') )
            {
                alt14 = 1;
            }
            else if ( (LA14_0 == 'f') )
            {
                alt14 = 2;
            }
            else 
            {
                NoViableAltException nvae_d14s0 =
                    new NoViableAltException("", 14, 0, input);

                throw nvae_d14s0;
            }
            switch (alt14) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:109:9: 'true'
                    {
                    	Match("true"); 


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:110:9: 'false'
                    {
                    	Match("false"); 


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "BOOLEAN"

    // $ANTLR start "SYMBOL"
    public void mSYMBOL() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SYMBOL;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:113:7: ( '/' | NAME ( '/' NAME )? )
            int alt16 = 2;
            int LA16_0 = input.LA(1);

            if ( (LA16_0 == '/') )
            {
                alt16 = 1;
            }
            else if ( (LA16_0 == '!' || LA16_0 == '$' || (LA16_0 >= '*' && LA16_0 <= '+') || LA16_0 == '-' || (LA16_0 >= '<' && LA16_0 <= '?') || (LA16_0 >= 'A' && LA16_0 <= 'Z') || LA16_0 == '_' || (LA16_0 >= 'a' && LA16_0 <= 'z')) )
            {
                alt16 = 2;
            }
            else 
            {
                NoViableAltException nvae_d16s0 =
                    new NoViableAltException("", 16, 0, input);

                throw nvae_d16s0;
            }
            switch (alt16) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:114:9: '/'
                    {
                    	Match('/'); 

                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:115:9: NAME ( '/' NAME )?
                    {
                    	mNAME(); 
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:115:14: ( '/' NAME )?
                    	int alt15 = 2;
                    	int LA15_0 = input.LA(1);

                    	if ( (LA15_0 == '/') )
                    	{
                    	    alt15 = 1;
                    	}
                    	switch (alt15) 
                    	{
                    	    case 1 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:115:15: '/' NAME
                    	        {
                    	        	Match('/'); 
                    	        	mNAME(); 

                    	        }
                    	        break;

                    	}


                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SYMBOL"

    // $ANTLR start "METADATA_TYPEHINT"
    public void mMETADATA_TYPEHINT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = METADATA_TYPEHINT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:118:18: ( ( NUMBER_SIGN )* CIRCUMFLEX ( 'ints' | 'floats' | 'longs' | 'doubles' | 'objects' | NAME | STRING )* )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:119:3: ( NUMBER_SIGN )* CIRCUMFLEX ( 'ints' | 'floats' | 'longs' | 'doubles' | 'objects' | NAME | STRING )*
            {
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:119:3: ( NUMBER_SIGN )*
            	do 
            	{
            	    int alt17 = 2;
            	    int LA17_0 = input.LA(1);

            	    if ( (LA17_0 == '#') )
            	    {
            	        alt17 = 1;
            	    }


            	    switch (alt17) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:3: NUMBER_SIGN
            			    {
            			    	mNUMBER_SIGN(); 

            			    }
            			    break;

            			default:
            			    goto loop17;
            	    }
            	} while (true);

            	loop17:
            		;	// Stops C# compiler whining that label 'loop17' has no statements

            	mCIRCUMFLEX(); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:119:27: ( 'ints' | 'floats' | 'longs' | 'doubles' | 'objects' | NAME | STRING )*
            	do 
            	{
            	    int alt18 = 8;
            	    alt18 = dfa18.Predict(input);
            	    switch (alt18) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:29: 'ints'
            			    {
            			    	Match("ints"); 


            			    }
            			    break;
            			case 2 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:38: 'floats'
            			    {
            			    	Match("floats"); 


            			    }
            			    break;
            			case 3 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:49: 'longs'
            			    {
            			    	Match("longs"); 


            			    }
            			    break;
            			case 4 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:59: 'doubles'
            			    {
            			    	Match("doubles"); 


            			    }
            			    break;
            			case 5 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:71: 'objects'
            			    {
            			    	Match("objects"); 


            			    }
            			    break;
            			case 6 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:83: NAME
            			    {
            			    	mNAME(); 

            			    }
            			    break;
            			case 7 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:119:90: STRING
            			    {
            			    	mSTRING(); 

            			    }
            			    break;

            			default:
            			    goto loop18;
            	    }
            	} while (true);

            	loop18:
            		;	// Stops C# compiler whining that label 'loop18' has no statements


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "METADATA_TYPEHINT"

    // $ANTLR start "NAME"
    public void mNAME() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:123:5: ( SYMBOL_HEAD ( SYMBOL_REST )* ( ':' ( SYMBOL_REST )+ )* )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:123:9: SYMBOL_HEAD ( SYMBOL_REST )* ( ':' ( SYMBOL_REST )+ )*
            {
            	mSYMBOL_HEAD(); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:123:21: ( SYMBOL_REST )*
            	do 
            	{
            	    int alt19 = 2;
            	    int LA19_0 = input.LA(1);

            	    if ( (LA19_0 == '!' || (LA19_0 >= '#' && LA19_0 <= '$') || (LA19_0 >= '*' && LA19_0 <= '+') || (LA19_0 >= '-' && LA19_0 <= '.') || (LA19_0 >= '0' && LA19_0 <= '9') || (LA19_0 >= '<' && LA19_0 <= '?') || (LA19_0 >= 'A' && LA19_0 <= 'Z') || LA19_0 == '_' || (LA19_0 >= 'a' && LA19_0 <= 'z')) )
            	    {
            	        alt19 = 1;
            	    }


            	    switch (alt19) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:123:21: SYMBOL_REST
            			    {
            			    	mSYMBOL_REST(); 

            			    }
            			    break;

            			default:
            			    goto loop19;
            	    }
            	} while (true);

            	loop19:
            		;	// Stops C# compiler whining that label 'loop19' has no statements

            	// C:\\Users\\Jon\\Desktop\\Clojure.g:123:34: ( ':' ( SYMBOL_REST )+ )*
            	do 
            	{
            	    int alt21 = 2;
            	    int LA21_0 = input.LA(1);

            	    if ( (LA21_0 == ':') )
            	    {
            	        alt21 = 1;
            	    }


            	    switch (alt21) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:123:35: ':' ( SYMBOL_REST )+
            			    {
            			    	Match(':'); 
            			    	// C:\\Users\\Jon\\Desktop\\Clojure.g:123:39: ( SYMBOL_REST )+
            			    	int cnt20 = 0;
            			    	do 
            			    	{
            			    	    int alt20 = 2;
            			    	    int LA20_0 = input.LA(1);

            			    	    if ( (LA20_0 == '!' || (LA20_0 >= '#' && LA20_0 <= '$') || (LA20_0 >= '*' && LA20_0 <= '+') || (LA20_0 >= '-' && LA20_0 <= '.') || (LA20_0 >= '0' && LA20_0 <= '9') || (LA20_0 >= '<' && LA20_0 <= '?') || (LA20_0 >= 'A' && LA20_0 <= 'Z') || LA20_0 == '_' || (LA20_0 >= 'a' && LA20_0 <= 'z')) )
            			    	    {
            			    	        alt20 = 1;
            			    	    }


            			    	    switch (alt20) 
            			    		{
            			    			case 1 :
            			    			    // C:\\Users\\Jon\\Desktop\\Clojure.g:123:39: SYMBOL_REST
            			    			    {
            			    			    	mSYMBOL_REST(); 

            			    			    }
            			    			    break;

            			    			default:
            			    			    if ( cnt20 >= 1 ) goto loop20;
            			    		            EarlyExitException eee20 =
            			    		                new EarlyExitException(20, input);
            			    		            throw eee20;
            			    	    }
            			    	    cnt20++;
            			    	} while (true);

            			    	loop20:
            			    		;	// Stops C# compiler whining that label 'loop20' has no statements


            			    }
            			    break;

            			default:
            			    goto loop21;
            	    }
            	} while (true);

            	loop21:
            		;	// Stops C# compiler whining that label 'loop21' has no statements


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "NAME"

    // $ANTLR start "SYMBOL_HEAD"
    public void mSYMBOL_HEAD() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:127:12: ( 'a' .. 'z' | 'A' .. 'Z' | '*' | '+' | '!' | '-' | '_' | '?' | '>' | '<' | '=' | '$' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:
            {
            	if ( input.LA(1) == '!' || input.LA(1) == '$' || (input.LA(1) >= '*' && input.LA(1) <= '+') || input.LA(1) == '-' || (input.LA(1) >= '<' && input.LA(1) <= '?') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "SYMBOL_HEAD"

    // $ANTLR start "SYMBOL_REST"
    public void mSYMBOL_REST() // throws RecognitionException [2]
    {
    		try
    		{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:133:12: ( SYMBOL_HEAD | '0' .. '9' | '.' | NUMBER_SIGN )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:
            {
            	if ( input.LA(1) == '!' || (input.LA(1) >= '#' && input.LA(1) <= '$') || (input.LA(1) >= '*' && input.LA(1) <= '+') || (input.LA(1) >= '-' && input.LA(1) <= '.') || (input.LA(1) >= '0' && input.LA(1) <= '9') || (input.LA(1) >= '<' && input.LA(1) <= '?') || (input.LA(1) >= 'A' && input.LA(1) <= 'Z') || input.LA(1) == '_' || (input.LA(1) >= 'a' && input.LA(1) <= 'z') ) 
            	{
            	    input.Consume();

            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    Recover(mse);
            	    throw mse;}


            }

        }
        finally 
    	{
        }
    }
    // $ANTLR end "SYMBOL_REST"

    // $ANTLR start "KEYWORD"
    public void mKEYWORD() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = KEYWORD;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:149:8: ( ':' SYMBOL )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:150:9: ':' SYMBOL
            {
            	Match(':'); 
            	mSYMBOL(); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "KEYWORD"

    // $ANTLR start "SYNTAX_QUOTE"
    public void mSYNTAX_QUOTE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SYNTAX_QUOTE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:153:13: ( '`' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:154:9: '`'
            {
            	Match('`'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SYNTAX_QUOTE"

    // $ANTLR start "UNQUOTE_SPLICING"
    public void mUNQUOTE_SPLICING() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNQUOTE_SPLICING;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:157:17: ( '~@' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:158:9: '~@'
            {
            	Match("~@"); 


            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "UNQUOTE_SPLICING"

    // $ANTLR start "UNQUOTE"
    public void mUNQUOTE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = UNQUOTE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:161:8: ( '~' )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:162:9: '~'
            {
            	Match('~'); 

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "UNQUOTE"

    // $ANTLR start "COMMENT"
    public void mCOMMENT() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = COMMENT;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:165:8: ( ';' (~ ( '\\r' | '\\n' ) )* ( ( '\\r' )? '\\n' )? )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:166:9: ';' (~ ( '\\r' | '\\n' ) )* ( ( '\\r' )? '\\n' )?
            {
            	Match(';'); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:166:13: (~ ( '\\r' | '\\n' ) )*
            	do 
            	{
            	    int alt22 = 2;
            	    int LA22_0 = input.LA(1);

            	    if ( ((LA22_0 >= '\u0000' && LA22_0 <= '\t') || (LA22_0 >= '\u000B' && LA22_0 <= '\f') || (LA22_0 >= '\u000E' && LA22_0 <= '\uFFFF')) )
            	    {
            	        alt22 = 1;
            	    }


            	    switch (alt22) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:166:13: ~ ( '\\r' | '\\n' )
            			    {
            			    	if ( (input.LA(1) >= '\u0000' && input.LA(1) <= '\t') || (input.LA(1) >= '\u000B' && input.LA(1) <= '\f') || (input.LA(1) >= '\u000E' && input.LA(1) <= '\uFFFF') ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    goto loop22;
            	    }
            	} while (true);

            	loop22:
            		;	// Stops C# compiler whining that label 'loop22' has no statements

            	// C:\\Users\\Jon\\Desktop\\Clojure.g:166:29: ( ( '\\r' )? '\\n' )?
            	int alt24 = 2;
            	int LA24_0 = input.LA(1);

            	if ( (LA24_0 == '\n' || LA24_0 == '\r') )
            	{
            	    alt24 = 1;
            	}
            	switch (alt24) 
            	{
            	    case 1 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:166:30: ( '\\r' )? '\\n'
            	        {
            	        	// C:\\Users\\Jon\\Desktop\\Clojure.g:166:30: ( '\\r' )?
            	        	int alt23 = 2;
            	        	int LA23_0 = input.LA(1);

            	        	if ( (LA23_0 == '\r') )
            	        	{
            	        	    alt23 = 1;
            	        	}
            	        	switch (alt23) 
            	        	{
            	        	    case 1 :
            	        	        // C:\\Users\\Jon\\Desktop\\Clojure.g:166:30: '\\r'
            	        	        {
            	        	        	Match('\r'); 

            	        	        }
            	        	        break;

            	        	}

            	        	Match('\n'); 

            	        }
            	        break;

            	}

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "COMMENT"

    // $ANTLR start "SPACE"
    public void mSPACE() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = SPACE;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:169:6: ( ( ' ' | '\\t' | ',' | '\\r' | '\\n' )+ )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:169:9: ( ' ' | '\\t' | ',' | '\\r' | '\\n' )+
            {
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:169:9: ( ' ' | '\\t' | ',' | '\\r' | '\\n' )+
            	int cnt25 = 0;
            	do 
            	{
            	    int alt25 = 2;
            	    int LA25_0 = input.LA(1);

            	    if ( ((LA25_0 >= '\t' && LA25_0 <= '\n') || LA25_0 == '\r' || LA25_0 == ' ' || LA25_0 == ',') )
            	    {
            	        alt25 = 1;
            	    }


            	    switch (alt25) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:
            			    {
            			    	if ( (input.LA(1) >= '\t' && input.LA(1) <= '\n') || input.LA(1) == '\r' || input.LA(1) == ' ' || input.LA(1) == ',' ) 
            			    	{
            			    	    input.Consume();

            			    	}
            			    	else 
            			    	{
            			    	    MismatchedSetException mse = new MismatchedSetException(null,input);
            			    	    Recover(mse);
            			    	    throw mse;}


            			    }
            			    break;

            			default:
            			    if ( cnt25 >= 1 ) goto loop25;
            		            EarlyExitException eee25 =
            		                new EarlyExitException(25, input);
            		            throw eee25;
            	    }
            	    cnt25++;
            	} while (true);

            	loop25:
            		;	// Stops C# compiler whining that label 'loop25' has no statements

            	_channel=HIDDEN;

            }

            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "SPACE"

    // $ANTLR start "LAMBDA_ARG"
    public void mLAMBDA_ARG() // throws RecognitionException [2]
    {
    		try
    		{
            int _type = LAMBDA_ARG;
    	int _channel = DEFAULT_TOKEN_CHANNEL;
            // C:\\Users\\Jon\\Desktop\\Clojure.g:173:11: ( '%' '1' .. '9' ( '0' .. '9' )* | '%&' | '%' )
            int alt27 = 3;
            int LA27_0 = input.LA(1);

            if ( (LA27_0 == '%') )
            {
                switch ( input.LA(2) ) 
                {
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                	{
                    alt27 = 1;
                    }
                    break;
                case '&':
                	{
                    alt27 = 2;
                    }
                    break;
                	default:
                    	alt27 = 3;
                    	break;}

            }
            else 
            {
                NoViableAltException nvae_d27s0 =
                    new NoViableAltException("", 27, 0, input);

                throw nvae_d27s0;
            }
            switch (alt27) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:174:9: '%' '1' .. '9' ( '0' .. '9' )*
                    {
                    	Match('%'); 
                    	MatchRange('1','9'); 
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:174:22: ( '0' .. '9' )*
                    	do 
                    	{
                    	    int alt26 = 2;
                    	    int LA26_0 = input.LA(1);

                    	    if ( ((LA26_0 >= '0' && LA26_0 <= '9')) )
                    	    {
                    	        alt26 = 1;
                    	    }


                    	    switch (alt26) 
                    		{
                    			case 1 :
                    			    // C:\\Users\\Jon\\Desktop\\Clojure.g:174:22: '0' .. '9'
                    			    {
                    			    	MatchRange('0','9'); 

                    			    }
                    			    break;

                    			default:
                    			    goto loop26;
                    	    }
                    	} while (true);

                    	loop26:
                    		;	// Stops C# compiler whining that label 'loop26' has no statements


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:175:9: '%&'
                    {
                    	Match("%&"); 


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:176:9: '%'
                    {
                    	Match('%'); 

                    }
                    break;

            }
            state.type = _type;
            state.channel = _channel;
        }
        finally 
    	{
        }
    }
    // $ANTLR end "LAMBDA_ARG"

    override public void mTokens() // throws RecognitionException 
    {
        // C:\\Users\\Jon\\Desktop\\Clojure.g:1:8: ( OPEN_PAREN | CLOSE_PAREN | AMPERSAND | LEFT_SQUARE_BRACKET | RIGHT_SQUARE_BRACKET | LEFT_CURLY_BRACKET | RIGHT_CURLY_BRACKET | BACKSLASH | CIRCUMFLEX | COMMERCIAL_AT | NUMBER_SIGN | APOSTROPHE | SPECIAL_FORM | STRING | REGEX_LITERAL | NUMBER | CHARACTER | HEXDIGIT | NIL | BOOLEAN | SYMBOL | METADATA_TYPEHINT | KEYWORD | SYNTAX_QUOTE | UNQUOTE_SPLICING | UNQUOTE | COMMENT | SPACE | LAMBDA_ARG )
        int alt28 = 29;
        alt28 = dfa28.Predict(input);
        switch (alt28) 
        {
            case 1 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:10: OPEN_PAREN
                {
                	mOPEN_PAREN(); 

                }
                break;
            case 2 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:21: CLOSE_PAREN
                {
                	mCLOSE_PAREN(); 

                }
                break;
            case 3 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:33: AMPERSAND
                {
                	mAMPERSAND(); 

                }
                break;
            case 4 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:43: LEFT_SQUARE_BRACKET
                {
                	mLEFT_SQUARE_BRACKET(); 

                }
                break;
            case 5 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:63: RIGHT_SQUARE_BRACKET
                {
                	mRIGHT_SQUARE_BRACKET(); 

                }
                break;
            case 6 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:84: LEFT_CURLY_BRACKET
                {
                	mLEFT_CURLY_BRACKET(); 

                }
                break;
            case 7 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:103: RIGHT_CURLY_BRACKET
                {
                	mRIGHT_CURLY_BRACKET(); 

                }
                break;
            case 8 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:123: BACKSLASH
                {
                	mBACKSLASH(); 

                }
                break;
            case 9 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:133: CIRCUMFLEX
                {
                	mCIRCUMFLEX(); 

                }
                break;
            case 10 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:144: COMMERCIAL_AT
                {
                	mCOMMERCIAL_AT(); 

                }
                break;
            case 11 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:158: NUMBER_SIGN
                {
                	mNUMBER_SIGN(); 

                }
                break;
            case 12 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:170: APOSTROPHE
                {
                	mAPOSTROPHE(); 

                }
                break;
            case 13 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:181: SPECIAL_FORM
                {
                	mSPECIAL_FORM(); 

                }
                break;
            case 14 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:194: STRING
                {
                	mSTRING(); 

                }
                break;
            case 15 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:201: REGEX_LITERAL
                {
                	mREGEX_LITERAL(); 

                }
                break;
            case 16 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:215: NUMBER
                {
                	mNUMBER(); 

                }
                break;
            case 17 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:222: CHARACTER
                {
                	mCHARACTER(); 

                }
                break;
            case 18 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:232: HEXDIGIT
                {
                	mHEXDIGIT(); 

                }
                break;
            case 19 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:241: NIL
                {
                	mNIL(); 

                }
                break;
            case 20 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:245: BOOLEAN
                {
                	mBOOLEAN(); 

                }
                break;
            case 21 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:253: SYMBOL
                {
                	mSYMBOL(); 

                }
                break;
            case 22 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:260: METADATA_TYPEHINT
                {
                	mMETADATA_TYPEHINT(); 

                }
                break;
            case 23 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:278: KEYWORD
                {
                	mKEYWORD(); 

                }
                break;
            case 24 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:286: SYNTAX_QUOTE
                {
                	mSYNTAX_QUOTE(); 

                }
                break;
            case 25 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:299: UNQUOTE_SPLICING
                {
                	mUNQUOTE_SPLICING(); 

                }
                break;
            case 26 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:316: UNQUOTE
                {
                	mUNQUOTE(); 

                }
                break;
            case 27 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:324: COMMENT
                {
                	mCOMMENT(); 

                }
                break;
            case 28 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:332: SPACE
                {
                	mSPACE(); 

                }
                break;
            case 29 :
                // C:\\Users\\Jon\\Desktop\\Clojure.g:1:338: LAMBDA_ARG
                {
                	mLAMBDA_ARG(); 

                }
                break;

        }

    }


    protected DFA1 dfa1;
    protected DFA13 dfa13;
    protected DFA18 dfa18;
    protected DFA28 dfa28;
	private void InitializeCyclicDFAs()
	{
	    this.dfa1 = new DFA1(this);
	    this.dfa13 = new DFA13(this);
	    this.dfa18 = new DFA18(this);
	    this.dfa28 = new DFA28(this);
	    this.dfa13.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA13_SpecialStateTransition);
	    this.dfa28.specialStateTransitionHandler = new DFA.SpecialStateTransitionHandler(DFA28_SpecialStateTransition);
	}

    const string DFA1_eotS =
        "\x1d\uffff";
    const string DFA1_eofS =
        "\x1d\uffff";
    const string DFA1_minS =
        "\x01\x2e\x01\x65\x01\uffff\x01\x65\x04\uffff\x01\x68\x01\x6f\x09"+
        "\uffff\x01\x6e\x01\x69\x01\x74\x01\x6f\x01\x72\x01\x2d\x01\x65\x01"+
        "\x6e\x02\uffff";
    const string DFA1_maxS =
        "\x01\x76\x01\x6f\x01\uffff\x01\x6f\x04\uffff\x01\x72\x01\x6f\x09"+
        "\uffff\x01\x6e\x01\x69\x01\x74\x01\x6f\x01\x72\x01\x2d\x01\x65\x01"+
        "\x78\x02\uffff";
    const string DFA1_acceptS =
        "\x02\uffff\x01\x02\x01\uffff\x01\x05\x01\x06\x01\x07\x01\x09\x02"+
        "\uffff\x01\x0e\x01\x0f\x01\x10\x01\x01\x01\x03\x01\x04\x01\x08\x01"+
        "\x0a\x01\x0b\x08\uffff\x01\x0c\x01\x0d";
    const string DFA1_specialS =
        "\x1d\uffff}>";
    static readonly string[] DFA1_transitionS = {
            "\x01\x0c\x35\uffff\x01\x01\x01\uffff\x01\x06\x02\uffff\x01"+
            "\x02\x02\uffff\x01\x03\x01\x09\x01\x0a\x02\uffff\x01\x04\x01"+
            "\x07\x01\x0b\x01\x08\x01\uffff\x01\x05",
            "\x01\x0d\x09\uffff\x01\x0e",
            "",
            "\x01\x0f\x09\uffff\x01\x10",
            "",
            "",
            "",
            "",
            "\x01\x11\x09\uffff\x01\x12",
            "\x01\x13",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x14",
            "\x01\x15",
            "\x01\x16",
            "\x01\x17",
            "\x01\x18",
            "\x01\x19",
            "\x01\x1a",
            "\x01\x1b\x09\uffff\x01\x1c",
            "",
            ""
    };

    static readonly short[] DFA1_eot = DFA.UnpackEncodedString(DFA1_eotS);
    static readonly short[] DFA1_eof = DFA.UnpackEncodedString(DFA1_eofS);
    static readonly char[] DFA1_min = DFA.UnpackEncodedStringToUnsignedChars(DFA1_minS);
    static readonly char[] DFA1_max = DFA.UnpackEncodedStringToUnsignedChars(DFA1_maxS);
    static readonly short[] DFA1_accept = DFA.UnpackEncodedString(DFA1_acceptS);
    static readonly short[] DFA1_special = DFA.UnpackEncodedString(DFA1_specialS);
    static readonly short[][] DFA1_transition = DFA.UnpackEncodedStringArray(DFA1_transitionS);

    protected class DFA1 : DFA
    {
        public DFA1(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 1;
            this.eot = DFA1_eot;
            this.eof = DFA1_eof;
            this.min = DFA1_min;
            this.max = DFA1_max;
            this.accept = DFA1_accept;
            this.special = DFA1_special;
            this.transition = DFA1_transition;

        }

        override public string Description
        {
            get { return "53:1: SPECIAL_FORM : ( 'def' | 'if' | 'do' | 'let' | 'quote' | 'var' | 'fn' | 'loop' | 'recur' | 'throw' | 'try' | 'monitor-enter' | 'monitor-exit' | 'new' | 'set!' | '.' );"; }
        }

    }

    const string DFA13_eotS =
        "\x02\uffff\x04\x06\x05\uffff";
    const string DFA13_eofS =
        "\x0b\uffff";
    const string DFA13_minS =
        "\x01\x5c\x01\x00\x01\x65\x01\x70\x01\x61\x01\x30\x05\uffff";
    const string DFA13_maxS =
        "\x01\x5c\x01\uffff\x01\x65\x01\x70\x01\x61\x01\x66\x05\uffff";
    const string DFA13_acceptS =
        "\x06\uffff\x01\x05\x01\x01\x01\x02\x01\x03\x01\x04";
    const string DFA13_specialS =
        "\x01\uffff\x01\x00\x09\uffff}>";
    static readonly string[] DFA13_transitionS = {
            "\x01\x01",
            "\x6e\x06\x01\x02\x04\x06\x01\x03\x01\x04\x01\x05\uff8a\x06",
            "\x01\x07",
            "\x01\x08",
            "\x01\x09",
            "\x0a\x0a\x07\uffff\x06\x0a\x1a\uffff\x06\x0a",
            "",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA13_eot = DFA.UnpackEncodedString(DFA13_eotS);
    static readonly short[] DFA13_eof = DFA.UnpackEncodedString(DFA13_eofS);
    static readonly char[] DFA13_min = DFA.UnpackEncodedStringToUnsignedChars(DFA13_minS);
    static readonly char[] DFA13_max = DFA.UnpackEncodedStringToUnsignedChars(DFA13_maxS);
    static readonly short[] DFA13_accept = DFA.UnpackEncodedString(DFA13_acceptS);
    static readonly short[] DFA13_special = DFA.UnpackEncodedString(DFA13_specialS);
    static readonly short[][] DFA13_transition = DFA.UnpackEncodedStringArray(DFA13_transitionS);

    protected class DFA13 : DFA
    {
        public DFA13(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 13;
            this.eot = DFA13_eot;
            this.eof = DFA13_eof;
            this.min = DFA13_min;
            this.max = DFA13_max;
            this.accept = DFA13_accept;
            this.special = DFA13_special;
            this.transition = DFA13_transition;

        }

        override public string Description
        {
            get { return "94:1: CHARACTER : ( '\\\\newline' | '\\\\space' | '\\\\tab' | '\\\\u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT | BACKSLASH . );"; }
        }

    }


    protected internal int DFA13_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            IIntStream input = _input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA13_1 = input.LA(1);

                   	s = -1;
                   	if ( (LA13_1 == 'n') ) { s = 2; }

                   	else if ( (LA13_1 == 's') ) { s = 3; }

                   	else if ( (LA13_1 == 't') ) { s = 4; }

                   	else if ( (LA13_1 == 'u') ) { s = 5; }

                   	else if ( ((LA13_1 >= '\u0000' && LA13_1 <= 'm') || (LA13_1 >= 'o' && LA13_1 <= 'r') || (LA13_1 >= 'v' && LA13_1 <= '\uFFFF')) ) { s = 6; }

                   	if ( s >= 0 ) return s;
                   	break;
        }
        NoViableAltException nvae13 =
            new NoViableAltException(dfa.Description, 13, _s, input);
        dfa.Error(nvae13);
        throw nvae13;
    }
    const string DFA18_eotS =
        "\x01\x01\x01\uffff\x05\x07\x02\uffff\x0a\x07\x01\uffff\x05\x07"+
        "\x01\uffff\x02\x07\x01\uffff\x02\x07\x02\uffff";
    const string DFA18_eofS =
        "\x21\uffff";
    const string DFA18_minS =
        "\x01\x21\x01\uffff\x01\x6e\x01\x6c\x02\x6f\x01\x62\x02\uffff\x01"+
        "\x74\x01\x6f\x01\x6e\x01\x75\x01\x6a\x01\x73\x01\x61\x01\x67\x01"+
        "\x62\x01\x65\x01\uffff\x01\x74\x01\x73\x01\x6c\x01\x63\x01\x73\x01"+
        "\uffff\x01\x65\x01\x74\x01\uffff\x02\x73\x02\uffff";
    const string DFA18_maxS =
        "\x01\x7a\x01\uffff\x01\x6e\x01\x6c\x02\x6f\x01\x62\x02\uffff\x01"+
        "\x74\x01\x6f\x01\x6e\x01\x75\x01\x6a\x01\x73\x01\x61\x01\x67\x01"+
        "\x62\x01\x65\x01\uffff\x01\x74\x01\x73\x01\x6c\x01\x63\x01\x73\x01"+
        "\uffff\x01\x65\x01\x74\x01\uffff\x02\x73\x02\uffff";
    const string DFA18_acceptS =
        "\x01\uffff\x01\x08\x05\uffff\x01\x06\x01\x07\x0a\uffff\x01\x01"+
        "\x05\uffff\x01\x03\x02\uffff\x01\x02\x02\uffff\x01\x04\x01\x05";
    const string DFA18_specialS =
        "\x21\uffff}>";
    static readonly string[] DFA18_transitionS = {
            "\x01\x07\x01\x08\x01\uffff\x01\x07\x05\uffff\x02\x07\x01\uffff"+
            "\x01\x07\x0e\uffff\x04\x07\x01\uffff\x1a\x07\x04\uffff\x01\x07"+
            "\x01\uffff\x03\x07\x01\x05\x01\x07\x01\x03\x02\x07\x01\x02\x02"+
            "\x07\x01\x04\x02\x07\x01\x06\x0b\x07",
            "",
            "\x01\x09",
            "\x01\x0a",
            "\x01\x0b",
            "\x01\x0c",
            "\x01\x0d",
            "",
            "",
            "\x01\x0e",
            "\x01\x0f",
            "\x01\x10",
            "\x01\x11",
            "\x01\x12",
            "\x01\x13",
            "\x01\x14",
            "\x01\x15",
            "\x01\x16",
            "\x01\x17",
            "",
            "\x01\x18",
            "\x01\x19",
            "\x01\x1a",
            "\x01\x1b",
            "\x01\x1c",
            "",
            "\x01\x1d",
            "\x01\x1e",
            "",
            "\x01\x1f",
            "\x01\x20",
            "",
            ""
    };

    static readonly short[] DFA18_eot = DFA.UnpackEncodedString(DFA18_eotS);
    static readonly short[] DFA18_eof = DFA.UnpackEncodedString(DFA18_eofS);
    static readonly char[] DFA18_min = DFA.UnpackEncodedStringToUnsignedChars(DFA18_minS);
    static readonly char[] DFA18_max = DFA.UnpackEncodedStringToUnsignedChars(DFA18_maxS);
    static readonly short[] DFA18_accept = DFA.UnpackEncodedString(DFA18_acceptS);
    static readonly short[] DFA18_special = DFA.UnpackEncodedString(DFA18_specialS);
    static readonly short[][] DFA18_transition = DFA.UnpackEncodedStringArray(DFA18_transitionS);

    protected class DFA18 : DFA
    {
        public DFA18(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 18;
            this.eot = DFA18_eot;
            this.eof = DFA18_eof;
            this.min = DFA18_min;
            this.max = DFA18_max;
            this.accept = DFA18_accept;
            this.special = DFA18_special;
            this.transition = DFA18_transition;

        }

        override public string Description
        {
            get { return "()* loopback of 119:27: ( 'ints' | 'floats' | 'longs' | 'doubles' | 'objects' | NAME | STRING )*"; }
        }

    }

    const string DFA28_eotS =
        "\x08\uffff\x01\x25\x01\x26\x01\uffff\x01\x28\x01\uffff\x01\x2c"+
        "\x04\x1d\x01\x2c\x05\x1d\x02\uffff\x01\x1d\x01\uffff\x01\x2c\x03"+
        "\uffff\x01\x3e\x09\uffff\x01\x1d\x01\x18\x01\uffff\x01\x18\x04\x1d"+
        "\x01\x18\x08\x1d\x01\x3c\x03\uffff\x02\x18\x02\x1d\x01\x18\x03\x1d"+
        "\x01\x18\x02\x1d\x01\x18\x01\x56\x03\x1d\x01\x18\x04\x1d\x01\x5f"+
        "\x01\x1d\x01\uffff\x01\x18\x01\x3c\x01\x1d\x01\x3c\x01\x18\x01\x5f"+
        "\x02\x18\x01\uffff\x0a\x1d\x02\x18";
    const string DFA28_eofS =
        "\x6c\uffff";
    const string DFA28_minS =
        "\x01\x09\x07\uffff\x01\x00\x01\x21\x01\uffff\x01\x22\x01\uffff"+
        "\x01\x21\x01\x66\x01\x65\x01\x75\x01\x61\x01\x21\x01\x65\x01\x68"+
        "\x01\x6f\x02\x65\x02\uffff\x01\x30\x01\uffff\x01\x21\x03\uffff\x01"+
        "\x40\x09\uffff\x01\x66\x01\x21\x01\uffff\x01\x21\x01\x74\x02\x6f"+
        "\x01\x72\x01\x21\x01\x6c\x01\x63\x01\x72\x01\x75\x01\x6e\x01\x77"+
        "\x01\x6c\x01\x74\x01\x21\x03\uffff\x02\x21\x01\x70\x01\x74\x01\x21"+
        "\x01\x73\x01\x75\x01\x6f\x01\x21\x01\x65\x01\x69\x03\x21\x01\x30"+
        "\x01\x2d\x01\x21\x02\x65\x01\x72\x01\x77\x01\x21\x01\x74\x01\uffff"+
        "\x02\x21\x01\x30\x05\x21\x01\uffff\x01\x6f\x01\x72\x01\x2d\x01\x65"+
        "\x01\x6e\x01\x74\x01\x69\x01\x65\x01\x74\x01\x72\x02\x21";
    const string DFA28_maxS =
        "\x01\x7e\x07\uffff\x01\uffff\x01\x7a\x01\uffff\x01\x5e\x01\uffff"+
        "\x01\x7a\x01\x66\x01\x6f\x01\x75\x01\x61\x01\x7a\x01\x65\x01\x72"+
        "\x01\x6f\x01\x69\x01\x65\x02\uffff\x01\x39\x01\uffff\x01\x7a\x03"+
        "\uffff\x01\x40\x09\uffff\x01\x66\x01\x7a\x01\uffff\x01\x7a\x01\x74"+
        "\x02\x6f\x01\x72\x01\x7a\x01\x6c\x01\x63\x01\x72\x01\x79\x01\x6e"+
        "\x01\x77\x01\x6c\x01\x74\x01\x7a\x03\uffff\x02\x7a\x01\x70\x01\x74"+
        "\x01\x7a\x01\x73\x01\x75\x01\x6f\x01\x7a\x01\x65\x01\x69\x02\x7a"+
        "\x01\x21\x02\x39\x01\x7a\x02\x65\x01\x72\x01\x77\x01\x7a\x01\x74"+
        "\x01\uffff\x02\x7a\x01\x39\x05\x7a\x01\uffff\x01\x6f\x01\x72\x01"+
        "\x2d\x01\x65\x01\x78\x01\x74\x01\x69\x01\x65\x01\x74\x01\x72\x02"+
        "\x7a";
    const string DFA28_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01\x05\x01\x06\x01"+
        "\x07\x02\uffff\x01\x0a\x01\uffff\x01\x0c\x0b\uffff\x01\x0d\x01\x0e"+
        "\x01\uffff\x01\x10\x01\uffff\x01\x15\x01\x17\x01\x18\x01\uffff\x01"+
        "\x1b\x01\x1c\x01\x1d\x01\x11\x01\x08\x01\x09\x01\x16\x01\x0b\x01"+
        "\x0f\x02\uffff\x01\x12\x0f\uffff\x01\x10\x01\x19\x01\x1a\x17\uffff"+
        "\x01\x13\x08\uffff\x01\x14\x0c\uffff";
    const string DFA28_specialS =
        "\x08\uffff\x01\x00\x63\uffff}>";
    static readonly string[] DFA28_transitionS = {
            "\x02\x22\x02\uffff\x01\x22\x12\uffff\x01\x22\x01\x1d\x01\x19"+
            "\x01\x0b\x01\x1d\x01\x23\x01\x03\x01\x0c\x01\x01\x01\x02\x02"+
            "\x1d\x01\x22\x01\x1a\x01\x18\x01\x1d\x0a\x1b\x01\x1e\x01\x21"+
            "\x04\x1d\x01\x0a\x06\x1c\x14\x1d\x01\x04\x01\x08\x01\x05\x01"+
            "\x09\x01\x1d\x01\x1f\x03\x1c\x01\x0d\x01\x1c\x01\x12\x02\x1d"+
            "\x01\x0e\x02\x1d\x01\x0f\x01\x15\x01\x16\x02\x1d\x01\x10\x01"+
            "\x13\x01\x17\x01\x14\x01\x1d\x01\x11\x04\x1d\x01\x06\x01\uffff"+
            "\x01\x07\x01\x20",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x00\x24",
            "\x02\x27\x01\uffff\x01\x27\x05\uffff\x02\x27\x01\uffff\x01"+
            "\x27\x0e\uffff\x04\x27\x01\uffff\x1a\x27\x04\uffff\x01\x27\x01"+
            "\uffff\x1a\x27",
            "",
            "\x01\x29\x01\x27\x3a\uffff\x01\x27",
            "",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x04\x1d\x01\x2a\x09\x1d\x01\x2b\x0b\x1d",
            "\x01\x2d",
            "\x01\x2e\x09\uffff\x01\x2f",
            "\x01\x30",
            "\x01\x31",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x01\x33\x0c\x1d\x01\x32\x0c\x1d",
            "\x01\x34",
            "\x01\x35\x09\uffff\x01\x36",
            "\x01\x37",
            "\x01\x38\x03\uffff\x01\x39",
            "\x01\x3a",
            "",
            "",
            "\x0a\x3b",
            "",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "",
            "",
            "",
            "\x01\x3d",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "\x01\x3f",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x40",
            "\x01\x41",
            "\x01\x42",
            "\x01\x43",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x44",
            "\x01\x45",
            "\x01\x46",
            "\x01\x48\x03\uffff\x01\x47",
            "\x01\x49",
            "\x01\x4a",
            "\x01\x4b",
            "\x01\x4c",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x01"+
            "\x1d\x01\x4d\x01\x1d\x0a\x3b\x01\x1d\x01\uffff\x04\x1d\x01\uffff"+
            "\x04\x1d\x01\x4e\x15\x1d\x04\uffff\x01\x1d\x01\uffff\x04\x1d"+
            "\x01\x4e\x15\x1d",
            "",
            "",
            "",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x4f",
            "\x01\x50",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x51",
            "\x01\x52",
            "\x01\x53",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x54",
            "\x01\x55",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x57",
            "\x0a\x58",
            "\x01\x59\x02\uffff\x0a\x5a",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x5b",
            "\x01\x5c",
            "\x01\x5d",
            "\x01\x5e",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x60",
            "",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x03"+
            "\x1d\x0a\x58\x01\x1d\x01\uffff\x04\x1d\x01\uffff\x04\x1d\x01"+
            "\x4e\x15\x1d\x04\uffff\x01\x1d\x01\uffff\x04\x1d\x01\x4e\x15"+
            "\x1d",
            "\x0a\x5a",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x03"+
            "\x1d\x0a\x5a\x01\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04"+
            "\uffff\x01\x1d\x01\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "",
            "\x01\x61",
            "\x01\x62",
            "\x01\x63",
            "\x01\x64",
            "\x01\x65\x09\uffff\x01\x66",
            "\x01\x67",
            "\x01\x68",
            "\x01\x69",
            "\x01\x6a",
            "\x01\x6b",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d",
            "\x01\x1d\x01\uffff\x02\x1d\x05\uffff\x02\x1d\x01\uffff\x0e"+
            "\x1d\x01\uffff\x04\x1d\x01\uffff\x1a\x1d\x04\uffff\x01\x1d\x01"+
            "\uffff\x1a\x1d"
    };

    static readonly short[] DFA28_eot = DFA.UnpackEncodedString(DFA28_eotS);
    static readonly short[] DFA28_eof = DFA.UnpackEncodedString(DFA28_eofS);
    static readonly char[] DFA28_min = DFA.UnpackEncodedStringToUnsignedChars(DFA28_minS);
    static readonly char[] DFA28_max = DFA.UnpackEncodedStringToUnsignedChars(DFA28_maxS);
    static readonly short[] DFA28_accept = DFA.UnpackEncodedString(DFA28_acceptS);
    static readonly short[] DFA28_special = DFA.UnpackEncodedString(DFA28_specialS);
    static readonly short[][] DFA28_transition = DFA.UnpackEncodedStringArray(DFA28_transitionS);

    protected class DFA28 : DFA
    {
        public DFA28(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 28;
            this.eot = DFA28_eot;
            this.eof = DFA28_eof;
            this.min = DFA28_min;
            this.max = DFA28_max;
            this.accept = DFA28_accept;
            this.special = DFA28_special;
            this.transition = DFA28_transition;

        }

        override public string Description
        {
            get { return "1:1: Tokens : ( OPEN_PAREN | CLOSE_PAREN | AMPERSAND | LEFT_SQUARE_BRACKET | RIGHT_SQUARE_BRACKET | LEFT_CURLY_BRACKET | RIGHT_CURLY_BRACKET | BACKSLASH | CIRCUMFLEX | COMMERCIAL_AT | NUMBER_SIGN | APOSTROPHE | SPECIAL_FORM | STRING | REGEX_LITERAL | NUMBER | CHARACTER | HEXDIGIT | NIL | BOOLEAN | SYMBOL | METADATA_TYPEHINT | KEYWORD | SYNTAX_QUOTE | UNQUOTE_SPLICING | UNQUOTE | COMMENT | SPACE | LAMBDA_ARG );"; }
        }

    }


    protected internal int DFA28_SpecialStateTransition(DFA dfa, int s, IIntStream _input) //throws NoViableAltException
    {
            IIntStream input = _input;
    	int _s = s;
        switch ( s )
        {
               	case 0 : 
                   	int LA28_8 = input.LA(1);

                   	s = -1;
                   	if ( ((LA28_8 >= '\u0000' && LA28_8 <= '\uFFFF')) ) { s = 36; }

                   	else s = 37;

                   	if ( s >= 0 ) return s;
                   	break;
        }
        NoViableAltException nvae28 =
            new NoViableAltException(dfa.Description, 28, _s, input);
        dfa.Error(nvae28);
        throw nvae28;
    }
 
    
}
