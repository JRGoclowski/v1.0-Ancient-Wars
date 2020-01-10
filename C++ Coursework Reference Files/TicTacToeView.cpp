#include <iostream>
#include <ostream>
#include <sstream>
#include <string>

#include "TicTacToeView.h"
#include "TicTacToeBoard.h"
#include "TicTacToeMove.h"

using namespace std;

std::unique_ptr<AWGameMove> TicTacToeView::ParseMove(const std::string& move) const {
	if (move == "pass") {
		return std::move(make_unique<TicTacToeMove>());
	}
	int sRow, sCol;
	char openParen, comma, closeParen;
	istringstream passedString(move);
	passedString >> openParen >> sRow >> comma >> sCol >> closeParen;
	return std::move(make_unique<TicTacToeMove>(AWBoardPosition(sRow, sCol)));
}

void TicTacToeView::PrintBoard(std::ostream& s) const {
	TicTacToeBoard::Player playerAt;
	std::array <char, 4> title = { '-', '0', '1', '2'};
	for (int column = 0; column < 4; column++) {
		s << title[column];
	}
	s << endl;
	for (int row = 1; row < 4; row++) {
		s << title[row];
		for (int column = 0; column < 3; column++) {
			playerAt = mTicTacToeBoard->mBoard[(row - 1)][column];
			if (playerAt == TicTacToeBoard::Player::EMPTY) {
				s << '.';
			}
			else if (playerAt == TicTacToeBoard::Player::EX) {
				s << 'X';
			}
			else if (playerAt == TicTacToeBoard::Player::OH) {
				s << 'O';
			}
		}
		s << endl;
	}
}

std::string TicTacToeView::GetPlayerString(int player) const {
	return player == 1 ? "X" : "O";
}
