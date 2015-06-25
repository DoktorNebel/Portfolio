#pragma once

#include <sstream>
#include <iomanip>
#include "interfacewindow.h"

class DataWindow :
	public InterfaceWindow
{
private:

	sf::Text TimeAndDateText;
	sf::Text MoneyText;
	sf::Text CustomersText;
	sf::Text PrestigeText;

public:

	short State;

public:

	DataWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler);
	~DataWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;

	virtual std::string getSelected();
};

