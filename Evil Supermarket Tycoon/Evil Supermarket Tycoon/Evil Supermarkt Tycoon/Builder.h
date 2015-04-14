#pragma once

#include <sstream>
#include <list>
#include <deque>
#include <SFML/Graphics.hpp>
#include "GameData.h"
#include "TextureHandler.h"
#include "StoreObject.h"
#include "Input.h"
#include "Constants.h"
#include "Interface.h"

class Builder : public sf::Drawable
{
private:

	sf::Sprite* WhiteTileSprite;
	StoreObject* SelectedObject;
	StoreObject* ExtraObject;
	bool ValidPos;
	int Rotation;
	sf::Vector2f Origins[4];
	sf::Vector2i StartPos;
	sf::Vector2i CurrentPos;

	enum Mode
	{
		None,
		Remove,
		SingleObject,
		Wall,
		Floor
	} BuildMode;

	struct ObjectPrice
	{
		StoreObject::Type Type;
		StoreObject::Quality Quality;
		double Price;
	};

	std::vector<ObjectPrice> Prices;
	double CurrentPrice;

	sf::Text PriceText;

public:

	std::list<FeedMessage>* Feed;

public:
	
	Builder();
	Builder(TextureHandler* texHandler);
	~Builder(void);

	void switchBuildMode();
	bool isActivated();

	void update(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, Input* input, unsigned int* highestID, std::deque<unsigned int>* freeIDs, TextureHandler* texHandler, Interface* UI, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void AdjustCostGrid(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid);

private:

	void changeCurrentObject(std::string spriteName, TextureHandler* texHandler, bool deleteOld);
	void changeCurrentPrice(StoreObject::Type type, StoreObject::Quality quality);
	void placeCurrentObject(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, unsigned int* highestID, std::deque<unsigned int>* freeIDs, TextureHandler* texHandler, GameData* gameData);
	void adjustWall(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, TextureHandler* texHandler);
	void hideWalls(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, TextureHandler* texHandler);
	void AdjustAccessibility(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, std::list<sf::Vector2i> searched);
};

