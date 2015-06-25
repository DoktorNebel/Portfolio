#include "StoreObject.h"


StoreObject::StoreObject()
{
	this->Rotation = 0;
	this->Moved = false;
	this->DeleteMe = false;
	this->Dirtiness = 0;
	this->DirtTimer = 0;
	this->PreviousMinute = 0;
	this->CurrentWorker = 0;
	this->CustomerCount = 0;
}


StoreObject::StoreObject(StoreObject::Type type, StoreObject::Quality quality, short width, short height, std::vector<sf::Vector2i> accessPoints, std::vector<sf::Vector2i> workPoints, unsigned short slots, std::vector<std::vector<std::vector<sf::Vector2i>>> pPositions, int prodsPerLayer, int prodsPerSlot)
{
	this->ObjectType = type;
	this->ObjectQuality = quality;
	this->Width = width;
	this->Height = height;
	this->AccessPoints = accessPoints;
	this->WorkPoints = workPoints;
	this->GridPosition = sf::Vector2f(0, 0);
	this->Rotation = 0;
	this->Slots.resize(slots);
	this->Products.resize(slots);
	this->Prices.resize(slots);
	this->Dates.resize(slots);
	this->ProductPositions = pPositions;
	this->ProdsPerLayer = prodsPerLayer;
	this->Moved = false;
	this->DeleteMe = false;
	this->ProdsPerSlot = prodsPerSlot;
	this->ShownProducts.resize(this->Slots.size());
	for (unsigned int i = 0; i < this->ShownProducts.size(); i++)
	{
		this->ShownProducts[i].resize(this->ProdsPerSlot);
		for (unsigned int j = 0; j < this->ShownProducts[i].size(); j++)
		{
			this->ShownProducts[i][j] = false;
		}
	}
	this->Dates.resize(this->Slots.size());
	this->Prices.resize(this->Slots.size());
	this->Dirtiness = 0;
	this->setDirtTimer();
	this->PreviousMinute = 0;
	this->CurrentWorker = 0;
	this->CustomerCount = 0;
}


StoreObject::~StoreObject(void)
{

}


void StoreObject::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{
	if (gameData != 0 && (int)gameData->Time.Minute != this->PreviousMinute && this->Dirtiness < 3 &&
		this->ObjectQuality != StoreObject::Quality::Nothing && ((this->ObjectType == StoreObject::Type::Floor && (*this->Grid)[(unsigned int)this->GridPosition.x][(unsigned int)this->GridPosition.y].size() == 1) || this->ObjectType == StoreObject::Type::Shelf))
	{
		this->PreviousMinute = (int)gameData->Time.Minute;
		this->DirtTimer--;
		
		if (this->DirtTimer <= 0)
		{
			this->setDirtTimer();
			this->Dirtiness++;
		}
	}

	std::list<Product>::iterator iter;
	std::list<Product>::iterator end;
	for (unsigned int i = 0; i < this->Products.size(); i++)
	{
		if (this->Products[i].size() > 0)
		{
			iter = this->Products[i].begin();
			end = this->Products[i].end();

			while (iter != end)
			{
				iter->update(gameData, this->Feed, this);

				iter->Price = this->Prices[i];
				iter->ExpirationDate = this->Dates[i];

				iter++;
			}
		}
	}
	

	float screenPosX = ((this->GridPosition.x - 0.5f) - (this->GridPosition.y + 0.5f)) * (float)Constants::GridSize;
	float screenPosY = ((this->GridPosition.x - 0.5f) + (this->GridPosition.y + 0.5f)) * (float)Constants::GridSize / 2.0f;
	for (unsigned int i = 0; i < this->Sprite.size(); i++)
	{
		for (unsigned int j = 0; j < this->Sprite[0].size(); j++)
		{
			this->Sprite[i][j]->setOrigin(this->Origins[this->Rotation]);
			this->Sprite[i][j]->setPosition(screenPosX, screenPosY);
		}
	}
}


void StoreObject::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	std::list<Product>::const_iterator iter;
	std::list<Product>::const_iterator end;

	if (this->Sprite.size() == 1)
	{
		target.draw(*this->Sprite[0][0]);
		if (this->Dirtiness > 0)
		{
			this->DirtSprites[this->Dirtiness - 1]->setPosition(this->Sprite[0][0]->getPosition());
			this->DirtSprites[this->Dirtiness - 1]->setOrigin(this->Sprite[0][0]->getOrigin());
			target.draw(*this->DirtSprites[this->Dirtiness - 1]);
		}

		if (this->ObjectType != StoreObject::Type::Checkout)
		{
			for (unsigned int i = 0; i < this->Slots.size(); i++)
			{
				if (this->Products.size() > i && !this->Products[i].empty())
				{
					for (unsigned int j = this->ProductPositions[this->Rotation][i].size() - 1; j >= 0; j--)
					{
						if (this->ShownProducts[i][j] && this->ProductPositions[this->Rotation][i][j] != sf::Vector2i(0, 0))
						{
							sf::Vector2f pos = this->Sprite[0][0]->getPosition();
							sf::Vector2f off = sf::Vector2f((float)this->ProductPositions[this->Rotation][i][j].x, (float)this->ProductPositions[this->Rotation][i][j].y);
							//off.x * this->Sprite[0][0]->getScale().x;
							//off.y * this->Sprite[0][0]->getScale().y;
							pos += off;
							if (this->Products[i].size() > 0)
							{
								this->Products[i].front().Sprite->setPosition(pos);
								if (this->Rotation % 2 && this->Products[i].front().Sprite->getScale().x > 0)
								{
									this->Products[i].front().Sprite->scale(-1, 1);
								}
								target.draw(*this->Products[i].front().Sprite);
							}
						}
					}
				}
			}
		}
	}
	else
	{
		target.draw(*this->Sprite[0][0]);

		if (this->ObjectType != StoreObject::Type::Checkout)
		{
			int startprod = 0;
			int endprod = this->Slots.size() / 2;
			if (this->Rotation > 1)
			{
				startprod = this->Slots.size() / 2;
				endprod = this->Slots.size();
			}

			for (unsigned int i = 1; i < this->Sprite.size(); i++)
			{
				target.draw(*this->Sprite[i][0]);


				if (this->Rotation < 2)
				{
					for (unsigned int j = this->Slots.size() - 1; j >= 0; j--)
					{
						if (!this->Products[j].empty())
						{
							for (unsigned int k = i * this->ProdsPerLayer - 1; k >= (i - 1) * this->ProdsPerLayer; k--)
							{
								if (i * this->ProdsPerLayer <= this->ProductPositions[this->Rotation][j].size() && this->ShownProducts[j][k] && this->ProductPositions[this->Rotation][j][k] != sf::Vector2i(0, 0))
								{
									sf::Vector2f pos = this->Sprite[0][0]->getPosition();
									sf::Vector2f off = sf::Vector2f((float)this->ProductPositions[this->Rotation][j][k].x, (float)this->ProductPositions[this->Rotation][j][k].y);
									//off.x * this->Sprite[0][0]->getScale().x;
									//off.y * this->Sprite[0][0]->getScale().y;
									pos += off;
									this->Products[j].front().Sprite->setPosition(pos);
									if (this->Rotation % 2 && this->Products[j].front().Sprite->getScale().x > 0)
									{
										this->Products[j].front().Sprite->scale(-1, 1);
									}
									target.draw(*this->Products[j].front().Sprite);
								}
							}
						}
					}
				}
				else
				{
					for (unsigned int j = 0; j < this->Slots.size(); j++)
					{
						if (!this->Products[j].empty())
						{
							for (unsigned int k = i * this->ProdsPerLayer - 1; k >= (i - 1) * this->ProdsPerLayer; k--)
							{
								if (this->ShownProducts[j][k] && i * this->ProdsPerLayer <= this->ProductPositions[this->Rotation][j].size() && this->ProductPositions[this->Rotation][j][k] != sf::Vector2i(0, 0))
								{
									sf::Vector2f pos = this->Sprite[0][0]->getPosition();
									sf::Vector2f off = sf::Vector2f((float)this->ProductPositions[this->Rotation][j][k].x, (float)this->ProductPositions[this->Rotation][j][k].y);
									//off.x * this->Sprite[0][0]->getScale().x;
									//off.y * this->Sprite[0][0]->getScale().y;
									pos += off;
									this->Products[j].front().Sprite->setPosition(pos);
									if (this->Rotation % 2 && this->Products[j].front().Sprite->getScale().x > 0)
									{
										this->Products[j].front().Sprite->scale(-1, 1);
									}
									target.draw(*this->Products[j].front().Sprite);
								}
							}
						}
					}
				}
			}
		}
	}
}


void StoreObject::setDirtTimer()
{
	if (this->ObjectQuality == StoreObject::Quality::Normal)
	{
		this->DirtTimer = 480 + rand() % 481 - 240;
	}
	else if (this->ObjectQuality == StoreObject::Quality::Premium)
	{
		this->DirtTimer = 960 + rand() % 961 - 480;
	}
	else
	{
		this->DirtTimer = 240 + rand() % 241 - 120;
	}
}