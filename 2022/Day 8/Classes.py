
from dataclasses import dataclass
from typing import List

@dataclass
class Tree:
    height: int
    score: int = 0
    visibleX: bool = False
    visibleY: bool = False

