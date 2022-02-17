grammar ABLProgress;

UNKNOWN_VALUE: QUESTION_MARK;
QUESTION_MARK: '?';
VALID_SEPARATORS: DASH | UNDERSCORE;

/* * Parser Rules */
script: instruction*;

instruction:
	action (
		sequenceObject
		| tableObject
		| fieldObject
		| indexObject
	);

action: KW_ADD | KW_ALTER | KW_DROP;

sequenceCycleOnLimitValue: KW_YES | KW_NO;
sequenceIncrementValue: INT;
sequenceInitialValue: INT;
sequenceMinValue: INT | UNKNOWN_VALUE;
sequenceForeignName:
	STRING_DELIMITER* unquotedString STRING_DELIMITER*;
sequenceForeigOwner:
	STRING_DELIMITER* unquotedString STRING_DELIMITER*;
sequenceObject: KW_SEQUENCE objectName sequenceOptions*;
sequenceOptions:
	KW_CYCLE_ON_LIMIT sequenceCycleOnLimitValue
	| KW_MIN_VAL sequenceMinValue
	| KW_INITIAL sequenceInitialValue
	| KW_INCREMENT sequenceIncrementValue
	| KW_FOREIGN_NAME sequenceForeignName
	| KW_FOREIGN_OWNER sequenceForeigOwner;

indexObject:
	KW_INDEX objectName KW_ON indexOnValue indexOptions* indexFields+;

indexOnValue: objectName;
indexForeignNameValue: quotedString;
indexNumValue: INT;
indexFieldValue: objectName;
indexFields:
	KW_INDEX_FIELD indexFieldValue (
		KW_INDEX_SORT_ASC
		| KW_INDEX_SORT_DESC
	);
indexOptions:
	KW_AREA indexAreavalue
	| KW_UNIQUE
	| KW_PRIMARY
	| KW_FOREIGN_NAME indexForeignNameValue
	| KW_INDEX_NUM indexNumValue;

indexAreavalue: quotedString;

tableObject:
	KW_TABLE objectName (KW_TYPE tableType)? tableOptions*;
tableOptions:
	KW_AREA tableAreaValue
	| KW_DUMP_NAME tableDumpNameValue
	| KW_FOREIGN_NAME tableForeignNameValue
	| KW_FOREIGN_TYPE foreignTypeValues
	| KW_FOREIGN_OWNER tableForeignOwnerValue
	| KW_PROGRESS_RECID tableProgressRecidValue;

tableAreaValue: quotedString;
tableDumpNameValue: objectName;
tableForeignNameValue: objectName;
tableForeignOwnerValue: objectName;
tableProgressRecidValue: INT;
tableType: unquotedString;

fieldObject:
	KW_FIELD objectName KW_OF fieldOfValue KW_AS fieldDataTypeValue fieldOptions*;

fieldDataTypeValue: dataTypes;
fieldForeignCodeValue: INT;
fieldForeignNameValue: objectName;
fieldForeignPosValue: INT;
fieldFormatValue: fieldFormatMask;
fieldInitialValue:
	(STRING_DELIMITER* unquotedString STRING_DELIMITER*)
	| QUESTION_MARK;
fieldMisc13Value: INT;
fieldMisc14Value: INT;
fieldMisc15Value: INT;
fieldOfValue: objectName;
fieldClobCodePageValue: quotedString;
fieldClobCollationValue: quotedString;
fieldClobTypeValue: INT;
fieldOptions:
	KW_POSITION fieldPositionValue
	| KW_FORMAT fieldFormatValue
	| KW_INITIAL fieldInitialValue
	| KW_ORDER fieldOrderValue
	| KW_FOREIGN_POS fieldForeignPosValue
	| KW_FOREIGN_NAME fieldForeignNameValue
	| KW_FOREIGN_TYPE foreignTypeValues
	| KW_FIELD_MISC15 fieldMisc15Value
	| KW_FIELD_MISC14 fieldMisc14Value
	| KW_FIELD_MISC13 fieldMisc13Value
	| KW_SHADOW_COL fieldShadowColValue
	| KW_FOREIGN_CODE fieldForeignCodeValue
	| KW_CLOB_CODEPAGE fieldClobCodePageValue
	| KW_CLOB_COLLATION fieldClobCollationValue
	| KW_CLOB_TYPE fieldClobTypeValue;

fieldOrderValue: INT;
fieldPositionValue: INT;
fieldShadowColValue: quotedString;

objectName:
	STRING_DELIMITER LETTER (LETTER | VALID_SEPARATORS | INT)* STRING_DELIMITER;

foreignTypeValues:
	STRING_DELIMITER KW_TABLE STRING_DELIMITER
	| STRING_DELIMITER KW_NUMBER STRING_DELIMITER
	| STRING_DELIMITER KW_CHARACTER STRING_DELIMITER
	| STRING_DELIMITER KW_CHAR STRING_DELIMITER
	| STRING_DELIMITER KW_DATE STRING_DELIMITER
	| STRING_DELIMITER KW_CLOB STRING_DELIMITER;

quotedString: STRING_DELIMITER unquotedString STRING_DELIMITER;

unquotedString: (
		LETTER
		| INT
		| VALID_SEPARATORS
		| PARENTHESIS_OPEN
		| PARENTHESIS_CLOSE
		| SLASH
		| HASH
		| BACKSLASH
		| UNKNOWN_VALUE
	)*;

fieldFormatMask:
	STRING_DELIMITER (
		LETTER
		| INT
		| HASH
		| DOLLAR
		| DOT
		| SLASH
		| BACKSLASH
		| PARENTHESIS_OPEN
		| PARENTHESIS_CLOSE
		| DASH
		| GREATER_THAN
		| LESS_THAN
		| COMMA
	)* STRING_DELIMITER;

dataTypes:
	KW_INTEGER
	| KW_LOGICAL
	| KW_CHARACTER
	| KW_DATE
	| KW_DECIMAL
	| KW_CLOB;

KW_UNIQUE: U N I Q U E;
KW_PRIMARY: P R I M A R Y;
KW_INDEX_NUM: I N D E X '-' N U M;
KW_INDEX_FIELD: I N D E X '-' F I E L D;
KW_INDEX_SORT_ASC: A S C (E N D I N G)?;
KW_INDEX_SORT_DESC: D E S C (E N D I N G)?;
KW_ADD: A D D;
KW_ALTER: A L T E R;
KW_AREA: A R E A;
KW_AS: A S;
KW_CHAR: C H A R;
KW_CHARACTER: C H A R A C T E R;
KW_CLOB_CODEPAGE: C L O B '-' C O D E P A G E;
KW_CLOB_COLLATION: C L O B '-' C O L L A T I O N;
KW_CLOB_TYPE: C L O B '-' T Y P E;
KW_CLOB: C L O B;
KW_CYCLE_ON_LIMIT: C Y C L E '-' O N '-' L I M I T;
KW_DATE: D A T E;
KW_DECIMAL: D E C I M A L;
KW_DROP: D R O P;
KW_DUMP_NAME: D U M P '-' N A M E;
KW_FIELD_MISC13: F I E L D '-' M I S C '13';
KW_FIELD_MISC14: F I E L D '-' M I S C '14';
KW_FIELD_MISC15: F I E L D '-' M I S C '15';
KW_FIELD: F I E L D;
KW_FOREIGN_CODE: F O R E I G N '-' C O D E;
KW_FOREIGN_NAME: F O R E I G N '-' N A M E;
KW_FOREIGN_OWNER: F O R E I G N '-' O W N E R;
KW_FOREIGN_POS: F O R E I G N '-' P O S;
KW_FOREIGN_TYPE: F O R E I G N '-' T Y P E;
KW_FORMAT: F O R M A T;
KW_INCREMENT: I N C R E M E N T;
KW_INDEX: I N D E X;
KW_INITIAL: I N I T I A L;
KW_INTEGER: I N T E G E R;
KW_LOGICAL: L O G I C A L;
KW_MIN_VAL: M I N '-' V A L;
KW_NO: N O;
KW_NUMBER: N U M B E R;
KW_OF: O F;
KW_ON: O N;
KW_ORDER: O R D E R;
KW_POSITION: P O S I T I O N;
KW_PROGRESS_RECID: P R O G R E S S '-' R E C I D;
KW_SEQUENCE: S E Q U E N C E;
KW_SHADOW_COL: S H A D O W '-' C O L;
KW_TABLE: T A B L E;
KW_TYPE: T Y P E;
KW_YES: Y E S;

COMMA: ',';
BACKSLASH: '\\';
DOLLAR: '$';
DOT: '\\.';
GREATER_THAN: '>';
HASH: '#';
INT: DIGIT+;
LESS_THAN: '<';
LETTER: CHAR_+;
LOGICAL_VALUE: Y E S | N O | UNKNOWN_VALUE;
PARENTHESIS_CLOSE: ')';
PARENTHESIS_OPEN: '(';
SLASH: SLASH_;
STRING_DELIMITER: QUOTE_;
DASH: '-';
UNDERSCORE: '_';

WS: [ \t\r\n\f] -> channel(HIDDEN);

fragment NEW_LINE: [\n] | [\r];
fragment BACKSLASH_: '\\';
fragment SPACE: ' ';
fragment DIGIT: '0' ..'9';
fragment CHAR_: 'a' ..'z' | 'A' ..'Z';
fragment SLASH_: '/';
fragment QUOTE_: '"';
fragment A: ('a' | 'A');
fragment B: ('b' | 'B');
fragment C: ('c' | 'C');
fragment D: ('d' | 'D');
fragment E: ('e' | 'E');
fragment F: ('f' | 'F');
fragment G: ('g' | 'G');
fragment H: ('h' | 'H');
fragment I: ('i' | 'I');
fragment J: ('j' | 'J');
fragment K: ('k' | 'K');
fragment L: ('l' | 'L');
fragment M: ('m' | 'M');
fragment N: ('n' | 'N');
fragment O: ('o' | 'O');
fragment P: ('p' | 'P');
fragment Q: ('q' | 'Q');
fragment R: ('r' | 'R');
fragment S: ('s' | 'S');
fragment T: ('t' | 'T');
fragment U: ('u' | 'U');
fragment V: ('v' | 'V');
fragment W: ('w' | 'W');
fragment X: ('x' | 'X');
fragment Y: ('y' | 'Y');
fragment Z: ('z' | 'Z');