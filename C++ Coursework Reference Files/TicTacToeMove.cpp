#include <ostream>
#include <sstream>

#include "TicTacToeMove.h"


TicTacToeMove::operator std::string() const {
	std::ostringstream moveStr;
	if (mPosition.getRow() == -1 && mPosition.getColumn() == -1) {
		return "pass";
	}
	moveStr << "(" << (int)mPosition.getRow() << "," << (int)mPosition.getColumn() << ")";
	return moveStr.str();
}

bool TicTacToeMove::operator==(const AWGameMove& rhs) const {
	const TicTacToeMove* move = dynamic_cast<const TicTacToeMove*> (&rhs);
	return ((this->mPosition.getColumn() == move->mPosition.getColumn()) && (this->mPosition.getRow() == move->mPosition.getRow()));
}
