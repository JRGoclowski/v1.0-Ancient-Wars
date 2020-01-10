# include "OthelloMove.h"
# include <ostream>
# include <sstream>


bool OthelloMove::operator==(const AWGameMove & rhs) const{
	const OthelloMove* move = dynamic_cast<const OthelloMove*> (&rhs);
	return ((mPosition.getColumn() == move->mPosition.getColumn()) && (mPosition.getRow() == move->mPosition.getRow()));
}

// TODO check this is the correct format
OthelloMove::operator std::string() const {
	std::ostringstream moveStr;
	if (mPosition.getRow() == -1 && mPosition.getColumn() == -1) {
		return "pass";
	}
	moveStr << "(" << (int) mPosition.getRow() << "," << (int) mPosition.getColumn() << ")";
	return moveStr.str();
}