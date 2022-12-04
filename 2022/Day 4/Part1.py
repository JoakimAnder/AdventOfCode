

def ParseInputToElfPair(input):
    return [(int(fromSection), int(toSection)) for (fromSection, toSection) in [section.split('-') for section in input.split(',') ]]

def HasFullyContained(firstElf, secondElf):
    if min(firstElf) == min(secondElf):
        return True

    if min(firstElf) < min(secondElf):
        return max(firstElf) >= max(secondElf)
    return max(firstElf) <= max(secondElf)

amountOfFullyContainedSections = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()
        
        elfPair = ParseInputToElfPair(line)
        hasFullyContained = HasFullyContained(elfPair[0], elfPair[1])

        if hasFullyContained:
            amountOfFullyContainedSections += 1


print('Amount of fully contained sections:', amountOfFullyContainedSections)






