#include "Game.h"


Game::Game(sf::Vector2f screenSize)
{
	this->Loaded = false;
	this->GameState = Game::State::Intro;
	this->PreviousState = Game::State::Intro;
	this->Speed = 1;
	this->TexHandler.LoadTextures();
	this->Bob = Builder(&this->TexHandler);
	this->Bob.Feed = &this->Feed;

	this->loadProducts();

	IllegalActivity tmp;
	tmp.Active = false;
	tmp.WasActive = false;
	tmp.Name = L"Alkohol an Minderjährige verkaufen";
	tmp.Description = L"";
	tmp.PenaltyProbability = 0.5f;

	this->IllegalStuff.push_back(tmp);

	tmp.Active = false;
	tmp.WasActive = false;
	tmp.Name = L"Lachgas";
	tmp.Description = L"Kostet 200€ pro Stunde. Sorgt für volle Kundenzufriedenheit.";
	tmp.PenaltyProbability = 0.4f;

	this->IllegalStuff.push_back(tmp);

	tmp.Active = false;
	tmp.WasActive = false;
	tmp.Name = L"Unterschwellige Werbung";
	tmp.Description = L"Kostet 150€ pro Tag. Sorgt für 30% mehr Kunden.";
	tmp.PenaltyProbability = 0.4f;

	this->IllegalStuff.push_back(tmp);

	tmp.Active = false;
	tmp.WasActive = false;
	tmp.Name = L"Illegale Produkte";
	tmp.Description = L"";
	tmp.PenaltyProbability = 0.3f;

	this->IllegalStuff.push_back(tmp);

	tmp.Active = false;
	tmp.WasActive = false;
	tmp.Name = L"Abgelaufene Produkte";
	tmp.Description = L"";
	tmp.PenaltyProbability = 0.0f;

	this->IllegalStuff.push_back(tmp);


	this->PenaltyLevel.resize(this->IllegalStuff.size() + 1);

	this->GameMenu = MenuSystem(screenSize, &this->TexHandler);
	this->GameMenu.SetActiveMenu("");

	this->Intro1 = *this->TexHandler.GetSprite("GALogo")[0][0];

	while (this->Intro1.getGlobalBounds().width < screenSize.x && this->Intro1.getGlobalBounds().height < screenSize.y)
	{
		this->Intro1.scale(1.1f, 1.1f);
	}

	while (this->Intro1.getGlobalBounds().width > screenSize.x && this->Intro1.getGlobalBounds().height > screenSize.y)
	{
		this->Intro1.scale(0.9f, 0.9f);
	}

	float difX = screenSize.x - this->Intro1.getGlobalBounds().width;
	float difY = screenSize.y - this->Intro1.getGlobalBounds().height;
	this->Intro1.setPosition(difX / 2, difY / 2);

	this->Intro2 = *this->TexHandler.GetSprite("TeamLogo")[0][0];

	while (this->Intro2.getGlobalBounds().width < screenSize.x && this->Intro2.getGlobalBounds().height < screenSize.y)
	{
		this->Intro2.scale(1.1f, 1.1f);
	}

	while (this->Intro2.getGlobalBounds().width > screenSize.x && this->Intro2.getGlobalBounds().height > screenSize.y)
	{
		this->Intro2.scale(0.9f, 0.9f);
	}

	difX = screenSize.x - this->Intro2.getGlobalBounds().width;
	difY = screenSize.y - this->Intro2.getGlobalBounds().height;
	this->Intro2.setPosition(difX / 2, difY / 2);

	this->IntroFade = 0.0f;

	this->IntroPhase = 0;
}


Game::~Game(void)
{
	
}


void Game::update(sf::Time elapsedTime, Input* input, Interface* UI)
{
	switch (this->GameState)
	{
	case Game::State::Intro:
		this->IntroFade += 0.0001f - 0.0002f * (this->IntroPhase % 2);
		if ((!(this->IntroPhase % 2) && this->IntroFade >= 1.0f) || (this->IntroPhase % 2 && this->IntroFade <= 0.0f))
		{
			this->IntroPhase++;
		}
		if (this->IntroPhase == 4)
		{
			this->GameState = MainMenu;
			this->GameMenu.SetActiveMenu("Main");
		}
		this->Intro1.setColor(sf::Color(255, 255, 255, 255 * this->IntroFade));
		this->Intro2.setColor(sf::Color(255, 255, 255, 255 * this->IntroFade));

		break;


	case Game::State::MainMenu:
		break;


	default:
		if (this->GameState == Game::State::MainGame)
		{
			this->Data.update(elapsedTime, this->Speed);
			if (this->Data.Time.Hour == this->CurrentStore->ClosingHour[this->Data.WeekDay])
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameState;
				tmp.Text = "AfterClosing";
				UI->Messages.push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Storage";
				UI->Messages.push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Workers";
				UI->Messages.push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::OpenWindow;
				tmp.Text = "Products";
				UI->Messages.push_back(InterfaceMessage(tmp));
			}
		}

		if (input->JustPressed(Input::Function::Pause))
		{
			this->PreviousState = this->GameState;
			this->GameState = Game::State::Pause;
			this->GameMenu.SetActiveMenu("Pause");
		}


		this->Bob.update(&this->CurrentStore->Grid, &this->CurrentStore->CostGrid, input, &this->CurrentStore->HighestID, &this->CurrentStore->FreeIDs, &this->TexHandler, UI, &this->Data);

		this->ThePathfinder.Update(&this->CurrentStore->CostGrid);

		this->CurrentStore->update(elapsedTime, this->Speed, &this->Data);

		if (!UI->Messages.empty())
		{
			if (UI->Messages.front().MessageType == InterfaceMessage::Type::ActivateIllegalActivity)
			{
				this->IllegalStuff[atoi(UI->Messages.front().Text.c_str())].Active = true;
				this->IllegalStuff[atoi(UI->Messages.front().Text.c_str())].WasActive = true;

				std::wstring name = this->IllegalStuff[atoi(UI->Messages.front().Text.c_str())].Name;

				if (name == L"Alkohol an Minderjährige verkaufen")
				{
					this->CurrentStore->PeepGenerator.ChangeClassProbability(L"Kind", 0.1f);
					this->CurrentStore->PeepGenerator.ChangeClassProbability(L"Jugendlich", 0.2f);
				}

				if (name == L"Unterschwellige Werbung")
				{
					this->CurrentStore->SpawnRate -= 1.5f;
				}

				UI->Messages.pop_front();
			}
			else if (UI->Messages.front().MessageType == InterfaceMessage::Type::DeactivateIllegalActivity)
			{
				this->IllegalStuff[atoi(UI->Messages.front().Text.c_str())].Active = false;

				std::wstring name = this->IllegalStuff[atoi(UI->Messages.front().Text.c_str())].Name;

				if (name == L"Alkohol an Minderjährige verkaufen")
				{
					this->CurrentStore->PeepGenerator.ChangeClassProbability(L"Jugendlich", -0.2f);
					this->CurrentStore->PeepGenerator.ChangeClassProbability(L"Kind", -0.1f);
				}

				if (name == L"Unterschwellige Werbung")
				{
					this->CurrentStore->SpawnRate += 1.5f;
				}

				UI->Messages.pop_front();
			}
		}
		break;
	}

	this->GameMenu.update(input);
	if (this->GameMenu.ActiveMenu && this->GameMenu.ActiveMenu->Name == "Main")
	{
		this->GameState = Game::State::MainMenu;
	}
	if (!this->GameMenu.Messages.empty())
	{
		if (this->GameMenu.Messages.front().MessageType == MenuMessage::Quit)
		{
			this->GameState = Game::State::Quit;
			this->GameMenu.Messages.pop_front();
		}
		else if (this->GameMenu.Messages.front().MessageType == MenuMessage::StartGame)
		{
			this->GameState = Game::State::AfterClosing;
			this->Feed.clear();
			this->GameMenu.SetActiveMenu("");
			this->GameMenu.Messages.pop_front();
			if (this->Stores.size() == 1)
			{
				this->Stores.pop_back();
			}
			this->Stores.push_back(Store(20, 20, &this->ProductList, &this->ThePathfinder, &this->TexHandler));
			this->CurrentStore = &this->Stores.back();
			this->CurrentStore->PeepGenerator.Jobs = &this->CurrentStore->Jobs;
			this->CurrentStore->PeepGenerator.ProductStorage = &this->CurrentStore->ProductStorage;
			this->CurrentStore->PeepGenerator.Feed = &this->Feed;
			this->CurrentStore->PeepGenerator.Grid = &this->CurrentStore->Grid;
			this->CurrentStore->PeepGenerator.Messages = &this->CurrentStore->Messages;
			this->CurrentStore->ProductStorage.ProductList = &this->ProductList;
			this->CurrentStore->ProductStorage.TexHandler = &this->TexHandler;
			this->CurrentStore->ProductStorage.Feed = &this->Feed;

			this->CurrentStore->OpeningDays[0] = true;
			this->CurrentStore->OpeningDays[1] = true;
			this->CurrentStore->OpeningDays[2] = true;
			this->CurrentStore->OpeningDays[3] = true;
			this->CurrentStore->OpeningDays[4] = true;
			this->CurrentStore->OpeningDays[5] = true;
			this->CurrentStore->OpeningDays[6] = false;

			this->CurrentStore->OpeningHour[0] = 8;
			this->CurrentStore->OpeningHour[1] = 8;
			this->CurrentStore->OpeningHour[2] = 8;
			this->CurrentStore->OpeningHour[3] = 8;
			this->CurrentStore->OpeningHour[4] = 8;
			this->CurrentStore->OpeningHour[5] = 8;

			this->CurrentStore->ClosingHour[0] = 18;
			this->CurrentStore->ClosingHour[1] = 18;
			this->CurrentStore->ClosingHour[2] = 18;
			this->CurrentStore->ClosingHour[3] = 18;
			this->CurrentStore->ClosingHour[4] = 18;
			this->CurrentStore->ClosingHour[5] = 18;

			for (int i = 0; i < this->CurrentStore->GridSizeX; i++)
			{
				for (int j = 0; j < this->CurrentStore->GridSizeY; j++)
				{
					this->CurrentStore->Grid[i][j].push_back(new StoreObject(StoreObject::Type::Floor, StoreObject::Quality::Nothing, 0, 0, std::vector<sf::Vector2i>(), std::vector<sf::Vector2i>(), 0, std::vector<std::vector<std::vector<sf::Vector2i>>>(), 0, 0));
					this->CurrentStore->Grid[i][j].back()->Sprite = this->TexHandler.GetSprite("Grass3");
					this->CurrentStore->Grid[i][j].back()->Origins = this->TexHandler.GetOrigins("Grass3");
					this->CurrentStore->Grid[i][j].back()->GridPosition = sf::Vector2f(i, j);
					this->CurrentStore->Grid[i][j].back()->update(sf::Time::Zero, 1, 0);
					this->CurrentStore->Grid[i][j].back()->ID = ++this->CurrentStore->HighestID;
					this->CurrentStore->Grid[i][j].back()->SpriteName = "Grass3";
				}
			}

			this->CurrentStore->UpdateBackground();
			
			time_t t = time(0);
			struct tm* now = localtime(&t);

			this->Data.CurrentMoney = 5000;
			this->Data.CurrentCustomers = 0;
			this->Data.Date.Day = now->tm_mday;
			this->Data.Date.Month = now->tm_mon + 1;
			this->Data.Date.Year = now->tm_year + 1900;
			this->Data.Time.Hour = this->CurrentStore->ClosingHour[0];
			this->Data.Time.Minute = 0;
			this->Data.IllegalStuff = &this->IllegalStuff;
			this->Data.SoundHandler = &this->SoundHandler;

			this->Data.Prestige = 2000;

			for (int i = 0; i < this->PenaltyLevel.size(); i++)
			{
				this->PenaltyLevel[i] = 0;
			}

			this->Loaded = true;
		}
		else if (this->GameMenu.Messages.front().MessageType == MenuMessage::LoadGame)
		{
			this->Load(std::wstring(this->GameMenu.Messages.front().Text.begin(), this->GameMenu.Messages.front().Text.end()));
			this->GameMenu.Messages.pop_front();
			this->GameMenu.SetActiveMenu("");
		}
		else if (this->GameMenu.Messages.front().MessageType == MenuMessage::SaveGame)
		{
			this->Save(std::wstring(this->GameMenu.Messages.front().Text.begin(), this->GameMenu.Messages.front().Text.end()));
			this->GameMenu.Messages.pop_front();
		}
		else if (this->GameMenu.Messages.front().MessageType == MenuMessage::ResumeGame)
		{
			this->GameState = this->PreviousState;
			this->GameMenu.SetActiveMenu("");
			this->GameMenu.Messages.pop_front();
		}
		else if (this->GameMenu.Messages.front().MessageType == MenuMessage::ChangeVolume)
		{
			this->SoundHandler.SetSoundVolume(this->GameMenu.Messages.front().Value);
			this->GameMenu.Messages.pop_front();
		}
	}
}


void Game::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->GameState != Game::State::Intro && this->GameState != Game::State::MainMenu && this->GameState != Game::State::Quit)
	{
		target.draw(*this->CurrentStore);


		target.draw(this->Bob);
	}
	else if (this->GameState == Game::State::Intro)
	{
		if (this->IntroPhase < 2)
		{
			target.draw(this->Intro1);
		}
		else
		{
			target.draw(this->Intro2);
		}
	}
}


void Game::loadProducts()
{
	std::wifstream file("Content/Data/Products.txt");
	std::wstring line;

	std::string field;
	int status = 0;

	std::string name;
	std::string cat;
	std::string subcat;
	std::string prod;
	std::string qual;
	std::string price;
	std::string minExp;
	std::string maxExp;
	std::wstring descr;

	while (std::getline(file, line))
	{
		std::wstring::iterator iter = line.begin();
		std::wstring::iterator end = line.end();

		while (iter != end)
		{
			if (*iter == ' ' && status < 1)
			{
				status++;
			}
			else if (*iter == '=')
			{
				status--;
			}
			else if (*iter != ';')
			{
				switch (status)
				{
				case 0:
					field += *iter;
					break;

				case 1:
					if (field == "Name")
					{
						name += *iter;
					}
					else if (field == "Category")
					{
						cat += *iter;
					}
					else if (field == "Subcategory")
					{
						subcat += *iter;
					}
					else if (field == "Product")
					{
						prod += *iter;
					}
					else if (field == "Quality")
					{
						qual += *iter;
					}
					else if (field == "Price")
					{
						price += *iter;
					}
					else if (field == "MinDaysToExpire")
					{
						minExp += *iter;
					}
					else if (field == "MaxDaysToExpire")
					{
						maxExp += *iter;
					}
					else if (field == "Description")
					{
						descr += *iter;
					}
					break;
				}
			}

			if (*iter == ';')
			{
				field = "";
				status = 0;
			}

			iter++;
		}

		if (line.empty())
		{
			iter = descr.begin();
			end = descr.end();

			int counter = 0;

			while (iter != end)
			{
				if (*iter == ' ')
				{
					counter++;
				}

				if (counter == 5)
				{
					counter = 0;
					descr.replace(iter, iter + 1, 1, '\n');
				}

				iter++;
			}

			ProductListItem* tmp = new ProductListItem;
			tmp->Name = name;
			tmp->Category = cat;
			tmp->Subcategory = subcat;
			tmp->Product = prod;
			tmp->Quality = ProductQuality::Illegal;
			if (qual == "Cheap")
			{
				tmp->Quality = ProductQuality::Cheap;
			}
			else if (qual == "Normal")
			{
				tmp->Quality = ProductQuality::Normal;
			}
			else if (qual == "Premium")
			{
				tmp->Quality = ProductQuality::Premium;
			}
			tmp->Price = atof(price.c_str());
			tmp->MinDaysToExpire = atoi(minExp.c_str());
			tmp->MaxDaysToExpire = atoi(maxExp.c_str());
			tmp->Description = descr;

			this->ProductList.push_back(tmp);


			name.clear();
			cat.clear();
			subcat.clear();
			prod.clear();
			qual.clear();
			price.clear();
			minExp.clear();
			maxExp.clear();
			descr.clear();
		}
	}

	file.close();
}


void Game::Save(std::wstring path)
{
	std::wfstream file;
	/*
	file.open(L"Saves/" + path, std::ios::out | std::ios::binary);
	unsigned char smarker[3];
	smarker[0] = 0xEF;
	smarker[1] = 0xBB;
	smarker[2] = 0xBF;
	file << smarker;
	file.close();*/
	std::locale utf8_locale(std::locale(), new gel::stdx::utf8cvt<true>);
	file.imbue(utf8_locale);
	file.open(L"Saves/" + path, std::ios::out);

	//Save Gamedata
	file << "CurrentCustomers=" << this->Data.CurrentCustomers << "\n";
	file << "CurrentMoney=" << this->Data.CurrentMoney << "\n";
	file << "Day=" << this->Data.Date.Day << "\n";
	file << "Month=" << this->Data.Date.Month << "\n";
	file << "Year=" << this->Data.Date.Year << "\n";
	file << "Hygiene=" << this->Data.Hygiene << "\n";
	file << "Prestige=" << this->Data.Prestige << "\n";
	file << "Hour=" << this->Data.Time.Hour << "\n";
	file << "Minute=" << this->Data.Time.Minute << "\n";

	for (int i = 0; i < this->PenaltyLevel.size(); i++)
	{
		file << "PenaltyLevel=" << this->PenaltyLevel[i] << "\n";
	}

	file << "GameSpeed=" << this->Speed << "\n";
	file << "GameState=" << this->GameState << "\n";
	file << "PreviousGameState=" << this->PreviousState << "\n";


	//Save Pathfinder
	std::deque<Pathfinder::Request>::iterator reqiter = ThePathfinder.Requests.begin();
	std::deque<Pathfinder::Request>::iterator reqend = ThePathfinder.Requests.end();

	while (reqiter != reqend)
	{
		file << "ReqID=" << reqiter->ID << "\n";
		file << "ReqStartPointX=" << reqiter->StartPoint.x << "\n";
		file << "ReqStartPointY=" << reqiter->StartPoint.y << "\n";
		file << "ReqEndPointX=" << reqiter->EndPoint.x << "\n";
		file << "ReqEndPointY=" << reqiter->EndPoint.y << "\n";

		reqiter++;
	}

	std::list<Pathfinder::Path>::iterator piter = ThePathfinder.Paths.begin();
	std::list<Pathfinder::Path>::iterator pend = ThePathfinder.Paths.end();

	while (piter != pend)
	{
		file << "PathID=" << piter->ID << "\n";

		std::list<sf::Vector2i>::iterator wpiter = piter->WayPoints.begin();
		std::list<sf::Vector2i>::iterator wpend = piter->WayPoints.end();

		while (wpiter != wpend)
		{
			file << "PathWayPointX=" << wpiter->x << "\n";
			file << "PathWayPointY=" << wpiter->y << "\n";

			wpiter++;
		}

		piter++;
	}


	//Save Store
	file << "GridSizeX=" << this->CurrentStore->GridSizeX << "\n";
	file << "GridSizeY=" << this->CurrentStore->GridSizeY << "\n";

	file << "HighestID=" << this->CurrentStore->HighestID << "\n";

	std::deque<unsigned int>::iterator iditer = this->CurrentStore->FreeIDs.begin();
	std::deque<unsigned int>::iterator idend = this->CurrentStore->FreeIDs.end();

	while (iditer != idend)
	{
		file << "FreeID=" << *iditer << "\n";

		iditer++;
	}

	for (int i = 0; i < 7; i++)
	{
		file << "OpeningDay=" << this->CurrentStore->OpeningDays[i] << "\n";
	}

	for (int i = 0; i < 7; i++)
	{
		file << "OpeningHour=" << this->CurrentStore->OpeningHour[i] << "\n";
	}

	for (int i = 0; i < 7; i++)
	{
		file << "ClosingHour=" << this->CurrentStore->ClosingHour[i] << "\n";
	}

	std::list<Product>::iterator productiter = this->CurrentStore->ProductStorage.StorageItems.begin();
	std::list<Product>::iterator productend = this->CurrentStore->ProductStorage.StorageItems.end();

	while (productiter != productend)
	{
		file << "StorageProductName=" << std::wstring(productiter->Name.begin(), productiter->Name.end()) << "\n";
		file << "StorageProductAmount=" << productiter->Amount << "\n";
		file << "StorageProductDaysToExp=" << productiter->DaysToExpire << "\n";
		file << "StorageProductExDays=" << productiter->ExistenceDays << "\n";
		file << "StorageProductExpDay=" << productiter->ExpirationDate.Day << "\n";
		file << "StorageProductExpMonth=" << productiter->ExpirationDate.Month << "\n";
		file << "StorageProductExpYear=" << productiter->ExpirationDate.Year << "\n";
		file << "StorageProductExpState=" << productiter->ExpirationState << "\n";
		file << "StorageProductLastDay=" << productiter->LastDay << "\n";
		file << "StorageProductPrice=" << productiter->Price << "\n";

		productiter++;
	}


	for (int i = 0; i < this->CurrentStore->PeepGenerator.ClassProbabilities.size(); i++)
	{
		file << "ClassProbability=" << this->CurrentStore->PeepGenerator.ClassProbabilities[i] << "\n";
	}

	file << "AverageMoney=" << this->CurrentStore->PeepGenerator.AverageMoney << "\n";
	file << "AverageAge=" << this->CurrentStore->PeepGenerator.AverageAge << "\n";
	file << "AverageStealingPotential=" << this->CurrentStore->PeepGenerator.AverageStealingPotential << "\n";

	file << "SpawnTimer=" << this->CurrentStore->SpawnTimer << "\n";
	file << "SpawnRate=" << this->CurrentStore->SpawnRate << "\n";
	file << "Spawn=" << this->CurrentStore->Spawn << "\n";

	std::list<StoreMessage>::iterator messageiter = this->CurrentStore->Messages.begin();
	std::list<StoreMessage>::iterator messageend = this->CurrentStore->Messages.end();

	while (messageiter != messageend)
	{
		file << "MesID=" << messageiter->ID << "\n";
		file << "MesType=" << messageiter->MessageType << "\n";
		file << "MesName=" << std::wstring(messageiter->Name.begin(), messageiter->Name.end()) << "\n";
		if (messageiter->Pointer)
		{
			file << "MesPointerID=" << messageiter->Pointer->ID << "\n";
		}
		else
		{
			file << "MesPointerID=-1" << "\n";
		}
		file << "MesPosX=" << messageiter->Position.x << "\n";
		file << "MesPosY=" << messageiter->Position.y << "\n";

		messageiter++;
	}

	std::list<StoreJob>::iterator jobiter = this->CurrentStore->Jobs.begin();
	std::list<StoreJob>::iterator jobend = this->CurrentStore->Jobs.end();

	while (jobiter != jobend)
	{
		file << "JobType=" << jobiter->JobType << "\n";
		if (jobiter->Object)
		{
			file << "JobObjectID=" << jobiter->Object->ID << "\n";
		}
		else
		{
			file << "JobObjectID=-1" << "\n";
		}
		if (jobiter->ProductItem)
		{
			file << "JobProductName=" << std::wstring(jobiter->ProductItem->Name.begin(), jobiter->ProductItem->Name.end()) << "\n";
		}
		else
		{
			file << "JobProductName=-1" << "\n";
		}

		jobiter++;
	}

	file << "ExitPositionX=" << this->CurrentStore->ExitPosition.x << "\n";
	file << "ExitPositionY=" << this->CurrentStore->ExitPosition.y << "\n";

	file << "DrugsDelivered=" << this->CurrentStore->DrugsDelivered << "\n";
	file << "DrugHour=" << this->CurrentStore->DrugHour << "\n";
	file << "Drug=" << std::wstring(this->CurrentStore->Drug.begin(), this->CurrentStore->Drug.end()) << "\n";

	std::vector<bool> processed;
	processed.resize(this->CurrentStore->HighestID + 1);

	std::list<Entity*>::iterator iter;
	std::list<Entity*>::iterator end;

	for (int y = 0; y < this->CurrentStore->GridSizeY; y++)
	{
		for (int x = 0; x < this->CurrentStore->GridSizeX; x++)
		{
			iter = this->CurrentStore->Grid[x][y].begin();
			end = this->CurrentStore->Grid[x][y].end();

			while (iter != end)
			{
				if (!processed[(*iter)->ID])
				{
					processed[(*iter)->ID] = true;

					StoreObject* object = dynamic_cast<StoreObject*>(*iter);
					Customer* customer = dynamic_cast<Customer*>(*iter);
					Worker* worker = dynamic_cast<Worker*>(*iter);
					Animal* animal = dynamic_cast<Animal*>(*iter);

					if (object)
					{
						file << "NewEntity=StoreObject" << "\n";
					}
					if (customer)
					{
						file << "NewEntity=Customer" << "\n";
					}
					if (worker)
					{
						file << "NewEntity=Worker" << "\n";
					}
					if (animal)
					{
						file << "NewEntity=Animal" << "\n";
					}

					file << "AnimPhase=" << (*iter)->AnimPhase << "\n";
					file << "AnimTimer=" << (*iter)->AnimTimer << "\n";
					file << "DeleteMe=" << (*iter)->DeleteMe << "\n";
					file << "GridPosX=" << (*iter)->GridPosition.x << "\n";
					file << "GridPosY=" << (*iter)->GridPosition.y << "\n";
					file << "ID=" << (*iter)->ID << "\n";
					file << "LowestGridPositionX=" << (*iter)->LowestGridPosition.x << "\n";
					file << "LowestGridPositionY=" << (*iter)->LowestGridPosition.y << "\n";
					file << "LowestScreenPositionX=" << (*iter)->LowestScreenPosition.x << "\n";
					file << "LowestScreenPositionY=" << (*iter)->LowestScreenPosition.y << "\n";
					file << "Moved=" << (*iter)->Moved << "\n";
					file << "PreviousGridPositionX=" << (*iter)->PreviousGridPosition.x << "\n";
					file << "PreviousGridPositionY=" << (*iter)->PreviousGridPosition.y << "\n";

					std::list<Product>::iterator proditer;
					std::list<Product>::iterator prodend;

					for (int i = 0; i < (*iter)->Products.size(); i++)
					{
						proditer = (*iter)->Products[i].begin();
						prodend = (*iter)->Products[i].end();

						while (proditer != prodend)
						{
							file << "EntityProductName" << i << "=" << std::wstring(proditer->Name.begin(), proditer->Name.end()) << "\n";
							file << "EntityProductAmount" << i << "=" << proditer->Amount << "\n";
							file << "EntityProductDaysToExp" << i << "=" << proditer->DaysToExpire << "\n";
							file << "EntityProductExDays" << i << "=" << proditer->ExistenceDays << "\n";
							file << "EntityProductExpDay" << i << "=" << proditer->ExpirationDate.Day << "\n";
							file << "EntityProductExpMonth" << i << "=" << proditer->ExpirationDate.Month << "\n";
							file << "EntityProductExpYear" << i << "=" << proditer->ExpirationDate.Year << "\n";
							file << "EntityProductExpState" << i << "=" << proditer->ExpirationState << "\n";
							file << "EntityProductLastDay" << i << "=" << proditer->LastDay << "\n";
							file << "EntityProductPrice" << i << "=" << proditer->Price << "\n";

							proditer++;
						}
					}

					file << "Rotation=" << (*iter)->Rotation << "\n";
					file << "SpriteName=" << std::wstring((*iter)->SpriteName.begin(), (*iter)->SpriteName.end()) << "\n";

					if (object)
					{
						for (int i = 0; i < object->AccessPoints.size(); i++)
						{
							file << "AccessPointX=" << object->AccessPoints[i].x << "\n";
							file << "AccessPointY=" << object->AccessPoints[i].y << "\n";
						}
						if (object->CurrentWorker)
						{
							file << "WorkerID=" << object->CurrentWorker->ID << "\n";
						}
						else
						{
							file << "WorkerID=-1" << "\n";
						}
						file << "CustomerCount=" << object->CustomerCount << "\n";
						for (int i = 0; i < object->Dates.size(); i++)
						{
							file << "ObjectDay=" << object->Dates[i].Day << "\n";
							file << "ObjectMonth=" << object->Dates[i].Month << "\n";
							file << "ObjectYear=" << object->Dates[i].Year << "\n";
						}
						file << "Dirtiness=" << object->Dirtiness << "\n";
						file << "DirtTimer=" << object->DirtTimer << "\n";
						file << "Height=" << object->Height << "\n";
						file << "Quality=" << object->ObjectQuality << "\n";
						file << "Type=" << object->ObjectType << "\n";
						file << "PreviousMinute=" << object->PreviousMinute << "\n";
						for (int i = 0; i < object->Prices.size(); i++)
						{
							file << "ObjectPrice=" << object->Prices[i] << "\n";
						}
						file << "ProdsPerLayer=" << object->ProdsPerLayer << "\n";
						file << "ProdsPerSlot=" << object->ProdsPerSlot << "\n";
						for (int i = 0; i < object->ProductPositions.size(); i++)
						{
							for (int j = 0; j < object->ProductPositions[i].size(); j++)
							{
								for (int k = 0; k < object->ProductPositions[i][j].size(); k++)
								{
									file << "ProductPosition" << i << "," << j << "X=" << object->ProductPositions[i][j][k].x << "\n";
									file << "ProductPosition" << i << "," << j << "Y=" << object->ProductPositions[i][j][k].y << "\n";
								}
							}
						}
						for (int i = 0; i < object->ShownProducts.size(); i++)
						{
							for (int j = 0; j < object->ShownProducts[i].size(); j++)
							{
								file << "ShownProduct" << i << "=" << object->ShownProducts[i][j] << "\n";
							}
						}
						for (int i = 0; i < object->Slots.size(); i++)
						{
							if (object->Slots[i])
							{
								file << "Slot=" << std::wstring(object->Slots[i]->Name.begin(), object->Slots[i]->Name.end()) << "\n";
							}
							else
							{
								file << "Slot=-1" << "\n";
							}
						}
						file << "Width=" << object->Width << "\n";
						for (int i = 0; i < object->WorkPoints.size(); i++)
						{
							file << "WorkPointX=" << object->WorkPoints[i].x << "\n";
							file << "WorkPointY=" << object->WorkPoints[i].y << "\n";
						}
					}

					if (customer)
					{
						file << "Class=" << customer->Class << "\n";
						file << "Deleted=" << customer->Deleted << "\n";
						file << "ExpectedProducts=" << customer->ExpectedProducts << "\n";
						file << "HygieneExpectation=" << customer->HygieneExpectation << "\n";
						file << "InLine=" << customer->InLine << "\n";
						file << "LinePosition=" << customer->LinePosition << "\n";
						file << "Money=" << customer->Money << "\n";
						file << "NextProduct=" << customer->NextProduct << "\n";
						if (customer->Object)
						{
							file << "CustomerObjectID=" << customer->Object->ID << "\n";
						}
						else
						{
							file << "CustomerObjectID=-1" << "\n";
						}
						file << "PersonState=" << customer->PersonState << "\n";
						for (int i = 0; i < customer->PriceExpectancy.size(); i++)
						{
							file << "PriceExpectancy=" << customer->PriceExpectancy[i] << "\n";
						}
						for (int i = 0; i < customer->ProductsFound.size(); i++)
						{
							file << "ProductFound=" << customer->ProductsFound[i] << "\n";
						}
						for (int i = 0; i < customer->ShoppingList.size(); i++)
						{
							file << "ShoppingListName=" << std::wstring(customer->ShoppingList[i]->Name.begin(), customer->ShoppingList[i]->Name.end()) << "\n";
						}
						file << "ShoppingState=" << customer->ShoppingState << "\n";
						file << "StealingPotential=" << customer->StealingPotential << "\n";
						file << "Waited=" << customer->Waited << "\n";
					}

					if (worker)
					{
						file << "AtWorkplace=" << worker->AtWorkplace << "\n";
						file << "CashieringEfficiency=" << worker->CashieringEfficiency << "\n";
						file << "CleaningEfficiency=" << worker->CleaningEfficiency << "\n";
						if (worker->Object)
						{
							file << "WorkerObjectID=" << worker->Object->ID << "\n";
						}
						else
						{
							file << "WorkerObjectID=-1" << "\n";
						}
						if (worker->ProductItem)
						{
							file << "ProductItem=" << std::wstring(worker->ProductItem->Name.begin(), worker->ProductItem->Name.end()) << "\n";
						}
						else
						{
							file << "ProductItem=-1" << "\n";
						}
						file << "RefillingEfficiency=" << worker->RefillingEfficiency << "\n";
						file << "Wage=" << worker->Wage << "\n";
						for (int i = 0; i < 7; i++)
						{
							file << "WorkDay=" << worker->WorkDays[i] << "\n";
						}
						file << "WorkerType=" << worker->WorkerType << "\n";
						file << "WorkHours=" << worker->WorkHours << "\n";
						file << "WorkState=" << worker->WorkState << "\n";
					}

					Person* person = dynamic_cast<Person*>(*iter);

					if (person)
					{
						file << "Age=" << person->Age << "\n";
						file << "EmoticonTimer=" << person->EmoticonTimer << "\n";
						file << "Happiness=" << person->Happiness << "\n";
						file << "Name=" << person->Name << "\n";

						std::list<sf::Vector2i>::iterator pathiter = person->Path.begin();
						std::list<sf::Vector2i>::iterator pathend = person->Path.end();

						while (pathiter != pathend)
						{
							file << "PathX=" << pathiter->x << "\n";
							file << "PathY=" << pathiter->y << "\n";

							pathiter++;
						}

						file << "Gender=" << person->PersonGender << "\n";
						file << "ShowEmotion=" << person->ShowEmotion << "\n";
						file << "Speed=" << person->Speed << "\n";
						file << "WaitTimer=" << person->WaitTimer << "\n";
					}

					if (animal)
					{
						file << "NextPosX=" << animal->NextPos.x << "\n";
						file << "NextPosY=" << animal->NextPos.y << "\n";
						file << "AnimalWaitTimer=" << animal->WaitTimer << "\n";
						file << "Walking=" << animal->Walking << "\n";
						file << "ToBeDeleted=" << animal->ToBeDeleted << "\n";
						file << "ToBeDeletedAgain=" << animal->ToBeDeletedAgain << "\n";
						file << "LifeTime=" << animal->LifeTime << "\n";
						file << "AnimalName=" << animal->Name << "\n";
					}
				}


				iter++;
			}
		}
	}



	file.close();
}


void Game::Load(std::wstring path)
{
	this->Loaded = true;

	this->PenaltyLevel.resize(0);

	Entity* currententity = 0;
	Pathfinder::Request currentrequest;
	currentrequest.ID = -1;
	Pathfinder::Path currentpathfinderpath;
	currentpathfinderpath.ID = -1;
	int gridSizeX = 0;
	int day = 0;
	Product product;
	product.Name = "Nothing";
	std::list<Product> productList;
	StoreMessage storeMessage;
	storeMessage.ID = -1;
	storeMessage.Pointer = 0;
	StoreJob storeJob;
	storeJob.JobType = StoreJob::Type::Nothing;
	storeJob.Object = 0;
	storeJob.ProductItem = 0;
	std::list<Connection> connections;
	Entity* entity = 0;
	StoreObject* object = 0;
	Worker* worker = 0;
	Customer* customer = 0;
	Person* person = 0;
	Animal* animal = 0;
	int pos = 0;


	std::wfstream file;
	std::locale utf8_locale(std::locale(), new gel::stdx::utf8cvt<true>);
	file.imbue(utf8_locale);
	file.open(L"Saves/" + path, std::ios::in);

	std::wstring line;

	std::wstring field;
	std::wstring value;

	while (std::getline(file, line))
	{
		std::wstring::iterator iter = line.begin();
		std::wstring::iterator end = line.end();

		while (*iter != '=' && iter != end)
		{
			field += *iter;

			iter++;
		}

		if (iter != end)
		{
			iter++;

			while (iter != end)
			{
				value += *iter;

				iter++;
			}
		}


		if (field == L"CurrentCustomers")
		{
			this->Data.CurrentCustomers = _wtoi(value.c_str());
		}
		if (field == L"CurrentMoney")
		{
			this->Data.CurrentMoney = _wtof(value.c_str());
		}
		if (field == L"Day")
		{
			this->Data.Date.Day = _wtoi(value.c_str());
		}
		if (field == L"Month")
		{
			this->Data.Date.Month = _wtoi(value.c_str());
		}
		if (field == L"Year")
		{
			this->Data.Date.Year = _wtoi(value.c_str());
		}
		if (field == L"Hygiene")
		{
			this->Data.Hygiene = _wtof(value.c_str());
		}
		if (field == L"Prestige")
		{
			this->Data.Prestige = _wtoi(value.c_str());
		}
		if (field == L"Hour")
		{
			this->Data.Time.Hour = _wtoi(value.c_str());
		}
		if (field == L"Minute")
		{
			this->Data.Time.Minute = _wtof(value.c_str());
		}
		if (field == L"PenaltyLevel")
		{
			this->PenaltyLevel.push_back(_wtoi(value.c_str()));
		}
		if (field == L"GameSpeed")
		{
			this->Speed = _wtoi(value.c_str());
		}
		if (field == L"GameState")
		{
			this->GameState = (Game::State)_wtoi(value.c_str());
		}
		if (field == L"PreviousGameState")
		{
			this->GameState = (Game::State)_wtoi(value.c_str());
			this->PreviousState = (Game::State)_wtoi(value.c_str());
		}
		if (field == L"ReqID")
		{
			if (currentrequest.ID == -1)
			{
				currentrequest.ID = _wtoi(value.c_str());
			}
			else
			{
				this->ThePathfinder.Requests.push_back(currentrequest);
				currentrequest = Pathfinder::Request();
				currentrequest.ID = _wtoi(value.c_str());
			}
		}
		if (field == L"ReqStartPointX")
		{
			currentrequest.StartPoint.x = _wtoi(value.c_str());
		}
		if (field == L"ReqStartPointY")
		{
			currentrequest.StartPoint.y = _wtoi(value.c_str());
		}
		if (field == L"ReqEndPointX")
		{
			currentrequest.EndPoint.x = _wtoi(value.c_str());
		}
		if (field == L"ReqEndPointY")
		{
			currentrequest.EndPoint.y = _wtoi(value.c_str());
		}
		if (field == L"PathID")
		{
			if (currentpathfinderpath.ID == -1)
			{
				currentpathfinderpath.ID = _wtoi(value.c_str());
			}
			else
			{
				this->ThePathfinder.Paths.push_back(currentpathfinderpath);
				currentpathfinderpath = Pathfinder::Path();
				currentpathfinderpath.ID = _wtoi(value.c_str());
			}
		}
		if (field == L"PathWayPointX")
		{
			currentpathfinderpath.WayPoints.push_back(sf::Vector2i(_wtoi(value.c_str()), 0));
		}
		if (field == L"PathWayPointY")
		{
			currentpathfinderpath.WayPoints.back().y = _wtoi(value.c_str());
		}
		if (field == L"GridSizeX")
		{
			if (currentrequest.ID != -1)
			{
				this->ThePathfinder.Requests.push_back(currentrequest);
			}
			if (currentpathfinderpath.ID != -1)
			{
				this->ThePathfinder.Paths.push_back(currentpathfinderpath);
			}
			gridSizeX = _wtoi(value.c_str());
		}
		if (field == L"GridSizeY")
		{
			if (!this->Stores.empty())
			{
				this->Stores.pop_back();
			}
			this->Stores.push_back(Store(gridSizeX, _wtoi(value.c_str()), &this->ProductList, &this->ThePathfinder, &this->TexHandler));
			this->CurrentStore = &this->Stores.back();
			this->CurrentStore->PeepGenerator.ClassProbabilities.resize(0);
			this->CurrentStore->UpdateBackground();
		}
		if (field == L"HighestID")
		{
			this->CurrentStore->HighestID = _wtoi(value.c_str());
		}
		if (field == L"FreeID")
		{
			this->CurrentStore->FreeIDs.push_back(_wtoi(value.c_str()));
		}
		if (field == L"OpeningDay")
		{
			this->CurrentStore->OpeningDays[day] = _wtoi(value.c_str());
			day++;
			if (day == 7)
			{
				day = 0;
			}
		}
		if (field == L"OpeningHour")
		{
			this->CurrentStore->OpeningHour[day] = _wtoi(value.c_str());
			day++;
			if (day == 7)
			{
				day = 0;
			}
		}
		if (field == L"ClosingHour")
		{
			this->CurrentStore->ClosingHour[day] = _wtoi(value.c_str());
			day++;
			if (day == 7)
			{
				day = 0;
			}
		}
		if (field == L"StorageProductName")
		{
			if (product.Name == "Nothing")
			{
				product.Name = std::string(value.begin(), value.end());
				product.Sprite = this->TexHandler.GetSprite(product.Name)[0][0];
			}
			else
			{
				productList.push_back(product);
				product = Product();
				product.Name = std::string(value.begin(), value.end());
				product.Sprite = this->TexHandler.GetSprite(product.Name)[0][0];
			}
		}
		if (field == L"StorageProductAmount")
		{
			product.Amount = _wtoi(value.c_str());
		}
		if (field == L"StorageProductDaysToExp")
		{
			product.DaysToExpire = _wtoi(value.c_str());
		}
		if (field == L"StorageProductExDays")
		{
			product.ExistenceDays = _wtoi(value.c_str());
		}
		if (field == L"StorageProductExpDay")
		{
			product.ExpirationDate.Day = _wtoi(value.c_str());
		}
		if (field == L"StorageProductExpMonth")
		{
			product.ExpirationDate.Month = _wtoi(value.c_str());
		}
		if (field == L"StorageProductExpYear")
		{
			product.ExpirationDate.Year = _wtoi(value.c_str());
		}
		if (field == L"StorageProductExpState")
		{
			if (_wtoi(value.c_str()) == Product::State::Good)
			{
				product.ExpirationState = Product::State::Good;
			}
			if (_wtoi(value.c_str()) == Product::State::Expired)
			{
				product.ExpirationState = Product::State::Expired;
			}
			if (_wtoi(value.c_str()) == Product::State::Rotten)
			{
				product.ExpirationState = Product::State::Rotten;
			}
		}
		if (field == L"StorageProductLastDay")
		{
			product.LastDay = _wtoi(value.c_str());
		}
		if (field == L"StorageProductPrice")
		{
			product.Price = _wtof(value.c_str());
		}
		if (field == L"ClassProbability")
		{
			if (product.Name != "Nothing")
			{
				productList.push_back(product);
				product = Product();
				product.Name = "Nothing";

				this->CurrentStore->ProductStorage.addProducts(&productList);
			}
			this->CurrentStore->PeepGenerator.ClassProbabilities.push_back(_wtof(value.c_str()));
		}
		if (field == L"AverageMoney")
		{
			this->CurrentStore->PeepGenerator.AverageMoney = _wtof(value.c_str());
		}
		if (field == L"AverageAge")
		{
			this->CurrentStore->PeepGenerator.AverageAge = _wtoi(value.c_str());
		}
		if (field == L"AverageStealingPotential")
		{
			this->CurrentStore->PeepGenerator.AverageStealingPotential = _wtof(value.c_str());
		}
		if (field == L"SpawnTimer")
		{
			this->CurrentStore->SpawnTimer = _wtof(value.c_str());
		}
		if (field == L"SpawnRate")
		{
			this->CurrentStore->SpawnRate = _wtof(value.c_str());
		}
		if (field == L"Spawn")
		{
			this->CurrentStore->Spawn = _wtoi(value.c_str());
		}
		if (field == L"MesID")
		{
			if (storeMessage.ID == -1)
			{
				storeMessage.ID = _wtoi(value.c_str());
			}
			else
			{
				this->CurrentStore->Messages.push_back(storeMessage);
				storeMessage = StoreMessage();
				storeMessage.ID = _wtoi(value.c_str());
			}
		}
		if (field == L"MesType")
		{
			storeMessage.MessageType = (StoreMessage::Type)_wtoi(value.c_str());
		}
		if (field == L"MesName")
		{
			storeMessage.Name = std::string(value.begin(), value.end());
		}
		if (field == L"MesPointerID")
		{
			if (_wtoi(value.c_str()) != -1)
			{
				Connection tmp;
				tmp.ConnectionType = Connection::Type::Message;
				tmp.FirstID = storeMessage.ID;
				tmp.SecondID = _wtoi(value.c_str());
				connections.push_back(tmp);
			}
		}
		if (field == L"MesPosX")
		{
			storeMessage.Position.x = _wtoi(value.c_str());
		}
		if (field == L"MesPosY")
		{
			storeMessage.Position.y = _wtoi(value.c_str());
		}
		if (field == L"JobType")
		{
			if (storeJob.JobType == StoreJob::Type::Nothing)
			{
				storeJob.JobType = (StoreJob::Type)_wtoi(value.c_str());
			}
			else
			{
				this->CurrentStore->Jobs.push_back(storeJob);
				storeJob = StoreJob();
				storeJob.JobType = (StoreJob::Type)_wtoi(value.c_str());
			}
		}
		if (field == L"JobObjectID")
		{
			if (_wtoi(value.c_str()) != -1)
			{
				Connection tmp;
				tmp.ConnectionType = Connection::Type::Job;
				tmp.FirstID = storeJob.JobType;
				tmp.SecondID = _wtoi(value.c_str());
				connections.push_back(tmp);
			}
		}
		if (field == L"JobProductName")
		{
			if (value != L"-1")
			{
				for (int i = 0; i < this->ProductList.size(); i++)
				{
					if (this->ProductList[i]->Name == std::string(value.begin(), value.end()))
					{
						storeJob.ProductItem = this->ProductList[i];
						break;
					}
				}
			}
		}
		if (field == L"ExitPositionX")
		{
			if (storeMessage.ID != -1)
			{
				this->CurrentStore->Messages.push_back(storeMessage);
			}
			if (storeJob.JobType != StoreJob::Type::Nothing)
			{
				this->CurrentStore->Jobs.push_back(storeJob);
			}
			this->CurrentStore->ExitPosition.x = _wtoi(value.c_str());
		}
		if (field == L"ExitPositionY")
		{
			this->CurrentStore->ExitPosition.y = _wtoi(value.c_str());
		}
		if (field == L"DrugsDelivered")
		{
			this->CurrentStore->DrugsDelivered = _wtoi(value.c_str());
		}
		if (field == L"DrugHour")
		{
			this->CurrentStore->DrugHour = _wtoi(value.c_str());
		}
		if (field == L"Drug")
		{
			this->CurrentStore->Drug = std::string(value.begin(), value.end());
		}
		if (field == L"NewEntity")
		{
			if (entity != 0)
			{
				if (dynamic_cast<Animal*>(entity))
				{
					entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName);
					entity->BackSprite = this->TexHandler.GetSprite(entity->SpriteName);
					entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);
					entity->Grid = &this->CurrentStore->Grid;
					this->CurrentStore->Grid[entity->GridPosition.x][entity->GridPosition.y].push_back(entity);
				}
				else if (dynamic_cast<Person*>(entity))
				{
					entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName + "_Front");
					entity->BackSprite = this->TexHandler.GetSprite(entity->SpriteName + "_Back");
					person->Emoticons = this->TexHandler.GetSprite("Emotion");
					for (int i = 0; i < person->Emoticons[0].size(); i++)
					{
						person->Emoticons[0][i]->setOrigin(person->Emoticons[0][i]->getGlobalBounds().width / 2, person->Emoticons[0][i]->getGlobalBounds().height);
					}
					person->ThePathfinder = &this->ThePathfinder;
					person->Messages = &this->CurrentStore->Messages;
					person->Grid = &this->CurrentStore->Grid;
					person->Feed = &this->Feed;
					if (dynamic_cast<Customer*>(entity))
					{
						customer->Products.resize(customer->ShoppingList.size());
					}
					if (dynamic_cast<Worker*>(entity))
					{
						worker->Jobs = &this->CurrentStore->Jobs;
						worker->TheStorage = &this->CurrentStore->ProductStorage;
						worker->calculateWageExpectency();
					}
					entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);
					this->CurrentStore->Grid[entity->GridPosition.x][entity->GridPosition.y].push_back(entity);
				}
				else
				{
					object->Products.resize(object->Slots.size());

					object->ShownProducts.resize(object->Slots.size());
					for (int i = 0; i < object->ShownProducts.size(); i++)
					{
						object->ShownProducts[i].resize(object->ProdsPerSlot);
						/*for (int j = 0; j < object->ShownProducts[i].size(); j++)
						{
							object->ShownProducts[i][j] = false;
						}*/
					}

					if (this->TexHandler.Exists(entity->SpriteName + "_Front"))
					{
						if (entity->Rotation < 2)
						{
							entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName + "_Front");
						}
						else
						{
							entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName + "_Back");
						}
					}
					else
					{
						entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName);
					}
					if (entity->Rotation % 2)
					{
						entity->scale(sf::Vector2f(-1.0f, 1.0f));
					}

					entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);

					if (object->ObjectType == StoreObject::Type::Floor && object->ObjectQuality != StoreObject::Quality::Nothing)
					{
						object->DirtSprites.resize(3);
						if (rand() % 2)
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Dirt_Floor11")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Dirt_Floor12")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Dirt_Floor13")[0][0];
						}
						else
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Dirt_Floor21")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Dirt_Floor22")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Dirt_Floor23")[0][0];
						}
					}
					else if (object->ObjectType == StoreObject::Type::Shelf)
					{
						object->DirtSprites.resize(3);
						if (object->ObjectQuality == StoreObject::Quality::Illegal)
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt1")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt1")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt2")[0][0];
						}
						else if (object->ObjectQuality == StoreObject::Quality::Cheap)
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt1")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt1")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt2")[0][0];
						}
						else if (object->ObjectQuality == StoreObject::Quality::Normal)
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Normal_Dirt1")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Normal_Dirt1")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Normal_Dirt2")[0][0];
						}
						else if (object->ObjectQuality == StoreObject::Quality::Premium)
						{
							object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Premium_Dirt1")[0][0];
							object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Premium_Dirt1")[0][0];
							object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Premium_Dirt2")[0][0];
						}

						if (object->Rotation % 2)
						{
							for (int i = 0; i < 3; i++)
							{
								object->DirtSprites[i]->scale(-1.0f, 1.0f);
							}
						}
					}

					object->Feed = &this->Feed;
					entity->Grid = &this->CurrentStore->Grid;

					if (object->Height < 0)
					{
						int bla = 0;
					}
					for (int i = 0; i <= abs(object->Width); i++)
					{
						for (int j = 0; j <= abs(object->Height); j++)
						{
							int setPosX = entity->GridPosition.x + i * (1 + (object->Width < 0) * -2);
							int setPosY = entity->GridPosition.y + j * (1 + (object->Height < 0) * -2);

							if (object->WorkPoints.size() == 0 ||
								(setPosX != entity->GridPosition.x + object->WorkPoints[0].x ||
								setPosY != entity->GridPosition.y + object->WorkPoints[0].y))
							{
								this->CurrentStore->Grid[setPosX][setPosY].push_back(entity);
								this->CurrentStore->CostGrid[setPosX][setPosY] = 0;
								if (object->ObjectType == StoreObject::Type::Floor)
								{
									this->CurrentStore->CostGrid[setPosX][setPosY] = 50;
								}
							}
						}
					}
				}
			}

			if (value == L"StoreObject")
			{
				entity = new StoreObject();
				object = dynamic_cast<StoreObject*>(entity);
			}
			if (value == L"Customer")
			{
				entity = new Customer();
				customer = dynamic_cast<Customer*>(entity);
				person = dynamic_cast<Person*>(entity);
			}
			if (value == L"Worker")
			{
				entity = new Worker();
				worker = dynamic_cast<Worker*>(entity);
				person = dynamic_cast<Person*>(entity);
			}
			if (value == L"Animal")
			{
				entity = new Animal();
				animal = dynamic_cast<Animal*>(entity);
			}
		}
		if (field == L"AccessPointX")
		{
			object->AccessPoints.push_back(sf::Vector2i(_wtoi(value.c_str()), 0));
		}
		if (field == L"AccessPointY")
		{
			object->AccessPoints.back().y = _wtoi(value.c_str());
		}
		if (field == L"WorkerID")
		{
			if (value != L"-1")
			{
				Connection tmp;
				tmp.ConnectionType = Connection::Type::StoreObject;
				tmp.FirstID = entity->ID;
				tmp.SecondID = _wtoi(value.c_str());
				connections.push_back(tmp);
			}
		}
		if (field == L"CustomerCount")
		{
			object->CustomerCount = _wtoi(value.c_str());
		}
		if (field == L"ObjectDay")
		{
			object->Dates.push_back(Date());
			object->Dates.back().Day = _wtoi(value.c_str());
		}
		if (field == L"ObjectMonth")
		{
			object->Dates.back().Month = _wtoi(value.c_str());
		}
		if (field == L"ObjectYear")
		{
			object->Dates.back().Year = _wtoi(value.c_str());
		}
		if (field == L"Dirtiness")
		{
			object->Dirtiness = _wtoi(value.c_str());
		}
		if (field == L"DirtTimer")
		{
			object->DirtTimer = _wtoi(value.c_str());
		}
		if (field == L"Height")
		{
			object->Height = _wtoi(value.c_str());
		}
		if (field == L"Quality")
		{
			object->ObjectQuality = (StoreObject::Quality)_wtoi(value.c_str());
		}
		if (field == L"Type")
		{
			object->ObjectType = (StoreObject::Type)_wtoi(value.c_str());
		}
		if (field == L"PreviousMinute")
		{
			object->PreviousMinute = _wtoi(value.c_str());
		}
		if (field == L"ObjectPrice")
		{
			object->Prices.push_back(_wtof(value.c_str()));
		}
		if (field == L"ProdsPerLayer")
		{
			object->ProdsPerLayer = _wtoi(value.c_str());
		}
		if (field == L"ProdsPerSlot")
		{
			object->ProdsPerSlot = _wtoi(value.c_str());
		}
		if (field.find(L"ProductPosition") != std::wstring::npos)
		{
			std::wstring istr = field;
			istr.erase(0, 15);
			std::wstring::iterator siter = istr.begin();
			while (*siter != ',')
			{
				siter++;
			}
			std::wstring jstr = std::wstring(siter + 1, istr.end());
			istr.erase(siter, istr.end());
			jstr.pop_back();
			int i = _wtoi(istr.c_str());
			int j = _wtoi(jstr.c_str());

			if (field.find(L"X") != std::wstring::npos)
			{
				if (object->ProductPositions.size() <= i)
				{
					object->ProductPositions.resize(i + 1);
				}
				if (object->ProductPositions[i].size() <= j)
				{
					object->ProductPositions[i].resize(j + 1);
				}
				object->ProductPositions[i][j].push_back(sf::Vector2i(_wtoi(value.c_str()), 0));
			}
			if (field.find(L"Y") != std::wstring::npos)
			{
				object->ProductPositions[i][j].back().y = _wtoi(value.c_str());
			}
		}
		if (field.find(L"ShownProduct") != std::wstring::npos)
		{
			std::wstring istr = field;
			istr.erase(0, 12);
			int i = _wtoi(istr.c_str());

			if (object->ShownProducts.size() <= i)
			{
				object->ShownProducts.resize(i + 1);
			}
			object->ShownProducts[i].push_back(_wtoi(value.c_str()));
		}
		if (field == L"Slot")
		{
			if (value == L"-1")
			{
				object->Slots.push_back(0);
			}
			else
			{
				for (int i = 0; i < this->ProductList.size(); i++)
				{
					if (this->ProductList[i]->Name == std::string(value.begin(), value.end()))
					{
						object->Slots.push_back(this->ProductList[i]);
					}
				}
			}
		}
		if (field == L"Width")
		{
			object->Width = _wtoi(value.c_str());
		}
		if (field == L"WorkPointX")
		{
			object->WorkPoints.push_back(sf::Vector2i(_wtoi(value.c_str()), 0));
		}
		if (field == L"WorkPointY")
		{
			object->WorkPoints.back().y = _wtoi(value.c_str());
		}
		if (field == L"Class")
		{
			customer->Class = value;
		}
		if (field == L"Deleted")
		{
			customer->Deleted = _wtoi(value.c_str());
		}
		if (field == L"ExpectedProducts")
		{
			customer->ExpectedProducts = _wtoi(value.c_str());
		}
		if (field == L"HygieneExpectation")
		{
			customer->HygieneExpectation = _wtof(value.c_str());
		}
		if (field == L"InLine")
		{
			customer->InLine = _wtoi(value.c_str());
		}
		if (field == L"LinePosition")
		{
			customer->LinePosition = _wtoi(value.c_str());
		}
		if (field == L"Money")
		{
			customer->Money = _wtof(value.c_str());
		}
		if (field == L"NextProduct")
		{
			customer->NextProduct = _wtoi(value.c_str());
		}
		if (field == L"CustomerObjectID")
		{
			if (value != L"-1")
			{
				Connection tmp;
				tmp.ConnectionType = Connection::Type::Customer;
				tmp.FirstID = entity->ID;
				tmp.SecondID = _wtoi(value.c_str());
				connections.push_back(tmp);
			}
		}
		if (field == L"PersonState")
		{
			customer->PersonState = (Customer::State)_wtoi(value.c_str());
		}
		if (field == L"PriceExpectancy")
		{
			customer->PriceExpectancy.push_back(_wtof(value.c_str()));
		}
		if (field == L"ProductFound")
		{
			customer->ProductsFound.push_back(_wtoi(value.c_str()));
		}
		if (field == L"ShoppingListName")
		{
			for (int i = 0; i < this->ProductList.size(); i++)
			{
				if (this->ProductList[i]->Name == std::string(value.begin(), value.end()))
				{
					customer->ShoppingList.push_back(this->ProductList[i]);
				}
			}
		}
		if (field == L"ShoppingState")
		{
			customer->ShoppingState = (Customer::ShopState)_wtoi(value.c_str());
		}
		if (field == L"StealingPotential")
		{
			customer->StealingPotential = _wtof(value.c_str());
		}
		if (field == L"Waited")
		{
			customer->Waited = _wtoi(value.c_str());
		}
		if (field == L"AtWorkplace")
		{
			worker->AtWorkplace = _wtoi(value.c_str());
		}
		if (field == L"CashieringEfficiency")
		{
			worker->CashieringEfficiency = _wtof(value.c_str());
		}
		if (field == L"CleaningEfficiency")
		{
			worker->CleaningEfficiency = _wtof(value.c_str());
		}
		if (field == L"WorkerObjectID")
		{
			if (value != L"-1")
			{
				Connection tmp;
				tmp.ConnectionType = Connection::Type::Worker;
				tmp.FirstID = entity->ID;
				tmp.SecondID = _wtoi(value.c_str());
				connections.push_back(tmp);
			}
		}
		if (field == L"ProductItem")
		{
			if (value != L"-1")
			{
				for (int i = 0; i < this->ProductList.size(); i++)
				{
					if (this->ProductList[i]->Name == std::string(value.begin(), value.end()))
					{
						worker->ProductItem = this->ProductList[i];
					}
				}
			}
		}
		if (field == L"RefillingEfficiency")
		{
			worker->RefillingEfficiency = _wtof(value.c_str());
		}
		if (field == L"Wage")
		{
			worker->Wage = _wtof(value.c_str());
		}
		if (field == L"WorkDay")
		{
			worker->WorkDays[day] = _wtoi(value.c_str());
			day++;
			if (day == 7)
			{
				day = 0;
			}
		}
		if (field == L"WorkerType")
		{
			worker->WorkerType = (Worker::Type)_wtoi(value.c_str());
		}
		if (field == L"WorkHours")
		{
			worker->WorkHours = _wtoi(value.c_str());
		}
		if (field == L"WorkState")
		{
			worker->WorkState = (Worker::State)_wtoi(value.c_str());
		}
		if (field == L"Age")
		{
			person->Age = _wtoi(value.c_str());
		}
		if (field == L"EmoticonTimer")
		{
			person->EmoticonTimer = _wtoi(value.c_str());
		}
		if (field == L"Happiness")
		{
			person->Happiness = _wtof(value.c_str());
		}
		if (field == L"Name")
		{
			person->Name = value;
		}
		if (field == L"PathX")
		{
			person->Path.push_back(sf::Vector2i(_wtoi(value.c_str()), 0));
		}
		if (field == L"PathY")
		{
			person->Path.back().y = _wtoi(value.c_str());
		}
		if (field == L"Gender")
		{
			person->PersonGender = (Person::Gender)_wtoi(value.c_str());
		}
		if (field == L"ShowEmotion")
		{
			person->ShowEmotion = _wtoi(value.c_str());
		}
		if (field == L"Speed")
		{
			person->Speed = _wtof(value.c_str());
		}
		if (field == L"WaitTimer")
		{
			person->WaitTimer = _wtof(value.c_str());
		}
		if (field == L"AnimPhase")
		{
			entity->AnimPhase = _wtoi(value.c_str());
		}
		if (field == L"AnimTimer")
		{
			entity->AnimTimer = _wtof(value.c_str());
		}
		if (field == L"DeleteMe")
		{
			entity->DeleteMe = _wtoi(value.c_str());
		}
		if (field == L"GridPosX")
		{
			entity->GridPosition = sf::Vector2f(_wtof(value.c_str()), 0);
		}
		if (field == L"GridPosY")
		{
			entity->GridPosition.y = _wtof(value.c_str());
		}
		if (field == L"ID")
		{
			entity->ID = _wtoi(value.c_str());
		}
		if (field == L"LowestGridPositionX")
		{
			entity->LowestGridPosition = sf::Vector2f(_wtof(value.c_str()), 0);
		}
		if (field == L"LowestGridPositionY")
		{
			entity->LowestGridPosition.y = _wtof(value.c_str());
		}
		if (field == L"LowestScreenPositionX")
		{
			entity->LowestScreenPosition = sf::Vector2f(_wtof(value.c_str()), 0);
		}
		if (field == L"LowestScreenPositionY")
		{
			entity->LowestScreenPosition.y = _wtof(value.c_str());
		}
		if (field == L"Moved")
		{
			entity->Moved = _wtoi(value.c_str());
		}
		if (field == L"PreviousGridPositionX")
		{
			entity->PreviousGridPosition = sf::Vector2i(_wtoi(value.c_str()), 0);
		}
		if (field == L"PreviousGridPositionY")
		{
			entity->PreviousGridPosition.y = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductName") != std::wstring::npos)
		{
			std::wstring istr = field;
			istr.erase(0, 17);
			pos = _wtoi(istr.c_str());

			if (entity->Products.size() <= pos)
			{
				entity->Products.resize(pos + 1);
			}
			entity->Products[pos].push_back(Product());
			entity->Products[pos].back().Name = std::string(value.begin(), value.end());
			entity->Products[pos].back().Sprite = this->TexHandler.GetSprite(entity->Products[pos].back().Name)[0][0];
		}
		if (field.find(L"EntityProductAmount") != std::wstring::npos)
		{
			entity->Products[pos].back().Amount = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductDaysToExp") != std::wstring::npos)
		{
			entity->Products[pos].back().DaysToExpire = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductExDays") != std::wstring::npos)
		{
			entity->Products[pos].back().ExistenceDays = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductExpDay") != std::wstring::npos)
		{
			entity->Products[pos].back().ExpirationDate.Day = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductExpMonth") != std::wstring::npos)
		{
			entity->Products[pos].back().ExpirationDate.Month = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductExpYear") != std::wstring::npos)
		{
			entity->Products[pos].back().ExpirationDate.Year = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductExpState") != std::wstring::npos)
		{
			entity->Products[pos].back().ExpirationState = (Product::State)_wtoi(value.c_str());
		}
		if (field.find(L"EntityProductLastDay") != std::wstring::npos)
		{
			entity->Products[pos].back().LastDay = _wtoi(value.c_str());
		}
		if (field.find(L"EntityProductPrice") != std::wstring::npos)
		{
			entity->Products[pos].back().Price = _wtof(value.c_str());
		}
		if (field == L"Rotation")
		{
			entity->Rotation = _wtoi(value.c_str());
		}
		if (field == L"SpriteName")
		{
			entity->SpriteName = std::string(value.begin(), value.end());
		}
		if (field == L"NextPosX")
		{
			animal->NextPos = sf::Vector2i(_wtoi(value.c_str()), 0);
		}
		if (field == L"NextPosY")
		{
			animal->NextPos.y = _wtoi(value.c_str());
		}
		if (field == L"AnimalWaitTimer")
		{
			animal->WaitTimer = _wtof(value.c_str());
		}
		if (field == L"Walking")
		{
			animal->Walking = _wtoi(value.c_str());
		}
		if (field == L"ToBeDeleted")
		{
			animal->ToBeDeleted = _wtoi(value.c_str());
		}
		if (field == L"ToBeDeletedAgain")
		{
			animal->ToBeDeletedAgain = _wtoi(value.c_str());
		}
		if (field == L"LifeTime")
		{
			animal->LifeTime = _wtof(value.c_str());
		}
		if (field == L"AnimalName")
		{
			animal->Name = value;
		}

		field.clear();
		value.clear();
	}
	if (entity != 0)
	{
		if (dynamic_cast<Animal*>(entity))
		{
			entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName);
			entity->BackSprite = this->TexHandler.GetSprite(entity->SpriteName);
			entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);
			entity->Grid = &this->CurrentStore->Grid;
			this->CurrentStore->Grid[entity->GridPosition.x][entity->GridPosition.y].push_back(entity);
		}
		else if (dynamic_cast<Person*>(entity))
		{
			entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName + "_Front");
			entity->BackSprite = this->TexHandler.GetSprite(entity->SpriteName + "_Back");
			person->Emoticons = this->TexHandler.GetSprite("Emotion");
			for (int i = 0; i < person->Emoticons[0].size(); i++)
			{
				person->Emoticons[0][i]->setOrigin(person->Emoticons[0][i]->getGlobalBounds().width / 2, person->Emoticons[0][i]->getGlobalBounds().height);
			}
			person->ThePathfinder = &this->ThePathfinder;
			person->Messages = &this->CurrentStore->Messages;
			person->Grid = &this->CurrentStore->Grid;
			person->Feed = &this->Feed;
			if (dynamic_cast<Customer*>(entity))
			{
				customer->Products.resize(customer->ShoppingList.size());
			}
			if (dynamic_cast<Worker*>(entity))
			{
				worker->Jobs = &this->CurrentStore->Jobs;
				worker->TheStorage = &this->CurrentStore->ProductStorage;
				worker->calculateWageExpectency();
			}
			entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);
			this->CurrentStore->Grid[entity->GridPosition.x][entity->GridPosition.y].push_back(entity);
		}
		else
		{
			object->Products.resize(object->ProdsPerSlot);
			if (this->TexHandler.Exists(entity->SpriteName + "_Front"))
			{
				entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName + "_Front");
				//entity->BackSprite = this->TexHandler.GetSprite(entity->SpriteName + "_Back");
			}
			else
			{
				entity->Sprite = this->TexHandler.GetSprite(entity->SpriteName);
			}
			entity->Origins = this->TexHandler.GetOrigins(entity->SpriteName);
			if (object->ObjectType == StoreObject::Type::Floor && object->ObjectQuality != StoreObject::Quality::Nothing)
			{
				object->DirtSprites.resize(3);
				if (rand() % 2)
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Dirt_Floor11")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Dirt_Floor12")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Dirt_Floor13")[0][0];
				}
				else
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Dirt_Floor21")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Dirt_Floor22")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Dirt_Floor23")[0][0];
				}
			}
			else if (object->ObjectType == StoreObject::Type::Shelf)
			{
				object->DirtSprites.resize(3);
				if (object->ObjectQuality == StoreObject::Quality::Illegal)
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt1")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt1")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Illegal_Dirt2")[0][0];
				}
				else if (object->ObjectQuality == StoreObject::Quality::Cheap)
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt1")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt1")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Cheap_Dirt2")[0][0];
				}
				else if (object->ObjectQuality == StoreObject::Quality::Normal)
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Normal_Dirt1")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Normal_Dirt1")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Normal_Dirt2")[0][0];
				}
				else if (object->ObjectQuality == StoreObject::Quality::Premium)
				{
					object->DirtSprites[0] = this->TexHandler.GetSprite("Shelf_Premium_Dirt1")[0][0];
					object->DirtSprites[1] = this->TexHandler.GetSprite("Shelf_Premium_Dirt1")[0][0];
					object->DirtSprites[2] = this->TexHandler.GetSprite("Shelf_Premium_Dirt2")[0][0];
				}

				if (object->Rotation % 2)
				{
					for (int i = 0; i < 3; i++)
					{
						object->DirtSprites[i]->scale(-1.0f, 1.0f);
					}
				}
			}

			object->Feed = &this->Feed;
			entity->Grid = &this->CurrentStore->Grid;
			
			for (int i = 0; i <= abs(object->Width); i++)
			{
				for (int j = 0; j <= abs(object->Height); j++)
				{
					int setPosX = entity->GridPosition.x + i * (1 + (object->Width < 0) * -2);
					int setPosY = entity->GridPosition.y + j * (1 + (object->Height < 0) * -2);

					if (object->WorkPoints.size() == 0 ||
						(setPosX != entity->GridPosition.x + object->WorkPoints[0].x ||
						setPosY != entity->GridPosition.y + object->WorkPoints[0].y))
					{
						this->CurrentStore->Grid[setPosX][setPosY].push_back(entity);
						this->CurrentStore->CostGrid[setPosX][setPosY] = 0;
						if (object->ObjectType == StoreObject::Type::Floor)
						{
							this->CurrentStore->CostGrid[setPosX][setPosY] = 50;
						}
					}
				}
			}
		}
	}


	this->Bob.AdjustCostGrid(&this->CurrentStore->Grid, &this->CurrentStore->CostGrid);
	this->CurrentStore->PeepGenerator.Grid = &this->CurrentStore->Grid;
	this->CurrentStore->PeepGenerator.Messages = &this->CurrentStore->Messages;
	this->CurrentStore->PeepGenerator.Jobs = &this->CurrentStore->Jobs;
	this->CurrentStore->PeepGenerator.ProductStorage = &this->CurrentStore->ProductStorage;
	this->CurrentStore->PeepGenerator.Feed = &this->Feed;
	this->CurrentStore->ProductStorage.ProductList = &this->ProductList;
	this->CurrentStore->ProductStorage.TexHandler = &this->TexHandler;
	this->CurrentStore->ProductStorage.Feed = &this->Feed;
	this->Data.IllegalStuff = &this->IllegalStuff;
	this->Data.SoundHandler = &this->SoundHandler;
	

	std::list<Connection>::iterator iter = connections.begin();
	std::list<Connection>::iterator end = connections.end();

	while (iter != end)
	{
		bool found = false;

		if (iter->ConnectionType == Connection::Type::Message)
		{
			std::list<StoreMessage>::iterator mesiter = this->CurrentStore->Messages.begin();
			std::list<StoreMessage>::iterator mesend = this->CurrentStore->Messages.end();

			while (mesiter != mesend && mesiter->ID != iter->FirstID)
			{
				mesiter++;
			}

			std::list<Entity*>::iterator entiter;
			std::list<Entity*>::iterator entend;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					entiter = this->CurrentStore->Grid[i][j].begin();
					entend = this->CurrentStore->Grid[i][j].end();

					while (entiter != entend)
					{
						if ((*entiter)->ID == iter->SecondID)
						{
							mesiter->Pointer = dynamic_cast<StoreObject*>(*entiter);
							break;
						}

						entiter++;
					}
				}
			}
		}

		if (iter->ConnectionType == Connection::Type::Job)
		{
			std::list<StoreJob>::iterator jobiter = this->CurrentStore->Jobs.begin();
			std::list<StoreJob>::iterator jobend = this->CurrentStore->Jobs.end();

			while (jobiter != jobend && (jobiter->JobType != iter->FirstID || (jobiter->JobType == iter->FirstID && jobiter->Object != 0)))
			{
				jobiter++;
			}

			std::list<Entity*>::iterator entiter;
			std::list<Entity*>::iterator entend;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					entiter = this->CurrentStore->Grid[i][j].begin();
					entend = this->CurrentStore->Grid[i][j].end();

					while (entiter != entend)
					{
						if ((*entiter)->ID == iter->SecondID)
						{
							jobiter->Object = dynamic_cast<StoreObject*>(*entiter);
							break;
						}

						entiter++;
					}
				}
			}
		}

		if (iter->ConnectionType == Connection::Type::StoreObject)
		{
			std::list<Entity*>::iterator entiter1;
			std::list<Entity*>::iterator entend1;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter1 = this->CurrentStore->Grid[i][j].begin();
						entend1 = this->CurrentStore->Grid[i][j].end();

						while (entiter1 != entend1)
						{
							if ((*entiter1)->ID == iter->FirstID)
							{
								found = true;
								break;
							}
							entiter1++;
						}
					}
				}
			}

			found = false;

			std::list<Entity*>::iterator entiter2;
			std::list<Entity*>::iterator entend2;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter2 = this->CurrentStore->Grid[i][j].begin();
						entend2 = this->CurrentStore->Grid[i][j].end();

						while (entiter2 != entend2)
						{
							if ((*entiter2)->ID == iter->SecondID)
							{
								found = true;
								break;
							}
							entiter2++;
						}
					}
				}
			}

			dynamic_cast<StoreObject*>(*entiter1)->CurrentWorker = dynamic_cast<Worker*>(*entiter2);
		}

		if (iter->ConnectionType == Connection::Type::Worker)
		{
			std::list<Entity*>::iterator entiter1;
			std::list<Entity*>::iterator entend1;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter1 = this->CurrentStore->Grid[i][j].begin();
						entend1 = this->CurrentStore->Grid[i][j].end();

						while (entiter1 != entend1)
						{
							if ((*entiter1)->ID == iter->FirstID)
							{
								found = true;
								break;
							}
							entiter1++;
						}
					}
				}
			}

			found = false;

			std::list<Entity*>::iterator entiter2;
			std::list<Entity*>::iterator entend2;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter2 = this->CurrentStore->Grid[i][j].begin();
						entend2 = this->CurrentStore->Grid[i][j].end();

						while (entiter2 != entend2)
						{
							if ((*entiter2)->ID == iter->SecondID)
							{
								found = true;
								break;
							}
							entiter2++;
						}
					}
				}
			}

			dynamic_cast<Worker*>(*entiter1)->Object = dynamic_cast<StoreObject*>(*entiter2);
		}

		if (iter->ConnectionType == Connection::Type::Customer)
		{
			std::list<Entity*>::iterator entiter1;
			std::list<Entity*>::iterator entend1;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter1 = this->CurrentStore->Grid[i][j].begin();
						entend1 = this->CurrentStore->Grid[i][j].end();

						while (entiter1 != entend1)
						{
							if ((*entiter1)->ID == iter->FirstID)
							{
								found = true;
								break;
							}
							entiter1++;
						}
					}
				}
			}

			found = false;

			std::list<Entity*>::iterator entiter2;
			std::list<Entity*>::iterator entend2;

			for (int i = 0; i < this->CurrentStore->Grid.size(); i++)
			{
				for (int j = 0; j < this->CurrentStore->Grid[i].size(); j++)
				{
					if (!found)
					{
						entiter2 = this->CurrentStore->Grid[i][j].begin();
						entend2 = this->CurrentStore->Grid[i][j].end();

						while (entiter2 != entend2)
						{
							if ((*entiter2)->ID == iter->SecondID)
							{
								found = true;
								break;
							}
							entiter2++;
						}
					}
				}
			}

			dynamic_cast<Customer*>(*entiter1)->Object = dynamic_cast<StoreObject*>(*entiter2);
		}

		iter++;
	}


	file.close();
}