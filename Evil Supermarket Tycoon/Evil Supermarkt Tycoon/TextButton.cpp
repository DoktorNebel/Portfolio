#include "TextButton.h"


TextButton::TextButton(void)
{

}


TextButton::TextButton(std::string name, TextButton::Type type, sf::Sprite* sprite, TextureHandler* texHandler, sf::Vector2f windowPosition, sf::Vector2f windowOffset)
{
	this->Name = name;
	this->ButtonType = type;
	this->Sprite = sprite;
	this->Sprite->setPosition(windowPosition + windowOffset);
	this->Text.setFont(*texHandler->GetFont());
	this->Text.setCharacterSize(14);
	this->Text.setColor(sf::Color(128, 242, 152, 255));
	this->text.setFont(*texHandler->GetFont());
	this->text.setCharacterSize(14);
	this->text.setColor(sf::Color(128, 242, 152, 255));
	this->text.setPosition(this->Sprite->getPosition() + sf::Vector2f(1, 2));
	this->Pressed = false;
	this->WasPressed = false;
	this->JustPressed = false;
	this->Value = "";
}


TextButton::~TextButton(void)
{
}


void TextButton::update(Input* input)
{
	sf::Vector2f mousePos = sf::Vector2f((float)input->GetMousePos().x, (float)input->GetMousePos().y);

	if (this->Pressed)
	{
		if ((input->JustClicked(sf::Mouse::Left) && !this->Sprite->getGlobalBounds().contains(mousePos)) || sf::Keyboard::isKeyPressed(sf::Keyboard::Key::Return))
		{
			this->setValue(this->Value);

			this->Pressed = false;
			this->WasPressed = true;

			this->Sprite->setColor(sf::Color::White);
		}

		if (input->GetText() == "delete" && this->Value.size() > 0)
		{
			this->Value.pop_back();
			this->text.setString(this->Value);
		}
		else if (((this->ButtonType == TextButton::Type::Price && this->Value.size() < 6) || (this->ButtonType == TextButton::Type::ExpDate && this->Value.size() < 10)) && ((input->GetText()[0] >= 48 && input->GetText()[0] <= 59) || input->GetText()[0] == 44 || input->GetText()[0] == 46))
		{
			this->Value += input->GetText();
			this->text.setString(this->Value);
		}
	}
	else
	{
		if (this->WasPressed)
		{
			this->WasPressed = false;
		}

		if (input->JustClicked(sf::Mouse::Left) && this->Sprite->getGlobalBounds().contains(mousePos))
		{
			this->Pressed = true;
			this->Sprite->setColor(sf::Color(192, 192, 192, 255));
		}
	}
}


void TextButton::draw(sf::RenderTarget& target, sf::RenderStates states) const
{
	target.draw(*this->Sprite);
	target.draw(this->Text);
	target.draw(this->text);
}


void TextButton::setValue(std::string value)
{
	if (this->ButtonType == TextButton::Type::Price)
	{
		if (this->Value.find(",") != std::string::npos)
		{
			this->Value.replace(this->Value.find(","), 1, ".");
		}
		std::stringstream sstream;
		sstream << std::fixed << std::setprecision(2) << atof(this->Value.c_str());
		this->Value = sstream.str();

		this->text.setString(sf::String(this->Value) + L"€");
	}
	else
	{
		std::string day;
		std::string month;
		std::string year;
		int stage = 0;

		std::string::iterator iter = this->Value.begin();
		std::string::iterator end = this->Value.end();
		while (iter != end)
		{
			if (*iter == '.')
			{
				stage++;
				iter++;
			}

			if (stage == 0)
			{
				day += *iter;
			}
			else if (stage == 1)
			{
				month += *iter;
			}
			else if (stage == 2)
			{
				year += *iter;
			}

			iter++;
		}

		if (day.size() > 2 || month.size() > 2)
		{
			this->Value = PrevValue;
		}

		this->text.setString(this->Value);
	}

	this->PrevValue = this->Value;
}


void TextButton::setValue(double value)
{
	std::stringstream sstream;
	sstream << std::fixed << std::setprecision(2) << value;
	this->Value = sstream.str();

	this->PrevValue = this->Value;

	this->text.setString(sf::String(this->Value) + L"€");
}


void TextButton::setValue(Date value)
{
	std::stringstream sstream;
	sstream << value.Day << "." << value.Month << "." << value.Year;
	this->Value = sstream.str();

	this->PrevValue = this->Value;

	this->text.setString(this->Value);
}