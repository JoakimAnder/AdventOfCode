from typing import Dict
from Classes import Addx, Noop, Instruction

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]




def parseInput(input: str) -> Instruction:
    splitInput = input.split(' ')
    inputInstruction = splitInput[0]
    if inputInstruction == 'noop':
        return Noop()
    if inputInstruction == 'addx':
        value = int(splitInput[1])
        return Addx(value)

def shouldSaveSiglanStrength(cycle: int) -> bool:
    return (cycle - 20) % 40 == 0

currenctCycle = 0
x = 1
signalStrengths: Dict[int, int] = {}
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        instruction = parseInput(line)
        while True:
            currenctCycle += 1
            if shouldSaveSiglanStrength(currenctCycle):
                signalStrengths[currenctCycle] = currenctCycle*x

            result = instruction.execute()
            if result.isDone:
                if isinstance(instruction, Addx):
                    x += result.value
                break


signalStrengthsToGet = [20, 60, 100, 140, 180, 220]
sum = 0
for strength in signalStrengthsToGet:
    sum += signalStrengths[strength]

print(sum)





