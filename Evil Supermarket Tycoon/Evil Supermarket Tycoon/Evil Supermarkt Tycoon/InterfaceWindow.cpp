#include "InterfaceWindow.h"


InterfaceWindow::InterfaceWindow(void)
{
}


InterfaceWindow::~InterfaceWindow(void)
{

}


void InterfaceWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->update(input);
		iter++;
	}
}


void InterfaceWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);
		iter++;
	}
}


std::string InterfaceWindow::getSelected()
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Pressed)
		{
			return (*iter)->Name;	
		}
		iter++;
	}

	return "NOOOOOO";
}


void InterfaceWindow::open()
{
	this->IsOpen = true;
}


void InterfaceWindow::close()
{
	this->IsOpen = false;
}


bool InterfaceWindow::isOpen() const
{
	return this->IsOpen;
}