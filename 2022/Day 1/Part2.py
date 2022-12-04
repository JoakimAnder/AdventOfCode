

inventories = []
currentInventory = []

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()

        if line == '':
            inventories.append(currentInventory)
            currentInventory = []
            continue

        currentInventory.append(int(line))

inventories.sort(key=lambda inventory : sum(inventory), reverse=True)
max3CalorySum = sum([sum(inventory) for inventory in inventories[0:3]])

print('Top 3 calory sum:', max3CalorySum)








