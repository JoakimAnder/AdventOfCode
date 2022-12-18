from typing import Dict, List
from Classes import Rock, Cave, Point, Line

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

def parse_input(input: str) -> Rock:
    points = [Point(int(inp.split(',')[0]), int(inp.split(',')[1])) for inp in input.split(' -> ')]
    lines: List[Line] = [Line(point, nextPoint) for (point, nextPoint) in zip(points, points[1:])]
    return Rock(lines)

rocks: List[Rock] = []
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        rocks.append(parse_input(line))

sand_source = Point(500, 0)
cave = Cave(rocks, sand_source, 2)
#print(cave)

cave.simulate_sand()

sand = cave.sand
#print(cave)
print('sand:', len(sand))



