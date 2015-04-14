#include "EntityWindow.h"


EntityWindow::EntityWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::list<StoreJob>* jobs)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->IsOpen = false;
	this->SelectedEntity = 0;
	this->EntityChanged = true;
	this->TexHandler = texHandler;
	this->Jobs = jobs;

	this->InfoText.setFont(*texHandler->GetFont());
	this->InfoText.setCharacterSize(14);
	this->InfoText.setColor(sf::Color(128, 242, 152, 255));
	this->InfoText.setPosition(position + sf::Vector2f(30, 20));
	this->InfoText.setString("");

	this->Buttons.push_back(new Bar("HappinessBar", L"Zufriedenheit", texHandler->GetSprite("InterfaceBar")[0][0], texHandler->GetSprite("InterfaceBarFill")[0][0], texHandler, position, sf::Vector2f(120, 200)));
	this->Buttons.back()->Visible = false;

	std::vector<std::string> strings;
	strings.push_back("Putzen");
	strings.push_back("Lager");
	strings.push_back("Kasse");
	this->Buttons.push_back(new DropDownButton("Job", texHandler->GetSprite("DropDown")[0][0], texHandler->GetSprite("DropDownList")[0][0], strings, texHandler, position, sf::Vector2f(400, 20)));
	this->Buttons.back()->Text.setString(L"Zuständigkeit");
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(-100, 5));
	this->Buttons.back()->Visible = false;

	this->Buttons.push_back(new TextButton("Wage", TextButton::Type::Price, texHandler->GetSprite("TextButton")[0][0], texHandler, position, sf::Vector2f(400, 150)));
	this->Buttons.back()->Text.setString("Gehalt (pro Stunde)");
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(-130, 5));
	this->Buttons.back()->Visible = false;

	this->Buttons.push_back(new InterfaceButton("Butcher", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], position, sf::Vector2f(400, 20)));
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Text.setString("Verarbeiten");
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Visible = false;
}


EntityWindow::~EntityWindow(void)
{
}


void EntityWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	if (this->EntityChanged)
	{
		if (this->SelectedEntity != 0)
		{
			if (dynamic_cast<StoreObject*>(this->SelectedEntity))
			{
				std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
				std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

				while (anotheriter != anotherend)
				{
					(*anotheriter)->Visible = false;

					anotheriter++;
				}

				this->InfoText.setString("");

				std::vector<ProductListItem*> tmp = dynamic_cast<StoreObject*>(this->SelectedEntity)->Slots;

				std::vector<sf::Sprite*>::iterator iter = this->ProductSprites.begin();
				std::vector<sf::Sprite*>::iterator end = this->ProductSprites.end();

				while (iter != end)
				{
					delete *iter;

					iter++;
				}

				this->ProductSprites.clear();
				this->ProductSprites.resize(tmp.size());

				this->OtherButtons.resize(tmp.size() * 2);
				this->Texts.resize(tmp.size());

				for (int i = 0; i < tmp.size(); i++)
				{
					this->OtherButtons[i] = TextButton("Price", TextButton::Type::Price, this->TexHandler->GetSprite("TextButton")[0][0], this->TexHandler, this->Sprite->getPosition(), sf::Vector2f(i * 150 + 30, 140));
					this->OtherButtons[i].Text.setPosition(this->OtherButtons[i].Sprite->getPosition().x + 20, this->OtherButtons[i].Sprite->getPosition().y - 16);
					this->OtherButtons[i].Text.setString("Preis");
					if (dynamic_cast<StoreObject*>(this->SelectedEntity)->Slots[i])
					{
						this->OtherButtons[i].setValue(dynamic_cast<StoreObject*>(this->SelectedEntity)->Prices[i]);
					}

					this->OtherButtons[i + tmp.size()] = TextButton("ExpDate", TextButton::Type::ExpDate, this->TexHandler->GetSprite("TextButton")[0][0], this->TexHandler, this->Sprite->getPosition(), sf::Vector2f(i * 150 + 30, 185));
					this->OtherButtons[i + tmp.size()].Text.setPosition(this->OtherButtons[i + tmp.size()].Sprite->getPosition().x - 15, this->OtherButtons[i + tmp.size()].Sprite->getPosition().y - 16);
					this->OtherButtons[i + tmp.size()].Text.setString("Haltbarkeitsdatum");
					if (dynamic_cast<StoreObject*>(this->SelectedEntity)->Slots[i])
					{
						this->OtherButtons[i + tmp.size()].setValue(dynamic_cast<StoreObject*>(this->SelectedEntity)->Dates[i]);
					}

					this->Texts[i].setCharacterSize(14);
					this->Texts[i].setColor(sf::Color::Black);
					this->Texts[i].setFont(*this->TexHandler->GetFont());
					this->Texts[i].setPosition(this->Sprite->getPosition() + sf::Vector2f(i * 150 + 15, 220));
					if (!this->SelectedEntity->Products[i].empty() && this->SelectedEntity->Products[i].front().ExpirationState == Product::State::Good)
					{
						this->Texts[i].setString("Zustand: Gut");
					}
					else if (!this->SelectedEntity->Products[i].empty() && this->SelectedEntity->Products[i].front().ExpirationState == Product::State::Expired)
					{
						this->Texts[i].setString("Zustand: Abgelaufen");
					}
					else if (!this->SelectedEntity->Products[i].empty() && this->SelectedEntity->Products[i].front().ExpirationState == Product::State::Rotten)
					{
						this->Texts[i].setString("Zustand: Vergammelt");
					}
				}

				this->Rectangles.resize(tmp.size());

				for (int i = 0; i < tmp.size(); i++)
				{
					if (tmp[i] != 0)
					{
						this->ProductSprites[i] = this->TexHandler->GetSprite(tmp[i]->Name)[0][0];
						this->ProductSprites[i]->scale(3, 3);
						this->ProductSprites[i]->setPosition(sf::Vector2f(this->Sprite->getPosition().x + i * 150 + 20, this->Sprite->getPosition().y + 20));
					}

					sf::RectangleShape rect;
					rect.setSize(sf::Vector2f(100, 100));
					rect.setFillColor(sf::Color::Transparent);
					rect.setOutlineColor(sf::Color::White);
					rect.setOutlineThickness(2);
					rect.setPosition(sf::Vector2f(this->Sprite->getPosition().x + i * 150 + 20, this->Sprite->getPosition().y + 20));

					this->Rectangles[i] = rect;
				}
			}
			else if (dynamic_cast<Customer*>(this->SelectedEntity))
			{
				std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
				std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

				while (anotheriter != anotherend)
				{
					if ((*anotheriter)->Name == "HappinessBar")
					{
						(*anotheriter)->Visible = true;
					}
					if ((*anotheriter)->Name == "Job")
					{
						(*anotheriter)->Visible = false;
					}
					if ((*anotheriter)->Name == "Wage")
					{
						(*anotheriter)->Visible = false;
					}
					if ((*anotheriter)->Name == "Butcher")
					{
						(*anotheriter)->Visible = false;
					}

					anotheriter++;
				}


				std::vector<ProductListItem*> tmp = dynamic_cast<Customer*>(this->SelectedEntity)->ShoppingList;

				std::vector<sf::Sprite*>::iterator iter = this->ProductSprites.begin();
				std::vector<sf::Sprite*>::iterator end = this->ProductSprites.end();

				while (iter != end)
				{
					delete *iter;

					iter++;
				}


				this->ProductSprites.clear();
				this->ProductSprites.resize(tmp.size());
				this->Rectangles.resize(tmp.size());
				this->OtherButtons.clear();
				this->Texts.clear();

				for (int i = 0; i < tmp.size(); i++)
				{
					if (tmp[i] != 0)
					{
						this->ProductSprites[i] = this->TexHandler->GetSprite(tmp[i]->Name)[0][0];
						this->ProductSprites[i]->setPosition(sf::Vector2f(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width - (i % 4) * 50 - 50, this->Sprite->getPosition().y + 20 + (i > 3) * 50 + (i > 7) * 50 + (i > 11) * 50));
					}

					sf::RectangleShape rect;
					rect.setSize(sf::Vector2f(32, 32));
					rect.setFillColor(sf::Color::Transparent);
					rect.setOutlineColor(sf::Color::White);
					rect.setOutlineThickness(2);
					rect.setPosition(sf::Vector2f(this->Sprite->getPosition().x + this->Sprite->getGlobalBounds().width - (i % 4) * 50 - 50, this->Sprite->getPosition().y + 20 + (i > 3) * 50 + (i > 7) * 50 + (i > 11) * 50));

					this->Rectangles[i] = rect;
				}
			}
			else if (dynamic_cast<Worker*>(this->SelectedEntity))
			{
				std::vector<sf::Sprite*>::iterator iter = this->ProductSprites.begin();
				std::vector<sf::Sprite*>::iterator end = this->ProductSprites.end();

				while (iter != end)
				{
					delete *iter;

					iter++;
				}

				this->ProductSprites.clear();
				this->Rectangles.clear();
				this->OtherButtons.clear();
				this->Texts.clear();

				std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
				std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

				while (anotheriter != anotherend)
				{
					if ((*anotheriter)->Name == "HappinessBar")
					{
						(*anotheriter)->Visible = true;
					}
					if ((*anotheriter)->Name == "Job")
					{
						(*anotheriter)->Visible = true;

						if (dynamic_cast<Worker*>(this->SelectedEntity)->WorkerType == Worker::Type::Cashier)
						{
							dynamic_cast<DropDownButton*>(*anotheriter)->Selected = "Kasse";
						}
						if (dynamic_cast<Worker*>(this->SelectedEntity)->WorkerType == Worker::Type::Cleaner)
						{
							dynamic_cast<DropDownButton*>(*anotheriter)->Selected = "Putzen";
						}
						if (dynamic_cast<Worker*>(this->SelectedEntity)->WorkerType == Worker::Type::Storager)
						{
							dynamic_cast<DropDownButton*>(*anotheriter)->Selected = "Lager";
						}
					}
					if ((*anotheriter)->Name == "Wage")
					{
						(*anotheriter)->Visible = true;
						
						dynamic_cast<TextButton*>(*anotheriter)->setValue(dynamic_cast<Worker*>(this->SelectedEntity)->Wage);
					}
					if ((*anotheriter)->Name == "Butcher")
					{
						(*anotheriter)->Visible = false;
					}

					anotheriter++;
				}

			}
			else if (dynamic_cast<Animal*>(this->SelectedEntity))
			{
				std::vector<sf::Sprite*>::iterator iter = this->ProductSprites.begin();
				std::vector<sf::Sprite*>::iterator end = this->ProductSprites.end();

				while (iter != end)
				{
					delete *iter;

					iter++;
				}

				this->ProductSprites.clear();
				this->Rectangles.clear();
				this->OtherButtons.clear();
				this->Texts.clear();

				std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
				std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

				while (anotheriter != anotherend)
				{
					(*anotheriter)->Visible = false;
					if ((*anotheriter)->Name == "Butcher")
					{
						(*anotheriter)->Visible = true;
					}

					anotheriter++;
				}
			}

			this->EntityChanged = false;
		}
	}



	Customer* customer = dynamic_cast<Customer*>(this->SelectedEntity);
	if (customer)
	{
		for (int i = 0; i < customer->ProductsFound.size(); i++)
		{
			if (customer->ProductsFound[i])
			{
				this->Rectangles[i].setOutlineColor(sf::Color::Green);
			}
		}

		std::wstringstream infoString;
		infoString << L"Name: " << customer->Name << L"\n";
		infoString << L"Alter: " << customer->Age << L"\n";
		infoString << L"Klasse: " << customer->Class << L"\n";
		infoString << std::fixed << std::setprecision(2) << L"Geld: " << customer->Money << L"€" << L"\n";
		this->InfoText.setString(infoString.str());

		dynamic_cast<Bar*>(this->Buttons.front())->SetValue(customer->Happiness);

		if (customer->Deleted)
		{
			this->SelectedEntity = 0;
			this->close();
		}
	}



	Worker* worker = dynamic_cast<Worker*>(this->SelectedEntity);
	if (worker)
	{
		std::wstringstream infoString;
		infoString << L"Name: " << worker->Name << L"\n";
		this->InfoText.setString(infoString.str());

		dynamic_cast<Bar*>(this->Buttons.front())->SetValue(worker->Happiness);

		std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
		std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

		while (iter != end)
		{
			if ((*iter)->WasPressed)
			{
				if ((*iter)->Name == "Job")
				{
					if (dynamic_cast<DropDownButton*>(*iter)->Selected == "Kasse")
					{
						worker->WorkerType = Worker::Type::Cashier;
					}
					if (dynamic_cast<DropDownButton*>(*iter)->Selected == "Putzen")
					{
						worker->WorkerType = Worker::Type::Cleaner;
					}
					if (dynamic_cast<DropDownButton*>(*iter)->Selected == "Lager")
					{
						worker->WorkerType = Worker::Type::Storager;
					}
				}
				if ((*iter)->Name == "Wage")
				{
					worker->Wage = atof(dynamic_cast<TextButton*>(*iter)->Value.c_str());
				}
			}
			iter++;
		}
	}

	Animal* animal = dynamic_cast<Animal*>(this->SelectedEntity);
	if (animal)
	{
		std::wstringstream infoString;
		infoString << L"Name: " << animal->Name << L"\n";
		this->InfoText.setString(infoString.str());

		if (animal->ToBeDeleted)
		{
			this->SelectedEntity = 0;
			this->close();
		}
	}


	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			(*iter)->update(input);

			if ((*iter)->JustPressed && (*iter)->Name == "Butcher")
			{
				StoreJob tmp;
				tmp.JobType = StoreJob::Type::Butchering;
				tmp.Object = this->SelectedEntity;
				tmp.ProductItem = 0;
				this->Jobs->push_back(tmp);
			}
		}
		iter++;
	}

	for (int i = 0; i < this->OtherButtons.size(); i++)
	{
		this->OtherButtons[i].update(input);

		if (this->OtherButtons[i].WasPressed)
		{
			StoreObject* object = dynamic_cast<StoreObject*>(this->SelectedEntity);
			if (object && object->Slots[i])
			{
				std::list<Product>::iterator anotheriter;
				std::list<Product>::iterator anotherend;

				if (i < this->OtherButtons.size() / 2)
				{
					object->Prices[i] = atof(this->OtherButtons[i].Value.c_str());

					if (object->Products[i].size() > 0)
					{
						anotheriter = object->Products[i % (this->OtherButtons.size() / 2)].begin();
						anotherend = object->Products[i % (this->OtherButtons.size() / 2)].end();

						while (anotheriter != anotherend)
						{
							anotheriter->Price = atof(this->OtherButtons[i].Value.c_str());

							anotheriter++;
						}

					}
				}
				else
				{
					Date date;

					std::string day;
					std::string month;
					std::string year;
					int stage = 0;

					std::string::iterator yetanotheriter = this->OtherButtons[i].Value.begin();
					std::string::iterator yetanotherend = this->OtherButtons[i].Value.end();
					while (yetanotheriter != yetanotherend)
					{
						if (*yetanotheriter == '.')
						{
							stage++;
							yetanotheriter++;
						}

						if (stage == 0)
						{
							day += *yetanotheriter;
						}
						else if (stage == 1)
						{
							month += *yetanotheriter;
						}
						else if (stage == 2)
						{
							year += *yetanotheriter;
						}

						yetanotheriter++;
					}

					date.Day = atoi(day.c_str());
					date.Month = atoi(month.c_str());
					date.Year = atoi(year.c_str());

					object->Dates[i] = date;

					if (object->Products[i].size() > 0)
					{
						anotheriter = object->Products[i % (this->OtherButtons.size() / 2)].begin();
						anotherend = object->Products[i % (this->OtherButtons.size() / 2)].end();

						while (anotheriter != anotherend)
						{
							anotheriter->ExpirationDate = date;

							anotheriter++;
						}
					}
				}
			}
		}
	}


	if (input->JustClicked(sf::Mouse::Right) && dynamic_cast<StoreObject*>(this->SelectedEntity))
	{
		for (int i = 0; i < this->Rectangles.size(); i++)
		{
			if (this->Rectangles[i].getGlobalBounds().contains(input->GetMousePos().x, input->GetMousePos().y))
			{
				delete this->ProductSprites[i];
				this->ProductSprites[i] = 0;

				delete dynamic_cast<StoreObject*>(this->SelectedEntity)->Slots[i];
				dynamic_cast<StoreObject*>(this->SelectedEntity)->Slots[i] = 0;
			}
		}
	}
}


void EntityWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			target.draw(**iter);
		}
		iter++;
	}

	if (this->SelectedEntity != 0)
	{
		for (int i = 0; i < this->ProductSprites.size(); i++)
		{
			target.draw(this->Rectangles[i]);

			if (this->ProductSprites[i] != 0)
			{
				target.draw(*this->ProductSprites[i]);
			}
		}

		for (int i = 0; i < this->OtherButtons.size(); i++)
		{
			target.draw(this->OtherButtons[i]);
		}

		for (int i = 0; i < this->Texts.size(); i++)
		{
			target.draw(this->Texts[i]);
		}
	}


	target.draw(this->InfoText);
}


void EntityWindow::PutObject(ProductListItem* object, sf::Sprite* sprite, Input* input, GameData* gameData, Storage* storage, std::list<StoreJob>* jobs)
{
	for (int i = 0; i < this->Rectangles.size(); i++)
	{
		if (this->Rectangles[i].getGlobalBounds().contains(input->GetMousePos().x, input->GetMousePos().y))
		{
			StoreObject* entity = dynamic_cast<StoreObject*>(this->SelectedEntity);

			entity->Slots[i] = object;
			entity->Prices[i] = object->Price;

			entity->Dates[i] = gameData->Date;

			for (int j = 0; j < object->MinDaysToExpire; j++)
			{
				entity->Dates[i].Day++;

				if (entity->Dates[i].Month == 2)
				{
					if (entity->Dates[i].Day == 29)
					{
						entity->Dates[i].Day = 1;
						entity->Dates[i].Month++;
					}
				}
				else if ((entity->Dates[i].Month % 2 == 0 && entity->Dates[i].Month < 7) || (entity->Dates[i].Month % 2 != 0 && entity->Dates[i].Month > 7))
				{
					if (entity->Dates[i].Day == 31)
					{
						entity->Dates[i].Day = 1;
						entity->Dates[i].Month++;
					}
				}
				else
				{
					if (entity->Dates[i].Day == 32)
					{
						entity->Dates[i].Day = 1;
						entity->Dates[i].Month++;
					}
				}


				if (entity->Dates[i].Month == 13)
				{
					entity->Dates[i].Month = 1;
					entity->Dates[i].Year++;
				}
			}

			this->OtherButtons[i].setValue(object->Price);
			this->OtherButtons[i + this->OtherButtons.size() / 2].setValue(entity->Dates[i]);


			this->ProductSprites[i] = new sf::Sprite(*sprite);
			this->ProductSprites[i]->setPosition(sf::Vector2f(this->Sprite->getPosition().x + i * 150 + 20, this->Sprite->getPosition().y + 20));

			StoreJob tmp;
			tmp.JobType = StoreJob::Type::Refilling;
			tmp.Object = dynamic_cast<StoreObject*>(this->SelectedEntity);
			tmp.ProductItem = object;
			jobs->push_back(tmp);
		}
	}
}