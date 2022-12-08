from typing import List, Set
from Classes import Tree

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

def getVisibleTreeIndexes(trees: List[Tree]) -> Set[int]:
    def oneWay(maxHeight: int, trees: List[Tree]) -> Set[int]:
        visibleIndexes = set()
        highestHeight = -1
        for i in range(len(trees)):
            currentHeight = trees[i].height
            if currentHeight > highestHeight:
                highestHeight = currentHeight
                visibleIndexes.add(i)
            if currentHeight == maxHeight:
                break
        return visibleIndexes

    highestTreeHeight = max(map(lambda t: t.height, trees))
    treesLength = len(trees)
    right = oneWay(highestTreeHeight, trees)
    left = set(map(lambda i: treesLength-1-i, oneWay(highestTreeHeight, trees[::-1])))

    return right.union(left)
    



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

forestWidth = len(forest[0])
forestHeight = len(forest)

for i in range(forestHeight):
    selection = forest[i]
    visibleIndexes = getVisibleTreeIndexes(selection)
    for visibleI in visibleIndexes:
        selection[visibleI].visibleX = True

for i in range(forestWidth):
    selection = [trees[i] for trees in forest]
    visibleIndexes = getVisibleTreeIndexes(selection)
    for visibleI in visibleIndexes:
        selection[visibleI].visibleY = True

allTrees: List[Tree] = []
for trees in forest:
    allTrees += trees 

amountOfVisibleTrees = len(list(filter(lambda tree: tree.visibleX or tree.visibleY, allTrees)))

print('amountOfVisibleTrees', amountOfVisibleTrees)






