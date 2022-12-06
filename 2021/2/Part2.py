INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

aim = 0
depth = 0
horizontalPosition = 0
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        (direction, inputAmount) = line.split(' ')
        amount = int(inputAmount)

        if direction == 'down':
            aim += amount
            continue
        if direction == 'up':
            aim -= amount
            continue
        
        horizontalPosition += amount
        depth += aim*amount


print('Final depth:', depth, 'horizontal position:', horizontalPosition, 'multiplied:', depth*horizontalPosition)







