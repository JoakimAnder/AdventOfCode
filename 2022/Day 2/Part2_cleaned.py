from Game import Game
INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1]

OPPONENT_MOVE_PARSER = {
        'A': Game.Move.rock,
        'B': Game.Move.paper,
        'C': Game.Move.scissors,
}
GAME_RESULT_PARSER = {
        'X': Game.Result.lose,
        'Y': Game.Result.draw,
        'Z': Game.Result.win,
}

def ParseInput(input: str) -> Game:
    (inputOpponentMove, inputGameResult) = input.split(' ')
    opponentMove = OPPONENT_MOVE_PARSER[inputOpponentMove]
    result = GAME_RESULT_PARSER[inputGameResult]
    return Game(opponentMove=opponentMove, result=result)

pointSum = 0

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        game = ParseInput(line)
        pointSum += game.getPoints()

print('Score:', pointSum)
