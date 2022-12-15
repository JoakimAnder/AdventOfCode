from json import JSONDecoder
from typing import Dict, List
from Classes import DistressSignal, Pair

INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]

disressSignal = DistressSignal()

with open(INPUT_BASE_DIR+'Input.txt') as input:
    decoder = JSONDecoder()
    while True:
        line1 = input.readline()
        if not line1: break
        line2 = input.readline()
        _ = input.readline()
        
        left = decoder.decode(line1)
        right = decoder.decode(line2)
        pair = Pair(left, right)
        disressSignal.pairs.append(pair)

result = {i+1: pair.is_correct_order() for (i, pair) in enumerate(disressSignal.pairs)}
result = [i for (i, _) in filter(lambda i: i[1], result.items())]
sum = sum(result)
print('sum:', sum, 'result:', result)



