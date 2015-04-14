#pragma once

#include <string>

class Entity;

struct FeedMessage
{
	Entity* Pointer;
	std::string Message;
};