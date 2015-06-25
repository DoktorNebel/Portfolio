#pragma once
#include <string>
#include <list>
#include <SFML/System/Vector2.hpp>
#include "Entity.h"
#include "Pathfinder.h"
#include "StoreMessage.h"
#include "FeedMessage.h"

class Person :
	public Entity
{
protected:

	bool PosRequested;
	bool PathRequested;
	int EmotionState;

public:

	float EmoticonTimer;
	unsigned int PersonID;
	std::wstring Name;
	unsigned short Age;
	float Happiness;
	std::list<sf::Vector2i> Path;
	float Speed;
	enum Gender
	{
		Male,
		Female
	} PersonGender;
	Pathfinder* ThePathfinder;
	std::list<StoreMessage>* Messages;
	float WaitTimer;
	bool ShowEmotion;
	std::vector<std::vector<sf::Sprite*>> Emoticons;
	std::list<FeedMessage>* Feed;

public:

	Person(void);
	~Person(void);

	void Move(sf::Vector2i targetPos);
	void UpdateEmoticon(sf::Time elapsedTime, unsigned short gameSpeed);
};

