from enum import Enum
from typing import List
INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

parseInput = lambda x: list(map(lambda y: y == '1', x))

values = []
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        values.append(parseInput(line))


def mostCommonValue(binaries: List[List[bool]], position: int) -> bool:
    halfAmount = len(binaries) / 2
    amountOfOnes = len(list(filter(lambda x: x[position], binaries)))
    
    if amountOfOnes == halfAmount:
        return None
    return amountOfOnes > halfAmount


def findRatingBinaries(binaries: List[List[bool]], defaultMostCommon: bool, reverseMostCommon: bool) -> List[bool]:
    for i in range(len(binaries[0])):
        if len(binaries) == 1:
            return binaries[0]

        mostCommon = mostCommonValue(binaries, i)
        if mostCommon == None:
            mostCommon = defaultMostCommon
        elif reverseMostCommon:
            mostCommon = not mostCommon

        binaries = list(filter(lambda x: x[i] == mostCommon, binaries))
    return binaries[0]

def calculateBinaries(binaries: List[bool]) -> int:
    totalValue = 0
    for i in range(len(binaries)):
        currentValue = 2 ** i
        if binaries[::-1][i]:
            totalValue += currentValue
    return totalValue


oxygenGeneratorRatingBinaries = findRatingBinaries(values, True, False)
co2ScrubberRatingBinaries = findRatingBinaries(values, False, True)
oxygenGeneratorRating = calculateBinaries(oxygenGeneratorRatingBinaries)
co2ScrubberRating = calculateBinaries(co2ScrubberRatingBinaries)



lifeSupportRating = oxygenGeneratorRating*co2ScrubberRating
print('oxygenGeneratorRating:', oxygenGeneratorRating, 'co2ScrubberRating:', co2ScrubberRating, 'lifeSupportRating:', lifeSupportRating)







