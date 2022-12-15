import functools
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

DIVIDER_PACKETS = [[[2]], [[6]]]
allPackets = [packet for pair in disressSignal.pairs for packet in [pair.left_packet, pair.right_packet]] + DIVIDER_PACKETS

allPackets.sort(key = functools.cmp_to_key(Pair.compareLists), reverse=True)

decoderKey = 1
for i in [allPackets.index(divider)+1 for divider in DIVIDER_PACKETS]:
    decoderKey *= i
print('decoderKey:', decoderKey, '\n', '\n'.join([str(packet) for packet in allPackets]))



