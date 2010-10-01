// $ANTLR 3.2 Sep 23, 2009 12:02:23 C:\\Users\\Jon\\Desktop\\Clojure.g 2010-10-01 12:57:09

// The variable 'variable' is assigned but its value is never used.
#pragma warning disable 168, 219
// Unreachable code detected.
#pragma warning disable 162


using System;
using System.Collections;
using Antlr.Runtime;
using IList 		= System.Collections.IList;
using ArrayList 	= System.Collections.ArrayList;
using Stack 		= Antlr.Runtime.Collections.StackList;


public partial class ClojureParser : Parser
{
    public static readonly string[] tokenNames = new string[] 
	{
        "<invalid>", 
		"<EOR>", 
		"<DOWN>", 
		"<UP>", 
		"OPEN_PAREN", 
		"CLOSE_PAREN", 
		"AMPERSAND", 
		"LEFT_SQUARE_BRACKET", 
		"RIGHT_SQUARE_BRACKET", 
		"LEFT_CURLY_BRACKET", 
		"RIGHT_CURLY_BRACKET", 
		"BACKSLASH", 
		"CIRCUMFLEX", 
		"COMMERCIAL_AT", 
		"NUMBER_SIGN", 
		"APOSTROPHE", 
		"SPECIAL_FORM", 
		"EscapeSequence", 
		"STRING", 
		"REGEX_LITERAL", 
		"UnicodeEscape", 
		"OctalEscape", 
		"HEXDIGIT", 
		"NUMBER", 
		"CHARACTER", 
		"NIL", 
		"BOOLEAN", 
		"NAME", 
		"SYMBOL", 
		"METADATA_TYPEHINT", 
		"SYMBOL_HEAD", 
		"SYMBOL_REST", 
		"KEYWORD", 
		"SYNTAX_QUOTE", 
		"UNQUOTE_SPLICING", 
		"UNQUOTE", 
		"COMMENT", 
		"SPACE", 
		"LAMBDA_ARG"
    };

    public const int SYNTAX_QUOTE = 33;
    public const int KEYWORD = 32;
    public const int SYMBOL = 28;
    public const int METADATA_TYPEHINT = 29;
    public const int SYMBOL_HEAD = 30;
    public const int NUMBER = 23;
    public const int AMPERSAND = 6;
    public const int OPEN_PAREN = 4;
    public const int COMMERCIAL_AT = 13;
    public const int SPACE = 37;
    public const int EOF = -1;
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
    public const int SYMBOL_REST = 31;
    public const int APOSTROPHE = 15;
    public const int REGEX_LITERAL = 19;
    public const int COMMENT = 36;
    public const int OctalEscape = 21;
    public const int EscapeSequence = 17;
    public const int UNQUOTE_SPLICING = 34;
    public const int CIRCUMFLEX = 12;
    public const int STRING = 18;
    public const int HEXDIGIT = 22;
    public const int BACKSLASH = 11;

    // delegates
    // delegators



        public ClojureParser(ITokenStream input)
    		: this(input, new RecognizerSharedState()) {
        }

        public ClojureParser(ITokenStream input, RecognizerSharedState state)
    		: base(input, state) {
            InitializeCyclicDFAs();

             
        }
        

    override public string[] TokenNames {
		get { return ClojureParser.tokenNames; }
    }

    override public string GrammarFileName {
		get { return "C:\\Users\\Jon\\Desktop\\Clojure.g"; }
    }


      bool inLambda=false;
          int syntaxQuoteDepth = 0;

          ArrayList symbols = new ArrayList();
          public ArrayList getCollectedSymbols() { return symbols; }

          Hashtable parensMatching = new Hashtable(); 
          
          public int matchingParenForPosition(int position)
          {
            return (int) parensMatching[position];
          }
          
          public void clearParensMatching() { parensMatching.Clear(); }



    // $ANTLR start "literal"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:140:1: literal : ( STRING | NUMBER | CHARACTER | NIL | BOOLEAN | KEYWORD );
    public void literal() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:140:8: ( STRING | NUMBER | CHARACTER | NIL | BOOLEAN | KEYWORD )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:
            {
            	if ( input.LA(1) == STRING || (input.LA(1) >= NUMBER && input.LA(1) <= BOOLEAN) || input.LA(1) == KEYWORD ) 
            	{
            	    input.Consume();
            	    state.errorRecovery = false;
            	}
            	else 
            	{
            	    MismatchedSetException mse = new MismatchedSetException(null,input);
            	    throw mse;
            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "literal"


    // $ANTLR start "file"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:183:1: file : ( form )* ;
    public void file() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:183:5: ( ( form )* )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:184:9: ( form )*
            {
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:184:9: ( form )*
            	do 
            	{
            	    int alt1 = 2;
            	    int LA1_0 = input.LA(1);

            	    if ( (LA1_0 == OPEN_PAREN || (LA1_0 >= AMPERSAND && LA1_0 <= LEFT_SQUARE_BRACKET) || LA1_0 == LEFT_CURLY_BRACKET || (LA1_0 >= CIRCUMFLEX && LA1_0 <= SPECIAL_FORM) || (LA1_0 >= STRING && LA1_0 <= REGEX_LITERAL) || (LA1_0 >= NUMBER && LA1_0 <= BOOLEAN) || LA1_0 == SYMBOL || (LA1_0 >= KEYWORD && LA1_0 <= COMMENT) || LA1_0 == LAMBDA_ARG) )
            	    {
            	        alt1 = 1;
            	    }


            	    switch (alt1) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:184:11: form
            			    {
            			    	PushFollow(FOLLOW_form_in_file1319);
            			    	form();
            			    	state.followingStackPointer--;

            			    	 Console.WriteLine("form found"); 

            			    }
            			    break;

            			default:
            			    goto loop1;
            	    }
            	} while (true);

            	loop1:
            		;	// Stops C# compiler whining that label 'loop1' has no statements


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "file"


    // $ANTLR start "form"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:188:1: form : ({...}? LAMBDA_ARG | literal | COMMENT | AMPERSAND | ( metadataForm )? ( SPECIAL_FORM | s= SYMBOL | list | vector | map ) | macroForm | dispatchMacroForm | set );
    public void form() // throws RecognitionException [1]
    {   
        IToken s = null;

        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:188:6: ({...}? LAMBDA_ARG | literal | COMMENT | AMPERSAND | ( metadataForm )? ( SPECIAL_FORM | s= SYMBOL | list | vector | map ) | macroForm | dispatchMacroForm | set )
            int alt4 = 8;
            alt4 = dfa4.Predict(input);
            switch (alt4) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:189:3: {...}? LAMBDA_ARG
                    {
                    	if ( !((this.inLambda)) ) 
                    	{
                    	    throw new FailedPredicateException(input, "form", "this.inLambda");
                    	}
                    	Match(input,LAMBDA_ARG,FOLLOW_LAMBDA_ARG_in_form1352); 

                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:190:10: literal
                    {
                    	PushFollow(FOLLOW_literal_in_form1363);
                    	literal();
                    	state.followingStackPointer--;


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:192:7: COMMENT
                    {
                    	Match(input,COMMENT,FOLLOW_COMMENT_in_form1389); 

                    }
                    break;
                case 4 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:193:9: AMPERSAND
                    {
                    	Match(input,AMPERSAND,FOLLOW_AMPERSAND_in_form1399); 

                    }
                    break;
                case 5 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:194:9: ( metadataForm )? ( SPECIAL_FORM | s= SYMBOL | list | vector | map )
                    {
                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:194:9: ( metadataForm )?
                    	int alt2 = 2;
                    	int LA2_0 = input.LA(1);

                    	if ( (LA2_0 == NUMBER_SIGN) )
                    	{
                    	    alt2 = 1;
                    	}
                    	switch (alt2) 
                    	{
                    	    case 1 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:9: metadataForm
                    	        {
                    	        	PushFollow(FOLLOW_metadataForm_in_form1409);
                    	        	metadataForm();
                    	        	state.followingStackPointer--;


                    	        }
                    	        break;

                    	}

                    	// C:\\Users\\Jon\\Desktop\\Clojure.g:194:23: ( SPECIAL_FORM | s= SYMBOL | list | vector | map )
                    	int alt3 = 5;
                    	switch ( input.LA(1) ) 
                    	{
                    	case SPECIAL_FORM:
                    		{
                    	    alt3 = 1;
                    	    }
                    	    break;
                    	case SYMBOL:
                    		{
                    	    alt3 = 2;
                    	    }
                    	    break;
                    	case OPEN_PAREN:
                    		{
                    	    alt3 = 3;
                    	    }
                    	    break;
                    	case LEFT_SQUARE_BRACKET:
                    		{
                    	    alt3 = 4;
                    	    }
                    	    break;
                    	case LEFT_CURLY_BRACKET:
                    		{
                    	    alt3 = 5;
                    	    }
                    	    break;
                    		default:
                    		    NoViableAltException nvae_d3s0 =
                    		        new NoViableAltException("", 3, 0, input);

                    		    throw nvae_d3s0;
                    	}

                    	switch (alt3) 
                    	{
                    	    case 1 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:25: SPECIAL_FORM
                    	        {
                    	        	Match(input,SPECIAL_FORM,FOLLOW_SPECIAL_FORM_in_form1414); 

                    	        }
                    	        break;
                    	    case 2 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:40: s= SYMBOL
                    	        {
                    	        	s=(IToken)Match(input,SYMBOL,FOLLOW_SYMBOL_in_form1420); 
                    	        	 symbols.Add(s.Text); 

                    	        }
                    	        break;
                    	    case 3 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:76: list
                    	        {
                    	        	PushFollow(FOLLOW_list_in_form1426);
                    	        	list();
                    	        	state.followingStackPointer--;


                    	        }
                    	        break;
                    	    case 4 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:83: vector
                    	        {
                    	        	PushFollow(FOLLOW_vector_in_form1430);
                    	        	vector();
                    	        	state.followingStackPointer--;


                    	        }
                    	        break;
                    	    case 5 :
                    	        // C:\\Users\\Jon\\Desktop\\Clojure.g:194:92: map
                    	        {
                    	        	PushFollow(FOLLOW_map_in_form1434);
                    	        	map();
                    	        	state.followingStackPointer--;


                    	        }
                    	        break;

                    	}


                    }
                    break;
                case 6 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:195:9: macroForm
                    {
                    	PushFollow(FOLLOW_macroForm_in_form1446);
                    	macroForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 7 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:196:9: dispatchMacroForm
                    {
                    	PushFollow(FOLLOW_dispatchMacroForm_in_form1456);
                    	dispatchMacroForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 8 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:197:9: set
                    {
                    	PushFollow(FOLLOW_set_in_form1466);
                    	set();
                    	state.followingStackPointer--;


                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "form"


    // $ANTLR start "macroForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:200:1: macroForm : ( quoteForm | metaForm | derefForm | syntaxQuoteForm | {...}? unquoteSplicingForm | {...}? unquoteForm );
    public void macroForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:200:10: ( quoteForm | metaForm | derefForm | syntaxQuoteForm | {...}? unquoteSplicingForm | {...}? unquoteForm )
            int alt5 = 6;
            switch ( input.LA(1) ) 
            {
            case APOSTROPHE:
            	{
                alt5 = 1;
                }
                break;
            case CIRCUMFLEX:
            	{
                alt5 = 2;
                }
                break;
            case COMMERCIAL_AT:
            	{
                alt5 = 3;
                }
                break;
            case SYNTAX_QUOTE:
            	{
                alt5 = 4;
                }
                break;
            case UNQUOTE_SPLICING:
            	{
                alt5 = 5;
                }
                break;
            case UNQUOTE:
            	{
                alt5 = 6;
                }
                break;
            	default:
            	    NoViableAltException nvae_d5s0 =
            	        new NoViableAltException("", 5, 0, input);

            	    throw nvae_d5s0;
            }

            switch (alt5) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:201:9: quoteForm
                    {
                    	PushFollow(FOLLOW_quoteForm_in_macroForm1497);
                    	quoteForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:202:9: metaForm
                    {
                    	PushFollow(FOLLOW_metaForm_in_macroForm1507);
                    	metaForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:203:9: derefForm
                    {
                    	PushFollow(FOLLOW_derefForm_in_macroForm1517);
                    	derefForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 4 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:204:9: syntaxQuoteForm
                    {
                    	PushFollow(FOLLOW_syntaxQuoteForm_in_macroForm1527);
                    	syntaxQuoteForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 5 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:205:7: {...}? unquoteSplicingForm
                    {
                    	if ( !(( this.syntaxQuoteDepth > 0 )) ) 
                    	{
                    	    throw new FailedPredicateException(input, "macroForm", " this.syntaxQuoteDepth > 0 ");
                    	}
                    	PushFollow(FOLLOW_unquoteSplicingForm_in_macroForm1537);
                    	unquoteSplicingForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 6 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:206:7: {...}? unquoteForm
                    {
                    	if ( !(( this.syntaxQuoteDepth > 0 )) ) 
                    	{
                    	    throw new FailedPredicateException(input, "macroForm", " this.syntaxQuoteDepth > 0 ");
                    	}
                    	PushFollow(FOLLOW_unquoteForm_in_macroForm1547);
                    	unquoteForm();
                    	state.followingStackPointer--;


                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "macroForm"


    // $ANTLR start "dispatchMacroForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:209:1: dispatchMacroForm : ( REGEX_LITERAL | varQuoteForm | {...}? lambdaForm );
    public void dispatchMacroForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:209:18: ( REGEX_LITERAL | varQuoteForm | {...}? lambdaForm )
            int alt6 = 3;
            int LA6_0 = input.LA(1);

            if ( (LA6_0 == REGEX_LITERAL) )
            {
                alt6 = 1;
            }
            else if ( (LA6_0 == NUMBER_SIGN) )
            {
                int LA6_2 = input.LA(2);

                if ( (LA6_2 == APOSTROPHE) )
                {
                    alt6 = 2;
                }
                else if ( (LA6_2 == OPEN_PAREN) )
                {
                    alt6 = 3;
                }
                else 
                {
                    NoViableAltException nvae_d6s2 =
                        new NoViableAltException("", 6, 2, input);

                    throw nvae_d6s2;
                }
            }
            else 
            {
                NoViableAltException nvae_d6s0 =
                    new NoViableAltException("", 6, 0, input);

                throw nvae_d6s0;
            }
            switch (alt6) 
            {
                case 1 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:210:9: REGEX_LITERAL
                    {
                    	Match(input,REGEX_LITERAL,FOLLOW_REGEX_LITERAL_in_dispatchMacroForm1574); 

                    }
                    break;
                case 2 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:211:9: varQuoteForm
                    {
                    	PushFollow(FOLLOW_varQuoteForm_in_dispatchMacroForm1584);
                    	varQuoteForm();
                    	state.followingStackPointer--;


                    }
                    break;
                case 3 :
                    // C:\\Users\\Jon\\Desktop\\Clojure.g:212:9: {...}? lambdaForm
                    {
                    	if ( !((!this.inLambda)) ) 
                    	{
                    	    throw new FailedPredicateException(input, "dispatchMacroForm", "!this.inLambda");
                    	}
                    	PushFollow(FOLLOW_lambdaForm_in_dispatchMacroForm1596);
                    	lambdaForm();
                    	state.followingStackPointer--;


                    }
                    break;

            }
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "dispatchMacroForm"


    // $ANTLR start "list"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:215:1: list : o= OPEN_PAREN ( form )* c= CLOSE_PAREN ;
    public void list() // throws RecognitionException [1]
    {   
        IToken o = null;
        IToken c = null;

        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:215:5: (o= OPEN_PAREN ( form )* c= CLOSE_PAREN )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:215:9: o= OPEN_PAREN ( form )* c= CLOSE_PAREN
            {
            	o=(IToken)Match(input,OPEN_PAREN,FOLLOW_OPEN_PAREN_in_list1617); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:215:22: ( form )*
            	do 
            	{
            	    int alt7 = 2;
            	    int LA7_0 = input.LA(1);

            	    if ( (LA7_0 == OPEN_PAREN || (LA7_0 >= AMPERSAND && LA7_0 <= LEFT_SQUARE_BRACKET) || LA7_0 == LEFT_CURLY_BRACKET || (LA7_0 >= CIRCUMFLEX && LA7_0 <= SPECIAL_FORM) || (LA7_0 >= STRING && LA7_0 <= REGEX_LITERAL) || (LA7_0 >= NUMBER && LA7_0 <= BOOLEAN) || LA7_0 == SYMBOL || (LA7_0 >= KEYWORD && LA7_0 <= COMMENT) || LA7_0 == LAMBDA_ARG) )
            	    {
            	        alt7 = 1;
            	    }


            	    switch (alt7) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:215:22: form
            			    {
            			    	PushFollow(FOLLOW_form_in_list1619);
            			    	form();
            			    	state.followingStackPointer--;


            			    }
            			    break;

            			default:
            			    goto loop7;
            	    }
            	} while (true);

            	loop7:
            		;	// Stops C# compiler whining that label 'loop7' has no statements

            	c=(IToken)Match(input,CLOSE_PAREN,FOLLOW_CLOSE_PAREN_in_list1625);

                parensMatching.Add(o.TokenIndex, c.TokenIndex);
                parensMatching.Add(c.TokenIndex, o.TokenIndex);


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "list"


    // $ANTLR start "vector"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:222:1: vector : LEFT_SQUARE_BRACKET ( form )* RIGHT_SQUARE_BRACKET ;
    public void vector() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:222:7: ( LEFT_SQUARE_BRACKET ( form )* RIGHT_SQUARE_BRACKET )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:222:10: LEFT_SQUARE_BRACKET ( form )* RIGHT_SQUARE_BRACKET
            {
            	Match(input,LEFT_SQUARE_BRACKET,FOLLOW_LEFT_SQUARE_BRACKET_in_vector1644); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:222:30: ( form )*
            	do 
            	{
            	    int alt8 = 2;
            	    int LA8_0 = input.LA(1);

            	    if ( (LA8_0 == OPEN_PAREN || (LA8_0 >= AMPERSAND && LA8_0 <= LEFT_SQUARE_BRACKET) || LA8_0 == LEFT_CURLY_BRACKET || (LA8_0 >= CIRCUMFLEX && LA8_0 <= SPECIAL_FORM) || (LA8_0 >= STRING && LA8_0 <= REGEX_LITERAL) || (LA8_0 >= NUMBER && LA8_0 <= BOOLEAN) || LA8_0 == SYMBOL || (LA8_0 >= KEYWORD && LA8_0 <= COMMENT) || LA8_0 == LAMBDA_ARG) )
            	    {
            	        alt8 = 1;
            	    }


            	    switch (alt8) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:222:30: form
            			    {
            			    	PushFollow(FOLLOW_form_in_vector1646);
            			    	form();
            			    	state.followingStackPointer--;


            			    }
            			    break;

            			default:
            			    goto loop8;
            	    }
            	} while (true);

            	loop8:
            		;	// Stops C# compiler whining that label 'loop8' has no statements

            	Match(input,RIGHT_SQUARE_BRACKET,FOLLOW_RIGHT_SQUARE_BRACKET_in_vector1649); 

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "vector"


    // $ANTLR start "map"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:225:1: map : LEFT_CURLY_BRACKET ( form form )* RIGHT_CURLY_BRACKET ;
    public void map() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:225:4: ( LEFT_CURLY_BRACKET ( form form )* RIGHT_CURLY_BRACKET )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:225:9: LEFT_CURLY_BRACKET ( form form )* RIGHT_CURLY_BRACKET
            {
            	Match(input,LEFT_CURLY_BRACKET,FOLLOW_LEFT_CURLY_BRACKET_in_map1668); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:225:28: ( form form )*
            	do 
            	{
            	    int alt9 = 2;
            	    int LA9_0 = input.LA(1);

            	    if ( (LA9_0 == OPEN_PAREN || (LA9_0 >= AMPERSAND && LA9_0 <= LEFT_SQUARE_BRACKET) || LA9_0 == LEFT_CURLY_BRACKET || (LA9_0 >= CIRCUMFLEX && LA9_0 <= SPECIAL_FORM) || (LA9_0 >= STRING && LA9_0 <= REGEX_LITERAL) || (LA9_0 >= NUMBER && LA9_0 <= BOOLEAN) || LA9_0 == SYMBOL || (LA9_0 >= KEYWORD && LA9_0 <= COMMENT) || LA9_0 == LAMBDA_ARG) )
            	    {
            	        alt9 = 1;
            	    }


            	    switch (alt9) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:225:29: form form
            			    {
            			    	PushFollow(FOLLOW_form_in_map1671);
            			    	form();
            			    	state.followingStackPointer--;

            			    	PushFollow(FOLLOW_form_in_map1673);
            			    	form();
            			    	state.followingStackPointer--;


            			    }
            			    break;

            			default:
            			    goto loop9;
            	    }
            	} while (true);

            	loop9:
            		;	// Stops C# compiler whining that label 'loop9' has no statements

            	Match(input,RIGHT_CURLY_BRACKET,FOLLOW_RIGHT_CURLY_BRACKET_in_map1677); 

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "map"


    // $ANTLR start "quoteForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:228:1: quoteForm : APOSTROPHE form ;
    public void quoteForm() // throws RecognitionException [1]
    {   
         this.syntaxQuoteDepth++; 
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:231:5: ( APOSTROPHE form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:231:8: APOSTROPHE form
            {
            	Match(input,APOSTROPHE,FOLLOW_APOSTROPHE_in_quoteForm1710); 
            	PushFollow(FOLLOW_form_in_quoteForm1712);
            	form();
            	state.followingStackPointer--;


            }

             this.syntaxQuoteDepth--; 
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "quoteForm"


    // $ANTLR start "metaForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:234:1: metaForm : CIRCUMFLEX form ;
    public void metaForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:234:9: ( CIRCUMFLEX form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:234:13: CIRCUMFLEX form
            {
            	Match(input,CIRCUMFLEX,FOLLOW_CIRCUMFLEX_in_metaForm1726); 
            	PushFollow(FOLLOW_form_in_metaForm1728);
            	form();
            	state.followingStackPointer--;


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "metaForm"


    // $ANTLR start "derefForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:237:1: derefForm : COMMERCIAL_AT form ;
    public void derefForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:237:10: ( COMMERCIAL_AT form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:237:13: COMMERCIAL_AT form
            {
            	Match(input,COMMERCIAL_AT,FOLLOW_COMMERCIAL_AT_in_derefForm1745); 
            	PushFollow(FOLLOW_form_in_derefForm1747);
            	form();
            	state.followingStackPointer--;


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "derefForm"


    // $ANTLR start "syntaxQuoteForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:240:1: syntaxQuoteForm : SYNTAX_QUOTE form ;
    public void syntaxQuoteForm() // throws RecognitionException [1]
    {   
         this.syntaxQuoteDepth++; 
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:243:5: ( SYNTAX_QUOTE form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:244:9: SYNTAX_QUOTE form
            {
            	Match(input,SYNTAX_QUOTE,FOLLOW_SYNTAX_QUOTE_in_syntaxQuoteForm1787); 
            	PushFollow(FOLLOW_form_in_syntaxQuoteForm1789);
            	form();
            	state.followingStackPointer--;


            }

             this.syntaxQuoteDepth--; 
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "syntaxQuoteForm"


    // $ANTLR start "unquoteForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:247:1: unquoteForm : UNQUOTE form ;
    public void unquoteForm() // throws RecognitionException [1]
    {   
         this.syntaxQuoteDepth--; 
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:250:5: ( UNQUOTE form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:251:9: UNQUOTE form
            {
            	Match(input,UNQUOTE,FOLLOW_UNQUOTE_in_unquoteForm1829); 
            	PushFollow(FOLLOW_form_in_unquoteForm1831);
            	form();
            	state.followingStackPointer--;


            }

             this.syntaxQuoteDepth++; 
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "unquoteForm"


    // $ANTLR start "unquoteSplicingForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:254:1: unquoteSplicingForm : UNQUOTE_SPLICING form ;
    public void unquoteSplicingForm() // throws RecognitionException [1]
    {   
         this.syntaxQuoteDepth--; 
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:257:5: ( UNQUOTE_SPLICING form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:258:9: UNQUOTE_SPLICING form
            {
            	Match(input,UNQUOTE_SPLICING,FOLLOW_UNQUOTE_SPLICING_in_unquoteSplicingForm1871); 
            	PushFollow(FOLLOW_form_in_unquoteSplicingForm1873);
            	form();
            	state.followingStackPointer--;


            }

             this.syntaxQuoteDepth++; 
        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "unquoteSplicingForm"


    // $ANTLR start "set"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:261:1: set : NUMBER_SIGN LEFT_CURLY_BRACKET ( form )* RIGHT_CURLY_BRACKET ;
    public void set() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:261:4: ( NUMBER_SIGN LEFT_CURLY_BRACKET ( form )* RIGHT_CURLY_BRACKET )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:261:9: NUMBER_SIGN LEFT_CURLY_BRACKET ( form )* RIGHT_CURLY_BRACKET
            {
            	Match(input,NUMBER_SIGN,FOLLOW_NUMBER_SIGN_in_set1892); 
            	Match(input,LEFT_CURLY_BRACKET,FOLLOW_LEFT_CURLY_BRACKET_in_set1894); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:261:40: ( form )*
            	do 
            	{
            	    int alt10 = 2;
            	    int LA10_0 = input.LA(1);

            	    if ( (LA10_0 == OPEN_PAREN || (LA10_0 >= AMPERSAND && LA10_0 <= LEFT_SQUARE_BRACKET) || LA10_0 == LEFT_CURLY_BRACKET || (LA10_0 >= CIRCUMFLEX && LA10_0 <= SPECIAL_FORM) || (LA10_0 >= STRING && LA10_0 <= REGEX_LITERAL) || (LA10_0 >= NUMBER && LA10_0 <= BOOLEAN) || LA10_0 == SYMBOL || (LA10_0 >= KEYWORD && LA10_0 <= COMMENT) || LA10_0 == LAMBDA_ARG) )
            	    {
            	        alt10 = 1;
            	    }


            	    switch (alt10) 
            		{
            			case 1 :
            			    // C:\\Users\\Jon\\Desktop\\Clojure.g:261:40: form
            			    {
            			    	PushFollow(FOLLOW_form_in_set1896);
            			    	form();
            			    	state.followingStackPointer--;


            			    }
            			    break;

            			default:
            			    goto loop10;
            	    }
            	} while (true);

            	loop10:
            		;	// Stops C# compiler whining that label 'loop10' has no statements

            	Match(input,RIGHT_CURLY_BRACKET,FOLLOW_RIGHT_CURLY_BRACKET_in_set1899); 

            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "set"


    // $ANTLR start "metadataForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:264:1: metadataForm : NUMBER_SIGN CIRCUMFLEX ( map | SYMBOL | KEYWORD | STRING ) ;
    public void metadataForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:264:13: ( NUMBER_SIGN CIRCUMFLEX ( map | SYMBOL | KEYWORD | STRING ) )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:265:9: NUMBER_SIGN CIRCUMFLEX ( map | SYMBOL | KEYWORD | STRING )
            {
            	Match(input,NUMBER_SIGN,FOLLOW_NUMBER_SIGN_in_metadataForm1919); 
            	Match(input,CIRCUMFLEX,FOLLOW_CIRCUMFLEX_in_metadataForm1921); 
            	// C:\\Users\\Jon\\Desktop\\Clojure.g:265:32: ( map | SYMBOL | KEYWORD | STRING )
            	int alt11 = 4;
            	switch ( input.LA(1) ) 
            	{
            	case LEFT_CURLY_BRACKET:
            		{
            	    alt11 = 1;
            	    }
            	    break;
            	case SYMBOL:
            		{
            	    alt11 = 2;
            	    }
            	    break;
            	case KEYWORD:
            		{
            	    alt11 = 3;
            	    }
            	    break;
            	case STRING:
            		{
            	    alt11 = 4;
            	    }
            	    break;
            		default:
            		    NoViableAltException nvae_d11s0 =
            		        new NoViableAltException("", 11, 0, input);

            		    throw nvae_d11s0;
            	}

            	switch (alt11) 
            	{
            	    case 1 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:265:33: map
            	        {
            	        	PushFollow(FOLLOW_map_in_metadataForm1924);
            	        	map();
            	        	state.followingStackPointer--;


            	        }
            	        break;
            	    case 2 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:265:39: SYMBOL
            	        {
            	        	Match(input,SYMBOL,FOLLOW_SYMBOL_in_metadataForm1928); 

            	        }
            	        break;
            	    case 3 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:265:46: KEYWORD
            	        {
            	        	Match(input,KEYWORD,FOLLOW_KEYWORD_in_metadataForm1930); 

            	        }
            	        break;
            	    case 4 :
            	        // C:\\Users\\Jon\\Desktop\\Clojure.g:265:54: STRING
            	        {
            	        	Match(input,STRING,FOLLOW_STRING_in_metadataForm1932); 

            	        }
            	        break;

            	}


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "metadataForm"


    // $ANTLR start "varQuoteForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:268:1: varQuoteForm : NUMBER_SIGN APOSTROPHE form ;
    public void varQuoteForm() // throws RecognitionException [1]
    {   
        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:268:13: ( NUMBER_SIGN APOSTROPHE form )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:269:9: NUMBER_SIGN APOSTROPHE form
            {
            	Match(input,NUMBER_SIGN,FOLLOW_NUMBER_SIGN_in_varQuoteForm1953); 
            	Match(input,APOSTROPHE,FOLLOW_APOSTROPHE_in_varQuoteForm1955); 
            	PushFollow(FOLLOW_form_in_varQuoteForm1957);
            	form();
            	state.followingStackPointer--;


            }

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "varQuoteForm"


    // $ANTLR start "lambdaForm"
    // C:\\Users\\Jon\\Desktop\\Clojure.g:272:1: lambdaForm : NUMBER_SIGN list ;
    public void lambdaForm() // throws RecognitionException [1]
    {   

        this.inLambda = true;

        try 
    	{
            // C:\\Users\\Jon\\Desktop\\Clojure.g:279:5: ( NUMBER_SIGN list )
            // C:\\Users\\Jon\\Desktop\\Clojure.g:279:7: NUMBER_SIGN list
            {
            	Match(input,NUMBER_SIGN,FOLLOW_NUMBER_SIGN_in_lambdaForm1984); 
            	PushFollow(FOLLOW_list_in_lambdaForm1986);
            	list();
            	state.followingStackPointer--;


            }


            this.inLambda = false;

        }
        catch (RecognitionException re) 
    	{
            ReportError(re);
            Recover(input,re);
        }
        finally 
    	{
        }
        return ;
    }
    // $ANTLR end "lambdaForm"

    // Delegated rules


   	protected DFA4 dfa4;
	private void InitializeCyclicDFAs()
	{
    	this.dfa4 = new DFA4(this);
	}

    const string DFA4_eotS =
        "\x0a\uffff";
    const string DFA4_eofS =
        "\x0a\uffff";
    const string DFA4_minS =
        "\x01\x04\x04\uffff\x01\x04\x04\uffff";
    const string DFA4_maxS =
        "\x01\x26\x04\uffff\x01\x0f\x04\uffff";
    const string DFA4_acceptS =
        "\x01\uffff\x01\x01\x01\x02\x01\x03\x01\x04\x01\uffff\x01\x05\x01"+
        "\x06\x01\x07\x01\x08";
    const string DFA4_specialS =
        "\x0a\uffff}>";
    static readonly string[] DFA4_transitionS = {
            "\x01\x06\x01\uffff\x01\x04\x01\x06\x01\uffff\x01\x06\x02\uffff"+
            "\x02\x07\x01\x05\x01\x07\x01\x06\x01\uffff\x01\x02\x01\x08\x03"+
            "\uffff\x04\x02\x01\uffff\x01\x06\x03\uffff\x01\x02\x03\x07\x01"+
            "\x03\x01\uffff\x01\x01",
            "",
            "",
            "",
            "",
            "\x01\x08\x04\uffff\x01\x09\x02\uffff\x01\x06\x02\uffff\x01"+
            "\x08",
            "",
            "",
            "",
            ""
    };

    static readonly short[] DFA4_eot = DFA.UnpackEncodedString(DFA4_eotS);
    static readonly short[] DFA4_eof = DFA.UnpackEncodedString(DFA4_eofS);
    static readonly char[] DFA4_min = DFA.UnpackEncodedStringToUnsignedChars(DFA4_minS);
    static readonly char[] DFA4_max = DFA.UnpackEncodedStringToUnsignedChars(DFA4_maxS);
    static readonly short[] DFA4_accept = DFA.UnpackEncodedString(DFA4_acceptS);
    static readonly short[] DFA4_special = DFA.UnpackEncodedString(DFA4_specialS);
    static readonly short[][] DFA4_transition = DFA.UnpackEncodedStringArray(DFA4_transitionS);

    protected class DFA4 : DFA
    {
        public DFA4(BaseRecognizer recognizer)
        {
            this.recognizer = recognizer;
            this.decisionNumber = 4;
            this.eot = DFA4_eot;
            this.eof = DFA4_eof;
            this.min = DFA4_min;
            this.max = DFA4_max;
            this.accept = DFA4_accept;
            this.special = DFA4_special;
            this.transition = DFA4_transition;

        }

        override public string Description
        {
            get { return "188:1: form : ({...}? LAMBDA_ARG | literal | COMMENT | AMPERSAND | ( metadataForm )? ( SPECIAL_FORM | s= SYMBOL | list | vector | map ) | macroForm | dispatchMacroForm | set );"; }
        }

    }

 

    public static readonly BitSet FOLLOW_set_in_literal0 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_form_in_file1319 = new BitSet(new ulong[]{0x0000005F178DF2D2UL});
    public static readonly BitSet FOLLOW_LAMBDA_ARG_in_form1352 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_literal_in_form1363 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_COMMENT_in_form1389 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_AMPERSAND_in_form1399 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_metadataForm_in_form1409 = new BitSet(new ulong[]{0x0000000010014290UL});
    public static readonly BitSet FOLLOW_SPECIAL_FORM_in_form1414 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SYMBOL_in_form1420 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_list_in_form1426 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_vector_in_form1430 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_map_in_form1434 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_macroForm_in_form1446 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_dispatchMacroForm_in_form1456 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_set_in_form1466 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_quoteForm_in_macroForm1497 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_metaForm_in_macroForm1507 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_derefForm_in_macroForm1517 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_syntaxQuoteForm_in_macroForm1527 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_unquoteSplicingForm_in_macroForm1537 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_unquoteForm_in_macroForm1547 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_REGEX_LITERAL_in_dispatchMacroForm1574 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_varQuoteForm_in_dispatchMacroForm1584 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_lambdaForm_in_dispatchMacroForm1596 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_OPEN_PAREN_in_list1617 = new BitSet(new ulong[]{0x0000005F178DF2F0UL});
    public static readonly BitSet FOLLOW_form_in_list1619 = new BitSet(new ulong[]{0x0000005F178DF2F0UL});
    public static readonly BitSet FOLLOW_CLOSE_PAREN_in_list1625 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LEFT_SQUARE_BRACKET_in_vector1644 = new BitSet(new ulong[]{0x0000005F178DF3D0UL});
    public static readonly BitSet FOLLOW_form_in_vector1646 = new BitSet(new ulong[]{0x0000005F178DF3D0UL});
    public static readonly BitSet FOLLOW_RIGHT_SQUARE_BRACKET_in_vector1649 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_LEFT_CURLY_BRACKET_in_map1668 = new BitSet(new ulong[]{0x0000005F178DF6D0UL});
    public static readonly BitSet FOLLOW_form_in_map1671 = new BitSet(new ulong[]{0x0000005F178DF6D0UL});
    public static readonly BitSet FOLLOW_form_in_map1673 = new BitSet(new ulong[]{0x0000005F178DF6D0UL});
    public static readonly BitSet FOLLOW_RIGHT_CURLY_BRACKET_in_map1677 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_APOSTROPHE_in_quoteForm1710 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_quoteForm1712 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_CIRCUMFLEX_in_metaForm1726 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_metaForm1728 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_COMMERCIAL_AT_in_derefForm1745 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_derefForm1747 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SYNTAX_QUOTE_in_syntaxQuoteForm1787 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_syntaxQuoteForm1789 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_UNQUOTE_in_unquoteForm1829 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_unquoteForm1831 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_UNQUOTE_SPLICING_in_unquoteSplicingForm1871 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_unquoteSplicingForm1873 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_SIGN_in_set1892 = new BitSet(new ulong[]{0x0000000000000200UL});
    public static readonly BitSet FOLLOW_LEFT_CURLY_BRACKET_in_set1894 = new BitSet(new ulong[]{0x0000005F178DF6D0UL});
    public static readonly BitSet FOLLOW_form_in_set1896 = new BitSet(new ulong[]{0x0000005F178DF6D0UL});
    public static readonly BitSet FOLLOW_RIGHT_CURLY_BRACKET_in_set1899 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_SIGN_in_metadataForm1919 = new BitSet(new ulong[]{0x0000000000001000UL});
    public static readonly BitSet FOLLOW_CIRCUMFLEX_in_metadataForm1921 = new BitSet(new ulong[]{0x0000000110054290UL});
    public static readonly BitSet FOLLOW_map_in_metadataForm1924 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_SYMBOL_in_metadataForm1928 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_KEYWORD_in_metadataForm1930 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_STRING_in_metadataForm1932 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_SIGN_in_varQuoteForm1953 = new BitSet(new ulong[]{0x0000000000008000UL});
    public static readonly BitSet FOLLOW_APOSTROPHE_in_varQuoteForm1955 = new BitSet(new ulong[]{0x0000005F178DF2D0UL});
    public static readonly BitSet FOLLOW_form_in_varQuoteForm1957 = new BitSet(new ulong[]{0x0000000000000002UL});
    public static readonly BitSet FOLLOW_NUMBER_SIGN_in_lambdaForm1984 = new BitSet(new ulong[]{0x0000000000000010UL});
    public static readonly BitSet FOLLOW_list_in_lambdaForm1986 = new BitSet(new ulong[]{0x0000000000000002UL});

}
