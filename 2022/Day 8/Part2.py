from typing import List
from Classes import Tree

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

def parseInput(input: str) -> List[Tree]:
    return list(map(lambda h: Tree(int(h)), input))

forest: List[List[Tree]] = []

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        parsedInput = parseInput(line)
        forest.append(parsedInput)



def isOnEdge(forest: List[List[Tree]], x: int, y: int) -> bool:
    if x == 0 or y == 0:
        return True

    forestHeight = len(forest)-1
    if forestHeight == y:
        return True
    forestWidth = len(forest[0])-1
    if forestWidth == x:
        return True
    return False

def getScore(trees: List[Tree], i: int) -> int:
    def getScoreDirection(trees, maxHeight):
        score = 0
        for tree in trees:
            currentHeight = tree.height
            score += 1
            if currentHeight >= maxHeight:
                break
        return score

    tree = trees[i]
    right = trees[i+1:]
    left = trees[i-1::-1]
    rightScore = getScoreDirection(right, tree.height)
    leftScore = getScoreDirection(left, tree.height)

    return rightScore * leftScore

def setScenicScore(forest: List[List[Tree]], x: int, y: int):
    if isOnEdge(forest, x, y):
        return
    row = forest[y]
    column = [trees[x] for trees in forest]
    rowScore = getScore(row, x)
    columnScore = getScore(column, y)
    score = rowScore*columnScore
    forest[y][x].score = score

    
forestWidth = len(forest[0])
forestHeight = len(forest)
for y in range(forestHeight):
    for x in range(forestWidth):
        setScenicScore(forest, x, y)

allTrees = [a for b in forest for a in b]
highestScoredTree = max(allTrees, key=lambda tree: tree.score)
print('highestScoredTree', highestScoredTree)





