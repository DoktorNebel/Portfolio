#pragma once

#include <iostream>
#include "DoublyLinkedList.h"

template<class T> class Tree
{
private:

	template<class T> struct Node
	{
		T Element;
		Node<T>* Parent;
		DoublyLinkedList<Node<T>*> Children;
	};

	Node<T>* Root;
	Node<T>* Current;


private:

	Node<T>* FindNode(T object, Node<T>* node)
	{
		if (node->Element == object)
		{
			return node;
		}
		else
		{
			node->Children.ToFirst();
			while (node->Children.HasAccess())
			{
				return this->FindNode(object, node->Children.GetObject());

				node->Children.Next();
			}
		}

		return 0;
	}


public:

	Tree(void)
	{
		this->Root = 0;
	}


	~Tree(void)
	{

	}


	void Add(T object, T parent = 0)
	{
		if (this->Root == 0)
		{
			Node<T>* tmp = new Node<T>();
			tmp->Parent = 0;
			tmp->Element = object;

			this->Root = tmp;
		}
		else if (parent == 0)
		{
			//fuck
		}
		else
		{
			Node<T>* node = this->FindNode(parent, this->Root);

			if (node != 0)
			{
				Node<T>* tmp = new Node<T>();
				tmp->Element = object;
				tmp->Parent = node;

				node->Children.Append(tmp);
			}
		}
	}


	void Delete(T object)
	{
		Node<T>* node = this->FindNode(object);
	}


	void PreOrder(Node<T>* node = 0)
	{
		if (node == 0)
		{
			node = this->Root;
		}

		std::cout << node->Element;

		node->Children.ToFirst();
		while (node->Children.HasAccess())
		{
			this->PreOrder(node->Children.GetObject());

			node->Children.Next();
		}
	}


	/*T GetObject()
	{
		return this->Current->Element;
	}


	void ToRoot()
	{
		this->Current = this->Root;
	}


	void Next()
	{
		
		this->Current->Parent->Children.Next();
		if (this->Current->Parent->Children.HasAccess())
		{
			this->Current = this->Current->Parent->Children.GetObject();
		}
		else
		{

		}
	}*/
};

