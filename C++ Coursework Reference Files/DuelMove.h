#pragma once

#include <string>
#include <vector>
#include "AWBoardPosition.h"
#include "AWBoardDirection.h"
#include "AWGameMove.h"

/*
An OthelloMove serves two purposes: identifying a position on an othello board
where a move can be made, and if that move is actually applied to the board, remembering
enough information that the move can be undone in the future.
*/
class DuelMove : public AWGameMove {
private:
	// An OthelloMove consists only of a position on the board.
	AWBoardPosition mPosition;

	/*
	A FlipSet tracks a direction and # of pieces that got flipped when this
	move was applied. See spec.
	*/
	class FlipSet {
	public:
		FlipSet(char flips, BoardDirection dir) : mFlipCount(flips), mDirection(dir) {
		}

		char mFlipCount;
		BoardDirection mDirection;
	};

	// If it is applied to the board, a move will save a number of FlipSets representing
	// the changes made to the board as a result.
	std::vector<FlipSet> mFlips;


	// A method for adding a FlipSet to a move's vector of flips.
	inline void AddFlipSet(FlipSet set) { 
		mFlips.push_back(set); 
	}

	friend class DuelBoard;

public:
	// Default constructor: initializes this move as a PASS.
	DuelMove() : mPosition(-1, -1) {

	}
	/*
	1-parameter constructor: initializes this move with the given position.
	*/
	DuelMove(AWBoardPosition p) : mPosition(p) {
	}
	
	/*
	Compares two OthelloMove objects for equality. Two moves are equal if they
	have the same position.
	*/
	bool operator==(const AWGameMove &rhs) const override;
	
	/*
	Converts the OthelloMove into a string representation, one that could be
	used correctly with operator=(string).
	*/
	operator std::string() const override;

	// Returns true if the move represents a Pass.
	inline bool IsPass() const {
		return ((mPosition.getColumn() == -1) && (mPosition.getRow() == -1));
	}
};
