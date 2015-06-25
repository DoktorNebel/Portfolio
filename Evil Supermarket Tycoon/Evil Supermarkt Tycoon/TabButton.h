#pragma once

#include <list>
#include "interfacebutton.h"

class TabButton :
	public InterfaceButton
{
public:

	std::list<InterfaceButton*> Buttons;
	InterfaceButton* CurrentButton;

public:

	TabButton();
	TabButton(std::string name, sf::Sprite* sprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset, std::list<InterfaceButton*> buttons);
	~TabButton(void);

	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual std::string getSelected();
	virtual std::string getHover();
	std::list<InterfaceButton*> getButtons();
};

