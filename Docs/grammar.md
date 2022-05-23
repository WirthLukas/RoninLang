# Ronin Grammar

## Token Classes

$identifier: (letter | "_" | "$"){(letter | "_" | "$")}
$number: digit{digit}
$string: '"'{nodoublequotes}'"'

## Grammar G(Ronin)

Ronin			    =	Statement

Statement           =   VariableDeclaration ";"
                        | VariableAssignment ";"
                        | Expression ";"
                        | IfStatement
                        | WhileStatement
                        | DoWhileStatement

Block               =   "{" { Statement } "}"

VariableDeclaration =   "var" $identifier "=" Expression
VariableAccess      =   $identifier
VariableAssignment  =   VariableAccess AssignmentOp Expression

IfStatement         =   If { "else" If } [ Else ]
If                  =   "if" Condition Block
Else                =   "else" Block
[Inline] Condition  =   "(" Expression ")"

WhileStatement      =   "while" Condition Block
DoWhileStatement    =   "do" Block "while" Condition

Expression          =   NotOp CompExpression
                        | CompExpression {BoolOP CompExpression}
CompExpression      =   ArithExpression [RelOP ArithExpression] | BoolValue
ArithExpression		=	Term {AddOp Term}
Term			    = 	Factor {MulOp Factor}
Factor			    =	$number | VariableAccess | AddOp Factor | "(" Expression | VariableAssignment ")"

[Inline] BoolValue           =   "true" | "false"

[Inline] AssignmentOp   = "=" // todo add "+=" "-=" and so on
[Inline] NotOp      =   "!"
[Inline] BoolOp     =   "&&" | "||"
[Inline] RelOp      =   "<" | "<=" | "==" | "=>" | ">" | "!="
[Inline] AddOp		=	"+" | "-"
[Inline] MulOp		=	"*" | "/"