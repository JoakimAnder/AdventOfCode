

def ParseInputToElfPair(input):
    return [(int(fromSection), int(toSection)) for (fromSection, toSection) in [section.split('-') for section in input.split(',') ]]

def DoesOverlap(firstElf, secondElf):
    return not (min(firstElf) > max(secondElf) or max(firstElf) < min(secondElf))

amountOfOverlapping = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()
        
        elfPair = ParseInputToElfPair(line)
        doesOverlap = DoesOverlap(*elfPair)

        if doesOverlap:
            amountOfOverlapping += 1


print('Amount of overlapping sections:', amountOfOverlapping)






