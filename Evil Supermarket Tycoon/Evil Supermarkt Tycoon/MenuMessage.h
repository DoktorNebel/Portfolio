#pragma once

#include <string>

struct MenuMessage
{
	enum Type
	{
		ChangeMenu,
		ChangeResolution,
		ChangeFullscreen,
		ChangeVolume,
		StartGame,
		LoadGame,
		SaveGame,
		ResumeGame,
		Quit
	} MessageType;

	std::string Text;
	float Value;
};