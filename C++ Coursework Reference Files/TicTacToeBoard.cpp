#include "TicTacToeBoard.h"
#include "TicTacToeMove.h"
#include "TicTacToeView.h"
#include "Functional.h"
#include <memory>

using namespace std;

TicTacToeBoard::TicTacToeBoard() : mBoard{Player::EMPTY}, mValue(0), mCurrentPlayer(Player::EX) {
	mHistory.clear();
}

std::vector<std::unique_ptr<AWGameMove>> TicTacToeBoard::GetPossibleMoves() const {
	auto possMoves = std::vector<std::unique_ptr<AWGameMove>>();
	for (AWBoardPosition currentPos : AWBoardPosition::GetRectangularPositions(BOARD_SIZE, BOARD_SIZE)) {
		if (GetPlayerAtPosition(currentPos) == Player::EMPTY) {
			possMoves.push_back(make_unique<TicTacToeMove>(currentPos));
		}
	}
	return possMoves;
}

void TicTacToeBoard::ApplyMove(std::unique_ptr<AWGameMove> move) {
	TicTacToeMove* lMove = dynamic_cast<TicTacToeMove*>(move.get());
	mBoard[lMove->mPosition.getRow()][lMove->mPosition.getColumn()] = mCurrentPlayer;
	(mCurrentPlayer == Player::EX) ? (mCurrentPlayer = Player::OH) : (mCurrentPlayer = Player::EX);
	mHistory.push_back(std::move(move));
}

void TicTacToeBoard::UndoLastMove() {
	auto lastMove = dynamic_cast<TicTacToeMove*>(&(**mHistory.rbegin()));
	mBoard[lastMove->mPosition.getRow()][lastMove->mPosition.getColumn()] = Player::EMPTY;
	mHistory.pop_back();
}

bool TicTacToeBoard::IsFinished() const {
	auto lPositions = AWBoardPosition::GetRectangularPositions(BOARD_SIZE, BOARD_SIZE);
	auto lPossMoves = GetPossibleMoves();
	if (lPossMoves.empty()) {
		return true;
	}
	for (AWBoardPosition p : lPositions) {
		for (BoardDirection d : BoardDirection::CARDINAL_DIRECTIONS) {
			if (isRow(p, d)) {
				return true;
			}
		}
	}
	return false;
}

int TicTacToeBoard::GetValue() const {
	if (!IsFinished()) {
		return 0;
	}
	else {
		if (GetPossibleMoves().empty()) {
			return 0;
		}
		return (static_cast <int> (GetPlayerAtPosition(PartOfRow())));
	}
}


bool TicTacToeBoard::isRow(AWBoardPosition pPosition, BoardDirection pDirection) const{ 
	int count = 0;
	auto playerChecked = GetPlayerAtPosition(pPosition);
	auto positionWalker = pPosition;
	if (!(playerChecked == Player::EMPTY)){
		while (positionWalker.InBounds(BOARD_SIZE)) {
			if (GetPlayerAtPosition(positionWalker) == playerChecked) {
				count++;
			}
			positionWalker = positionWalker + pDirection;
		}
	}
	return (count == 3);
}

AWBoardPosition TicTacToeBoard::PartOfRow() const {
	auto lPositions = AWBoardPosition::GetRectangularPositions(BOARD_SIZE, BOARD_SIZE);
	for (AWBoardPosition p : lPositions) {
		for (BoardDirection d : BoardDirection::CARDINAL_DIRECTIONS) {
			if (isRow(p, d)) {
				return p;
			}
		}
	}
}
