#pragma once
#include <string>
#include <SFML/System/Vector2.hpp>

struct Message
{
	enum Type
	{
		CheckPosition,
		RequestAccessPosition,
		RequestWorkPosition,
		SendPosition,
		BuyProduct,
		PlaceProduct,
		TakeFromStorage,
		PutInStorage,
		KnockOver,
		CutLine,
		Pay,
		Steal
	} MessageType;

	std::string ProductName;
	sf::Vector2i Position;

	unsigned int ID;
};