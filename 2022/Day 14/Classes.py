from dataclasses import dataclass, field
from typing import Callable, List


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

@dataclass
class Rock:
    lines: List[Line] = field(default_factory=list)

@dataclass
class Sand:
    location: Point

class Cave:
    def __init__(self, rocks: List[Rock], sand_source: Point, floor_offset: int = None) -> None:
        rock_points = [point for rock in rocks for line in rock.lines for point in [line.start, line.end]]
        all_points = [sand_source] + rock_points
        min_x = min([point.x for point in all_points])
        max_x = max([point.x for point in all_points])
        min_y = min([point.y for point in all_points])
        max_y = max([point.y for point in all_points])
        
        if floor_offset:
            max_y += floor_offset
        height = max_y - min_y + 1
        if floor_offset:
            min_x = min(min_x, sand_source.x-height)
            max_x = max(max_x, sand_source.x+height)
        width = max_x - min_x + 1

        matrix = Matrix(width, height, fill='.')
        min_point = Point(x=min_x, y=min_y)
        if floor_offset:
            for point in Line(Point(0, height-1), Point(width-1, height-1)).points():
                matrix.set(point, '#')


        for rock in rocks:
            for line in rock.lines:
                for point in line.points():
                    matrix.set(point-min_point, '#')
        matrix.set(sand_source-min_point, "+")
        self._is_bounded = floor_offset is None
        self._min_x = min_x
        self._max_y = max_y
        self._matrix = matrix
        self.sand_source = sand_source - min_point
        self.sand: List[Sand] = []


    def __str__(self) -> str:
        matrix = self._matrix.matrix()
        width = len(matrix[0])
        height = len(matrix)
        row_header_length = len(str(self._min_x+width))
        column_header_length = len(str(self._max_y))

        y = self._max_y - height + 1
        for row in matrix:
            for (i, c) in enumerate(f'{str(y).rjust(column_header_length)} '):
                row.insert(i, c)
            y += 1
        x = self._min_x - width + 1
        reversed_headers = []
        for i in range(width):
            reversed_headers.append(list(str(x+i).rjust(row_header_length)))
        headers = []
        header_buffer = [' '] * (column_header_length+1)
        for i in range(row_header_length):
            headers.append(header_buffer+[row[i] for row in reversed_headers])
        matrix = headers + matrix
        return '\n'.join([''.join([str(i) for i in row]) for row in matrix])
    
    def _create_sand(self) -> Sand:
        current_point = self.sand_source + Point()
        if self._matrix.get(current_point) == 'o':
            return None
        while True:
            is_out_of_bounds = self._is_bounded and (
                current_point.x < 0
                or current_point.y < 0
                or current_point.x >= len(self._matrix._matrix[0])
                or current_point.y >= len(self._matrix._matrix)
            ) 
            if is_out_of_bounds:
                return None

            for direction in [Point(0, 1), Point(-1, 1), Point(1, 1)]:
                point = current_point+direction
                value = self._matrix.get(point)
                if value == '.':
                    current_point = point
                    break
            else:
                break
        return Sand(current_point)

    def simulate_sand(self):
        while True:
            sand = self._create_sand()
            if not sand:
                break
            self.sand.append(sand)
            self._matrix.set(sand.location, 'o')









