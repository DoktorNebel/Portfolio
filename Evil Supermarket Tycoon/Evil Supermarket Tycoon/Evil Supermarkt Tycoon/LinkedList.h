#pragma once


namespace Framework {
	namespace Core {

		template<class T>
		class LinkedList
		{
		private:

			template<class T>
			struct Node
			{
				T Object;
				Node<T>* Next;
			};

			Node<T>* First;
			Node<T>* Last;
			Node<T>* Current;
			int Size;

		public:

			LinkedList()
			{
				this->First = 0;
				this->Last = 0;
				this->Current = 0;
				this->Size = 0;
			}


			~LinkedList()
			{
				this->ToFirst();

				while (this->HasAccess())
				{
					this->Remove();
				}
			}

			void Next()
			{
				if (this->Current != 0)
				{
					this->Current = this->Current->Next;
				}
			}


			void ToFirst()
			{
				this->Current = this->First;
			}


			bool HasAccess()
			{
				return (this->Current != 0);
			}


			void Append(T object)
			{
				if (this->Size == 0)
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = 0;
					this->First = tmp;
					this->Last = tmp;
				}
				else
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = 0;
					this->Last->Next = tmp;
					this->Last = tmp;
				}
				this->Size++;
			}


			void Insert(T object)
			{
				if (this->Size == 0)
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = 0;
					this->First = tmp;
					this->Last = tmp;
					this->Size++;
				}

				if (this->Current != 0)
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = this->Current;
					if (this->Current == this->First)
					{
						this->First = tmp;
					}
					else
					{
						this->ToFirst();
						while (this->Current->Next != tmp->Next)
						{
							this->Next();
						}
						this->Current->Next = tmp;
					}
					this->Size++;
				}
			}


			void Remove()
			{
				if (this->Current != 0)
				{
					if (this->Current == this->First)
					{
						this->First = this->Current->Next;
						if (this->First == 0)
						{
							this->Last = 0;
						}
						delete this->Current;
						this->Current = this->First;
					}
					else
					{
						Node<T>* tmp = this->First;
						while (tmp->Next != this->Current)
						{
							tmp = tmp->Next;
						}
						if (this->Current == this->Last)
						{
							this->Last = tmp;
							tmp->Next = 0;
						}
						else
						{
							tmp->Next = this->Current->Next;
						}
						delete this->Current;
						this->Current = tmp->Next;
					}
					this->Size--;
				}
			}


			T GetObject()
			{
				return this->Current->Object;
			}


			int GetSize()
			{
				return this->Size;
			}
		};

	}
}