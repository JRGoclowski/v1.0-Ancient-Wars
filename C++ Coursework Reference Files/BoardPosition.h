#pragma once
#include "BoardDirection.h"
#include <ostream>
#include <istream>
#include <vector>

class BoardPosition {

private:
	
	char mRow, mColumn;

public: 

	BoardPosition();
	
	BoardPosition(const char& row, const char& column);
	
	inline const char& getRow() const { return mRow; }

	inline const char& getColumn() const { return mColumn; }
	
	bool InBounds(char rows, char columns);

	bool InBounds(int boardSize);

	operator std::string() const;

	friend std::istream& operator>>(std::istream &lhs, BoardPosition& rhs);

	bool operator==(BoardPosition rhs);

	bool operator<(BoardPosition rhs);

	BoardPosition operator+(BoardDirection dir);

	static std::vector<BoardPosition> GetRectangularPositions(char rows, char columns);
};

std::ostream& operator<< (std::ostream &lhs, BoardPosition rhs);