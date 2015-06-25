#include "Pathfinder.h"


Pathfinder::Pathfinder(void)
{
}


Pathfinder::~Pathfinder(void)
{
}


void Pathfinder::Update(std::vector<std::vector<unsigned char>>* costGrid)
{
	if (!this->Requests.empty())
	{
		sf::Vector2i startPoint = this->Requests.front().StartPoint;
		sf::Vector2i endPoint = this->Requests.front().EndPoint;

		std::list<Node*> openList;
		std::list<Node*> closedList;

		bool found = false;

		Node* node = new Node;
		node->Position = startPoint;
		node->MovementCost = 0;
		node->Heuristic = 0;
		node->OverallCost = 0;
		node->Parent = 0;
		openList.push_back(node);

		std::list<Node*>::iterator iter;
		std::list<Node*>::iterator end;

		Node* nextNode;

		while (!found && !openList.empty())
		{
			iter = openList.begin();
			end = openList.end();

			nextNode = openList.front();

			while (iter != end)
			{
				if ((*iter)->OverallCost < nextNode->OverallCost)
				{
					nextNode = *iter;
				}

				iter++;
			}

			if (nextNode->Position != endPoint)
			{
				openList.remove(nextNode);

				sf::Vector2i pos(nextNode->Position.x - 1, nextNode->Position.y);
				if (pos.x > 0 && pos.x < (int)costGrid->size() && pos.y > 0 && pos.y < (int)(*costGrid)[0].size() && (*costGrid)[pos.x][pos.y])
				{
					iter = closedList.begin();
					end = closedList.end();

					bool alreadyFound = false;
					bool inOpenList = false;

					while (iter != end)
					{
						if ((*iter)->Position == pos)
						{
							alreadyFound = true;
							break;
						}

						iter++;
					}

					if (!alreadyFound)
					{
						node = new Node;
						node->Position = pos;
						node->MovementCost = nextNode->MovementCost + (*costGrid)[pos.x][pos.y];
						node->Heuristic = abs(endPoint.x - pos.x) + abs(endPoint.y - pos.y);
						node->OverallCost = node->MovementCost + node->Heuristic;
						node->Parent = nextNode;

						iter = openList.begin();
						end = openList.end();

						while (iter != end)
						{
							if ((*iter)->Position == node->Position)
							{
								inOpenList = true;
								if (node->OverallCost < (*iter)->OverallCost)
								{
									openList.erase(iter);
									openList.push_back(node);
								}
								break;
							}

							iter++;
						}

						if (!inOpenList)
						{
							openList.push_back(node);
						}
					}
				}

				pos = sf::Vector2i(nextNode->Position.x, nextNode->Position.y - 1);
				if (pos.x > 0 && pos.x < (int)costGrid->size() && pos.y > 0 && pos.y < (int)(*costGrid)[0].size() && (*costGrid)[pos.x][pos.y])
				{
					iter = closedList.begin();
					end = closedList.end();

					bool alreadyFound = false;
					bool inOpenList = false;

					while (iter != end)
					{
						if ((*iter)->Position == pos)
						{
							alreadyFound = true;
							break;
						}

						iter++;
					}

					if (!alreadyFound)
					{
						node = new Node;
						node->Position = pos;
						node->MovementCost = nextNode->MovementCost + (*costGrid)[pos.x][pos.y];
						node->Heuristic = abs(endPoint.x - pos.x) + abs(endPoint.y - pos.y);
						node->OverallCost = node->MovementCost + node->Heuristic;
						node->Parent = nextNode;

						iter = openList.begin();
						end = openList.end();

						while (iter != end)
						{
							if ((*iter)->Position == node->Position)
							{
								inOpenList = true;
								if (node->OverallCost < (*iter)->OverallCost)
								{
									openList.erase(iter);
									openList.push_back(node);
								}
								break;
							}

							iter++;
						}

						if (!inOpenList)
						{
							openList.push_back(node);
						}
					}
				}

				pos = sf::Vector2i(nextNode->Position.x + 1, nextNode->Position.y);
				if (pos.x > 0 && pos.x < (int)costGrid->size() && pos.y > 0 && pos.y < (int)(*costGrid)[0].size() && (*costGrid)[pos.x][pos.y])
				{
					iter = closedList.begin();
					end = closedList.end();

					bool alreadyFound = false;
					bool inOpenList = false;

					while (iter != end)
					{
						if ((*iter)->Position == pos)
						{
							alreadyFound = true;
							break;
						}

						iter++;
					}

					if (!alreadyFound)
					{
						node = new Node;
						node->Position = pos;
						node->MovementCost = nextNode->MovementCost + (*costGrid)[pos.x][pos.y];
						node->Heuristic = abs(endPoint.x - pos.x) + abs(endPoint.y - pos.y);
						node->OverallCost = node->MovementCost + node->Heuristic;
						node->Parent = nextNode;

						iter = openList.begin();
						end = openList.end();

						while (iter != end)
						{
							if ((*iter)->Position == node->Position)
							{
								inOpenList = true;
								if (node->OverallCost < (*iter)->OverallCost)
								{
									openList.erase(iter);
									openList.push_back(node);
								}
								break;
							}

							iter++;
						}

						if (!inOpenList)
						{
							openList.push_back(node);
						}
					}
				}

				pos = sf::Vector2i(nextNode->Position.x, nextNode->Position.y + 1);
				if (pos.x > 0 && pos.x < (int)costGrid->size() && pos.y > 0 && pos.y < (int)(*costGrid)[0].size() && (*costGrid)[pos.x][pos.y])
				{
					iter = closedList.begin();
					end = closedList.end();

					bool alreadyFound = false;
					bool inOpenList = false;

					while (iter != end)
					{
						if ((*iter)->Position == pos)
						{
							alreadyFound = true;
							break;
						}

						iter++;
					}

					if (!alreadyFound)
					{
						node = new Node;
						node->Position = pos;
						node->MovementCost = nextNode->MovementCost + (*costGrid)[pos.x][pos.y];
						node->Heuristic = abs(endPoint.x - pos.x) + abs(endPoint.y - pos.y);
						node->OverallCost = node->MovementCost + node->Heuristic;
						node->Parent = nextNode;

						iter = openList.begin();
						end = openList.end();

						while (iter != end)
						{
							if ((*iter)->Position == node->Position)
							{
								inOpenList = true;
								if (node->OverallCost < (*iter)->OverallCost)
								{
									openList.erase(iter);
									openList.push_back(node);
								}
								break;
							}

							iter++;
						}

						if (!inOpenList)
						{
							openList.push_back(node);
						}
					}
				}


				closedList.push_back(nextNode);
			}
			else
			{
				found = true;
			}
		}


		std::list<sf::Vector2i> result;

		while (nextNode != 0)
		{
			result.push_front(nextNode->Position);
			nextNode = nextNode->Parent;
		}

		while (!openList.empty())
		{
			delete openList.front();
			openList.pop_front();
		}

		while (!closedList.empty())
		{
			delete closedList.front();
			closedList.pop_front();
		}

		Pathfinder::Path tmp;
		tmp.ID = this->Requests.front().ID;
		tmp.WayPoints = result;
		this->Paths.push_back(tmp);

		this->Requests.pop_front();
	}
}


void Pathfinder::RequestPath(sf::Vector2i startPoint, sf::Vector2i endPoint, unsigned int id)
{
	Pathfinder::Request tmp;
	tmp.StartPoint = startPoint;
	tmp.EndPoint = endPoint;
	tmp.ID = id;
	this->Requests.push_back(tmp);
}


std::list<sf::Vector2i> Pathfinder::GetPath(unsigned int id)
{
	std::list<sf::Vector2i> result;

	std::list<Pathfinder::Path>::iterator iter = this->Paths.begin();
	std::list<Pathfinder::Path>::iterator end = this->Paths.end();

	while (iter != end)
	{
		if (iter->ID == id)
		{
			result = iter->WayPoints;
			this->Paths.erase(iter);
			break;
		}

		iter++;
	}

	return result;
}