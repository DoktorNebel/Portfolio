#include "WorkersWindow.h"


WorkersWindow::WorkersWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler, std::vector<Worker*>* workers)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->Sprite->scale(1, 2);
	this->IsOpen = false;
	this->texHandler = texHandler;
	this->SelectedWorker = 0;
	this->Workers = workers;
	this->Buttons.push_back(new InterfaceButton("Close", InterfaceButton::Type::Normal, this->texHandler->GetSprite("CloseButton")[0][0], position, sf::Vector2f(950, 30)));
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("CloseButton_Pressed")[0][0];
	this->Buttons.push_back(new InterfaceButton("W_0", InterfaceButton::Type::Normal, this->texHandler->GetSprite("Portrait_Empty")[0][0], position, sf::Vector2f(50, 50)));
}


WorkersWindow::~WorkersWindow(void)
{
}


void WorkersWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		(*iter)->update(input);

		if ((*iter)->JustPressed)
		{
			if (*iter == this->Buttons.back())
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::OpenWindow;
				tmp.Text = "Hiring";
				messages->push_back(tmp);
			}
			else if ((*iter)->Name == "Close")
			{
				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::CloseWindow;
				tmp.Text = "Workers";
				messages->push_back(tmp);
			}
			else if ((*iter)->Name != "Bar")
			{
				std::string numString = (*iter)->Name;
				numString.erase(0, 2);
				int num = atoi(numString.c_str());
				this->SelectedWorker = (*this->Workers)[num];

				InterfaceMessage tmp;
				tmp.MessageType = InterfaceMessage::Type::SetSelectedEntity;
				tmp.Text = "Interface";
				messages->push_back(tmp);
			}
		}

		iter++;
	}
}


void WorkersWindow::draw(sf::RenderTarget& target, sf::RenderStates states)
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


void WorkersWindow::AddWorker(Worker* worker)
{
	this->Workers->push_back(worker);
	delete this->Buttons.back()->Sprite;
	this->Buttons.back()->Sprite = this->texHandler->GetSprite(worker->SpriteName + "_Portrait")[0][0];
	this->Buttons.back()->Sprite->setPosition(this->Sprite->getPosition());
	this->Buttons.back()->Sprite->move(50, 50 + (this->Workers->size() - 1) * 100);
	this->Buttons.back()->Text.setFont(*this->texHandler->GetFont());
	this->Buttons.back()->Text.setString(worker->Name);
	this->Buttons.back()->Text.setPosition(this->Buttons.back()->Sprite->getPosition() + sf::Vector2f(100, 30));

	this->Buttons.push_back(new Bar("Bar", L"Putzen", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), this->Buttons.back()->Offset + sf::Vector2f(400, 0)));
	dynamic_cast<Bar*>(this->Buttons.back())->SetValue(this->Workers->back()->CleaningEfficiency);

	this->Buttons.push_back(new Bar("Bar", L"Lager", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), this->Buttons.back()->Offset + sf::Vector2f(0, 25)));
	dynamic_cast<Bar*>(this->Buttons.back())->SetValue(this->Workers->back()->RefillingEfficiency);

	this->Buttons.push_back(new Bar("Bar", L"Kasse", this->texHandler->GetSprite("InterfaceBar")[0][0], this->texHandler->GetSprite("InterfaceBarFill")[0][0], this->texHandler, this->Sprite->getPosition(), this->Buttons.back()->Offset + sf::Vector2f(0, 25)));
	dynamic_cast<Bar*>(this->Buttons.back())->SetValue(this->Workers->back()->CashieringEfficiency);

	std::stringstream sstream;
	sstream << "W_" << this->Workers->size();
	this->Buttons.push_back(new InterfaceButton(sstream.str(), InterfaceButton::Type::Normal, this->texHandler->GetSprite("Portrait_Empty")[0][0], this->Sprite->getPosition(), sf::Vector2f(50, 50 + 100 * this->Workers->size())));
}