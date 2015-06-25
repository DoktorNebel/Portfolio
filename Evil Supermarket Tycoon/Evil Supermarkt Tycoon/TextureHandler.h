#pragma once

#include <fstream>
#include <string>
#include <array>
#include <SFML/Graphics.hpp>

class TextureHandler
{
private:

	struct SpriteObject
	{
		std::string Name;
		sf::Sprite Sprite;
		std::array<sf::Vector2f, 4> Origins;
	};

	std::vector<sf::Texture> Textures;
	std::vector<SpriteObject> Sprites;
	sf::Font Font;

public:

	TextureHandler(void);

	~TextureHandler(void);

	void LoadTextures();

	std::vector<std::vector<sf::Sprite*>> GetSprite(std::string spriteName);
	sf::Font* GetFont();
	std::array<sf::Vector2f, 4> GetOrigins(std::string spriteName);
	bool Exists(std::string spriteName);
};

