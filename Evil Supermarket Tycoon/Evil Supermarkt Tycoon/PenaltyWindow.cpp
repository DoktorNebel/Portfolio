#include "PenaltyWindow.h"


PenaltyWindow::PenaltyWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->Sprite->setScale(0.35f, 0.5f);

	this->Message.setCharacterSize(14);
	this->Message.setColor(sf::Color(128, 242, 152, 255));
	this->Message.setFont(*texHandler->GetFont());
	this->Message.setPosition(position + sf::Vector2f(10, 10));

	this->Buttons.push_back(new InterfaceButton("OK", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], position, sf::Vector2f(150, 85)));
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(3, 2));
	this->Buttons.back()->Text.setString("OK");

	this->IsOpen = false;
}


PenaltyWindow::~PenaltyWindow(void)
{
}


void PenaltyWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->update(input);

		if ((*iter)->JustPressed)
		{
			InterfaceMessage tmp;
			tmp.MessageType = InterfaceMessage::Type::CloseWindow;
			tmp.Text = "Penalty";
			tmp.Pointer = 0;

			(*iter)->Pressed = false;
			(*iter)->JustPressed = false;

			messages->push_back(tmp);
		}

		iter++;
	}
}


void PenaltyWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	target.draw(this->Message);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);
		iter++;
	}
}