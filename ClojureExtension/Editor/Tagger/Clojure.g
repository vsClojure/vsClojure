/************************************************************************** ***** 
 * Copyright (c) 2009 Laurent PETIT. 
 * All rights reserved. This program and the accompanying materials 
 * are made available under the terms of the Eclipse Public License v1.0 
 * which accompanies this distribution, and is available at 
 * http://www.eclipse.org/legal/epl-v10.html 
 * 
 * Contributors: 
 *    Laurent PETIT - Initial implementation 
 *    jmis          - Adaptation to C# 
 *************************************************************************** ****/ 

grammar Clojure;

options { language=CSharp2; }

@members
{
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
}

/*
 * Lexer part
 */
 
OPEN_PAREN: '('
 	;
CLOSE_PAREN: ')'
	;
AMPERSAND: '&'
        ;
LEFT_SQUARE_BRACKET: '['
        ;
RIGHT_SQUARE_BRACKET: ']'
        ;
LEFT_CURLY_BRACKET: '{'
        ; 
RIGHT_CURLY_BRACKET: '}'
        ;
BACKSLASH: '\\'
        ;
CIRCUMFLEX: '^'
        ;
COMMERCIAL_AT: '@'
        ;
NUMBER_SIGN: '#'
        ;
APOSTROPHE: '\''
        ;
        
// TODO complete this list
SPECIAL_FORM: 'def' | 'if' | 'do' | 'let' | 'quote' | 'var' | 'fn' | 'loop' |
            'recur' | 'throw' | 'try' | 'monitor-enter' | 'monitor-exit' |
            'new' | 'set!' | '.'
    ;

// taken from the java grammar example of Terrence Parr
STRING
    :  '"' ( EscapeSequence | ~('\\'|'"') )* '"'
    ;

REGEX_LITERAL
    : NUMBER_SIGN '"' ( ~('\\' | '"') | '\\' . )* '"'
    ;

// taken from the java grammar example of Terrence Parr
fragment
EscapeSequence
    :   '\\' ('b'|'t'|'n'|'f'|'r'|'\"'|'\''|'\\')
    |   UnicodeEscape
    |   OctalEscape
    ;

// taken from the java grammar example of Terrence Parr
fragment
UnicodeEscape
    :   '\\' 'u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT
    ;

// taken from the java grammar example of Terrence Parr
fragment
OctalEscape
    :   '\\' ('0'..'3') ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7') ('0'..'7')
    |   '\\' ('0'..'7')
    ;

// TODO get the real definition from a java grammar.
// FIXME for the moment, allow just positive integers to start playing with the grammar
NUMBER: '-'? '0'..'9'+ ('.' '0'..'9'+)? (('e'|'E') '-'? '0'..'9'+)?
    ;

CHARACTER:
        '\\newline'
    |   '\\space'
    |   '\\tab'
    |   '\\u' HEXDIGIT HEXDIGIT HEXDIGIT HEXDIGIT
    |   BACKSLASH .   // TODO : is it correct to allow anything ?
    ;

HEXDIGIT:
        '0'..'9' | 'a'..'f' | 'A'..'F';
        
NIL:    'nil'
    ;
    
BOOLEAN:
        'true'
    |   'false'
    ;

SYMBOL:
        '/' // The division function FIXME is it necessary to hardcode this ?
    |   NAME ('/' NAME)?
    ;

METADATA_TYPEHINT:
		NUMBER_SIGN* CIRCUMFLEX ( 'ints' | 'floats' | 'longs' | 'doubles' | 'objects' | NAME | STRING )*
	;
	
fragment
NAME:   SYMBOL_HEAD SYMBOL_REST* (':' SYMBOL_REST+)*
    ;

fragment
SYMBOL_HEAD:   
        'a'..'z' | 'A'..'Z' | '*' | '+' | '!' | '-' | '_' | '?' | '>' | '<' | '=' | '$'
        // other characters will be allowed eventually, but not all macro characters have been determined
    ;

fragment
SYMBOL_REST:
        SYMBOL_HEAD
    |   '0'..'9' // Done this because a strange cannot find matchRange symbol occured when compiling the parser
    |   '.' // multiple successive points is allowed by the reader (but will break at evaluation)
    |   NUMBER_SIGN // FIXME normally # is allowed only in syntax quote forms, in last position
    ;

literal:
        STRING //-> template(it={$STRING.text}) "<span style='color: red ; '>$it$</span>" 
    |   NUMBER
    |   CHARACTER
    |   NIL
    |   BOOLEAN
    |   KEYWORD
    ;    

KEYWORD:
        ':' SYMBOL
    ;

SYNTAX_QUOTE:
        '`'
    ;
    
UNQUOTE_SPLICING:
        '~@'
    ;
    
UNQUOTE:    
        '~'
    ;
    
COMMENT:
        ';' ~('\r' | '\n')* ('\r'? '\n')? {$channel=HIDDEN;}  //{skip();} // FIXME should use NEWLINE but NEWLINE has a problem I don't understand for the moment
    ;

SPACE:  (' '|'\t'|','|'\r'|'\n')+ {$channel=HIDDEN;} // FIXME should use NEWLINE but NEWLINE has a problem I don't understand for the moment
    ;

// TODO how many
LAMBDA_ARG:
        '%' '1'..'9' '0'..'9'*
    |   '%&'
    |   '%'
    ;
    
/*
 * Parser part
 */

file:   
        ( form  { Console.WriteLine("form found"); }  )*
    ;
    
// Note : dispatch macros are hardwired in clojure
form	:	   
	 {this.inLambda}? LAMBDA_ARG
    |    literal // Place literal first to make nil and booleans take precedence over symbol (impossible to 
                // name a symbol nil, true or false)
    |	COMMENT
    |   AMPERSAND
    |   metadataForm? ( SPECIAL_FORM | s=SYMBOL { symbols.Add(s.Text); } | list | vector | map )
    |   macroForm
    |   dispatchMacroForm
    |   set
    ;
        
macroForm:   
        quoteForm
    |   metaForm
    |   derefForm
    |   syntaxQuoteForm
    |	{ this.syntaxQuoteDepth > 0 }? unquoteSplicingForm
    |	{ this.syntaxQuoteDepth > 0 }? unquoteForm
    ;
    
dispatchMacroForm:   
        REGEX_LITERAL
    |   varQuoteForm
    |   {!this.inLambda}? lambdaForm // contraction for anonymousFunction
    ;
    
list:   o=OPEN_PAREN form * c=CLOSE_PAREN
{
  parensMatching.Add(o.TokenIndex, c.TokenIndex);
  parensMatching.Add(c.TokenIndex, o.TokenIndex);
}
    ;
    
vector:  LEFT_SQUARE_BRACKET form* RIGHT_SQUARE_BRACKET
    ;
    
map:    LEFT_CURLY_BRACKET (form form)* RIGHT_CURLY_BRACKET
    ;
    
quoteForm
@init  { this.syntaxQuoteDepth++; }
@after { this.syntaxQuoteDepth--; }
    :  APOSTROPHE form
    ;

metaForm:   CIRCUMFLEX form
    ;
    
derefForm:  COMMERCIAL_AT form
    ;
    
syntaxQuoteForm
@init  { this.syntaxQuoteDepth++; }
@after { this.syntaxQuoteDepth--; }
    :
        SYNTAX_QUOTE form
    ;
    
unquoteForm
@init  { this.syntaxQuoteDepth--; }
@after { this.syntaxQuoteDepth++; }
    :
        UNQUOTE form
    ;
    
unquoteSplicingForm
@init  { this.syntaxQuoteDepth--; }
@after { this.syntaxQuoteDepth++; }
    :
        UNQUOTE_SPLICING form
    ;
    
set:    NUMBER_SIGN LEFT_CURLY_BRACKET form* RIGHT_CURLY_BRACKET
    ;

metadataForm:
        NUMBER_SIGN CIRCUMFLEX (map | SYMBOL|KEYWORD|STRING)
    ;

varQuoteForm:
        NUMBER_SIGN APOSTROPHE form
    ;

lambdaForm
@init {
this.inLambda = true;
}
@after {
this.inLambda = false;
}
    : NUMBER_SIGN list
    ;