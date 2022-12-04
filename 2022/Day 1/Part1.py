
maxCalorySum = -1
currentCalorySum = 0

inputFilePath = __file__[:__file__.rindex('/')+1] + 'Input.txt'
with open(inputFilePath, 'r') as input:
    while True:
        line = input.readline()
        if not line:
            break
        line = line.strip()

        if line == '':
            if currentCalorySum > maxCalorySum:
                maxCalorySum = currentCalorySum
            currentCalorySum = 0
            continue

        currentCalorySum += int(line)

print('Max calory sum:', maxCalorySum)








