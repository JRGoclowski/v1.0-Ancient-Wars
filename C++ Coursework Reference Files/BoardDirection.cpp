#include "BoardDirection.h"
#include <array>
using namespace std;

BoardDirection::BoardDirection() 
	: mRowChange(0), mColumnChange(0) {
}

BoardDirection::BoardDirection(const char & rowChange, const char & columnChange)
	: mRowChange(rowChange), mColumnChange(columnChange) {
	
}

std::array<BoardDirection, 8> BoardDirection::CARDINAL_DIRECTIONS = { BoardDirection(-1,-1), BoardDirection(-1,0),
	BoardDirection(-1,1), BoardDirection(0,-1), BoardDirection(0,1), BoardDirection(1,-1),
	BoardDirection(1,0), BoardDirection(1,1) };