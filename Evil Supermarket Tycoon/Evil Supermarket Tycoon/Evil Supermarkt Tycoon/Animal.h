#pragma once

#include "entity.h"
#include "StoreObject.h"

class Animal :
	public Entity
{

public:

	sf::Vector2i NextPos;
	float WaitTimer;
	bool Walking;
	bool ToBeDeletedAgain;
	float LifeTime;
	bool ToBeDeleted;
	std::wstring Name;

public:

	Animal(void);
	~Animal(void);

	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

