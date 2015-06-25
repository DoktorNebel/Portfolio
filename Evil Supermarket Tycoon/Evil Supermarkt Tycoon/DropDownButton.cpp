#include "DropDownButton.h"


DropDownButton::DropDownButton(std::string name, sf::Sprite* sprite, sf::Sprite* listSprite, std::vector<std::string> elements, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(windowPosition + windowOffset);
	this->ListSprite = listSprite;
	this->Elements = elements;
	this->Selected = this->Elements.front();
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->text = new sf::Text(this->Selected, *texHandler->GetFont(), 14);
	this->text->setPosition(windowPosition + windowOffset);
	this->text->setColor(sf::Color(128, 242, 152, 255));
	this->Pressed = false;
	this->JustPressed = false;
	this->WasPressed = false;
}


DropDownButton::~DropDownButton(void)
{
	delete this->ListSprite;
}


void DropDownButton::update(Input* input)
{
	this->MousePos = sf::Vector2f((float)input->GetMousePos().x, (float)input->GetMousePos().y);

	if (this->Pressed)
	{

		if (input->JustClicked(sf::Mouse::Button::Left))
		{
			for (unsigned int i = 0; i < this->Elements.size(); i++)
			{
				sf::FloatRect rect(this->Sprite->getGlobalBounds().left, this->Sprite->getGlobalBounds().top + this->Sprite->getGlobalBounds().height * (i + 1),
					this->Sprite->getGlobalBounds().width, this->Sprite->getGlobalBounds().height);

				if (rect.contains(this->MousePos))
				{
					this->Selected = this->Elements[i];
				}
			}
			this->Pressed = false;
			this->WasPressed = true;
		}
	}
	else
	{
		if (this->WasPressed)
		{
			this->WasPressed = false;
		}

		if (this->Sprite->getGlobalBounds().contains(this->MousePos) && input->JustClicked(sf::Mouse::Button::Left))
		{
			this->Pressed = true;
		}
	}
}


void DropDownButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	this->text->setPosition(this->Sprite->getPosition() + sf::Vector2f(5, 2));
	this->text->setString(this->Selected);
	target.draw(*this->text);
	target.draw(this->Text);

	if (this->Pressed)
	{
		for (unsigned int i = 0; i < this->Elements.size(); i++)
		{
			this->ListSprite->setPosition(this->Sprite->getGlobalBounds().left, this->Sprite->getGlobalBounds().top + this->Sprite->getGlobalBounds().height + i * this->ListSprite->getGlobalBounds().height);
			if (this->ListSprite->getGlobalBounds().contains(this->MousePos))
			{
				this->ListSprite->setColor(sf::Color(128, 128, 128, 255));
				this->text->setStyle(sf::Text::Style::Bold);
			}
			else
			{
				this->ListSprite->setColor(sf::Color::White);
				this->text->setStyle(sf::Text::Style::Regular);
			}
			target.draw(*this->ListSprite);
			this->text->setString(this->Elements[i]);
			this->text->setPosition(this->ListSprite->getPosition() + sf::Vector2f(5, 2));
			target.draw(*this->text);
		}
	}
}


std::string DropDownButton::getSelectedElement()
{
	return this->Selected;
}