from Game import Game
INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1]

OPPONENT_MOVE_PARSER = {
        'A': Game.Move.rock,
        'B': Game.Move.paper,
        'C': Game.Move.scissors,
}
PLAYER_MOVE_PARSER = {
        'X': Game.Move.rock,
        'Y': Game.Move.paper,
        'Z': Game.Move.scissors,
}

def ParseInput(input: str) -> Game:
    (inputOpponentMove, inputPlayerMove) = input.split(' ')
    opponentMove = OPPONENT_MOVE_PARSER[inputOpponentMove]
    playerMove = PLAYER_MOVE_PARSER[inputPlayerMove]
    return Game(opponentMove=opponentMove, playerMove=playerMove)

pointSum = 0

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        game = ParseInput(line)
        pointSum += game.getPoints()

print('Score:', pointSum)







