from dataclasses import dataclass, field
from typing import Callable, List


@dataclass
class Square:
    id: int 
    height: int
    neighbors: List = field(default_factory=list)
    score: int = None

    def add_neighbor(self, square):
        if square.height <= self.height + 1:
            self.neighbors.append(square)

    def try_score_self(self, parent) -> bool:
        filteredNeighbors = list(filter(lambda x: x.score is not None, self.neighbors))
        if len(filteredNeighbors) == 0:
            return False
        minNeighborScore = min([n.score for n in filteredNeighbors])
        shouldSetScore = self.score is None or self.score > minNeighborScore+1
        if shouldSetScore:
            self.score = minNeighborScore+1
            return True
        return False

        

    def try_score_neighbors(self) -> bool:
        # Neighbors where score is unset or more than self's score+1
        filteredNeighbors = list(filter(lambda x: x.score is None or x.score > self.score+1, self.neighbors))
        if len(filteredNeighbors) == 0:
            return False
        for neighbor in filteredNeighbors:
            neighbor.score = self.score+1
        return True


        


class Start(Square): pass
class End(Square): pass












