# Ronin Grammar

## Token Classes

$identifier: (letter | "_" | "$"){(letter | "_" | "$")}
$number: digit{digit}
$string: '"'{nodoublequotes}'"'

## Grammar G(Ronin)

Ronin			    =	Statement

Statement           =   VariableDeclaration ";" | Expression ";" | If

Block               =   "{" { Statement } "}"

VariableDeclaration =   "var" $identifier "=" Expression
VariableAccess      =   $identifier

If                  =   "if" "(" Expression ")" Block ["else" Block]

Expression          =   NotOp CompExpression | CompExpression [BoolOP CompExpression]
CompExpression      =   ArithExpression [RelOP ArithExpression]
ArithExpression		=	Term {AddOp Term}
Term			    = 	Factor {MulOp Factor}
Factor			    =	$number | VariableAccess | AddOp Factor | "(" Expression ")"

[Inline] NotOp      =   "!"
[Inline] BoolOp     =   "&&" | "||"
[Inline] RelOp      =   "<" | "<=" | "==" | "=>" | ">" | "!="
[Inline] AddOp		=	"+" | "-"
[Inline] MulOp		=	"*" | "/"