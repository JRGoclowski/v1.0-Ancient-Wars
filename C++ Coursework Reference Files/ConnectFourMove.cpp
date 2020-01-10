#include "ConnectFourMove.h"
#include <string>
using namespace std;

ConnectFourMove::ConnectFourMove(int column) : mColumn(column) {}
ConnectFourMove::ConnectFourMove() : mColumn(-1) {}

ConnectFourMove::operator std::string() const {
	// Convert the column to a letter from A to G.
	if (mColumn == -1) {
		return "pass";
	}
	return string(1, (char)('A' + mColumn));
}

bool ConnectFourMove::operator==(const AWGameMove & rhs) const {
	// Downcast and compare columns.
	const ConnectFourMove &m = dynamic_cast<const ConnectFourMove&>(rhs);
	return mColumn == m.mColumn;
}


