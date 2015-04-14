#pragma once


		template<class T>
		class DoublyLinkedList
		{
		private:

			template<class T>
			struct Node
			{
				T Object;
				Node<T>* Next;
				Node<T>* Previous;
			};

			Node<T>* First;
			Node<T>* Last;
			Node<T>* Current;
			int Size;

		public:

			DoublyLinkedList()
			{
				this->First = 0;
				this->Last = 0;
				this->Current = 0;
				this->Size = 0;
			}


			~DoublyLinkedList()
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


			void Prrevious()
			{
				if (this->Current != 0)
				{
					this->Current = this->Current->Previous;
				}
			}


			void ToFirst()
			{
				this->Current = this->First;
			}


			void ToLast()
			{
				this->Current = this->Last;
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
					tmp->Previous = 0;
					this->First = tmp;
					this->Last = tmp;
				}
				else
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = 0;
					tmp->Previous = this->Last;
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
					tmp->Previous = 0;
					this->First = tmp;
					this->Last = tmp;
					this->Size++;
				}
				else if (this->Current != 0)
				{
					Node<T>* tmp = new Node<T>();
					tmp->Object = object;
					tmp->Next = this->Current;
					tmp->Previous = this->Current->Previous;
					if (this->Current == this->First)
					{
						this->First = tmp;
					}
					else
					{
						tmp->Previous->Next = tmp;
						this->Current->Previous = tmp;
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
						this->Current->Previous = 0;
					}
					else
					{
						Node<T>* tmp = this->First;
						tmp = this->Current->Previous;
						if (this->Current == this->Last)
						{
							this->Last = tmp;
							tmp->Next = 0;
						}
						else
						{
							tmp->Next = this->Current->Next;
							this->Current->Next->Previous = tmp;
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
