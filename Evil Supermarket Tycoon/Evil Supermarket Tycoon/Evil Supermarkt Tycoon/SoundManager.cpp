#include "SoundManager.h"


SoundManager::SoundManager(void)
{
	SoundManager::SoundObject tmp;

	this->SoundData.push_back(sf::SoundBuffer());
	this->SoundData.back().loadFromFile("Content/Sounds/Scanning.wav");

	tmp.Name = "Scanning";
	tmp.TheSound.setBuffer(this->SoundData.back());
	tmp.TheSound.setLoop(true);
	this->Sounds.push_back(tmp);

	this->SoundData.push_back(sf::SoundBuffer());
	this->SoundData.back().loadFromFile("Content/Sounds/Meat.ogg");

	tmp.Name = "Meat";
	tmp.TheSound.setBuffer(this->SoundData.back());
	tmp.TheSound.setLoop(false);
	this->Sounds.push_back(tmp);

	this->SoundData.push_back(sf::SoundBuffer());
	this->SoundData.back().loadFromFile("Content/Sounds/Cash.wav");

	tmp.Name = "Cash";
	tmp.TheSound.setBuffer(this->SoundData.back());
	tmp.TheSound.setLoop(false);
	this->Sounds.push_back(tmp);
}


SoundManager::~SoundManager(void)
{
}


void SoundManager::PlaySound(sf::String name)
{
	SoundManager::SoundObject tmp;

	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		if (iter->Name == name)
		{
			if (iter->TheSound.getStatus() != sf::Sound::Status::Playing)
			{
				iter->TheSound.play();
				break;
			}
			else
			{
				tmp = *iter;
			}
		}

		iter++;
	}

	if (iter == end)
	{
		this->Sounds.push_back(tmp);
		this->Sounds.back().TheSound.play();
	}
}


void SoundManager::StopSound(sf::String name)
{
	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		if (iter->Name == name && iter->TheSound.getStatus() == sf::Sound::Status::Playing)
		{
			iter->TheSound.stop();
			break;
		}

		iter++;
	}
}


void SoundManager::SetSoundVolume(int volume)
{
	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		iter->TheSound.setVolume(volume);

		iter++;
	}
}


void SoundManager::SetMusicVolume(int volume)
{
	this->BackgroundMusic.setVolume(volume);
}


void SoundManager::PauseAll()
{
	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		iter->TheSound.pause();

		iter++;
	}
}


void SoundManager::ResumeAll()
{
	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		if (iter->TheSound.getStatus() == sf::Sound::Status::Paused)
		{
			iter->TheSound.play();
		}

		iter++;
	}
}


void SoundManager::SetSoundPitch(float pitch)
{
	std::list<SoundManager::SoundObject>::iterator iter = this->Sounds.begin();
	std::list<SoundManager::SoundObject>::iterator end = this->Sounds.end();

	while (iter != end)
	{
		iter->TheSound.setPitch(pitch);

		iter++;
	}
}