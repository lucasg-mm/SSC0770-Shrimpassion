# Para usuarios com nixos
Basta ter flakes ativado em seu configuration.nix
descrição dos comandos:
> $ nix develop

entra em um shell configurado com todos as dependencias

> $ nix build

compila o pdf gerando um arquivo result/document.pdf

# Para usuarios de outras distros:
Dependencias:
- lualatex
- scheme-medium
- framed
- titlesec
- cleveref
- multirow
- wrapfig
- tabu
- threeparttable
- threeparttablex
- makecell
- environ
- biblatex
- biber
- fvextra
- upquote
- catchfile
- xstring
- csquotes
- minted
- dejavu
- comment
- footmisc
- xltabular
- ltablex

com excessão do lualatex todos os outros devem estar incluidos em um pacote latex

No ubunto e derivados do debian:

> $ sudo apt install texlive-full lualatex

## Compilando:
Tendo todos os requisitos em sua maquina basta executar o comando:
> $ lualatex -shell-escape *.tex

Esse comando ira gerar um 000-main.pdf, o qual pode ser lido pelo seu leitor de PDF padrão
(Caso não tenha um recomendo o zathura)
