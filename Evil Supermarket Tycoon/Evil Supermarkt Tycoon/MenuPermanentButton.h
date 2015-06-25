#pragma once

#include "menubutton.h"

class MenuPermanentButton :
	public MenuButton
{
public:

	MenuPermanentButton();
	MenuPermanentButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize);
	~MenuPermanentButton(void);

	virtual void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual void AdjustPos(sf::FloatRect screen);
};

