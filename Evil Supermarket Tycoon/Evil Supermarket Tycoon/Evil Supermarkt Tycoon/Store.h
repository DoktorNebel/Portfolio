#pragma once
#include <SFML/Graphics.hpp>
#include <list>
#include <vector>
#include <deque>
#include "Entity.h"
#include "StoreObject.h"
#include "Constants.h"
#include "Storage.h"
#include "PeopleGenerator.h"
#include "StoreMessage.h"
#include "StoreJob.h"

class Store :
	public sf::Drawable
{
public:

	std::list<int> KnownCustomers;
	std::vector<std::vector<std::list<Entity*>>> Grid;
	std::vector<std::vector<unsigned char>> CostGrid;
	std::vector<Entity*> DrawList;
	unsigned int HighestID;
	std::deque<unsigned int> FreeIDs;
	unsigned short GridSizeX;
	unsigned short GridSizeY;
	int DailyCosts;
	bool OpeningDays[7];
	unsigned short OpeningHour[7];
	unsigned short ClosingHour[7];
	Storage ProductStorage;
	PeopleGenerator PeepGenerator;
	float SpawnTimer;
	float SpawnRate;
	bool Spawn;
	std::vector<Worker*> Workers;
	std::list<StoreMessage> Messages;
	std::list<StoreJob> Jobs;
	sf::Vector2i ExitPosition;
	sf::IntRect ViewPort;
	sf::Texture ConcreteTexture;
	sf::Sprite ConcreteSprite;
	TextureHandler* TexHandler;
	sf::Sprite* Tile;
	bool DrugsDelivered;
	int DrugHour;
	std::string Drug;

public:

	Store(unsigned short gridSizeX, unsigned short gridSizeY, std::vector<ProductListItem*>* productList, Pathfinder* pathfinder, TextureHandler* texHandler);
	~Store(void);
	
	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void UpdateBackground();
};

