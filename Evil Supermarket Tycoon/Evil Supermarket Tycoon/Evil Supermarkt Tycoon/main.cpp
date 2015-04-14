#include <algorithm>
#include <SFML/Graphics.hpp>
#include "Game.h"
#include "Input.h"
#include "Interface.h"
#include "DoublyLinkedList.h"
#include "Tree.h"

int main()
{
	srand(time(0));

	sf::RenderWindow window(sf::VideoMode::getDesktopMode(), "Supermarkt");
	

	sf::View View(sf::Vector2f(0, 500), sf::Vector2f(window.getSize().x, window.getSize().y));
	sf::View UIView(sf::FloatRect(0, 0, window.getSize().x, window.getSize().y));
	sf::FloatRect ResetRect(View.getCenter().x - View.getSize().x / 2, View.getCenter().y - View.getSize().y / 2, View.getSize().x, View.getSize().y);
	

	window.setView(View);
	
	Game TheGame(View.getSize());

	Interface UI(&window, &TheGame.TexHandler, &TheGame.ProductList, &TheGame.CurrentStore->ProductStorage, &TheGame.CurrentStore->Jobs, &TheGame.CurrentStore->PeepGenerator, &TheGame.CurrentStore->Workers, &TheGame.Feed, &TheGame.IllegalStuff);

	sf::Clock Clock;
	sf::Time ElapsedTime;

	Input InputHandler;

	int ZoomState = 0;

	while (window.isOpen())
	{
		sf::Event event;
		while (window.pollEvent(event))
		{
			switch (event.type)
			{
			case sf::Event::Closed:
				window.close();
				break;

			case sf::Event::Resized:
				View = sf::View(sf::FloatRect(0, 0, event.size.width, event.size.height));
				UIView = sf::View(sf::FloatRect(0, 0, event.size.width, event.size.height));
				ResetRect = sf::FloatRect(View.getCenter().x - View.getSize().x / 2, View.getCenter().y - View.getSize().y / 2, View.getSize().x, View.getSize().y);
				TheGame.GameMenu.Resize(View.getSize());
				break;

			case sf::Event::MouseWheelMoved:
				if (event.mouseWheel.delta > 0 && ZoomState < 10)
				{
					ZoomState = std::min(ZoomState + event.mouseWheel.delta, 10);
				}
				else if (event.mouseWheel.delta < 0 && ZoomState > -10)
				{
					ZoomState = std::max(ZoomState + event.mouseWheel.delta, -10);
				}
				View.reset(ResetRect);
				for (int i = 0; i < abs(ZoomState); i++)
				{
					if (ZoomState > 0)
					{
						View.zoom(0.9f);
					}
					else
					{
						View.zoom(1.1f);
					}
				}
				break;

			case sf::Event::TextEntered:
				InputHandler.UpdateText(event.text.unicode);
				break;
			}
		}

		ElapsedTime = Clock.restart();
		
		InputHandler.Update(&window);

		if (InputHandler.GetMousePos().x < 20 || InputHandler.IsPressed(Input::ScrollLeft))
		{
			View.move(-500 * ElapsedTime.asSeconds(), 0);
			ResetRect.left += -500 * ElapsedTime.asSeconds();
		}

		if (InputHandler.GetMousePos().x > window.getSize().x - 20 || InputHandler.IsPressed(Input::ScrollRight))
		{
			View.move(500 * ElapsedTime.asSeconds(), 0);
			ResetRect.left += 500 * ElapsedTime.asSeconds();
		}

		if (InputHandler.GetMousePos().y < 20 || InputHandler.IsPressed(Input::ScrollUp))
		{
			View.move(0, -500 * ElapsedTime.asSeconds());
			ResetRect.top += -500 * ElapsedTime.asSeconds();
		}

		if (InputHandler.GetMousePos().y > window.getSize().y - 20 || InputHandler.IsPressed(Input::ScrollDown))
		{
			View.move(0, 500 * ElapsedTime.asSeconds());
			ResetRect.top += 500 * ElapsedTime.asSeconds();
		}

		if (TheGame.GameState != Game::State::Pause && TheGame.GameState != Game::State::Intro && TheGame.GameState != Game::State::MainMenu)
		{
			UI.update(&InputHandler, &TheGame.Data, &TheGame.CurrentStore->Grid, &TheGame.CurrentStore->FreeIDs, &TheGame.CurrentStore->HighestID);
		}
		if (UI.Messages.size() > 0)
		{
			if (UI.Messages.front().MessageType == InterfaceMessage::Type::ChangeGameState)
			{
				if (UI.Messages.front().Text == "BeforeOpening")
				{
					TheGame.GameState = Game::State::BeforeOpening;
					UI.State = 0;
					while (TheGame.Data.Time.Hour != TheGame.CurrentStore->OpeningHour[TheGame.Data.WeekDay] || !TheGame.CurrentStore->OpeningDays[TheGame.Data.WeekDay])
					{
						TheGame.Data.update(ElapsedTime, 8);
					}

					if (TheGame.Data.Prestige >= 200)
					{
						if (TheGame.Data.Prestige >= 1500)
						{
							for (int i = 0; i < TheGame.CurrentStore->PeepGenerator.ClassProbabilities.size(); i++)
							{
								TheGame.CurrentStore->PeepGenerator.ClassProbabilities[i] = 1.0f / 9.0f;
								TheGame.CurrentStore->PeepGenerator.AverageStealingPotential = 0.01f;
							}
						}
						else
						{
							for (int i = 0; i < TheGame.CurrentStore->PeepGenerator.ClassProbabilities.size(); i++)
							{
								TheGame.CurrentStore->PeepGenerator.ClassProbabilities[i] = 0.0f;
								if (i < TheGame.CurrentStore->PeepGenerator.ClassProbabilities.size() - 2)
								{
									TheGame.CurrentStore->PeepGenerator.ClassProbabilities[i] = 1.0f / 7.0f;
								}
							}
							TheGame.CurrentStore->PeepGenerator.AverageStealingPotential = 0.03f;
						}
					}
					else
					{
						for (int i = 0; i < TheGame.CurrentStore->PeepGenerator.ClassProbabilities.size(); i++)
						{
							TheGame.CurrentStore->PeepGenerator.ClassProbabilities[i] = 0.0f;
							if (i < TheGame.CurrentStore->PeepGenerator.ClassProbabilities.size() - 4)
							{
								TheGame.CurrentStore->PeepGenerator.ClassProbabilities[i] = 1.0f / 5.0f;
							}
						}
						TheGame.CurrentStore->PeepGenerator.AverageStealingPotential = 0.05f;
					}

					int i = 0;
					if (TheGame.IllegalStuff[i].WasActive) //alkohol an jugendliche
					{
						if (rand() % 100 + 1 <= TheGame.IllegalStuff[i].PenaltyProbability * 100)
						{
							if (TheGame.PenaltyLevel[i] < 3)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Dem Jugendamt ist zu Ohren gekommen, dass sie Alkohol an Minderjaehrige verkaufen.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich.";
								TheGame.Data.Prestige -= 100;
							}
							if (TheGame.PenaltyLevel[i] == 2)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich und sie muessen 400Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 100;
								TheGame.Data.CurrentMoney -= 400.0;
							}
							if (TheGame.PenaltyLevel[i] == 3)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich massiv und sie muessen 800Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 200;
								TheGame.Data.CurrentMoney -= 800.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}

						TheGame.CurrentStore->PeepGenerator.ChangeClassProbability(L"Kind", 0.1f);
						TheGame.CurrentStore->PeepGenerator.ChangeClassProbability(L"Jugendlich", 0.2f);
					}

					i = 1;
					if (TheGame.IllegalStuff[i].WasActive) //Lachgas
					{
						TheGame.Data.CurrentMoney -= 200.0 * (TheGame.CurrentStore->ClosingHour[TheGame.Data.WeekDay] - TheGame.CurrentStore->OpeningHour[TheGame.Data.WeekDay]);
						if (rand() % 100 + 1 <= TheGame.IllegalStuff[i].PenaltyProbability * 100)
						{
							if (TheGame.PenaltyLevel[i] < 3)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Das Ordnungsamt ist auf ihre Lachgasnutzung aufmerksam geworden.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich.";
								TheGame.Data.Prestige -= 100;
							}
							if (TheGame.PenaltyLevel[i] == 2)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich und sie muessen 400Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 100;
								TheGame.Data.CurrentMoney -= 400.0;
							}
							if (TheGame.PenaltyLevel[i] == 3)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich massiv und sie muessen 800Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 200;
								TheGame.Data.CurrentMoney -= 800.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}
					}

					i = 2;
					if (TheGame.IllegalStuff[i].WasActive) //Werbung
					{
						TheGame.Data.CurrentMoney -= 150.0;
						if (rand() % 100 + 1 <= TheGame.IllegalStuff[i].PenaltyProbability * 100)
						{
							if (TheGame.PenaltyLevel[i] < 3)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Es ist aufgefallen, dass sie unterschwellige Werbung senden.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich.";
								TheGame.Data.Prestige -= 100;
							}
							if (TheGame.PenaltyLevel[i] == 2)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich und sie muessen 400Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 100;
								TheGame.Data.CurrentMoney -= 400.0;
							}
							if (TheGame.PenaltyLevel[i] == 3)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich massiv und sie muessen 800Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 200;
								TheGame.Data.CurrentMoney -= 800.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}
					}

					i = 3;
					if (TheGame.IllegalStuff[i].WasActive) //Illegale Produkte
					{
						if (rand() % 100 + 1 <= TheGame.IllegalStuff[i].PenaltyProbability * 100)
						{
							if (TheGame.PenaltyLevel[i] < 1)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Es ist aufgefallen, dass sie illegale Produkte verkaufen.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Sie muessen 1000Euro Strafe zahlen.";
								TheGame.Data.CurrentMoney -= 1000.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}
					}

					i = 4;
					if (TheGame.IllegalStuff[i].WasActive) //Abgelaufene Produkte
					{
						if (rand() % 100 + 1 <= TheGame.IllegalStuff[i].PenaltyProbability * 100)
						{
							if (TheGame.PenaltyLevel[i] < 3)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Einige Kunden beschweren sich ueber abgelaufene Produkte.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich.";
								TheGame.Data.Prestige -= 100;
							}
							if (TheGame.PenaltyLevel[i] == 2)
							{
								tmp.Text = "Kunden beschweren sich vermehrt ueber abgelaufene Produkte. Ihr Ruf verschlechtert sich und sie muessen 400Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 100;
								TheGame.Data.CurrentMoney -= 400.0;
							}
							if (TheGame.PenaltyLevel[i] == 3)
							{
								tmp.Text = "Viele Kunden beschweren sich ueber abgelaufene Produkte. Ihr Ruf verschlechtert sich massiv und sie muessen 800Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 200;
								TheGame.Data.CurrentMoney -= 800.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}
						if (TheGame.IllegalStuff[i].PenaltyProbability < 1.0f)
						{
							TheGame.IllegalStuff[i].PenaltyProbability += 0.1f;
						}
					}
					else
					{
						if (TheGame.IllegalStuff[i].PenaltyProbability > 0.0f)
						{
							TheGame.IllegalStuff[i].PenaltyProbability -= 0.1f;
						}
					}

					//Drogen
					if (TheGame.CurrentStore->DrugsDelivered)
					{
						TheGame.CurrentStore->DrugsDelivered = false;

						if (TheGame.CurrentStore->ProductStorage.countProduct("Krokodil") || TheGame.CurrentStore->ProductStorage.countProduct("Blue Meth"))
						{
							if (rand() % 2)
							{
								TheGame.CurrentStore->Drug = "Krokodil";
							}
							else
							{
								TheGame.CurrentStore->Drug = "Blue Meth";
							}
							TheGame.CurrentStore->DrugHour = rand() % (TheGame.CurrentStore->ClosingHour[(TheGame.Data.WeekDay + 1) * ((TheGame.Data.WeekDay + 1) < 7)] - TheGame.CurrentStore->OpeningHour[(TheGame.Data.WeekDay + 1) * ((TheGame.Data.WeekDay + 1) < 7)]) + TheGame.CurrentStore->OpeningHour[(TheGame.Data.WeekDay + 1) * ((TheGame.Data.WeekDay + 1) < 7)];
						}
						else
						{
							TheGame.CurrentStore->DrugHour = -1;
						}

						i = TheGame.PenaltyLevel.size() - 1;
						if (rand() % 100 + 1 <= 60)
						{
							if (TheGame.PenaltyLevel[i] < 3)
							{
								TheGame.PenaltyLevel[i]++;
							}

							InterfaceMessage tmp;
							tmp.MessageType = InterfaceMessage::Type::SetPenalty;
							tmp.Text = "Die Polizei ermittelt aufgrund von Berichten ueber Drogenverkaeufe in ihrem Laden.";
							if (TheGame.PenaltyLevel[i] == 1)
							{
								tmp.Text += "Ihr Ruf verschlechtert sich.";
								TheGame.Data.Prestige -= 100;
							}
							if (TheGame.PenaltyLevel[i] == 2)
							{
								tmp.Text = "Der Drogenhandel in ihrem Laden ist aufgeflogen. Ihr Ruf verschlechtert sich und sie muessen 400Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 100;
								TheGame.Data.CurrentMoney -= 400.0;
							}
							if (TheGame.PenaltyLevel[i] == 3)
							{
								tmp.Text += "Der Drogenhandel in ihrem Laden ist erneut aufgeflogen. Ihr Ruf verschlechtert sich massiv und sie muessen 800Euro Strafe zahlen.";
								TheGame.Data.Prestige -= 200;
								TheGame.Data.CurrentMoney -= 800.0;
							}
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);

							tmp.MessageType = InterfaceMessage::Type::OpenWindow;
							tmp.Text = "Penalty";
							tmp.Pointer = 0;

							UI.Messages.push_back(tmp);
						}
					}

					for (int i = 0; i < TheGame.IllegalStuff.size(); i++)
					{
						TheGame.IllegalStuff[i].WasActive = false;
					}
				}
				if (UI.Messages.front().Text == "MainGame")
				{
					TheGame.GameState = Game::State::MainGame;
					TheGame.CurrentStore->Spawn = true;
					UI.State = 1;
				}
				if (UI.Messages.front().Text == "AfterClosing")
				{
					TheGame.GameState = Game::State::AfterClosing;
					TheGame.CurrentStore->Spawn = false;
					UI.State = 2;

					std::vector<Worker*>::iterator iter = TheGame.CurrentStore->Workers.begin();
					std::vector<Worker*>::iterator end = TheGame.CurrentStore->Workers.end();

					while (iter != end)
					{
						TheGame.Data.CurrentMoney -= (*iter)->Wage * (*iter)->WorkHours;

						iter++;
					}
				}
				UI.Messages.pop_front();
			}
			else if (UI.Messages.front().MessageType == InterfaceMessage::Type::ChangeGameSpeed)
			{
				TheGame.Speed = atoi(UI.Messages.front().Text.c_str());
				if (TheGame.Speed == 0)
				{
					TheGame.SoundHandler.PauseAll();
				}
				else
				{
					TheGame.SoundHandler.SetSoundPitch(1.0f - 0.25f + 0.25f * TheGame.Speed);
					TheGame.SoundHandler.ResumeAll();
				}
				
				UI.Messages.pop_front();
			}
			else if (UI.Messages.front().MessageType == InterfaceMessage::Type::ChangeCameraPos)
			{
				View.setCenter(UI.Messages.front().Pointer->LowestScreenPosition);
				ResetRect.left = View.getCenter().x - View.getSize().x / 2;
				ResetRect.top = View.getCenter().y - View.getSize().y / 2;

				UI.Messages.pop_front();
			}
		}

		TheGame.update(ElapsedTime, &InputHandler, &UI);
		if (!TheGame.GameMenu.Messages.empty() && TheGame.GameMenu.Messages.front().MessageType == MenuMessage::Type::ChangeResolution)
		{
			std::string::iterator stringiter = TheGame.GameMenu.Messages.front().Text.begin();
			std::string::iterator stringend = TheGame.GameMenu.Messages.front().Text.end();

			std::string xString;
			std::string yString;
			unsigned int x;
			unsigned int y;
			bool first = true;

			while (stringiter != stringend)
			{
				if (*stringiter == 'x')
				{
					first = false;
				}
				else if (first)
				{
					xString += *stringiter;
				}
				else
				{
					yString += *stringiter;
				}

				stringiter++;
			}
			
			x = atoi(xString.c_str());
			y = atoi(yString.c_str());

			window.setSize(sf::Vector2u(x, y));

			TheGame.GameMenu.Messages.pop_front();
		}
		if (!TheGame.GameMenu.Messages.empty() && TheGame.GameMenu.Messages.front().MessageType == MenuMessage::Type::ChangeFullscreen)
		{
			if (TheGame.GameMenu.Messages.front().Value)
			{
				window.create(sf::VideoMode(window.getSize().x, window.getSize().y), "Supermarkt", sf::Style::Fullscreen);
			}
			else
			{
				window.create(sf::VideoMode(window.getSize().x, window.getSize().y), "Supermarkt", sf::Style::Default);
			}
			
			TheGame.GameMenu.Messages.pop_front();
		}
		if (TheGame.GameState == Game::State::Quit)
		{
			window.close();
		}
		if (TheGame.Loaded)
		{
			TheGame.Loaded = false;
			UI = Interface(&window, &TheGame.TexHandler, &TheGame.ProductList, &TheGame.CurrentStore->ProductStorage, &TheGame.CurrentStore->Jobs, &TheGame.CurrentStore->PeepGenerator, &TheGame.CurrentStore->Workers, &TheGame.Feed, &TheGame.IllegalStuff);
			if (TheGame.GameState == Game::State::BeforeOpening)
			{
				UI.State = 0;
			}
			else if (TheGame.GameState == Game::State::MainGame)
			{
				UI.State = 1;
			}
			else if (TheGame.GameState == Game::State::AfterClosing)
			{
				UI.State = 2;
			}
		}
		

		InputHandler.UpdateText();
		
		window.clear(sf::Color::Black);
		window.draw(TheGame);
		window.setView(UIView);
		window.draw(TheGame.GameMenu);
		if (TheGame.GameState != Game::State::Intro && TheGame.GameState != Game::State::MainMenu)
		{
			window.draw(UI);
		}
		if (TheGame.GameState != Game::State::Intro)
		{
			window.setView(View);
		}
		window.display();
	}
	
	return 0;
}