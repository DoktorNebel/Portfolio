#include "TabButton.h"


TabButton::TabButton(void)
{
}


TabButton::TabButton(std::string name, sf::Sprite* sprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset, std::list<InterfaceButton*> buttons)
{
	this->Name = name;
	this->Sprite = sprite;
	this->WindowPosition = windowPosition;
	this->Offset = windowOffset;
	this->Buttons = buttons;
	this->Sprite->setPosition(this->WindowPosition + this->Offset);
	this->JustPressed = false;
	this->Pressed = false;
	this->CurrentButton = 0;
	this->Visible = true;
	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setPosition(this->Sprite->getPosition() + sf::Vector2f(2, 2));
}


TabButton::~TabButton(void)
{
}


void TabButton::update(Input* input)
{
	sf::Vector2i mousePos = input->GetMousePos();
	bool clicked = false;
	if (input->JustClicked(sf::Mouse::Left))
	{
		clicked = true;
	}

	if (this->Sprite->getGlobalBounds().contains((float)mousePos.x, (float)mousePos.y))
	{
		this->MouseOver = true;
		this->Text.setStyle(sf::Text::Style::Bold);
	}
	else
	{
		this->MouseOver = false;
		this->Text.setStyle(sf::Text::Style::Regular);
		if (input->JustClicked(sf::Mouse::Left))
		{
			clicked = true;
		}
	}

	if (this->MouseOver && input->IsClicked(sf::Mouse::Left))
	{
		this->Pressed = true;
	}

	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator enditer = this->Buttons.end();

	if (this->MouseOver && input->JustClicked(sf::Mouse::Left))
	{
		this->JustPressed = true;
	}
	else
	{
		this->JustPressed = false;
	}


	bool somethingPressed = false;

	if (this->Pressed)
	{
		iter = this->Buttons.begin();
		enditer = this->Buttons.end();

		while (iter != enditer)
		{
			if ((*iter)->Visible)
			{
				(*iter)->update(input);
			}

			if ((*iter)->JustPressed)
			{
				somethingPressed = true;
				this->Pressed = true;
			}

			if ((*iter)->Pressed && (*iter) != this->CurrentButton)
			{
				if (this->CurrentButton != 0)
				{
					this->CurrentButton->JustPressed = false;
					this->CurrentButton->Pressed = false;
				}
				this->CurrentButton = (*iter);
			}

			iter++;
		}
	}
	else
	{
		iter = this->Buttons.begin();

		while (iter != enditer)
		{
			(*iter)->JustPressed = false;
			(*iter)->Pressed = false;

			iter++;
		}
	}
}


void TabButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Visible)
	{
		if (this->MouseOver)
		{
			this->Sprite->setColor(sf::Color(230, 230, 255, 255));
		}
		else
		{
			this->Sprite->setColor(sf::Color::White);
		}

		if (this->Pressed)
		{
			this->Sprite->setColor(sf::Color(210, 210, 255, 255));
		}

		target.draw(*this->Sprite);
		target.draw(this->Text);


		if (this->Pressed)
		{
			std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
			std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

			while (iter != end)
			{
				target.draw(**iter);

				iter++;
			}
		}
	}
}


std::string TabButton::getSelected()
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Pressed)
		{
			return (*iter)->getSelected();
		}

		iter++;
	}

	return "NOOOOOOO";
}


std::string TabButton::getHover()
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if (dynamic_cast<TabButton*>(*iter) && dynamic_cast<TabButton*>(*iter)->Pressed)
		{
			return (*iter)->getHover();
		}
		else if ((*iter)->MouseOver)
		{
			return (*iter)->getSelected();
		}

		iter++;
	}

	return "NOOOOOOO";
}


std::list<InterfaceButton*> TabButton::getButtons()
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if (dynamic_cast<TabButton*>(*iter) && dynamic_cast<TabButton*>(*iter)->Pressed)
		{
			return dynamic_cast<TabButton*>(*iter)->getButtons();
		}

		iter++;
	}

	return this->Buttons;
}