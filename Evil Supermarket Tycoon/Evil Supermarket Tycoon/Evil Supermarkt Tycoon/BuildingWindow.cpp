#include "BuildingWindow.h"


BuildingWindow::BuildingWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, TextureHandler* texHandler)
{
	this->Name = name;
	this->Sprite = sprite;
	this->Sprite->setPosition(position);
	this->IsOpen = false;
	this->CurrentButton = 0;


	this->Buttons.push_back(new InterfaceButton("Close", InterfaceButton::Type::Normal, texHandler->GetSprite("CloseButton")[0][0], this->Sprite->getPosition(), sf::Vector2f(950, 30)));
	this->Buttons.back()->PressedSprite = texHandler->GetSprite("CloseButton_Pressed")[0][0];


	std::list<InterfaceButton*> tmpList;


	//add floors
	sf::Sprite* floor = texHandler->GetSprite("Floor_Cheap_1")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_1", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(25, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_2")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_2", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(100, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_3")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_3", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(175, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_4")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_4", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(250, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_5")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_5", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(325, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_6")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_6", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(400, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_7")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_7", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(475, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_8")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_8", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(550, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_9")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_9", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(625, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_10")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_10", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(700, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_11")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_11", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(775, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");

	floor = texHandler->GetSprite("Floor_Cheap_12")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Cheap_12", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(850, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"5€");



	floor = texHandler->GetSprite("Floor_Normal_1")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_1", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(25, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_2")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_2", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(100, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_3")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_3", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(175, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_4")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_4", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(250, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_5")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_5", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(325, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_6")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_6", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(400, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_7")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_7", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(475, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_8")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_8", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(550, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_9")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_9", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(625, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_10")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_10", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(700, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_11")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_11", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(775, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");

	floor = texHandler->GetSprite("Floor_Normal_12")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Normal_12", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(850, 125)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"20€");




	floor = texHandler->GetSprite("Floor_Premium_1")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_1", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(25, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_2")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_2", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(100, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_3")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_3", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(175, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_4")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_4", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(250, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_5")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_5", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(325, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_6")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_6", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(400, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_7")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_7", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(475, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_8")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_8", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(550, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_9")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_9", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(625, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_10")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_10", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(700, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_11")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_11", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(775, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");

	floor = texHandler->GetSprite("Floor_Premium_12")[0][0];
	floor->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Floor_Premium_12", InterfaceButton::Permanent, floor, this->Sprite->getPosition(), sf::Vector2f(850, 175)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 31));
	tmpList.back()->Text.setString(L"60€");



	sf::Sprite* floorButton = texHandler->GetSprite("SimpleButton")[0][0];
	this->Buttons.push_back(new TabButton("FloorButton", floorButton, this->Sprite->getPosition(), sf::Vector2f(15, 15), tmpList));
	this->Buttons.back()->Text.setString("Boden");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());


	tmpList.clear();


	//add walls
	sf::Sprite* wall = texHandler->GetSprite("Wall_Cheap")[0][0];
	wall->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Wall_Cheap", InterfaceButton::Permanent, wall, this->Sprite->getPosition(), sf::Vector2f(25, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(22, 130));
	tmpList.back()->Text.setString(L"29€");

	wall = texHandler->GetSprite("Door_Cheap")[0][0];
	wall->scale(0.5, 0.5);
	wall->setOrigin(100, 100);
	tmpList.push_back(new InterfaceButton("Door_Cheap", InterfaceButton::Permanent, wall, this->Sprite->getPosition(), sf::Vector2f(125, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(52, 140));
	tmpList.back()->Text.setString(L"100€");

	wall = texHandler->GetSprite("Checkout_Cheap_Front")[0][0];
	wall->scale(0.5, 0.5);
	wall->setOrigin(0, 0);
	tmpList.push_back(new InterfaceButton("Checkout_Cheap", InterfaceButton::Permanent, wall, this->Sprite->getPosition(), sf::Vector2f(275, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(55, 120));
	tmpList.back()->Text.setString(L"79€");

	wall = texHandler->GetSprite("Fruitshelf_Normal")[0][0];
	wall->scale(0.5, 0.5);
	wall->setOrigin(0, 0);
	tmpList.push_back(new InterfaceButton("Fruitshelf_Normal", InterfaceButton::Permanent, wall, this->Sprite->getPosition(), sf::Vector2f(450, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(55, 140));
	tmpList.back()->Text.setString(L"200€");

	sf::Sprite* wallsButton = texHandler->GetSprite("SimpleButton")[0][0];
	this->Buttons.push_back(new TabButton("WallsButton", wallsButton, this->Sprite->getPosition(), sf::Vector2f(100, 15), tmpList));
	this->Buttons.back()->Text.setString("Wände");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());


	tmpList.clear();


	//add shelfs
	sf::Sprite* shelf = texHandler->GetSprite("Shelf_Illegal")[0][0];
	shelf->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Shelf_Illegal", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(25, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(50, 145));
	tmpList.back()->Text.setString(L"50€");

	shelf = texHandler->GetSprite("Shelf_Cheap")[0][0];
	shelf->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Shelf_Cheap", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(125, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(50, 145));
	tmpList.back()->Text.setString(L"100€");

	shelf = texHandler->GetSprite("Shelf_Normal")[0][0];
	shelf->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Shelf_Normal", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(225, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(50, 145));
	tmpList.back()->Text.setString(L"250€");

	shelf = texHandler->GetSprite("Shelf_Premium")[0][0];
	shelf->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Shelf_Premium", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(325, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(50, 145));
	tmpList.back()->Text.setString(L"600€");

	shelf = texHandler->GetSprite("Fridge_Normal")[0][0];
	shelf->scale(0.5, 0.5);
	tmpList.push_back(new InterfaceButton("Fridge_Normal", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(435, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(50, 145));
	tmpList.back()->Text.setString(L"349€");

	shelf = texHandler->GetSprite("Freezer_Normal")[0][0];
	shelf->scale(0.75, 0.75);
	tmpList.push_back(new InterfaceButton("Freezer_Normal", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(575, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(70, 130));
	tmpList.back()->Text.setString(L"669€");

	shelf = texHandler->GetSprite("Palette_Normal")[0][0];
	shelf->scale(0.75, 0.75);
	tmpList.push_back(new InterfaceButton("Palette_Normal", InterfaceButton::Permanent, shelf, this->Sprite->getPosition(), sf::Vector2f(725, 75)));
	tmpList.back()->Text.setFont(*texHandler->GetFont());
	tmpList.back()->Text.setPosition(tmpList.back()->Sprite->getPosition() + sf::Vector2f(70, 100));
	tmpList.back()->Text.setString(L"99€");

	sf::Sprite* shelfButton = texHandler->GetSprite("SimpleButton")[0][0];
	this->Buttons.push_back(new TabButton("ShelfButton", shelfButton, this->Sprite->getPosition(), sf::Vector2f(185, 15), tmpList));
	this->Buttons.back()->Text.setString("Regale");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());


	sf::Sprite* removeButton = texHandler->GetSprite("SimpleButton")[0][0];
	this->Buttons.push_back(new InterfaceButton("RemoveButton", InterfaceButton::Permanent, removeButton, this->Sprite->getPosition(), sf::Vector2f(270, 15)));
	this->Buttons.back()->Text.setString("Löschen");
	this->Buttons.back()->Text.setFont(*texHandler->GetFont());
}


BuildingWindow::~BuildingWindow(void)
{

}


void BuildingWindow::filterButtons(std::string mode)
{
	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	std::list<InterfaceButton*>::iterator buttiter;
	std::list<InterfaceButton*>::iterator buttend;

	while (iter != end)
	{
		if (dynamic_cast<TabButton*>(*iter))
		{
			buttiter = dynamic_cast<TabButton*>(*iter)->Buttons.begin();
			buttend = dynamic_cast<TabButton*>(*iter)->Buttons.end();

			while (buttiter != buttend)
			{
				if (mode == "Level 1")
				{
					(*buttiter)->Disabled = (*buttiter)->Name.find("Normal") != std::string::npos || (*buttiter)->Name.find("Premium") != std::string::npos;
				}
				else if (mode == "Level 2")
				{
					(*buttiter)->Disabled = (*buttiter)->Name.find("Premium") != std::string::npos;
				}
				else
				{
					(*buttiter)->Disabled = false;
				}

				buttiter++;
			}
		}

		iter++;
	}
}


void BuildingWindow::update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages)
{
	sf::Vector2i mousePos = input->GetMousePos();
	bool clicked = false;
	if (input->JustClicked(sf::Mouse::Left) && this->Sprite->getGlobalBounds().contains(mousePos.x, mousePos.y))
	{
		clicked = true;
	}


	std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			(*iter)->update(input);
		}

		if ((*iter)->Pressed && (*iter) != this->CurrentButton)
		{
			if (this->CurrentButton != 0)
			{
				this->CurrentButton->JustPressed = false;
				this->CurrentButton->Pressed = false;
			}
			this->CurrentButton = (*iter);
		}

		iter++;
	}

	if (this->CurrentButton != 0 && this->CurrentButton->Name == "Close")
	{
		InterfaceMessage tmp;
		tmp.MessageType = InterfaceMessage::Type::CloseWindow;
		tmp.Text = "Building";
		messages->push_back(InterfaceMessage(tmp));
		this->CurrentButton->JustPressed = false;
		this->CurrentButton->Pressed = false;
		this->CurrentButton = 0;
	}
}


void BuildingWindow::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);

	std::list<InterfaceButton*>::const_iterator iter = this->Buttons.begin();
	std::list<InterfaceButton*>::const_iterator end = this->Buttons.end();

	while (iter != end)
	{
		if ((*iter)->Visible)
		{
			target.draw(**iter);
		}
		iter++;
	}
}


std::string BuildingWindow::getSelected()
{
	if (this->isOpen())
	{
		std::list<InterfaceButton*>::iterator iter = this->Buttons.begin();
		std::list<InterfaceButton*>::iterator end = this->Buttons.end();

		while (iter != end)
		{
			if ((*iter)->Pressed)
			{
				if ((*iter)->Name == "RemoveButton")
				{
					return "Remove";
				}
				else if ((*iter)->Name != "Close")
				{
					return dynamic_cast<TabButton*>(*iter)->getSelected();
				}
			}
			iter++;
		}
	}

	return "NOOOOOOO";
}