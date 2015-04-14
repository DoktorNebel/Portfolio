#pragma once

#include "interfacewindow.h"

class PenaltyWindow :
	public InterfaceWindow
{
public:

	sf::Text Message;

public:

	PenaltyWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler);
	~PenaltyWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

