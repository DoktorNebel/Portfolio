#include "MenuTextButton.h"


MenuTextButton::MenuTextButton(std::string name, sf::String text, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize)
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

	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152));
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setString(text);
	this->Text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y));
	this->Text.setOrigin(round(this->Text.getGlobalBounds().width / 2.0f), round(this->Text.getGlobalBounds().height / 2.0f));
	
	this->text.setCharacterSize(14);
	this->text.setColor(sf::Color(128, 242, 152));
	this->text.setFont(*texHandler->GetFont());
	this->text.setString("");
	this->text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y));
	this->text.setOrigin(round(this->text.getGlobalBounds().width / 2.0f), round(this->text.getGlobalBounds().height / 2.0f));

	while (this->Text.getGlobalBounds().left + this->Text.getGlobalBounds().width > this->Sprite->getGlobalBounds().left - 5)
	{
		this->Text.move(-1, 0);
	}

	this->JustPressedMessage = 0;
	this->WasPressedMessage = 0;
}


MenuTextButton::~MenuTextButton(void)
{
}


void MenuTextButton::update(Input* input, std::list<MenuMessage>* messages)
{
	sf::Vector2f mousePos = sf::Vector2f((float)input->GetMousePos().x, (float)input->GetMousePos().y);

	if (this->Pressed)
	{
		if ((input->JustClicked(sf::Mouse::Left) && !this->Sprite->getGlobalBounds().contains(mousePos)) || sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Return))
		{
			this->Pressed = false;
			this->WasPressed = true;

			this->Sprite->setColor(sf::Color::White);

			if (this->WasPressedMessage)
			{
				this->WasPressedMessage->Text = this->Value;
				messages->push_back(MenuMessage(*this->WasPressedMessage));
			}
		}

		if (input->GetText() == "delete" && this->Value.size() > 0)
		{
			this->Value.pop_back();
			this->text.setString(this->Value);
			this->text.setOrigin(round(this->text.getGlobalBounds().width / 2.0f), round(this->text.getGlobalBounds().height / 2.0f));
			this->text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y - 5.0f));
		}
		else
		{
			this->Value += input->GetText();
			this->text.setString(this->Value);
			this->text.setOrigin(round(this->text.getGlobalBounds().width / 2.0f), round(this->text.getGlobalBounds().height / 2.0f));
			this->text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y - 5.0f));
		}
	}
	else
	{
		if (this->WasPressed)
		{
			this->WasPressed = false;
		}

		if (input->JustClicked(sf::Mouse::Left) && this->Sprite->getGlobalBounds().contains(mousePos))
		{
			this->Pressed = true;
			this->Sprite->setColor(sf::Color(192, 192, 192, 255));
			
			if (this->JustPressedMessage)
			{
				this->JustPressedMessage->Text = this->Value;
				messages->push_back(MenuMessage(*this->JustPressedMessage));
			}
		}
	}
}


void MenuTextButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	target.draw(this->Text);
	target.draw(this->text);
}


void MenuTextButton::AdjustPos(sf::FloatRect screen)
{
	this->Sprite->setPosition(screen.left + screen.width * this->NormalizedPos.x, screen.top + screen.height * this->NormalizedPos.y);
	this->Text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y));

	this->text.setPosition(round(this->Sprite->getPosition().x), round(this->Sprite->getPosition().y));

	while (this->Text.getGlobalBounds().left + this->Text.getGlobalBounds().width > this->Sprite->getGlobalBounds().left - 5.0f)
	{
		this->Text.move(-1, 0);
	}
}

