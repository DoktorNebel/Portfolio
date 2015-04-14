#include "GameData.h"


GameData::GameData(void)
{
	this->WeekDay = 0;
	this->Hygiene = 1.0f;
	this->Prestige = 0;
}


GameData::~GameData(void)
{
}


void GameData::update(sf::Time elapsedTime, unsigned short gameSpeed)
{
	this->Time.Minute += (elapsedTime.asSeconds() / 2) * gameSpeed;

	if (this->Time.Minute >= 60)
	{
		this->Time.Minute = 0;
		this->Time.Hour++;
	}

	if (this->Time.Hour == 24)
	{
		this->Time.Hour = 0;
		this->Date.Day++;
		this->WeekDay++;
		if (this->WeekDay == 7)
		{
			this->WeekDay = 0;
		}
	}

	if (this->Date.Month == 2)
	{
		if (this->Date.Day == 29)
		{
			this->Date.Day = 1;
			this->Date.Month++;
		}
	}
	else if ((this->Date.Month % 2 == 0 && this->Date.Month < 7) || (this->Date.Month % 2 != 0 && this->Date.Month > 7))
	{
		if (this->Date.Day == 31)
		{
			this->Date.Day = 1;
			this->Date.Month++;
		}
	}
	else
	{
		if (this->Date.Day == 32)
		{
			this->Date.Day = 1;
			this->Date.Month++;
		}
	}


	if (this->Date.Month == 13)
	{
		this->Date.Month = 1;
		this->Date.Year++;
	}
}