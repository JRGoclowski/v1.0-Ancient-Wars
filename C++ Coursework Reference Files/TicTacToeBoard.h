#pragma once
 
#include "BoardPosition.h"
#include "GameBoard.h"


/*
Represents the state of a game board, with methods to query and manipulate that state
to support a game-agnostic main application.
*/
class TicTacToeBoard : public GameBoard {
public:

	enum class Player : char {
		EMPTY = 0,
		EX = 1,
		OH = -1
	};

	TicTacToeBoard();

	std::vector<std::unique_ptr<GameMove>> GetPossibleMoves() const override;

	void ApplyMove(std::unique_ptr<GameMove> move) override;

	void UndoLastMove() override;

	bool IsFinished() const override;

	int GetValue() const override;

	int GetCurrentPlayer() const override {
		if (mCurrentPlayer == Player::EX) {
			return 1;
		}
		else {
			return -1;
		}
	}

	inline const std::vector<std::unique_ptr<GameMove>>& GetMoveHistory() const override {
		return mHistory;
	}


	static const int BOARD_SIZE = 3;

private:
	friend class TicTacToeView;

	std::array<std::array<Player, 3>, 3> mBoard;

	std::vector<std::unique_ptr<GameMove>> mHistory;

	Player mCurrentPlayer;

	int mValue;

	inline bool InBounds(BoardPosition p) const {
		return p.InBounds(BOARD_SIZE);
	}

	inline bool PositionIsSelf(BoardPosition position) const {
		Player atPosition = GetPlayerAtPosition(position);
		return (atPosition == mCurrentPlayer);
	}
	
	inline Player GetPlayerAtPosition(BoardPosition position) const {
		return mBoard[position.getRow()][position.getColumn()];
	}

	bool isRow(BoardPosition pPosition, BoardDirection pDirection) const;

	BoardPosition PartOfRow() const;
};

