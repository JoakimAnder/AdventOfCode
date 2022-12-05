
from typing import List


class Procedure:
    def __init__(self, boxAmount: int, fromStack: int, toStack: int) -> None:
        self.boxAmount = boxAmount
        self.fromStack = fromStack
        self.toStack = toStack

class Stack:
    def __init__(self) -> None:
        self._boxes: list[str] = []

    def take(self, boxAmount: int) -> List[str]:
        boxes = self._boxes[:boxAmount]
        self._boxes = self._boxes[boxAmount:]
        return boxes

    def place(self, boxes: List[str]):
        self._boxes = boxes + self._boxes

    def showTopBox(self) -> str:
        return self._boxes[0]

class Supply:
    def __init__(self, stacks: List[Stack]) -> None:
        self._stacks = stacks

    def showTopBoxes(self) -> List[str]:
        return list(map(lambda stack: stack.showTopBox(), self._stacks))

    def rearrange(self, procedure: Procedure):
        for i in range(procedure.boxAmount):
            boxes = self._stacks[procedure.fromStack-1].take(1)
            self._stacks[procedure.toStack-1].place(boxes)
    
    def rearrange9001(self, procedure: Procedure):
        boxes = self._stacks[procedure.fromStack-1].take(procedure.boxAmount)
        self._stacks[procedure.toStack-1].place(boxes)



