#include "Worker.h"


Worker::Worker(void)
{
	this->Moved = false;
	this->Object = 0;
	this->animal = 0;
	this->WaitTimer = 0.0f;
	this->DeleteMe = false;
	this->Rotation = 2;
	this->Moved = false;
	this->AnimPhase = 0;
	this->WorkState = Worker::State::Nothing;
	this->AtWorkplace = false;
	this->ProductItem = 0;
	this->IdleWait = 0.0f;
	this->AnimState = 3;
	this->AnimPhase = 0;
	this->AnimTimer = 0.0f;
	this->EmoticonTimer = 0.0f;
	this->EmotionState = 0;
	this->ShowEmotion = false;
}


Worker::~Worker(void)
{
}


void Worker::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{
	int difX = 0;
	int difY = 0;

	int amount = 0;
	int putAmount = 0;
	int putAmount2 = 0;

	sf::Vector2i objPos;
	sf::Vector2i checkPos;

	std::list<StoreJob>::iterator iter;
	std::list<StoreJob>::iterator end;

	std::list<Product>::iterator anotheriter;
	std::list<Product>::iterator anotherend;

	
	std::list<Entity*>::iterator yetanotheriter;
	std::list<Entity*>::iterator yetanotherend;

	double dif = this->WageExpectancy - this->Wage;
	
	this->Happiness = std::max(std::min(1.0f - (float)dif * 0.5f, 1.0f), 0.0f);
	

	switch (this->WorkState)
	{
	case Worker::State::Walking:
		if (this->Path.empty())
		{
			if (this->WorkerType == Worker::Type::Cashier)
			{
				this->WorkState = Worker::State::Cashiering;
				this->AtWorkplace = true;
				this->WaitTimer = 10.0f;
			}
			if (this->WorkerType == Worker::Type::Cleaner)
			{
				this->WorkState = Worker::State::Cleaning;
				this->WaitTimer = 0.5f;
			}
			if (this->WorkerType == Worker::Type::Storager)
			{
				if (this->animal)
				{
					this->WorkState = Worker::State::Butchering;
					this->WaitTimer = 1.0f;
					gameData->SoundHandler->PlaySound("Meat");
				}
				else
				{
					checkPos = sf::Vector2i((int)(this->GridPosition.x - 0.5f), (int)(this->GridPosition.y + 0.5f));
					yetanotheriter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					yetanotherend = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (yetanotheriter != yetanotherend)
					{
						if (dynamic_cast<StoreObject*>(*yetanotheriter) == this->Object)
						{
							objPos = checkPos;
						}
						yetanotheriter++;
					}

					checkPos = sf::Vector2i((int)(this->GridPosition.x + 1.5f), (int)(this->GridPosition.y + 0.5f));
					yetanotheriter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					yetanotherend = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (yetanotheriter != yetanotherend)
					{
						if (dynamic_cast<StoreObject*>(*yetanotheriter) == this->Object)
						{
							objPos = checkPos;
						}
						yetanotheriter++;
					}

					checkPos = sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y - 0.5f));
					yetanotheriter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					yetanotherend = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (yetanotheriter != yetanotherend)
					{
						if (dynamic_cast<StoreObject*>(*yetanotheriter) == this->Object)
						{
							objPos = checkPos;
						}
						yetanotheriter++;
					}

					checkPos = sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y + 1.5f));
					yetanotheriter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					yetanotherend = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (yetanotheriter != yetanotherend)
					{
						if (dynamic_cast<StoreObject*>(*yetanotheriter) == this->Object)
						{
							objPos = checkPos;
						}
						yetanotheriter++;
					}

					if (this->GridPosition.x - objPos.x > 0.5f)
					{
						this->Rotation = 2;
					}
					if (this->GridPosition.x - objPos.x < -0.5f)
					{
						this->Rotation = 0;
					}
					if (this->GridPosition.y - objPos.y > 0.5f)
					{
						this->Rotation = 3;
					}
					if (this->GridPosition.y - objPos.y < -0.5f)
					{
						this->Rotation = 1;
					}

					this->WorkState = Worker::State::Refilling;
					this->WaitTimer = 1.0f;
				}
			}
		}
		else
		{
			if (this->GridPosition.x > (float)this->Path.front().x)
			{
				difX = -1;
			}
			else if (this->GridPosition.x < (float)this->Path.front().x)
			{
				difX = 1;
			}

			if (this->GridPosition.y > (float)this->Path.front().y)
			{
				difY = -1;
			}
			else if (this->GridPosition.y < (float)this->Path.front().y)
			{
				difY = 1;
			}

			this->GridPosition += sf::Vector2f(difX * this->Speed * elapsedTime.asSeconds() * gameSpeed, difY * this->Speed * elapsedTime.asSeconds() * gameSpeed);
			this->LowestGridPosition = this->GridPosition - sf::Vector2f(0.5, 0.5);
			if ((int)this->GridPosition.x != this->PreviousGridPosition.x || (int)this->GridPosition.y != this->PreviousGridPosition.y)
			{
				this->Moved = true;
			}

			if (abs(this->GridPosition.x - this->Path.front().x) < 0.1f && abs(this->GridPosition.y - this->Path.front().y) < 0.1f)
			{
				this->Path.pop_front();
				if (!this->Path.empty())
				{
					if (this->GridPosition.x - this->Path.front().x > 0.5f)
					{
						this->Rotation = 2;
					}
					if (this->GridPosition.x - this->Path.front().x < -0.5f)
					{
						this->Rotation = 0;
					}
					if (this->GridPosition.y - this->Path.front().y > 0.5f)
					{
						this->Rotation = 3;
					}
					if (this->GridPosition.y - this->Path.front().y < -0.5f)
					{
						this->Rotation = 1;
					}
				}
			}
		}
		break;


	case Worker::State::Nothing:
		iter = this->Jobs->begin();
		end = this->Jobs->end();

		while (iter != end)
		{
			if (this->WorkerType == Worker::Type::Cashier && iter->JobType == StoreJob::Type::Cashiering)
			{
				if (this->Object)
				{
					this->Object->CurrentWorker = 0;
				}
				this->Object = dynamic_cast<StoreObject*>(iter->Object);
				this->ThePathfinder->RequestPath(sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y + 0.5f)), sf::Vector2i((int)this->Object->GridPosition.x + this->Object->WorkPoints[0].x, (int)this->Object->GridPosition.y + this->Object->WorkPoints[0].y), this->ID);
				this->WorkState = Worker::State::WaitingForPath;
				this->Object->CurrentWorker = this;
				this->Jobs->erase(iter);
				break;
			}

			if (this->WorkerType == Worker::Type::Cleaner && iter->JobType == StoreJob::Type::Cleaning && !iter->Taken)
			{
				if (this->Object)
				{
					this->Object->CurrentWorker = 0;
				}
				sf::Vector2i pos = sf::Vector2i((int)iter->Object->GridPosition.x, (int)iter->Object->GridPosition.y);
				sf::Vector2i point;
				this->Object = dynamic_cast<StoreObject*>(iter->Object);
				if (this->Object->AccessPoints.size() > 0)
				{
					point = this->Object->AccessPoints[rand() % this->Object->AccessPoints.size()];
					pos = sf::Vector2i((int)iter->Object->GridPosition.x + point.x, (int)iter->Object->GridPosition.y + point.y);
				}
				this->ThePathfinder->RequestPath(sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y + 0.5f)), pos, this->ID);
				this->WorkState = Worker::State::WaitingForPath;
				iter->Taken = true;
				this->CurrentJob = *iter;
				break;
			}

			if (this->WorkerType == Worker::Type::Storager)
			{
				if (iter->JobType == StoreJob::Type::Refilling)
				{
					if (this->Object)
					{
						this->Object->CurrentWorker = 0;
					}
					sf::Vector2i pos;
					this->Object = dynamic_cast<StoreObject*>(iter->Object);
					for (unsigned int i = 0; i < this->Object->Slots.size(); i++)
					{
						if (this->Object->Slots[i] && this->Object->Slots[i]->Name == iter->ProductItem->Name)
						{
							pos = sf::Vector2i((int)this->Object->GridPosition.x + this->Object->AccessPoints[i].x, (int)this->Object->GridPosition.y + this->Object->AccessPoints[i].y);
						}
					}
					this->ThePathfinder->RequestPath(sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y + 0.5f)), pos, this->ID);
					this->WorkState = Worker::State::WaitingForPath;
					this->ProductItem = iter->ProductItem;
					this->Jobs->erase(iter);
					break;
				}
				else if (iter->JobType == StoreJob::Type::Butchering)
				{
					if (this->Object)
					{
						this->Object->CurrentWorker = 0;
					}
					this->animal = dynamic_cast<Animal*>(iter->Object);
					this->ThePathfinder->RequestPath(sf::Vector2i((int)(this->GridPosition.x + 0.5f), (int)(this->GridPosition.y + 0.5f)), sf::Vector2i((int)this->animal->GridPosition.x, (int)this->animal->GridPosition.y), this->ID);
					this->WorkState = Worker::State::WaitingForPath;
					this->ProductItem = iter->ProductItem;
					this->Jobs->erase(iter);
					break;
				}
			}

			iter++;
		}
		break;


	case Worker::State::WaitingForPath:
		this->Path = this->ThePathfinder->GetPath(this->ID);
		if (!this->Path.empty())
		{
			if (this->animal)
			{
				this->Path.pop_back();
			}
			this->WorkState = Worker::State::Walking;
			this->AtWorkplace = false;
		}
		break;


	case Worker::State::Cashiering:
		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			this->WorkState = Worker::State::Nothing;
		}
		break;


	case Worker::State::Cleaning:
		this->WaitTimer -= elapsedTime.asSeconds() * this->CleaningEfficiency * std::max(0.3f, this->Happiness) * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			if (this->Object->Dirtiness > 0)
			{
				this->Object->Dirtiness--;
			}
			this->WorkState = Worker::State::Nothing;
			iter = this->Jobs->begin();
			end = this->Jobs->end();

			while (iter != end)
			{
				if (iter->JobType == this->CurrentJob.JobType && iter->Object->ID == this->CurrentJob.Object->ID)
				{
					this->Jobs->erase(iter);
					break;
				}

				iter++;
			}
		}
		break;


	case Worker::State::Refilling:
		this->WaitTimer -= elapsedTime.asSeconds() * this->RefillingEfficiency * std::max(0.3f, this->Happiness) * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			for (unsigned int i = 0; i < this->Object->Slots.size(); i++)
			{
				if (this->Object->Slots[i] && this->ProductItem->Name == this->Object->Slots[i]->Name)
				{
					if (this->Object->Products[i].size() > 0 && this->Object->Products[i].front().Name != this->ProductItem->Name)
					{
						this->TheStorage->addProducts(&this->Object->Products[i]);
						this->Object->Products[i].clear();

						for (unsigned int j = 0; j < this->Object->ShownProducts[i].size(); j++)
						{
							this->Object->ShownProducts[i][j] = false;
						}
					}
					anotheriter = this->Object->Products[i].begin();
					anotherend = this->Object->Products[i].end();

					while (anotheriter != anotherend)
					{
						amount += anotheriter->Amount;

						anotheriter++;
					}
					putAmount = std::min(this->Object->ProdsPerSlot, this->TheStorage->countProduct(this->ProductItem->Name)) - amount;
					putAmount2 = putAmount;

					anotheriter = this->TheStorage->StorageItems.begin();
					anotherend = this->TheStorage->StorageItems.end();

					while (anotheriter != anotherend)
					{
						if (anotheriter->Name == this->ProductItem->Name)
						{
							this->Object->Products[i].push_back(*anotheriter);


							if (this->Object->Products[i].back().Amount >= putAmount)
							{
								this->Object->Products[i].back().Amount = putAmount;
								break;
							}
							putAmount -= this->Object->Products[i].back().Amount;
						}

						anotheriter++;
					}

					for (int j = 0; j < putAmount2; j++)
					{
						int k = rand() % this->Object->ShownProducts[i].size();
						while (this->Object->ShownProducts[i][k])
						{
							k = rand() % this->Object->ShownProducts[i].size();
						}
						this->Object->ShownProducts[i][k] = true;
					}

					this->TheStorage->deleteProducts(this->ProductItem->Name, putAmount2);

				}
			}
			this->WorkState = Worker::State::Nothing;
		}
		break;


	case Worker::State::Butchering:
		this->WaitTimer -= elapsedTime.asSeconds() * this->RefillingEfficiency * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			this->animal->ToBeDeleted = true;
			Product* product = this->TheStorage->addMeat(gameData);
			if (product)
			{
				FeedMessage tmp;
				std::stringstream sstream;
				sstream << product->Name << "(" << product->Amount << ") wurde zum Lager hinzugefügt.";
				tmp.Message = sstream.str();
				tmp.Pointer = 0;
				this->Feed->push_front(tmp);

				delete product;
			}
			this->animal = 0;
			this->WorkState = Worker::State::Nothing;
			gameData->Prestige -= 2;

		}
		break;
	}

	if (this->WorkState == Worker::State::Walking)
	{
		this->AnimState = 0;
		this->IdleWait = (float)(rand() % 6 + 5);
	}
	else if (this->WorkState == Worker::State::Nothing)
	{
		if (this->Sprite[1].size() > 0)
		{
			this->IdleWait -= elapsedTime.asSeconds() * gameSpeed;

			if (this->IdleWait > 0.0f)
			{
				this->AnimState = 3;
				this->AnimPhase = 0;
			}
			else if (this->AnimState == 3)
			{
				if (this->Sprite.size() > 4)
				{
					if (rand() % 2)
					{
						this->AnimState = 1;
					}
					else
					{
						this->AnimState = 4;
					}
				}
				else
				{
					this->AnimState = 1;
				}
			}
		}
		else
		{
			this->AnimState = 3;
			this->AnimPhase = 0;
		}
	}
	else if (this->WorkState == Worker::State::Cleaning)
	{
		this->AnimState = 2;
		this->IdleWait = (float)(rand() % 6 + 5);
	}
	else
	{
		this->AnimState = 3;
		this->IdleWait = (float)(rand() % 6 + 5);
		this->AnimPhase = 0;
	}

	this->UpdateSprite(elapsedTime, gameSpeed);

	if (this->AnimPhase >= this->Sprite[this->AnimState].size())
	{
		this->AnimPhase = 0;
	}


	float screenPosX = ((this->GridPosition.x - 0.5f) - (this->GridPosition.y + 0.5f)) * (float)Constants::GridSize;
	float screenPosY = ((this->GridPosition.x - 0.5f) + (this->GridPosition.y + 0.5f)) * (float)Constants::GridSize / 2.0f;
	
	this->Sprite[this->AnimState][this->AnimPhase]->setOrigin(this->Origins[this->Rotation]);
	if (this->AnimState == 2)
	{
		this->Sprite[this->AnimState][this->AnimPhase]->setOrigin(this->Sprite[this->AnimState][this->AnimPhase]->getOrigin() + this->CleaningOffsets[this->Rotation]);
	}
	this->Sprite[this->AnimState][this->AnimPhase]->setPosition(screenPosX, screenPosY);
	if (this->AnimPhase < this->BackSprite[this->AnimState].size())
	{
		this->BackSprite[this->AnimState][this->AnimPhase]->setOrigin(this->Origins[this->Rotation]);
		if (this->AnimState == 2)
		{
			this->BackSprite[this->AnimState][this->AnimPhase]->setOrigin(this->BackSprite[this->AnimState][this->AnimPhase]->getOrigin() + this->CleaningOffsets[this->Rotation]);
		}
		this->BackSprite[this->AnimState][this->AnimPhase]->setPosition(screenPosX, screenPosY);
	}

	if (this->Rotation == 0 && this->Sprite[this->AnimState][this->AnimPhase]->getScale().x < 0)
	{
		this->Sprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 1 && this->Sprite[this->AnimState][this->AnimPhase]->getScale().x > 0)
	{
		this->Sprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 2 && this->AnimPhase < this->BackSprite[this->AnimState].size() && this->BackSprite[this->AnimState][this->AnimPhase]->getScale().x < 0)
	{
		this->BackSprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	if (this->Rotation == 3 && this->AnimPhase < this->BackSprite[this->AnimState].size() && this->BackSprite[this->AnimState][this->AnimPhase]->getScale().x > 0)
	{
		this->BackSprite[this->AnimState][this->AnimPhase]->scale(-1, 1);
	}
	this->LowestScreenPosition = sf::Vector2f(screenPosX - 0.5f * (float)Constants::GridSize, screenPosY - 0.5f * (float)Constants::GridSize);
}


void Worker::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Rotation < 2 || this->AnimPhase >= this->BackSprite[this->AnimState].size())
	{
		target.draw(*this->Sprite[this->AnimState][this->AnimPhase]);
	}
	else
	{
		target.draw(*this->BackSprite[this->AnimState][this->AnimPhase]);
	}
}


void Worker::calculateWageExpectency()
{
	float highest = std::max(std::max(this->CleaningEfficiency, this->RefillingEfficiency), this->CashieringEfficiency);
	
	this->WageExpectancy = highest * 15.0;
}