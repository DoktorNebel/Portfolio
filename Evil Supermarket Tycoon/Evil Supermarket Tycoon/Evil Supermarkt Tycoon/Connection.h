#pragma once

struct Connection
{
	enum Type
	{
		Message,
		Job,
		StoreObject,
		Customer,
		Worker
	} ConnectionType;
	unsigned int FirstID;
	unsigned int SecondID;
};