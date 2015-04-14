#include "FeedWindow.h"


FeedWindow::FeedWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::list<FeedMessage>* feed)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->PrevSize = 0;
	this->Feed = feed;
	this->TexHandler = texHandler;
	this->PullButton = new InterfaceButton("Pull", InterfaceButton::Type::Normal, texHandler->GetSprite("PullButton")[0][0], position, sf::Vector2f(this->Sprite->getGlobalBounds().width / 2, 40));
	this->PullButton->Sprite->setOrigin(this->PullButton->Sprite->getGlobalBounds().width / 2, this->PullButton->Sprite->getGlobalBounds().height / 2);
	this->PullButton->PressedSprite = texHandler->GetSprite("PullButton_Pressed")[0][0];
	this->PullButton->PressedSprite->setPosition(this->PullButton->Sprite->getPosition());
	this->PullButton->PressedSprite->setOrigin(this->PullButton->Sprite->getOrigin());
	this->MessageCount = 0;

	this->IsOpen = true;
}


FeedWindow::~FeedWindow(void)
{
}


void FeedWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	if (this->Feed->size() > this->PrevSize)
	{
		if (this->Feed->size() >= 51)
		{
			this->Feed->pop_back();

			if (!this->Texts.empty())
			{
				delete this->Texts.back();
				this->Texts.pop_back();
			}

			if (!this->Buttons.empty())
			{
				delete this->Buttons.back();
				this->Buttons.pop_back();
			}
		}
		this->PrevSize = this->Feed->size();

		this->Texts.push_front(new sf::Text(this->Feed->front().Message, *this->TexHandler->GetFont(), 14));
		this->Texts.front()->setColor(sf::Color(128, 242, 152, 255));
		this->Texts.front()->setPosition(10, 10);

		this->Buttons.push_front(new InterfaceButton("", InterfaceButton::Type::Normal, this->TexHandler->GetSprite("JumpToPosButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(500, 10)));
		this->Buttons.front()->PressedSprite = this->TexHandler->GetSprite("JumpToPosButton_Pressed")[0][0];
	}


	this->PullButton->update(input);
	if (this->PullButton->Pressed)
	{
		this->PullButton->Sprite->setPosition(this->PullButton->Sprite->getPosition().x, input->GetMousePos().y);
		if (this->PullButton->Sprite->getPosition().y < this->Sprite->getPosition().y + 40)
		{
			this->PullButton->Sprite->setPosition(this->Sprite->getPosition() + sf::Vector2f(this->Sprite->getGlobalBounds().width / 2, 40));
			this->PullButton->PressedSprite->setPosition(this->Sprite->getPosition());
		}
	}

	this->MessageCount = (this->PullButton->Sprite->getPosition().y - 30 - this->Sprite->getPosition().y) / 14.0f;
	int i = 0;


	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();
	std::list<FeedMessage>::iterator feediter = this->Feed->begin();
	std::list<FeedMessage>::iterator feedend = this->Feed->end();

	while (iter != end && i < this->MessageCount)
	{
		if (feediter->Pointer != 0)
		{
			(*iter)->update(input);
		}

		if ((*iter)->JustPressed)
		{
			InterfaceMessage tmp;
			tmp.MessageType = InterfaceMessage::Type::ChangeCameraPos;
			tmp.Pointer = feediter->Pointer;

			messages->push_back(tmp);
		}

		iter++;
		feediter++;
		i++;
	}
}


void FeedWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	std::list<InterfaceButton*>::const_iterator buttoniter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator buttonend = this->Buttons.end();
	std::list<sf::Text*>::const_iterator textiter = this->Texts.begin();
	std::list<sf::Text*>::const_iterator textend = this->Texts.end();
	std::list<FeedMessage>::iterator feediter = this->Feed->begin();
	std::list<FeedMessage>::iterator feedend = this->Feed->end();
	int i = 0;

	this->Sprite->setScale(1.0f, 0.0545f * ((float)this->MessageCount + 1.4f));
	target.draw(*this->Sprite);

	target.draw(*this->PullButton);

	while (buttoniter != buttonend && i < this->MessageCount / 1.8f)
	{
		int opacity = (this->MessageCount / 1.8f - i) * 255;
		if (opacity > 255 || i == 0)
		{
			opacity = 255;
		}

		if (feediter->Pointer != 0)
		{
			(*buttoniter)->Sprite->setPosition(this->PullButton->Sprite->getPosition() + sf::Vector2f(450, -40 - 25 * i));
			(*buttoniter)->PressedSprite->setPosition((*buttoniter)->Sprite->getPosition());

			(*buttoniter)->Sprite->setColor(sf::Color(255, 255, 255, opacity));
			(*buttoniter)->PressedSprite->setColor(sf::Color(255, 255, 255, opacity));
			target.draw(**buttoniter);
		}

		(*textiter)->setPosition(this->PullButton->Sprite->getPosition() + sf::Vector2f(-500, -35 - 25 * i));
		(*textiter)->setColor(sf::Color(128, 242, 152, opacity));
		target.draw(**textiter);

		i++;
		buttoniter++;
		textiter++;
		feediter++;
	}
}