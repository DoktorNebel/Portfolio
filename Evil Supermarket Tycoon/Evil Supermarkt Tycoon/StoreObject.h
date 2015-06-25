#pragma once
#include <SFML/System/Vector2.hpp>
#include <list>
#include "Entity.h"
#include "Product.h"
#include "Constants.h"
#include "ProductListItem.h"
//#include "Worker.h"

class Worker;

class StoreObject :
	public Entity
{
public:

	int ProdsPerLayer;
	enum Type
	{
		Floor,
		Wall,
		Door,
		Shelf,
		Fridge,
		Freezer,
		Checkout,
		MeatCounter,
		Palette,
		Fruitshelf
	} ObjectType;

	enum Quality
	{
		Nothing,
		Illegal,
		Cheap,
		Normal,
		Premium
	} ObjectQuality;

	int PreviousMinute;
	int DirtTimer;
	unsigned short Dirtiness;
	short Width;
	short Height;
	std::vector<sf::Vector2i> AccessPoints;
	std::vector<sf::Vector2i> WorkPoints;
	std::vector<std::vector<std::vector<sf::Vector2i>>> ProductPositions;
	std::vector<ProductListItem*> Slots;
	int ProdsPerSlot;
	std::vector<std::vector<bool>> ShownProducts;
	Worker* CurrentWorker;
	int CustomerCount;
	std::vector<double> Prices;
	std::vector<Date> Dates;
	std::vector<sf::Sprite*> DirtSprites;
	std::list<FeedMessage>* Feed;

public:

	StoreObject();
	StoreObject(StoreObject::Type type, StoreObject::Quality quality, short width, short height, std::vector<sf::Vector2i> accessPoints, std::vector<sf::Vector2i> workPoints, unsigned short slots, std::vector<std::vector<std::vector<sf::Vector2i>>> pPositions, int prodsPerLayer, int prodsPerSlot);
	~StoreObject(void);
	
	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void setDirtTimer();
};

