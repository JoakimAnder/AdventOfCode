from Classes import BingoBoard, BingoGame

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
BINGO_BOARD_WIDTH = 5

drawNumbers = []
boardNumbers = []

currentBoardIndex = -1
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        if not drawNumbers:
            drawNumbers = list(map(lambda x: int(x), line.split(',')))
            continue
        
        if not line:
            currentBoardIndex += 1
            boardNumbers.append([])
        
        numbers = list(map(lambda x: int(x), filter(lambda y: y, line.split(' '))))
        boardNumbers[currentBoardIndex] += numbers
        
boards = list(map(lambda numbers: BingoBoard(numbers, BINGO_BOARD_WIDTH), boardNumbers))
game = BingoGame(drawNumbers, boards)
result = game.getResult()

winningNumber = result.winningNumber
score = result.winningBoards[0].calculateScore(winningNumber)

print('score:', score, 'winningNumber:', winningNumber, 'winners:', len(result.winningBoards))







