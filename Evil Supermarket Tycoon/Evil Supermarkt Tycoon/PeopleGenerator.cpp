#include "PeopleGenerator.h"


PeopleGenerator::PeopleGenerator()
{

}


PeopleGenerator::PeopleGenerator(std::vector<ProductListItem*>* productList, Pathfinder* pathfinder, TextureHandler* texHandler, std::list<StoreJob>* jobs)
{
	this->AverageAge = 30;
	this->AverageMoney = 50.00;
	this->AverageStealingPotential = 0.01f;

	this->ThePathfinder = pathfinder;
	this->TexHandler = texHandler;
	this->Jobs = jobs;

	this->Classes.push_back(L"Hartz IV-ler");
	this->Classes.push_back(L"Punk");
	this->Classes.push_back(L"Nazi");
	this->Classes.push_back(L"Kind");
	this->Classes.push_back(L"Jugendlich");
	this->Classes.push_back(L"Rentner");
	this->Classes.push_back(L"Einfacher Angestellter");
	this->Classes.push_back(L"Leitender Angestellter");
	this->Classes.push_back(L"Reich");

	for (unsigned int i = 0; i < this->Classes.size(); i++)
	{
		this->ClassProbabilities.push_back(1.0f / (float)this->Classes.size());
	}
	
	this->ProductList = productList;

	this->loadShoppingLists();

	this->Shoppinglists.push_back(this->getItems("Illegal"));
	std::vector<ProductListItem*> products = this->getItems("Luxus");
	this->Shoppinglists.back().insert(this->Shoppinglists.back().end(), products.begin(), products.end());


	std::wifstream file("Content/Data/First Names Male.txt");
	std::wstring line;

	while (std::getline(file, line))
	{
		this->FirstNamesMale.push_back(line);
	}

	file.close();

	file.open("Content/Data/First Names Female.txt");

	while (std::getline(file, line))
	{
		this->FirstNamesFemale.push_back(line);
	}

	file.close();

	file.open("Content/Data/Surnames.txt");

	while (std::getline(file, line))
	{
		this->Surnames.push_back(line);
	}

	file.close();
}


PeopleGenerator::~PeopleGenerator(void)
{
}


Customer* PeopleGenerator::GenerateCustomer()
{
	Customer* result = new Customer();
	std::string spriteName = "Customer";
	int random = rand() % 1000 + 1;
	unsigned int i = 0;
	int prob = (int)(this->ClassProbabilities[0] * 1000.0f);
	while (random > prob && i < this->ClassProbabilities.size() - 1)
	{
		i++;
   		prob += (int)(this->ClassProbabilities[i] * 1000.0f);
	}
	result->Class = this->Classes[i];

	if (result->Class == L"Kind")
	{
		result->Age = 10 + rand() % 5 - 2;
	}
	else if (result->Class == L"Jugendlich")
	{
		result->Age = 16 + rand() % 6 - 3;
	}
	else
	{
		result->Age = AverageAge + (unsigned short)sqrt((float)(rand() % 101)) * (1 - (2 * (rand() % 2)));
		if (result->Age > 100)
		{
			result->Age = 100;
		}
		if (result->Age < 6)
		{
			result->Age = 6;
		}
	}

	result->Money = AverageMoney + sqrt((float)(rand() % (int)AverageMoney * 2.0)) * (1 - (2 * (rand() % 2)));
	result->StealingPotential = AverageStealingPotential + (float)(rand() % 201 - 100) / 10000.0f;
	result->Happiness = 1.0f;
	if (rand() % 2)
	{
		result->PersonGender = Person::Gender::Male;
		spriteName += "_Male";

		if (result->Class == L"Hartz IV-ler")
		{
			spriteName += "_Fatass";
		}
		if (result->Class == L"Punk")
		{
			spriteName += "_Tieguy";
		}
		if (result->Class == L"Nazi")
		{
			spriteName += "_Nazi";
		}
		if (result->Class == L"Kind")
		{
			spriteName += "_Child";
		}
		if (result->Class == L"Jugendlich")
		{
			spriteName += "_Child";
		}
		if (result->Class == L"Rentner")
		{
			if (rand() % 2)
			{
				spriteName += "_Grandpa";
			}
			else
			{
				spriteName += "_FatGrandpa";
			}
		}
		if (result->Class == L"Einfacher Angestellter")
		{
			if (rand() % 2)
			{
				spriteName += "_Tieguy";
			}
			else
			{
				spriteName += "_Teacher";
			}
		}
		if (result->Class == L"Leitender Angestellter")
		{
			spriteName += "_Business";
		}
		if (result->Class == L"Reich")
		{
			spriteName += "_Rich";
		}
	}
	else
	{
		result->PersonGender = Person::Gender::Female;
		spriteName += "_Female";

		if (result->Class == L"Hartz IV-ler")
		{
			spriteName += "_Blonde";
		}
		if (result->Class == L"Punk")
		{
			spriteName += "_Emo";
		}
		if (result->Class == L"Nazi")
		{
			spriteName += "_Army";
		}
		if (result->Class == L"Kind")
		{
			spriteName += "_Child";
		}
		if (result->Class == L"Jugendlich")
		{
			if (rand() % 2)
			{
				spriteName += "_Redhead";
			}
			else
			{
				spriteName += "_Emo";
			}
		}
		if (result->Class == L"Rentner")
		{
			spriteName += "_Grandma";
		}
		if (result->Class == L"Einfacher Angestellter")
		{
			spriteName += "_Normal";
		}
		if (result->Class == L"Leitender Angestellter")
		{
			spriteName += "_Intellectual";
		}
		if (result->Class == L"Reich")
		{
			spriteName += "_Intellectual";
		}
	}


	if (result->Class == L"Hartz IV-ler")
	{
		result->ExpectedProducts = 7;
	}
	if (result->Class == L"Punk")
	{
		result->ExpectedProducts = 9;
	}
	if (result->Class == L"Nazi")
	{
		result->ExpectedProducts = 10;
	}
	if (result->Class == L"Kind")
	{
		result->ExpectedProducts = 10;
	}
	if (result->Class == L"Jugendlich")
	{
		result->ExpectedProducts = 10;
	}
	if (result->Class == L"Rentner")
	{
		result->ExpectedProducts = 4;
	}
	if (result->Class == L"Einfacher Angestellter")
	{
		result->ExpectedProducts = 5;
	}
	if (result->Class == L"Leitender Angestellter")
	{
		result->ExpectedProducts = 2;
	}
	if (result->Class == L"Reich")
	{
		result->ExpectedProducts = 1;
	}

	result->HygieneExpectation = 1.0f - (float)result->ExpectedProducts / 10.0f;
	

	std::wstring name = L"";
	if (rand() % 100 < 1)
	{
		name += L"Prof. ";
	}
	if (rand() % 100 < 5)
	{
		name += L"Dr. ";
		if (rand() % 100 < 5)
		{
			name += L"Dr. ";
			if (rand() % 100 < 5)
			{
				name += L"Dr. ";
			}
		}
	}
	if (result->PersonGender == Person::Gender::Male)
	{
		name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
	}
	else
	{
		name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
	}
	if (rand() % 100 < 5)
	{
		if (result->PersonGender == Person::Gender::Male)
		{
			name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
		}
		else
		{
			name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
		}

		if (rand() % 100 < 5)
		{
			if (result->PersonGender == Person::Gender::Male)
			{
				name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
			}
			else
			{
				name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
			}
		}
	}

	name += this->Surnames[rand() % this->Surnames.size()];

	result->Name = name;

	result->Speed = 1.5f + (float)(rand() % 7 - 3) / 10.0f;
	if (result->Class == L"Rentner")
	{
		if (result->PersonGender == Person::Gender::Female)
		{
			result->Speed *= 0.15f;
		}
		else
		{
			result->Speed *= 0.6f;
		}
		result->Age += 50;
	}

	std::vector<ProductListItem*> shoppingList;

	for (int j = 0; j < 10; j++)
	{
		ProductListItem* product = 0;
		while (product == 0)
		{
			product = this->Shoppinglists[i][rand() % this->Shoppinglists[i].size()];
		}
		shoppingList.push_back(product);
	}

	result->ShoppingList = shoppingList;



	result->Sprite = this->TexHandler->GetSprite(spriteName + "_Front");
	result->BackSprite = this->TexHandler->GetSprite(spriteName + "_Back");
	result->Emoticons = this->TexHandler->GetSprite("Emotion");
	for (unsigned int j = 0; j < result->Emoticons[0].size(); j++)
	{
		result->Emoticons[0][j]->setOrigin(result->Emoticons[0][j]->getGlobalBounds().width / 2, result->Emoticons[0][j]->getGlobalBounds().height);
	}
	result->SpriteName = spriteName;
	result->Origins = this->TexHandler->GetOrigins(spriteName);
	result->ThePathfinder = this->ThePathfinder;
	result->Grid = this->Grid;
	result->Messages = this->Messages;
	result->ProductsFound.resize(result->ShoppingList.size());
	result->Products.resize(result->ShoppingList.size());
	result->PriceExpectancy.resize(result->ShoppingList.size());
	for (unsigned int j = 0; j < result->PriceExpectancy.size(); j++)
	{
		result->PriceExpectancy[j] = result->ShoppingList[j]->Price * 2.0;
	}
	result->Feed = this->Feed;

	return result;
}


Customer* PeopleGenerator::GenerateDrugGuy(std::string drug)
{
	Customer* result = new Customer();

	result->Name = L"Giovanni";
	result->Class = L"Drogenkurier";
	result->Age = 30;
	result->Money = 1000.0f;
	result->Speed = 1.5f;
	result->ExpectedProducts = 1;
	result->HygieneExpectation = 0.5f;
	result->PersonGender = Person::Gender::Male;

	for (unsigned int i = 0; i < this->ProductList->size(); i++)
	{
		if ((*this->ProductList)[i]->Name == drug)
		{
			result->ShoppingList.push_back((*this->ProductList)[i]);
			break;
		}
	}


	result->SpriteName = "Customer_Male_Jamaican";
	result->Sprite = this->TexHandler->GetSprite(result->SpriteName + "_Front");
	result->BackSprite = this->TexHandler->GetSprite(result->SpriteName + "_Back");
	result->Emoticons = this->TexHandler->GetSprite("Emotion");
	for (unsigned int i = 0; i < result->Emoticons[0].size(); i++)
	{
		result->Emoticons[0][i]->setOrigin(result->Emoticons[0][i]->getGlobalBounds().width / 2, result->Emoticons[0][i]->getGlobalBounds().height);
	}
	result->Origins = this->TexHandler->GetOrigins(result->SpriteName);
	result->ThePathfinder = this->ThePathfinder;
	result->Grid = this->Grid;
	result->Messages = this->Messages;
	result->ProductsFound.resize(result->ShoppingList.size());
	result->Products.resize(result->ShoppingList.size());
	result->PriceExpectancy.resize(result->ShoppingList.size());
	for (unsigned int i = 0; i < result->PriceExpectancy.size(); i++)
	{
		result->PriceExpectancy[i] = result->ShoppingList[i]->Price * 2.0;
	}
	result->Feed = this->Feed;

	return result;
}


Worker* PeopleGenerator::GenerateWorker()
{
	std::string spriteName = "Worker";
	Worker* result = new Worker();
	result->Speed = 1.5f;
	result->Happiness = 0.5f;
	result->Age = 30;
	if (rand() % 2)
	{
		result->PersonGender = Person::Gender::Male;
		spriteName += "_Male";

		int blarand = rand() % 7;

		if (blarand == 0)
		{
			spriteName += "_Asian";
			result->CleaningOffsets[0] = sf::Vector2f(153, 38);
			result->CleaningOffsets[1] = sf::Vector2f(-163, 48);
			result->CleaningOffsets[2] = sf::Vector2f(-49, -86);
			result->CleaningOffsets[3] = sf::Vector2f(33, -87);
		}
		if (blarand == 1)
		{
			spriteName += "_Black";
			result->CleaningOffsets[0] = sf::Vector2f(230, -40);
			result->CleaningOffsets[1] = sf::Vector2f(-246, -30);
			result->CleaningOffsets[2] = sf::Vector2f(-110, -243);
			result->CleaningOffsets[3] = sf::Vector2f(23, -243);
		}
		if (blarand == 2)
		{
			spriteName += "_Grandpa";
		}
		if (blarand == 3)
		{
			spriteName += "_Indian";
			result->CleaningOffsets[0] = sf::Vector2f(39, 40);
			result->CleaningOffsets[1] = sf::Vector2f(-54, 50);
			result->CleaningOffsets[2] = sf::Vector2f(-63, -40);
			result->CleaningOffsets[3] = sf::Vector2f(0, -41);
		}
		if (blarand == 4)
		{
			spriteName += "_Intern";
		}
		if (blarand == 5)
		{
			spriteName += "_Mexican";
			result->CleaningOffsets[0] = sf::Vector2f(111, 39);
			result->CleaningOffsets[1] = sf::Vector2f(-126, 49);
			result->CleaningOffsets[2] = sf::Vector2f(-16, -47);
			result->CleaningOffsets[3] = sf::Vector2f(3, -46);
		}
		if (blarand == 6)
		{
			spriteName += "_Russian";
		}
	}
	else
	{
		result->PersonGender = Person::Gender::Female;
		spriteName += "_Female";

		int blarand = rand() % 2;

		if (blarand == 0)
		{
			spriteName += "_Bitch";
		}
		if (blarand == 1)
		{
			spriteName += "_Intern";
			result->CleaningOffsets[0] = sf::Vector2f(16, -90);
			result->CleaningOffsets[1] = sf::Vector2f(-2, -80);
			result->CleaningOffsets[2] = sf::Vector2f(-40, -143);
			result->CleaningOffsets[3] = sf::Vector2f(73, -161);
		}
	}

	std::wstring name = L"";
	if (result->PersonGender == Person::Gender::Male)
	{
		name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
	}
	else
	{
		name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
	}
	if (rand() % 100 < 5)
	{
		if (result->PersonGender == Person::Gender::Male)
		{
			name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
		}
		else
		{
			name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
		}

		if (rand() % 100 < 5)
		{
			if (result->PersonGender == Person::Gender::Male)
			{
				name += this->FirstNamesMale[rand() % this->FirstNamesMale.size()] + L" ";
			}
			else
			{
				name += this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()] + L" ";
			}
		}
	}

	name += this->Surnames[rand() % this->Surnames.size()];

	result->Name = name;

	result->Grid = this->Grid;
	result->CleaningEfficiency = (float)(rand() % 100 + 1) / 100.0f;
	result->CashieringEfficiency = (float)(rand() % 100 + 1) / 100.0f;
	result->RefillingEfficiency = (float)(rand() % 100 + 1) / 100.0f;
	result->calculateWageExpectency();
	result->Messages = this->Messages;
	result->ThePathfinder = this->ThePathfinder;
	result->Sprite = this->TexHandler->GetSprite(spriteName + "_Front");
	result->BackSprite = this->TexHandler->GetSprite(spriteName + "_Back");
	result->SpriteName = spriteName;
	result->Origins = this->TexHandler->GetOrigins(spriteName);
	result->WorkerType = Worker::Type::Storager;
	result->Jobs = this->Jobs;
	result->TheStorage = this->ProductStorage;
	result->Wage = 7.00;
	result->WorkHours = 8;
	result->WorkDays[0] = true;
	result->WorkDays[1] = true;
	result->WorkDays[2] = true;
	result->WorkDays[3] = true;
	result->WorkDays[4] = true;
	result->WorkDays[5] = true;
	result->WorkDays[6] = false;
	result->Feed = this->Feed;

	return result;
}


Animal* PeopleGenerator::GenerateAnimal()
{
	Animal* result = new Animal();

	result->Grid = this->Grid;

	if (rand() % 2)
	{
		result->Name = this->FirstNamesMale[rand() % this->FirstNamesMale.size()];
	}
	else
	{
		result->Name = this->FirstNamesFemale[rand() % this->FirstNamesFemale.size()];
	}

	result->Sprite = this->TexHandler->GetSprite("Dog_Front");
	result->BackSprite = this->TexHandler->GetSprite("Dog_Back");
	result->SpriteName = "Dog";

	return result;
}


void PeopleGenerator::ChangeClassProbability(std::wstring className, float value)
{
	for (unsigned int i = 0; i < this->Classes.size(); i++)
	{
		if (this->Classes[i] == className)
		{
			this->ClassProbabilities[i] += value;
		}
		else
		{
			this->ClassProbabilities[i] -= value / (float)(this->Classes.size() - 1);
		}
	}
}


void PeopleGenerator::loadShoppingLists()
{
	std::fstream file;
	file.open("Content/Data/ShoppingLists.txt");
	std::string product;
	std::string quality;
	std::vector<ProductListItem*> products;
	bool first = true;

	for(std::istreambuf_iterator<char> i(file), e; i != e; ++i)
	{
		if (*i == '\n')
		{
			i++;
		}
		else if (*i == ':')
		{
			first = false;
			i++;
		}
		else if (*i == '/')
		{
			products.push_back(this->getItem(product, quality));
			quality.clear();
		}
		else if (*i == ',')
		{
			products.push_back(this->getItem(product, quality));
			quality.clear();
			product.clear();
			first = true;
			i++;
		}
		else if (*i == ';')
		{
			products.push_back(this->getItem(product, quality));
			this->Shoppinglists.push_back(products);
			products.clear();
			quality.clear();
			product.clear();
			first = true;
		}
		else
		{
			if (first)
			{
				product += *i;
			}
			else
			{
				quality += *i;
			}
		}
	}

	file.close();
}


ProductListItem* PeopleGenerator::getItem(std::string productName, std::string quality)
{
	ProductQuality qual = ProductQuality::Illegal;
	if (quality == "Billig")
	{
		qual = ProductQuality::Cheap;
	}
	else if (quality == "Normal")
	{
		qual = ProductQuality::Normal;
	}
	else if (quality == "Luxus")
	{
		qual = ProductQuality::Premium;
	}

	for (unsigned int i = 0; i < this->ProductList->size(); i++)
	{
		if ((*this->ProductList)[i]->Product == productName && (*this->ProductList)[i]->Quality == qual)
		{
			return (*this->ProductList)[i];
		}
	}

	return 0;
}


std::vector<ProductListItem*> PeopleGenerator::getItems(std::string quality)
{
	std::vector<ProductListItem*> result;
	ProductQuality qual = ProductQuality::Illegal;
	if (quality == "Billig")
	{
		qual = ProductQuality::Cheap;
	}
	else if (quality == "Normal")
	{
		qual = ProductQuality::Normal;
	}
	else if (quality == "Luxus")
	{
		qual = ProductQuality::Premium;
	}

	for (unsigned int i = 0; i < this->ProductList->size(); i++)
	{
		if ((*this->ProductList)[i]->Quality == qual && (*this->ProductList)[i]->Product != "Drogen")
		{
			result.push_back((*this->ProductList)[i]);
		}
	}

	return result;
}