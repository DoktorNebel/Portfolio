#pragma once

#include "interfacewindow.h"
#include "IllegalActivity.h"
#include <sstream>

class IllegalWindow :
	public InterfaceWindow
{
private:

	sf::Sprite* HoverWindowSprite;
	sf::Text HoverWindowText;
	std::vector<IllegalActivity>* IllegalStuff;

public:

	IllegalWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::vector<IllegalActivity>* illegalStuff);
	~IllegalWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

