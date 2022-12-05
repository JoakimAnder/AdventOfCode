from typing import List
from Classes import Procedure, Stack, Supply

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
STACK_NAME_SPACING = '   '
STACK_BOX_SPACING_SIZE = 1
STACK_BOX_SIZE = 3

inputStacks: List[str] = []
supply: Supply = None

def parseInputStacks() -> Supply:
    stackNames = inputStacks.pop(0).strip().split(STACK_NAME_SPACING)
    stacks = []
    for i in range(len(stackNames)):
        stack = Stack()
        for input in inputStacks:
            boxStart = i*STACK_BOX_SIZE + i*STACK_BOX_SPACING_SIZE
            boxEnd = boxStart+STACK_BOX_SIZE
            box = input[boxStart:boxEnd]
            if box.strip() == '':
                continue
            boxName = box[1]
            stack.place([boxName])
        stacks.append(stack)
    return Supply(stacks)

def parseInputProcedure(line: str) -> Procedure:
    (_, _, line) = line.partition('move')
    (moveInput, _, line) = line.partition('from')
    (fromInput, _, toInput) = line.partition('to')

    boxAmount = int(moveInput)
    fromStack = int(fromInput)
    toStack = int(toInput)
    procedure = Procedure(boxAmount, fromStack, toStack)
    return procedure

shouldParseProcedures = False
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break

        if line == '\n':
            shouldParseProcedures = True
            supply = parseInputStacks()
            continue

        if not shouldParseProcedures:
            inputStacks.insert(0, line)
            continue
        
        procedure = parseInputProcedure(line)
        supply.rearrange(procedure)

topBoxes = supply.showTopBoxes()
print('Top crates:', topBoxes, ''.join(topBoxes))







