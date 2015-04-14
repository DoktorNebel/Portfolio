#pragma once

#include <list>
#include "SFML/Graphics.hpp"
#include "Input.h"
#include "MenuMessage.h"
#include "MenuButton.h"

class SubMenu : public sf::Drawable
{
private:

	sf::Sprite* Background;

public:

	std::string Name;
	std::list<MenuButton*> Buttons;

public:

	SubMenu(std::string name, sf::Sprite* background, sf::Vector2f screenSize);
	~SubMenu(void);

	void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void Resize(sf::Vector2f screenSize);
};

