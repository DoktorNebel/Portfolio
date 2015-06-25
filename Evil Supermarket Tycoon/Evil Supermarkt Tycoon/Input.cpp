#include "Input.h"


Input::Input(void)
{
	this->Keys.resize(7);

	this->Keys[Input::ScrollLeft].Primary = sf::Keyboard::A;

	this->Keys[Input::ScrollRight].Primary = sf::Keyboard::D;

	this->Keys[Input::ScrollUp].Primary = sf::Keyboard::W;

	this->Keys[Input::ScrollDown].Primary = sf::Keyboard::S;

	this->Keys[Input::RotateLeft].Primary = sf::Keyboard::Q;

	this->Keys[Input::RotateRight].Primary = sf::Keyboard::E;

	this->Keys[Input::Pause].Primary = sf::Keyboard::Escape;

	for (unsigned int i = 0; i < this->Keys.size(); i++)
	{
		this->Keys[i].IsPressed = false;
		this->Keys[i].JustPressed = false;
		this->Keys[i].WasPressed = false;
		this->Keys[i].HasPrimary = true;
		this->Keys[i].HasSecondary = false;
	}

	this->LIsClicked = false;
	this->LJustClicked = false;
	this->LWasClicked = false;
	this->MIsClicked = false;
	this->MJustClicked = false;
	this->MWasClicked = false;
	this->RIsClicked = false;
	this->RJustClicked = false;
	this->RWasClicked = false;
}


Input::~Input(void)
{
}


void Input::Update(sf::RenderWindow* window)
{
	this->MousePos = sf::Mouse::getPosition(*window);
	this->MouseCoords = window->mapPixelToCoords(this->MousePos);
	int gridPosX = ((2 * (int)this->MouseCoords.y + (int)this->MouseCoords.x) / 2) / Constants::GridSize;
	int gridPosY = ((2 * (int)this->MouseCoords.y - (int)this->MouseCoords.x) / 2) / Constants::GridSize;
	this->GridPos = sf::Vector2i(gridPosX, gridPosY);

	//Check Left Mouse Button
	if (sf::Mouse::isButtonPressed(sf::Mouse::Left))
	{
		if (this->LIsClicked)
		{
			this->LJustClicked = false;
		}
		else
		{
			this->LJustClicked = true;
		}
		this->LIsClicked = true;
	}
	else
	{
		if (this->LIsClicked)
		{
			this->LIsClicked = false;
			this->LJustClicked = false;
			this->LWasClicked = true;
		}
		else
		{
			this->LWasClicked = false;
		}
	}

	//Check Middle Mouse Button
	if (sf::Mouse::isButtonPressed(sf::Mouse::Middle))
	{
		if (this->MIsClicked)
		{
			this->MJustClicked = false;
		}
		else
		{
			this->MJustClicked = true;
		}
		this->MIsClicked = true;
	}
	else
	{
		if (this->MIsClicked)
		{
			this->MIsClicked = false;
			this->MJustClicked = false;
			this->MWasClicked = true;
		}
		else
		{
			this->MWasClicked = false;
		}
	}

	//Check Right Mouse Button
	if (sf::Mouse::isButtonPressed(sf::Mouse::Right))
	{
		if (this->RIsClicked)
		{
			this->RJustClicked = false;
		}
		else
		{
			this->RJustClicked = true;
		}
		this->RIsClicked = true;
	}
	else
	{
		if (this->RIsClicked)
		{
			this->RIsClicked = false;
			this->RJustClicked = false;
			this->RWasClicked = true;
		}
		else
		{
			this->RWasClicked = false;
		}
	}


	//Check Keyboard input
	for (unsigned int i = 0; i < this->Keys.size(); i++)
	{
		Input::Key* CurrentKey = &this->Keys[i];

		if (CurrentKey->HasPrimary)
		{
			if(sf::Keyboard::isKeyPressed(CurrentKey->Primary))
			{
				if (CurrentKey->IsPressed)
				{
					CurrentKey->JustPressed = false;
				}
				else
				{
					CurrentKey->JustPressed = true;
				}
				CurrentKey->IsPressed = true;
			}
			else
			{
				if (CurrentKey->IsPressed)
				{
					CurrentKey->IsPressed = false;
					CurrentKey->JustPressed = false;
					CurrentKey->WasPressed = true;
				}
				else
				{
					CurrentKey->WasPressed = false;
				}
			}
		}


		if (CurrentKey->HasSecondary)
		{
			if(sf::Keyboard::isKeyPressed(CurrentKey->Secondary))
			{
				if (CurrentKey->IsPressed)
				{
					CurrentKey->JustPressed = false;
				}
				else
				{
					CurrentKey->JustPressed = true;
				}
				CurrentKey->IsPressed = true;
			}
			else
			{
				if (CurrentKey->IsPressed)
				{
					CurrentKey->IsPressed = false;
					CurrentKey->JustPressed = false;
					CurrentKey->WasPressed = true;
				}
				else
				{
					CurrentKey->WasPressed = false;
				}
			}
		}
	}
}


bool Input::IsPressed(Input::Function function)
{
	return this->Keys[function].IsPressed;
}


bool Input::JustPressed(Input::Function function)
{
	return this->Keys[function].JustPressed;
}


bool Input::WasPressed(Input::Function function)
{
	return this->Keys[function].WasPressed;
}


sf::Vector2f Input::GetMouseCoords()
{
	return this->MouseCoords;
}


sf::Vector2i Input::GetMousePos()
{
	return this->MousePos;
}


sf::Vector2i Input::GetGridPos()
{
	return this->GridPos;
}


bool Input::IsClicked(sf::Mouse::Button mouseButton)
{
	if (mouseButton == sf::Mouse::Left)
	{
		return this->LIsClicked;
	}
	else if (mouseButton == sf::Mouse::Middle)
	{
		return this->MIsClicked;
	}
	else if (mouseButton == sf::Mouse::Right)
	{
		return this->RIsClicked;
	}
	return false;
}


bool Input::JustClicked(sf::Mouse::Button mouseButton)
{
	if (mouseButton == sf::Mouse::Left)
	{
		return this->LJustClicked;
	}
	else if (mouseButton == sf::Mouse::Middle)
	{
		return this->MJustClicked;
	}
	else if (mouseButton == sf::Mouse::Right)
	{
		return this->RJustClicked;
	}
	return false;
}


bool Input::WasClicked(sf::Mouse::Button mouseButton)
{
	if (mouseButton == sf::Mouse::Left)
	{
		return this->LWasClicked;
	}
	else if (mouseButton == sf::Mouse::Middle)
	{
		return this->MWasClicked;
	}
	else if (mouseButton == sf::Mouse::Right)
	{
		return this->RWasClicked;
	}
	return false;
}


void Input::RebindKey(Input::Function function, bool primary, sf::Keyboard::Key key)
{
	if (primary)
	{
		this->Keys[function].HasPrimary = true;
		this->Keys[function].Primary = key;
	}
	else
	{
		this->Keys[function].HasSecondary = true;
		this->Keys[function].Secondary = key;
	}
}


void Input::UnbindKey(Input::Function function, bool primary)
{
	if (primary)
	{
		this->Keys[function].HasPrimary = false;
	}
	else
	{
		this->Keys[function].HasSecondary = false;
	}
}


void Input::UpdateText()
{
	this->Character.clear();
}


void Input::UpdateText(unsigned int text)
{
	if (text == 8)
	{
		this->Character = "delete";
	}
	else
	{
		this->Character = text;
	}
}


std::string Input::GetText()
{
	return this->Character;
}