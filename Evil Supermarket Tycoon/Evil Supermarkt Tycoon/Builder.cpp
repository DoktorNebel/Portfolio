#include "Builder.h"


Builder::Builder()
{

}

Builder::Builder(TextureHandler* texHandler)
{
	this->Rotation = 0;
	this->BuildMode = Builder::Mode::None;
	

	this->WhiteTileSprite = texHandler->GetSprite("Tile")[0][0];
	this->WhiteTileSprite->setColor(sf::Color(0, 255, 0, 128));

	this->PriceText.setFont(*texHandler->GetFont());
	this->PriceText.setCharacterSize(14);
	this->PriceText.setColor(sf::Color::Black);

	this->SelectedObject = 0;
	this->ExtraObject = 0;

	this->StartPos = sf::Vector2i(-1, -1);

	this->CurrentPrice = 0;

	std::string line;
	std::ifstream file("Content/Data/ObjectPrices.txt");
	while(std::getline(file, line))
	{
		std::string type = "";
		std::string quality = "";
		std::string price = "";

		int status = 0;
		std::string::iterator iter = line.begin();
		while (iter != line.end())
		{
			if (*iter == ' ')
			{
				status++;
			}
			else
			{
				switch (status)
				{
				case 0:
					type += *iter;
					break;

				case 1:
					quality += *iter;
					break;

				case 2:
					price += *iter;
					break;
				}
			}

			iter++;
		}

		Builder::ObjectPrice tmp;
		if (type == "Floor")
		{
			tmp.Type = StoreObject::Type::Floor;
		}
		if (type == "Wall")
		{
			tmp.Type = StoreObject::Type::Wall;
		}
		if (type == "Shelf")
		{
			tmp.Type = StoreObject::Type::Shelf;
		}
		if (type == "Palette")
		{
			tmp.Type = StoreObject::Type::Palette;
		}
		if (type == "Freezer")
		{
			tmp.Type = StoreObject::Type::Freezer;
		}
		if (type == "Fridge")
		{
			tmp.Type = StoreObject::Type::Fridge;
		}
		if (type == "Checkout")
		{
			tmp.Type = StoreObject::Type::Checkout;
		}
		if (type == "Door")
		{
			tmp.Type = StoreObject::Type::Door;
		}
		if (type == "Fruitshelf")
		{
			tmp.Type = StoreObject::Type::Fruitshelf;
		}

		
		if (quality == "Illegal")
		{
			tmp.Quality = StoreObject::Quality::Illegal;
		}
		if (quality == "Cheap")
		{
			tmp.Quality = StoreObject::Quality::Cheap;
		}
		if (quality == "Normal")
		{
			tmp.Quality = StoreObject::Quality::Normal;
		}
		if (quality == "Premium")
		{
			tmp.Quality = StoreObject::Quality::Premium;
		}

		tmp.Price = atof(price.c_str());

		this->Prices.push_back(tmp);
	}

	file.close();
}


Builder::~Builder(void)
{
}


void Builder::switchBuildMode()
{
	this->BuildMode = Builder::Mode::None;
}


bool Builder::isActivated()
{
	return !this->BuildMode == Builder::Mode::None;
}


void Builder::update(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, Input* input, unsigned int* highestID, std::deque<unsigned int>* freeIDs, TextureHandler* texHandler, Interface* UI, GameData* gameData)
{
	this->CurrentPos = input->GetGridPos();
	if (this->BuildMode != Builder::Mode::None && this->BuildMode != Builder::Mode::Remove)
	{
		this->ValidPos = false;
		this->SelectedObject->setColor(sf::Color(255, 0, 0, 128));
		if (this->ExtraObject)
		{
			this->ExtraObject->setColor(sf::Color(255, 0, 0, 128));
		}

	


		//check if grid position is valid
		if (CurrentPos.x + this->SelectedObject->Width * (this->SelectedObject->Width < 0) >= 0 &&
			CurrentPos.x + this->SelectedObject->Width * (this->SelectedObject->Width > 0) <= (int)grid->size() - 1 &&
			CurrentPos.y + this->SelectedObject->Height * (this->SelectedObject->Height < 0) >= 0 &&
			CurrentPos.y + this->SelectedObject->Height * (this->SelectedObject->Height > 0) <= (int)(*grid)[0].size() - 1)
		{
			this->ValidPos = true;
		}


		for (int i = 0; i <= abs(this->SelectedObject->Width); i++)
		{
			for (int j = 0; j <= abs(this->SelectedObject->Height); j++)
			{
				int checkPosX = CurrentPos.x + i * (1 + (this->SelectedObject->Width < 0) * -2);
				int checkPosY = CurrentPos.y + j * (1 + (this->SelectedObject->Height < 0) * -2);
				if (this->ValidPos && (*grid)[checkPosX][checkPosY].size() > 0)
				{
					std::list<Entity*>::iterator iter = (*grid)[checkPosX][checkPosY].begin();
					std::list<Entity*>::iterator end = (*grid)[checkPosX][checkPosY].end();

					while (iter != end)
					{
						if (this->BuildMode == Builder::Mode::SingleObject && dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType != StoreObject::Floor)
						{
							this->ValidPos = false;
						}

						iter++;
					}
				}
			}
		}

		/*std::list<sf::Vector2i>::const_iterator iter;
		iter = SelectedObject->AccessPoints.begin();
		while (iter != SelectedObject->AccessPoints.end())
		{
			int checkPosX = gridPosX + iter->x;
			int checkPosY = gridPosY + iter->y;
			if (this->ValidPos && (*grid)[checkPosX][checkPosY].size() > 0)
			{
				this->ValidPos = false;
			}
			iter++;
		}*/


		if (this->ValidPos)
		{
			this->SelectedObject->setColor(sf::Color(255, 255, 255, 128));
			if (this->ExtraObject)
			{
				this->ExtraObject->setColor(sf::Color(255, 255, 255, 128));
			}
		}


		double wholePrice = this->CurrentPrice;

		if (this->BuildMode == Builder::Mode::Floor && this->StartPos != sf::Vector2i(-1, -1))
		{
			wholePrice *= abs(StartPos.x - CurrentPos.x) + 1;
			wholePrice *= abs(StartPos.y - CurrentPos.y) + 1;
		}

		if (this->BuildMode == Builder::Mode::Wall && this->StartPos != sf::Vector2i(-1, -1))
		{
			int difX = abs(this->StartPos.x - this->CurrentPos.x) + 1;
			int difY = abs(this->StartPos.y - this->CurrentPos.y) + 1;

			if (difX > 1 || difY > 1)
			{
				wholePrice *= difX * (difX > difY) + difY * (difY > difX);
			}
		}

		std::stringstream priceStream;
		priceStream << wholePrice;
		this->PriceText.setString(priceStream.str());
		this->PriceText.setPosition(input->GetMouseCoords().x + 5, input->GetMouseCoords().y + 25);
	}


	//check if mouse is inside UI
	bool active = !UI->containsMouse(input);

	if (active && this->BuildMode != Builder::Mode::None)
	{
		//rotate object
		if (input->JustPressed(Input::RotateLeft) && this->BuildMode == Builder::Mode::SingleObject)
		{
			this->Rotation--;
			if (this->Rotation < 0)
			{
				this->Rotation = 3;
			}

			this->SelectedObject->Rotation = this->Rotation;
			if (this->ExtraObject)
			{
				this->ExtraObject->Rotation = this->Rotation;
			}

			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->deleteSprites();
				if (this->SelectedObject->Rotation < 2)
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				}
				else
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Back");
				}
				if (this->SelectedObject->Rotation % 2)
				{
					this->SelectedObject->scale(sf::Vector2f(-1, 1));
				}

				if (this->ExtraObject)
				{
					this->ExtraObject->deleteSprites();
					if (this->ExtraObject->Rotation < 2)
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Front");
					}
					else
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Back");
					}
					if (this->ExtraObject->Rotation % 2)
					{
						this->ExtraObject->scale(sf::Vector2f(-1, 1));
					}
				}
			}
			else
			{
				this->SelectedObject->scale(sf::Vector2f(-1, 1));
				if (this->ExtraObject)
				{
					this->ExtraObject->scale(sf::Vector2f(-1, 1));
				}
			}

			this->SelectedObject->setOrigin(this->SelectedObject->Origins[this->Rotation]);
			short tmp = this->SelectedObject->Width;
			this->SelectedObject->Width = this->SelectedObject->Height;
			this->SelectedObject->Height = -tmp;

			if (this->ExtraObject)
			{
				this->ExtraObject->setOrigin(this->ExtraObject->Origins[this->Rotation]);
				short tmp = this->ExtraObject->Width;
				this->ExtraObject->Width = this->ExtraObject->Height;
				this->ExtraObject->Height = -tmp;
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 2 || this->SelectedObject->Rotation == 0))
				{
					this->ExtraObject->Height *= -1;
				}
			}


			//adjust access- and workpoints
			std::vector<sf::Vector2i>::iterator iter;
			iter = this->SelectedObject->AccessPoints.begin();
			while (iter != this->SelectedObject->AccessPoints.end())
			{
				*iter = sf::Vector2i(iter->y, -iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 2 || this->SelectedObject->Rotation == 0))
				{
					iter->y *= -1;
				}
				iter++;
			}

			iter = this->SelectedObject->WorkPoints.begin();
			while (iter != this->SelectedObject->WorkPoints.end())
			{
				*iter = sf::Vector2i(iter->y, -iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 2 || this->SelectedObject->Rotation == 0))
				{
					iter->y *= -1;
				}
				iter++;
			}

		}


		if (input->JustPressed(Input::RotateRight) && this->BuildMode == Builder::Mode::SingleObject)
		{
			this->Rotation++;
			if (this->Rotation > 3)
			{
				this->Rotation = 0;
			}

			this->SelectedObject->Rotation = this->Rotation;
			if (this->ExtraObject)
			{
				this->ExtraObject->Rotation = this->Rotation;
			}

			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->deleteSprites();
				if (this->SelectedObject->Rotation < 2)
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				}
				else
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Back");
				}
				if (this->SelectedObject->Rotation % 2)
				{
					this->SelectedObject->scale(sf::Vector2f(-1, 1));
				}

				if (this->ExtraObject)
				{
					this->ExtraObject->deleteSprites();
					if (this->ExtraObject->Rotation < 2)
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Front");
					}
					else
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Back");
					}
					if (this->ExtraObject->Rotation % 2)
					{
						this->ExtraObject->scale(sf::Vector2f(-1, 1));
					}
				}
			}
			else
			{
				this->SelectedObject->scale(sf::Vector2f(-1, 1));
				if (this->ExtraObject)
				{
					this->ExtraObject->scale(sf::Vector2f(-1, 1));
				}
			}

			this->SelectedObject->setOrigin(this->SelectedObject->Origins[this->Rotation]);
			short tmp = this->SelectedObject->Width;
			this->SelectedObject->Width = -this->SelectedObject->Height;
			this->SelectedObject->Height = tmp;
			
			if (this->ExtraObject)
			{
				this->ExtraObject->setOrigin(this->ExtraObject->Origins[this->Rotation]);
				short tmp = this->ExtraObject->Width;
				this->ExtraObject->Width = -this->ExtraObject->Height;
				this->ExtraObject->Height = tmp;
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					this->ExtraObject->Width *= -1;
				}
			}


			//adjust access- and workpoints
			std::vector<sf::Vector2i>::iterator iter;
			iter = this->SelectedObject->AccessPoints.begin();
			while (iter != this->SelectedObject->AccessPoints.end())
			{
				*iter = sf::Vector2i(-iter->y, iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					iter->x *= -1;
				}
				iter++;
			}

			iter = this->SelectedObject->WorkPoints.begin();
			while (iter != this->SelectedObject->WorkPoints.end())
			{
				*iter = sf::Vector2i(-iter->y, iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					iter->x *= -1;
				}
				iter++;
			}
		}




		//place object
		if (input->JustClicked(sf::Mouse::Left))
		{
			if (this->BuildMode == Builder::Mode::SingleObject && this->ValidPos && this->CurrentPrice <= gameData->CurrentMoney)
			{
				this->placeCurrentObject(grid, costGrid, highestID, freeIDs, texHandler, gameData);
			}
			else
			{
				this->StartPos = input->GetGridPos();
			}
		}

		if (input->WasClicked(sf::Mouse::Left) && this->BuildMode != Builder::Mode::SingleObject && this->StartPos != sf::Vector2i(-1, -1))
		{
			bool enoughMoney = true;
			double price = CurrentPrice;

			if (this->BuildMode == Builder::Mode::Floor)
			{
				price *= abs(StartPos.x - CurrentPos.x) + 1;
				price *= abs(StartPos.y - CurrentPos.y) + 1;

				if (price > gameData->CurrentMoney)
				{
					enoughMoney = false;
				}
			}

			if (this->BuildMode == Builder::Mode::Wall)
			{
				int difX = abs(this->StartPos.x - this->CurrentPos.x) + 1;
				int difY = abs(this->StartPos.y - this->CurrentPos.y) + 1;

				if (difX > 1 || difY > 1)
				{
					price *= difX * (difX > difY) + difY * (difY > difX);
				}

				if (price > gameData->CurrentMoney)
				{
					enoughMoney = false;
				}
			}

			if (enoughMoney)
			{
				this->placeCurrentObject(grid, costGrid, highestID, freeIDs, texHandler, gameData);
			}

			this->StartPos = sf::Vector2i(-1, -1);
		}
	}
	else
	{
		if (input->JustClicked(sf::Mouse::Left))
		{
			this->Rotation = 0;

			std::string object = UI->getSelectedStoreObject();

			if (object != "NOOOOOOO")
			{
				if (object == "Remove")
				{
					this->BuildMode = Builder::Mode::Remove;
					if (this->SelectedObject != 0)
					{
						delete this->SelectedObject;
						this->SelectedObject = 0;
					}
				}
				else
				{
					this->changeCurrentObject(object, texHandler, true);
				}
			}
		}
	}


	if (input->JustClicked(sf::Mouse::Right))
	{
		this->BuildMode = Builder::Mode::None;
	}


	if (this->BuildMode != Builder::Mode::None && this->BuildMode != Builder::Mode::Remove)
	{
		this->SelectedObject->GridPosition = sf::Vector2f((float)CurrentPos.x, (float)CurrentPos.y);
		this->SelectedObject->update(sf::Time::Zero, 1, 0);

		if (this->ExtraObject)
		{
			this->ExtraObject->GridPosition = sf::Vector2f((float)CurrentPos.x, (float)CurrentPos.y);
			this->ExtraObject->update(sf::Time::Zero, 1, 0);
		}
	}
}


void Builder::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	if (this->BuildMode != Builder::Mode::None)
	{
		if (this->BuildMode == Builder::Mode::Remove)
		{
			if (this->StartPos != sf::Vector2i(-1, -1))
			{
				this->WhiteTileSprite->setColor(sf::Color(255, 255, 255, 128));

				int difX = abs(this->StartPos.x - this->CurrentPos.x);
				int difY = abs(this->StartPos.y - this->CurrentPos.y);

				for (int i = 0; i <= difX; i++)
				{
					for (int j = 0; j <= difY; j++)
					{
						int posX = this->StartPos.x + i * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
						int posY = this->StartPos.y + j * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

						float screenPosX = (((float)posX - 0.5f) - ((float)posY + 0.5f)) * (float)Constants::GridSize;
						float screenPosY = (((float)posX - 0.5f) + ((float)posY + 0.5f)) * (float)Constants::GridSize / 2.0f;

						this->WhiteTileSprite->setPosition(screenPosX, screenPosY);

						target.draw(*this->WhiteTileSprite);
					}
				}

				this->WhiteTileSprite->setColor(sf::Color(0, 255, 0, 128));
			}
		}
		else if (this->SelectedObject != 0)
		{
			std::vector<sf::Vector2i>::const_iterator iter = this->SelectedObject->AccessPoints.begin();

			while (iter != this->SelectedObject->AccessPoints.end())
			{
				int gridPosX = (int)this->SelectedObject->GridPosition.x + iter->x;
				int gridPosY = (int)this->SelectedObject->GridPosition.y + iter->y;
				float screenPosX = (((float)gridPosX - 0.5f) - ((float)gridPosY + 0.5f)) * (float)Constants::GridSize;
				float screenPosY = (((float)gridPosX - 0.5f) + ((float)gridPosY + 0.5f)) * (float)Constants::GridSize / 2.0f;
				this->WhiteTileSprite->setPosition(screenPosX, screenPosY);
				target.draw(*this->WhiteTileSprite);
				iter++;
			}
			
			iter = this->SelectedObject->WorkPoints.begin();
			this->WhiteTileSprite->setColor(sf::Color(0, 0, 255, 128));

			while (iter != this->SelectedObject->WorkPoints.end())
			{
				int gridPosX = (int)this->SelectedObject->GridPosition.x + iter->x;
				int gridPosY = (int)this->SelectedObject->GridPosition.y + iter->y;
				float screenPosX = (((float)gridPosX - 0.5f) - ((float)gridPosY + 0.5f)) * (float)Constants::GridSize;
				float screenPosY = (((float)gridPosX - 0.5f) + ((float)gridPosY + 0.5f)) * (float)Constants::GridSize / 2.0f;
				this->WhiteTileSprite->setPosition(screenPosX, screenPosY);
				target.draw(*this->WhiteTileSprite);
				iter++;
			}

			this->WhiteTileSprite->setColor(sf::Color(0, 255, 0, 128));

			if (this->BuildMode == Builder::Mode::Floor && this->StartPos != sf::Vector2i(-1, -1))
			{
				int difX = abs(this->StartPos.x - this->CurrentPos.x);
				int difY = abs(this->StartPos.y - this->CurrentPos.y);
				for (int i = 0; i <= difX; i++)
				{
					for (int j = 0; j <= difY; j++)
					{
						int posX = this->StartPos.x + i * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
						int posY = this->StartPos.y + j * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

						this->SelectedObject->GridPosition = sf::Vector2f((float)posX, (float)posY);
						this->SelectedObject->update(sf::Time::Zero, 1, 0);
						target.draw(*this->SelectedObject);
					}
				}
			}
			else if (this->BuildMode == Builder::Mode::Wall && this->StartPos != sf::Vector2i(-1, -1))
			{
				int difX = abs(this->StartPos.x - this->CurrentPos.x);
				int difY = abs(this->StartPos.y - this->CurrentPos.y);
				int dif = difX * (difX > difY) + difY * (difY > difX);
				for (int i = 0; i <= dif; i++)
				{
					int posX = this->StartPos.x + i * (difX > difY) * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
					int posY = this->StartPos.y + i * (difY > difX) * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

					this->SelectedObject->GridPosition = sf::Vector2f((float)posX, (float)posY);
					this->SelectedObject->update(sf::Time::Zero, 1, 0);
					target.draw(*this->SelectedObject);
				}
			}
			else
			{
				if (this->ExtraObject)
				{
					target.draw(*this->ExtraObject);
				}
				target.draw(*this->SelectedObject);
			}

			target.draw(this->PriceText);
		}
	}
}




void Builder::changeCurrentPrice(StoreObject::Type type, StoreObject::Quality quality)
{
	std::vector<Builder::ObjectPrice>::iterator iter = this->Prices.begin();
	std::vector<Builder::ObjectPrice>::iterator enditer = this->Prices.end();

	while (iter != enditer)
	{
		if (iter->Type == type)
		{
			if (iter->Quality == quality)
			{
				this->CurrentPrice = iter->Price;
				break;
			}
		}
		iter++;
	}

	if (quality == StoreObject::Quality::Nothing)
	{
		this->CurrentPrice = 0;
	}
}



void Builder::changeCurrentObject(std::string spriteName, TextureHandler* texHandler, bool deleteOld)
{
	if (deleteOld && this->BuildMode != Builder::Mode::None)
	{
		delete this->SelectedObject;
		if (this->ExtraObject)
		{
			delete this->ExtraObject;
		}
	}
	if (this->ExtraObject)
	{
		this->ExtraObject = 0;
	}

	StoreObject::Type type;
	StoreObject::Quality quality;

	if (spriteName.find("Floor") != std::string::npos)
	{
		type = StoreObject::Type::Floor;
	}
	if (spriteName.find("Wall") != std::string::npos)
	{
		type = StoreObject::Type::Wall;
	}
	if (spriteName.find("Door") != std::string::npos)
	{
		type = StoreObject::Type::Door;
	}
	if (spriteName.find("Shelf") != std::string::npos)
	{
		type = StoreObject::Type::Shelf;
	}
	if (spriteName.find("Fridge") != std::string::npos)
	{
		type = StoreObject::Type::Fridge;
	}
	if (spriteName.find("Freezer") != std::string::npos)
	{
		type = StoreObject::Type::Freezer;
	}
	if (spriteName.find("Palette") != std::string::npos)
	{
		type = StoreObject::Type::Palette;
	}
	if (spriteName.find("Checkout") != std::string::npos)
	{
		type = StoreObject::Type::Checkout;
	}
	if (spriteName.find("Fruitshelf") != std::string::npos)
	{
		type = StoreObject::Type::Fruitshelf;
	}
	
	if (spriteName.find("Illegal") != std::string::npos)
	{
		quality = StoreObject::Quality::Illegal;
	}
	if (spriteName.find("Cheap") != std::string::npos)
	{
		quality = StoreObject::Quality::Cheap;
	}
	if (spriteName.find("Normal") != std::string::npos)
	{
		quality = StoreObject::Quality::Normal;
	}
	if (spriteName.find("Premium") != std::string::npos)
	{
		quality = StoreObject::Quality::Premium;
	}


	this->changeCurrentPrice(type, quality);

	
	std::vector<sf::Vector2i> apList;
	std::vector<sf::Vector2i> wpList;
	std::vector<std::vector<std::vector<sf::Vector2i>>> ppList;
	ppList.resize(4);
	int prodsPerLayer = 0;
	int prodsPerSlot = 0;

	switch (type)
	{
	case StoreObject::Type::Floor:
		this->BuildMode = Builder::Mode::Floor;
		switch (quality)
		{
		case StoreObject::Quality::Cheap:
			this->SelectedObject = new StoreObject(type, quality, 0, 0, apList, wpList, 0, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;


		case StoreObject::Quality::Normal:
			this->SelectedObject = new StoreObject(type, quality, 0, 0, apList, wpList, 0, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;


		case StoreObject::Quality::Premium:
			this->SelectedObject = new StoreObject(type, quality, 0, 0, apList, wpList, 0, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;


	case StoreObject::Type::Wall:
		this->BuildMode = Builder::Mode::Wall;
		switch (quality)
		{
		case StoreObject::Quality::Cheap:
			this->SelectedObject = new StoreObject(type, quality, 0, 0, apList, wpList, 0, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;


	case StoreObject::Type::Door:
		this->BuildMode = Builder::Mode::SingleObject;
		switch (quality)
		{
		case StoreObject::Quality::Cheap:
			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 0, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;


	case StoreObject::Type::Shelf:
		this->BuildMode = Builder::Mode::SingleObject;

		ppList[0].resize(6);
		ppList[1].resize(6);
		ppList[2].resize(6);
		ppList[3].resize(6);
		ppList[0][0].resize(8);

		for (unsigned int i = 1; i < ppList[0].size(); i++)
		{
			ppList[0][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[1][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[2][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[3][i].resize(ppList[0][0].size());
		}

		ppList[0][0][0] = sf::Vector2i(60, -140);
		ppList[0][0][1] = sf::Vector2i(85, -155);
		ppList[0][0][2] = sf::Vector2i(60, -95);
		ppList[0][0][3] = sf::Vector2i(85, -110);
		ppList[0][0][4] = sf::Vector2i(60, -50);
		ppList[0][0][5] = sf::Vector2i(85, -65);
		ppList[0][0][6] = sf::Vector2i(60, 0);
		ppList[0][0][7] = sf::Vector2i(85, -15);
		ppList[0][1][0] = sf::Vector2i(110, -170);
		ppList[0][1][1] = sf::Vector2i(135, -185);
		ppList[0][1][2] = sf::Vector2i(110, -125);
		ppList[0][1][3] = sf::Vector2i(135, -140);
		ppList[0][1][4] = sf::Vector2i(110, -78);
		ppList[0][1][5] = sf::Vector2i(135, -93);
		ppList[0][1][6] = sf::Vector2i(110, -27);
		ppList[0][1][7] = sf::Vector2i(135, -42);
		ppList[0][2][0] = sf::Vector2i(160, -200);
		ppList[0][2][1] = sf::Vector2i(185, -215);
		ppList[0][2][2] = sf::Vector2i(160, -152);
		ppList[0][2][3] = sf::Vector2i(185, -167);
		ppList[0][2][4] = sf::Vector2i(160, -105);
		ppList[0][2][5] = sf::Vector2i(185, -120);
		ppList[0][2][6] = sf::Vector2i(160, -57);
		ppList[0][2][7] = sf::Vector2i(185, -72);

		ppList[1][0][0] = sf::Vector2i(65, -155);
		ppList[1][0][1] = sf::Vector2i(90, -140);
		ppList[1][0][2] = sf::Vector2i(65, -110);
		ppList[1][0][3] = sf::Vector2i(90, -95);
		ppList[1][0][4] = sf::Vector2i(65, -65);
		ppList[1][0][5] = sf::Vector2i(90, -50);
		ppList[1][0][6] = sf::Vector2i(65, -15);
		ppList[1][0][7] = sf::Vector2i(90, 0);
		ppList[1][1][0] = sf::Vector2i(115, -125);
		ppList[1][1][1] = sf::Vector2i(140, -110);
		ppList[1][1][2] = sf::Vector2i(115, -80);
		ppList[1][1][3] = sf::Vector2i(140, -65);
		ppList[1][1][4] = sf::Vector2i(115, -35);
		ppList[1][1][5] = sf::Vector2i(140, -20);
		ppList[1][1][6] = sf::Vector2i(115, 15);
		ppList[1][1][7] = sf::Vector2i(140, 30);
		ppList[1][2][0] = sf::Vector2i(165, -95);
		ppList[1][2][1] = sf::Vector2i(190, -80);
		ppList[1][2][2] = sf::Vector2i(165, -50);
		ppList[1][2][3] = sf::Vector2i(190, -35);
		ppList[1][2][4] = sf::Vector2i(165, -5);
		ppList[1][2][5] = sf::Vector2i(190, 10);
		ppList[1][2][6] = sf::Vector2i(165, 45);
		ppList[1][2][7] = sf::Vector2i(190, 60);

		ppList[2][5][0] = sf::Vector2i(-65, -80);
		ppList[2][5][1] = sf::Vector2i(-40, -95);
		ppList[2][5][2] = sf::Vector2i(-65, -35);
		ppList[2][5][3] = sf::Vector2i(-40, -50);
		ppList[2][5][4] = sf::Vector2i(-65, 10);
		ppList[2][5][5] = sf::Vector2i(-40, -5);
		ppList[2][5][6] = sf::Vector2i(-65, 60);
		ppList[2][5][7] = sf::Vector2i(-40, 45);
		ppList[2][4][0] = sf::Vector2i(-15, -110);
		ppList[2][4][1] = sf::Vector2i(10, -125);
		ppList[2][4][2] = sf::Vector2i(-15, -60);
		ppList[2][4][3] = sf::Vector2i(10, -75);
		ppList[2][4][4] = sf::Vector2i(-15, -15);
		ppList[2][4][5] = sf::Vector2i(10, -30);
		ppList[2][4][6] = sf::Vector2i(-15, 30);
		ppList[2][4][7] = sf::Vector2i(10, 15);
		ppList[2][3][0] = sf::Vector2i(35, -140);
		ppList[2][3][1] = sf::Vector2i(60, -155);
		ppList[2][3][2] = sf::Vector2i(35, -90);
		ppList[2][3][3] = sf::Vector2i(60, -105);
		ppList[2][3][4] = sf::Vector2i(35, -45);
		ppList[2][3][5] = sf::Vector2i(60, -60);
		ppList[2][3][6] = sf::Vector2i(35, 0);
		ppList[2][3][7] = sf::Vector2i(60, -15);

		ppList[3][5][0] = sf::Vector2i(-60, -215);
		ppList[3][5][1] = sf::Vector2i(-35, -200);
		ppList[3][5][2] = sf::Vector2i(-60, -165);
		ppList[3][5][3] = sf::Vector2i(-35, -150);
		ppList[3][5][4] = sf::Vector2i(-60, -120);
		ppList[3][5][5] = sf::Vector2i(-35, -105);
		ppList[3][5][6] = sf::Vector2i(-60, -70);
		ppList[3][5][7] = sf::Vector2i(-35, -55);
		ppList[3][4][0] = sf::Vector2i(-10, -185);
		ppList[3][4][1] = sf::Vector2i(15, -170);
		ppList[3][4][2] = sf::Vector2i(-10, -140);
		ppList[3][4][3] = sf::Vector2i(15, -125);
		ppList[3][4][4] = sf::Vector2i(-10, -95);
		ppList[3][4][5] = sf::Vector2i(15, -80);
		ppList[3][4][6] = sf::Vector2i(-10, -45);
		ppList[3][4][7] = sf::Vector2i(15, -30);
		ppList[3][3][0] = sf::Vector2i(40, -155);
		ppList[3][3][1] = sf::Vector2i(65, -140);
		ppList[3][3][2] = sf::Vector2i(40, -110);
		ppList[3][3][3] = sf::Vector2i(65, -95);
		ppList[3][3][4] = sf::Vector2i(40, -65);
		ppList[3][3][5] = sf::Vector2i(65, -50);
		ppList[3][3][6] = sf::Vector2i(40, -15);
		ppList[3][3][7] = sf::Vector2i(65, 0);

		prodsPerLayer = 2;
		prodsPerSlot = 8;

		switch (quality)
		{
		case StoreObject::Quality::Illegal:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(1, -2));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));


			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 6, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}
			break;

		case StoreObject::Quality::Cheap:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(1, -2));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));


			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 6, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}
			break;

		case StoreObject::Quality::Normal:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(1, -2));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));

			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 6, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}
			break;

		case StoreObject::Quality::Premium:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(1, -2));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));

			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 6, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}
			break;
		}
		break;


	case StoreObject::Type::Fridge:
		this->BuildMode = Builder::Mode::SingleObject;

		ppList[0].resize(6);
		ppList[1].resize(6);
		ppList[2].resize(6);
		ppList[3].resize(6);
		ppList[0][0].resize(12);

		for (unsigned int i = 1; i < ppList[0].size(); i++)
		{
			ppList[0][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[1][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[2][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[3][i].resize(ppList[0][0].size());
		}

		ppList[0][0][0] = sf::Vector2i(50, -96);
		ppList[0][0][1] = sf::Vector2i(78, -112);
		ppList[0][0][2] = sf::Vector2i(50, -75);
		ppList[0][0][3] = sf::Vector2i(78, -88);
		ppList[0][0][4] = sf::Vector2i(50, -53);
		ppList[0][0][5] = sf::Vector2i(78, -68);
		ppList[0][0][6] = sf::Vector2i(50, -30);
		ppList[0][0][7] = sf::Vector2i(78, -45);
		ppList[0][0][8] = sf::Vector2i(50, -12);
		ppList[0][0][9] = sf::Vector2i(78, -25);
		ppList[0][0][10] = sf::Vector2i(50, 10);
		ppList[0][0][11] = sf::Vector2i(78, -3);
		ppList[0][1][0] = sf::Vector2i(110, -130);
		ppList[0][1][1] = sf::Vector2i(140, -145);
		ppList[0][1][2] = sf::Vector2i(110, -108);
		ppList[0][1][3] = sf::Vector2i(140, -123);
		ppList[0][1][4] = sf::Vector2i(110, -87);
		ppList[0][1][5] = sf::Vector2i(140, -101);
		ppList[0][1][6] = sf::Vector2i(110, -64);
		ppList[0][1][7] = sf::Vector2i(140, -77);
		ppList[0][1][8] = sf::Vector2i(110, -43);
		ppList[0][1][9] = sf::Vector2i(140, -57);
		ppList[0][1][10] = sf::Vector2i(110, -20);
		ppList[0][1][11] = sf::Vector2i(140, -35);
		ppList[0][2][0] = sf::Vector2i(176, -161);
		ppList[0][2][1] = sf::Vector2i(200, -173);
		ppList[0][2][2] = sf::Vector2i(176, -139);
		ppList[0][2][3] = sf::Vector2i(200, -152);
		ppList[0][2][4] = sf::Vector2i(176, -116);
		ppList[0][2][5] = sf::Vector2i(200, -129);
		ppList[0][2][6] = sf::Vector2i(176, -95);
		ppList[0][2][7] = sf::Vector2i(200, -107);
		ppList[0][2][8] = sf::Vector2i(176, -74);
		ppList[0][2][9] = sf::Vector2i(200, -86);
		ppList[0][2][10] = sf::Vector2i(176, -52);
		ppList[0][2][11] = sf::Vector2i(200, -65);


		ppList[1][0][0] = sf::Vector2i(53, -112);
		ppList[1][0][1] = sf::Vector2i(79, -100);
		ppList[1][0][2] = sf::Vector2i(53, -89);
		ppList[1][0][3] = sf::Vector2i(79, -77);
		ppList[1][0][4] = sf::Vector2i(53, -66);
		ppList[1][0][5] = sf::Vector2i(79, -54);
		ppList[1][0][6] = sf::Vector2i(53, -44);
		ppList[1][0][7] = sf::Vector2i(79, -33);
		ppList[1][0][8] = sf::Vector2i(53, -22);
		ppList[1][0][9] = sf::Vector2i(79, -11);
		ppList[1][0][10] = sf::Vector2i(53, -1);
		ppList[1][0][11] = sf::Vector2i(79, 12);
		ppList[1][1][0] = sf::Vector2i(114, -82);
		ppList[1][1][1] = sf::Vector2i(140, -70);
		ppList[1][1][2] = sf::Vector2i(114, -58);
		ppList[1][1][3] = sf::Vector2i(140, -46);
		ppList[1][1][4] = sf::Vector2i(114, -37);
		ppList[1][1][5] = sf::Vector2i(140, -24);
		ppList[1][1][6] = sf::Vector2i(114, -15);
		ppList[1][1][7] = sf::Vector2i(140, -3);
		ppList[1][1][8] = sf::Vector2i(114, 7);
		ppList[1][1][9] = sf::Vector2i(140, 19);
		ppList[1][1][10] = sf::Vector2i(114, 30);
		ppList[1][1][11] = sf::Vector2i(140, 41);
		ppList[1][2][0] = sf::Vector2i(176, -51);
		ppList[1][2][1] = sf::Vector2i(200, -39);
		ppList[1][2][2] = sf::Vector2i(176, -29);
		ppList[1][2][3] = sf::Vector2i(200, -17);
		ppList[1][2][4] = sf::Vector2i(176, -7);
		ppList[1][2][5] = sf::Vector2i(200, 5);
		ppList[1][2][6] = sf::Vector2i(176, 14);
		ppList[1][2][7] = sf::Vector2i(200, 27);
		ppList[1][2][8] = sf::Vector2i(176, 38);
		ppList[1][2][9] = sf::Vector2i(200, 48);
		ppList[1][2][10] = sf::Vector2i(176, 60);
		ppList[1][2][11] = sf::Vector2i(200, 70);


		ppList[2][5][0] = sf::Vector2i(-71, -38);
		ppList[2][5][1] = sf::Vector2i(-46, -50);
		ppList[2][5][2] = sf::Vector2i(-71, -16);
		ppList[2][5][3] = sf::Vector2i(-46, -27);
		ppList[2][5][4] = sf::Vector2i(-71, 6);
		ppList[2][5][5] = sf::Vector2i(-46, -5);
		ppList[2][5][6] = sf::Vector2i(-71, 28);
		ppList[2][5][7] = sf::Vector2i(-46, 17);
		ppList[2][5][8] = sf::Vector2i(-71, 50);
		ppList[2][5][9] = sf::Vector2i(-46, 38);
		ppList[2][5][10] = sf::Vector2i(-71, 73);
		ppList[2][5][11] = sf::Vector2i(-46, 61);
		ppList[2][4][0] = sf::Vector2i(-10, -68);
		ppList[2][4][1] = sf::Vector2i(15, -81);
		ppList[2][4][2] = sf::Vector2i(-10, -45);
		ppList[2][4][3] = sf::Vector2i(15, -57);
		ppList[2][4][4] = sf::Vector2i(-10, -24);
		ppList[2][4][5] = sf::Vector2i(15, -36);
		ppList[2][4][6] = sf::Vector2i(-10, -1);
		ppList[2][4][7] = sf::Vector2i(15, -13);
		ppList[2][4][8] = sf::Vector2i(-10, 20);
		ppList[2][4][9] = sf::Vector2i(15, 9);
		ppList[2][4][10] = sf::Vector2i(-10, 42);
		ppList[2][4][11] = sf::Vector2i(15, 31);
		ppList[2][3][0] = sf::Vector2i(50, -98);
		ppList[2][3][1] = sf::Vector2i(75, -111);
		ppList[2][3][2] = sf::Vector2i(50, -75);
		ppList[2][3][3] = sf::Vector2i(75, -88);
		ppList[2][3][4] = sf::Vector2i(50, -53);
		ppList[2][3][5] = sf::Vector2i(75, -66);
		ppList[2][3][6] = sf::Vector2i(50, -31);
		ppList[2][3][7] = sf::Vector2i(75, -43);
		ppList[2][3][8] = sf::Vector2i(50, -9);
		ppList[2][3][9] = sf::Vector2i(75, -21);
		ppList[2][3][10] = sf::Vector2i(50, 12);
		ppList[2][3][11] = sf::Vector2i(75, 0);


		ppList[3][5][0] = sf::Vector2i(-73, -174);
		ppList[3][5][1] = sf::Vector2i(-48, -163);
		ppList[3][5][2] = sf::Vector2i(-73, -153);
		ppList[3][5][3] = sf::Vector2i(-48, -140);
		ppList[3][5][4] = sf::Vector2i(-73, -130);
		ppList[3][5][5] = sf::Vector2i(-48, -118);
		ppList[3][5][6] = sf::Vector2i(-73, -109);
		ppList[3][5][7] = sf::Vector2i(-48, -97);
		ppList[3][5][8] = sf::Vector2i(-73, -86);
		ppList[3][5][9] = sf::Vector2i(-48, -74);
		ppList[3][5][10] = sf::Vector2i(-73, -64);
		ppList[3][5][11] = sf::Vector2i(-48, -52);
		ppList[3][4][0] = sf::Vector2i(-13, -145);
		ppList[3][4][1] = sf::Vector2i(12, -133);
		ppList[3][4][2] = sf::Vector2i(-13, -123);
		ppList[3][4][3] = sf::Vector2i(12, -111);
		ppList[3][4][4] = sf::Vector2i(-13, -101);
		ppList[3][4][5] = sf::Vector2i(12, -88);
		ppList[3][4][6] = sf::Vector2i(-13, -78);
		ppList[3][4][7] = sf::Vector2i(12, -66);
		ppList[3][4][8] = sf::Vector2i(-13, -56);
		ppList[3][4][9] = sf::Vector2i(12, -44);
		ppList[3][4][10] = sf::Vector2i(-13, -34);
		ppList[3][4][11] = sf::Vector2i(12, -22);
		ppList[3][3][0] = sf::Vector2i(48, -115);
		ppList[3][3][1] = sf::Vector2i(73, -103);
		ppList[3][3][2] = sf::Vector2i(48, -92);
		ppList[3][3][3] = sf::Vector2i(73, -80);
		ppList[3][3][4] = sf::Vector2i(48, -70);
		ppList[3][3][5] = sf::Vector2i(73, -58);
		ppList[3][3][6] = sf::Vector2i(48, -48);
		ppList[3][3][7] = sf::Vector2i(73, -36);
		ppList[3][3][8] = sf::Vector2i(48, -26);
		ppList[3][3][9] = sf::Vector2i(73, -14);
		ppList[3][3][10] = sf::Vector2i(48, -3);
		ppList[3][3][11] = sf::Vector2i(73, 6);

		prodsPerLayer = 2;
		prodsPerSlot = 12;

		switch (quality)
		{
		case StoreObject::Quality::Normal:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(1, -2));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));


			this->SelectedObject = new StoreObject(type, quality, 0, -2, apList, wpList, 6, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;
		

	case StoreObject::Type::Freezer:
		this->BuildMode = Builder::Mode::SingleObject;

		ppList[0].resize(4);
		ppList[1].resize(4);
		ppList[2].resize(4);
		ppList[3].resize(4);
		ppList[0][0].resize(24);

		for (unsigned int i = 1; i < ppList[0].size(); i++)
		{
			ppList[0][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[1][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[2][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[3][i].resize(ppList[0][0].size());
		}

		ppList[0][0][0] = sf::Vector2i(56, -26);
		ppList[0][0][1] = sf::Vector2i(56, -21);
		ppList[0][0][2] = sf::Vector2i(56, -16);
		ppList[0][0][3] = sf::Vector2i(56, -11);
		ppList[0][0][4] = sf::Vector2i(56, -6);
		ppList[0][0][5] = sf::Vector2i(56, -1);
		ppList[0][0][6] = sf::Vector2i(56, 4);
		ppList[0][0][7] = sf::Vector2i(56, 9);
		ppList[0][0][8] = sf::Vector2i(56, 14);
		ppList[0][0][9] = sf::Vector2i(56, 19);
		ppList[0][0][10] = sf::Vector2i(56, 24);
		ppList[0][0][11] = sf::Vector2i(56, 29);
		ppList[0][0][12] = sf::Vector2i(78, -37);
		ppList[0][0][13] = sf::Vector2i(78, -32);
		ppList[0][0][14] = sf::Vector2i(78, -27);
		ppList[0][0][15] = sf::Vector2i(78, -22);
		ppList[0][0][16] = sf::Vector2i(78, -17);
		ppList[0][0][17] = sf::Vector2i(78, -12);
		ppList[0][0][18] = sf::Vector2i(78, -7);
		ppList[0][0][19] = sf::Vector2i(78, -2);
		ppList[0][0][20] = sf::Vector2i(78, 3);
		ppList[0][0][21] = sf::Vector2i(78, 8);
		ppList[0][0][22] = sf::Vector2i(78, 13);
		ppList[0][0][23] = sf::Vector2i(78, 18);

		ppList[0][1][0] = sf::Vector2i(112, -55);
		ppList[0][1][1] = sf::Vector2i(112, -50);
		ppList[0][1][2] = sf::Vector2i(112, -45);
		ppList[0][1][3] = sf::Vector2i(112, -40);
		ppList[0][1][4] = sf::Vector2i(112, -35);
		ppList[0][1][5] = sf::Vector2i(112, -30);
		ppList[0][1][6] = sf::Vector2i(112, -25);
		ppList[0][1][7] = sf::Vector2i(112, -20);
		ppList[0][1][8] = sf::Vector2i(112, -15);
		ppList[0][1][9] = sf::Vector2i(112, -10);
		ppList[0][1][10] = sf::Vector2i(112, -5);
		ppList[0][1][11] = sf::Vector2i(112, 0);
		ppList[0][1][12] = sf::Vector2i(134, -66);
		ppList[0][1][13] = sf::Vector2i(134, -61);
		ppList[0][1][14] = sf::Vector2i(134, -56);
		ppList[0][1][15] = sf::Vector2i(134, -51);
		ppList[0][1][16] = sf::Vector2i(134, -46);
		ppList[0][1][17] = sf::Vector2i(134, -41);
		ppList[0][1][18] = sf::Vector2i(134, -36);
		ppList[0][1][19] = sf::Vector2i(134, -31);
		ppList[0][1][20] = sf::Vector2i(134, -26);
		ppList[0][1][21] = sf::Vector2i(134, -21);
		ppList[0][1][22] = sf::Vector2i(134, -16);
		ppList[0][1][23] = sf::Vector2i(134, -11);

		ppList[0][2][0] = sf::Vector2i(33, -40);
		ppList[0][2][1] = sf::Vector2i(33, -35);
		ppList[0][2][2] = sf::Vector2i(33, -30);
		ppList[0][2][3] = sf::Vector2i(33, -25);
		ppList[0][2][4] = sf::Vector2i(33, -20);
		ppList[0][2][5] = sf::Vector2i(33, -15);
		ppList[0][2][6] = sf::Vector2i(33, -10);
		ppList[0][2][7] = sf::Vector2i(33, -5);
		ppList[0][2][8] = sf::Vector2i(33, 0);
		ppList[0][2][9] = sf::Vector2i(33, 5);
		ppList[0][2][10] = sf::Vector2i(33, 10);
		ppList[0][2][11] = sf::Vector2i(33, 15);
		ppList[0][2][12] = sf::Vector2i(55, -50);
		ppList[0][2][13] = sf::Vector2i(55, -45);
		ppList[0][2][14] = sf::Vector2i(55, -40);
		ppList[0][2][15] = sf::Vector2i(55, -35);
		ppList[0][2][16] = sf::Vector2i(55, -30);
		ppList[0][2][17] = sf::Vector2i(55, -25);
		ppList[0][2][18] = sf::Vector2i(55, -20);
		ppList[0][2][19] = sf::Vector2i(55, -15);
		ppList[0][2][20] = sf::Vector2i(55, -10);
		ppList[0][2][21] = sf::Vector2i(55, -5);
		ppList[0][2][22] = sf::Vector2i(55, 0);
		ppList[0][2][23] = sf::Vector2i(55, 5);

		ppList[0][3][0] = sf::Vector2i(89, -69);
		ppList[0][3][1] = sf::Vector2i(89, -64);
		ppList[0][3][2] = sf::Vector2i(89, -59);
		ppList[0][3][3] = sf::Vector2i(89, -54);
		ppList[0][3][4] = sf::Vector2i(89, -49);
		ppList[0][3][5] = sf::Vector2i(89, -44);
		ppList[0][3][6] = sf::Vector2i(89, -39);
		ppList[0][3][7] = sf::Vector2i(89, -34);
		ppList[0][3][8] = sf::Vector2i(89, -29);
		ppList[0][3][9] = sf::Vector2i(89, -24);
		ppList[0][3][10] = sf::Vector2i(89, -19);
		ppList[0][3][11] = sf::Vector2i(89, -14);
		ppList[0][3][12] = sf::Vector2i(111, -80);
		ppList[0][3][13] = sf::Vector2i(111, -75);
		ppList[0][3][14] = sf::Vector2i(111, -70);
		ppList[0][3][15] = sf::Vector2i(111, -65);
		ppList[0][3][16] = sf::Vector2i(111, -60);
		ppList[0][3][17] = sf::Vector2i(111, -55);
		ppList[0][3][18] = sf::Vector2i(111, -50);
		ppList[0][3][19] = sf::Vector2i(111, -45);
		ppList[0][3][20] = sf::Vector2i(111, -40);
		ppList[0][3][21] = sf::Vector2i(111, -35);
		ppList[0][3][22] = sf::Vector2i(111, -30);
		ppList[0][3][23] = sf::Vector2i(111, -25);


		ppList[1][0][0] = sf::Vector2i(56, -34);
		ppList[1][0][1] = sf::Vector2i(56, -29);
		ppList[1][0][2] = sf::Vector2i(56, -24);
		ppList[1][0][3] = sf::Vector2i(56, -19);
		ppList[1][0][4] = sf::Vector2i(56, -14);
		ppList[1][0][5] = sf::Vector2i(56, -9);
		ppList[1][0][6] = sf::Vector2i(56, -4);
		ppList[1][0][7] = sf::Vector2i(56, 1);
		ppList[1][0][8] = sf::Vector2i(56, 6);
		ppList[1][0][9] = sf::Vector2i(56, 11);
		ppList[1][0][10] = sf::Vector2i(56, 16);
		ppList[1][0][11] = sf::Vector2i(56, 21);
		ppList[1][0][12] = sf::Vector2i(78, -23);
		ppList[1][0][13] = sf::Vector2i(78, -18);
		ppList[1][0][14] = sf::Vector2i(78, -13);
		ppList[1][0][15] = sf::Vector2i(78, -8);
		ppList[1][0][16] = sf::Vector2i(78, -3);
		ppList[1][0][17] = sf::Vector2i(78, 2);
		ppList[1][0][18] = sf::Vector2i(78, 7);
		ppList[1][0][19] = sf::Vector2i(78, 12);
		ppList[1][0][20] = sf::Vector2i(78, 17);
		ppList[1][0][21] = sf::Vector2i(78, 22);
		ppList[1][0][22] = sf::Vector2i(78, 27);
		ppList[1][0][23] = sf::Vector2i(78, 32);

		ppList[1][1][0] = sf::Vector2i(112, -5);
		ppList[1][1][1] = sf::Vector2i(112, 0);
		ppList[1][1][2] = sf::Vector2i(112, 5);
		ppList[1][1][3] = sf::Vector2i(112, 10);
		ppList[1][1][4] = sf::Vector2i(112, 15);
		ppList[1][1][5] = sf::Vector2i(112, 20);
		ppList[1][1][6] = sf::Vector2i(112, 25);
		ppList[1][1][7] = sf::Vector2i(112, 30);
		ppList[1][1][8] = sf::Vector2i(112, 35);
		ppList[1][1][9] = sf::Vector2i(112, 40);
		ppList[1][1][10] = sf::Vector2i(112, 45);
		ppList[1][1][11] = sf::Vector2i(112, 50);
		ppList[1][1][12] = sf::Vector2i(134, 6);
		ppList[1][1][13] = sf::Vector2i(134, 11);
		ppList[1][1][14] = sf::Vector2i(134, 16);
		ppList[1][1][15] = sf::Vector2i(134, 21);
		ppList[1][1][16] = sf::Vector2i(134, 26);
		ppList[1][1][17] = sf::Vector2i(134, 31);
		ppList[1][1][18] = sf::Vector2i(134, 36);
		ppList[1][1][19] = sf::Vector2i(134, 41);
		ppList[1][1][20] = sf::Vector2i(134, 46);
		ppList[1][1][21] = sf::Vector2i(134, 51);
		ppList[1][1][22] = sf::Vector2i(134, 56);
		ppList[1][1][23] = sf::Vector2i(134, 61);

		ppList[1][2][0] = sf::Vector2i(80, -48);
		ppList[1][2][1] = sf::Vector2i(80, -43);
		ppList[1][2][2] = sf::Vector2i(80, -38);
		ppList[1][2][3] = sf::Vector2i(80, -33);
		ppList[1][2][4] = sf::Vector2i(80, -28);
		ppList[1][2][5] = sf::Vector2i(80, -23);
		ppList[1][2][6] = sf::Vector2i(80, -18);
		ppList[1][2][7] = sf::Vector2i(80, -13);
		ppList[1][2][8] = sf::Vector2i(80, -8);
		ppList[1][2][9] = sf::Vector2i(80, -3);
		ppList[1][2][10] = sf::Vector2i(80, 2);
		ppList[1][2][11] = sf::Vector2i(80, 7);
		ppList[1][2][12] = sf::Vector2i(102, -36);
		ppList[1][2][13] = sf::Vector2i(102, -31);
		ppList[1][2][14] = sf::Vector2i(102, -26);
		ppList[1][2][15] = sf::Vector2i(102, -21);
		ppList[1][2][16] = sf::Vector2i(102, -16);
		ppList[1][2][17] = sf::Vector2i(102, -11);
		ppList[1][2][18] = sf::Vector2i(102, -6);
		ppList[1][2][19] = sf::Vector2i(102, -1);
		ppList[1][2][20] = sf::Vector2i(102, 4);
		ppList[1][2][21] = sf::Vector2i(102, 9);
		ppList[1][2][22] = sf::Vector2i(102, 14);
		ppList[1][2][23] = sf::Vector2i(102, 19);

		ppList[1][3][0] = sf::Vector2i(135, -18);
		ppList[1][3][1] = sf::Vector2i(135, -13);
		ppList[1][3][2] = sf::Vector2i(135, -8);
		ppList[1][3][3] = sf::Vector2i(135, -3);
		ppList[1][3][4] = sf::Vector2i(135, 2);
		ppList[1][3][5] = sf::Vector2i(135, 7);
		ppList[1][3][6] = sf::Vector2i(135, 12);
		ppList[1][3][7] = sf::Vector2i(135, 17);
		ppList[1][3][8] = sf::Vector2i(135, 22);
		ppList[1][3][9] = sf::Vector2i(135, 27);
		ppList[1][3][10] = sf::Vector2i(135, 32);
		ppList[1][3][11] = sf::Vector2i(135, 37);
		ppList[1][3][12] = sf::Vector2i(157, -8);
		ppList[1][3][13] = sf::Vector2i(157, -3);
		ppList[1][3][14] = sf::Vector2i(157, 2);
		ppList[1][3][15] = sf::Vector2i(157, 7);
		ppList[1][3][16] = sf::Vector2i(157, 12);
		ppList[1][3][17] = sf::Vector2i(157, 17);
		ppList[1][3][18] = sf::Vector2i(157, 22);
		ppList[1][3][19] = sf::Vector2i(157, 27);
		ppList[1][3][20] = sf::Vector2i(157, 32);
		ppList[1][3][21] = sf::Vector2i(157, 37);
		ppList[1][3][22] = sf::Vector2i(157, 42);
		ppList[1][3][23] = sf::Vector2i(157, 47);


		ppList[2][3][0] = sf::Vector2i(-8, 5);
		ppList[2][3][1] = sf::Vector2i(-8, 10);
		ppList[2][3][2] = sf::Vector2i(-8, 15);
		ppList[2][3][3] = sf::Vector2i(-8, 20);
		ppList[2][3][4] = sf::Vector2i(-8, 25);
		ppList[2][3][5] = sf::Vector2i(-8, 30);
		ppList[2][3][6] = sf::Vector2i(-8, 35);
		ppList[2][3][7] = sf::Vector2i(-8, 40);
		ppList[2][3][8] = sf::Vector2i(-8, 45);
		ppList[2][3][9] = sf::Vector2i(-8, 50);
		ppList[2][3][10] = sf::Vector2i(-8, 55);
		ppList[2][3][11] = sf::Vector2i(-8, 60);
		ppList[2][3][12] = sf::Vector2i(14, -6);
		ppList[2][3][13] = sf::Vector2i(14, -1);
		ppList[2][3][14] = sf::Vector2i(14, 4);
		ppList[2][3][15] = sf::Vector2i(14, 9);
		ppList[2][3][16] = sf::Vector2i(14, 14);
		ppList[2][3][17] = sf::Vector2i(14, 19);
		ppList[2][3][18] = sf::Vector2i(14, 24);
		ppList[2][3][19] = sf::Vector2i(14, 29);
		ppList[2][3][20] = sf::Vector2i(14, 34);
		ppList[2][3][21] = sf::Vector2i(14, 39);
		ppList[2][3][22] = sf::Vector2i(14, 44);
		ppList[2][3][23] = sf::Vector2i(14, 49);

		ppList[2][2][0] = sf::Vector2i(48, -23);
		ppList[2][2][1] = sf::Vector2i(48, -18);
		ppList[2][2][2] = sf::Vector2i(48, -13);
		ppList[2][2][3] = sf::Vector2i(48, -8);
		ppList[2][2][4] = sf::Vector2i(48, -3);
		ppList[2][2][5] = sf::Vector2i(48, 2);
		ppList[2][2][6] = sf::Vector2i(48, 7);
		ppList[2][2][7] = sf::Vector2i(48, 12);
		ppList[2][2][8] = sf::Vector2i(48, 17);
		ppList[2][2][9] = sf::Vector2i(48, 22);
		ppList[2][2][10] = sf::Vector2i(48, 27);
		ppList[2][2][11] = sf::Vector2i(48, 32);
		ppList[2][2][12] = sf::Vector2i(70, -34);
		ppList[2][2][13] = sf::Vector2i(70, -29);
		ppList[2][2][14] = sf::Vector2i(70, -24);
		ppList[2][2][15] = sf::Vector2i(70, -19);
		ppList[2][2][16] = sf::Vector2i(70, -14);
		ppList[2][2][17] = sf::Vector2i(70, -9);
		ppList[2][2][18] = sf::Vector2i(70, -4);
		ppList[2][2][19] = sf::Vector2i(70, 1);
		ppList[2][2][20] = sf::Vector2i(70, 6);
		ppList[2][2][21] = sf::Vector2i(70, 11);
		ppList[2][2][22] = sf::Vector2i(70, 16);
		ppList[2][2][23] = sf::Vector2i(70, 21);

		ppList[2][1][0] = sf::Vector2i(-31, -8);
		ppList[2][1][1] = sf::Vector2i(-31, -3);
		ppList[2][1][2] = sf::Vector2i(-31, 2);
		ppList[2][1][3] = sf::Vector2i(-31, 7);
		ppList[2][1][4] = sf::Vector2i(-31, 12);
		ppList[2][1][5] = sf::Vector2i(-31, 17);
		ppList[2][1][6] = sf::Vector2i(-31, 22);
		ppList[2][1][7] = sf::Vector2i(-31, 27);
		ppList[2][1][8] = sf::Vector2i(-31, 32);
		ppList[2][1][9] = sf::Vector2i(-31, 37);
		ppList[2][1][10] = sf::Vector2i(-31, 42);
		ppList[2][1][11] = sf::Vector2i(-31, 47);
		ppList[2][1][12] = sf::Vector2i(-8, -20);
		ppList[2][1][13] = sf::Vector2i(-8, -15);
		ppList[2][1][14] = sf::Vector2i(-8, -10);
		ppList[2][1][15] = sf::Vector2i(-8, -5);
		ppList[2][1][16] = sf::Vector2i(-8, 0);
		ppList[2][1][17] = sf::Vector2i(-8, 5);
		ppList[2][1][18] = sf::Vector2i(-8, 10);
		ppList[2][1][19] = sf::Vector2i(-8, 15);
		ppList[2][1][20] = sf::Vector2i(-8, 20);
		ppList[2][1][21] = sf::Vector2i(-8, 25);
		ppList[2][1][22] = sf::Vector2i(-8, 30);
		ppList[2][1][23] = sf::Vector2i(-8, 35);

		ppList[2][0][0] = sf::Vector2i(25, -37);
		ppList[2][0][1] = sf::Vector2i(25, -32);
		ppList[2][0][2] = sf::Vector2i(25, -27);
		ppList[2][0][3] = sf::Vector2i(25, -22);
		ppList[2][0][4] = sf::Vector2i(25, -17);
		ppList[2][0][5] = sf::Vector2i(25, -12);
		ppList[2][0][6] = sf::Vector2i(25, -7);
		ppList[2][0][7] = sf::Vector2i(25, -2);
		ppList[2][0][8] = sf::Vector2i(25, 3);
		ppList[2][0][9] = sf::Vector2i(25, 8);
		ppList[2][0][10] = sf::Vector2i(25, 13);
		ppList[2][0][11] = sf::Vector2i(25, 18);
		ppList[2][0][12] = sf::Vector2i(47, -49);
		ppList[2][0][13] = sf::Vector2i(47, -44);
		ppList[2][0][14] = sf::Vector2i(47, -39);
		ppList[2][0][15] = sf::Vector2i(47, -34);
		ppList[2][0][16] = sf::Vector2i(47, -29);
		ppList[2][0][17] = sf::Vector2i(47, -24);
		ppList[2][0][18] = sf::Vector2i(47, -19);
		ppList[2][0][19] = sf::Vector2i(47, -14);
		ppList[2][0][20] = sf::Vector2i(47, -9);
		ppList[2][0][21] = sf::Vector2i(47, -4);
		ppList[2][0][22] = sf::Vector2i(47, 1);
		ppList[2][0][23] = sf::Vector2i(47, 6);


		ppList[3][3][0] = sf::Vector2i(-7, -64);
		ppList[3][3][1] = sf::Vector2i(-7, -59);
		ppList[3][3][2] = sf::Vector2i(-7, -54);
		ppList[3][3][3] = sf::Vector2i(-7, -49);
		ppList[3][3][4] = sf::Vector2i(-7, -44);
		ppList[3][3][5] = sf::Vector2i(-7, -39);
		ppList[3][3][6] = sf::Vector2i(-7, -34);
		ppList[3][3][7] = sf::Vector2i(-7, -29);
		ppList[3][3][8] = sf::Vector2i(-7, -24);
		ppList[3][3][9] = sf::Vector2i(-7, -19);
		ppList[3][3][10] = sf::Vector2i(-7, -14);
		ppList[3][3][11] = sf::Vector2i(-7, -9);
		ppList[3][3][12] = sf::Vector2i(15, -54);
		ppList[3][3][13] = sf::Vector2i(15, -49);
		ppList[3][3][14] = sf::Vector2i(15, -44);
		ppList[3][3][15] = sf::Vector2i(15, -39);
		ppList[3][3][16] = sf::Vector2i(15, -34);
		ppList[3][3][17] = sf::Vector2i(15, -29);
		ppList[3][3][18] = sf::Vector2i(15, -24);
		ppList[3][3][19] = sf::Vector2i(15, -19);
		ppList[3][3][20] = sf::Vector2i(15, -14);
		ppList[3][3][21] = sf::Vector2i(15, -9);
		ppList[3][3][22] = sf::Vector2i(15, -4);
		ppList[3][3][23] = sf::Vector2i(15, 1);

		ppList[3][2][0] = sf::Vector2i(49, -36);
		ppList[3][2][1] = sf::Vector2i(49, -31);
		ppList[3][2][2] = sf::Vector2i(49, -26);
		ppList[3][2][3] = sf::Vector2i(49, -21);
		ppList[3][2][4] = sf::Vector2i(49, -16);
		ppList[3][2][5] = sf::Vector2i(49, -11);
		ppList[3][2][6] = sf::Vector2i(49, -6);
		ppList[3][2][7] = sf::Vector2i(49, -1);
		ppList[3][2][8] = sf::Vector2i(49, 4);
		ppList[3][2][9] = sf::Vector2i(49, 9);
		ppList[3][2][10] = sf::Vector2i(49, 14);
		ppList[3][2][11] = sf::Vector2i(49, 19);
		ppList[3][2][12] = sf::Vector2i(71, -24);
		ppList[3][2][13] = sf::Vector2i(71, -19);
		ppList[3][2][14] = sf::Vector2i(71, -14);
		ppList[3][2][15] = sf::Vector2i(71, -9);
		ppList[3][2][16] = sf::Vector2i(71, -4);
		ppList[3][2][17] = sf::Vector2i(71, 1);
		ppList[3][2][18] = sf::Vector2i(71, 6);
		ppList[3][2][19] = sf::Vector2i(71, 11);
		ppList[3][2][20] = sf::Vector2i(71, 16);
		ppList[3][2][21] = sf::Vector2i(71, 21);
		ppList[3][2][22] = sf::Vector2i(71, 26);
		ppList[3][2][23] = sf::Vector2i(71, 31);

		ppList[3][1][0] = sf::Vector2i(16, -79);
		ppList[3][1][1] = sf::Vector2i(16, -74);
		ppList[3][1][2] = sf::Vector2i(16, -69);
		ppList[3][1][3] = sf::Vector2i(16, -64);
		ppList[3][1][4] = sf::Vector2i(16, -59);
		ppList[3][1][5] = sf::Vector2i(16, -54);
		ppList[3][1][6] = sf::Vector2i(16, -49);
		ppList[3][1][7] = sf::Vector2i(16, -44);
		ppList[3][1][8] = sf::Vector2i(16, -39);
		ppList[3][1][9] = sf::Vector2i(16, -34);
		ppList[3][1][10] = sf::Vector2i(16, -29);
		ppList[3][1][11] = sf::Vector2i(16, -24);
		ppList[3][1][12] = sf::Vector2i(38, -68);
		ppList[3][1][13] = sf::Vector2i(38, -63);
		ppList[3][1][14] = sf::Vector2i(38, -58);
		ppList[3][1][15] = sf::Vector2i(38, -53);
		ppList[3][1][16] = sf::Vector2i(38, -48);
		ppList[3][1][17] = sf::Vector2i(38, -43);
		ppList[3][1][18] = sf::Vector2i(38, -38);
		ppList[3][1][19] = sf::Vector2i(38, -33);
		ppList[3][1][20] = sf::Vector2i(38, -28);
		ppList[3][1][21] = sf::Vector2i(38, -23);
		ppList[3][1][22] = sf::Vector2i(38, -18);
		ppList[3][1][23] = sf::Vector2i(38, -13);

		ppList[3][0][0] = sf::Vector2i(73, -50);
		ppList[3][0][1] = sf::Vector2i(73, -45);
		ppList[3][0][2] = sf::Vector2i(73, -40);
		ppList[3][0][3] = sf::Vector2i(73, -35);
		ppList[3][0][4] = sf::Vector2i(73, -30);
		ppList[3][0][5] = sf::Vector2i(73, -25);
		ppList[3][0][6] = sf::Vector2i(73, -20);
		ppList[3][0][7] = sf::Vector2i(73, -15);
		ppList[3][0][8] = sf::Vector2i(73, -10);
		ppList[3][0][9] = sf::Vector2i(73, -5);
		ppList[3][0][10] = sf::Vector2i(73, 0);
		ppList[3][0][11] = sf::Vector2i(73, 5);
		ppList[3][0][12] = sf::Vector2i(94, -39);
		ppList[3][0][13] = sf::Vector2i(94, -34);
		ppList[3][0][14] = sf::Vector2i(94, -29);
		ppList[3][0][15] = sf::Vector2i(94, -24);
		ppList[3][0][16] = sf::Vector2i(94, -19);
		ppList[3][0][17] = sf::Vector2i(94, -14);
		ppList[3][0][18] = sf::Vector2i(94, -9);
		ppList[3][0][19] = sf::Vector2i(94, -4);
		ppList[3][0][20] = sf::Vector2i(94, 1);
		ppList[3][0][21] = sf::Vector2i(94, 6);
		ppList[3][0][22] = sf::Vector2i(94, 11);
		ppList[3][0][23] = sf::Vector2i(94, 16);

		prodsPerLayer = 24;
		prodsPerSlot = 24;

		switch (quality)
		{
		case StoreObject::Quality::Normal:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));

			this->SelectedObject = new StoreObject(type, quality, 0, -1, apList, wpList, 4, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;


	case StoreObject::Type::Palette:
		this->BuildMode = Builder::Mode::SingleObject;
		switch (quality)
		{
		case StoreObject::Quality::Normal:
			apList.push_back(sf::Vector2i(1, 0));
			apList.push_back(sf::Vector2i(1, -1));
			apList.push_back(sf::Vector2i(0, -2));
			apList.push_back(sf::Vector2i(-1, -2));
			apList.push_back(sf::Vector2i(-2, -1));
			apList.push_back(sf::Vector2i(-2, 0));
			apList.push_back(sf::Vector2i(-1, 1));
			apList.push_back(sf::Vector2i(0, 1));

			this->SelectedObject = new StoreObject(type, quality, -1, -1, apList, wpList, 4, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
			this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			break;
		}
		break;


	case StoreObject::Type::Checkout:
		this->BuildMode = Builder::Mode::SingleObject;

		prodsPerLayer = 1;
		prodsPerSlot = 1;

		switch (quality)
		{
		case StoreObject::Quality::Cheap:
			apList.push_back(sf::Vector2i(2, 1));
			apList.push_back(sf::Vector2i(1, 1));
			apList.push_back(sf::Vector2i(0, 1));
			apList.push_back(sf::Vector2i(-1, 1));
			apList.push_back(sf::Vector2i(-2, 1));
			apList.push_back(sf::Vector2i(-3, 1));
			
			wpList.push_back(sf::Vector2i(1, -1));

			this->SelectedObject = new StoreObject(type, quality, 2, 0, apList, wpList, 4, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}

			this->ExtraObject = new StoreObject(StoreObject::Type::Shelf, StoreObject::Quality::Nothing, 0, -1, std::vector<sf::Vector2i>(), std::vector<sf::Vector2i>(), 4, ppList, 0, 0);
			this->ExtraObject->SpriteName = spriteName + "_Extra";
			if (texHandler->Exists(this->ExtraObject->SpriteName + "_Front"))
			{
				this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Front");
				this->ExtraObject->Origins = texHandler->GetOrigins(this->ExtraObject->SpriteName + "_Front");
			}
			else
			{
				this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName);
				this->ExtraObject->Origins = texHandler->GetOrigins(this->ExtraObject->SpriteName);
			}
			break;
		}
		break;


	case StoreObject::Type::Fruitshelf:
		this->BuildMode = Builder::Mode::SingleObject;

		ppList[0].resize(8);
		ppList[1].resize(8);
		ppList[2].resize(8);
		ppList[3].resize(8);
		ppList[0][0].resize(25);

		for (unsigned int i = 1; i < ppList[0].size(); i++)
		{
			ppList[0][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[1][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[2][i].resize(ppList[0][0].size());
		}
		for (unsigned int i = 0; i < ppList[1].size(); i++)
		{
			ppList[3][i].resize(ppList[0][0].size());
		}

		ppList[0][0][0] = sf::Vector2i(174, 49);
		ppList[0][0][1] = sf::Vector2i(184, 44);
		ppList[0][0][2] = sf::Vector2i(194, 39);
		ppList[0][0][3] = sf::Vector2i(204, 34);
		ppList[0][0][4] = sf::Vector2i(214, 29);
		ppList[0][0][5] = sf::Vector2i(162, 34);
		ppList[0][0][6] = sf::Vector2i(172, 29);
		ppList[0][0][7] = sf::Vector2i(182, 24);
		ppList[0][0][8] = sf::Vector2i(192, 19);
		ppList[0][0][9] = sf::Vector2i(202, 14);
		ppList[0][0][10] = sf::Vector2i(148, 19);
		ppList[0][0][11] = sf::Vector2i(158, 14);
		ppList[0][0][12] = sf::Vector2i(168, 9);
		ppList[0][0][13] = sf::Vector2i(178, 4);
		ppList[0][0][14] = sf::Vector2i(188, -1);
		ppList[0][0][15] = sf::Vector2i(134, 2);
		ppList[0][0][16] = sf::Vector2i(144, -3);
		ppList[0][0][17] = sf::Vector2i(154, -8);
		ppList[0][0][18] = sf::Vector2i(164, -13);
		ppList[0][0][19] = sf::Vector2i(174, -18);
		ppList[0][0][20] = sf::Vector2i(120, -14);
		ppList[0][0][21] = sf::Vector2i(130, -19);
		ppList[0][0][22] = sf::Vector2i(140, -24);
		ppList[0][0][23] = sf::Vector2i(150, -29);
		ppList[0][0][24] = sf::Vector2i(160, -34);

		ppList[0][1][0] = sf::Vector2i(240, 15);
		for (int i = 1; i < 5; i++)
		{
			ppList[0][1][i] = sf::Vector2i(ppList[0][1][i - 1].x + 10, ppList[0][1][i - 1].y - 5);
		}
		ppList[0][1][5] = sf::Vector2i(228, 0);
		for (int i = 6; i < 10; i++)
		{
			ppList[0][1][i] = sf::Vector2i(ppList[0][1][i - 1].x + 10, ppList[0][1][i - 1].y - 5);
		}
		ppList[0][1][10] = sf::Vector2i(214, -15);
		for (int i = 11; i < 15; i++)
		{
			ppList[0][1][i] = sf::Vector2i(ppList[0][1][i - 1].x + 10, ppList[0][1][i - 1].y - 5);
		}
		ppList[0][1][15] = sf::Vector2i(200, -33);
		for (int i = 16; i < 20; i++)
		{
			ppList[0][1][i] = sf::Vector2i(ppList[0][1][i - 1].x + 10, ppList[0][1][i - 1].y - 5);
		}
		ppList[0][1][20] = sf::Vector2i(185, -47);
		for (int i = 21; i < 25; i++)
		{
			ppList[0][1][i] = sf::Vector2i(ppList[0][1][i - 1].x + 10, ppList[0][1][i - 1].y - 5);
		}

		ppList[0][2][0] = sf::Vector2i(305, -16);
		for (int i = 1; i < 5; i++)
		{
			ppList[0][2][i] = sf::Vector2i(ppList[0][2][i - 1].x + 10, ppList[0][2][i - 1].y - 5);
		}
		ppList[0][2][5] = sf::Vector2i(291, -31);
		for (int i = 6; i < 10; i++)
		{
			ppList[0][2][i] = sf::Vector2i(ppList[0][2][i - 1].x + 10, ppList[0][2][i - 1].y - 5);
		}
		ppList[0][2][10] = sf::Vector2i(278, -47);
		for (int i = 11; i < 15; i++)
		{
			ppList[0][2][i] = sf::Vector2i(ppList[0][2][i - 1].x + 10, ppList[0][2][i - 1].y - 5);
		}
		ppList[0][2][15] = sf::Vector2i(264, -65);
		for (int i = 16; i < 20; i++)
		{
			ppList[0][2][i] = sf::Vector2i(ppList[0][2][i - 1].x + 10, ppList[0][2][i - 1].y - 5);
		}
		ppList[0][2][20] = sf::Vector2i(250, -80);
		for (int i = 21; i < 25; i++)
		{
			ppList[0][2][i] = sf::Vector2i(ppList[0][2][i - 1].x + 10, ppList[0][2][i - 1].y - 5);
		}

		ppList[0][3][0] = sf::Vector2i(369, -50);
		for (int i = 1; i < 5; i++)
		{
			ppList[0][3][i] = sf::Vector2i(ppList[0][3][i - 1].x + 10, ppList[0][3][i - 1].y - 5);
		}
		ppList[0][3][5] = sf::Vector2i(355, -65);
		for (int i = 6; i < 10; i++)
		{
			ppList[0][3][i] = sf::Vector2i(ppList[0][3][i - 1].x + 10, ppList[0][3][i - 1].y - 5);
		}
		ppList[0][3][10] = sf::Vector2i(341, -80);
		for (int i = 11; i < 15; i++)
		{
			ppList[0][3][i] = sf::Vector2i(ppList[0][3][i - 1].x + 10, ppList[0][3][i - 1].y - 5);
		}
		ppList[0][3][15] = sf::Vector2i(327, -97);
		for (int i = 16; i < 20; i++)
		{
			ppList[0][3][i] = sf::Vector2i(ppList[0][3][i - 1].x + 10, ppList[0][3][i - 1].y - 5);
		}
		ppList[0][3][20] = sf::Vector2i(314, -112);
		for (int i = 21; i < 25; i++)
		{
			ppList[0][3][i] = sf::Vector2i(ppList[0][3][i - 1].x + 10, ppList[0][3][i - 1].y - 5);
		}


		ppList[1][0][0] = sf::Vector2i(-59, 49);
		for (int i = 1; i < 5; i++)
		{
			ppList[1][0][i] = sf::Vector2i(ppList[1][0][i - 1].x - 10, ppList[1][0][i - 1].y - 5);
		}
		ppList[1][0][5] = sf::Vector2i(-46, 34);
		for (int i = 6; i < 10; i++)
		{
			ppList[1][0][i] = sf::Vector2i(ppList[1][0][i - 1].x - 10, ppList[1][0][i - 1].y - 5);
		}
		ppList[1][0][10] = sf::Vector2i(-32, 18);
		for (int i = 11; i < 15; i++)
		{
			ppList[1][0][i] = sf::Vector2i(ppList[1][0][i - 1].x - 10, ppList[1][0][i - 1].y - 5);
		}
		ppList[1][0][15] = sf::Vector2i(-18, 1);
		for (int i = 16; i < 20; i++)
		{
			ppList[1][0][i] = sf::Vector2i(ppList[1][0][i - 1].x - 10, ppList[1][0][i - 1].y - 5);
		}
		ppList[1][0][20] = sf::Vector2i(-4, -13);
		for (int i = 21; i < 25; i++)
		{
			ppList[1][0][i] = sf::Vector2i(ppList[1][0][i - 1].x - 10, ppList[1][0][i - 1].y - 5);
		}

		ppList[1][1][0] = sf::Vector2i(4, 82);
		for (int i = 1; i < 5; i++)
		{
			ppList[1][1][i] = sf::Vector2i(ppList[1][1][i - 1].x - 10, ppList[1][1][i - 1].y - 5);
		}
		ppList[1][1][5] = sf::Vector2i(17, 66);
		for (int i = 6; i < 10; i++)
		{
			ppList[1][1][i] = sf::Vector2i(ppList[1][1][i - 1].x - 10, ppList[1][1][i - 1].y - 5);
		}
		ppList[1][1][10] = sf::Vector2i(32, 51);
		for (int i = 11; i < 15; i++)
		{
			ppList[1][1][i] = sf::Vector2i(ppList[1][1][i - 1].x - 10, ppList[1][1][i - 1].y - 5);
		}
		ppList[1][1][15] = sf::Vector2i(45, 34);
		for (int i = 16; i < 20; i++)
		{
			ppList[1][1][i] = sf::Vector2i(ppList[1][1][i - 1].x - 10, ppList[1][1][i - 1].y - 5);
		}
		ppList[1][1][20] = sf::Vector2i(59, 18);
		for (int i = 21; i < 25; i++)
		{
			ppList[1][1][i] = sf::Vector2i(ppList[1][1][i - 1].x - 10, ppList[1][1][i - 1].y - 5);
		}

		ppList[1][2][0] = sf::Vector2i(68, 114);
		for (int i = 1; i < 5; i++)
		{
			ppList[1][2][i] = sf::Vector2i(ppList[1][2][i - 1].x - 10, ppList[1][2][i - 1].y - 5);
		}
		ppList[1][2][5] = sf::Vector2i(82, 99);
		for (int i = 6; i < 10; i++)
		{
			ppList[1][2][i] = sf::Vector2i(ppList[1][2][i - 1].x - 10, ppList[1][2][i - 1].y - 5);
		}
		ppList[1][2][10] = sf::Vector2i(96, 83);
		for (int i = 11; i < 15; i++)
		{
			ppList[1][2][i] = sf::Vector2i(ppList[1][2][i - 1].x - 10, ppList[1][2][i - 1].y - 5);
		}
		ppList[1][2][15] = sf::Vector2i(109, 66);
		for (int i = 16; i < 20; i++)
		{
			ppList[1][2][i] = sf::Vector2i(ppList[1][2][i - 1].x - 10, ppList[1][2][i - 1].y - 5);
		}
		ppList[1][2][20] = sf::Vector2i(123, 50);
		for (int i = 21; i < 25; i++)
		{
			ppList[1][2][i] = sf::Vector2i(ppList[1][2][i - 1].x - 10, ppList[1][2][i - 1].y - 5);
		}

		ppList[1][3][0] = sf::Vector2i(133, 147);
		for (int i = 1; i < 5; i++)
		{
			ppList[1][3][i] = sf::Vector2i(ppList[1][3][i - 1].x - 10, ppList[1][3][i - 1].y - 5);
		}
		ppList[1][3][5] = sf::Vector2i(145, 132);
		for (int i = 6; i < 10; i++)
		{
			ppList[1][3][i] = sf::Vector2i(ppList[1][3][i - 1].x - 10, ppList[1][3][i - 1].y - 5);
		}
		ppList[1][3][10] = sf::Vector2i(159, 116);
		for (int i = 11; i < 15; i++)
		{
			ppList[1][3][i] = sf::Vector2i(ppList[1][3][i - 1].x - 10, ppList[1][3][i - 1].y - 5);
		}
		ppList[1][3][15] = sf::Vector2i(173, 99);
		for (int i = 16; i < 20; i++)
		{
			ppList[1][3][i] = sf::Vector2i(ppList[1][3][i - 1].x - 10, ppList[1][3][i - 1].y - 5);
		}
		ppList[1][3][20] = sf::Vector2i(187, 83);
		for (int i = 21; i < 25; i++)
		{
			ppList[1][3][i] = sf::Vector2i(ppList[1][3][i - 1].x - 10, ppList[1][3][i - 1].y - 5);
		}


		ppList[2][7][0] = sf::Vector2i(-135, 81);
		for (int i = 1; i < 5; i++)
		{
			ppList[2][7][i] = sf::Vector2i(ppList[2][7][i - 1].x + 10, ppList[2][7][i - 1].y - 5);
		}
		ppList[2][7][5] = sf::Vector2i(-148, 66);
		for (int i = 6; i < 10; i++)
		{
			ppList[2][7][i] = sf::Vector2i(ppList[2][7][i - 1].x + 10, ppList[2][7][i - 1].y - 5);
		}
		ppList[2][7][10] = sf::Vector2i(-162, 50);
		for (int i = 11; i < 15; i++)
		{
			ppList[2][7][i] = sf::Vector2i(ppList[2][7][i - 1].x + 10, ppList[2][7][i - 1].y - 5);
		}
		ppList[2][7][15] = sf::Vector2i(-176, 33);
		for (int i = 16; i < 20; i++)
		{
			ppList[2][7][i] = sf::Vector2i(ppList[2][7][i - 1].x + 10, ppList[2][7][i - 1].y - 5);
		}
		ppList[2][7][20] = sf::Vector2i(-191, 18);
		for (int i = 21; i < 25; i++)
		{
			ppList[2][7][i] = sf::Vector2i(ppList[2][7][i - 1].x + 10, ppList[2][7][i - 1].y - 5);
		}

		ppList[2][6][0] = sf::Vector2i(-71, 48);
		for (int i = 1; i < 5; i++)
		{
			ppList[2][6][i] = sf::Vector2i(ppList[2][6][i - 1].x + 10, ppList[2][6][i - 1].y - 5);
		}
		ppList[2][6][5] = sf::Vector2i(-84, 32);
		for (int i = 6; i < 10; i++)
		{
			ppList[2][6][i] = sf::Vector2i(ppList[2][6][i - 1].x + 10, ppList[2][6][i - 1].y - 5);
		}
		ppList[2][6][10] = sf::Vector2i(-98, 17);
		for (int i = 11; i < 15; i++)
		{
			ppList[2][6][i] = sf::Vector2i(ppList[2][6][i - 1].x + 10, ppList[2][6][i - 1].y - 5);
		}
		ppList[2][6][15] = sf::Vector2i(-112, 0);
		for (int i = 16; i < 20; i++)
		{
			ppList[2][6][i] = sf::Vector2i(ppList[2][6][i - 1].x + 10, ppList[2][6][i - 1].y - 5);
		}
		ppList[2][6][20] = sf::Vector2i(-127, -15);
		for (int i = 21; i < 25; i++)
		{
			ppList[2][6][i] = sf::Vector2i(ppList[2][6][i - 1].x + 10, ppList[2][6][i - 1].y - 5);
		}

		ppList[2][5][0] = sf::Vector2i(-7, 16);
		for (int i = 1; i < 5; i++)
		{
			ppList[2][5][i] = sf::Vector2i(ppList[2][5][i - 1].x + 10, ppList[2][5][i - 1].y - 5);
		}
		ppList[2][5][5] = sf::Vector2i(-20, 1);
		for (int i = 6; i < 10; i++)
		{
			ppList[2][5][i] = sf::Vector2i(ppList[2][5][i - 1].x + 10, ppList[2][5][i - 1].y - 5);
		}
		ppList[2][5][10] = sf::Vector2i(-34, -15);
		for (int i = 11; i < 15; i++)
		{
			ppList[2][5][i] = sf::Vector2i(ppList[2][5][i - 1].x + 10, ppList[2][5][i - 1].y - 5);
		}
		ppList[2][5][15] = sf::Vector2i(-48, -31);
		for (int i = 16; i < 20; i++)
		{
			ppList[2][5][i] = sf::Vector2i(ppList[2][5][i - 1].x + 10, ppList[2][5][i - 1].y - 5);
		}
		ppList[2][5][20] = sf::Vector2i(-62, -45);
		for (int i = 21; i < 25; i++)
		{
			ppList[2][5][i] = sf::Vector2i(ppList[2][5][i - 1].x + 10, ppList[2][5][i - 1].y - 5);
		}

		ppList[2][4][0] = sf::Vector2i(56, -16);
		for (int i = 1; i < 5; i++)
		{
			ppList[2][4][i] = sf::Vector2i(ppList[2][4][i - 1].x + 10, ppList[2][4][i - 1].y - 5);
		}
		ppList[2][4][5] = sf::Vector2i(43, -31);
		for (int i = 6; i < 10; i++)
		{
			ppList[2][4][i] = sf::Vector2i(ppList[2][4][i - 1].x + 10, ppList[2][4][i - 1].y - 5);
		}
		ppList[2][4][10] = sf::Vector2i(30, -48);
		for (int i = 11; i < 15; i++)
		{
			ppList[2][4][i] = sf::Vector2i(ppList[2][4][i - 1].x + 10, ppList[2][4][i - 1].y - 5);
		}
		ppList[2][4][15] = sf::Vector2i(16, -65);
		for (int i = 16; i < 20; i++)
		{
			ppList[2][4][i] = sf::Vector2i(ppList[2][4][i - 1].x + 10, ppList[2][4][i - 1].y - 5);
		}
		ppList[2][4][20] = sf::Vector2i(1, -79);
		for (int i = 21; i < 25; i++)
		{
			ppList[2][4][i] = sf::Vector2i(ppList[2][4][i - 1].x + 10, ppList[2][4][i - 1].y - 5);
		}


		ppList[3][7][0] = sf::Vector2i(-122, -112);
		for (int i = 1; i < 5; i++)
		{
			ppList[3][7][i] = sf::Vector2i(ppList[3][7][i - 1].x - 10, ppList[3][7][i - 1].y - 5);
		}
		ppList[3][7][5] = sf::Vector2i(-109, -126);
		for (int i = 6; i < 10; i++)
		{
			ppList[3][7][i] = sf::Vector2i(ppList[3][7][i - 1].x - 10, ppList[3][7][i - 1].y - 5);
		}
		ppList[3][7][10] = sf::Vector2i(-95, -142);
		for (int i = 11; i < 15; i++)
		{
			ppList[3][7][i] = sf::Vector2i(ppList[3][7][i - 1].x - 10, ppList[3][7][i - 1].y - 5);
		}
		ppList[3][7][15] = sf::Vector2i(-81, -160);
		for (int i = 16; i < 20; i++)
		{
			ppList[3][7][i] = sf::Vector2i(ppList[3][7][i - 1].x - 10, ppList[3][7][i - 1].y - 5);
		}
		ppList[3][7][20] = sf::Vector2i(-67, -175);
		for (int i = 21; i < 25; i++)
		{
			ppList[3][7][i] = sf::Vector2i(ppList[3][7][i - 1].x - 10, ppList[3][7][i - 1].y - 5);
		}

		ppList[3][6][0] = sf::Vector2i(-58, -79);
		for (int i = 1; i < 5; i++)
		{
			ppList[3][6][i] = sf::Vector2i(ppList[3][6][i - 1].x - 10, ppList[3][6][i - 1].y - 5);
		}
		ppList[3][6][5] = sf::Vector2i(-45, -93);
		for (int i = 6; i < 10; i++)
		{
			ppList[3][6][i] = sf::Vector2i(ppList[3][6][i - 1].x - 10, ppList[3][6][i - 1].y - 5);
		}
		ppList[3][6][10] = sf::Vector2i(-31, -109);
		for (int i = 11; i < 15; i++)
		{
			ppList[3][6][i] = sf::Vector2i(ppList[3][6][i - 1].x - 10, ppList[3][6][i - 1].y - 5);
		}
		ppList[3][6][15] = sf::Vector2i(-17, -127);
		for (int i = 16; i < 20; i++)
		{
			ppList[3][6][i] = sf::Vector2i(ppList[3][6][i - 1].x - 10, ppList[3][6][i - 1].y - 5);
		}
		ppList[3][6][20] = sf::Vector2i(-3, -142);
		for (int i = 21; i < 25; i++)
		{
			ppList[3][6][i] = sf::Vector2i(ppList[3][6][i - 1].x - 10, ppList[3][6][i - 1].y - 5);
		}

		ppList[3][5][0] = sf::Vector2i(6, -47);
		for (int i = 1; i < 5; i++)
		{
			ppList[3][5][i] = sf::Vector2i(ppList[3][5][i - 1].x - 10, ppList[3][5][i - 1].y - 5);
		}
		ppList[3][5][5] = sf::Vector2i(19, -61);
		for (int i = 6; i < 10; i++)
		{
			ppList[3][5][i] = sf::Vector2i(ppList[3][5][i - 1].x - 10, ppList[3][5][i - 1].y - 5);
		}
		ppList[3][5][10] = sf::Vector2i(32, -78);
		for (int i = 11; i < 15; i++)
		{
			ppList[3][5][i] = sf::Vector2i(ppList[3][5][i - 1].x - 10, ppList[3][5][i - 1].y - 5);
		}
		ppList[3][5][15] = sf::Vector2i(46, -94);
		for (int i = 16; i < 20; i++)
		{
			ppList[3][5][i] = sf::Vector2i(ppList[3][5][i - 1].x - 10, ppList[3][5][i - 1].y - 5);
		}
		ppList[3][5][20] = sf::Vector2i(60, -110);
		for (int i = 21; i < 25; i++)
		{
			ppList[3][5][i] = sf::Vector2i(ppList[3][5][i - 1].x - 10, ppList[3][5][i - 1].y - 5);
		}

		ppList[3][4][0] = sf::Vector2i(70, -14);
		for (int i = 1; i < 5; i++)
		{
			ppList[3][4][i] = sf::Vector2i(ppList[3][4][i - 1].x - 10, ppList[3][4][i - 1].y - 5);
		}
		ppList[3][4][5] = sf::Vector2i(83, -29);
		for (int i = 6; i < 10; i++)
		{
			ppList[3][4][i] = sf::Vector2i(ppList[3][4][i - 1].x - 10, ppList[3][4][i - 1].y - 5);
		}
		ppList[3][4][10] = sf::Vector2i(97, -44);
		for (int i = 11; i < 15; i++)
		{
			ppList[3][4][i] = sf::Vector2i(ppList[3][4][i - 1].x - 10, ppList[3][4][i - 1].y - 5);
		}
		ppList[3][4][15] = sf::Vector2i(110, -62);
		for (int i = 16; i < 20; i++)
		{
			ppList[3][4][i] = sf::Vector2i(ppList[3][4][i - 1].x - 10, ppList[3][4][i - 1].y - 5);
		}
		ppList[3][4][20] = sf::Vector2i(124, -77);
		for (int i = 21; i < 25; i++)
		{
			ppList[3][4][i] = sf::Vector2i(ppList[3][4][i - 1].x - 10, ppList[3][4][i - 1].y - 5);
		}

		prodsPerLayer = 25;
		prodsPerSlot = 25;

		switch (quality)
		{
		case StoreObject::Quality::Normal:
			apList.push_back(sf::Vector2i(3, 0));
			apList.push_back(sf::Vector2i(3, -1));
			apList.push_back(sf::Vector2i(3, -2));
			apList.push_back(sf::Vector2i(3, -3));
			apList.push_back(sf::Vector2i(-1, 0));
			apList.push_back(sf::Vector2i(-1, -1));
			apList.push_back(sf::Vector2i(-1, -2));
			apList.push_back(sf::Vector2i(-1, -3));

			this->SelectedObject = new StoreObject(type, quality, 2, -3, apList, wpList, 8, ppList, prodsPerLayer, prodsPerSlot);
			this->SelectedObject->SpriteName = spriteName;
			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName + "_Front");
			}
			else
			{
				this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName);
				this->SelectedObject->Origins = texHandler->GetOrigins(this->SelectedObject->SpriteName);
			}
			break;
		}
		break;
	}
}


void Builder::placeCurrentObject(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, unsigned int* highestID, std::deque<unsigned int>* freeIDs, TextureHandler* texHandler, GameData* gameData)
{
	float lowX;
	float lowY;
	float lowestX;
	float lowestY;
	int difX;
	int difY;
	int dif;
	int posX;
	int posY;
	switch (this->BuildMode)
	{
	case Builder::Mode::SingleObject:
		this->SelectedObject->setColor(sf::Color::White);
		if (freeIDs->size() > 0)
		{
			this->SelectedObject->ID = freeIDs->back();
			freeIDs->pop_back();
		}
		else
		{
			this->SelectedObject->ID = ++(*highestID);
		}

		lowX = this->SelectedObject->GridPosition.x + (this->SelectedObject->Width < 0) * this->SelectedObject->Width;
		lowY = this->SelectedObject->GridPosition.y + (this->SelectedObject->Height < 0) * this->SelectedObject->Height;
		lowestX = (lowX - lowY) * Constants::GridSize;
		lowestY = (lowX + lowY) * Constants::GridSize / 2;
		this->SelectedObject->LowestGridPosition = sf::Vector2f(lowX, lowY);
		this->SelectedObject->LowestScreenPosition = sf::Vector2f(lowestX, lowestY);
		

		if (this->SelectedObject->ObjectType == StoreObject::Type::Shelf)
		{
			this->SelectedObject->DirtSprites.resize(3);
			if (this->SelectedObject->ObjectQuality == StoreObject::Quality::Illegal)
			{
				this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Shelf_Illegal_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Shelf_Illegal_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Shelf_Illegal_Dirt2")[0][0];
			}
			else if (this->SelectedObject->ObjectQuality == StoreObject::Quality::Cheap)
			{
				this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Shelf_Cheap_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Shelf_Cheap_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Shelf_Cheap_Dirt2")[0][0];
			}
			else if (this->SelectedObject->ObjectQuality == StoreObject::Quality::Normal)
			{
				this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Shelf_Normal_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Shelf_Normal_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Shelf_Normal_Dirt2")[0][0];
			}
			else if (this->SelectedObject->ObjectQuality == StoreObject::Quality::Premium)
			{
				this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Shelf_Premium_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Shelf_Premium_Dirt1")[0][0];
				this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Shelf_Premium_Dirt2")[0][0];
			}

			if (this->SelectedObject->Rotation % 2)
			{
				for (int i = 0; i < 3; i++)
				{
					this->SelectedObject->DirtSprites[i]->scale(-1.0f, 1.0f);
				}
			}
		}
		this->SelectedObject->Grid = grid;
		this->SelectedObject->Feed = this->Feed;

		for (int i = 0; i <= abs(this->SelectedObject->Width); i++)
		{
			for (int j = 0; j <= abs(this->SelectedObject->Height); j++)
			{
				int setPosX = CurrentPos.x + i * (1 + (this->SelectedObject->Width < 0) * -2);
				int setPosY = CurrentPos.y + j * (1 + (this->SelectedObject->Height < 0) * -2);

				if (this->SelectedObject->WorkPoints.size() == 0 ||
					(setPosX != CurrentPos.x + this->SelectedObject->WorkPoints[0].x ||
					setPosY != CurrentPos.y + this->SelectedObject->WorkPoints[0].y))
				{
					(*grid)[setPosX][setPosY].push_back(this->SelectedObject);
					(*costGrid)[setPosX][setPosY] = 0;
				}
			}
		}
		
		if (this->ExtraObject)
		{
			this->ExtraObject->setColor(sf::Color::White);
			if (freeIDs->size() > 0)
			{
				this->ExtraObject->ID = freeIDs->back();
				freeIDs->pop_back();
			}
			else
			{
				this->ExtraObject->ID = ++(*highestID);
			}

			lowX -= 1.0f;
			lowY -= 1.0f;
			lowestX = (lowX - lowY) * Constants::GridSize;
			lowestY = (lowX + lowY) * Constants::GridSize / 2;
			this->ExtraObject->LowestGridPosition = sf::Vector2f(lowX, lowY);
			this->ExtraObject->LowestScreenPosition = sf::Vector2f(lowestX, lowestY);
			
			this->ExtraObject->Grid = grid;
			this->ExtraObject->Feed = this->Feed;
			
			for (int i = 0; i <= abs(this->ExtraObject->Width); i++)
			{
				for (int j = 0; j <= abs(this->ExtraObject->Height); j++)
				{
					int setPosX = CurrentPos.x + i * (1 + (this->ExtraObject->Width < 0) * -2);
					int setPosY = CurrentPos.y + j * (1 + (this->ExtraObject->Height < 0) * -2);

					if (this->ExtraObject->WorkPoints.size() == 0 ||
						(setPosX != CurrentPos.x + this->ExtraObject->WorkPoints[0].x ||
						setPosY != CurrentPos.y + this->ExtraObject->WorkPoints[0].y))
					{
						(*grid)[setPosX][setPosY].push_back(this->ExtraObject);
						(*costGrid)[setPosX][setPosY] = 0;
					}
				}
			}
		}

		gameData->CurrentMoney -= this->CurrentPrice;

		this->AdjustCostGrid(grid, costGrid);

		this->changeCurrentObject(this->SelectedObject->SpriteName, texHandler, false);

		while (this->SelectedObject->Rotation != this->Rotation)
		{
			this->SelectedObject->Rotation++;

			if (this->ExtraObject)
			{
				this->ExtraObject->Rotation++;
			}

			if (texHandler->Exists(this->SelectedObject->SpriteName + "_Front"))
			{
				this->SelectedObject->deleteSprites();
				if (this->SelectedObject->Rotation < 2)
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Front");
				}
				else
				{
					this->SelectedObject->Sprite = texHandler->GetSprite(this->SelectedObject->SpriteName + "_Back");
				}
				if (this->SelectedObject->Rotation % 2)
				{
					this->SelectedObject->scale(sf::Vector2f(-1, 1));
				}

				if (this->ExtraObject)
				{
					this->ExtraObject->deleteSprites();
					if (this->ExtraObject->Rotation < 2)
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Front");
					}
					else
					{
						this->ExtraObject->Sprite = texHandler->GetSprite(this->ExtraObject->SpriteName + "_Back");
					}
					if (this->ExtraObject->Rotation % 2)
					{
						this->ExtraObject->scale(sf::Vector2f(-1, 1));
					}
				}
			}
			else
			{
				this->SelectedObject->scale(sf::Vector2f(-1, 1));
				if (this->ExtraObject)
				{
					this->ExtraObject->scale(sf::Vector2f(-1, 1));
				}
			}

			this->SelectedObject->setOrigin(this->SelectedObject->Origins[this->Rotation]);
			short tmp = this->SelectedObject->Width;
			this->SelectedObject->Width = -this->SelectedObject->Height;
			this->SelectedObject->Height = tmp;
			
			if (this->ExtraObject)
			{
				this->ExtraObject->setOrigin(this->ExtraObject->Origins[this->Rotation]);
				short tmp = this->ExtraObject->Width;
				this->ExtraObject->Width = -this->ExtraObject->Height;
				this->ExtraObject->Height = tmp;
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					this->ExtraObject->Width *= -1;
				}
			}

			//adjust access- and workpoints
			std::vector<sf::Vector2i>::iterator iter;
			iter = this->SelectedObject->AccessPoints.begin();
			while (iter != this->SelectedObject->AccessPoints.end())
			{
				*iter = sf::Vector2i(-iter->y, iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					iter->x *= -1;
				}
				iter++;
			}

			iter = this->SelectedObject->WorkPoints.begin();
			while (iter != this->SelectedObject->WorkPoints.end())
			{
				*iter = sf::Vector2i(-iter->y, iter->x);
				if (this->SelectedObject->ObjectType == StoreObject::Type::Checkout && (this->SelectedObject->Rotation == 1 || this->SelectedObject->Rotation == 3))
				{
					iter->x *= -1;
				}
				iter++;
			}
		}
		break;



	case Builder::Mode::Floor:
		if (this->ValidPos)
		{
			difX = abs(this->StartPos.x - this->CurrentPos.x);
			difY = abs(this->StartPos.y - this->CurrentPos.y);
			for (int i = 0; i <= difX; i++)
			{
				for (int j = 0; j <= difY; j++)
				{
					posX = this->StartPos.x + i * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
					posY = this->StartPos.y + j * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

					std::list<Entity*>::iterator iter = (*grid)[posX][posY].begin();
					std::list<Entity*>::iterator enditer = (*grid)[posX][posY].end();

					bool sameFloor = false;

					while (iter != enditer)
					{
						if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Floor)
						{
							if ((*iter)->SpriteName == this->SelectedObject->SpriteName)
							{
								sameFloor = true;
								iter++;
							}
							else
							{
								this->changeCurrentPrice(dynamic_cast<StoreObject*>(*iter)->ObjectType, dynamic_cast<StoreObject*>(*iter)->ObjectQuality);
								gameData->CurrentMoney += this->CurrentPrice;
								delete *iter;
								iter = (*grid)[posX][posY].erase(iter);
							}
						}
						else
						{
							iter++;
						}
					}


					if (!sameFloor)
					{
						if (freeIDs->size() > 0)
						{
							this->SelectedObject->ID = freeIDs->back();
							freeIDs->pop_back();
						}
						else
						{
							this->SelectedObject->ID = ++(*highestID);
						}
						this->SelectedObject->GridPosition = sf::Vector2f((float)posX, (float)posY);
						this->SelectedObject->setColor(sf::Color::White);
						this->SelectedObject->update(sf::Time::Zero, 1, 0);
						this->SelectedObject->DirtSprites.resize(3);
						if (rand() % 2)
						{
							this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Dirt_Floor11")[0][0];
							this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Dirt_Floor12")[0][0];
							this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Dirt_Floor13")[0][0];
						}
						else
						{
							this->SelectedObject->DirtSprites[0] = texHandler->GetSprite("Dirt_Floor21")[0][0];
							this->SelectedObject->DirtSprites[1] = texHandler->GetSprite("Dirt_Floor22")[0][0];
							this->SelectedObject->DirtSprites[2] = texHandler->GetSprite("Dirt_Floor23")[0][0];
						}
						this->SelectedObject->Grid = grid;
						this->SelectedObject->Feed = this->Feed;
						(*grid)[posX][posY].push_front(this->SelectedObject);
						(*costGrid)[posX][posY] = 50;

						this->changeCurrentPrice(this->SelectedObject->ObjectType, this->SelectedObject->ObjectQuality);
						gameData->CurrentMoney -= this->CurrentPrice;

						this->changeCurrentObject(this->SelectedObject->SpriteName, texHandler, false);
					}
				}
			}

			this->AdjustCostGrid(grid, costGrid);
		}
		break;



	case Builder::Mode::Wall:
		if (this->ValidPos)
		{
			difX = abs(this->StartPos.x - this->CurrentPos.x);
			difY = abs(this->StartPos.y - this->CurrentPos.y);
			dif = difX * (difX > difY) + difY * (difY > difX);
			for (int i = 0; i <= dif; i++)
			{
				posX = this->StartPos.x + i * (difX > difY) * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
				posY = this->StartPos.y + i * (difY > difX) * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

				std::list<Entity*>::iterator iter = (*grid)[posX][posY].begin();
				std::list<Entity*>::iterator enditer = (*grid)[posX][posY].end();

				bool occupied = false;

				while (iter != enditer)
				{
					if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType != StoreObject::Type::Floor)
					{
						occupied = true;
					}

					iter++;
				}


				if (!occupied)
				{
					if (freeIDs->size() > 0)
					{
						this->SelectedObject->ID = freeIDs->back();
						freeIDs->pop_back();
					}
					else
					{
						this->SelectedObject->ID = ++(*highestID);
					}

					this->SelectedObject->GridPosition = sf::Vector2f((float)posX, (float)posY);

					lowX = this->SelectedObject->GridPosition.x + (this->SelectedObject->Width < 0) * this->SelectedObject->Width - 0.5f;
					lowY = this->SelectedObject->GridPosition.y + (this->SelectedObject->Height < 0) * this->SelectedObject->Height - 0.5f;
					lowestX = (lowX - lowY) * Constants::GridSize;
					lowestY = (lowX + lowY) * Constants::GridSize / 2;
					this->SelectedObject->LowestGridPosition = sf::Vector2f(lowX, lowY);
					this->SelectedObject->LowestScreenPosition = sf::Vector2f(lowestX, lowestY);

					this->SelectedObject->setColor(sf::Color::White);
					this->SelectedObject->update(sf::Time::Zero, 1, 0);
					this->SelectedObject->Grid = grid;
					this->SelectedObject->Feed = this->Feed;
					(*grid)[posX][posY].push_back(this->SelectedObject);
					(*costGrid)[posX][posY] = 0;

					gameData->CurrentMoney -= this->CurrentPrice;

					this->changeCurrentObject(this->SelectedObject->SpriteName, texHandler, false);

					this->adjustWall(sf::Vector2i(posX, posY), grid, texHandler);
					this->adjustWall(sf::Vector2i(posX, posY - 1), grid, texHandler);
					this->adjustWall(sf::Vector2i(posX, posY + 1), grid, texHandler);
					this->adjustWall(sf::Vector2i(posX - 1, posY), grid, texHandler);
					this->adjustWall(sf::Vector2i(posX + 1, posY), grid, texHandler);

					this->hideWalls(sf::Vector2i(posX, posY), grid, texHandler);
				}
			}

			this->AdjustCostGrid(grid, costGrid);
		}
		break;



	case Builder::Mode::Remove:
		std::list<Entity*> deleteList;
		std::list<Entity*>::iterator iter;
		std::list<Entity*>::iterator end;
		difX = abs(this->StartPos.x - this->CurrentPos.x);
		difY = abs(this->StartPos.y - this->CurrentPos.y);
		for (int i = 0; i <= difX; i++)
		{
			for (int j = 0; j <= difY; j++)
			{
				posX = this->StartPos.x + i * (1 + (this->StartPos.x > this->CurrentPos.x) * -2);
				posY = this->StartPos.y + j * (1 + (this->StartPos.y > this->CurrentPos.y) * -2);

				if (posX >= 0 && posY >= 0 && posX < (int)grid->size() && posY < (int)(*grid)[0].size())
				{
					iter = (*grid)[posX][posY].begin();
					end = (*grid)[posX][posY].end();


					while (iter != end)
					{
						if (dynamic_cast<StoreObject*>(*iter))
						{
							bool deleted = false;

							std::list<Entity*>::iterator anotheriter = deleteList.begin();
							std::list<Entity*>::iterator anotherend = deleteList.end();

							while (anotheriter != anotherend)
							{
								if ((*anotheriter)->ID == (*iter)->ID)
								{
									deleted = true;
								}

								anotheriter++;
							}

							if (!deleted)
							{
								deleteList.push_back(*iter);
							}

							if (dynamic_cast<StoreObject*>(*iter)->Width != 0 || dynamic_cast<StoreObject*>(*iter)->Height != 0)
							{
								for (int i = 0; i <= abs(dynamic_cast<StoreObject*>(*iter)->Width); i++)
								{
									for (int j = 0; j <= abs(dynamic_cast<StoreObject*>(*iter)->Height); j++)
									{
										int checkPosX = (int)(*iter)->GridPosition.x + i * (1 + (dynamic_cast<StoreObject*>(*iter)->Width < 0) * -2);
										int checkPosY = (int)(*iter)->GridPosition.y + j * (1 + (dynamic_cast<StoreObject*>(*iter)->Height < 0) * -2);

										std::list<Entity*>::iterator yetanotheriter = (*grid)[checkPosX][checkPosY].begin();
										std::list<Entity*>::iterator yetanotherend = (*grid)[checkPosX][checkPosY].end();

										while (yetanotheriter != yetanotherend)
										{
											if (dynamic_cast<StoreObject*>(*yetanotheriter) && (*yetanotheriter)->ID == (*iter)->ID && ((checkPosX != posX || checkPosY != posY) || yetanotheriter != iter))
											{
												yetanotheriter = (*grid)[checkPosX][checkPosY].erase(yetanotheriter);
											}
											else
											{
												yetanotheriter++;
											}
										}
									}
								}
							}

							iter = (*grid)[posX][posY].erase(iter);
						}
						else
						{
							iter++;
						}
					}
				}

				(*costGrid)[i][j] = 0;
			}
		}

		iter = deleteList.begin();
		end = deleteList.end();

		while (iter != end)
		{
			if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall)
			{
				this->adjustWall(sf::Vector2i((int)(*iter)->GridPosition.x, (int)(*iter)->GridPosition.y - 1), grid, texHandler);
				this->adjustWall(sf::Vector2i((int)(*iter)->GridPosition.x, (int)(*iter)->GridPosition.y + 1), grid, texHandler);
				this->adjustWall(sf::Vector2i((int)(*iter)->GridPosition.x - 1, (int)(*iter)->GridPosition.y), grid, texHandler);
				this->adjustWall(sf::Vector2i((int)(*iter)->GridPosition.x + 1, (int)(*iter)->GridPosition.y), grid, texHandler);
			}

			this->changeCurrentPrice(dynamic_cast<StoreObject*>(*iter)->ObjectType, dynamic_cast<StoreObject*>(*iter)->ObjectQuality);
			gameData->CurrentMoney += this->CurrentPrice;
			

			delete *iter;

			iter++;
		}

		this->AdjustCostGrid(grid, costGrid);

		break;
	}
}


void Builder::adjustWall(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, TextureHandler* texHandler)
{
	if (pos.x >= 0 && pos.y >= 0 && pos.x < (int)grid->size() && pos.y < (int)(*grid)[0].size())
	{
		bool up = false;
		bool down = false;
		bool left = false;
		bool right = false;

		std::list<Entity*>::iterator iter = (*grid)[pos.x][pos.y].begin();
		std::list<Entity*>::iterator enditer = (*grid)[pos.x][pos.y].end();

		std::list<Entity*>::iterator anotheriter;
		std::list<Entity*>::iterator anotherenditer;

		while (iter != enditer)
		{
			if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall)
			{
				if (pos.y > 0)
				{
					anotheriter = (*grid)[pos.x][pos.y - 1].begin();
					anotherenditer = (*grid)[pos.x][pos.y - 1].end();

					while (anotheriter != anotherenditer)
					{
						if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*anotheriter)->ObjectType == StoreObject::Type::Wall)
						{
							up = true;
						}
						anotheriter++;
					}
				}

				if (pos.y + 1 < (int)(*grid)[0].size())
				{
					anotheriter = (*grid)[pos.x][pos.y + 1].begin();
					anotherenditer = (*grid)[pos.x][pos.y + 1].end();

					while (anotheriter != anotherenditer)
					{
						if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*anotheriter)->ObjectType == StoreObject::Type::Wall)
						{
							down = true;
						}
						anotheriter++;
					}
				}

				if (pos.x > 0)
				{
					anotheriter = (*grid)[pos.x - 1][pos.y].begin();
					anotherenditer = (*grid)[pos.x - 1][pos.y].end();

					while (anotheriter != anotherenditer)
					{
						if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*anotheriter)->ObjectType == StoreObject::Type::Wall)
						{
							left = true;
						}
						anotheriter++;
					}
				}

				if (pos.x + 1 < (int)grid->size())
				{
					anotheriter = (*grid)[pos.x + 1][pos.y].begin();
					anotherenditer = (*grid)[pos.x + 1][pos.y].end();

					while (anotheriter != anotherenditer)
					{
						if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*anotheriter)->ObjectType == StoreObject::Type::Wall)
						{
							right = true;
						}
						anotheriter++;
					}
				}



				std::string name = "Wall_";
				if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectQuality == StoreObject::Quality::Cheap)
				{
					name += "Cheap_";
				}
				else if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectQuality == StoreObject::Quality::Normal)
				{
					name += "Normal_";
				}
				else if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectQuality == StoreObject::Quality::Premium)
				{
					name += "Premium_";
				}

				if (up)
				{
					name += "Up_";
				}

				if (down)
				{
					name += "Down_";
				}

				if (left)
				{
					name += "Left_";
				}

				if (right)
				{
					name += "Right_";
				}

				name.erase(name.size() - 1);

				if ((*iter)->SpriteName.find("_Hidden") != std::string::npos)
				{
					name += "_Hidden";
				}

				(*iter)->deleteSprites();
				(*iter)->Sprite = texHandler->GetSprite(name);
				(*iter)->Origins = texHandler->GetOrigins(name);
				(*iter)->SpriteName = name;
			}


			iter++;
		}
	}
}


void Builder::hideWalls(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, TextureHandler* texHandler)
{
	int x = pos.x - 1;
	int y = pos.y - 1;

	std::list<Entity*>::iterator iter;
	std::list<Entity*>::iterator enditer;

	bool wallFound = false;

	while (x >= 0 && y >= 0 && !wallFound)
	{
		iter = (*grid)[x][y].begin();
		enditer = (*grid)[x][y].end();

		while (iter != enditer)
		{
			if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall)
			{
				wallFound = true;
			}

			iter++;
		}

		x--;
		y--;
	}

	if (wallFound)
	{
		iter = (*grid)[pos.x][pos.y].begin();
		enditer = (*grid)[pos.x][pos.y].end();

		while (iter != enditer)
		{
			if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall)
			{
				if ((*iter)->SpriteName.find("_Hidden") == std::string::npos)
				{
					(*iter)->deleteSprites();
					(*iter)->Sprite = texHandler->GetSprite((*iter)->SpriteName += "_Hidden");
				}
			}

			iter++;
		}
	}


	x = pos.x + 1;
	y = pos.y + 1;

	wallFound = false;

	while (x < (int)(*grid).size() && y < (int)(*grid)[0].size() && !wallFound)
	{
		iter = (*grid)[x][y].begin();
		enditer = (*grid)[x][y].end();

		while (iter != enditer)
		{
			if (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Wall)
			{
				wallFound = true;
				if ((*iter)->SpriteName.find("_Hidden") == std::string::npos)
				{
					(*iter)->deleteSprites();
					(*iter)->Sprite = texHandler->GetSprite((*iter)->SpriteName += "_Hidden");
				}
			}

			iter++;
		}

		x++;
		y++;
	}
}


void Builder::AdjustCostGrid(std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid)
{
	//sf::Vector2i doorPos(-1, -1);

	std::list<Entity*>::iterator iter;
	std::list<Entity*>::iterator end;

	for (unsigned int x = 0; x < grid->size(); x++)
	{
		for (unsigned int y = 0; y < (*grid)[0].size(); y++)
		{
			(*costGrid)[x][y] = 0;

			iter = (*grid)[x][y].begin();
			end = (*grid)[x][y].end();

			while (iter != end)
			{
				if (dynamic_cast<StoreObject*>(*iter) && (dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Floor || dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Door))
				{
					(*costGrid)[x][y] = 50;
					//doorPos = sf::Vector2i(x, y);
				}
				else if (dynamic_cast<StoreObject*>(*iter))
				{
					(*costGrid)[x][y] = 0;
					break;
				}

				iter++;
			}
		}
	}


	/*if (doorPos != sf::Vector2i(-1, -1))
	{
		std::list<sf::Vector2i> searched;
		this->AdjustAccessibility(doorPos, grid, costGrid, searched);
	}*/
}


void Builder::AdjustAccessibility(sf::Vector2i pos, std::vector<std::vector<std::list<Entity*>>>* grid, std::vector<std::vector<unsigned char>>* costGrid, std::list<sf::Vector2i> searched)
{
	if (std::find(searched.begin(), searched.end(), pos) == searched.end())
	{
		searched.push_back(pos);


		std::list<Entity*>::iterator iter = (*grid)[pos.x][pos.y].begin();
		std::list<Entity*>::iterator end = (*grid)[pos.x][pos.y].end();

		while (iter != end)
		{
			if ((dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Floor) || (dynamic_cast<StoreObject*>(*iter) && dynamic_cast<StoreObject*>(*iter)->ObjectType == StoreObject::Type::Door))
			{
				(*costGrid)[pos.x][pos.y] = 50;
			}
			else
			{
				(*costGrid)[pos.x][pos.y] = 0;
			}

			iter++;
		}

		if ((*costGrid)[pos.x][pos.y])
		{
			sf::Vector2i newPos = pos + sf::Vector2i(-1, 0);
			if (newPos.x > 0)
			{
				this->AdjustAccessibility(newPos, grid, costGrid, searched);
			}
			newPos = pos + sf::Vector2i(0, -1);
			if (newPos.y > 0)
			{
				this->AdjustAccessibility(newPos, grid, costGrid, searched);
			}
			newPos = pos + sf::Vector2i(1, 0);
			if (newPos.x < (int)grid->size())
			{
				this->AdjustAccessibility(newPos, grid, costGrid, searched);
			}
			newPos = pos + sf::Vector2i(0, 1);
			if (newPos.y < (int)(*grid)[0].size())
			{
				this->AdjustAccessibility(newPos, grid, costGrid, searched);
			}
		}
	}
}