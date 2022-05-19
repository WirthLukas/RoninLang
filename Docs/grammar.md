# Ronin Grammar

## Token Classes

$identifier: (letter | "_" | "$"){(letter | "_" | "$")}
$number: digit{digit}
$string: '"'{nodoublequotes}'"'

## Grammar G(Ronin)

Ronin			    =	Statement

Statement           =   VariableDeclaration ";"
                        | Expression ";"
                        | IfStatement
                        | WhileStatement
                        | DoWhileStatement

Block               =   "{" { Statement } "}"

VariableDeclaration =   "var" $identifier "=" Expression
VariableAccess      =   $identifier

IfStatement         =   If { "else" If } [ Else ]
If                  =   "if" Condition Block
Else                =   "else" Block
[Inline] Condition  =   "(" Expression ")"

WhileStatement      =   "while" Condition Block
DoWhileStatement    =   "do" Block "while" Condition

Expression          =   NotOp CompExpression
                        | CompExpression [BoolOP CompExpression]
CompExpression      =   ArithExpression [RelOP ArithExpression] | BoolValue
ArithExpression		=	Term {AddOp Term}
Term			    = 	Factor {MulOp Factor}
Factor			    =	$number | VariableAccess | AddOp Factor | "(" Expression ")"

[Inline] BoolValue           =   "true" | "false"

[Inline] NotOp      =   "!"
[Inline] BoolOp     =   "&&" | "||"
[Inline] RelOp      =   "<" | "<=" | "==" | "=>" | ">" | "!="
[Inline] AddOp		=	"+" | "-"
[Inline] MulOp		=	"*" | "/"