#pragma once

#include "interfacewindow.h"
#include "PeopleGenerator.h"
#include "Bar.h"
#include <sstream>

class HiringWindow :
	public InterfaceWindow
{
private:

	TextureHandler* texHandler;
	std::vector<Worker*> Workers;
	PeopleGenerator* PeepGenerator;
	InterfaceButton* CurrentButton;

public:

	HiringWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, PeopleGenerator* peepGenerator);
	~HiringWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states);
	void GenerateWorkers();
};

