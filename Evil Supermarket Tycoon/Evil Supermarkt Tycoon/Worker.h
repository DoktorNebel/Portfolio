#pragma once

#include "Person.h"
#include "StoreJob.h"
#include "StoreObject.h"
#include "Animal.h"
#include "Storage.h"
#include <sstream>

class Worker :
	public Person
{
public:

	double Wage;
	double WageExpectancy;
	bool WorkDays[7];
	unsigned short WorkHours;
	enum Type
	{
		Cashier,
		Cleaner,
		Security,
		Storager
	} WorkerType;
	float CleaningEfficiency;
	float CashieringEfficiency;
	float RefillingEfficiency;
	std::list<StoreJob>* Jobs;
	StoreObject* Object;
	Animal* animal;
	ProductListItem* ProductItem;
	Storage* TheStorage;
	enum State
	{
		Nothing,
		Waiting,
		WaitingForPath,
		Walking,
		Cashiering,
		Cleaning,
		Refilling,
		Butchering
	} WorkState;
	bool AtWorkplace;
	StoreJob CurrentJob;
	std::array<sf::Vector2f, 4> CleaningOffsets;

public:

	Worker(void);
	~Worker(void);

	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void calculateWageExpectency();
};

