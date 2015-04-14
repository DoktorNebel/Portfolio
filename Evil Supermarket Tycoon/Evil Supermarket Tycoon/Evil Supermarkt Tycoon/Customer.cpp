#include "Customer.h"


Customer::Customer(void)
{
	this->Rotation = 2;
	this->Moved = false;
	this->PosRequested = false;
	this->PathRequested = false;
	this->CheckedOut = false;
	this->NextProduct = -1;
	this->DeleteMe = false;
	this->Object = 0;
	this->Deleted = false;
	this->PersonState = Customer::State::Nothing;
	this->ShoppingState = Customer::ShopState::Buying;
	this->AnimPhase = 0;
	this->Waited = false;
	this->InLine = false;
	this->WaitTimer = 0;
	this->ShowEmotion = false;
	this->EmoticonTimer = 0.0f;
	this->EmotionState = 0;
	this->AnimState = 1;
	this->AnimPhase = 0;
	this->AnimTimer = 0.0f;
}


Customer::~Customer(void)
{
}


void Customer::update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData)
{
	StoreMessage tmp;
	sf::Vector2i objPos;
	std::list<Entity*>::iterator iter;
	std::list<Entity*>::iterator end;
	sf::Vector2i checkPos;

	switch (this->PersonState)
	{
	case Customer::State::Walking:
		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0.0f)
		{
			if (this->Path.empty())
			{
				switch (this->ShoppingState)
				{
				case Customer::ShopState::Buying:
					checkPos = sf::Vector2i(this->GridPosition.x - 0.5, this->GridPosition.y + 0.5);
					if (checkPos.x >= 0 && checkPos.x < this->Grid->size() &&
						checkPos.y >= 0 && checkPos.y < (*this->Grid)[0].size())
					{
						iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
						end = (*this->Grid)[checkPos.x][checkPos.y].end();

						while (iter != end)
						{
							if (dynamic_cast<StoreObject*>(*iter) == this->Object)
							{
								objPos = checkPos;
							}
							iter++;
						}
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 1.5, this->GridPosition.y + 0.5);
					if (checkPos.x >= 0 && checkPos.x < this->Grid->size() &&
						checkPos.y >= 0 && checkPos.y < (*this->Grid)[0].size())
					{
						iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
						end = (*this->Grid)[checkPos.x][checkPos.y].end();

						while (iter != end)
						{
							if (dynamic_cast<StoreObject*>(*iter) == this->Object)
							{
								objPos = checkPos;
							}
							iter++;
						}
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 0.5, this->GridPosition.y - 0.5);
					if (checkPos.x >= 0 && checkPos.x < this->Grid->size() &&
						checkPos.y >= 0 && checkPos.y < (*this->Grid)[0].size())
					{
						iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
						end = (*this->Grid)[checkPos.x][checkPos.y].end();

						while (iter != end)
						{
							if (dynamic_cast<StoreObject*>(*iter) == this->Object)
							{
								objPos = checkPos;
							}
							iter++;
						}
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 0.5, this->GridPosition.y + 1.5);
					if (checkPos.x >= 0 && checkPos.x < this->Grid->size() &&
						checkPos.y >= 0 && checkPos.y < (*this->Grid)[0].size())
					{
						iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
						end = (*this->Grid)[checkPos.x][checkPos.y].end();

						while (iter != end)
						{
							if (dynamic_cast<StoreObject*>(*iter) == this->Object)
							{
								objPos = checkPos;
							}
							iter++;
						}
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

					this->PersonState = Customer::State::TakingProduct;
					this->WaitTimer = 1.0f;
					break;

				case Customer::ShopState::CheckingOut:
					this->PersonState = Customer::State::StandingInLine;
					this->WaitTimer = 0.0f;
					break;

				case Customer::ShopState::Leaving:
					this->ShoppingState = Customer::ShopState::Left;
					this->DeleteMe = true;
					break;

				case Customer::ShopState::Stealing:
					this->ShoppingState = Customer::ShopState::Left;
					this->DeleteMe = true;
					break;
				}
			}
			else
			{
				int difX = 0;
				int difY = 0;

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
				if (abs(this->GridPosition.x - (float)this->PreviousGridPosition.x) > 0.5f || abs(this->GridPosition.y - (float)this->PreviousGridPosition.y) > 0.5f)
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
						if (this->Path.size() <= 1 && this->ShoppingState == Customer::ShopState::CheckingOut)
						{
							bool walk = true;

							iter = (*this->Grid)[this->GridPosition.x + 0.5][this->GridPosition.y + 0.5].begin();
							end = (*this->Grid)[this->GridPosition.x + 0.5][this->GridPosition.y + 0.5].end();

							while (iter != end)
							{
								if (*iter != this && dynamic_cast<Customer*>(*iter) && dynamic_cast<Customer*>(*iter)->WaitTimer <= 0.0f && !dynamic_cast<Customer*>(*iter)->Waited)
								{
									walk = false;
								}

								iter++;
							}

							if (!walk)
							{
								this->WaitTimer = 1.5f;
								this->Waited = true;
							}
							else
							{
								this->Waited = false;
							}
						}
						if (this->Path.size() == 1 && (this->ShoppingState == Customer::ShopState::Leaving || this->ShoppingState == Customer::ShopState::Stealing))
						{
							this->Deleted = true;
						}
					}
				}
			}
		}
		break;


	case Customer::State::Nothing:
		switch (this->ShoppingState)
		{
		case Customer::ShopState::Buying:
			this->NextProduct++;

			if (this->NextProduct < this->ShoppingList.size())
			{
				tmp.ID = this->ID;
				tmp.MessageType = StoreMessage::Type::RequestProductPos;
				tmp.Name = this->ShoppingList[this->NextProduct]->Name;
				this->Messages->push_back(tmp);
				this->PersonState = Customer::State::WaitingForPos;
			}
			else
			{
				if (rand() % 1000 < this->StealingPotential * 1000.0f)
				{
					this->ShoppingState = Customer::ShopState::Stealing;
					this->Speed *= 2.0f;
				}
				else
				{
					this->ShoppingState = Customer::ShopState::CheckingOut;
				}
			}
			break;


		case Customer::ShopState::CheckingOut:
			if (this->Products.empty())
			{
				this->ShoppingState = Customer::ShopState::Leaving;
			}
			else
			{
				for (int i = 0; i < this->Products.size(); i++)
				{
					if (this->ProductsFound[i])
					{
						tmp.ID = this->ID;
						tmp.MessageType = StoreMessage::Type::RequestCheckoutPos;
						this->Messages->push_back(tmp);
						this->PersonState = Customer::State::WaitingForPos;
						this->ShoppingState = Customer::ShopState::CheckingOut;
						break;
					}
					else
					{
						this->ShoppingState = Customer::ShopState::Leaving;
					}
				}
			}
			break;


		case Customer::ShopState::Leaving:
			tmp.ID = this->ID;
			tmp.MessageType = StoreMessage::Type::RequestExitPos;
			this->Messages->push_back(tmp);
			this->PersonState = Customer::State::WaitingForPos;
			break;


		case Customer::ShopState::Stealing:
			tmp.ID = this->ID;
			tmp.MessageType = StoreMessage::Type::RequestExitPos;
			this->Messages->push_back(tmp);
			this->PersonState = Customer::State::WaitingForPos;
			break;
		}
		break;


	case Customer::State::WaitingForPos:
		if (this->Messages->front().MessageType == StoreMessage::Type::SendPos && this->Messages->front().ID == this->ID)
		{
			if (this->Messages->front().Position != sf::Vector2i(-1, -1))
			{
				this->ThePathfinder->RequestPath(sf::Vector2i(this->GridPosition.x + 0.5, this->GridPosition.y + 0.5), this->Messages->front().Position, this->ID);
				this->PersonState = Customer::State::WaitingForPath;
				this->Object = this->Messages->front().Pointer;

				if (this->ShoppingState == Customer::ShopState::CheckingOut)
				{
					if (this->Object)
					{
						this->LinePosition = this->Object->AccessPoints.size() - 1;
					}
				}
			}
			else
			{
				if (this->NextProduct < this->ShoppingList.size())
				{
					this->Happiness -= 0.1f;
				}
				if ((*gameData->IllegalStuff)[1].Active)
				{
					this->Happiness = 1.0f;
				}
				this->PersonState = Customer::State::Nothing;
				if (this->ShoppingState == Customer::ShopState::Buying)
				{
					FeedMessage tmp;
					tmp.Pointer = 0;
					tmp.Message = "Im Laden gibt es kein " + this->ShoppingList[this->NextProduct]->Name + ".";

					this->Feed->push_front(tmp);
				}

				if (this->ShoppingState == Customer::ShopState::CheckingOut)
				{
					FeedMessage tmp;
					tmp.Pointer = 0;
					tmp.Message = "Es befindet sich keine Kasse im Laden.";

					this->Feed->push_front(tmp);

					this->ShoppingState = Customer::ShopState::Leaving;
				}
			}
			this->Messages->pop_front();
		}
		break;


	case Customer::State::WaitingForPath:
		this->Path = this->ThePathfinder->GetPath(this->ID);
		if (!this->Path.empty())
		{
			this->PersonState = Customer::State::Walking;
		}
		break;


	case Customer::State::TakingProduct:
		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			this->ProductsFound[this->NextProduct] = false;

			if (this->Object != 0)
			{
				for (int i = 0; i < this->Object->Products.size(); i++)
				{
					if (this->Object->Products[i].size() > 0 && this->Object->Products[i].front().Name == this->ShoppingList[this->NextProduct]->Name)
					{
						bool expired = false;
						if (this->Object->Products[i].front().ExpirationDate.Day < gameData->Date.Day)
						{
							expired = true;
						}
						if (this->Object->Products[i].front().ExpirationDate.Month < gameData->Date.Month)
						{
							expired = true;
						}
						else if (this->Object->Products[i].front().ExpirationDate.Month > gameData->Date.Month)
						{
							expired = false;
						}
						if (this->Object->Products[i].front().ExpirationDate.Year < gameData->Date.Year)
						{
							expired = true;
						}
						else if (this->Object->Products[i].front().ExpirationDate.Year > gameData->Date.Year)
						{
							expired = false;
						}

						if (expired)
						{
							this->Happiness -= 0.1f;

							FeedMessage tmp;
							tmp.Pointer = this->Object;
							tmp.Message = this->ShoppingList[this->NextProduct]->Name + " ist abgelaufen.";

							this->Feed->push_front(tmp);
						}
						else if (this->ShoppingList[this->NextProduct]->Subcategory == "Alkoholische Getränke" && (this->Class == L"Kind" || this->Class == L"Jugendlich") && !(*gameData->IllegalStuff)[0].Active)
						{
							this->Happiness -= 0.1f;

							FeedMessage tmp;
							tmp.Pointer = this;
							tmp.Message = std::string(this->Name.begin(), this->Name.end()) + " ist todtraurig, dass er keinen Alkohol kaufen darf.";

							this->Feed->push_front(tmp);
						}
						else if (this->Object->Products[i].front().Price <= this->PriceExpectancy[this->NextProduct])
						{
							this->Products[this->NextProduct].push_back(this->Object->Products[i].front());
							this->Products[this->NextProduct].back().Amount = 1;
							this->ProductsFound[this->NextProduct] = true;
							this->Happiness += 0.1f;
							this->Object->Products[i].front().Amount--;

							int j = 0;
							int k = 100;
							if (this->Object->ObjectType == StoreObject::Type::Freezer)
							{
								while (!this->Object->ShownProducts[i][j] && k > 0)
								{
									j++;
									k--;
								}
							}
							else
							{
								j = rand() % this->Object->ShownProducts[i].size();
								while (!this->Object->ShownProducts[i][j] && k > 0)
								{
									j = rand() % this->Object->ShownProducts[i].size();
									k--;
								}
							}

							this->Object->ShownProducts[i][j] = false;

							if (this->Object->Products[i].front().Amount == 0 || this->Object->Products[i].front().Amount > 65000)
							{
								this->Object->Products[i].pop_front();
							}
							break;
						}
						else
						{
							this->Happiness -= 0.1f;

							FeedMessage tmp;
							tmp.Pointer = this->Object;
							tmp.Message = std::string(this->Name.begin(), this->Name.end()) + " ist " + this->ShoppingList[this->NextProduct]->Name + " zu teuer.";

							this->Feed->push_front(tmp);
						}
					}
					else if (this->Object->Slots[i] && this->Object->Slots[i]->Name == this->ShoppingList[this->NextProduct]->Name)
					{
						this->Happiness -= 0.1f;

						FeedMessage tmp;
						tmp.Pointer = this->Object;
						tmp.Message = "Im Regal ist kein " + this->ShoppingList[this->NextProduct]->Name + " mehr.";

						this->Feed->push_front(tmp);
					}
				}
			}

			if ((*gameData->IllegalStuff)[1].Active)
			{
				this->Happiness = 1.0f;
			}

			this->PersonState = Customer::State::Nothing;
		}
		break;


	case Customer::State::Paying:
		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			gameData->SoundHandler->StopSound("Scanning");
			gameData->SoundHandler->PlaySound("Cash");

			this->Happiness = 0.0f;
			for (int i = 0; i < this->Products.size(); i++)
			{
				if (this->ProductsFound[i])
				{
					if (this->Happiness * 10 < this->ExpectedProducts)
					{
						this->Happiness += 0.1f;
					}
					this->Money -= this->Products[i].front().Price;
					gameData->CurrentMoney += this->Products[i].front().Price;
					if (this->Products[i].front().Quality == ProductQuality::Illegal)
					{
						(*gameData->IllegalStuff)[3].WasActive = true;
					}
					if (this->Products[i].front().ExpirationState != Product::State::Good)
					{
						(*gameData->IllegalStuff)[4].WasActive = true;
					}
				}
			}
			this->Happiness += std::min(this->HygieneExpectation, gameData->Hygiene);
			if ((*gameData->IllegalStuff)[1].Active)
			{
				this->Happiness = 1.0f;
			}

			if (this->Happiness >= 0.8f)
			{
				gameData->Prestige += 2;
			}
			else if (this->Happiness >= 0.6f)
			{
				gameData->Prestige += 1;
			}

			this->PersonState = Customer::State::Nothing;
			this->ShoppingState = Customer::ShopState::Leaving;
			this->Object->CustomerCount--;

			this->ShowEmotion = true;
		}
		break;


	case Customer::State::StandingInLine:
		this->WaitTimer -= elapsedTime.asSeconds() * gameSpeed;

		if (this->WaitTimer <= 0)
		{
			if (this->LinePosition == 0)
			{
				if (this->Object->CurrentWorker != 0 && this->Object->CurrentWorker->AtWorkplace)
				{
					gameData->SoundHandler->PlaySound("Scanning");

					this->PersonState = Customer::State::Paying;
					this->WaitTimer = 5.0f + 2.0f * ((1.0f - this->Object->CurrentWorker->CashieringEfficiency) / 5.0f) / std::max(0.3f, this->Object->CurrentWorker->Happiness) / this->Speed;
					this->Object->CurrentWorker->WorkState = Worker::State::Cashiering;
					this->Object->CurrentWorker->WaitTimer = this->WaitTimer * 2.0f;;


					checkPos = sf::Vector2i(this->GridPosition.x - 0.5, this->GridPosition.y + 0.5);
					iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					end = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter) == this->Object)
						{
							objPos = checkPos;
						}
						iter++;
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 1.5, this->GridPosition.y + 0.5);
					iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					end = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter) == this->Object)
						{
							objPos = checkPos;
						}
						iter++;
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 0.5, this->GridPosition.y - 0.5);
					iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					end = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter) == this->Object)
						{
							objPos = checkPos;
						}
						iter++;
					}

					checkPos = sf::Vector2i(this->GridPosition.x + 0.5, this->GridPosition.y + 1.5);
					iter = (*this->Grid)[checkPos.x][checkPos.y].begin();
					end = (*this->Grid)[checkPos.x][checkPos.y].end();

					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter) == this->Object)
						{
							objPos = checkPos;
						}
						iter++;
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
				}
				else
				{
					this->WaitTimer = 0.5f;
				}
			}
			else
			{
				bool occupied = false;

				iter = (*this->Grid)[this->Object->GridPosition.x + this->Object->AccessPoints[this->LinePosition - 1].x][this->Object->GridPosition.y + this->Object->AccessPoints[this->LinePosition - 1].y].begin();
				end = (*this->Grid)[this->Object->GridPosition.x + this->Object->AccessPoints[this->LinePosition - 1].x][this->Object->GridPosition.y + this->Object->AccessPoints[this->LinePosition - 1].y].end();

				while (iter != end)
				{
					if (dynamic_cast<Customer*>(*iter))
					{
						occupied = true;
					}

					iter++;
				}

				if (occupied)
				{
					this->WaitTimer = 0.5f;
				}
				else
				{
					this->Path.push_back(sf::Vector2i(this->Object->GridPosition.x + this->Object->AccessPoints[this->LinePosition - 1].x, this->Object->GridPosition.y + this->Object->AccessPoints[this->LinePosition - 1].y));

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

					this->PersonState = Customer::State::Walking;
					this->LinePosition--;
				}
			}
		}
		break;
	}

	if (this->PersonState == Customer::State::Walking)
	{
		this->AnimState = 0;
	}
	else
	{
		this->AnimState = 1;
		this->AnimPhase = 0;
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

	this->LowestScreenPosition = sf::Vector2f(screenPosX - 0.3f * Constants::GridSize, screenPosY - 0.3f * Constants::GridSize);


	if (this->ShowEmotion)
	{
		this->UpdateEmoticon(elapsedTime, gameSpeed);
	}
}


void Customer::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->Rotation < 2)
	{
		target.draw(*this->Sprite[this->AnimState][this->AnimPhase]);
	}
	else
	{
		target.draw(*this->BackSprite[this->AnimState][this->AnimPhase]);
	}

	if (this->ShowEmotion)
	{
		target.draw(*this->Emoticons[0][this->EmotionState]);
	}
}