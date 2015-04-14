#pragma once

#include "interfacewindow.h"
#include "FeedMessage.h"

class FeedWindow :
	public InterfaceWindow
{
private:

	std::list<FeedMessage>* Feed;
	std::list<sf::Text*> Texts;
	int PrevSize;
	TextureHandler* TexHandler;
	InterfaceButton* PullButton;
	float MessageCount;

public:

	FeedWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::list<FeedMessage>* feed);
	~FeedWindow(void);
	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
};

