#include "MenuButton.h"


MenuButton::MenuButton()
{
	this->Visible = true;
	this->JustPressed = false;
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Pressed = false;
	this->WasPressed = false;
	this->JustPressedMessage = 0;
	this->WasPressedMessage = 0;
	this->PressedSprite = 0;
}


MenuButton::MenuButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize)
{
	this->Visible = true;
	this->JustPressed = false;
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Name = name;
	this->Pressed = false;
	this->Sprite = sprite;
	this->Sprite->setPosition(position.x * screenSize.x, position.y * screenSize.y);
	this->Sprite->setOrigin(this->Sprite->getGlobalBounds().width / 2, this->Sprite->getGlobalBounds().height / 2);
	this->NormalizedPos = position;
	this->WasPressed = false;
	this->PressedSprite = 0;

	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setString(text);
	this->Text.setPosition((int)this->Sprite->getPosition().x, (int)this->Sprite->getPosition().y);
	this->Text.setOrigin((int)this->Text.getGlobalBounds().width / 2, (int)this->Text.getGlobalBounds().height / 2);


	while (this->Sprite->getGlobalBounds().width < this->Text.getGlobalBounds().width)
	{
		this->Sprite->scale(1.1f, 1.0f);
	}

	this->JustPressedMessage = 0;
	this->WasPressedMessage = 0;
}


MenuButton::~MenuButton(void)
{

}


void MenuButton::update(Input* input, std::list<MenuMessage>* messages)
{
	sf::Vector2i MousePos = input->GetMousePos();

	if (this->Sprite->getGlobalBounds().contains(MousePos.x, MousePos.y) || this->Text.getGlobalBounds().contains(MousePos.x, MousePos.y))
	{
		this->MouseOver = true;
		this->Sprite->setColor(sf::Color(230, 230, 255, 255));
		this->Text.setStyle(sf::Text::Style::Bold);
	}
	else
	{
		this->MouseOver = false;
		this->Sprite->setColor(sf::Color(255, 255, 255, 255));
		this->Text.setStyle(sf::Text::Style::Regular);
	}

	if (this->MouseOver && input->JustClicked(sf::Mouse::Left))
	{
		this->JustPressed = true;
		this->Pressed = true;
		if (this->JustPressedMessage)
		{
			messages->push_back(MenuMessage(*this->JustPressedMessage));
		}
		this->Sprite->setColor(sf::Color(210, 210, 255, 255));
	}
	else
	{
		this->JustPressed = false;
	}

	if (this->Pressed && input->WasClicked(sf::Mouse::Left))
	{
		this->WasPressed = true;
		this->Pressed = false;
		if (this->WasPressedMessage)
		{
			messages->push_back(MenuMessage(*this->WasPressedMessage));
		}
		this->Sprite->setColor(sf::Color(255, 255, 255, 255));
	}
	else
	{
		this->WasPressed = false;
	}
}


void MenuButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->PressedSprite && this->MouseOver)
	{
		target.draw(*this->PressedSprite);
	}
	else
	{
		target.draw(*this->Sprite);
	}
	target.draw(this->Text);
}


void MenuButton::AdjustPos(sf::FloatRect screen)
{
	this->Sprite->setPosition(screen.left + screen.width * this->NormalizedPos.x, screen.top + screen.height * this->NormalizedPos.y);
	if (this->PressedSprite)
	{
		this->PressedSprite->setPosition(screen.left + screen.width * this->NormalizedPos.x, screen.top + screen.height * this->NormalizedPos.y);
	}
	this->Text.setPosition((int)this->Sprite->getPosition().x, (int)this->Sprite->getPosition().y);
}