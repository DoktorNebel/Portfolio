#include "Product.h"


Product::Product()
{

}


Product::Product(ProductListItem* description, unsigned short amount, GameData* gameData, TextureHandler* texHandler)
{
	this->Name = description->Name;
	this->Amount = amount;
	this->Quality = description->Quality;
	this->Price = description->Price;
	this->ExistenceDays = 0;
	this->DaysToExpire = description->MinDaysToExpire;
	this->ExpirationDate = gameData->Date;
	this->Sprite = texHandler->GetSprite(description->Name)[0][0];

	for (int i = 0; i < this->DaysToExpire; i++)
	{
		this->ExpirationDate.Day++;

		if (this->ExpirationDate.Month == 2)
		{
			if (this->ExpirationDate.Day == 29)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}
		else if ((this->ExpirationDate.Month % 2 == 0 && this->ExpirationDate.Month < 7) || (this->ExpirationDate.Month % 2 != 0 && this->ExpirationDate.Month > 7))
		{
			if (this->ExpirationDate.Day == 31)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}
		else
		{
			if (this->ExpirationDate.Day == 32)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}


		if (this->ExpirationDate.Month == 13)
		{
			this->ExpirationDate.Month = 1;
			this->ExpirationDate.Year++;
		}
	}

	this->ExpirationState = Product::State::Good;

	this->Description = description->Description;
}


Product::~Product(void)
{
}


void Product::update(GameData* gameData, std::list<FeedMessage>* feed, Entity* entity)
{
	if (gameData->Date.Day != this->LastDay)
	{
		this->ExistenceDays++;
		this->LastDay = gameData->Date.Day;
	}

	if (this->ExistenceDays > this->DaysToExpire)
	{
		if (this->ExpirationState == Product::State::Good)
		{
			FeedMessage tmp;
			tmp.Message = this->Name + "ist abgelaufen.";
			tmp.Pointer = entity;
			
			feed->push_back(tmp);
		}
		this->ExpirationState = Product::State::Expired;

		if (this->ExistenceDays > this->DaysToExpire + 14)
		{
			this->ExpirationState = Product::State::Rotten;
		}
	}
}


void Product::draw(sf::RenderTarget& target, sf::RenderStates states) const
{

}


void Product::refresh(GameData* gameData)
{
	this->ExistenceDays = 0;
	this->ExpirationDate = gameData->Date;

	for (int i = 0; i < this->DaysToExpire; i++)
	{
		this->ExpirationDate.Day++;

		if (this->ExpirationDate.Month == 2)
		{
			if (this->ExpirationDate.Day == 29)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}
		else if ((this->ExpirationDate.Month % 2 == 0 && this->ExpirationDate.Month < 7) || (this->ExpirationDate.Month % 2 != 0 && this->ExpirationDate.Month > 7))
		{
			if (this->ExpirationDate.Day == 31)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}
		else
		{
			if (this->ExpirationDate.Day == 32)
			{
				this->ExpirationDate.Day = 1;
				this->ExpirationDate.Month++;
			}
		}


		if (this->ExpirationDate.Month == 13)
		{
			this->ExpirationDate.Month = 1;
			this->ExpirationDate.Year++;
		}
	}

	this->ExpirationState = Product::State::Good;
}