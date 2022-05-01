# Ronin Grammar

## Token Classes

$identifier: (letter | "_" | "$"){(letter | "_" | "$")}
$number: digit{digit}
$string: '"'{nodoublequotes}'"'

## Grammar G(Ronin)

Ronin			    =	Expression | Statement

Statement           =   VariableDeclaration ";"

VariableDeclaration =   "var" $identifier "=" Expression
VariableAccess      =   $identifier

Expression          =   AddExpression
AddExpression		=	Term {AddOp Term}
Term			    = 	Factor {MulOp Factor}
Factor			    =	$number | VariableAccess | AddOp Factor | "(" Expression ")"

[Inline] AddOp		=	"+" | "-"
[Inline] MulOp		=	"*" | "/"