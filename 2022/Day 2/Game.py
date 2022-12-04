from enum import Enum

class Game:
    class Move(Enum):
        rock = 1
        paper = 2
        scissors = 3

        def calculatePoints(self):
            return self.value

    class Result(Enum):
        lose = 0
        draw = 3
        win = 6

        def calculatePoints(self):
            return self.value
            
    GAME_RESULT_CALCULATOR = {
        Move.rock : {
            Move.rock : Result.draw,
            Move.paper : Result.win,
            Move.scissors : Result.lose,
        },
        Move.paper : {
            Move.rock : Result.lose,
            Move.paper : Result.draw,
            Move.scissors : Result.win,
        },
        Move.scissors : {
            Move.rock : Result.win,
            Move.paper : Result.lose,
            Move.scissors : Result.draw,
        },
    }

    def calculateResult(opponentMove: Move, playerMove: Move) -> Result:
        return Game.GAME_RESULT_CALCULATOR[opponentMove][playerMove]

    def calculateMove(opponentMove: Move, expectedResult: Result) -> Move:
        for (playerMove, result) in Game.GAME_RESULT_CALCULATOR[opponentMove].items():
            if result == expectedResult:
                return playerMove
        raise Exception(f'Move not found for move "{opponentMove}" and result "{expectedResult}"')

    def __init__(self, opponentMove: Move = None, playerMove: Move = None, result: Result = None) -> None:
        self._opponentMove = opponentMove
        self._playerMove = playerMove
        self._result = result

    def getOpponentMove(self) -> Move:
        if self._opponentMove:
            return self._opponentMove
        return Game.calculateMove(self._playerMove, self._result)
    def getPlayerMove(self) -> Move:
        if self._playerMove:
            return self._playerMove
        return Game.calculateMove(self._opponentMove, self._result)
    def getGameResult(self) -> Result:
        if self._result:
            return self._result
        return Game.calculateResult(self._opponentMove, self._playerMove)
    
    def getPoints(self) -> int:
        playerMovePoints = self.getPlayerMove().calculatePoints()
        gameResultPoints = self.getGameResult().calculatePoints()
        return playerMovePoints + gameResultPoints
