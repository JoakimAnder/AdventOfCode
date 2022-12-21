from typing import Dict, List
from Classes import Point, Line, Sensor

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

Y = 2000000
dict_row: Dict[int, str] = {}

for sensor in sensors:
    for (k, v) in sensor.get_row(Y).items():
        if v in ('S', 'B'):
            dict_row[k] = v
            continue
        if k not in dict_row:
            dict_row[k] = v

empty_spaces = list(filter(lambda v: v == '#', dict_row.values()))

print('empty_spaces:', len(empty_spaces))



