#include "Bar.h"


Bar::Bar(std::string name, std::wstring text, sf::Sprite* sprite, sf::Sprite* fillSprite, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset)
{
	this->Name = name;
	this->Value = 1.0f;
	this->Sprite = sprite;
	this->FillSprite = fillSprite;
	this->Sprite->setPosition(windowPosition + windowOffset);
	this->WindowPosition = windowPosition;
	this->Offset = windowOffset;
	this->FillSprite->setPosition(windowPosition + windowOffset);
	this->FillSprite->setColor(sf::Color(128, 242, 152, 255));
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->Text.setString(text);
	this->Text.setPosition(this->Sprite->getPosition().x - this->Text.getGlobalBounds().width - 10, this->Sprite->getPosition().y - 1);
	this->Pressed = false;
	this->JustPressed = false;
	this->WasPressed = false;
}


Bar::~Bar(void)
{
}


void Bar::update(Input* input)
{

}


void Bar::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	target.draw(*this->FillSprite);
	target.draw(this->Text);
}


void Bar::SetValue(float value)
{
	this->Value = value;
	this->FillSprite->setTextureRect(sf::IntRect(this->FillSprite->getTextureRect().left, this->FillSprite->getTextureRect().top,this->Sprite->getTextureRect().width * (int)this->Value, this->FillSprite->getTextureRect().height));
}


float Bar::GetValue()
{
	return this->Value;
}