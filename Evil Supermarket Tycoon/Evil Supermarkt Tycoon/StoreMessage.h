#pragma once

#include <string>
#include <SFML/Graphics.hpp>
//#include "StoreObject.h"

class StoreObject;

struct StoreMessage
{
	enum Type
	{
		RequestProductPos,
		RequestCheckoutPos,
		RequestExitPos,
		SendPos
	} MessageType;

	unsigned int ID;
	StoreObject* Pointer;
	std::string Name;
	sf::Vector2i Position;
};