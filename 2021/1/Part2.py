INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
from typing import List
WINDOW_SIZE = 3



measurements: List[int] = []
lastMeasurement = None
increases = 0

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break

        depth = int(line)
        measurements.append(depth)
        if len(measurements) < WINDOW_SIZE:
            continue

        currentMeasurement = sum(measurements)
        measurements.pop(0)

        if lastMeasurement == None: 
            lastMeasurement = currentMeasurement
            continue
    
        if currentMeasurement > lastMeasurement:
            increases += 1

        lastMeasurement = currentMeasurement

print('Increases:', increases)







