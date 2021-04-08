# Ronin Grammar

## Token Classes

$identifier: (letter | "_" | "$"){(letter | "_" | "$")}
$number: digit{digit}
$string: '"'{nodoublequotes}'"'

## Grammar G(Ronin)

/*
Ronin                       =   [ModuleDef] MainDef
ModuleDef                  =   "module" $identifier ";"
MainDef                     =   "fun" " " ("Main" | "main") "(" ")" [TypeDef] Block

TypeDef                     =   ":" Type
Type                        =   "void" | "int"

Block                       =   BracketBlock | ArrowBlock
ArrowBlock                  =   "=" ">" Statement
BracketBlock                =   "{" {Statement} "}"

Statement                   =   VariableDeclaration ";"

VariableDeclaration         =   ("var" | "val") $identifier ([TypeDef] "=" Expression | TypeDef)
*/

Ronin			    =	Expression

Expression                  =   AddExpression
AddExpression		    =	Term {AddOp Term}
Term			    = 	Factor {MulOp Factor}
Factor			    =	$number | AddOp Factor | "(" Expression ")"

AddOp			    =	"+" | "-"
MulOp			    =	"*" | "/"