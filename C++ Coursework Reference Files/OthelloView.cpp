#include <iostream>
#include <ostream>
#include <sstream>
#include <string>
#include "OthelloView.h"
#include "OthelloBoard.h"

using namespace std;

void OthelloView::PrintBoard(std::ostream & s) const {
	OthelloBoard::Player playerAt;
	std::array <char, 9> title = { '-', '0', '1', '2', '3', '4', '5', '6', '7' };
	for (int column = 0; column < 9; column++) {
		s << title[column];
	}
	s << endl;
	for (int row = 1; row < 9; row++) {
		s << title[row];
		for (int column = 0; column < 8; column++) {
			playerAt = mOthelloBoard->mBoard[(row - 1)][column];
			if (playerAt == OthelloBoard::Player::EMPTY) {
				s << '.';
			}
			else if (playerAt == OthelloBoard::Player::BLACK) {
				s << 'B';
			}
			else if (playerAt == OthelloBoard::Player::WHITE) {
				s << 'W';
			}
		}
		s << endl;
	}
}

std::unique_ptr<AWGameMove> OthelloView::ParseMove(const std::string & strFormat) const {
	if (strFormat == "pass") {
		return std::move(make_unique<OthelloMove>());
	}
	int sRow, sCol;
	char openParen, comma, closeParen;
	istringstream passedString(strFormat);
	passedString >> openParen >> sRow >> comma >> sCol >> closeParen;
	return std::move(make_unique<OthelloMove>(BoardPosition(sRow, sCol)));
}


std::ostream & operator<<(std::ostream & lhs, const OthelloView & rhs) {
	rhs.PrintBoard(lhs);
	(rhs.mOthelloBoard->GetCurrentPlayer() == 1) ? (lhs << "Black") : (lhs << "White");
	return lhs << "'s Move";
}

std::ostream & operator<<(std::ostream & lhs, const OthelloMove & rhs) {
	return lhs << string(rhs);
}

std::string OthelloView::GetPlayerString(int player) const {
	return player == 1 ? "Black" : "White";
}
