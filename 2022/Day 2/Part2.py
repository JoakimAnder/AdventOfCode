ROCK = 'ROCK'
PAPER = 'PAPER'
SCISSORS = 'SCISSORS'

LOSE = 'LOSE'
DRAW = 'DRAW'
WIN = 'WIN'

playPoints = {
    ROCK: 1,
    PAPER: 2,
    SCISSORS: 3,
}

gameResultPoints = {
        LOSE: 0,
        DRAW: 3,
        WIN: 6,
}

gameMove = {
    ROCK: {
        LOSE: SCISSORS,
        DRAW: ROCK,
        WIN: PAPER,
    },
    PAPER: {
        LOSE: ROCK,
        DRAW: PAPER,
        WIN: SCISSORS,
    },
    SCISSORS: {
        LOSE: PAPER,
        DRAW: SCISSORS,
        WIN: ROCK,
    },
}

opponentChoices = {
        'A': ROCK,
        'B': PAPER,
        'C': SCISSORS,
}
gameResultChoices = {
        'X': LOSE,
        'Y': DRAW,
        'Z': WIN,
}

def ConvertInputOpponentChoice(input):
        return opponentChoices[input]

def ConvertInputGameResult(input):
        return gameResultChoices[input]

def CalculatePlayerMove(opponentChoice, gameResult):
        return gameMove[opponentChoice][gameResult]

def CalculatePlayerPoint(choice):
    return playPoints[choice]

def CalculateGamePoints(gameResult):
        return gameResultPoints[gameResult]

def UltraTopSecretStrategyGuide(opponentChoice, gameResult):
    playerMove = CalculatePlayerMove(opponentChoice, gameResult)

    playerChoicePoints = CalculatePlayerPoint(playerMove)
    gamePoints = CalculateGamePoints(gameResult)
    return playerChoicePoints + gamePoints

pointSum = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()
        (inputOpponentChoice, inputGameResult) = line.split(' ')
        opponentChoice = ConvertInputOpponentChoice(inputOpponentChoice)
        gameResult = ConvertInputGameResult(inputGameResult)
        result = UltraTopSecretStrategyGuide(opponentChoice, gameResult)
        pointSum += result


print('Score', pointSum)






