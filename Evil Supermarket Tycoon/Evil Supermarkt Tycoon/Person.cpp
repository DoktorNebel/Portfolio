#include "Person.h"


Person::Person(void)
{
}


Person::~Person(void)
{
}


void Person::UpdateEmoticon(sf::Time elapsedTime, unsigned short gameSpeed)
{
	this->EmotionState = (int)std::min(this->Happiness * 6.0f, 5.0f);

	this->EmoticonTimer += elapsedTime.asSeconds() * gameSpeed;

	this->Emoticons[0][this->EmotionState]->setPosition(this->Sprite[0][this->AnimPhase]->getPosition() + sf::Vector2f(40, -260));

	if (this->EmoticonTimer < 1.5f)
	{
		float scale = -pow(this->EmoticonTimer - 1, 2) + 1;
		this->Emoticons[0][this->EmotionState]->setScale(scale * 1.3f, scale * 1.3f);
		this->Emoticons[0][this->EmotionState]->setColor(sf::Color(255, 255, 255, (unsigned char)(scale * 255.0f)));
	}
	else if (this->EmoticonTimer < 5.0f)
	{
		this->Emoticons[0][this->EmotionState]->setScale(1.0f, 1.0f);
		this->Emoticons[0][this->EmotionState]->setColor(sf::Color::White);
	}
	else if (this->EmoticonTimer < 6.0f)
	{
		this->Emoticons[0][this->EmotionState]->setColor(sf::Color(255, 255, 255, (unsigned char)((6.0f - this->EmoticonTimer) * 255.0f)));
	}
	else
	{
		this->ShowEmotion = false;
		this->EmoticonTimer = 0.0f;
	}
}