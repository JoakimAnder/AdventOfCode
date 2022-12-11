from dataclasses import dataclass
from abc import ABC, abstractmethod



class Instruction(ABC):
    @dataclass
    class Result:
        isDone: bool
        value: any = None

    @abstractmethod
    def execute(self) -> Result: pass

class Addx(Instruction):
    cycleCost = 2

    def __init__(self, value) -> None:
        super().__init__()
        self.value = value
        self._cycleProgress = 0

    def execute(self) -> Instruction.Result:
        self._cycleProgress += 1
        if self._cycleProgress >= self.cycleCost:
            return Instruction.Result(True, value=self.value)
        return Instruction.Result(False)

class Noop(Instruction):
    cycleCost = 1

    def execute(self) -> Instruction.Result:
        return Instruction.Result(True)

class Sprite:
    def __init__(self, size: int) -> None:
        self.x = 1
        self.size = size

    def shouldFillPixel(self, x:int) -> bool:
        min = self.x - self.size/2
        max = self.x + self.size/2
        result = min <= x and x <= max
        return result