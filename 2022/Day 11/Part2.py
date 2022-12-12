from typing import Dict, List
from Classes import Monkey

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]


def parseInput(input: List[str]) -> Monkey:
    id = Monkey.Parser.getId(input)
    items = Monkey.Parser.getItems(input)
    operation = Monkey.Parser.getOperation(input)
    test = Monkey.Parser.getTest(input)
    divisionCheckNum = Monkey.Parser.getDivisionCheckNum(input)
    return Monkey(id, items, operation, test, divisionCheckNum)

monkeys: Dict[int, Monkey] = {}
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        if(line.startswith("Monkey")):
            inputMonkey = [line]
            for _ in range(5):
                inputMonkey.append(input.readline().strip())
            monkey = parseInput(inputMonkey)
            monkeys[monkey.id] = monkey

checkNums = set(map(lambda m: m.divisionCheckNum, monkeys.values()))
checkNum = 1
for c in checkNums:
    checkNum *= c

ROUNDS = 10000
for round in range(ROUNDS):
    for monkey in monkeys.values():
        while monkey.items:
            item = monkey.items.pop(0)
            # inspect
            worryLevel = monkey.operation(item)
            monkey.inspections += 1
            # relief
            worryLevel = worryLevel%checkNum
            # throw
            monkeyId = monkey.test(worryLevel)
            monkeys[monkeyId].items.append(worryLevel)
    if round % 1000 == 0:
        print(f'Round {round}\n\t'+'\n\t'.join(map(lambda monkey: f'Monkey {monkey.id} inspected items {monkey.inspections} times.', monkeys.values())))


output = '\n'.join(map(lambda monkey: f'Monkey {monkey.id} inspected items {monkey.inspections} times.', monkeys.values()))
print(output)

monkeyList = list(monkeys.values())
monkeyList.sort(key=lambda m: m.inspections, reverse=True)

monkeyBusiness  = monkeyList[0].inspections * monkeyList[1].inspections
print('monkeyBuiness:', monkeyBusiness)



