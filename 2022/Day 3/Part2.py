GROUP_SIZE = 3

def FindSharedGroupItemType(group):
    for itemType in group[0]:
        if itemType in group[1] and itemType in group[2]:
            return itemType

def CalculatePriority(itemType):
    if itemType.islower():
        return ord(itemType) - ord('a') + 1
    return ord(itemType) - ord('A') + 27

totalPriority = 0
group = []

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()
        
        group.append(line)
        if len(group) == GROUP_SIZE:
            sharedItemType = FindSharedGroupItemType(group)
            totalPriority += CalculatePriority(sharedItemType)
            group = []



print('Total priority:', totalPriority)






