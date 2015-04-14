#pragma once

#include "interfacewindow.h"
#include "Worker.h"
#include "Bar.h"
#include <sstream>

class WorkersWindow :
	public InterfaceWindow
{
private:

	TextureHandler* texHandler;
	std::vector<Worker*>* Workers;

public:

	Worker* SelectedWorker;

public:

	WorkersWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::vector<Worker*>* workers);
	~WorkersWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states);
	void AddWorker(Worker* worker);
};

