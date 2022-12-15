from dataclasses import dataclass, field
from typing import Callable, List


@dataclass
class Pair:
    left_packet: list
    right_packet: list

    @staticmethod
    def compare(left, right) -> int:
        if type(left) is not type(right):
            if isinstance(left, int) and not isinstance(right, int):
                return Pair.compare([left], right)
            if isinstance(left, list) and not isinstance(right, list):
                return Pair.compare(left, [right])
            raise Exception(f'Type not expected, {type(left)} or {type(right)}')
        
        if isinstance(left, int):
            return Pair.compareInts(left, right)
        return Pair.compareLists(left, right)

    @staticmethod
    def compareLists(leftList: list, rightList:list) -> int:
        comparison = 0
        for (left, right) in zip(leftList, rightList):
            comparison = Pair.compare(left, right)
            if comparison == 0:
                continue
            return comparison

        return Pair.compareInts(len(leftList), len(rightList))

    @staticmethod
    def compareInts(left: int, right:int) -> int:
        if left == right: return 0
        return 1 if left < right else -1
        

    def is_correct_order(self):
        return Pair.compareLists(self.left_packet, self.right_packet) >= 0


@dataclass
class DistressSignal:
    pairs: List[Pair] = field(default_factory=list)








