#pragma once
#include <SFML/Graphics.hpp>
#include <cstdlib>
#include "Store.h"
#include "Product.h"
#include "TextureHandler.h"
#include "Builder.h"
#include "Input.h"
#include "GameData.h"
#include "ProductsWindow.h"
#include "ProductListItem.h"
#include "Pathfinder.h"
#include "FeedMessage.h"
#include "IllegalActivity.h"
#include "Connection.h"
#include "MenuSystem.h"
#include "stxutif.h"
#include "SoundManager.h"

class Game : public sf::Drawable
{
public:

	SoundManager SoundHandler;

	TextureHandler TexHandler;

	GameData Data;

	std::vector<ProductListItem*> ProductList;

	Store* CurrentStore;

	Pathfinder ThePathfinder;

	enum State 
	{ 
		Intro,
		MainMenu,
		BeforeOpening,
		AfterClosing,
		MainGame,
		Pause,
		Quit
	} GameState;

	Game::State PreviousState;

	unsigned short Speed;

	std::list<FeedMessage> Feed;

	std::vector<IllegalActivity> IllegalStuff;

	std::vector<int> PenaltyLevel;

	MenuSystem GameMenu;

	bool Loaded;

	sf::Sprite Intro1;
	sf::Sprite Intro2;

	float IntroFade;

	int IntroPhase;

private:
	
	std::list<Store> Stores;

	Builder Bob;


public:

	Game(sf::Vector2f screenSize);
	~Game(void);

	void update(sf::Time elapsedTime, Input* input, Interface* UI);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void loadProducts();
	void Save(std::wstring path);
	void Load(std::wstring path);
};

