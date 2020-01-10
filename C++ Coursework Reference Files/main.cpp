#include <functional>
#include "TicTacToeBoard.h"
#include "TicTacToeMove.h"
#include "TicTacToeView.h"
#include "ConnectFourBoard.h"
#include "ConnectFourMove.h"
#include "ConnectFourView.h"
#include "OthelloBoard.h"
#include "OthelloView.h"
#include "OthelloMove.h"
#include "BoardPosition.h"
#include "BoardDirection.h"
#include "Functional.cpp"
#include <iostream>
#include <istream>
#include <string>
#include <sstream>
#include <vector>
#include <memory>

using namespace std;

int main(int argc, char* argv[]) {

	shared_ptr<GameBoard> board;
	unique_ptr<GameView> v;
	bool continueRunning = true, continuePlaying, validMove = false;
	unique_ptr<AWGameMove> pass;
	string moveFormat;

	while (continueRunning) {
		continuePlaying = true;
		cout << "Which game would you like to play?" << endl;
		cout << "1) Othello; 2) Tic Tac Toe; 3) Connect Four; 4) Exit;" << endl;
		string desiredGame;
		getline(cin, desiredGame);
		if (desiredGame == "1"){
			auto oBoard = make_shared<OthelloBoard>();
			v = make_unique<OthelloView>(oBoard);
			board = oBoard;
			pass = make_unique<OthelloMove>();
			moveFormat = "(r,c)";
		}
		else if (desiredGame == "2") {
			auto oBoard = make_shared<TicTacToeBoard>();
			v = make_unique<TicTacToeView>(oBoard);
			board = oBoard;
			pass = make_unique<TicTacToeMove>();
			moveFormat = "(r,c)";
		}
		else if (desiredGame == "3") {
			auto oBoard = make_shared<ConnectFourBoard>();
			v = make_unique<ConnectFourView>(oBoard);
			board = oBoard;
			pass = make_unique<ConnectFourMove>();
			moveFormat = "X";
		}
		else if (desiredGame == "4") {
			continuePlaying = false;
			continueRunning = false;

		}

		bool lastMovePass = false;
		while (continuePlaying) {
			validMove = false;
			string userInput;
			cout << *v << endl;
			cout << v->GetPlayerString(board->GetCurrentPlayer()) << "'s Turn" << endl << "Possible Moves: " << endl;
			std::vector<unique_ptr<AWGameMove>> possibleMoves = board->GetPossibleMoves();
			for (auto moveWalker = possibleMoves.begin(); moveWalker != possibleMoves.end(); moveWalker++) {
				cout << (string)(**moveWalker) << " ";
			}
			cout << endl << "[move "<< moveFormat << "][undo n][showValue][showHistory][quit]" << endl << "Please input action:";
			getline(cin, userInput);
			//cout << "Debug statment: Input was " << userInput << endl << endl << endl;
				if (userInput.substr(0, 4) == "move") {
				string moveInput = userInput.substr(5, userInput.capacity());
				if (v->ParseMove(moveInput) == pass) {
					board->ApplyMove(std::move(v->ParseMove(moveInput)));
					if (lastMovePass) {
						continuePlaying = false;
					}
					lastMovePass = true;
					validMove = true;
				}
				auto begin = possibleMoves.begin();
				auto end = possibleMoves.end();
				for (begin; begin != end; begin++) {
					if ((**begin) == *v->ParseMove(moveInput)) {
						board->ApplyMove(std::move(v->ParseMove(moveInput)));
						lastMovePass = false;
						validMove = true;
					}
					
				}
				if (!validMove) {
					cout << endl << "That was not a move option";
				}
			
			}
			else if (userInput.substr(0, 4) == "undo") {
				int moveCount = board->GetMoveHistory().capacity();
				if ((stoi(userInput.substr(5, userInput.capacity())) <= moveCount)) {
					moveCount = (stoi(userInput.substr(5, userInput.capacity())));
				}
				for (int i = 0; i < moveCount; i++) {
					if (!(board->GetMoveHistory().empty())) {
						board->UndoLastMove();
					}
				}
			}
			else if (userInput.substr(0, 4) == "show") {
				switch (toupper(userInput[4])) {
				case 'V':
					cout << "The current board value is " << board->GetValue() << endl;
					break;
				case 'H':
					auto lastItr = board->GetMoveHistory().rbegin();
					int startPlayer = board->GetCurrentPlayer();
					for (lastItr; lastItr != board->GetMoveHistory().rend(); lastItr++) {
						startPlayer *= -1;
						cout << endl << v->GetPlayerString(startPlayer) << ": " << string(**lastItr);
					}
					break;
				}
			}
			else if (userInput.substr(0, 4) == "quit") {
				continuePlaying = false;
			}
			else {
				cout << endl << "That was not a valid input, please input a proper input";
			}
			cout << endl;
			if (board->IsFinished()) {
				if (board->GetValue() != 0) {
					string winner;
					(board->GetValue() < 0) ? (winner = v->GetPlayerString(-1)) : (winner = v->GetPlayerString(1));
					cout << winner << " wins!"<< endl;
				}
				else {
					cout << "It's a draw!"<< endl;
				}
				continuePlaying = false;
			}
		}
	}


}