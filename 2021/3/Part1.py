INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

values = []


with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        values.append(line)

amountOfValues = len(values)
binaryGammaRate = ''
for i in range(len(values[0])):
    amountOfOnes = len(list(filter(lambda x: x == '1', [value[i] for value in values])))
    binaryGammaRate += '1' if amountOfOnes > amountOfValues/2 else '0'

gammaRate = 0
epsilonRate = 0
for i in range(len(binaryGammaRate)):
    value = 2 ** i
    if binaryGammaRate[::-1][i] == '1':
        gammaRate += value
    else:
        epsilonRate += value

powerConsumption = gammaRate*epsilonRate
print('gammaRate:', gammaRate, 'epsilonRate:', epsilonRate, 'powerConsumption:', powerConsumption)







