
from typing import List


class File:
    def __init__(self, name: str, size:int) -> None:
        self.name = name
        self.size = size

class Directory:
    name = 'dir'
    def __init__(self, name: str) -> None:
        self.name = name
        self.parent:Directory = None
        self._directories: dict[str, Directory] = {}
        self._files: dict[str, File] = {}

    def addFile(self, file):
        self._files[file.name] = file

    def addDirectory(self, directory):
        directory.parent = self
        self._directories[directory.name] = directory

    def getFile(self, name: str) -> File:
        return self._files[name]

    def getDirectory(self, name: str):
        return self._directories[name]

    def getFiles(self) -> List[File]:
        return list(self._files.values())

    def getDirectories(self):
        return list(self._directories.values())

    def getAllDirectories(self):
        directories = []
        for dir in self.getDirectories():
            directories.append(dir)
            directories += dir.getAllDirectories()
        return directories

    def getSize(self) -> int:
        totalSize = 0
        totalSize += sum(map(lambda dir: dir.getSize(), self._directories.values()))
        totalSize += sum(map(lambda file: file.size, self._files.values()))
        return totalSize

class Command:
    prefix = '$'

class ChangeDirectoryCommand(Command):
    commandName = 'cd'

    def __init__(self, value: str) -> None:
        super().__init__()
        self.value = value

    def execute(self, directory: Directory) -> Directory:
        return directory.getDirectory(self.value)

class UpOneLevelCommand(ChangeDirectoryCommand):
    commandValue = '..'
    def execute(self, directory: Directory) -> Directory:
        return directory.parent




