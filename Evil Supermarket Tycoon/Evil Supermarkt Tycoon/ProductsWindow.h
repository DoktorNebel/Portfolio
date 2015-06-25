#pragma once

#include <iomanip>
#include "interfacewindow.h"
#include "ProductListItem.h"
#include "Slider.h"
#include "Storage.h"
#include "ProductQuality.h"

class ProductsWindow :
	public InterfaceWindow
{
private:

	std::list<InterfaceButton> FilterButtons;
	InterfaceButton* CurrentButton;
	std::list<InterfaceButton*> ShownButtons;
	std::vector<ProductListItem*>* ProductList;
	sf::Sprite* HoverWindowSprite;
	sf::Text HoverWindowText;
	sf::Text PriceText;
	Storage* ProductStorage;
	double CurrentPrice;
	std::list<Product> BuyList;
	TextureHandler* TexHandler;
	InterfaceButton* AnotherCurrentButton;

public:

	ProductListItem* CurrentItem;
	bool StorageOnly;

public:

	ProductsWindow(std::string name, sf::Sprite* sprite, sf::Vector2f position, std::vector<ProductListItem*>* productList, TextureHandler* texHandler, Storage* storage);
	~ProductsWindow(void);

	virtual void update(Input* input, GameData* gameData, std::list<InterfaceMessage>* messages);
	virtual void draw(sf::RenderTarget& target, sf::RenderStates states) const;
	void filterButtons(std::string mode);

private:

	void loadButtons(TextureHandler* texHandler, std::vector<ProductListItem*>* productList);
	ProductListItem* getCurrentItem(std::string name);
	void addToCart(std::string productName, unsigned int amount, GameData* gameData);
	void calculatePrice();
	void resetSliders();
};

