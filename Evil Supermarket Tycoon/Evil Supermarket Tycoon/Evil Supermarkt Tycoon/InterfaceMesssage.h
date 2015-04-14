#pragma once

#include <string>

class Entity;

struct InterfaceMessage
{
	enum Type
	{
		OpenWindow,
		CloseWindow,
		SetSelectedEntity,
		HireWorker,
		ChangeGameState,
		ChangeGameSpeed,
		ChangeCameraPos,
		ActivateIllegalActivity,
		DeactivateIllegalActivity,
		SetPenalty
	} MessageType;

	std::string Text;
	Entity* Pointer;
};