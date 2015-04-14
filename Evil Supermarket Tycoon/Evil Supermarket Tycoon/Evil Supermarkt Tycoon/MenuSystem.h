#pragma once

#include "SubMenu.h"
#include "dirent.h"
#include "MenuTextButton.h"
#include "MenuDropDownButton.h"
#include "MenuSlider.h"
#include "MenuPermanentButton.h"
#include <sstream>

class MenuSystem : public sf::Drawable
{
public:

	std::list<SubMenu> Menus;
	SubMenu* ActiveMenu;
	std::list<MenuMessage> Messages;

public:

	MenuSystem();
	MenuSystem(sf::Vector2f screenSize, TextureHandler* texHandler);
	~MenuSystem(void);

	void update(Input* input);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void SetActiveMenu(std::string name);
	void Resize(sf::Vector2f screenSize);
};

