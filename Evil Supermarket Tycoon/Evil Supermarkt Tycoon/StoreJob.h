#pragma once

#include "ProductListItem.h"

class StoreObject;

struct StoreJob
{
	enum Type
	{
		Cashiering,
		Cleaning,
		Refilling,
		Nothing,
		Butchering
	} JobType;

	Entity* Object;
	ProductListItem* ProductItem;
	bool Taken;
};