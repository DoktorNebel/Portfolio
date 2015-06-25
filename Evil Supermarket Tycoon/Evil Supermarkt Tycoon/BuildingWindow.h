#pragma once

#include "interfacewindow.h"

class BuildingWindow :
	public InterfaceWindow
{
private:

	InterfaceButton* CurrentButton;

public:

	BuildingWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler);
	~BuildingWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	virtual std::string getSelected();
	void filterButtons(std::string mode);
};

