#pragma once

#include "menubutton.h"

class MenuDropDownButton :
	public MenuButton
{
private:

	sf::Sprite* ListSprite;
	std::vector<std::string> Elements;
	sf::Text* text;
	sf::Vector2f MousePos;

public:

	std::string Selected;

public:

	MenuDropDownButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Sprite* listSprite, sf::Vector2f position, std::vector<std::string> elements, TextureHandler* texHandler, sf::Vector2f screenSize);
	~MenuDropDownButton(void);

	virtual void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual void AdjustPos(sf::FloatRect screen);
};

