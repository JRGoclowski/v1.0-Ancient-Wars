#pragma once

#include "GameMove.h"
#include <functional>
#include <algorithm>
#include <vector>
#include <functional>
#include <memory>

using namespace std;

template<typename TData, typename TPred>
vector <TData> CopyIf(const vector<TData>& values, TPred predicate) {
	vector<TData> result;
	for (TData& i : values) {
		if (predicate(i)) {
			result.push_back(std::move(i));
		}
	}
	return result;
}
