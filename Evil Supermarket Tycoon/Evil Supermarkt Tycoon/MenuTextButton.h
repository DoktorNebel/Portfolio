#pragma once

#include "menubutton.h"

class MenuTextButton :
	public MenuButton
{
private:

	sf::Text text;
	std::string Value;

public:

	MenuTextButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize);
	~MenuTextButton(void);

	virtual void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual void AdjustPos(sf::FloatRect screen);
};

