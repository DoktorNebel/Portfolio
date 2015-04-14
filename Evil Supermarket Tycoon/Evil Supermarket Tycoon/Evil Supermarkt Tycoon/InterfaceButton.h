#pragma once

#include <string>
#include <SFML\Graphics.hpp>
#include "Input.h"

class InterfaceButton : public sf::Drawable
{
protected:

	sf::Vector2f WindowPosition;

public:

	sf::Vector2f Offset;
	enum Type
	{
		Normal,
		Permanent,
	} ButtonType;
	std::string Name;
	bool MouseOver;
	bool Pressed;
	bool JustPressed;
	bool WasPressed;
	bool MouseWasOver;
	bool Visible;
	bool Disabled;
	bool HalfDisabled;
	sf::Sprite* Sprite;
	sf::Sprite* PressedSprite;
	sf::Text Text;

public:

	InterfaceButton();
	InterfaceButton(std::string name, InterfaceButton::Type type, sf::Sprite* sprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset);
	~InterfaceButton(void);

	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void setWindowPosition(sf::Vector2f position);
	void scale(sf::Vector2f factor);
	virtual std::string getSelected();
	virtual std::string getHover();
};

