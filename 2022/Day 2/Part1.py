
playPoints = {
    'X': 1,
    'Y': 2,
    'Z': 3,
}

gameRules = {
    'A': {
        'X': 3,
        'Y': 6,
        'Z': 0,
    },
    'B': {
        'X': 0,
        'Y': 3,
        'Z': 6,
    },
    'C': {
        'X': 6,
        'Y': 0,
        'Z': 3,
    },
}

def CalculatePlayerPoint(choice):
    return playPoints[choice]

def CalculateGamePoints(opponentChoice, playerChoice):
    return gameRules[opponentChoice][playerChoice]

def StrategyGuide(opponentChoice, playerChoice): 
    playerChoicePoints = CalculatePlayerPoint(playerChoice)
    gamePoints = CalculateGamePoints(opponentChoice, playerChoice)
    return playerChoicePoints + gamePoints

pointSum = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()

        (opponentChoice, playerChoice) = line.split(' ')
        result = StrategyGuide(opponentChoice, playerChoice)
        pointSum += result


print('Score', pointSum)






