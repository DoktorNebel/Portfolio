#pragma once

#include <vector>
#include "interfacebutton.h"
#include "TextureHandler.h"

class DropDownButton :
	public InterfaceButton
{
private:

	sf::Sprite* ListSprite;
	std::vector<std::string> Elements;
	sf::Text* text;
	sf::Vector2f MousePos;

public:

	std::string Selected;
	
public:

	DropDownButton(std::string name, sf::Sprite* sprite, sf::Sprite* listSprite, std::vector<std::string> elements, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset);
	~DropDownButton(void);
	
	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	std::string getSelectedElement();
};

