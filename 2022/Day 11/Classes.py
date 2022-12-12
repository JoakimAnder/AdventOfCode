from typing import Callable, List


class Monkey:
    def __init__(self, id: int, items: List[int], operation: Callable, test: Callable, divisionCheckNum: int = None) -> None:
        self.id = id
        self.items = items
        self.operation = operation
        self.test = test
        self.inspections = 0
        self.divisionCheckNum = divisionCheckNum

    class Parser:
        def _multiply(x: int, y: int) -> int:
            return x*y
        def _add(x: int, y: int) -> int:
            return x+y
        operationParser = {
            '+': _add,
            '*': _multiply,
        }

        def getId(input: List[str]) -> int:
            idLine = input[0]
            idStr = idLine[len('Monkey '): idLine.find(':')]
            return int(idStr)
            
        def getItems(input: List[str]) -> List[int]:
            items = [int(item) for item in filter(lambda x: x, input[1].split(':')[-1].split(','))]
            return items

        def getOperation(input: List[str]) -> Callable:
            operationStr = input[2][len("Operation: new = "):]
            (xStr,operationStr,yStr) = input[2].split('=')[-1].strip().split(' ')
            operation = Monkey.Parser.operationParser[operationStr]

            if xStr == 'old' and yStr == 'old':
                return lambda x: operation(x,x)
            if xStr == 'old':
                return lambda x: operation(x, int(yStr))
            if yStr == 'old':
                return lambda y: operation(int(xStr), y)
            return lambda _: operation(int(xStr), int(yStr))

        def getTest(input: List[str]) -> Callable:
            divisibleBy = int(input[3].split(' ')[-1])
            trueMonkeyId = int(input[4].split(' ')[-1])
            falseMonkeyId = int(input[5].split(' ')[-1])

            return lambda x: trueMonkeyId if x % divisibleBy == 0 else falseMonkeyId

        def getDivisionCheckNum(input: List[str]) -> int:
            divisibleBy = int(input[3].split(' ')[-1])

            return divisibleBy

