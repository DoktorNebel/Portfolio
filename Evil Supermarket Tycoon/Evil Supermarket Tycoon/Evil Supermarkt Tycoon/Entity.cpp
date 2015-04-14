#include "Entity.h"


Entity::Entity(void)
{
}


Entity::~Entity(void)
{
	this->deleteSprites();
}


void Entity::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{

}


void Entity::draw(sf::RenderTarget& target, sf::RenderStates states) const
{

}


void Entity::setColor(sf::Color color)
{
	for (int i = 0; i < this->Sprite.size(); i++)
	{
		for (int j = 0; j < this->Sprite[i].size(); j++)
		{
			this->Sprite[i][j]->setColor(color);
		}
	}

	if (!this->BackSprite.empty())
	{

		for (int i = 0; i < this->BackSprite.size(); i++)
		{
			for (int j = 0; j < this->BackSprite[i].size(); j++)
			{
				this->BackSprite[i][j]->setColor(color);
			}
		}
	}
}


void Entity::scale(sf::Vector2f factor)
{
	for (int i = 0; i < this->Sprite.size(); i++)
	{
		for (int j = 0; j < this->Sprite[i].size(); j++)
		{
			this->Sprite[i][j]->scale(factor);
		}
	}
}


void Entity::setOrigin(sf::Vector2f origin)
{
	for (int i = 0; i < this->Sprite.size(); i++)
	{
		for (int j = 0; j < this->Sprite[i].size(); j++)
		{
			this->Sprite[i][j]->setOrigin(origin);
		}
	}
}


void Entity::deleteSprites()
{
	for (int i = 0; i < this->Sprite.size(); i++)
	{
		for (int j = 0; j < this->Sprite[i].size(); j++)
		{
			delete this->Sprite[i][j];
		}
	}
}


void Entity::UpdateSprite(sf::Time elapsedTime, unsigned short gameSpeed)
{
	if (this->Sprite[0].size() > 1)
	{
		this->AnimTimer += elapsedTime.asSeconds() * gameSpeed;

		if (AnimTimer >= 0.10f)
		{
			this->AnimTimer = 0.0f;
			this->AnimPhase++;
			if (this->AnimPhase >= this->Sprite[this->AnimState].size())
			{
				this->AnimPhase = 0;
				if (this->IdleWait < 0.0f && rand() % 10 < 1)
				{
					this->IdleWait = rand() % 6 + 5;
				}
			}
		}
	}
}