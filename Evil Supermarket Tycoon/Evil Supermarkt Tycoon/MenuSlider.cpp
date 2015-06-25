#include "MenuSlider.h"


MenuSlider::MenuSlider(std::string name, sf::String text, float min, float max, sf::Sprite* sprite, sf::Sprite* sliderSprite, sf::Vector2f position, TextureHandler* texHandler, sf::Vector2f screenSize)
{
	this->Visible = true;
	this->JustPressed = false;
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Pressed = false;
	this->WasPressed = false;
	this->JustPressedMessage = 0;
	this->WasPressedMessage = 0;

	this->Sprite = sprite;
	this->Sprite->setPosition(position.x * screenSize.x, position.y * screenSize.y);
	this->Sprite->setOrigin(this->Sprite->getGlobalBounds().width / 2, this->Sprite->getGlobalBounds().height / 2);
	this->SliderSprite = sliderSprite;
	this->SliderSprite->setPosition(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width, this->Sprite->getPosition().y);
	this->SliderSprite->setOrigin(this->SliderSprite->getGlobalBounds().width / 2, this->SliderSprite->getGlobalBounds().height / 2);

	this->NormalizedPos = position;
	this->Min = min;
	this->Max = max;
	this->Value = 0;

	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setString(text);
	this->Text.setPosition(round(this->Sprite->getPosition().x - this->Sprite->getGlobalBounds().width / 2.0f - this->Text.getGlobalBounds().width / 2.0f - 5.0f), round(this->Sprite->getPosition().y - 5.0f));
	this->Text.setOrigin(round(this->Text.getGlobalBounds().width / 2.0f), round(this->Text.getGlobalBounds().height / 2.0f));

	this->text.setCharacterSize(14);
	this->text.setColor(sf::Color(128, 242, 152, 255));
	this->text.setFont(*texHandler->GetFont());
	this->text.setString("100");
	this->text.setPosition(round(this->Sprite->getPosition().x + this->Text.getGlobalBounds().width + 5.0f), round(this->Sprite->getPosition().y));
	this->text.setOrigin(round(this->Text.getGlobalBounds().width / 2.0f), round(this->Text.getGlobalBounds().height / 2.0f));
}


MenuSlider::~MenuSlider(void)
{
}


void MenuSlider::update(Input* input, std::list<MenuMessage>* messages)
{
	sf::Vector2i MousePos = input->GetMousePos();

	if (this->Sprite->getGlobalBounds().contains((float)MousePos.x, (float)MousePos.y))
	{
		this->MouseOver = true;
		if (input->JustClicked(sf::Mouse::Left))
		{
			this->MouseWasOver = true;
		}
	}
	else
	{
		this->MouseOver = false;
	}

	if (this->MouseWasOver && input->IsClicked(sf::Mouse::Left))
	{
		float xPos = (float)input->GetMousePos().x;
		if (xPos < this->Sprite->getGlobalBounds().left)
		{
			xPos = this->Sprite->getGlobalBounds().left;
		}
		if (xPos > this->Sprite->getGlobalBounds().left + this->Sprite->getGlobalBounds().width)
		{
			xPos = this->Sprite->getGlobalBounds().left + this->Sprite->getGlobalBounds().width;
		}
		this->SliderSprite->setPosition(xPos, this->Sprite->getPosition().y);
		float dif = this->Sprite->getPosition().x - this->Sprite->getGlobalBounds().width / 2 - this->SliderSprite->getPosition().x;
		float perc = abs(dif) / this->Sprite->getGlobalBounds().width;
		this->Value = this->Min + this->Max * perc;
		

		std::ostringstream ss;
		ss << (int)this->Value;
		std::string s(ss.str());

		this->text.setString(s);
	}


	if (input->WasClicked(sf::Mouse::Left))
	{
		if (this->MouseWasOver)
		{
			this->MouseWasOver = false;
			if (this->WasPressedMessage)
			{
				this->WasPressedMessage->Value = this->Value;
				messages->push_back(MenuMessage(*this->WasPressedMessage));
			}
			this->WasPressed = true;
		}
	}
	else
	{
		this->WasPressed = false;
	}
}


void MenuSlider::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	target.draw(*this->SliderSprite);
	target.draw(this->Text);
	target.draw(this->text);
}


void MenuSlider::AdjustPos(sf::FloatRect screen)
{
	this->Sprite->setPosition(screen.left + screen.width * this->NormalizedPos.x, screen.top + screen.height * this->NormalizedPos.y);
	this->SliderSprite->setPosition(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width / 2 - this->SliderSprite->getGlobalBounds().width / 2, this->Sprite->getPosition().y);
	this->Text.setPosition(round(this->Sprite->getPosition().x - this->Sprite->getGlobalBounds().width / 2.0f - this->Text.getGlobalBounds().width / 2.0f - 10.0f), round(this->Sprite->getPosition().y - 5.0f));
	this->text.setPosition(round(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width / 2.0f + this->Text.getGlobalBounds().width / 2.0f + 10.0f), round(this->Sprite->getPosition().y - 5.0f));
}