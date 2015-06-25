#include "DataWindow.h"


DataWindow::DataWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler)
{
	this->Name = name;

	this->MoneyText.setFont(*texHandler->GetFont());
	this->MoneyText.setPosition(position + sf::Vector2f(25, 10));
	this->MoneyText.setColor(sf::Color(128, 242, 152, 255));
	this->MoneyText.setCharacterSize(14);
	this->MoneyText.setString("lool");
	this->CustomersText.setFont(*texHandler->GetFont());
	this->CustomersText.setPosition(position + sf::Vector2f(120, 10));
	this->CustomersText.setColor(sf::Color(128, 242, 152, 255));
	this->CustomersText.setCharacterSize(14);
	this->PrestigeText.setFont(*texHandler->GetFont());
	this->PrestigeText.setPosition(position + sf::Vector2f(215, 10));
	this->PrestigeText.setColor(sf::Color(128, 242, 152, 255));
	this->PrestigeText.setCharacterSize(14);
	this->TimeAndDateText.setFont(*texHandler->GetFont());
	this->TimeAndDateText.setPosition(position + sf::Vector2f(850, 10));
	this->TimeAndDateText.setColor(sf::Color(128, 242, 152, 255));
	this->TimeAndDateText.setCharacterSize(14);

	this->Sprite = sprite;
	this->Sprite->scale(1.0f, 0.15f);
	this->Sprite->setPosition(position);

	this->Buttons.push_back(new InterfaceButton("Building", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(350, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Bauen");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.push_back(new InterfaceButton("Products", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(420, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Produkte");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.push_back(new InterfaceButton("Storage", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(420, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Lager");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Visible = false;
	this->Buttons.push_back(new InterfaceButton("Workers", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(490, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Arbeiter");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.push_back(new InterfaceButton("Illegal", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(560, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Illegales");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.push_back(new InterfaceButton("Open", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(700, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString(L"Laden öffnen");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Sprite->scale(1.3f, 1.0f);
	this->Buttons.back()->Visible = true;
	this->Buttons.push_back(new InterfaceButton("End", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(700, 5)));
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(2, 3));
	this->Buttons.back()->Text.setString("Tag beenden");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Sprite->scale(1.3f, 1.0f);
	this->Buttons.back()->Visible = false;
	this->Buttons.push_back(new InterfaceButton("Pause", InterfaceButton::Type::Normal, texHandler->GetSprite("PauseButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(650, 3)));
	this->Buttons.back()->Visible = false;
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("PauseButton_Pressed")[0][0];
	this->Buttons.push_back(new InterfaceButton("Normal", InterfaceButton::Type::Normal, texHandler->GetSprite("NormalSpeedButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(700, 3)));
	this->Buttons.back()->Visible = false;
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("NormalSpeedButton_Pressed")[0][0];
	this->Buttons.push_back(new InterfaceButton("Double", InterfaceButton::Type::Normal, texHandler->GetSprite("DoubleSpeedButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(750, 3)));
	this->Buttons.back()->Visible = false;
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("DoubleSpeedButton_Pressed")[0][0];
	this->Buttons.push_back(new InterfaceButton("Quad", InterfaceButton::Type::Normal, texHandler->GetSprite("QuadSpeedButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(800, 3)));
	this->Buttons.back()->Visible = false;
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("QuadSpeedButton_Pressed")[0][0];

	this->IsOpen = true;

	this->State = 2;
}


DataWindow::~DataWindow(void)
{
}


void DataWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::wstringstream string;
	string << std::fixed << std::setprecision(2) << gameData->CurrentMoney << L"€";
	this->MoneyText.setString(string.str());

	string.str(L"");

	string << "Kunden: " << gameData->CurrentCustomers;
	this->CustomersText.setString(string.str());

	string.str(L"");

	string << "Prestige: " << gameData->Prestige;
	this->PrestigeText.setString(string.str());


	string.str(L"");

	std::wstring dayAddOn = L"";
	if (gameData->Date.Day < 10)
	{
		dayAddOn = L"0";
	}

	std::wstring monthAddOn = L"";
	if (gameData->Date.Month < 10)
	{
		monthAddOn = L"0";
	}

	std::wstring hourAddOn = L"";
	if (gameData->Time.Hour < 10)
	{
		hourAddOn = L"0";
	}

	std::wstring minuteAddOn = L"";
	if (gameData->Time.Minute < 10)
	{
		minuteAddOn = L"0";
	}

	string << dayAddOn << gameData->Date.Day << "." << monthAddOn << gameData->Date.Month << "." << gameData->Date.Year << "     ";
	string << hourAddOn << gameData->Time.Hour << ":" << minuteAddOn << static_cast<int>(gameData->Time.Minute);
	this->TimeAndDateText.setString(string.str());


	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			(*iter)->update(input);
		}

		if ((*iter)->Name == "Building" && (*iter)->JustPressed)
		{
			InterfaceMessage tmp;
			tmp.MessageType = InterfaceMessage::Type::OpenWindow;
			tmp.Text = "Building";
			messages->push_back(InterfaceMessage(tmp));

			tmp.MessageType = InterfaceMessage::Type::CloseWindow;
			tmp.Text = "Entities";
			messages->push_back(InterfaceMessage(tmp));
		}
		if ((*iter)->Name == "Products")
		{
			if (this->State == 2)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::OpenWindow;
				tmp.Text = "Products";
				messages->push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Workers";
				messages->push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Illegal";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Storage")
		{
			if (this->State == 2)
			{
				(*iter)->Visible = false;
			}
			else
			{
				(*iter)->Visible = true;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::OpenWindow;
				tmp.Text = "Storage";
				messages->push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Workers";
				messages->push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Illegal";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Illegal" && (*iter)->JustPressed)
		{
			InterfaceMessage tmp;
			tmp.MessageType = InterfaceMessage::Type::OpenWindow;
			tmp.Text = "Illegal";
			messages->push_back(InterfaceMessage(tmp));

			tmp.MessageType = InterfaceMessage::Type::CloseWindow;
			tmp.Text = "Workers";
			messages->push_back(InterfaceMessage(tmp));

			tmp.MessageType = InterfaceMessage::Type::CloseWindow;
			tmp.Text = "Products";
			messages->push_back(InterfaceMessage(tmp));
		}
		if ((*iter)->Name == "Open")
		{
			if (this->State == 0)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameState;
				tmp.Text = "MainGame";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "End")
		{
			if (this->State == 2)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameState;
				tmp.Text = "BeforeOpening";
				messages->push_back(InterfaceMessage(tmp));

				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Products";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Pause")
		{
			if (this->State == 1)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameSpeed;
				tmp.Text = "0";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Normal")
		{
			if (this->State == 1)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameSpeed;
				tmp.Text = "1";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Double")
		{
			if (this->State == 1)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameSpeed;
				tmp.Text = "2";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Quad")
		{
			if (this->State == 1)
			{
				(*iter)->Visible = true;
			}
			else
			{
				(*iter)->Visible = false;
			}
			if ((*iter)->JustPressed)
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::ChangeGameSpeed;
				tmp.Text = "4";
				messages->push_back(InterfaceMessage(tmp));
			}
		}
		if ((*iter)->Name == "Workers" && (*iter)->JustPressed)
		{
			InterfaceMessage tmp;
			tmp.MessageType = InterfaceMessage::Type::OpenWindow;
			tmp.Text = "Workers";
			messages->push_back(InterfaceMessage(tmp));

			tmp.MessageType = InterfaceMessage::Type::CloseWindow;
			tmp.Text = "Products";
			messages->push_back(InterfaceMessage(tmp));
		}
		iter++;
	}
}


void DataWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	target.draw(this->MoneyText);
	target.draw(this->CustomersText);
	target.draw(this->PrestigeText);
	target.draw(this->TimeAndDateText);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);
		iter++;
	}
}


std::string DataWindow::getSelected()
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Pressed)
		{
			return (*iter)->Name;	
		}
		iter++;
	}

	return "NOOOOOO";
}