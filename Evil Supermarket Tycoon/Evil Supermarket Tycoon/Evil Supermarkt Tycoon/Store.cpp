#include "Store.h"
#include <SFML/Graphics.hpp>


Store::Store(unsigned short gridSizeX, unsigned short gridSizeY, std::vector<ProductListItem*>* productList, Pathfinder* pathfinder, TextureHandler* texHandler)
{
	this->Grid.resize(gridSizeX);
	this->CostGrid.resize(gridSizeX);
	for (int i = 0; i < gridSizeX; i++)
	{
		this->Grid[i].resize(gridSizeY);
		this->CostGrid[i].resize(gridSizeY);
		for (int j = 0; j < gridSizeY; j++)
		{
			this->CostGrid[i][j] = 50;
		}
	}


	this->GridSizeX = gridSizeX;
	this->GridSizeY = gridSizeY;

	this->DailyCosts = 0;

	for (int i = 0; i < 6; i++)
	{
		this->OpeningDays[i] = true;
	}
	this->OpeningDays[6] = false;

	for (int i = 0; i < 7; i++)
	{
		this->OpeningHour[i] = 8;
		this->ClosingHour[i] = 20;
	}

	this->HighestID = 0;
	
	this->PeepGenerator = PeopleGenerator(productList, pathfinder, texHandler, &this->Jobs);

	this->Spawn = false;

	this->ExitPosition = sf::Vector2i(this->Grid.size() - 1, this->Grid[0].size() / 2);

	this->TexHandler = texHandler;

	this->SpawnRate = 5.0f;

	this->SpawnTimer = 0.0f;

	this->Tile = texHandler->GetSprite("Tile")[0][0];

	this->DrugsDelivered = false;

	this->DrugHour = -1;

	this->Drug = "";
}


Store::~Store(void)
{
	std::list<Entity*> deletedList;
	std::list<Entity*>::iterator iter;

	for (int i = 0; i < this->GridSizeX; i++)
	{
		for (int j = 0; j < this->GridSizeY; j++)
		{
			while (!this->Grid[i][j].empty())
			{
				bool deleted = false;
				iter = deletedList.begin();
				while (iter != deletedList.end())
				{
					if (this->Grid[i][j].front() == *iter)
					{
						deleted = true;
					}
					iter++;
				}

				if (!deleted)
				{
					deletedList.push_back(this->Grid[i][j].front());
					delete this->Grid[i][j].front();
				}
				this->Grid[i][j].pop_front();
			}
		}
	}
}


void Store::UpdateBackground()
{
	sf::RenderTexture bgTarget;
	bgTarget.create(Constants::GridSize * this->GridSizeX + 100 * Constants::GridSize, Constants::GridSize / 2 * this->GridSizeY + 100 * Constants::GridSize / 2);
	sf::Sprite asphalt = sf::Sprite(*this->TexHandler->GetSprite("Asphalt2")[0][0]);
	sf::Sprite pavementStraight = sf::Sprite(*this->TexHandler->GetSprite("PavementStraight")[0][0]);
	pavementStraight.setOrigin(this->TexHandler->GetOrigins("PavementStraight")[0]);
	sf::Sprite pavementLeft = sf::Sprite(*this->TexHandler->GetSprite("PavementLeft")[0][0]);
	pavementLeft.setOrigin(this->TexHandler->GetOrigins("PavementLeft")[0]);
	sf::Sprite pavementRight = sf::Sprite(*this->TexHandler->GetSprite("PavementRight")[0][0]);
	pavementRight.setOrigin(this->TexHandler->GetOrigins("PavementRight")[0]);
	sf::Sprite pavementUp = sf::Sprite(*this->TexHandler->GetSprite("PavementUp")[0][0]);
	pavementUp.setOrigin(this->TexHandler->GetOrigins("PavementUp")[0]);
	sf::Sprite pavementDown = sf::Sprite(*this->TexHandler->GetSprite("PavementDown")[0][0]);
	pavementDown.setOrigin(this->TexHandler->GetOrigins("PavementDown")[0]);

	for (int x = -40; x < this->GridSizeX + 40; x++)
	{
		for (int y = -40; y < this->GridSizeY + 40; y++)
		{
			if (x < -1 || x > this->GridSizeX || y < -1 || y > this->GridSizeY)
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				asphalt.setPosition(screenPosX, screenPosY);
				bgTarget.draw(asphalt);
			}

			if (((x == -1 || x == this->GridSizeX) && (y >= 0 && y < this->GridSizeY)) ||
				((y == -1 || y == this->GridSizeY) && (x >= 0 && x < this->GridSizeX)))
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				pavementStraight.setPosition(screenPosX, screenPosY);
				bgTarget.draw(pavementStraight);
			}

			if (x == -1 && y == -1)
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				asphalt.setPosition(screenPosX, screenPosY);
				bgTarget.draw(asphalt);
				pavementUp.setPosition(screenPosX, screenPosY);
				bgTarget.draw(pavementUp);
			}

			if (x == this->GridSizeX && y == -1)
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				asphalt.setPosition(screenPosX, screenPosY);
				bgTarget.draw(asphalt);
				pavementRight.setPosition(screenPosX, screenPosY);
				bgTarget.draw(pavementRight);
			}

			if (x == -1 && y == this->GridSizeY)
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				asphalt.setPosition(screenPosX, screenPosY);
				bgTarget.draw(asphalt);
				pavementLeft.setPosition(screenPosX, screenPosY);
				bgTarget.draw(pavementLeft);
			}

			if (x == this->GridSizeX && y == this->GridSizeY)
			{
				float screenPosX = (x - y) * Constants::GridSize;
				float screenPosY = (x + y) * Constants::GridSize / 2;
				
				screenPosX += Constants::GridSize * this->GridSizeX / 2 + 50 * Constants::GridSize;
				screenPosY += (50 - this->GridSizeY / 2) * Constants::GridSize / 2;

				asphalt.setPosition(screenPosX, screenPosY);
				bgTarget.draw(asphalt);
				pavementDown.setPosition(screenPosX, screenPosY);
				bgTarget.draw(pavementDown);
			}
		}
	}

	bgTarget.display();

	this->ConcreteTexture = sf::Texture(bgTarget.getTexture());
	this->ConcreteSprite.setTexture(this->ConcreteTexture);
	this->ConcreteSprite.setOrigin(this->ConcreteSprite.getGlobalBounds().width / 2, this->ConcreteSprite.getGlobalBounds().height / 2);
	this->ConcreteSprite.setPosition((((float)this->GridSizeX / 2.0f - 0.5f) - ((float)this->GridSizeY / 2.0f + 0.5f)) * Constants::GridSize, (((float)this->GridSizeX / 2.0f - 0.5f) + ((float)this->GridSizeY / 2.0f + 0.5f)) * Constants::GridSize / 2);

}


bool compare(Entity* first, Entity* second)
{
	if (first->LowestScreenPosition.y == second->LowestScreenPosition.y)
	{
		if (first->Rotation % 2 == 0 && second->Rotation % 2 == 0)
		{
			return (first->LowestGridPosition.x < second->LowestGridPosition.x);
		}
		else
		{
			return (first->LowestGridPosition.y < second->LowestGridPosition.y);
		}
	}
	else
	{
		return (first->LowestScreenPosition.y < second->LowestScreenPosition.y);
	}
}

void Store::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{
	//update entities and put them into the drawlist
	this->DrawList.clear();


	StoreMessage tmp;
	tmp.Pointer = 0;
	bool request = false;

	if (this->Messages.size() > 0 && this->Messages.front().MessageType == StoreMessage::Type::RequestProductPos)
	{
		tmp.ID = this->Messages.front().ID;
		tmp.Position = sf::Vector2i(-1, -1);
		tmp.MessageType = StoreMessage::Type::SendPos;
		tmp.Pointer = 0;
		request = true;
	}

	if (this->Messages.size() > 0 && this->Messages.front().MessageType == StoreMessage::Type::RequestCheckoutPos)
	{
		tmp.ID = this->Messages.front().ID;
		tmp.Position = sf::Vector2i(-1, -1);
		tmp.MessageType = StoreMessage::Type::SendPos;
		tmp.Pointer = 0;
		request = true;
	}

	if (this->Messages.size() > 0 && this->Messages.front().MessageType == StoreMessage::Type::RequestExitPos)
	{
		tmp.ID = this->Messages.front().ID;
		tmp.Position = this->ExitPosition;
		tmp.MessageType = StoreMessage::Type::SendPos;
		tmp.Pointer = 0;
		request = true;
	}


	std::list<Entity*>::const_iterator iter;
	std::list<Entity*>::const_iterator end;

	std::vector<bool> processed;
	processed.resize(this->HighestID + 1);
	

	unsigned int objectCount = 0;
	unsigned int dirtyObjects = 0;

	for (int j = 0; j < this->GridSizeY; j++)
	{
		for (int i = 0; i < this->GridSizeX; i++)
		{
			iter = this->Grid[i][j].begin();
			end = this->Grid[i][j].end();
			while (iter != end)
			{
				if (!processed[(*iter)->ID])
				{
					(*iter)->update(elapsedTime, gameSpeed, gameData);
					processed[(*iter)->ID] = true;

					StoreObject* object = dynamic_cast<StoreObject*>(*iter);
					Customer* customer = dynamic_cast<Customer*>(*iter);
					

					if (object)
					{
						objectCount += 3;
						if (object->Dirtiness > 0)
						{
							dirtyObjects += object->Dirtiness;
							StoreJob job;
							job.JobType = StoreJob::Type::Cleaning;
							job.Object = object;
							job.ProductItem = 0;
							job.Taken = false;
							

							std::list<StoreJob>::iterator anotheriter = this->Jobs.begin();
							std::list<StoreJob>::iterator anotherend = this->Jobs.end();

							while (anotheriter != anotherend)
							{
								if (anotheriter->JobType == job.JobType && anotheriter->Object->ID == job.Object->ID)
								{
									break;
								}

								anotheriter++;
							}

							if (anotheriter == anotherend)
							{
								this->Jobs.push_back(job);
							}
						}

						if (this->Messages.size() > 0 && this->Messages.front().MessageType == StoreMessage::Type::RequestProductPos)
						{
							for (int k = 0; k < object->Slots.size(); k++)
							{
								if (object->Slots[k] && object->Slots[k]->Name == this->Messages.front().Name && object->Products[k].size() > 0)
								{
									sf::Vector2i pos = object->AccessPoints[k];
									
									tmp.ID = this->Messages.front().ID;
									tmp.MessageType = StoreMessage::Type::SendPos;
									tmp.Position = sf::Vector2i(object->GridPosition.x + pos.x, object->GridPosition.y + pos.y);
									tmp.Pointer = object;
									request = true;
								}
							}
						}

						if (this->Messages.size() > 0 && this->Messages.front().MessageType == StoreMessage::Type::RequestCheckoutPos)
						{
							if (object->ObjectType == StoreObject::Type::Checkout)
							{
								if (object->CustomerCount < 6 && (!tmp.Pointer || tmp.Pointer->CustomerCount > object->CustomerCount))
								{
									if (tmp.Pointer)
									{
										tmp.Pointer->CustomerCount--;
									}
									sf::Vector2i pos = object->AccessPoints.back();
									
									tmp.ID = this->Messages.front().ID;
									tmp.MessageType = StoreMessage::Type::SendPos;
									tmp.Position = sf::Vector2i(object->GridPosition.x + pos.x, object->GridPosition.y + pos.y);
									tmp.Pointer = object;
									object->CustomerCount++;
									request = true;
								}
							}
						}
					}

					if (customer)
					{
						if (customer->Object && customer->Object->ObjectType == StoreObject::Type::Checkout && !customer->Object->CurrentWorker)
						{
							StoreJob job;
							job.JobType = StoreJob::Type::Cashiering;
							job.Object = customer->Object;
							job.ProductItem = 0;

							std::list<StoreJob>::iterator anotheriter = this->Jobs.begin();
							std::list<StoreJob>::iterator anotherend = this->Jobs.end();

							while (anotheriter != anotherend)
							{
								if (anotheriter->JobType == job.JobType && anotheriter->Object == job.Object)
								{
									break;
								}

								anotheriter++;
							}

							if (anotheriter == anotherend)
							{
								this->Jobs.push_back(job);
							}
						}
					}


					DrawList.push_back(*iter);

					if ((*iter)->Moved)
					{
						if (std::floor((*iter)->GridPosition.x) < 0)
						{
							(*iter)->GridPosition.x = 0.0f;
						}
						if (std::ceil((*iter)->GridPosition.x) >= this->Grid.size())
						{
							(*iter)->GridPosition.x = this->Grid.size() - 1;
						}
						if (std::floor((*iter)->GridPosition.y) < 0)
						{
							(*iter)->GridPosition.y = 0.0f;
						}
						if (std::ceil((*iter)->GridPosition.y) >= this->Grid[0].size())
						{
							(*iter)->GridPosition.y = this->Grid[0].size() - 1;
						}

						int x;
						int y;
						if ((*iter)->GridPosition.x - (int)(*iter)->GridPosition.x >= 0.5f)
						{
							x = std::ceil((*iter)->GridPosition.x);
						}
						else
						{
							x = std::floor((*iter)->GridPosition.x);
						}
						if ((*iter)->GridPosition.y - (int)(*iter)->GridPosition.y >= 0.5f)
						{
							y = std::ceil((*iter)->GridPosition.y);
						}
						else
						{
							y = std::floor((*iter)->GridPosition.y);
						}

						this->Grid[x][y].push_back(*iter);
						(*iter)->PreviousGridPosition = sf::Vector2i(x, y);
						(*iter)->Moved = false;
						iter = this->Grid[i][j].erase(iter);
					}
					else if ((*iter)->DeleteMe)
					{
						if (dynamic_cast<Customer*>(*iter))
						{
							gameData->CurrentCustomers--;
						}
						this->FreeIDs.push_back((*iter)->ID);
						delete *iter;
						iter = this->Grid[i][j].erase(iter);
						this->DrawList.pop_back();

					}
					else
					{
						iter++;
					}
				}
				else
				{
					iter++;
				}
			}
		}
	}

	gameData->Hygiene = 1.0f - (float)dirtyObjects / (float)objectCount;


	if (request)
	{
		this->Messages.push_back(tmp);
		this->Messages.pop_front();
	}

	std::stable_sort(DrawList.begin(), DrawList.end(), compare);

	if (!this->DrugsDelivered && gameData->Time.Hour == this->DrugHour)
	{
		this->DrugsDelivered = true;

		Customer* tmp = this->PeepGenerator.GenerateDrugGuy(this->Drug);

		if (!this->FreeIDs.empty())
		{
			tmp->ID = this->FreeIDs.back();
			this->FreeIDs.pop_back();
		}
		else
		{
			tmp->ID = ++this->HighestID;
		}

		tmp->GridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
		tmp->LowestGridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
		tmp->PreviousGridPosition = this->ExitPosition;

		this->Grid[this->ExitPosition.x][this->ExitPosition.y].push_back(tmp);

		gameData->CurrentCustomers++;
	}


	//Spawn new guys
	this->SpawnTimer += elapsedTime.asSeconds() * gameSpeed;
	if (this->SpawnTimer >= this->SpawnRate + (float)(rand() % 101) / 100.0f && this->Spawn)
	{
		this->SpawnTimer = 0.0f;

		Customer* tmp = this->PeepGenerator.GenerateCustomer();
		if (!this->FreeIDs.empty())
		{
			tmp->ID = this->FreeIDs.back();
			this->FreeIDs.pop_back();
		}
		else
		{
			tmp->ID = ++this->HighestID;
		}

		tmp->GridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
		tmp->LowestGridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
		tmp->PreviousGridPosition = this->ExitPosition;

		this->Grid[this->ExitPosition.x][this->ExitPosition.y].push_back(tmp);

		gameData->CurrentCustomers++;

		if (rand() % 100 < 5)
		{
			Animal* animal = this->PeepGenerator.GenerateAnimal();

			if (!this->FreeIDs.empty())
			{
				animal->ID = this->FreeIDs.back();
				this->FreeIDs.pop_back();
			}
			else
			{
				animal->ID = ++this->HighestID;
			}

			animal->GridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
			animal->LowestGridPosition = sf::Vector2f(this->ExitPosition.x, this->ExitPosition.y);
			animal->PreviousGridPosition = this->ExitPosition;

			this->Grid[this->ExitPosition.x][this->ExitPosition.y].push_back(animal);
		}
	}
}

void Store::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(this->ConcreteSprite);

	//draw the grid
	sf::Vertex line[2];
	line[0].color = sf::Color(0, 0, 0, 128);
	line[1].color = sf::Color(0, 0, 0, 128);
	for (int i = 0; i <= this->GridSizeX; i++)
	{
		float screenPosX = (i - 0) * Constants::GridSize;
		float screenPosY = (i + 0) * Constants::GridSize / 2;
		line[0].position = sf::Vector2f(screenPosX, screenPosY);

		screenPosX = (i - this->GridSizeY) * Constants::GridSize;
		screenPosY = (i + this->GridSizeY) * Constants::GridSize / 2;
		line[1].position = sf::Vector2f(screenPosX, screenPosY);

		target.draw(line, 2, sf::Lines);
	}

	for (int j = 0; j <= this->GridSizeY; j++)
	{
		float screenPosX = (0 - j) * Constants::GridSize;
		float screenPosY = (0 + j) * Constants::GridSize / 2;
		line[0].position = sf::Vector2f(screenPosX, screenPosY);

		screenPosX = (this->GridSizeX - j) * Constants::GridSize;
		screenPosY = (this->GridSizeX + j) * Constants::GridSize / 2;
		line[1].position = sf::Vector2f(screenPosX, screenPosY);

		target.draw(line, 2, sf::Lines);
	}


	//draw the entities
	std::vector<Entity*>::const_iterator iter = this->DrawList.begin();
	std::vector<Entity*>::const_iterator enditer = this->DrawList.end();

	while (iter != enditer)
	{
		target.draw(**iter);
		iter++;
	}

	/*for (int j = 0; j < this->GridSizeY; j++)
	{
		for (int i = 0; i < this->GridSizeX; i++)
		{
			if (this->CostGrid[i][j] == 0)
			{
				float screenPosX = ((i - 0.5f) - (j + 0.5f)) * Constants::GridSize;
				float screenPosY = ((i - 0.5f) + (j + 0.5f)) * Constants::GridSize / 2;

				this->Tile->setPosition(screenPosX, screenPosY);
				this->Tile->setColor(sf::Color(255, 0, 0, 128));
				target.draw(*this->Tile);
			}
		}
	}*/
}