#include "IllegalWindow.h"


IllegalWindow::IllegalWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::vector<IllegalActivity>* illegalStuff)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);

	std::stringstream sstream;
	for (int i = 0; i < illegalStuff->size() - 2; i++)
	{
		sstream.str("");
		sstream << i;
		this->Buttons.push_back(new InterfaceButton(sstream.str(), InterfaceButton::Type::Permanent, texHandler->GetSprite("CheckBox")[0][0], position, sf::Vector2f(10, 10 + 50 * i)));
		this->Buttons.back()->PressedSprite = texHandler->GetSprite("CheckBox_Pressed")[0][0];
		this->Buttons.back()->PressedSprite->setPosition(this->Buttons.back()->Sprite->getPosition());
		this->Buttons.back()->Sprite->setScale(0.5f, 0.5f);
		this->Buttons.back()->PressedSprite->setScale(0.5f, 0.5f);
		this->Buttons.back()->Text.setFont(*texHandler->GetFont());
		this->Buttons.back()->Text.setString((*illegalStuff)[i].Name);
		this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(25, -2));
	}

	this->Buttons.push_back(new InterfaceButton("Close", InterfaceButton::Type::Normal, texHandler->GetSprite("CloseButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(950, 30)));
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("CloseButton_Pressed")[0][0];

	this->IsOpen = false;

	this->HoverWindowSprite = texHandler->GetSprite("Window")[0][0];
	this->HoverWindowSprite->setScale(0.6, 0.1);
	this->HoverWindowText.setFont(*texHandler->GetFont());
	this->HoverWindowText.setCharacterSize(14);
	this->HoverWindowText.setColor(sf::Color(128, 242, 152, 255));

	this->IllegalStuff = illegalStuff;
}


IllegalWindow::~IllegalWindow(void)
{
}


void IllegalWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	bool mouseOver = false;

	while (iter != end)
	{
		(*iter)->update(input);

		if ((*iter)->JustPressed)
		{
			if ((*iter)->Name == "Close")
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Illegal";
				messages->push_back(InterfaceMessage(tmp));
			}
			else
			{
				InterfaceMessage tmp;
				if ((*iter)->Pressed)
				{
					tmp.MessageType = InterfaceMessage::Type::ActivateIllegalActivity;
				}
				else
				{
					tmp.MessageType = InterfaceMessage::Type::DeactivateIllegalActivity;
				}
				tmp.Text = (*iter)->Name;
				messages->push_back(InterfaceMessage(tmp));
			}
		}

		if ((*iter)->MouseOver && (*iter)->Name != "Close")
		{
			mouseOver = true;
			this->HoverWindowSprite->setPosition(input->GetMousePos().x, input->GetMousePos().y);
			this->HoverWindowText.setPosition(this->HoverWindowSprite->getPosition() + sf::Vector2f(10, 10));
			this->HoverWindowText.setString((*this->IllegalStuff)[atoi((*iter)->Name.c_str())].Description);
		}
		iter++;
	}

	if (!mouseOver)
	{
		this->HoverWindowText.setString("");
	}
}


void IllegalWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);
		iter++;
	}

	if (this->HoverWindowText.getString() != "")
	{
		target.draw(*this->HoverWindowSprite);
		target.draw(this->HoverWindowText);
	}
}