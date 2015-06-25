#include "ProductsWindow.h"


ProductsWindow::ProductsWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, std::vector<ProductListItem*>* productList, TextureHandler* texHandler, Storage* storage)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->Sprite->scale(1, 2);
	this->IsOpen = false;
	this->StorageOnly = false;
	this->ProductList = productList;
	this->loadButtons(texHandler, productList);
	this->CurrentButton = 0;
	this->CurrentItem = 0;
	this->HoverWindowSprite = texHandler->GetSprite("Window")[0][0];
	this->HoverWindowSprite->setScale(0.3f, 0.6f);
	this->HoverWindowText.setFont(*texHandler->GetFont());
	this->HoverWindowText.setCharacterSize(14);
	this->HoverWindowText.setColor(sf::Color(128, 242, 152, 255));
	this->HoverWindowText.setPosition(position + sf::Vector2f(500, 300));
	this->PriceText.setFont(*texHandler->GetFont());
	this->PriceText.setCharacterSize(14);
	this->PriceText.setColor(sf::Color(128, 242, 152, 255));
	this->PriceText.setPosition(position + sf::Vector2f(920, 450));
	this->PriceText.setString(L"0.00€");
	this->ProductStorage = storage;
	this->TexHandler = texHandler;
	this->AnotherCurrentButton = 0;
}																	   
																	
																	   
ProductsWindow::~ProductsWindow(void)								   
{
}


void ProductsWindow::loadButtons(TextureHandler* texHandler, std::vector<ProductListItem*>* productList)
{
	/*this->Buttons.clear();
	this->FilterButtons.clear();

	this->FilterButtons.push_back(InterfaceButton("Close", InterfaceButton::Type::Normal, texHandler->GetSprite("CloseButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(950, 30)));
	this->FilterButtons.push_back(InterfaceButton("Buy", InterfaceButton::Type::Normal, texHandler->GetSprite("BuyButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(915, 420)));

	std::vector<ProductListItem*>::iterator iter = productList->begin();
	std::vector<ProductListItem*>::iterator end = productList->end();

	int x1 = 300;
	int y1 = 100;
	int x2 = 30;
	int y2 = 50;

	while (iter != end)
	{
		InterfaceButton* tmp = new InterfaceButton((*iter)->Name, InterfaceButton::Type::Permanent, texHandler->GetSprite((*iter)->Name)[0][0], this->Sprite->getPosition(), sf::Vector2f(x1, y1));
		tmp->Sprite->scale(3, 3);
		tmp->Text.setFont(*texHandler->GetFont());
		tmp->Text.setString((*iter)->Name);
		tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(15, -15));

		this->Buttons.push_back(tmp);

		tmp = new Slider((*iter)->Name + "Slider", -0.1, 0.1, Slider::Type::Centered, texHandler->GetSprite("Slider")[0][0], texHandler->GetSprite("SliderSlider")[0][0], this->Sprite->getPosition(), sf::Vector2f(x1, y1 + 50));
		tmp->Sprite->setScale(0.8, 1);
		tmp->Text.setFont(*texHandler->GetFont());
		tmp->Text.setString("0");
		tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(100, 0));

		this->Buttons.push_back(tmp);

		x1 += 100;
		if (x1 > 900)
		{
			x1 = 0;
			y1 += 100;
		}


		std::list<InterfaceButton>::iterator anotheriter = this->FilterButtons.begin();
		std::list<InterfaceButton>::iterator anotherend = this->FilterButtons.end();

		bool existing = false;

		while (anotheriter != anotherend)
		{
			if (anotheriter->Name == (*iter)->Category)
			{
				existing = true;
			}

			anotheriter++;
		}

		if (!existing)
		{
			InterfaceButton temp = InterfaceButton((*iter)->Category, InterfaceButton::Permanent, texHandler->GetSprite("CheckBox")[0][0], this->Sprite->getPosition(), sf::Vector2f(x2, y2));
			temp.Text.setFont(*texHandler->GetFont());
			temp.Text.setString((*iter)->Category);
			temp.Text.setPosition(temp.Sprite->getPosition() + sf::Vector2f(15, -2));
			temp.Text.setCharacterSize(10);
			this->FilterButtons.push_back(temp);

			y2 += 30;
		}


		iter++;
	}

	x2 = 135;
	y2 = 50;

	InterfaceButton temp = InterfaceButton("Illegal", InterfaceButton::Permanent, texHandler->GetSprite("CheckBox")[0][0], this->Sprite->getPosition(), sf::Vector2f(x2, y2));
	temp.Text.setFont(*texHandler->GetFont());
	temp.Text.setString("Illegal");
	temp.Text.setPosition(temp.Sprite->getPosition() + sf::Vector2f(15, -2));
	temp.Text.setCharacterSize(10);
	this->FilterButtons.push_back(temp);

	y2 += 30;

	temp = InterfaceButton("Billig", InterfaceButton::Permanent, texHandler->GetSprite("CheckBox")[0][0], this->Sprite->getPosition(), sf::Vector2f(x2, y2));
	temp.Text.setFont(*texHandler->GetFont());
	temp.Text.setString("Billig");
	temp.Text.setPosition(temp.Sprite->getPosition() + sf::Vector2f(15, -2));
	temp.Text.setCharacterSize(10);
	this->FilterButtons.push_back(temp);

	y2 += 30;

	temp = InterfaceButton("Normal", InterfaceButton::Permanent, texHandler->GetSprite("CheckBox")[0][0], this->Sprite->getPosition(), sf::Vector2f(x2, y2));
	temp.Text.setFont(*texHandler->GetFont());
	temp.Text.setString("Normal");
	temp.Text.setPosition(temp.Sprite->getPosition() + sf::Vector2f(15, -2));
	temp.Text.setCharacterSize(10);
	this->FilterButtons.push_back(temp);

	y2 += 30;

	temp = InterfaceButton("Premium", InterfaceButton::Permanent, texHandler->GetSprite("CheckBox")[0][0], this->Sprite->getPosition(), sf::Vector2f(x2, y2));
	temp.Text.setFont(*texHandler->GetFont());
	temp.Text.setString("Premium");
	temp.Text.setPosition(temp.Sprite->getPosition() + sf::Vector2f(15, -2));
	temp.Text.setCharacterSize(10);
	this->FilterButtons.push_back(temp);

	y2 += 30;*/

	this->Buttons.push_back(new InterfaceButton("Close", InterfaceButton::Type::Normal, texHandler->GetSprite("CloseButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(950, 30)));
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("CloseButton_Pressed")[0][0];
	this->Buttons.push_back(new InterfaceButton("Buy", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(915, 420)));
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Kaufen");
	this->Buttons.push_back(new InterfaceButton("Dispose", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(100, 420)));
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Abgelaufene Proudkte wegwerfen");
	while (this->Buttons.back()->Sprite->getGlobalBounds().width < this->Buttons.back()->Text.getGlobalBounds().width + 20)
	{
		this->Buttons.back()->Sprite->scale(1.1f, 1.0f);
	}

	std::vector<ProductListItem*>::iterator iter = productList->begin();
	std::vector<ProductListItem*>::iterator end = productList->end();

	int x = 1;
	int y = 1;

	std::list<InterfaceButton*> emptyList;

	//Create Button for each category
	while (iter != end)
	{
		std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
		std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

		bool existing = false;

		while (anotheriter != anotherend)
		{
			if ((*anotheriter)->Name == (*iter)->Category)
			{
				existing = true;
			}

			anotheriter++;
		}

		if (!existing)
		{
			this->Buttons.push_back(new TabButton((*iter)->Category, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f((float)x * 150.0f, (float)y * 50.0f), emptyList));
			this->Buttons.back()->Text.setFont(*texHandler->GetFont());
			this->Buttons.back()->Text.setString((*iter)->Category);
			this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
			while (this->Buttons.back()->Sprite->getGlobalBounds().width < this->Buttons.back()->Text.getGlobalBounds().width)
			{
				this->Buttons.back()->Sprite->scale(1.1f, 1.0f);
			}

			x++;
		}

		iter++;
	}


	x = 1;

	iter = productList->begin();
	end = productList->end();

	//Create Button for each Subcategory
	while (iter != end)
	{
		std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
		std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

		while (anotheriter != anotherend)
		{
			if ((*anotheriter)->Name == (*iter)->Category)
			{
				std::list<InterfaceButton*>::iterator yetanotheriter = dynamic_cast<TabButton*>(*anotheriter)->Buttons.begin();
				std::list<InterfaceButton*>::iterator yetanotherend = dynamic_cast<TabButton*>(*anotheriter)->Buttons.end();

				bool existing = false;

				while (yetanotheriter != yetanotherend)
				{
					if ((*yetanotheriter)->Name == (*iter)->Subcategory)
					{
						existing = true;
					}

					yetanotheriter++;
				}

				if (!existing)
				{
					x = dynamic_cast<TabButton*>(*anotheriter)->Buttons.size() + 1;

					TabButton* tmp = new TabButton((*iter)->Subcategory, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f((float)x * 130.0f, (float)y * 100.0f), emptyList);
					tmp->Text.setFont(*texHandler->GetFont());
					tmp->Text.setString((*iter)->Subcategory);
					tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(2, 3));
					while (tmp->Sprite->getGlobalBounds().width < tmp->Text.getGlobalBounds().width)
					{
						tmp->Sprite->scale(1.1f, 1.0f);
					}

					dynamic_cast<TabButton*>(*anotheriter)->Buttons.push_back(tmp);
				}
			}

			anotheriter++;
		}


		x++;

		iter++;
	}

	

	x = 1;

	iter = productList->begin();
	end = productList->end();

	//Create Button for each Productcategory
	while (iter != end)
	{
		std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
		std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

		while (anotheriter != anotherend)
		{
			if ((*anotheriter)->Name == (*iter)->Category)
			{
				std::list<InterfaceButton*>::iterator yetanotheriter = dynamic_cast<TabButton*>(*anotheriter)->Buttons.begin();
				std::list<InterfaceButton*>::iterator yetanotherend = dynamic_cast<TabButton*>(*anotheriter)->Buttons.end();


				while (yetanotheriter != yetanotherend)
				{
					if ((*yetanotheriter)->Name == (*iter)->Subcategory)
					{
						std::list<InterfaceButton*>::iterator stopwithallthoseiters = dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.begin();
						std::list<InterfaceButton*>::iterator stopwithallthoseends = dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.end();

						bool existing = false;

						while (stopwithallthoseiters != stopwithallthoseends)
						{
							if ((*stopwithallthoseiters)->Name == (*iter)->Product)
							{
								existing = true;
							}
							stopwithallthoseiters++;
						}
						
						if (!existing)
						{
							x = dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.size() + 1;

							TabButton* tmp = new TabButton((*iter)->Product, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f((float)x * 130.0f, (float)y * 150.0f), emptyList);
							tmp->Text.setFont(*texHandler->GetFont());
							tmp->Text.setString((*iter)->Product);
							tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(2, 3));
							while (tmp->Sprite->getGlobalBounds().width < tmp->Text.getGlobalBounds().width)
							{
								tmp->Sprite->scale(1.1f, 1.0f);
							}

							dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.push_back(tmp);
						}
					}

					yetanotheriter++;
				}
			}

			anotheriter++;
		}


		x++;

		iter++;
	}



	x = 1;

	iter = productList->begin();
	end = productList->end();

	//Create Button for each Product
	while (iter != end)
	{
		std::list<InterfaceButton*>::iterator anotheriter = this->Buttons.begin();
		std::list<InterfaceButton*>::iterator anotherend = this->Buttons.end();

		while (anotheriter != anotherend)
		{
			if ((*anotheriter)->Name == (*iter)->Category)
			{
				std::list<InterfaceButton*>::iterator yetanotheriter = dynamic_cast<TabButton*>(*anotheriter)->Buttons.begin();
				std::list<InterfaceButton*>::iterator yetanotherend = dynamic_cast<TabButton*>(*anotheriter)->Buttons.end();

				while (yetanotheriter != yetanotherend)
				{
					if ((*yetanotheriter)->Name == (*iter)->Subcategory)
					{
						std::list<InterfaceButton*>::iterator stopwithallthoseiters = dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.begin();
						std::list<InterfaceButton*>::iterator stopwithallthoseends = dynamic_cast<TabButton*>(*yetanotheriter)->Buttons.end();

						while (stopwithallthoseiters != stopwithallthoseends)
						{
							if ((*stopwithallthoseiters)->Name == (*iter)->Product)
							{
								x = dynamic_cast<TabButton*>(*stopwithallthoseiters)->Buttons.size() + 1;
								InterfaceButton* tmp = new InterfaceButton((*iter)->Name, InterfaceButton::Type::Normal, texHandler->GetSprite((*iter)->Name)[0][0], this->Sprite->getPosition(), sf::Vector2f((float)x * 100.0f + 35.0f, (float)y * 250.0f + 10.0f));
								tmp->Sprite->setScale(1, 1);
								while (tmp->Sprite->getGlobalBounds().height > 100)
								{
									tmp->Sprite->scale(0.9f, 0.9f);
								}
								tmp->Sprite->setOrigin(tmp->Sprite->getGlobalBounds().width / 2, tmp->Sprite->getGlobalBounds().height / 2);
								tmp->Text.setFont(*texHandler->GetFont());
								tmp->Text.setString((*iter)->Name);
								tmp->Text.setOrigin(round(tmp->Text.getGlobalBounds().width / 2.0f), round(tmp->Text.getGlobalBounds().height / 2.0f));
								tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(20, -50));
						
								dynamic_cast<TabButton*>(*stopwithallthoseiters)->Buttons.push_back(tmp);

								tmp = new Slider((*iter)->Name + "Slider", -0.1f, 0.1f, Slider::Type::Centered, texHandler->GetSprite("Slider")[0][0], texHandler->GetSprite("SliderSlider")[0][0], this->Sprite->getPosition(), sf::Vector2f((float)x * 100.0f, (float)y * 350.0f));
								tmp->Sprite->setScale(0.8f, 1.0f);
								tmp->Text.setFont(*texHandler->GetFont());
								tmp->Text.setString("0");
								tmp->Text.setPosition(tmp->Sprite->getPosition() + sf::Vector2f(110, 0));

								dynamic_cast<TabButton*>(*stopwithallthoseiters)->Buttons.push_back(tmp);
							}

							stopwithallthoseiters++;
						}
					}

					yetanotheriter++;
				}
			}

			anotheriter++;
		}


		x++;

		iter++;
	}
}


void ProductsWindow::filterButtons(std::string mode)
{
	std::vector<ProductListItem*>::iterator iter = this->ProductList->begin();
	std::vector<ProductListItem*>::iterator end = this->ProductList->end();

	std::list<InterfaceButton*>::iterator catiter;
	std::list<InterfaceButton*>::iterator catend;

	std::list<InterfaceButton*>::iterator subcatiter;
	std::list<InterfaceButton*>::iterator subcatend;

	std::list<InterfaceButton*>::iterator prodcatiter;
	std::list<InterfaceButton*>::iterator prodcatend;

	std::list<InterfaceButton*>::iterator buttiter;
	std::list<InterfaceButton*>::iterator buttend;

	if (mode == "Storage")
	{
		while (iter != end)
		{
			catiter = this->Buttons.begin();
			catend = this->Buttons.end();

			while (catiter != catend)
			{
				if ((*catiter)->Name == (*iter)->Category)
				{
					subcatiter = dynamic_cast<TabButton*>(*catiter)->Buttons.begin();
					subcatend = dynamic_cast<TabButton*>(*catiter)->Buttons.end();

					while (subcatiter != subcatend)
					{
						if ((*subcatiter)->Name == (*iter)->Subcategory)
						{
							prodcatiter = dynamic_cast<TabButton*>(*subcatiter)->Buttons.begin();
							prodcatend = dynamic_cast<TabButton*>(*subcatiter)->Buttons.end();

							while (prodcatiter != prodcatend)
							{
								if ((*prodcatiter)->Name == (*iter)->Product)
								{
									buttiter = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.begin();
									buttend = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.end();

									while (buttiter != buttend)
									{
										if ((*buttiter)->Name.find("Slider") != std::string::npos)
										{
											(*buttiter)->Visible = false;
										}
										else if ((*buttiter)->Name == (*iter)->Name)
										{
											(*buttiter)->HalfDisabled = false;
											(*buttiter)->Visible = this->ProductStorage->countProduct((*iter)->Name) > 0;
											(*buttiter)->Sprite->setColor(sf::Color::Black);
										}

										buttiter++;
									}

									break;
								}

								prodcatiter++;
							}

							break;
						}

						subcatiter++;
					}

					break;
				}
				if ((*catiter)->Name == "Buy")
				{
					(*catiter)->Visible = false;
				}
				if ((*catiter)->Name == "Close")
				{
					(*catiter)->Visible = true;
				}
				if ((*catiter)->Name == "Dispose")
				{
					(*catiter)->Visible = true;
				}

				catiter++;
			}

			iter++;
		}
	}
	else if (mode == "Shelf")
	{
		while (iter != end)
		{
			catiter = this->Buttons.begin();
			catend = this->Buttons.end();

			while (catiter != catend)
			{
				if ((*catiter)->Name == (*iter)->Category)
				{
					subcatiter = dynamic_cast<TabButton*>(*catiter)->Buttons.begin();
					subcatend = dynamic_cast<TabButton*>(*catiter)->Buttons.end();

					while (subcatiter != subcatend)
					{
						if ((*subcatiter)->Name == (*iter)->Subcategory)
						{
							prodcatiter = dynamic_cast<TabButton*>(*subcatiter)->Buttons.begin();
							prodcatend = dynamic_cast<TabButton*>(*subcatiter)->Buttons.end();

							while (prodcatiter != prodcatend)
							{
								if ((*prodcatiter)->Name == (*iter)->Product)
								{
									buttiter = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.begin();
									buttend = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.end();

									while (buttiter != buttend)
									{
										if ((*buttiter)->Name == (*iter)->Name)
										{
											(*buttiter)->HalfDisabled = (*iter)->Subcategory == "Obst" || (*iter)->Subcategory == "Gemüse" || (*iter)->Subcategory == "Wurst";
										}

										buttiter++;
									}

									break;
								}

								prodcatiter++;
							}

							break;
						}

						subcatiter++;
					}

					break;
				}
				if ((*catiter)->Name == "Buy")
				{
					(*catiter)->Visible = true;
				}
				if ((*catiter)->Name == "Close")
				{
					(*catiter)->Visible = true;
				}
				if ((*catiter)->Name == "Dispose")
				{
					(*catiter)->Visible = true;
				}

				catiter++;
			}

			iter++;
		}
	}
	else
	{
		ProductQuality quality;
		if (mode == "Level 1")
		{
			quality = ProductQuality::Cheap;
		}
		else if (mode == "Level 2")
		{
			quality = ProductQuality::Normal;
		}
		else if (mode == "Level 3")
		{
			quality = ProductQuality::Premium;
		}
		while (iter != end)
		{
			catiter = this->Buttons.begin();
			catend = this->Buttons.end();

			while (catiter != catend)
			{
				if ((*catiter)->Name == (*iter)->Category)
				{
					subcatiter = dynamic_cast<TabButton*>(*catiter)->Buttons.begin();
					subcatend = dynamic_cast<TabButton*>(*catiter)->Buttons.end();

					while (subcatiter != subcatend)
					{
						if ((*subcatiter)->Name == (*iter)->Subcategory)
						{
							prodcatiter = dynamic_cast<TabButton*>(*subcatiter)->Buttons.begin();
							prodcatend = dynamic_cast<TabButton*>(*subcatiter)->Buttons.end();

							while (prodcatiter != prodcatend)
							{
								if ((*prodcatiter)->Name == (*iter)->Product)
								{
									buttiter = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.begin();
									buttend = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.end();

									while (buttiter != buttend)
									{
										if ((*buttiter)->Name.find((*iter)->Name) != std::string::npos)
										{
											if ((*buttiter)->Name == (*iter)->Name)
											{
												(*buttiter)->Visible = true;
												(*buttiter)->Disabled = (*iter)->Quality > quality;
												(*buttiter)->HalfDisabled = false;
											}
											else
											{
												(*buttiter)->Visible = (*iter)->Quality <= quality;
											}
										}

										buttiter++;
									}

									break;
								}

								prodcatiter++;
							}

							break;
						}

						subcatiter++;
					}

					break;
				}
				if ((*catiter)->Name == "Buy")
				{
					(*catiter)->Visible = true;
				}
				if ((*catiter)->Name == "Close")
				{
					(*catiter)->Visible = true;
				}
				if ((*catiter)->Name == "Dispose")
				{
					(*catiter)->Visible = true;
				}

				catiter++;
			}

			iter++;
		}
	}
}


void ProductsWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	std::list<InterfaceButton*>::iterator anotheriter;
	std::list<InterfaceButton*>::iterator anotherend;

	std::string hover = "NOOOOOOO";

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			(*iter)->update(input);
		}

		if ((*iter)->Name == "Close")
		{
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Products";
				messages->push_back(InterfaceMessage(tmp));
				this->CurrentButton = 0;

				(*iter)->Pressed = false;
				(*iter)->JustPressed = false;
			}
		}
		else if ((*iter)->Name == "Buy")
		{
			if ((*iter)->JustPressed)
			{
				if (this->CurrentPrice <= gameData->CurrentMoney)
				{
					gameData->CurrentMoney -= this->CurrentPrice;
					this->ProductStorage->addProducts(&this->BuyList);
					this->BuyList.clear();
					this->CurrentPrice = 0.0;
					this->PriceText.setString(L"0.00€");
					this->resetSliders();
				}
			}
		}
		else if ((*iter)->Name == "Dispose")
		{
			if ((*iter)->JustPressed)
			{
				this->ProductStorage->dispose();
			}
		}
		else
		{
			if ((*iter)->Pressed && (*iter) != this->CurrentButton)
			{
				if (this->CurrentButton != 0)
				{
					this->CurrentButton->JustPressed = false;
					this->CurrentButton->Pressed = false;
				}
				this->CurrentButton = (*iter);
			}

			if (this->CurrentButton)
			{
				hover = this->CurrentButton->getHover();

				std::list<InterfaceButton*> buttons = dynamic_cast<TabButton*>(this->CurrentButton)->getButtons();
				if (!dynamic_cast<TabButton*>(buttons.front()))
				{
					anotheriter = buttons.begin();
					anotherend = buttons.end();

					while (anotheriter != anotherend)
					{
						if ((*anotheriter)->WasPressed && (*anotheriter)->Name.find("Slider") != std::string::npos)
						{
							std::string tmp = (*anotheriter)->Name;
							std::string::iterator first = tmp.end() - 6;
							tmp.erase(first, tmp.end());

							this->addToCart(tmp, (unsigned int)dynamic_cast<Slider*>(*anotheriter)->Value, gameData);
						}

						anotheriter++;
					}
				}
			}
		}



		iter++;
	}

	if (hover != "NOOOOOOO")
	{
		this->CurrentItem = this->getCurrentItem(hover);
		if (CurrentItem != 0)
		{
			std::wstringstream ss;

			ss << std::fixed << std::setprecision(2) << this->CurrentItem->Price;
			std::wstring tmp = L"Preis: " + ss.str() + L"€\n";

			ss.str(L"");

			ss << this->ProductStorage->countProduct(this->CurrentItem->Name);
			tmp += L"Im Lager: " + ss.str() + L"\n";

			this->HoverWindowText.setString(tmp + L"Beschreibung:\n" + this->CurrentItem->Description);
		}
	}
	else
	{
		this->CurrentItem = 0;
		this->HoverWindowText.setString("");
	}
	this->HoverWindowText.setPosition(round((float)input->GetMousePos().x + 20.0f), round((float)input->GetMousePos().y + 20.0f));
	this->HoverWindowSprite->setPosition((float)input->GetMousePos().x + 10.0f, (float)input->GetMousePos().y + 10.0f);



	/*std::list<InterfaceButton>::iterator anotheriter = this->FilterButtons.begin();
	std::list<InterfaceButton>::iterator anotherend = this->FilterButtons.end();

	while (anotheriter != anotherend)
	{
		anotheriter->update(input);
		if (anotheriter->JustPressed)
		{
			if (anotheriter->Name == "Close")
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Products";
				messages->push_back(InterfaceMessage(tmp));
				this->CurrentButton = 0;

				anotheriter->Pressed = false;
				anotheriter->JustPressed = false;
			}
			else if (anotheriter->Name == "Buy")
			{
				if (this->CurrentPrice <= gameData->CurrentMoney)
				{
					gameData->CurrentMoney -= this->CurrentPrice;
					this->ProductStorage->addProducts(&this->BuyList);
				}
			}
			else
			{
				this->filterButtons();
			}
		}
		anotheriter++;
	}*/
}


void ProductsWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);
		iter++;
	}

	if (this->HoverWindowText.getString() != "")
	{
		target.draw(*this->HoverWindowSprite);
	}
	target.draw(this->HoverWindowText);
	if (!this->StorageOnly)
	{
		target.draw(this->PriceText);
	}
}


ProductListItem* ProductsWindow::getCurrentItem(std::string name)
{
	if (this->CurrentButton != 0)
	{
		std::vector<ProductListItem*>::iterator iter = this->ProductList->begin();
		std::vector<ProductListItem*>::iterator end = this->ProductList->end();

		while (iter != end)
		{
			if ((*iter)->Name == name)
			{
				return *iter;
			}

			iter++;
		}
	}

	return 0;
}


void ProductsWindow::addToCart(std::string productName, unsigned int amount, GameData* gameData)
{
	std::vector<ProductListItem*>::iterator iter = this->ProductList->begin();
	std::vector<ProductListItem*>::iterator end = this->ProductList->end();

	ProductListItem* product;

	while (iter != end)
	{
		if ((*iter)->Name == productName)
		{
			product = *iter;
			break;
		}

		iter++;
	}


	std::list<Product>::iterator anotheriter = this->BuyList.begin();
	std::list<Product>::iterator anotherend = this->BuyList.end();

	while (anotheriter != anotherend)
	{
		if (anotheriter->Name == productName)
		{
			anotheriter = this->BuyList.erase(anotheriter);
		}
		else
		{
			anotheriter++;
		}
	}

	this->BuyList.push_back(Product(product, amount, gameData, this->TexHandler));

	this->calculatePrice();
}


void ProductsWindow::calculatePrice()
{
	this->CurrentPrice = 0;

	std::list<Product>::iterator iter = this->BuyList.begin();
	std::list<Product>::iterator end = this->BuyList.end();

	while (iter != end)
	{
		this->CurrentPrice += iter->Price * iter->Amount;

		iter++;
	}

	std::wstringstream ss;
	ss << std::fixed << std::setprecision(2) << this->CurrentPrice;
	this->PriceText.setString(ss.str() + L"€");
}


void ProductsWindow::resetSliders()
{
	std::list<InterfaceButton*>::iterator catiter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator catend = this->Buttons.end();

	while (catiter != catend)
	{
		if ((*catiter)->Name != "Buy" && (*catiter)->Name != "Close" && (*catiter)->Name != "Dispose")
		{
			std::list<InterfaceButton*>::iterator subcatiter = dynamic_cast<TabButton*>(*catiter)->Buttons.begin();
			std::list<InterfaceButton*>::iterator subcatend = dynamic_cast<TabButton*>(*catiter)->Buttons.end();

			while (subcatiter != subcatend)
			{
				std::list<InterfaceButton*>::iterator prodcatiter = dynamic_cast<TabButton*>(*subcatiter)->Buttons.begin();
				std::list<InterfaceButton*>::iterator prodcatend = dynamic_cast<TabButton*>(*subcatiter)->Buttons.end();

				while (prodcatiter != prodcatend)
				{
					std::list<InterfaceButton*>::iterator buttiter = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.begin();
					std::list<InterfaceButton*>::iterator buttend = dynamic_cast<TabButton*>(*prodcatiter)->Buttons.end();

					while (buttiter != buttend)
					{
						if ((*buttiter)->Name.find("Slider") != std::string::npos)
						{
							dynamic_cast<Slider*>(*buttiter)->Value = 0;
							(*buttiter)->Text.setString("0");
						}

						buttiter++;
					}

					prodcatiter++;
				}

				subcatiter++;
			}
		}

		catiter++;
	}
}