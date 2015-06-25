#pragma once

#include <SFML/Graphics.hpp>
#include <vector>
#include "Constants.h"

class Input
{
public:

	enum Function
	{
		ScrollLeft,
		ScrollRight,
		ScrollUp,
		ScrollDown,
		RotateLeft,
		RotateRight,
		Pause
	};

private:

	sf::Vector2f MouseCoords;
	sf::Vector2i MousePos;
	sf::Vector2i GridPos;
	bool LIsClicked;
	bool LJustClicked;
	bool LWasClicked;
	bool MIsClicked;
	bool MJustClicked;
	bool MWasClicked;
	bool RIsClicked;
	bool RJustClicked;
	bool RWasClicked;
	std::string Character;

	struct Key
	{
		bool HasPrimary;
		bool HasSecondary;
		sf::Keyboard::Key Primary;
		sf::Keyboard::Key Secondary;
		bool IsPressed;
		bool JustPressed;
		bool WasPressed;
	};

	std::vector<Input::Key> Keys;

public:
	Input(void);
	~Input(void);

	void Update(sf::RenderWindow* window);
	bool IsPressed(Input::Function function);
	bool JustPressed(Input::Function function);
	bool WasPressed(Input::Function function);
	bool IsClicked(sf::Mouse::Button mouseButton);
	bool JustClicked(sf::Mouse::Button mouseButton);
	bool WasClicked(sf::Mouse::Button mouseButton);
	sf::Vector2f GetMouseCoords();
	sf::Vector2i GetMousePos();
	sf::Vector2i GetGridPos();
	void RebindKey(Input::Function function, bool primary, sf::Keyboard::Key key);
	void UnbindKey(Input::Function function, bool primary);
	void UpdateText();
	void UpdateText(unsigned int text);
	std::string GetText();
};

