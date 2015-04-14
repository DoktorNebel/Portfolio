#include "Slider.h"


Slider::Slider(std::string name, float min, float max, Slider::Type type, sf::Sprite* sprite, sf::Sprite* sliderSprite, sf::Vector2f windowPosition, sf::Vector2f windowOffset)
{
	this->Name = name;
	this->SliderType = type;
	this->Sprite = sprite;
	this->SliderSprite = sliderSprite;
	this->WindowPosition = windowPosition;
	this->Offset = windowOffset;
	this->Sprite->setPosition(this->WindowPosition + this->Offset);
	this->SliderSprite->setPosition(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width / 2 - this->SliderSprite->getGlobalBounds().width / 2, this->Sprite->getPosition().y);
	this->JustPressed = false;
	this->Pressed = false;
	this->WasPressed = false;
	this->Min = min;
	this->Max = max;
	this->Value = 0;
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setCharacterSize(14);
	this->Text.setString("0");
	this->ButtonType = InterfaceButton::Type::Normal;
	this->MouseOver = false;
	this->MouseWasOver = false;
	this->Visible = true;
}


Slider::~Slider(void)
{

}


void Slider::update(Input* input)
{
	sf::Vector2i MousePos = input->GetMousePos();

	if (this->Sprite->getGlobalBounds().contains(MousePos.x, MousePos.y))
	{
		this->MouseOver = true;
	}
	else
	{
		this->MouseOver = false;
	}

	if (this->MouseOver && input->IsClicked(sf::Mouse::Left))
	{
		if (this->SliderType == Slider::Type::Normal)
		{
			this->SliderSprite->setPosition(input->GetMousePos().x - this->SliderSprite->getGlobalBounds().width / 2, this->Sprite->getPosition().y);
			float dif = this->SliderSprite->getPosition().x + this->SliderSprite->getGlobalBounds().width / 2 - this->Sprite->getPosition().x;
			float perc = dif / this->Sprite->getGlobalBounds().width;
			this->Value = this->Min + this->Max * perc;
		}
		else
		{
			this->SliderSprite->setPosition(input->GetMousePos().x - this->SliderSprite->getGlobalBounds().width / 2, this->Sprite->getPosition().y);
			float dif = this->SliderSprite->getPosition().x + this->SliderSprite->getGlobalBounds().width / 2 - (this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width / 2);
			float perc = dif / this->Sprite->getGlobalBounds().width;
			this->Value += this->Max * perc;
			if (this->Value < 0)
			{
				this->Value = 0;
			}
		}

		std::ostringstream ss;
		ss << (int)this->Value;
		std::string s(ss.str());

		this->Text.setString(s);
	}
	else
	{
		if (this->SliderType == Slider::Type::Centered)
		{
			this->SliderSprite->setPosition(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width / 2 - this->SliderSprite->getGlobalBounds().width / 2, this->Sprite->getPosition().y);
		}
	}

	if (this->MouseOver && input->IsClicked(sf::Mouse::Right))
	{
		this->Value = 0;

		std::ostringstream ss;
		ss << (int)this->Value;
		std::string s(ss.str());

		this->Text.setString(s);
	}

	if (this->MouseOver && (input->WasClicked(sf::Mouse::Left) || input->WasClicked(sf::Mouse::Right)))
	{
		this->WasPressed = true;
	}
	else
	{
		this->WasPressed = false;
	}
}


void Slider::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Visible)
	{
		target.draw(*this->Sprite);
		target.draw(*this->SliderSprite);
		target.draw(this->Text);
	}
}