#include "MenuSystem.h"


MenuSystem::MenuSystem()
{
	this->ActiveMenu = 0;
}


MenuSystem::MenuSystem(sf::Vector2f screenSize, TextureHandler* texHandler)
{
	DIR *dir;
	struct dirent *ent;
	
	dir = opendir("Saves");
	std::vector<std::string> saves;
	while ((ent = readdir (dir)) != NULL)
	{
		saves.push_back(ent->d_name);
	}
	closedir(dir);

	this->ActiveMenu = 0;

	MenuMessage tmp;

	//-----------------Main Menu-----------------
	this->Menus.push_back(SubMenu("Main", texHandler->GetSprite("MenuBackground")[0][0], screenSize));

	//Start Button
	this->Menus.back().Buttons.push_back(new MenuButton("Start", "", texHandler->GetSprite("NewGameButton")[0][0], sf::Vector2f(0.15f, 0.1f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::StartGame;
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	this->Menus.back().Buttons.back()->PressedSprite = texHandler->GetSprite("NewGameButton_Pressed")[0][0];
	this->Menus.back().Buttons.back()->PressedSprite->setOrigin(this->Menus.back().Buttons.back()->Sprite->getOrigin());
	
	//Load Button
	this->Menus.back().Buttons.push_back(new MenuButton("Load", "", texHandler->GetSprite("LoadGameButton")[0][0], sf::Vector2f(0.15f, 0.3f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Load";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	this->Menus.back().Buttons.back()->PressedSprite = texHandler->GetSprite("LoadGameButton_Pressed")[0][0];
	this->Menus.back().Buttons.back()->PressedSprite->setOrigin(this->Menus.back().Buttons.back()->Sprite->getOrigin());

	//Options Button
	this->Menus.back().Buttons.push_back(new MenuButton("Options", "", texHandler->GetSprite("OptionsButton")[0][0], sf::Vector2f(0.15f, 0.5f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Options";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	this->Menus.back().Buttons.back()->PressedSprite = texHandler->GetSprite("OptionsButton_Pressed")[0][0];
	this->Menus.back().Buttons.back()->PressedSprite->setOrigin(this->Menus.back().Buttons.back()->Sprite->getOrigin());

	//Quit Button
	this->Menus.back().Buttons.push_back(new MenuButton("Quit", "", texHandler->GetSprite("ExitButton")[0][0], sf::Vector2f(0.15f, 0.7f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::Quit;
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	this->Menus.back().Buttons.back()->PressedSprite = texHandler->GetSprite("ExitButton_Pressed")[0][0];
	this->Menus.back().Buttons.back()->PressedSprite->setOrigin(this->Menus.back().Buttons.back()->Sprite->getOrigin());
	

	//-----------------Load Menu-----------------
	this->Menus.push_back(SubMenu("Load", texHandler->GetSprite("MenuBackground")[0][0], screenSize));

	//Save Buttons
	for (int i = 2; i < saves.size(); i++)
	{
		this->Menus.back().Buttons.push_back(new MenuButton("Save", saves[i], texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.1f, 0.1f + (i - 2) * 0.05f), texHandler, screenSize));
		tmp.MessageType = MenuMessage::Type::LoadGame;
		tmp.Text = saves[i];
		this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	}

	//Back Button
	this->Menus.back().Buttons.push_back(new MenuButton("Back", "Zurück", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.1f, 0.8f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Main";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);


	//-----------------Options Menu-----------------
	this->Menus.push_back(SubMenu("Options", texHandler->GetSprite("MenuBackground")[0][0], screenSize));
	
	//Volume Button
	this->Menus.back().Buttons.push_back(new MenuSlider("Volume", "Lautstärke", 0, 100, texHandler->GetSprite("Slider")[0][0], texHandler->GetSprite("SliderSlider")[0][0], sf::Vector2f(0.2f, 0.3f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeVolume;
	tmp.Text = "";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);

	//Resolution Button
	std::vector<std::string> elements;
	std::vector<sf::VideoMode> modes = sf::VideoMode::getFullscreenModes();
	std::stringstream sstream;

	for (int i = 0; i < modes.size(); i++)
	{
		sstream << modes[i].width << "x" << modes[i].height;
		elements.push_back(sstream.str());
		sstream.str("");
	}

	this->Menus.back().Buttons.push_back(new MenuDropDownButton("Resolution", "Auflösung:", texHandler->GetSprite("DropDown")[0][0], texHandler->GetSprite("DropDownList")[0][0], sf::Vector2f(0.2f, 0.1f), elements, texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeResolution;
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	sstream << screenSize.x << "x" << screenSize.y;
	dynamic_cast<MenuDropDownButton*>(this->Menus.back().Buttons.back())->Selected = sstream.str();

	//Fullscreen Button
	this->Menus.back().Buttons.push_back(new MenuPermanentButton("Fullscreen", "Vollbild:", texHandler->GetSprite("CheckBox")[0][0], sf::Vector2f(0.2f, 0.2f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeFullscreen;
	tmp.Text = "";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	this->Menus.back().Buttons.back()->Sprite->setScale(0.5f, 0.5f);
	this->Menus.back().Buttons.back()->PressedSprite = texHandler->GetSprite("CheckBox_Pressed")[0][0];
	this->Menus.back().Buttons.back()->PressedSprite->setPosition(this->Menus.back().Buttons.back()->Sprite->getPosition());
	this->Menus.back().Buttons.back()->PressedSprite->setOrigin(this->Menus.back().Buttons.back()->Sprite->getOrigin());
	this->Menus.back().Buttons.back()->PressedSprite->setScale(0.5f, 0.5f);
	
	//Back Button
	this->Menus.back().Buttons.push_back(new MenuButton("Back", "Zurück", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.1f, 0.8f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Main";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);

	
	//-----------------Pause Menu-----------------
	this->Menus.push_back(SubMenu("Pause", texHandler->GetSprite("PauseBackground")[0][0], screenSize));
	
	//Resume Button
	this->Menus.back().Buttons.push_back(new MenuButton("Resume", "Fortsetzen", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.45f, 0.4f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ResumeGame;
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	
	//Save Button
	this->Menus.back().Buttons.push_back(new MenuButton("Save", "Speichern", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.45f, 0.5f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Save";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	
	//Back Button
	this->Menus.back().Buttons.push_back(new MenuButton("Back", "Hauptmenü", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.45f, 0.6f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Main";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);

	
	//-----------------Save Menu-----------------
	this->Menus.push_back(SubMenu("Save", texHandler->GetSprite("PauseBackground")[0][0], screenSize));
	
	//Text Button
	this->Menus.back().Buttons.push_back(new MenuTextButton("Text", "Name:", texHandler->GetSprite("TextButton")[0][0], sf::Vector2f(0.45f, 0.6f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::SaveGame;
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);
	
	//Save Button
	this->Menus.back().Buttons.push_back(new MenuButton("Save", "Speichern", texHandler->GetSprite("SimpleButton")[0][0], sf::Vector2f(0.45f, 0.7f), texHandler, screenSize));
	tmp.MessageType = MenuMessage::Type::ChangeMenu;
	tmp.Text = "Pause";
	this->Menus.back().Buttons.back()->WasPressedMessage = new MenuMessage(tmp);


	std::list<SubMenu>::iterator iter = this->Menus.begin();
	std::list<SubMenu>::iterator end = this->Menus.end();

	while (iter != end)
	{
		iter->Resize(screenSize);

		iter++;
	}
}


MenuSystem::~MenuSystem(void)
{
}


void MenuSystem::update(Input* input)
{
	if (this->ActiveMenu)
	{
		this->ActiveMenu->update(input, &this->Messages);
	}

	if (!this->Messages.empty())
	{
		switch (this->Messages.front().MessageType)
		{
		case MenuMessage::Type::ChangeMenu:
			this->SetActiveMenu(this->Messages.front().Text);
			this->Messages.pop_front();
			break;
		}
	}
}


void MenuSystem::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->ActiveMenu)
	{
		target.draw(*this->ActiveMenu);
	}
}


void MenuSystem::SetActiveMenu(std::string name)
{
	this->ActiveMenu = 0;

	std::list<SubMenu>::iterator iter = this->Menus.begin();
	std::list<SubMenu>::iterator end = this->Menus.end();

	while (iter != end)
	{
		if (iter->Name == name)
		{
			this->ActiveMenu = &(*iter);
			break;
		}

		iter++;
	}
}


void MenuSystem::Resize(sf::Vector2f screenSize)
{
	std::list<SubMenu>::iterator iter = this->Menus.begin();
	std::list<SubMenu>::iterator end = this->Menus.end();

	while (iter != end)
	{
		iter->Resize(screenSize);

		iter++;
	}
}