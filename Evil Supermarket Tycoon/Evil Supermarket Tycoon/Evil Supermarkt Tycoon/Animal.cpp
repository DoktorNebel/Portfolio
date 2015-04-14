#include "Animal.h"


Animal::Animal(void)
{
	this->Rotation = 2;
	this->Moved = false;
	this->DeleteMe = false;
	this->AnimPhase = 0;
	this->Walking = false;
	this->WaitTimer = 0.0f;
	this->ToBeDeleted = false;
	this->ToBeDeletedAgain = false;
	this->LifeTime = 120.0f;
	this->AnimState = 0;
}


Animal::~Animal(void)
{
}


void Animal::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{
	if (this->Walking)
	{
		int difX = 0;
		int difY = 0;

		if (this->GridPosition.x > (float)this->NextPos.x)
		{
			difX = -1;
		}
		else if (this->GridPosition.x < (float)this->NextPos.x)
		{
			difX = 1;
		}

		if (this->GridPosition.y > (float)this->NextPos.y)
		{
			difY = -1;
		}
		else if (this->GridPosition.y < (float)this->NextPos.y)
		{
			difY = 1;
		}


		this->GridPosition += sf::Vector2f(difX * elapsedTime.asSeconds() * gameSpeed, difY * elapsedTime.asSeconds() * gameSpeed);
		this->LowestGridPosition = this->GridPosition - sf::Vector2f(0.5, 0.5);
		if (abs(this->GridPosition.x - (float)this->PreviousGridPosition.x) > 0.5f || abs(this->GridPosition.y - (float)this->PreviousGridPosition.y) > 0.5f)
		{
			this->Moved = true;
		}

		if (abs(this->GridPosition.x - this->NextPos.x) < 0.1f && abs(this->GridPosition.y - this->NextPos.y) < 0.1f)
		{
			this->Walking = false;
			this->WaitTimer = 5.0f;
		}
	}
	else
	{
		this->AnimPhase = this->Sprite[0].size() - 1;

		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0.0f)
		{
			bool pos = false;
			int j = 0;

			while (!pos && j < 4)
			{
				int i = rand() % 4;
				j++;
				pos = true;

				if (i == 0)
				{
					this->NextPos = sf::Vector2i(this->GridPosition.x + 1, this->GridPosition.y);
					this->Rotation = 0;
				}
				else if (i == 1)
				{
					this->NextPos = sf::Vector2i(this->GridPosition.x, this->GridPosition.y + 1);
					this->Rotation = 1;
				}
				else if (i == 2)
				{
					this->NextPos = sf::Vector2i(this->GridPosition.x - 1, this->GridPosition.y);
					this->Rotation = 2;
				}
				else if (i == 3)
				{
					this->NextPos = sf::Vector2i(this->GridPosition.x, this->GridPosition.y - 1);
					this->Rotation = 3;
				}

				if (this->NextPos.x > 0 && this->NextPos.x < this->Grid->size() && 
					this->NextPos.y > 0 && this->NextPos.y < (*this->Grid)[0].size())
				{
					std::list<Entity*>::iterator iter = (*this->Grid)[this->NextPos.x][this->NextPos.y].begin();
					std::list<Entity*>::iterator end = (*this->Grid)[this->NextPos.x][this->NextPos.y].end();

					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter) && (dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall || dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Door))
						{
							pos = false;
						}

						iter++;
					}
				}
				else 
				{
					pos = false;
				}
			}

			if (pos)
			{
				this->Walking = true;
			}
		}
	}

	this->LifeTime -= elapsedTime.asSeconds() * gameSpeed;
	
	if (this->ToBeDeletedAgain)
	{
		this->DeleteMe = true;
	}

	if (this->ToBeDeleted)
	{
		this->ToBeDeletedAgain = true;
	}
	
	if (this->LifeTime <= 0.0f)
	{
		this->ToBeDeleted = true;
	}

	this->UpdateSprite(elapsedTime, gameSpeed);

	float screenPosX = ((this->GridPosition.x - 0.5) - (this->GridPosition.y + 0.5)) * Constants::GridSize;
	float screenPosY = ((this->GridPosition.x - 0.5) + (this->GridPosition.y + 0.5)) * Constants::GridSize / 2;
	
	this->Sprite[this->AnimState][this->AnimPhase]->setOrigin(this->Origins[this->Rotation]);
	this->Sprite[this->AnimState][this->AnimPhase]->setPosition(screenPosX, screenPosY);
	this->BackSprite[this->AnimState][this->AnimPhase]->setOrigin(this->Origins[this->Rotation]);
	this->BackSprite[this->AnimState][this->AnimPhase]->setPosition(screenPosX, screenPosY);

	if (this->Rotation == 0 && this->Sprite[this->AnimState][this->AnimPhase]->getScale().x < 0)
	{
		this->Sprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 1 && this->Sprite[this->AnimState][this->AnimPhase]->getScale().x > 0)
	{
		this->Sprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 2 && this->BackSprite[this->AnimState][this->AnimPhase]->getScale().x < 0)
	{
		this->BackSprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 3 && this->BackSprite[this->AnimState][this->AnimPhase]->getScale().x > 0)
	{
		this->BackSprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	this->LowestScreenPosition = sf::Vector2f(screenPosX - 0.5 * Constants::GridSize, screenPosY - 0.5 * Constants::GridSize);
}


void Animal::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Rotation < 2)
	{
		target.draw(*this->Sprite[this->AnimState][this->AnimPhase]);
	}
	else
	{
		target.draw(*this->BackSprite[this->AnimState][this->AnimPhase]);
	}
}