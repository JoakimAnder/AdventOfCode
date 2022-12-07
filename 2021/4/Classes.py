from dataclasses import dataclass
from typing import List

class BingoBoard:
    def __init__(self, numbers: List[int], width: int = 5) -> None:
        self._numbers = [*numbers]
        self._markedSpots = list(map(lambda _: False, numbers))
        self._width = width

    def mark(self, number: int):
        for i in range(len(self._numbers)):
            if self._numbers[i] == number:
                self._markedSpots[i] = True

    def hasWon(self) -> bool:
        for i in range(self._width):
            rowWon = all(self._markedSpots[i:i+self._width])
            if rowWon:
                return True
            columnWon = all(self._markedSpots[i::self._width])
            if columnWon:
                return True
        return False

    def calculateScore(self, winningNumber: int) -> int:
        unmarkedNumbers = []
        for i in range(len(self._numbers)):
            if not self._markedSpots[i]:
                unmarkedNumbers.append(self._numbers[i])
        return winningNumber * sum(unmarkedNumbers)

@dataclass
class BingoResult:
    winningNumber: int
    winningBoards: List[BingoBoard]

class BingoGame:
    def __init__(self, numbers: List[int], boards: List[BingoBoard]) -> None:
        self._numbers = [*numbers]
        self._boards: List[BingoBoard] = [*boards]

    def getResult(self) -> BingoResult:
        for number in self._numbers:
            for board in self._boards:
                board.mark(number)
            winningBoards = list(filter(lambda board: board.hasWon(), self._boards))
            if len(winningBoards) > 0:
                return BingoResult(number, winningBoards)