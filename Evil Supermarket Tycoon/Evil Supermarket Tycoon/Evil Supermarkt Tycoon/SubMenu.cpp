#include "SubMenu.h"


SubMenu::SubMenu(std::string name, sf::Sprite* background, sf::Vector2f screenSize)
{
	this->Name = name;
	this->Background = background;
	this->Resize(screenSize);
}


SubMenu::~SubMenu(void)
{
	//delete this->Background;
}


void SubMenu::update(Input* input, std::list<MenuMessage>* messages)
{
	std::list<MenuButton*>::iterator iter = this->Buttons.begin();
	std::list<MenuButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->update(input, messages);
		if ((*iter)->Pressed)
		{
			break;
		}

		iter++;
	}
}


void SubMenu::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Background);

	std::list<MenuButton*>::const_iterator iter = this->Buttons.begin();
	std::list<MenuButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			target.draw(**iter);
		}

		iter++;
	}
}


void SubMenu::Resize(sf::Vector2f screenSize)
{
	while (this->Background->getGlobalBounds().width < screenSize.x && this->Background->getGlobalBounds().height < screenSize.y)
	{
		this->Background->scale(1.1f, 1.1f);
	}

	while (this->Background->getGlobalBounds().width > screenSize.x && this->Background->getGlobalBounds().height > screenSize.y)
	{
		this->Background->scale(0.9f, 0.9f);
	}

	float difX = screenSize.x - this->Background->getGlobalBounds().width;
	float difY = screenSize.y - this->Background->getGlobalBounds().height;
	this->Background->setPosition(difX / 2, difY / 2);

	std::list<MenuButton*>::iterator iter = this->Buttons.begin();
	std::list<MenuButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->AdjustPos(this->Background->getGlobalBounds());

		iter++;
	}
}