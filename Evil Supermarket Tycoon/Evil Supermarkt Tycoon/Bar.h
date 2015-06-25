#pragma once

#include "interfacebutton.h"
#include "TextureHandler.h"

class Bar :
	public InterfaceButton
{
private:

	float Value;
	sf::Sprite* FillSprite;

public:

	Bar(std::string name, std::wstring text, sf::Sprite* sprite, sf::Sprite* fillSprite, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset);
	~Bar(void);

	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void SetValue(float value);
	float GetValue();
};

