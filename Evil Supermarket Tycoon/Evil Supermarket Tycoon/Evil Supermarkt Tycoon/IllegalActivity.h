#pragma once

#include <string>

struct IllegalActivity
{
	std::wstring Name;
	std::wstring Description;
	float PenaltyProbability;
	bool Active;
	bool WasActive;
};