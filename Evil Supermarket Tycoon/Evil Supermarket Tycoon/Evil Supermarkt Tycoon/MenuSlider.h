#pragma once

#include "menubutton.h"
#include <sstream>

class MenuSlider :
	public MenuButton
{
private:

	float Min;
	float Max;
	sf::Sprite* SliderSprite;
	sf::Text text;

public:
	
	float Value;

public:

	MenuSlider(std::string name, sf::String text, float min, float max, sf::Sprite* sprite, sf::Sprite* sliderSprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize);
	~MenuSlider(void);
	
	virtual void update(Input* input, std::list<MenuMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual void AdjustPos(sf::FloatRect screen);
};

