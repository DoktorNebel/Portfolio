#pragma once
#include <SFML/Graphics.hpp>
#include <string>
#include "GameData.h"
#include "Date.h"
#include "ProductListItem.h"
#include "ProductQuality.h"
#include "TextureHandler.h"
#include "FeedMessage.h"

class Product : public sf::Drawable
{
public:

	int ExistenceDays;
	int DaysToExpire;
	int LastDay;
	sf::Sprite* Sprite;
	std::string Name;
	ProductQuality Quality;
	double Price;
	unsigned short Amount;
	Date ExpirationDate;
	enum State
	{
		Good,
		Expired,
		Rotten
	} ExpirationState;
	unsigned short AgeRestriction;
	std::wstring Description;

public:

	Product();
	Product(ProductListItem* description, unsigned short amount, GameData* gameData, TextureHandler* texHandler);
	~Product(void);

	void update(GameData* gameData, std::list<FeedMessage>* feed, Entity* entity);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void refresh(GameData* gameData);
};

