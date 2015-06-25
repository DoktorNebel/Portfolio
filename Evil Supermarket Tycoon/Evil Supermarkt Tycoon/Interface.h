#pragma once

#include <SFML\Graphics.hpp>
#include <list>
#include "InterfaceWindow.h"
#include "BuildingWindow.h"
#include "DataWindow.h"
#include "ProductsWindow.h"
#include "EntityWindow.h"
#include "WorkersWindow.h"
#include "HiringWindow.h"
#include "FeedWindow.h"
#include "IllegalWindow.h"
#include "PenaltyWindow.h"
#include "StoreObject.h"
#include "InterfaceMesssage.h"
#include "StoreJob.h"

class Interface : public sf::Drawable
{
private:

	Entity* SelectedEntity;
	ProductListItem* SelectedProduct;
	sf::Sprite* SelectedSprite;
	TextureHandler* TexHandler;
	bool BuildMode;
	int PrevDay;
	bool ProdsOpen;
	bool StorOpen;

public:

	std::list<InterfaceWindow*> Windows;
	std::list<InterfaceMessage> Messages;
	std::list<StoreJob>* Jobs;
	Storage* ProductStorage;
	short State;

public:
	
	Interface();
	Interface(sf::RenderWindow* window, TextureHandler* texHandler, std::vector<ProductListItem*>* productList, Storage* storage, std::list<StoreJob>* jobs, PeopleGenerator* peepGenerator, std::vector<Worker*>* workers, std::list<FeedMessage>* feed, std::vector<IllegalActivity>* illegalStuff);
	~Interface(void);
	
	void update(Input* input, GameData* gameData, std::vector<std::vector<std::list<Entity*>>>* grid, std::deque<unsigned int>* freeIDs, unsigned int* highestID);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	std::string getSelectedStoreObject();
	bool containsMouse(Input* input);
};

