#pragma once
#include <array>

class AWBoardDirection {
private:
	char mRowChange, mColumnChange;

public:
	AWBoardDirection();
	AWBoardDirection(const char& rowChange, const char& columnChange);
	static ::std::array<AWBoardDirection, 8> CARDINAL_DIRECTIONS;
	inline const char& getRowChange() { return mRowChange; }
	inline const char& getColumnChange() { return mColumnChange; }

};