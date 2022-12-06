INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]
START_OF_PACKET_SIZE = 4
START_OF_MESSAGE_SIZE = 14

def getStartOfSection(data: str, size: int) -> str:
    for i in range(len(data)-size):
        marker = data[i:i+size]
        if len(set(marker)) == size:
            return marker

    return None

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        startOfPacket = getStartOfSection(line, START_OF_MESSAGE_SIZE)
        print('StartOfPacket:', startOfPacket, line.index(startOfPacket)+START_OF_MESSAGE_SIZE)







