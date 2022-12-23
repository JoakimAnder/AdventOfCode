from dataclasses import dataclass, field
from typing import Callable, List, Dict, Tuple
import logging
import time
logging.basicConfig(level=logging.NOTSET, format='%(asctime)s %(levelname)s: %(message)s')
logger = logging.getLogger(__file__)


@dataclass
class Point:
    x: int = 0
    y: int = 0
    
    def normalize(self):
        x = 0 if self.x == 0 else int(self.x / abs(self.x))
        y = 0 if self.y == 0 else int(self.y / abs(self.y))
        return Point(x, y)

    def absolute(self):
        x = abs(self.x)
        y = abs(self.y)
        return Point(x, y)
        

    def __add__(self, other):
        if isinstance(other, int):
            other = Point(other, other)
        x = self.x + other.x
        y = self.y + other.y
        return Point(x, y)

    def __sub__(self, other):
        if isinstance(other, int):
            other = Point(other, other)
        x = self.x - other.x
        y = self.y - other.y
        return Point(x, y)

    def __mul__(self, other):
        if isinstance(other, int):
            other = Point(other, other)
        x = self.x * other.x
        y = self.y * other.y
        return Point(x, y)

    def __hash__(self) -> int:
        return hash((self.x, self.y))

@dataclass
class Line:
    start: Point
    end: Point
    _manhattan_distance = None

    def manhattan_distance(self) -> int:
        if self._manhattan_distance:
            return self._manhattan_distance
        difference = self.end - self.start
        absolute_difference = difference.absolute()
        self._manhattan_distance = absolute_difference.x + absolute_difference.y
        return self._manhattan_distance
    
    def points_count(self):
        return self.manhattan_distance() + 1

    def slope(self):
        return (self.start.y - self.end.y) / (self.start.x - self.end.x)

    def points(self) -> List[Point]:
        points = []
        current_point = self.start
        while current_point != self.end:
            points.append(current_point)
            normalized_difference = (self.end-current_point).normalize()
            current_point += normalized_difference
        points.append(self.end)
        return points

    def _seperate(self):
        difference = self.end - self.start
        normalized_difference = difference.normalize()
        absolute_difference = difference.absolute()
        
        x_is_lowest_diff = absolute_difference.x < absolute_difference.y
        distance = absolute_difference.x if x_is_lowest_diff else absolute_difference.y
        if distance == 0:
            return (Line(self.start, self.start), self)
        separator = normalized_difference*distance

        diagonal = Line(self.start, separator)
        straight = Line(separator, self.end)
        return (diagonal, straight)

    def is_intersecting(self, other) -> bool:
        (diagonal_line, straight_line) = self._seperate()
        (other_diagonal_line, other_straight_line) = other._seperate()

        if (straight_line.is_containing(other_straight_line.start) or 
            other_straight_line.is_containing(straight_line.start)
            ):
            return True
        if (diagonal_line.is_containing(other_diagonal_line.start) or 
            other_diagonal_line.is_containing(diagonal_line.start)
            ):
            return True
        return False

    def is_containing(self, point:Point) -> bool:
        def check_the_simple_things(line: Line, point: Point) -> bool:
            if line.start == point or line.end == point:
                return True
            
            is_out_of_bounds = (
                min(line.start.x, line.end.x) > point.x or
                max(line.start.x, line.end.x) < point.x or
                min(line.start.y, line.end.y) > point.y or
                max(line.start.y, line.end.y) < point.y
            )
            if is_out_of_bounds:
                return False
            return None

        def check_simple_line(line: Line, point: Point) -> bool:
            simple_check = check_the_simple_things(line, point)
            if simple_check is not None:
                return simple_check
            
            if line.slope() == Line(line.start, point).slope():
                return True
            
            return False
        
        simple_check = check_the_simple_things(self, point)
        if simple_check is not None:
            return simple_check

        (diagonal_line, straight_line) = self._seperate()
        
        if check_simple_line(straight_line, point):
            return True
        if check_simple_line(diagonal_line, point):
            return True
        
        return False

class Sensor:
    def __init__(self, location: Point, beacon: Point) -> None:
        self.location = location
        self.beacon = beacon
        self._manhattan_distance = Line(location, beacon).manhattan_distance()

    def contains(self, point: Point) -> bool:
        point_distance = Line(self.location, point).manhattan_distance()
        return self._manhattan_distance >= point_distance

    def get_row(self, y:int, min_x = None, max_x = None) -> Line:
        y_distance = Line(self.location, Point(self.location.x, y)).manhattan_distance()
        distance_difference = self._manhattan_distance - y_distance
        if distance_difference < 0:
            return None
        
        min_ = self.location.x - distance_difference
        if min_x is not None:
            min_ = max(min_x, min_)
        max_ = self.location.x + distance_difference
        if max_x is not None:
            max_ = min(max_x, max_)
        result = Line(Point(min_, y), Point(max_, y))
        return result
        
    def get_rows(self, min_: Point = None, max_: Point = None) -> List[Line]:
        def get_y_row(y:int):
            y_distance = Line(self.location, Point(self.location.x, y)).manhattan_distance()
            distance_difference = self._manhattan_distance - y_distance

            min_x = self.location.x - distance_difference
            max_x = self.location.x + distance_difference
            if min_:
                min_x = max(min_.x, min_x)
            if max_:
                max_x = min(max_.x, max_x)

            return Line(Point(min_x, y), Point(max_x, y))
        start_time = time.perf_counter()
        start_y = self.location.y - self._manhattan_distance
        end_y = self.location.y + self._manhattan_distance
        if min_:
            start_y = max(min_.y, start_y)
        if max_:
            end_y = min(max_.y, end_y)

        result = list(map(get_y_row, range(start_y, end_y + 1)))
        end_time = time.perf_counter()
        logger.debug(f'Got rows for Sensor with location {self.location} and distance {self._manhattan_distance} in {end_time-start_time} seconds')
        return result


class Map:
    def __init__(self) -> None:
        self.sensors: List[Sensor] = []

    def add_sensor(self, sensor: Sensor):
        self.sensors.append(sensor)

    def slice(self, y:int, min_x:int = None, max_x:int = None) -> List[Line]:
        rows = list(filter(lambda row: row, [sensor.get_row(y, min_x=min_x, max_x=max_x) for sensor in self.sensors]))
        combined = Map._combine_rows(rows)
        return combined

    def get_rows(self, min_: Point = None, max_: Point = None) -> List[List[Line]]:
        dict_ = {}
        for sensor in self.sensors:
            for row in sensor.get_rows(min_, max_):
                y = row.start.y
                if y in dict_:
                    dict_[y] = Map._combine_rows(y, dict_[y]+[row])
                else: 
                    dict_[y] = [row]
        return dict_.values()
    
    @staticmethod
    def _combine_rows(y:int, rows: List[Line]) -> List[Line]:
        lines = sorted(rows, key=lambda line: line.start.x)
        has_changed = True
        while has_changed:
            for i, (line, next_line) in enumerate(zip(lines, lines[1:])):
                is_next_to_eachother = line.end.x + 1 == next_line.start.x
                if not (is_next_to_eachother or line.is_intersecting(next_line)):
                    continue
                x1 = min(line.start.x, line.end.x, next_line.start.x, next_line.end.x)
                x2 = max(line.start.x, line.end.x, next_line.start.x, next_line.end.x)
                new_line = Line(Point(x1, y), Point(x2, y))
                lines = lines[:i]+[new_line]+lines[i+2:]
                break
            else:
                has_changed = False
        return lines

