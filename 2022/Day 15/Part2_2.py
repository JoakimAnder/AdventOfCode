from enum import Enum
from typing import Dict, List, Tuple
from Classes_2 import Point, Map, Sensor, Line
import logging
import time
logging.basicConfig(level=logging.NOTSET, format='%(asctime)s %(levelname)s: %(message)s')
logger = logging.getLogger(__file__)

start_time = time.perf_counter()

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
i = 0
with open(INPUT_BASE_DIR+INPUT.value) as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()
        sensor = parse_input(line)
        sensor.id = chr(ord('a') + i)
        map_.add_sensor(sensor)
        i += 1

min_ = Point(0, 0)
max_ = Point(20, 20) if INPUT == Input.Small else Point(4_000_000, 4_000_000)

all_filled_points = {point for sensor in map_.sensors for point in [sensor.location, sensor.beacon]}
min_x = min(filter(lambda x: x >= min_.x, map(lambda point: point.x, all_filled_points)))
max_x = max(filter(lambda x: x <= max_.x, map(lambda point: point.x, all_filled_points)))

end_time = time.perf_counter()
logger.debug(f'Parsing took {end_time-start_time} seconds') #
start_time = end_time

map_.join_sensors()

end_time = time.perf_counter()
logger.debug(f'Joining sensors took {end_time-start_time} seconds') #
start_time = end_time

dark_spots = map_.find_dark_spot(min_, max_)

end_time = time.perf_counter()
logger.debug(f'Gathering {len(dark_spots)} dark spot(s) took {end_time-start_time} seconds') #
start_time = end_time

for point in dark_spots:
    tuning_frequency = point.x*4_000_000 + point.y
    print(f'tuning_frequency of {point}: {tuning_frequency}')


"""

3249595: [
    Line(start=Point(x=0, y=3249595), end=Point(x=3340223, y=3249595)), 
    Line(start=Point(x=3340225, y=3249595), end=Point(x=4000000, y=3249595))]}  

print((3340224*4000000)+3249595)
"""
