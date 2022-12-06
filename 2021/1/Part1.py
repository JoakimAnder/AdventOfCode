INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]


lastMeasurement = None
increases = 0

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        currentMeasurement = int(line)
        if lastMeasurement == None: 
            lastMeasurement = currentMeasurement
            continue
        
        if currentMeasurement > lastMeasurement:
            increases += 1

        lastMeasurement = currentMeasurement

print('Increases:', increases)







