#pragma once

#include <list>
#include "SFML/Graphics.hpp"
#include "TextureHandler.h"
#include "Input.h"
#include "MenuMessage.h"

class MenuButton : public sf::Drawable
{
public:

	std::string Name;
	bool MouseOver;
	bool Pressed;
	bool JustPressed;
	bool WasPressed;
	bool MouseWasOver;
	bool Visible;
	sf::Sprite* Sprite;
	sf::Sprite* PressedSprite;
	sf::Text Text;
	MenuMessage* JustPressedMessage;
	MenuMessage* WasPressedMessage;
	sf::Vector2f NormalizedPos;

public:

	MenuButton();
	MenuButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize);
	~MenuButton(void);

	virtual void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual void AdjustPos(sf::FloatRect screen);
};

