#pragma once
#include <string>

class AWGameMove {
public:

   virtual operator std::string() const = 0;

   virtual bool operator==(const AWGameMove &rhs) const = 0;
};
