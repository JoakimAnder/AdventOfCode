
def FindSharedItemTypes(rucksack):
    return list({item for item in rucksack[0] if item in rucksack[1]})

def ParseInputIntoRucksack(input):
    halfLength = int(len(input) / 2)
    return [input[:halfLength], input[halfLength:]]

def CalculatePriority(itemType):
    if itemType.islower():
        return ord(itemType) - ord('a') + 1
    return ord(itemType) - ord('A') + 27

totalPriority = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()
        
        rucksack = ParseInputIntoRucksack(line)
        sharedItemTypes = FindSharedItemTypes(rucksack)

        for itemType in sharedItemTypes:
            result = CalculatePriority(itemType)
            totalPriority += result


print('Total priority:', totalPriority)






