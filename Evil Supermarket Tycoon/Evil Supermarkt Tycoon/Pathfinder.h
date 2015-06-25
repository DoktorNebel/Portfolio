#pragma once

#include <vector>
#include <list>
#include <deque>
#include <SFML/Graphics.hpp>

class Pathfinder
{
private:

	struct Node
	{
		sf::Vector2i Position;
		unsigned short MovementCost;
		unsigned short Heuristic;
		unsigned short OverallCost;
		Node* Parent;
	};


public:

	struct Request
	{
		unsigned int ID;
		sf::Vector2i StartPoint;
		sf::Vector2i EndPoint;
	};

	std::deque<Request> Requests;

	struct Path
	{
		unsigned int ID;
		std::list<sf::Vector2i> WayPoints;
	};

	std::list<Path> Paths;

public:

	Pathfinder(void);
	~Pathfinder(void);

	void Update(std::vector<std::vector<unsigned char>>* costGrid);

	void RequestPath(sf::Vector2i startPoint, sf::Vector2i endPoint, unsigned int id);

	std::list<sf::Vector2i> GetPath(unsigned int id);
};

