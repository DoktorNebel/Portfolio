#pragma once

#include "interfacebutton.h"
#include "TextureHandler.h"
#include "Date.h"
#include <sstream>
#include <iomanip>

class TextButton :
	public InterfaceButton
{
private:

	sf::Text text;
	std::string PrevValue;

public:

	std::string Value;
	enum Type
	{
		Price,
		ExpDate
	} ButtonType;

public:

	TextButton(void);
	TextButton(std::string name, TextButton::Type type, sf::Sprite* sprite, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset);
	~TextButton(void);

	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void setValue(std::string value);
	void setValue(double value);
	void setValue(Date value);
};

