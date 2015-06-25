#pragma once

#include <list>
#include <SFML\Graphics.hpp>
#include "InterfaceButton.h"
#include "TabButton.h"
#include "GameData.h"
#include "TextureHandler.h"
#include "InterfaceMesssage.h"

class InterfaceWindow : public sf::Drawable
{
protected:

	std::list<InterfaceButton*> Buttons;
	bool IsOpen;

public:

	sf::Sprite* Sprite;
	std::string Name;

public:

	InterfaceWindow();
	~InterfaceWindow();

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual std::string getSelected();
	void open();
	void close();
	bool isOpen() const;
};

