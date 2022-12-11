from Classes import Point, Rope

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]


directionParser = {
    'U' : Point(0,1),
    'D' : Point(0,-1),
    'R' : Point(1,0),
    'L' : Point(-1,0),
}    

def parseInput(input: str) -> Point:
    (inputDirection, inputSteps) = input.split(' ')
    direction = directionParser[inputDirection]
    steps = int(inputSteps)
    return direction*steps

ropelength = 2
rope = Rope(ropelength)
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        direction = parseInput(line)
        rope.move(direction)


tailMoveAmount = len(set([(point.x, point.y) for point in rope.knots[-1].history]))
print('tailMoveAmount:', tailMoveAmount)





