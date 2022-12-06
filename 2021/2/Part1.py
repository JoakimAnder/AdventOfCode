INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

class Vector:
    def __init__(self, x: int = 0, y: int = 0) -> None:
        self.x = x
        self.y = y

    def add(self, vector):
        return Vector(self.x + vector.x, self.y + vector.y)
    
    def multiply(self, amount: int):
        return Vector(self.x*amount, self.y*amount)

    def __str__(self) -> str:
        return f'x: {self.x}, y: {self.y}'

inputDirectionParser = {
    'forward': Vector(x=1),
    'up': Vector(y=-1),
    'down': Vector(y=1),
}

def parseInput(input: str) -> Vector:
    (inputDirection, inputAmount) = input.split(' ')
    direction = inputDirectionParser[inputDirection]
    amount = int(inputAmount)
    return direction.multiply(amount)

position = Vector()
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        vector = parseInput(line)
        position = position.add(vector)


print('Final position:', position, 'multiplied:', position.x*position.y)







