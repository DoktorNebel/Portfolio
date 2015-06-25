#include "TextureHandler.h"


TextureHandler::TextureHandler(void)
{
}


TextureHandler::~TextureHandler(void)
{

}

void TextureHandler::LoadTextures()
{
	this->Font.loadFromFile("Content/Fonts/arial.ttf");

	std::vector<std::string> textureNames;
	textureNames.push_back("StoreObjects");
	textureNames.push_back("Products");
	textureNames.push_back("Customers_Male_1");
	textureNames.push_back("Customers_Male_2");
	textureNames.push_back("Customers_Male_3");
	textureNames.push_back("Customers_Female_1");
	textureNames.push_back("Customers_Female_2");
	textureNames.push_back("Workers_Male_1");
	textureNames.push_back("Workers_Male_2");
	textureNames.push_back("Workers_Male_3");
	textureNames.push_back("Workers_Male_4");
	textureNames.push_back("Workers_Female");
	textureNames.push_back("Animals");
	textureNames.push_back("Interface");

	this->Textures.resize(textureNames.size());

	std::string line;

	for (unsigned int i = 0; i < this->Textures.size(); i++)
	{
		this->Textures[i].loadFromFile("Content/Sprites/" + textureNames[i] + ".png");
		//this->Textures[i].setSmooth(true);

		std::ifstream file("Content/Sprites/" + textureNames[i] + ".txt");
		while(std::getline(file, line))
		{
			std::string spriteName = "";
			std::string rectX = "";
			std::string rectY = "";
			std::string rectWidth = "";
			std::string rectHeight = "";

			int status = 0;
			std::string::iterator iter = line.begin();
			while (iter != line.end())
			{
				if (*iter == ' ' && status != 0)
				{
					status++;
				}
				else if (*iter == '=')
				{
					status = 1;
				}
				else
				{
					switch (status)
					{
					case 0:
						spriteName += *iter;
						break;

					case 2:
						rectX += *iter;
						break;

					case 3:
						rectY += *iter;
						break;

					case 4:
						rectWidth += *iter;
						break;

					case 5:
						rectHeight += *iter;
						break;
					}
				}

				iter++;
			}

			spriteName.erase(spriteName.size() - 1);
			TextureHandler::SpriteObject tmp;
			tmp.Name = spriteName;
			tmp.Sprite = sf::Sprite(this->Textures[i], sf::IntRect(atoi(rectX.c_str()), atoi(rectY.c_str()), atoi(rectWidth.c_str()), atoi(rectHeight.c_str())));
			tmp.Sprite.setScale(0.1f, 0.1f);
			tmp.Origins[0] = sf::Vector2f(0, 0);
			tmp.Origins[1] = sf::Vector2f(0, 0);
			tmp.Origins[2] = sf::Vector2f(0, 0);
			tmp.Origins[3] = sf::Vector2f(0, 0);

			this->Sprites.push_back(tmp);
		}
	}

	


	
	std::ifstream file2("Content/Sprites/Origins.txt");
	while(std::getline(file2, line))
	{
		std::string spriteName = "";
		std::string orig1X = "";
		std::string orig1Y = "";
		std::string orig2X = "";
		std::string orig2Y = "";
		std::string orig3X = "";
		std::string orig3Y = "";
		std::string orig4X = "";
		std::string orig4Y = "";
		std::string scaleX = "";
		std::string scaleY = "";

		int status = 0;
		std::string::iterator iter = line.begin();
		while (iter != line.end())
		{
			if (*iter == ' ' && status != 0)
			{
				status++;
			}
			else if (*iter == '=')
			{
				status = 1;
			}
			else
			{
				switch (status)
				{
				case 0:
					spriteName += *iter;
					break;

				case 2:
					orig1X += *iter;
					break;

				case 3:
					orig1Y += *iter;
					break;

				case 4:
					orig2X += *iter;
					break;

				case 5:
					orig2Y += *iter;
					break;

				case 6:
					orig3X += *iter;
					break;

				case 7:
					orig3Y += *iter;
					break;

				case 8:
					orig4X += *iter;
					break;

				case 9:
					orig4Y += *iter;
					break;

				case 10:
					scaleX += *iter;
					break;

				case 11:
					scaleY += *iter;
					break;
				}
			}

			iter++;
		}

		spriteName.erase(spriteName.size() - 1);

		std::vector<TextureHandler::SpriteObject>::iterator anotheriter = this->Sprites.begin();
		while (anotheriter != this->Sprites.end())
		{
			if (anotheriter->Name.find(spriteName) != std::string::npos)
			{
				anotheriter->Sprite.setScale((float)atof(scaleX.c_str()), (float)atof(scaleY.c_str()));
				anotheriter->Origins[0] = sf::Vector2f((float)atoi(orig1X.c_str()), (float)atoi(orig1Y.c_str()));
				anotheriter->Origins[1] = sf::Vector2f((float)atoi(orig2X.c_str()), (float)atoi(orig2Y.c_str()));
				anotheriter->Origins[2] = sf::Vector2f((float)atoi(orig3X.c_str()), (float)atoi(orig3Y.c_str()));
				anotheriter->Origins[3] = sf::Vector2f((float)atoi(orig4X.c_str()), (float)atoi(orig4Y.c_str()));
			}
			anotheriter++;
		}
	}
}


std::vector<std::vector<sf::Sprite*>> TextureHandler::GetSprite(std::string spriteName)
{
	std::vector<std::vector<sf::Sprite*>> result;
	result.resize(1);
	result[0].resize(1);


	std::vector<TextureHandler::SpriteObject>::const_iterator iter;
	iter = this->Sprites.begin();
	while (iter != this->Sprites.end())
	{
		if (iter->Name.find(spriteName) != std::string::npos)
		{
			if (iter->Name.find(spriteName + "_") != std::string::npos)
			{
				if (spriteName == "Person")
				{
					result.resize(1);
				}
				std::string posStr = iter->Name.substr(spriteName.size() + 1, 2);
				if (posStr.size() > 1 && posStr[1] == '_')
				{
					posStr.erase(1);
				}

				int pos = atoi(posStr.c_str());

				if ((int)result.size() < pos)
				{
					result.resize(pos);
					result[pos - 1].resize(1);
				}

				if (iter->Name.size() > spriteName.size() + 3)
				{
					std::string posStr2 = iter->Name.substr(iter->Name.size() - 2, 2);
					if (posStr2[0] == '_')
					{
						posStr2.erase(0, 1);
					}

					int pos2 = atoi(posStr2.c_str());

					if ((int)result[pos - 1].size() < pos2)
					{
						result[pos - 1].resize(pos2);
					}

					result[pos - 1][pos2 - 1] = new sf::Sprite(iter->Sprite);
				}
				else
				{
					result[pos - 1][0] = new sf::Sprite(iter->Sprite);
				}
			}
			else
			{
				result[0][0] = new sf::Sprite(iter->Sprite);
				break;
			}
		}

		iter++;
	}

	return result;
}


std::array<sf::Vector2f, 4> TextureHandler::GetOrigins(std::string spriteName)
{
	std::vector<TextureHandler::SpriteObject>::const_iterator iter;
	iter = this->Sprites.begin();
	while (iter != this->Sprites.end() && iter->Name.find(spriteName) == std::string::npos)
	{
		iter++;
	}

	return iter->Origins;
}


sf::Font* TextureHandler::GetFont()
{
	return &this->Font;
}


bool TextureHandler::Exists(std::string spriteName)
{
	std::vector<TextureHandler::SpriteObject>::const_iterator iter;
	iter = this->Sprites.begin();
	while (iter != this->Sprites.end())
	{
		if (iter->Name == spriteName)
		{
			return true;
		}
		iter++;
	}

	return false;
}