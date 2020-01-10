#pragma once
#include <array>

class BoardDirection {
private:
	char mRowChange, mColumnChange;

public:
	BoardDirection();
	BoardDirection(const char& rowChange, const char& columnChange);
	static ::std::array<BoardDirection, 8> CARDINAL_DIRECTIONS;
	inline const char& getRowChange() { return mRowChange; }
	inline const char& getColumnChange() { return mColumnChange; }
	
};