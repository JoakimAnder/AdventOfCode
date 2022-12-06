INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
START_OF_PACKET_SIZE = 4

def getStartOfPacket(data: str) -> str:
    for i in range(len(data)-START_OF_PACKET_SIZE):
        marker = data[i:i+START_OF_PACKET_SIZE]
        if len(set(marker)) == START_OF_PACKET_SIZE:
            return marker

    return None

with open(INPUT_BASE_DIR+'InputSmall.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        startOfPacket = getStartOfPacket(line)
        print('StartOfPacket:', startOfPacket, line.index(startOfPacket)+START_OF_PACKET_SIZE)







