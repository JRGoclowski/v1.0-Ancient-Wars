#include "OthelloBoard.h"


OthelloBoard::OthelloBoard() :mBoard {Player::EMPTY} , mValue(0), mMoveCount(0), mNextPlayer(Player::BLACK) {
	mBoard[(BOARD_SIZE / 2) - 1][(BOARD_SIZE / 2) - 1] = Player::WHITE ;
	mBoard[(BOARD_SIZE / 2) - 1][(BOARD_SIZE / 2)] = Player::BLACK;
	mBoard[(BOARD_SIZE / 2)][(BOARD_SIZE / 2) - 1] = Player::BLACK;
	mBoard[(BOARD_SIZE / 2)][(BOARD_SIZE / 2)] = Player::WHITE;
	mHistory.clear(); 
}


std::vector<std::unique_ptr<AWGameMove>> OthelloBoard::GetPossibleMoves() const {
	auto possMoves = std::vector<std::unique_ptr<AWGameMove>>();
	for (BoardPosition currentPos : BoardPosition::GetRectangularPositions(BOARD_SIZE, BOARD_SIZE)) {
		if (GetPlayerAtPosition(currentPos) != Player::EMPTY) {
			continue;
		}
		for (BoardDirection currentDir : BoardDirection::CARDINAL_DIRECTIONS) {
			int flipCounter = 0;
			auto moveWalker = currentPos + currentDir;
			while (InBounds(moveWalker) && PositionIsEnemy(moveWalker, mNextPlayer)) {
				flipCounter++;
				moveWalker = moveWalker + currentDir;
			}
			if (InBounds(moveWalker) && flipCounter > 0) {
				if (GetPlayerAtPosition(moveWalker) == mNextPlayer) {
					possMoves.push_back(std::make_unique<OthelloMove>(currentPos));
					break;
				}
			}
		}
	}
	if (possMoves.empty()) {
		possMoves.push_back(std::make_unique<OthelloMove>(BoardPosition(-1, -1)));
	}
	return std::move(possMoves);
}

void OthelloBoard::ApplyMove(std::unique_ptr<AWGameMove> m) {
	OthelloMove* move = dynamic_cast<OthelloMove*>(m.get());
	if (!(move->IsPass())) {
		mMoveCount++;
		for (BoardDirection currentDir : BoardDirection::CARDINAL_DIRECTIONS) {
			int flipCounter = 0;
			auto moveWalker = move->mPosition + currentDir;
			while (InBounds(moveWalker) && PositionIsEnemy(moveWalker, mNextPlayer)) {
				flipCounter++;
				moveWalker = moveWalker + currentDir;
			}
			if (InBounds(moveWalker) && GetPlayerAtPosition(moveWalker) == mNextPlayer && flipCounter > 0) {
				auto applyWalker = move->mPosition;
				while (!(applyWalker + currentDir == moveWalker)) {
					applyWalker = applyWalker + currentDir;
					mBoard[applyWalker.getRow()][applyWalker.getColumn()] = mNextPlayer;
					mValue = mValue + 2 * (static_cast <int> (mNextPlayer));
				}
				move->AddFlipSet(OthelloMove::FlipSet::FlipSet((char)flipCounter, currentDir));
			}
		}
		mBoard[move->mPosition.getRow()][move->mPosition.getColumn()] = mNextPlayer;
		mValue = mValue + static_cast <int> (mNextPlayer);
	}
	(mNextPlayer == Player::BLACK) ? (mNextPlayer = Player::WHITE) : (mNextPlayer = Player::BLACK);
	mHistory.push_back(std::move(m));
}

void OthelloBoard::UndoLastMove() {
	auto lastMove = dynamic_cast<OthelloMove*>(&(**mHistory.rbegin()));
	if (!(lastMove->IsPass())) {
		for (OthelloMove::FlipSet currentFlips : lastMove->mFlips) {
			BoardPosition currentPos = lastMove->mPosition + currentFlips.mDirection;
			for (int i = 0; i < (int)currentFlips.mFlipCount; i++) {
				mBoard[currentPos.getRow()][currentPos.getColumn()] = mNextPlayer;
				mValue = mValue + (2 * (static_cast <int> (mNextPlayer)));
				currentPos = currentPos + currentFlips.mDirection;
			}
		}
		mValue = mValue + (static_cast <int> (mNextPlayer));
		mBoard[lastMove->mPosition.getRow()][lastMove->mPosition.getColumn()] = Player::EMPTY;
		mMoveCount--;
	}
	(mNextPlayer == Player::BLACK) ? (mNextPlayer = Player::WHITE) : (mNextPlayer = Player::BLACK);
	mHistory.pop_back();	
}

bool OthelloBoard::IsFinished() const {
	if (mMoveCount >= 60) {
		return true;
	}
	OthelloMove* lastMove = dynamic_cast<OthelloMove*>(&(**mHistory.rbegin()));
	if (lastMove->IsPass()) {
		auto secondToLast = mHistory.rbegin();
		secondToLast++;
		OthelloMove* secondLastMove = dynamic_cast<OthelloMove*>(&(**secondToLast));
		if (secondLastMove->IsPass()) {
			return true;
		}
	}
	return false;
}
