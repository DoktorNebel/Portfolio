#pragma once

#include "Customer.h"
#include "Worker.h"
#include "Animal.h"
#include "Pathfinder.h"
#include "TextureHandler.h"
#include "StoreJob.h"
#include "Storage.h"

class PeopleGenerator
{
private:

	std::vector<std::wstring> Classes;
	std::vector<std::vector<ProductListItem*>> Shoppinglists;
	std::vector<std::wstring> FirstNamesMale;
	std::vector<std::wstring> FirstNamesFemale;
	std::vector<std::wstring> Surnames;
	std::vector<ProductListItem*>* ProductList;
	Pathfinder* ThePathfinder;
	TextureHandler* TexHandler;

public:

	std::vector<float> ClassProbabilities;
	double AverageMoney;
	unsigned short AverageAge;
	float AverageStealingPotential;
	std::vector<std::vector<std::list<Entity*>>>* Grid;
	std::list<StoreMessage>* Messages;
	std::list<StoreJob>* Jobs;
	Storage* ProductStorage;
	std::list<FeedMessage>* Feed;

public:

	PeopleGenerator();
	PeopleGenerator(std::vector<ProductListItem*>* productList, Pathfinder* pathfinder, TextureHandler* texHandler, std::list<StoreJob>* jobs);
	~PeopleGenerator(void);

	Customer* GenerateCustomer();
	Customer* GenerateDrugGuy(std::string drug);
	Worker* GenerateWorker();
	Animal* GenerateAnimal();
	void ChangeClassProbability(std::wstring className, float value);
	void loadShoppingLists();
	ProductListItem* getItem(std::string productName, std::string quality);
	std::vector<ProductListItem*> getItems(std::string quality);
};

