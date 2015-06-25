#pragma once
#include <list>
#include "Person.h"
#include "Worker.h"
#include "Product.h"
#include "Constants.h"
#include "StoreObject.h"
#include "IllegalActivity.h"

class Customer :
	public Person
{
public:

	int NextProduct;
	bool CheckedOut;
	bool InLine;
	int LinePosition;
	double Money;
	std::vector<ProductListItem*> ShoppingList;
	std::vector<bool> ProductsFound;
	std::vector<double> PriceExpectancy;
	float StealingPotential;
	float HygieneExpectation;
	float AssholeFactor;
	std::wstring Class;
	bool Deleted;
	StoreObject* Object;
	enum State
	{
		Nothing,
		WaitingForPos,
		WaitingForPath,
		Walking,
		TakingProduct,
		StandingInLine,
		Paying
	} PersonState;

	enum ShopState
	{
		Buying,
		CheckingOut,
		Leaving,
		Left,
		Stealing
	} ShoppingState;
	bool Waited;
	unsigned short ExpectedProducts;

public:

	Customer(void);
	~Customer(void);

	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

