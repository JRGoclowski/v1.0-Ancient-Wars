#pragma once

#include <functional>
#include <algorithm>
#include <vector>

template<typename TData, typename TPred>
std::vector <TData> CopyIf(const std::vector<TData>& values, TPred predicate);

template<typename TData, typename TPred>
bool Contains(const std::vector<TData>& values, TPred predicate);