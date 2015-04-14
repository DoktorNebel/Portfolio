#pragma once

#include "interfacewindow.h"
#include "StoreObject.h"
#include "Storage.h"
#include "Customer.h"
#include "Worker.h"
#include "Animal.h"
#include "StoreJob.h"
#include "Bar.h"
#include "DropDownButton.h"
#include "TextButton.h"
#include <sstream>
#include <iomanip>

class EntityWindow :
	public InterfaceWindow
{
private:

	TextureHandler* TexHandler;
	std::vector<sf::Sprite*> ProductSprites;
	std::vector<sf::RectangleShape> Rectangles;
	std::vector<TextButton> OtherButtons;
	std::vector<sf::Text> Texts;
	sf::Text InfoText;
	std::list<StoreJob>* Jobs;

public:

	Entity* SelectedEntity;
	bool EntityChanged;

public:

	EntityWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::list<StoreJob>* jobs);
	~EntityWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void PutObject(ProductListItem* object, sf::Sprite* sprite, Input* input, GameData* gameData, Storage* storage, std::list<StoreJob>* jobs);
};