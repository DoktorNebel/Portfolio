#pragma once
#include <SFML/Graphics.hpp>
#include <list>
#include <array>
#include "GameData.h"
#include "Product.h"

class Entity :
	public sf::Drawable
{
public:

	float AnimTimer;
	unsigned int AnimPhase;
	unsigned int AnimState;
	std::string SpriteName;
	sf::Vector2f GridPosition;
	sf::Vector2f LowestGridPosition;
	sf::Vector2f LowestScreenPosition;
	int Rotation;
	unsigned int ID;
	std::vector<std::vector<sf::Sprite*>> Sprite;
	std::vector<std::vector<sf::Sprite*>> BackSprite;
	std::array<sf::Vector2f, 4> Origins;
	std::vector<std::list<Product>> Products;
	bool Moved;
	std::vector<std::vector<std::list<Entity*>>>* Grid;
	sf::Vector2i PreviousGridPosition;
	bool DeleteMe;
	float IdleWait;
	

protected:

	void UpdateSprite(sf::Time elapsedTime, unsigned short gameSpeed);

public:

	Entity(void);
	~Entity(void);
	
	virtual void update(sf::Time elapsedTime, unsigned short gameSpeed, GameData* gameData);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void setColor(sf::Color color);
	void scale(sf::Vector2f factor);
	void setOrigin(sf::Vector2f origin);
	void deleteSprites();
};

