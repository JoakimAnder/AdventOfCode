from dataclasses import dataclass
from typing import List
import logging
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

    def manhattan_distance(self) -> int:
        difference = self.end - self.start
        absolute_difference = difference.absolute()
        return absolute_difference.x + absolute_difference.y

    def slope(self):
        if self.end.x - self.start.x == 0:
            return None
        return (self.end.y - self.start.y) / (self.end.x - self.start.x)

    def intersection(self, other) -> Point():
        other:Line = other
        if self.slope() == other.slope():
            return None
        (s_min_x, s_max_x) = sorted([self.start.x, self.end.x])
        (s_min_y, s_max_y) = sorted([self.start.y, self.end.y])
        (o_min_x, o_max_x) = sorted([other.start.x, other.end.x])
        (o_min_y, o_max_y) = sorted([other.start.y, other.end.y])
        if (
            s_min_x > o_max_x or
            s_max_x < o_min_x or
            s_min_y > o_max_y or
            s_max_y < o_min_y
        ):
            return None

        self_difference = self.end - self.start
        other_difference = other.end - other.start

        s = (-self_difference.y * (self.start.x - other.start.x) + self_difference.x * (self.start.y - other.start.y)) / (-other_difference.x * self_difference.y + self_difference.x * other_difference.y)
        t = ( other_difference.x * (self.start.y - other.start.y) - other_difference.y * (self.start.x - other.start.x)) / (-other_difference.x * self_difference.y + self_difference.x * other_difference.y)

        if (s >= 0 and s <= 1 and t >= 0 and t <= 1):
            x = self.start.x + (t * self_difference.x)
            y = self.start.y + (t * self_difference.y)
            return Point(x, y)

        return None

class Form:
    def __init__(self, edges: List[Point]) -> None:
        self.edges = list(edges)
        
class Square(Form):
    def __init__(self, center: Point, length: int) -> None:
        self.center = center
        self.length = length
        self.left_point = center - Point(length)
        self.right_point = center + Point(length)
        self.top_point = center - Point(y=length)
        self.bottom_point = center + Point(y=length)
        super().__init__([self.top_point, self.right_point, self.bottom_point, self.left_point])

    def does_contain_whole(self, other):
        other: Square = other
        result = Line(self.center, other.center).manhattan_distance() + other.length <= self.length
        return result
    
    def combines_with(self, other) -> bool:
        other:Square = other
        if self.does_contain_whole(other) or other.does_contain_whole(self):
            return False
        length = self.length+1
        combined_length = length + other.length
        center_distance = Line(self.center, other.center).manhattan_distance()
        return combined_length > center_distance

    def contains_point(self, point: Point) -> bool:
        point_distance = Line(self.center, point).manhattan_distance()
        return self.length >= point_distance
    
    def intersection(self, other) -> Form:
        if not self.combines_with(other):
            raise ValueError(f'Squares aren\'t close enough')

        def ns_intersection():
            (n, s) = sorted([self, other], key=lambda s: s.center.y)
            length = (n.bottom_point.y - s.top_point.y) / 2
            center_point = Point(n.center.x, s.top_point.y+length)
            return Square(center_point, length)

        def ew_intersection():
            (w, e) = sorted([self, other], key=lambda s: s.center.x)
            length = (w.right_point.x - e.left_point.x) / 2
            center_point = Point(e.left_point.x+length, w.center.y)
            return Square(center_point, length)

        def nwse_intersection():
            (nw, se) = sorted([self, other], key=lambda s: s.center.x)
            def get_first_intersection(nw_line: Line, se_line: Line):
                intersection = nw_line.intersection(se_line)
                return intersection

            
            if se.contains_point(nw.bottom_point) and se.contains_point(nw.right_point):
                top_point = get_first_intersection(Line(nw.top_point, nw.right_point), Line(se.top_point, se.left_point))
                right_point = nw.right_point
                bottom_point = nw.bottom_point
                left_point = get_first_intersection(Line(nw.left_point, nw.bottom_point), Line(se.left_point, se.top_point))
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if nw.contains_point(se.top_point) and nw.contains_point(se.left_point):
                top_point = se.top_point
                right_point = get_first_intersection(Line(nw.right_point, nw.bottom_point), Line(se.top_point, se.right_point))
                bottom_point = get_first_intersection(Line(nw.bottom_point, nw.right_point), Line(se.left_point, se.bottom_point))
                left_point = se.left_point
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if se.contains_point(nw.bottom_point) and nw.contains_point(se.top_point):
                top_point = se.top_point
                right_point = get_first_intersection(Line(nw.right_point, nw.bottom_point), Line(se.top_point, se.right_point))
                bottom_point = nw.bottom_point
                left_point = get_first_intersection(Line(nw.left_point, nw.bottom_point), Line(se.top_point, se.left_point))
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if se.contains_point(nw.right_point) and nw.contains_point(se.left_point):
                top_point = get_first_intersection(Line(nw.top_point, nw.right_point), Line(se.left_point, se.top_point))
                right_point = nw.right_point
                bottom_point = get_first_intersection(Line(nw.bottom_point, nw.right_point), Line(se.left_point, se.bottom_point))
                left_point = se.left_point
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))
            
            left_point = sorted([nw.bottom_point, se.left_point], key=lambda p: p.x)[-1]
            right_point = sorted([nw.right_point, se.top_point], key=lambda p: p.x)[0]
            return Form([right_point, left_point])
            
        def nesw_intersection():
            (sw, ne) = sorted([self, other], key=lambda s: s.center.x)
            def get_first_intersection(nw_line: Line, se_line: Line):
                intersection = nw_line.intersection(se_line)
                if not intersection:
                    points = nw_line.points()
                    for point, next_point in zip(points, points[1:]):
                        if ne.contains_point(next_point):
                            intersection = point
                            break
                return intersection
            
            if ne.contains_point(sw.top_point) and ne.contains_point(sw.right_point):
                top_point = sw.top_point
                right_point = sw.right_point
                bottom_point = get_first_intersection(Line(sw.bottom_point, sw.right_point), Line(ne.bottom_point, ne.left_point))
                left_point = get_first_intersection(Line(sw.left_point, sw.top_point), Line(ne.left_point, ne.bottom_point))
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if sw.contains_point(ne.bottom_point) and sw.contains_point(ne.left_point):
                top_point = get_first_intersection(Line(sw.top_point, sw.right_point), Line(ne.left_point, ne.top_point))
                right_point = get_first_intersection(Line(sw.right_point, sw.top_point), Line(ne.bottom_point, ne.right_point))
                bottom_point = ne.bottom_point
                left_point = ne.left_point
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if ne.contains_point(sw.right_point) and sw.contains_point(ne.left_point):
                top_point = get_first_intersection(Line(sw.top_point, sw.right_point), Line(ne.left_point, ne.top_point))
                right_point = sw.right_point
                bottom_point = get_first_intersection(Line(sw.bottom_point, sw.right_point), Line(ne.left_point, ne.bottom_point))
                left_point = ne.left_point
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))

            if ne.contains_point(sw.top_point) and sw.contains_point(ne.bottom_point):
                top_point = sw.top_point
                right_point = get_first_intersection(Line(sw.right_point, sw.top_point), Line(ne.bottom_point, ne.right_point))
                bottom_point = ne.bottom_point
                left_point = get_first_intersection(Line(sw.left_point, sw.top_point), Line(ne.bottom_point, ne.left_point))
                return Form(filter(lambda x: x, [top_point, right_point, bottom_point, left_point]))
            
            left_point = sorted([ne.left_point, sw.top_point], key=lambda p: p.x)[-1]
            right_point = sorted([ne.bottom_point, sw.right_point], key=lambda p: p.x)[0]
            return Form([right_point, left_point])

        slope = Line(self.center, other.center).slope()
        if slope is None:
            return ns_intersection()
        elif slope == 0:
            return ew_intersection()
        elif slope > 0:
            return nwse_intersection()
        else:
            return nesw_intersection()
        
class Sensor:
    def __init__(self, location: Point, beacon: Point) -> None:
        self.location = location
        self.beacon = beacon
        self._manhattan_distance = Line(location, beacon).manhattan_distance()
        self._topmost_point = location - Point(y=self._manhattan_distance)
        self._bottommost_point = location + Point(y=self._manhattan_distance)
        self._leftmost_point = location - Point(self._manhattan_distance)
        self._rightmost_point = location + Point(self._manhattan_distance)
        self.extreme_points = (self._topmost_point, self._rightmost_point, self._bottommost_point, self._leftmost_point)
        self.square = Square(location, self._manhattan_distance)
        self.adjecent_sensors: List[Sensor] = []

    def __hash__(self) -> int:
        return hash((self.location.x, self.location.y))
        

class Map:
    def __init__(self) -> None:
        self.sensors: List[Sensor] = []
        self._forms: List[Form] = []

    def join_sensors(self) -> None:
        for i, current_sensor in enumerate(self.sensors):
            for other_sensor in self.sensors[:i] + self.sensors[i+1:]:
                if current_sensor.square.combines_with(other_sensor.square):
                    current_sensor.adjecent_sensors.append(other_sensor)

    @staticmethod
    def _find_circulating_sensors(input: List[Sensor]) -> List[List[Sensor]]:
        input_length = len(input)
        if input_length < 1 or input_length > 4:
            return []

        result: List[List[Sensor]] = []

        first_sensor = input[0]
        last_sensor = input[-1]
        if input_length == 4 and first_sensor in last_sensor.adjecent_sensors:
            result.append(input)

        for next_sensor in filter(lambda s: s not in input, last_sensor.adjecent_sensors):
            res = Map._find_circulating_sensors(input + [next_sensor])
            if res:
                result += res
        return result
        
    def _find_enclosed_points(self, sensors: List[Sensor], min_: Point, max_: Point) -> List[Point]:
        nw = sorted(sensors, key=lambda s: (s.location.x*s.location.y, s.location.x, s.location.y))[0]
        while sensors[0] is not nw:
            sensors = sensors[1:]+[sensors[0]]

        (nw, ne, se, sw) = sensors
        n_intersection = nw.square.intersection(ne.square)
        e_intersection = ne.square.intersection(se.square)
        s_intersection = se.square.intersection(sw.square)
        w_intersection = sw.square.intersection(nw.square)

        top = sorted(n_intersection.edges, key=lambda p: (-p.y, p.x))[0]
        right = sorted(e_intersection.edges, key=lambda p: (p.x, p.y))[0]
        bottom = sorted(s_intersection.edges, key=lambda p: (p.y, -p.x))[0]
        left = sorted(w_intersection.edges, key=lambda p: (-p.x, -p.y))[0]
        
        if Line(top, bottom).manhattan_distance() > 10 or Line(left, right).manhattan_distance() > 10:
            return []

        center_x = left.x + (right.x - left.x)/2
        center_y = top.y + (bottom.y - top.y)/2
        center_point = Point(center_x, center_y)
        length = max([Line(center_point, p).manhattan_distance() for p in (top, right, bottom, left)])
        check_square = Square(center_point, length)
        if any([s.square.does_contain_whole(check_square) for s in self.sensors]):
            return []

        (min_x, max_x) = (left.x, right.x)
        (min_y, max_y) = (top.y, bottom.y)
        points = []
        for y in range(int(min_y), int(max_y)+1):
            if not (min_y >= y or y >= max_y or min_.y > y or y > max_.y):
                for x in range(int(min_x), int(max_x)+1):
                    if not (min_x >= x or x >= max_x or min_.x > x or x > max_.x):
                        points.append(Point(x, y))
        return points

    def find_dark_spot(self, min_: Point, max_: Point) -> List[Point]:
        list_of_circles = list(map(lambda s: Map._find_circulating_sensors([s]), self.sensors))
        circulating_sensors = [circle for circles in list_of_circles for circle in circles]
        circulating_sensors = list(filter(lambda c: c, circulating_sensors))
        
        distinct_circles: List[List[Sensor]] = []
        for circle in circulating_sensors:
            for c in distinct_circles:
                if len(circle) != len(c):
                    continue
                if all([s in c for s in circle]):
                    break
            else:
                distinct_circles.append(circle)

        result: List[Point] = []
        for circle in distinct_circles:
            points = self._find_enclosed_points(circle, min_, max_) + self._find_enclosed_points(circle[::-1], min_, max_)
            if points:
                result += points
        return result

    def add_sensor(self, sensor: Sensor):
        self.sensors.append(sensor)
