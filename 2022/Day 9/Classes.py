from dataclasses import dataclass


@dataclass
class Point:
    x: int
    y: int

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


class Knot:
    def __init__(self) -> None:
        self.point = Point(0,0)
        self.history = [self.point]

    def isNextTo(self, point: Point) -> bool:
        difference = point - self.point
        absoluteDifference = difference.absolute()
        stepsFromPoint = max(absoluteDifference.x, absoluteDifference.y)
        return stepsFromPoint < 2

    def moveTowards(self, point: Point):
        difference = point - self.point
        normalDifference = difference.normalize()
        self.move(normalDifference)

    def move(self, direction: Point):
        self.point += direction
        self.history.append(self.point)


class Rope:
    def __init__(self, length = 2) -> None:
        self.knots = [Knot() for _ in range(length)]

    def move(self, direction: Point):
        def tryReverseMoveAnyKnot() -> bool:
            knots = self.knots[::-1]
            for knot, nextKnot in zip(knots, knots[1:]+[None]):
                if nextKnot is None:
                    break
                if knot.isNextTo(nextKnot.point):
                    continue
                knot.moveTowards(nextKnot.point)
                return True
            return False

        firstKnot = self.knots[0]
        finalDestination = firstKnot.point+direction
        while firstKnot.point != finalDestination:
            firstKnot.moveTowards(finalDestination)
            while True:
                if not tryReverseMoveAnyKnot(): break





