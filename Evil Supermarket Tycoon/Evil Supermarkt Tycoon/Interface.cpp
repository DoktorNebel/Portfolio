#include "Interface.h"


Interface::Interface()
{

}


Interface::Interface(sf::RenderWindow* window, TextureHandler* texHandler, std::vector<ProductListItem*>* productList, Storage* storage, std::list<StoreJob>* jobs, PeopleGenerator* peepGenerator, std::vector<Worker*>* workers, std::list<FeedMessage>* feed, std::vector<IllegalActivity>* illegalStuff)
{
	this->Windows.push_back(new BuildingWindow("Building", texHandler->GetSprite("Window")[0][0], sf::Vector2f(10.0f, (float)window->getSize().y - 300.0f), texHandler));
	this->Windows.push_back(new DataWindow("Data", texHandler->GetSprite("Window")[0][0], sf::Vector2f(100.0f, 10.0f), texHandler));
	this->Windows.push_back(new ProductsWindow("Products", texHandler->GetSprite("Window")[0][0], sf::Vector2f(100.0f, 100.0f), productList, texHandler, storage));
	this->Windows.push_back(new EntityWindow("Entities", texHandler->GetSprite("Window")[0][0], sf::Vector2f(10.0f, (float)window->getSize().y - 300.0f), texHandler, jobs));
	this->Windows.push_back(new WorkersWindow("Workers", texHandler->GetSprite("Window")[0][0], sf::Vector2f(100.0f, 100.0f), texHandler, workers));
	this->Windows.push_back(new HiringWindow("Hiring", texHandler->GetSprite("Window")[0][0], sf::Vector2f(200.0f, 150.0f), texHandler, peepGenerator));
	this->Windows.push_back(new FeedWindow("Feed", texHandler->GetSprite("Window")[0][0], sf::Vector2f(100.0f, 50.0f), texHandler, feed));
	this->Windows.push_back(new IllegalWindow("Illegal", texHandler->GetSprite("Window")[0][0], sf::Vector2f(100.0f, 100.0f), texHandler, illegalStuff));
	this->Windows.push_back(new PenaltyWindow("Penalty", texHandler->GetSprite("Window")[0][0], sf::Vector2f(430.0f, 350.0f), texHandler));

	this->TexHandler = texHandler;

	this->SelectedEntity = 0;

	this->SelectedProduct = 0;

	this->SelectedSprite = 0;

	this->ProductStorage = storage;

	this->BuildMode = false;

	this->Jobs = jobs;

	this->PrevDay = 0;

	this->State = 2;

	this->ProdsOpen = false;

	this->StorOpen = false;
}


Interface::~Interface(void)
{
	/*while (!this->Windows.empty())
	{
		delete this->Windows.front();
		this->Windows.pop_front();
	}*/
}



void Interface::update(Input* input, GameData* gameData, std::vector<std::vector<std::list<Entity*>>>* grid, std::deque<unsigned int>* freeIDs, unsigned int* highestID)
{
	//update Windows
	std::list<InterfaceWindow*>::iterator iter = this->Windows.begin();
	std::list<InterfaceWindow*>::iterator end = this->Windows.end();
	std::list<InterfaceMessage>::iterator anotheriter;
	std::list<InterfaceMessage>::iterator anotherend;

	while (iter != end)
	{
		anotheriter = this->Messages.begin();
		anotherend = this->Messages.end();

		while (anotheriter != anotherend)
		{
			if (anotheriter->MessageType == InterfaceMessage::Type::OpenWindow)
			{
				if ((*iter)->Name == anotheriter->Text || (*iter)->Name == "Products" && anotheriter->Text == "Storage")
				{
					if (this->PrevDay != gameData->Date.Day && anotheriter->Text == "Hiring")
					{
						dynamic_cast<HiringWindow*>(*iter)->GenerateWorkers();
						this->PrevDay = gameData->Date.Day;
					}

					if ((*iter)->Name != "Penalty" || ((*iter)->Name == "Penalty" && !(*iter)->isOpen()))
					{
						(*iter)->open();
					}

					if (anotheriter->Text == "Building")
					{
						if (gameData->Prestige >= 300)
						{
							if (gameData->Prestige >= 1500)
							{
								dynamic_cast<BuildingWindow*>(*iter)->filterButtons("Level 3");
							}
							else
							{
								dynamic_cast<BuildingWindow*>(*iter)->filterButtons("Level 2");
							}
						}
						else
						{
							dynamic_cast<BuildingWindow*>(*iter)->filterButtons("Level 1");
						}
					}

					if (anotheriter->Text == "Storage")
					{
						this->StorOpen = true;
						dynamic_cast<ProductsWindow*>(*iter)->StorageOnly = true;
						dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Storage");
						if (dynamic_cast<StoreObject*>(this->SelectedEntity))
						{
							if (dynamic_cast<StoreObject*>(this->SelectedEntity)->ObjectType == StoreObject::Type::Shelf)
							{
								dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Shelf");
							}
						}
					}

					if (anotheriter->Text == "Products")
					{
						this->ProdsOpen = true;
						dynamic_cast<ProductsWindow*>(*iter)->StorageOnly = false;
						if (gameData->Prestige >= 300)
						{
							if (gameData->Prestige >= 1500)
							{
								dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Level 3");
							}
							else
							{
								dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Level 2");
							}
						}
						else
						{
							dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Level 1");
						}
						if (dynamic_cast<StoreObject*>(this->SelectedEntity))
						{
							if (dynamic_cast<StoreObject*>(this->SelectedEntity)->ObjectType == StoreObject::Type::Shelf)
							{
								dynamic_cast<ProductsWindow*>(*iter)->filterButtons("Shelf");
							}
						}
					}

					this->Messages.erase(anotheriter);
					break;
				}
			}
			else if (anotheriter->MessageType == InterfaceMessage::Type::CloseWindow)
			{
				if ((*iter)->Name == anotheriter->Text || (anotheriter->Text == "Storage" && (*iter)->Name == "Products"))
				{
					(*iter)->close();

					if (anotheriter->Text == "Products")
					{
						this->StorOpen = false;
						this->ProdsOpen = false;
					}

					this->Messages.erase(anotheriter);
					break;
				}
			}
			else if (anotheriter->MessageType == InterfaceMessage::Type::SetSelectedEntity)
			{
				if (anotheriter->Text == "Interface" && (*iter)->Name == "Workers")
				{
					if (this->SelectedEntity != 0)
					{
						this->SelectedEntity->setColor(sf::Color::White);
					}
					this->SelectedEntity = dynamic_cast<WorkersWindow*>(*iter)->SelectedWorker;
					this->SelectedEntity->setColor(sf::Color::Green);
					anotheriter->Text = "Entities";

					InterfaceMessage tmp;
					tmp.MessageType = InterfaceMessage::Type::OpenWindow;
					tmp.Text = "Entities";
					this->Messages.push_back(InterfaceMessage(tmp));
				}

				if (anotheriter->Text == "Entities" && (*iter)->Name == "Entities")
				{
					dynamic_cast<EntityWindow*>(*iter)->SelectedEntity = this->SelectedEntity;
					dynamic_cast<EntityWindow*>(*iter)->EntityChanged = true;
					this->Messages.erase(anotheriter);
					break;
				}
			}
			else if (anotheriter->MessageType == InterfaceMessage::Type::HireWorker)
			{
				if ((*iter)->Name == anotheriter->Text)
				{
					int x;
					int y;
					int i = -1;
					do
					{
						x = rand() % grid->size();
						y = rand() % (*grid)[0].size();
						i++;
					} while (i < 100 && ((*grid)[x][y].size() > 1 || (*grid)[x][y].empty() || (dynamic_cast<StoreObject*>((*grid)[x][y].front()) && dynamic_cast<StoreObject*>((*grid)[x][y].front())->ObjectQuality == StoreObject::Quality::Nothing)));

					while ((*grid)[x][y].size() > 1 || (*grid)[x][y].empty())
					{
						x = rand() % grid->size();
						y = rand() % (*grid)[0].size();
					}

					((Entity*)anotheriter->Pointer)->GridPosition = sf::Vector2f((float)x, (float)y);
					((Entity*)anotheriter->Pointer)->LowestGridPosition = sf::Vector2f((float)x, (float)y);
					((Entity*)anotheriter->Pointer)->PreviousGridPosition = sf::Vector2i(x, y);

					if (!freeIDs->empty())
					{
						((Entity*)anotheriter->Pointer)->ID = freeIDs->back();
						freeIDs->pop_back();
					}
					else
					{
						((Entity*)anotheriter->Pointer)->ID = ++(*highestID);
					}

					(*grid)[x][y].push_back((Entity*)anotheriter->Pointer);

					dynamic_cast<WorkersWindow*>(*iter)->AddWorker((Worker*)anotheriter->Pointer);
					this->Messages.erase(anotheriter);
					break;
				}
			}
			else if (anotheriter->MessageType == InterfaceMessage::Type::SetPenalty)
			{
				if ((*iter)->Name == "Penalty" && !(*iter)->isOpen())
				{
					std::wstring tmp(anotheriter->Text.begin(), anotheriter->Text.end());
					tmp.replace(tmp.find(L"Euro"), 4, L"€");
					while (tmp.find(L"ae") != std::wstring::npos)
					{
						tmp.replace(tmp.find(L"ae"), 2, L"ä");
					}
					while (tmp.find(L"oe") != std::wstring::npos)
					{
						tmp.replace(tmp.find(L"oe"), 2, L"ö");
					}
					while (tmp.find(L"ue") != std::wstring::npos)
					{
						tmp.replace(tmp.find(L"ue"), 2, L"ü");
					}

					std::wstring::iterator stringiter = tmp.begin();
					std::wstring::iterator stringend = tmp.end();
					int i = 0;

					while (stringiter != stringend)
					{
						if (*stringiter == ' ')
						{
							i++;
						}

						if (i == 7)
						{
							i = 0;

							stringiter++;
							tmp.insert(stringiter, '\n');
						}

						stringiter++;
					}

					dynamic_cast<PenaltyWindow*>(*iter)->Message.setString(tmp);

					this->Messages.erase(anotheriter);
					break;
				}
			}

			anotheriter++;
		}



		if ((*iter)->isOpen())
		{
			(*iter)->update(input, gameData, &this->Messages);

			if ((*iter)->Name == "Products")
			{
				if (input->JustClicked(sf::Mouse::Left))
				{
					ProductListItem* tmp = dynamic_cast<ProductsWindow*>(*iter)->CurrentItem;
					if (tmp != 0)
					{
						this->SelectedProduct = tmp;
						this->SelectedSprite = this->TexHandler->GetSprite(this->SelectedProduct->Name)[0][0];
						this->SelectedSprite->scale(3, 3);
					}
				}
			}

			if ((*iter)->Name == "Entities")
			{
				if (input->WasClicked(sf::Mouse::Left) && this->SelectedProduct != 0 && this->ProductStorage->countProduct(this->SelectedProduct->Name))
				{
					dynamic_cast<EntityWindow*>(*iter)->PutObject(this->SelectedProduct, this->SelectedSprite, input, gameData, this->ProductStorage, this->Jobs);
				}
			}


			if ((*iter)->Name == "Data")
			{
				dynamic_cast<DataWindow*>(*iter)->State = this->State;
			}

		}


		if ((*iter)->Name == "Building")
		{
			if ((*iter)->isOpen())
			{
				this->BuildMode = true;
				if (this->SelectedEntity != 0)
				{
					this->SelectedEntity->Sprite[0][0]->setColor(sf::Color::White);
				}
				this->SelectedEntity = 0;

				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::SetSelectedEntity;
				tmp.Text = "Entities";
				this->Messages.push_back(InterfaceMessage(tmp));
			}
			else
			{
				this->BuildMode = false;
			}
		}


		iter++;
	}


	//Product Drag and drop stuff
	if (input->IsClicked(sf::Mouse::Left) && this->SelectedSprite != 0)
	{
		this->SelectedSprite->setPosition(input->GetMousePos().x - this->SelectedSprite->getGlobalBounds().width / 2, input->GetMousePos().y - this->SelectedSprite->getGlobalBounds().height / 2);
	}
	if (input->WasClicked(sf::Mouse::Left) && this->SelectedSprite != 0)
	{
		delete this->SelectedSprite;
		this->SelectedProduct = 0;
		this->SelectedSprite = 0;
	}


	//check if Object is selected
	if (!this->BuildMode && input->JustClicked(sf::Mouse::Left) && !this->containsMouse(input))
	{
		sf::Vector2i gridPos = input->GetGridPos();

		bool something = false;

		for (int i = 0; i < 4; i++)
		{
			if (gridPos.x + i > 0 && gridPos.x + i < (int)(*grid).size() &&
				gridPos.y + i > 0 && gridPos.y + i < (int)(*grid)[0].size())
			{
				std::list<Entity*>::iterator anotheriter = (*grid)[gridPos.x + i][gridPos.y + i].begin();
				std::list<Entity*>::iterator anotherend = (*grid)[gridPos.x + i][gridPos.y + i].end();

				while (anotheriter != anotherend)
				{
					if (dynamic_cast<StoreObject*>(*anotheriter) == 0 ||
						(dynamic_cast<StoreObject*>(*anotheriter)->ObjectType != StoreObject::Type::Floor &&
						dynamic_cast<StoreObject*>(*anotheriter)->ObjectType != StoreObject::Type::Wall))
					{
						something = true;

						if (this->SelectedEntity != 0)
						{
							this->SelectedEntity->setColor(sf::Color::White);
						}

						InterfaceMessage tmp;

						if (dynamic_cast<Customer*>(*anotheriter) && dynamic_cast<Customer*>(*anotheriter)->ShoppingState == Customer::ShopState::Stealing)
						{
							gameData->Prestige += 3;
							double money = 0.0;
							for (unsigned int i = 0; i < (*anotheriter)->Products.size(); i++)
							{
								if ((*anotheriter)->Products[i].size() > 0)
								{
									money += (*anotheriter)->Products[i].front().Price * 2.0;
								}
							}
							gameData->CurrentMoney += money;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							std::stringstream sstream;
							sstream << "Sie haben den Dieb gefasst. Sie erhalten " << std::fixed << std::setprecision(2) << money << "Euro";
							tmp.Text = sstream.str();

							this->Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							this->Messages.push_back(InterfaceMessage(tmp));

							(*anotheriter)->DeleteMe = true;
						}
						else
						{
							this->SelectedEntity = *anotheriter;
							this->SelectedEntity->setColor(sf::Color::Green);


							if (this->ProdsOpen)
							{
								tmp.MessageType = InterfaceMessage::Type::OpenWindow;
								tmp.Text = "Products";
								this->Messages.push_back(InterfaceMessage(tmp));
							}
							if (this->StorOpen)
							{
								tmp.MessageType = InterfaceMessage::Type::OpenWindow;
								tmp.Text = "Storage";
								this->Messages.push_back(InterfaceMessage(tmp));
							}

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Entities";
							this->Messages.push_back(InterfaceMessage(tmp));

							tmp.MessageType = InterfaceMessage::Type::SetSelectedEntity;
							tmp.Text = "Entities";
							this->Messages.push_back(InterfaceMessage(tmp));
						}
					}

					anotheriter++;
				}
			}
		}

		if (!something)
		{
			if (this->SelectedEntity != 0)
			{
				this->SelectedEntity->setColor(sf::Color::White);
				this->SelectedEntity = 0;

				InterfaceMessage tmp;

				if (this->ProdsOpen)
				{
					tmp.MessageType = InterfaceMessage::Type::OpenWindow;
					tmp.Text = "Products";
					this->Messages.push_back(InterfaceMessage(tmp));
				}
				if (this->StorOpen)
				{
					tmp.MessageType = InterfaceMessage::Type::OpenWindow;
					tmp.Text = "Storage";
					this->Messages.push_back(InterfaceMessage(tmp));
				}

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Entities";
				this->Messages.push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::SetSelectedEntity;
				tmp.Text = "Entities";
				this->Messages.push_back(InterfaceMessage(tmp));
			}
		}
	}

	if (dynamic_cast<Customer*>(this->SelectedEntity) && dynamic_cast<Customer*>(this->SelectedEntity)->Deleted)
	{
		this->SelectedEntity = 0;
	}
	if (dynamic_cast<Animal*>(this->SelectedEntity) && dynamic_cast<Animal*>(this->SelectedEntity)->ToBeDeleted)
	{
		this->SelectedEntity = 0;
	}
}


void Interface::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	std::list<InterfaceWindow*>::const_iterator iter = this->Windows.begin();
	std::list<InterfaceWindow*>::const_iterator end = this->Windows.end();

	while (iter != end)
	{
		if ((*iter)->isOpen())
		{
			target.draw(**iter);
		}
		iter++;
	}

	if (this->SelectedSprite != 0)
	{
		target.draw(*this->SelectedSprite);
	}
}


std::string Interface::getSelectedStoreObject()
{
	std::list<InterfaceWindow*>::iterator iter = this->Windows.begin();
	std::list<InterfaceWindow*>::iterator end = this->Windows.end();

	while (iter != end)
	{
		if ((*iter)->Name == "Building")
		{
			return (*iter)->getSelected();
		}
		iter++;
	}

	return "";
}


bool Interface::containsMouse(Input* input)
{
	sf::Vector2i mousePos = input->GetMousePos();

	std::list<InterfaceWindow*>::iterator iter = this->Windows.begin();
	std::list<InterfaceWindow*>::iterator end = this->Windows.end();

	while (iter != end)
	{
		if ((*iter)->isOpen() && (*iter)->Sprite->getGlobalBounds().contains((float)mousePos.x, (float)mousePos.y))
		{
			return true;
		}
		iter++;
	}

	return false;
}