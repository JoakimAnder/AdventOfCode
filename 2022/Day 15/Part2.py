from typing import Dict, List, Tuple
from Classes import Point, Matrix, Sensor, Line

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

def parse_point_from_input(input: str) -> Point:
    split_input = input.split(' ')
    x = int(list(filter(lambda x: x.startswith('x='), split_input))[0][2:-1])
    y = int(list(filter(lambda x: x.startswith('y='), split_input))[0][2:])
    return Point(x, y)

def parse_input(input: str) -> Sensor:
    (sensor_input, beacon_input) = input.split(':')
    sensor_point = parse_point_from_input(sensor_input)
    beacon_point = parse_point_from_input(beacon_input)
    return Sensor(sensor_point, beacon_point)
    
sensors: List[Sensor] = []
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        sensors.append(parse_input(line))

y_lines: Dict[int, List[Line]] = {}

min_ = Point(0, 0)
max_ = Point(4_000_000, 4_000_000)
max_ = Point(20, 20)
for sensor in sensors:
    for line in sensor.to_lines(min_, max_):
        y = line.start.y
        if y not in y_lines:
            y_lines[y] = [line]
            continue
        
        lines = sorted(y_lines[y] + [line], key=lambda line: line.start.x)
        has_changed = True
        while has_changed:
            before_len = len(lines)
            for i, (line, next_line) in enumerate(zip(lines, lines[1:])):
                is_next_to_eachother = line.end.x + 1 == next_line.start.x
                if not (line.is_intersecting(next_line) or is_next_to_eachother):
                    continue
                x1 = min(line.start.x, line.end.x, next_line.start.x, next_line.end.x)
                x2 = max(line.start.x, line.end.x, next_line.start.x, next_line.end.x)
                new_line = Line(Point(x1, y), Point(x2, y))
                lines = lines[:i]+[new_line]+lines[i+2:]
                break
            else:
                has_changed = False
        y_lines[y] = lines

lines_with_holes = {}
for y, lines in y_lines.items():
    if len(lines) > 1:
        lines_with_holes[y] = lines

print(lines_with_holes)
#{3249595: [Line(start=Point(x=0, y=3249595), end=Point(x=3340223, y=3249595)), Line(start=Point(x=3340225, y=3249595), end=Point(x=4000000, y=3249595))]}   
print((3340224*4000000)+3249595)