#pragma once
#include <string>
#include <list>
#include "Product.h"
#include "Date.h"

class Storage
{
public:

	std::list<Product> StorageItems;
	std::vector<ProductListItem*>* ProductList;
	TextureHandler* TexHandler;
	std::list<FeedMessage>* Feed;

public:

	Storage(void);
	~Storage(void);

	void update(GameData* gameData);
	int countProduct(std::string name);
	void addProducts(std::list<Product>* productList);
	void deleteProducts(std::string name, int amount);
	Product* addMeat(GameData* gameData);
	void dispose();
};

