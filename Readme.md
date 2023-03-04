# RoninLang

A programming language

## Current State

The Ronin Conpiler currently converts the Ronin source code into JavaScript source code.

```
# input on the REPL console
ronin > var i = 0;

# output
let i = 0

# input on the REPL console
ronin > while (i < 9) { var j = i; while(j <= 8) { j = j + 1; } i = i + 1; }

# output
while (i < 9) {
	let j = i;
     
     	while (j <= 8) {
		j = j + 1;
	}
     
     i = i + 1;
}
``` 

## Resources

* [https://github.com/mwerezak/sphinx-lang](https://github.com/mwerezak/sphinx-lang)
* [https://github.com/Bauepete/no-beard](https://github.com/Bauepete/no-beard)
* [https://www.youtube.com/playlist?list=PLZQftyCk7_SdoVexSmwy_tBgs7P0b97yD](https://www.youtube.com/playlist?list=PLZQftyCk7_SdoVexSmwy_tBgs7P0b97yD)