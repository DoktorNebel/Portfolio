#include "HiringWindow.h"


HiringWindow::HiringWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, PeopleGenerator* peepGenerator)
{
	this->Name = name;
	this->texHandler = texHandler;
	this->PeepGenerator = peepGenerator;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->Sprite->scale(0.75, 1.5);
	this->IsOpen = false;
	this->CurrentButton = 0;
	this->Buttons.push_back(new InterfaceButton("Close", InterfaceButton::Type::Normal, texHandler->GetSprite("CloseButton")[0][0], position, sf::Vector2f(700, 30)));
	this->Buttons.push_back(new InterfaceButton("Hire", InterfaceButton::Type::Normal, texHandler->GetSprite("SimpleButton")[0][0], position, sf::Vector2f(680, 320)));
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
	this->Buttons.back()->Text.setPosition(position.x + 682, position.y + 323);
	this->Buttons.back()->Text.setString("Einstellen");
}


HiringWindow::~HiringWindow(void)
{
}


void HiringWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->update(input);

		if ((*iter)->JustPressed)
		{
			if (*iter != this->CurrentButton && (*iter)->Name != "Close" && (*iter)->Name != "Hire")
			{
				if (this->CurrentButton)
				{
					this->CurrentButton->Pressed = false;
				}
				this->CurrentButton = *iter;
			}

			if ((*iter)->Name == "Hire")
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::HireWorker;
				tmp.Text = "Workers";

				std::string numString = this->CurrentButton->Name;
				numString.erase(0, 2);
				tmp.Pointer = this->Workers[atoi(numString.c_str())];
				
				std::vector<Worker*>::iterator anotheriter = this->Workers.begin();

				anotheriter += atoi(numString.c_str());

				this->Workers.erase(anotheriter);

				std::list<InterfaceButton*>::iterator yetanotheriter = this->Buttons.begin();
				std::list<InterfaceButton*>::iterator yetanotherend = this->Buttons.end();

				while (yetanotheriter != yetanotherend)
				{
					if ((*yetanotheriter)->Name == this->CurrentButton->Name + "_Cleaning" ||
						(*yetanotheriter)->Name == this->CurrentButton->Name + "_Storage" ||
						(*yetanotheriter)->Name == this->CurrentButton->Name + "_Checkout")
					{
						delete *yetanotheriter;
						yetanotheriter = this->Buttons.erase(yetanotheriter);
					}
					else
					{
						yetanotheriter++;
					}
				}

				this->Buttons.remove(this->CurrentButton);
				delete this->CurrentButton;
				this->CurrentButton = 0;

				yetanotheriter = this->Buttons.begin();
				yetanotherend = this->Buttons.end();

				for (unsigned int j = 0; j < this->Buttons.size() - this->Workers.size(); j++)
				{
					yetanotheriter++;
				}

				int i = 0;

				while (yetanotheriter != yetanotherend)
				{
					std::stringstream sstream;
					sstream << "W_" << i;
					(*yetanotheriter)->Name = sstream.str();

					yetanotheriter++;
					i++;
				}


				yetanotheriter = this->Buttons.begin();
				yetanotherend = this->Buttons.end();

				for (unsigned int j = this->Buttons.size() - this->Workers.size(); j < this->Buttons.size() + 2; j++)
				{
					yetanotherend--;
				}

				i = this->Workers.size() - 1;

				while (yetanotheriter != yetanotherend)
				{
					std::stringstream sstream;
					sstream << "W_" << i;
					
					(*yetanotheriter)->Name.replace(0, 3, sstream.str());

					if ((*yetanotheriter)->Name.find("Cleaning") != std::string::npos)
					{
						i--;
					}

					yetanotheriter++;
				}

				messages->push_back(tmp);
			}

			if ((*iter)->Name == "Close")
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Hiring";
				messages->push_back(tmp);
			}
		}

		iter++;
	}

}


void HiringWindow::draw(sf::RenderTarget& target, sf::RenderStates states)
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		target.draw(**iter);

		iter++;
	}
}


void HiringWindow::GenerateWorkers()
{
	while (this->Workers.size() > 0)
	{
		delete this->Workers.back();
		this->Workers.pop_back();

		delete this->Buttons.back();
		this->Buttons.pop_back();

		delete this->Buttons.front();
		this->Buttons.pop_front();
		delete this->Buttons.front();
		this->Buttons.pop_front();
		delete this->Buttons.front();
		this->Buttons.pop_front();
	}

	std::stringstream sstream;
	std::string nameString;
	for (int i = 0; i < 3; i++)
	{
		sstream.str("");
		sstream << "W_" << this->Workers.size();
		nameString = sstream.str();
		this->Workers.push_back(this->PeepGenerator->GenerateWorker());
		this->Buttons.push_back(new InterfaceButton(nameString, InterfaceButton::Type::Permanent, texHandler->GetSprite(this->Workers.back()->SpriteName + "_Portrait")[0][0], this->Sprite->getPosition(), sf::Vector2f(50.0f, 50.0f + (float)i * 100.0f)));
		this->Buttons.back()->Text.setFont(*this->texHandler->GetFont());
		this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(70, 23));
		this->Buttons.back()->Text.setString(this->Workers.back()->Name);

		this->Buttons.push_front(new Bar(nameString + "_Cleaning", L"Putzen", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), sf::Vector2f(450.0f, 50.0f + (float)i * 100.0f)));
		dynamic_cast<Bar*>(this->Buttons.front())->SetValue(this->Workers.back()->CleaningEfficiency);

		this->Buttons.push_front(new Bar(nameString + "_Storage", L"Lager", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), sf::Vector2f(450.0f, 75.0f + (float)i * 100.0f)));
		dynamic_cast<Bar*>(this->Buttons.front())->SetValue(this->Workers.back()->RefillingEfficiency);

		this->Buttons.push_front(new Bar(nameString + "_Checkout", L"Kasse", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), sf::Vector2f(450.0f, 100.0f + (float)i * 100.0f)));
		dynamic_cast<Bar*>(this->Buttons.front())->SetValue(this->Workers.back()->CashieringEfficiency);
	}
}