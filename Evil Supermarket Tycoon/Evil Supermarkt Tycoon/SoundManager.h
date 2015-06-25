#pragma once

#include <list>
#include "SFML/Audio.hpp"

class SoundManager
{
private:

	std::list<sf::SoundBuffer> SoundData;

	struct SoundObject
	{
		sf::String Name;
		sf::Sound TheSound;
	};

	std::list<SoundManager::SoundObject> Sounds;

	sf::Music BackgroundMusic;

public:

	SoundManager(void);
	~SoundManager(void);

	void PlaySound(sf::String name);
	void StopSound(sf::String name);
	void SetSoundVolume(int volume);
	void SetMusicVolume(int volume);
	void PauseAll();
	void ResumeAll();
	void SetSoundPitch(float pitch);
};

