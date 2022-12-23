from enum import Enum
from typing import Dict, List
from Classes import Point, Line, Sensor, Map

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
class Input(Enum):
    Small = 'InputSmall.txt'
    Medium = 'InputMedium.txt'
    Full = 'Input.txt'

INPUT = Input.Full

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
    
map_ = Map()
with open(INPUT_BASE_DIR+INPUT.value) as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        map_.add_sensor(parse_input(line))

Y = 10 if INPUT == Input.Small else 2000000

row = map_.slice(Y)
occupied_count = sum([line.points_count() for line in row])
all_filled_points = {point for sensor in map_.sensors for point in [sensor.location, sensor.beacon]}
filled_count = len(list(filter(lambda point: any([line.is_containing(point) for line in row]), all_filled_points)))
empty_spaces_count = occupied_count - filled_count
print('empty_spaces_count:', empty_spaces_count)

