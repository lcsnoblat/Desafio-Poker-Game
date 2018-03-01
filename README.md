# Desafio-Poker-Game

O desafio é um simples jogo de poker, em que são dados duas mãos de cartas para o jogador "player" e o "computer", a seguir o sistema avalia a mão dos jogadores e informa qual foi o jogador vencedor!

# Como funciona

Bom, quando o usuário inicia o projeto e abre o navegador, no controller da aplicação é iniciado o método para criar as cartas, a classe Cards possui dois Enum's um para os números e o outro para o tipo, e então, na classe DeckOfCards são criadas 52 cartas e o próximo método é chamado, para embaralhar as mesmas. 
Logo depois em outra classe que herda de DeckOfCards, é feita a validação que informa o vencedor!

