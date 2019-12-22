## 2º Projeto de Linguagens de Programação II 2019/2020

### Autores

*[João Rebelo - a21805230](https://github.com/JBernardoRebelo)*

*[Miguel Fernández - a21803644](https://github.com/MizuRyujin)*

### Repositório Git

[2º Projeto LPII, Fernandez
 e Rebelo](https://github.com/JBernardoRebelo/Projeto2_LPII_Fernandez_Rebelo)

### Quem fez o quê

## Controlos
- 'WASD' para mover e rodar o personagem;
- 'Space' ou '1' para atacar;
- 'C' para mostrar detalhes do personagem;
- 'Esc' para aceder ao menu de pausa;
- (Por agora) Escrever a opção pretendida dentro de menus;  

## Descrição e arquitetura da Solução

## Diagrama UML

![](DiabloUml.png)

## Referências

#### *[.NET API](https://docs.microsoft.com/en-us/dotnet/api/?view=netcore-2.2)*

- *[Vector2 Struct](https://docs.microsoft.com/en-us/dotnet/api/system.numerics.vector2?view=netframework-4.8)*

## **Notas de dev:**

### **De Essencial implementação:**

- Implementação o Game Loop e o Update Method.
- Duas threads: a thread principal do jogo (que executa o game loop) e uma thread para ler input do utilizador.
- Implementar as mecânicas base do jogo original.
  - Movimento ´WASD´;
  - 1 Ataque skill;
  - 1 Inimigo atacável;
  - 1 coletável a definir;
  - 1 dungeon explorável;
- Ser jogável e ter algum tipo de pontuação.
  - XP que se ganha ao matar inimigo;
  - Ter algum tipo de ecrã inicial ou opção no menu onde sejam explicadas 
  as regras e indicados os controlos do jogo;
  - Menu de escolha de classes com 2 classes, paladin e assassin;
  - Regras e controlos;

### **Mecânicas omitidas de desenvolvimento que poderiam ser implementados**

- Sistema de inventário e Lojas/Transações de Gold
  - De momento só existe 1 item possível e é a _Short Sword_ usada para atacar;
  - Ver detalhes de personagem mostra também a arma e os seus detalhes
- Classe _Sorcerer_;
- Várias _stats_ de classe;
- Interações e existência de NPC's;
- Controlo e ataque de jogador com rato (utilizar o teclado pareceu mais prático de implementar);
