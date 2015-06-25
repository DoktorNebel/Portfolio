#include "MenuDropDownButton.h"


MenuDropDownButton::MenuDropDownButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Sprite* listSprite, sf::Vector2f position, std::vector<std::string> elements, TextureHandler* texHandler, sf::Vector2f screenSize)
{
	this->Visible = true;
	this->JustPressed = false;
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Pressed = false;
	this->WasPressed = false;
	this->JustPressedMessage = 0;
	this->WasPressedMessage = 0;

	this->Elements = elements;

	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position.x * screenSize.x, position.y * screenSize.y);
	this->ListSprite = listSprite;
	this->NormalizedPos = position;

	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setString(text);
	this->Text.setPosition(round(this->Sprite->getPosition().x - this->Text.getGlobalBounds().width - 10.0f), round(this->Sprite->getPosition().y + 3.0f));
	
	this->text = new sf::Text();
	this->text->setCharacterSize(14);
	this->text->setColor(sf::Color(128, 242, 152, 255));
	this->text->setFont(*texHandler->GetFont());
	this->text->setString(text);
	this->text->setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y));
}


MenuDropDownButton::~MenuDropDownButton(void)
{
}


void MenuDropDownButton::update(Input* input, std::list<MenuMessage>* messages)
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
			
			if (this->WasPressedMessage)
			{
				this->WasPressedMessage->Text = this->Selected;
				messages->push_back(MenuMessage(*this->WasPressedMessage));
			}
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
			if (this->JustPressedMessage)
			{
				this->JustPressedMessage->Text = this->Selected;
				messages->push_back(MenuMessage(*this->JustPressedMessage));
			}
		}
	}
}


void MenuDropDownButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	this->text->setPosition(round(this->Sprite->getPosition().x + 5.0f), round(this->Sprite->getPosition().y + 2.0f));
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
			this->text->setPosition(round(this->ListSprite->getPosition().x + 5.0f), round(this->ListSprite->getPosition().y + 2.0f));
			target.draw(*this->text);
		}
	}
}


void MenuDropDownButton::AdjustPos(sf::FloatRect screen)
{
	this->Sprite->setPosition(screen.left + screen.width * this->NormalizedPos.x, screen.top + screen.height * this->NormalizedPos.y);
	this->Text.setPosition(round(this->Sprite->getPosition().x - this->Text.getGlobalBounds().width - 10.0f), round(this->Sprite->getPosition().y + 3.0f));
}

