#include "InterfaceButton.h"


InterfaceButton::InterfaceButton()
{

}


InterfaceButton::InterfaceButton(std::string name, InterfaceButton::Type type, sf::Sprite* sprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset)
{
	this->Name = name;
	this->ButtonType = type;
	this->Sprite = sprite;
	this->WindowPosition = windowPosition;
	this->Offset = windowOffset;
	this->Sprite->setPosition(this->WindowPosition + this->Offset);
	this->JustPressed = false;
	this->Pressed = false;
	this->WasPressed = false;
	this->Visible = true;
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setCharacterSize(14);
	this->Text.setPosition(this->Sprite->getPosition() + sf::Vector2f(2, 2));
	while (this->Sprite->getGlobalBounds().width < this->Text.getGlobalBounds().width)
	{
		this->Sprite->scale(1.1f, 1.0f);
	}
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Disabled = false;
	this->HalfDisabled = false;
	this->PressedSprite = 0;
}


InterfaceButton::~InterfaceButton(void)
{
}


void InterfaceButton::update(Input* input)
{
	if (!this->Disabled && !this->HalfDisabled)
	{
		sf::Vector2i MousePos = input->GetMousePos();

		if (this->Sprite->getGlobalBounds().contains(MousePos.x, MousePos.y) || this->Text.getGlobalBounds().contains(MousePos.x, MousePos.y))
		{
			this->MouseOver = true;
			this->Text.setStyle(sf::Text::Style::Bold);
		}
		else
		{
			this->MouseOver = false;
			this->Text.setStyle(sf::Text::Style::Regular);
		}

		if (this->MouseOver && input->JustClicked(sf::Mouse::Left))
		{
			this->JustPressed = true;
			this->MouseWasOver = true;
			if (this->ButtonType == InterfaceButton::Type::Permanent)
			{
				this->Pressed = !this->Pressed;
			}
		}
		else
		{
			if (input->JustClicked(sf::Mouse::Left))
			{
				this->MouseWasOver = false;
			}
			this->JustPressed = false;
		}

		if (this->Pressed && input->WasClicked(sf::Mouse::Left))
		{
			this->WasPressed = true;
			this->MouseWasOver = false;
		}
		else
		{
			this->WasPressed = false;
		}

		if (this->ButtonType == InterfaceButton::Type::Normal)
		{
			if (input->IsClicked(sf::Mouse::Left) && !this->MouseWasOver)
			{
				this->Pressed = false;
			}
			else if (input->IsClicked(sf::Mouse::Left) && this->MouseOver)
			{
				this->Pressed = true;
			}
			else if (input->WasClicked(sf::Mouse::Left))
			{
				this->Pressed = false;
			}
		}
	}
}


void InterfaceButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Visible)
	{
		if (this->Disabled)
		{
			this->Sprite->setColor(sf::Color(50, 50, 50, 255));
		}
		else if (this->HalfDisabled)
		{
			this->Sprite->setColor(sf::Color(150, 150, 150, 255));
		}
		else
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
		}

		if (this->PressedSprite && ((this->ButtonType == InterfaceButton::Type::Normal && this->MouseOver) || (this->ButtonType == InterfaceButton::Type::Permanent && this->Pressed)))
		{
			this->PressedSprite->setPosition(this->Sprite->getPosition());
			target.draw(*this->PressedSprite);
		}
		else
		{
			target.draw(*this->Sprite);
		}
		target.draw(this->Text);
	}
}


void InterfaceButton::setWindowPosition(sf::Vector2f position)
{
	this->WindowPosition = position;
}


void InterfaceButton::scale(sf::Vector2f factor)
{
	this->Sprite->scale(factor);
	this->Offset = sf::Vector2f(this->Offset.x * factor.x, this->Offset.y * factor.y);
	this->Sprite->setPosition(this->WindowPosition + this->Offset);
}


std::string InterfaceButton::getSelected()
{
	return this->Name;
}


std::string InterfaceButton::getHover()
{
	return this->Name;
}