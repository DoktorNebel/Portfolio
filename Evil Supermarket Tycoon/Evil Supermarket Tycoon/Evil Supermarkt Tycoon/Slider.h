#pragma once

#include "interfacebutton.h"
#include <sstream>

class Slider :
	public InterfaceButton
{
private:

	float Min;
	float Max;
	sf::Sprite* SliderSprite;

public:

	float Value;
	enum Type
	{
		Normal,
		Centered
	} SliderType;

public:

	Slider(std::string name, float min, float max, Slider::Type type, sf::Sprite* sprite, sf::Sprite* sliderSprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset);
	~Slider(void);
	
	virtual void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

