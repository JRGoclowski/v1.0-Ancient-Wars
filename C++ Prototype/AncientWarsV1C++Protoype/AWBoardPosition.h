#include <ostream>
#include <istream>
#include <vector>
#include "AWBoardDirection.h"

class AWBoardPosition {

private:

	char mRow, mColumn;

public:

	AWBoardPosition();

	AWBoardPosition(const char& row, const char& column);

	inline const char& getRow() const { return mRow; }

	inline const char& getColumn() const { return mColumn; }

	bool InBounds(char rows, char columns);

	bool InBounds(int boardSize);

	operator std::string() const;

	friend std::istream& operator>>(std::istream& lhs, AWBoardPosition& rhs);

	bool operator==(AWBoardPosition rhs);

	bool operator<(AWBoardPosition rhs);

	AWBoardPosition operator+(AWBoardDirection dir);

	static std::vector<AWBoardPosition> GetRectangularPositions(char rows, char columns);
};

std::ostream& operator<< (std::ostream& lhs, AWBoardPosition rhs);