#pragma once

#include <list>
#include <vector>
#include <SFML/System/Time.hpp>
#include "Date.h"
#include "TimeOfDay.h"
#include "IllegalActivity.h"
#include "SoundManager.h"

class GameData
{
public:

	Date Date;
	TimeOfDay Time;
	unsigned short WeekDay;
	unsigned short CurrentCustomers;
	double CurrentMoney;
	float Hygiene;
	std::list<unsigned short> AverageCustomers;
	std::list<int> AverageSales;
	std::vector<IllegalActivity>* IllegalStuff;
	int Prestige;
	SoundManager* SoundHandler;

public:

	GameData(void);
	~GameData(void);

	void update(sf::Time elapsedTime, unsigned short gameSpeed);
};

