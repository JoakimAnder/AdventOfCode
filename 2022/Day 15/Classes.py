from dataclasses import dataclass, field
from typing import Callable, List, Dict, Tuple

from functools import wraps
import time


def timeit(func):
    @wraps(func)
    def timeit_wrapper(*args, **kwargs):
        start_time = time.perf_counter()
        result = func(*args, **kwargs)
        end_time = time.perf_counter()
        total_time = end_time - start_time
        print(f'Function {func.__name__}{args} {kwargs} Took {total_time:.4f} seconds')
        return result
    return timeit_wrapper


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

class Matrix:
    def __init__(self, width: int, height: int, fill=None) -> None:
        self._matrix: List[list] = []
        for _ in range(height):
            row = []
            for _ in range(width):
                row.append(fill)
            self._matrix.append(row)

    def get(self, point: Point):
        return self._matrix[point.y][point.x]

    def set(self, point: Point, value):
        self._matrix[point.y][point.x] = value

    def matrix(self) -> List[list]:
        matrix = []
        for row in self._matrix:
            matrix.append(row[:])
        return matrix


@dataclass
class Line:
    start: Point
    end: Point

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

    def slope(self):
        return (self.start.y - self.end.y) / (self.start.x - self.end.x)

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

    def manhattan_distance(self) -> int:
        difference = self.end - self.start
        absolute_difference = difference.absolute()
        return absolute_difference.x + absolute_difference.y

class Sensor:
    def __init__(self, location: Point, beacon: Point) -> None:
        self.location = location
        self.beacon = beacon
        self._manhatan_distance = Line(location, beacon).manhattan_distance()

    def contains(self, point: Point) -> bool:
        point_distance = Line(self.location, point).manhattan_distance()
        return self._manhatan_distance >= point_distance

    def get_row(self, y:int) -> Dict[int, str]:

        y_distance = Line(self.location, Point(self.location.x, y)).manhattan_distance()
        distance_difference = self._manhatan_distance - y_distance
        if distance_difference < 0:
            return dict()
        
        min_x = self.location.x - distance_difference
        max_x = self.location.x + distance_difference

        result = {x: '#' for x in range(min_x, max_x+1)}
        if self.beacon.y == y:
            result[self.beacon.x] = 'B'
        if self.location.y == y:
            result[self.location.x] = 'S'
        return result

    @timeit
    def to_lines(self, min_: Point = None, max_: Point = None) -> List[Line]:
        result = []
        start_y = self.location.y - self._manhatan_distance
        end_y = self.location.y + self._manhatan_distance
        if min_:
            start_y = max(min_.y, start_y)
        if max_:
            end_y = min(max_.y, end_y)
        for y in range(start_y, end_y + 1):
            y_distance = Line(self.location, Point(self.location.x, y)).manhattan_distance()
            distance_difference = self._manhatan_distance - y_distance

            min_x = self.location.x - distance_difference
            max_x = self.location.x + distance_difference
            if min_:
                min_x = max(min_.x, min_x)
            if max_:
                max_x = min(max_.x, max_x)

            result.append(Line(Point(min_x, y), Point(max_x, y)))
        return result


