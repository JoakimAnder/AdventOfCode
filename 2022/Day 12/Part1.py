from typing import Dict, List
from Classes import Square, Start, End

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]


squares:List[List[Square]] = []
idCounter = 0

def parse_input(input: str) -> Square:
    global idCounter
    idCounter += 1
    if input == 'S':
        return Start(idCounter, ord('a')-1, score=0)
    if input == 'E':
        return End(idCounter, ord('z')+1)
    return Square(idCounter, ord(input))

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        squares.append([parse_input(c) for c in line])


start = None
end = None
for y in range(len(squares)):
    for x in range(len(squares[0])):
        square = squares[y][x]
        
        rightNeigbor = None if x+1 >= len(squares[0]) else squares[y][x+1]
        leftNeigbor = None if x-1 < 0 else squares[y][x-1]
        topNeigbor = None if y-1 < 0 else squares[y-1][x]
        bottomNeigbor = None if y+1 >= len(squares) else squares[y+1][x]
        if rightNeigbor:
            square.add_neighbor(rightNeigbor)
        if leftNeigbor:
            square.add_neighbor(leftNeigbor)
        if topNeigbor:
            square.add_neighbor(topNeigbor)
        if bottomNeigbor:
            square.add_neighbor(bottomNeigbor)

        if isinstance(square, Start):
            start = square
        if isinstance(square, End):
            end = square


squaresDict = {s.id: s for row in squares for s in row}

squareIds = []
def getIds(square):
    global squareIds
    if square.id in squareIds:
        return
    squareIds.append(square.id)
    for neighbor in square.neighbors:
        getIds(neighbor)
getIds(start)

while True:
    hasChanged = False
    for id in squareIds:
        if squaresDict[id].try_score_neighbors():
            hasChanged = True
    if not hasChanged:
        break

endScore = end.score
print('endScore:', endScore)



