#pragma once

#include "ProductQuality.h"

struct ProductListItem
{
	std::string Name;
	std::string Category;
	std::string Subcategory;
	std::string Product;
	ProductQuality Quality;
	double Price;
	int MinDaysToExpire;
	int MaxDaysToExpire;
	std::wstring Description;
};