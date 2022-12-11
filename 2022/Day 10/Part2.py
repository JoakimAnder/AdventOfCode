from typing import Dict, List
from Classes import Addx, Noop, Instruction, Sprite
import logging

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

SCREEN_WIDTH = 40
currenctCycle = 0
sprite = Sprite(3)
screen = ''
with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        instruction = parseInput(line)
        while True:
            pixel = currenctCycle % SCREEN_WIDTH
            screen += '#' if sprite.shouldFillPixel(pixel) else '.'
            if pixel == SCREEN_WIDTH-1:
                screen += '\n'

            currenctCycle += 1
            result = instruction.execute()

            if result.isDone:
                if isinstance(instruction, Addx):
                    sprite.x += result.value
                break


print(screen)





