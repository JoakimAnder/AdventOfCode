from Classes import Directory, File, Command, ChangeDirectoryCommand, UpOneLevelCommand


INPUT_BASE_DIR = __file__[:__file__.rindex('/')+1] if '/' in __file__ else __file__[:__file__.rindex('\\')+1]



def parseCommand(input: str) -> Command:
    input = input[len(Command.prefix):].strip()
    if input.startswith(ChangeDirectoryCommand.commandName):
        input = input[len(ChangeDirectoryCommand.commandName):].strip()
        if input == UpOneLevelCommand.commandValue:
            return UpOneLevelCommand(input)
        return ChangeDirectoryCommand(input)
    return None

def parseDirectory(input: str) -> Directory:
    input = input[len(Directory.name):].strip()
    return Directory(input)

def parseFile(input: str) -> File:
    (inputSize, name) = input.split(' ')
    size = int(inputSize)
    return File(name, size)

def parseInput(input: str):
    if input.startswith(Command.prefix):
        return parseCommand(input)
    if input.startswith(Directory.name):
        return parseDirectory(input)
    return parseFile(input)

mainDir = Directory("/")
currentDirectory = mainDir

with open(INPUT_BASE_DIR+'Input.txt') as input:
    while True:
        line = input.readline()
        if not line: break
        line = line.strip()

        parsedInput = parseInput(line)
        if isinstance(parsedInput, Directory):
            currentDirectory.addDirectory(parsedInput)
        if isinstance(parsedInput, File):
            currentDirectory.addFile(parsedInput)
        if isinstance(parsedInput, Command):
            if isinstance(parsedInput, ChangeDirectoryCommand):
                if parsedInput.value == mainDir.name:
                    currentDirectory = mainDir
                else:
                    currentDirectory = parsedInput.execute(currentDirectory)

allDirectories = mainDir.getAllDirectories()
directorySizes = list(map(lambda dir: dir.getSize(), allDirectories))

MAX_TOTAL_SIZE = 70000000
UPDATE_FILE_SIZE = 30000000

occupiedSpace = mainDir.getSize()
freeSpace = MAX_TOTAL_SIZE - occupiedSpace
minSizeToDelete = UPDATE_FILE_SIZE - freeSpace

directorySizes.sort()
smallestFittingDirectory = list(filter(lambda size: size > minSizeToDelete, directorySizes))[0]

print('smallestDirectorySize:', smallestFittingDirectory, 'minSizeToDelete:', minSizeToDelete, 'freeSpace:', freeSpace, 'occupiedSpace:', occupiedSpace)






