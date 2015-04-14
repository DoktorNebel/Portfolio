#include "Storage.h"


Storage::Storage(void)
{
}


Storage::~Storage(void)
{
}


void Storage::update(GameData* gameData)
{
	std::list<Product>::iterator iter = this->StorageItems.begin();
	std::list<Product>::iterator end = this->StorageItems.end();

	while (iter != end)
	{
		iter->update(gameData, this->Feed, 0);

		iter++;
	}
}


int Storage::countProduct(std::string name)
{
	std::list<Product>::iterator iter = this->StorageItems.begin();
	std::list<Product>::iterator end = this->StorageItems.end();

	int result = 0;

	while (iter != end)
	{
		if (iter->Name == name)
		{
			result += iter->Amount;
		}

		iter++;
	}

	return result;
}


void Storage::addProducts(std::list<Product>* productList)
{
	std::list<Product>::iterator iter = productList->begin();
	std::list<Product>::iterator end = productList->end();

	while (iter != end)
	{
		this->StorageItems.push_back(Product(*iter));

		iter++;
	}
}


void Storage::deleteProducts(std::string name, int amount)
{
	std::list<Product>::iterator iter = this->StorageItems.begin();
	std::list<Product>::iterator end = this->StorageItems.end();


	while (iter != end)
	{
		if (iter->Name == name)
		{
			iter->Amount -= amount;
			if (iter->Amount > 0)
			{
				break;
			}
			else if (iter->Amount == 0)
			{
				this->StorageItems.erase(iter);
				break;
			}
			else
			{
				amount = abs(iter->Amount);
				iter = this->StorageItems.erase(iter);
			}
		}
		else
		{
			iter++;
		}
	}
}


Product* Storage::addMeat(GameData* gameData)
{
	int j = rand() % 100;
	while (j != 0)
	{
		for (int i = 0; i < this->ProductList->size(); i++)
		{
			if ((*this->ProductList)[i]->Subcategory == "Fleisch" || (*this->ProductList)[i]->Subcategory == "Wurst")
			{
				if (j == 0)
				{
					this->StorageItems.push_back(Product((*this->ProductList)[i], 10 + rand() % 5 - 2, gameData, this->TexHandler));
					return new Product(this->StorageItems.back());
				}

				j--;
			}
		}
	}

	return 0;
}


void Storage::dispose()
{
	std::list<Product>::iterator iter = this->StorageItems.begin();
	std::list<Product>::iterator end = this->StorageItems.end();


	while (iter != end)
	{
		if (iter->ExpirationState == Product::State::Expired || iter->ExpirationState == Product::State::Rotten)
		{
			iter = this->StorageItems.erase(iter);
		}
		else
		{
			iter++;
		}
	}
}