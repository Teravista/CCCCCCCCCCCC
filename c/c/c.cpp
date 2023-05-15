#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <time.h>
#include <pthread.h>

pthread_mutex_t mutx;

struct ListaOfList* rootE = 0;
typedef struct Dane {
	void* wartoscDana;
	int iloscBajtow;
}Dane;
typedef struct ListaOfList {
	struct ListaOfList* right;
	struct ListElement* down;
	//int Haszowanko(struct Dane* element,int sizeOfList)
	int (*hashRef)(struct Dane*, int);
	int index;
}ListaOfList;
typedef struct ListElement {
	struct ListElement* down;
	struct Dane* dana;
}ListElement;
void func(int (*f)(int, int));
void DeleteMainList(struct ListaOfList* rootElement);
void Deletor(struct ListaOfList* rootElement);
void Indexator(struct ListaOfList* rootElement);
int Haszowanko(struct Dane* element, int sizeOfList);
void AddElement(struct ListaOfList* rootElement, int sizeOfList, struct Dane* element);
struct ListaOfList* initialzeHash(int (*hashRef)(struct Dane*, int));
void func(int (*f)(int, int))
{
	struct ListElement* newELemelent = malloc(sizeof(struct ListElement));
	newELemelent->dana = 0;
	newELemelent->down = 0;
}

void DeleteMainList(struct ListaOfList* rootElement)
{
	if (rootElement->right != 0)
	{

		struct ListaOfList* tempIterator = rootElement;
		struct ListaOfList* toDelete = rootElement;
		while (tempIterator->right != 0)
		{
			toDelete = tempIterator;
			tempIterator = tempIterator->right;
			free(toDelete);
		}
		free(tempIterator);

	}

}
void Deletor(struct ListaOfList* rootElement)
{
	struct ListaOfList* curList = rootElement;
	while (curList->right != 0)
	{
		if (curList->down != 0)
		{

			struct ListElement* tempIterator = curList->down;
			struct ListElement* toDelete = curList->down;
			while (tempIterator->down != 0)
			{
				toDelete = tempIterator;
				tempIterator = tempIterator->down;

				free(toDelete->dana);
				free(toDelete);
			}
			free(tempIterator->dana);
			free(tempIterator);

		}
		curList->down = 0;
		curList = curList->right;
	}
	if (curList->down != 0)
	{

		struct ListElement* tempIterator = curList->down;
		struct ListElement* toDelete = curList->down;
		while (tempIterator->down != 0)
		{
			toDelete = tempIterator;
			tempIterator = tempIterator->down;
			free(toDelete->dana);
			free(toDelete);
		}
		free(tempIterator->dana);
		free(tempIterator);

	}
	curList->down = 0;
	curList = rootElement;
	DeleteMainList(curList);
}
void Indexator(struct ListaOfList* rootElement)
{
	struct ListaOfList* curList = rootElement;
	while (curList->right != 0)
	{

		if (curList->down != 0)
		{
			printf("%d. ", curList->index);
			struct Dane* element;
			struct ListElement* cur = curList->down;
			while (cur->down != 0)
			{
				element = cur->dana;
				int* wartosc = element->wartoscDana;
				printf("%d ", *wartosc);
				cur = cur->down;
			}
			element = cur->dana;
			int* wartosc = element->wartoscDana;
			printf("%d", *wartosc);
			cur = cur->down;
		}
		curList = curList->right;
		printf("\n");
	}
	if (curList->down != 0)
	{
		printf("%d. ", curList->index);
		struct Dane* element;
		struct ListElement* cur = curList->down;
		while (cur->down != 0)
		{
			element = cur->dana;
			int* wartosc = element->wartoscDana;
			printf("%d ", *wartosc);
			cur = cur->down;
		}
		element = cur->dana;
		int* wartosc = element->wartoscDana;
		printf("%d", *wartosc);
		cur = cur->down;
	}
	curList = curList->right;
	printf("\n");
}
int Haszowanko(struct Dane* element, int sizeOfList)
{
	int vartosc = 0;
	char* pointer = element->wartoscDana;
	for (int i = 0; i < element->iloscBajtow; i++) {
		vartosc += *(pointer + i);
	}
	return abs((12 * vartosc) % 10);
}
void AddElement(struct ListaOfList* rootElement, int sizeOfList, struct Dane* element)
{
	int index = rootElement->hashRef(element, sizeOfList);
	struct ListaOfList* curList = rootElement;
	while (curList->index != index)
	{
		struct ListaOfList* tempList = 0;
		int nullLista = 0;
		if (curList->right == 0)
		{
			nullLista = 1;
		}
		else
			tempList = curList->right;
		if (nullLista == 1 || tempList->index > index)
		{
			struct ListaOfList* newList = malloc(sizeof(struct ListaOfList));
			newList->hashRef = rootElement->hashRef;
			newList->right = curList->right;
			curList->right = newList;
			newList->down = 0;
			newList->index = index;
		}
		curList = curList->right;
	}
	if (curList->down != 0) {
		struct ListElement* curElem = curList->down;
		while (curElem->down != 0)
		{
			curElem = curElem->down;
		}
		struct ListElement* newELemelent = malloc(sizeof(struct ListElement));
		curElem->down = newELemelent;
		newELemelent->dana = element;
		newELemelent->down = 0;
	}
	else {
		struct ListElement* newELemelent = malloc(sizeof(struct ListElement));
		curList->down = newELemelent;
		newELemelent->dana = element;
		newELemelent->down = 0;
	}
}
struct ListaOfList* initialzeHash(int (*hashRef)(struct Dane*, int))
{
	struct ListaOfList* rootE = malloc(sizeof(struct ListaOfList));
	rootE->right = 0;
	rootE->down = 0;
	rootE->index = 0;
	rootE->hashRef = hashRef;
	return rootE;

}
int Deleteeelement(struct ListaOfList* rootElement, int sizeOfList, struct Dane* element)
{
	int index = rootElement->hashRef(element, sizeOfList);
	struct ListaOfList* curList = rootElement;
	while (curList->index != index)
	{
		struct ListaOfList* tempList = 0;
		int nullLista = 0;
		if (curList->right == 0)
		{
			nullLista = 1;
		}
		else
			tempList = curList->right;
		if (nullLista == 1 || tempList->index > index)
		{

			break;
		}
		curList = curList->right;
	}
	if (curList->down != 0) {
		struct ListElement* curElem = curList->down;
		struct ListElement* tempel = 0;;
		while (curElem->down != 0)
		{
			tempel = curElem;
			curElem = curElem->down;
		}
		if (tempel != 0)
		{
			tempel->down = 0;

		}
		else {
			curList->down = 0;
		}
		//free(curElem->dana);
		free(curElem);
		return -1;
	}
	else {

	}
	return 0;
}
void* threadDod(void* i)
{
	int c = 1000;
	while (c > 0)
	{

		int* liczba = ((int*)i);
		//printf("%d",liczba);
		struct Dane* dana11 = malloc(sizeof(struct Dane));
		dana11->iloscBajtow = sizeof(int);
		dana11->wartoscDana = liczba;
		pthread_mutex_lock(&mutx);
		AddElement(rootE, 10, dana11);
		c--;
		pthread_mutex_unlock(&mutx);

	}
}
void* threadDel(void* i)
{
	int c = 1000;
	while (c > 0)
	{

		int* liczba = ((int*)i);
		struct Dane* dana1 = malloc(sizeof(struct Dane));
		dana1->iloscBajtow = sizeof(int);
		dana1->wartoscDana = liczba;
		pthread_mutex_lock(&mutx);
		c = c + Deleteeelement(rootE, 10, dana1);
		pthread_mutex_unlock(&mutx);

	}
}
int main(int argc, char** argv) {
	int size = 10;
	rootE = initialzeHash(Haszowanko);
	int a = 1034 * 2048, b = 2049;
	int c = 50;
	int gcc = 50;
	int g = 990; int o = 509090;
	int oo = 509090;



	pthread_t add1, add2, add3, add4, add5;
	pthread_t del1, del2, del3, del4, del5;
	int num1 = 1, num2 = 2, num3 = 3, num4 = 4, num5 = 7;
	pthread_mutex_init(&mutx, NULL);

	pthread_create(&add1, NULL, threadDod, &num1);
	pthread_create(&add2, NULL, threadDod, &num2);
	pthread_create(&add3, NULL, threadDod, &num3);
	pthread_create(&add4, NULL, threadDod, &num4);
	pthread_create(&add5, NULL, threadDod, &num5);



	pthread_create(&del1, NULL, threadDel, &num1);
	pthread_create(&del2, NULL, threadDel, &num2);
	pthread_create(&del3, NULL, threadDel, &num3);
	pthread_create(&del4, NULL, threadDel, &num4);
	pthread_create(&del5, NULL, threadDel, &num5);

	pthread_join(add1, NULL);
	pthread_join(add2, NULL);
	pthread_join(add3, NULL);
	pthread_join(add4, NULL);
	pthread_join(add5, NULL);

	pthread_join(del1, NULL);
	pthread_join(del2, NULL);
	pthread_join(del3, NULL);
	pthread_join(del4, NULL);
	pthread_join(del5, NULL);



	struct Dane* dana4 = malloc(sizeof(struct Dane));
	dana4->iloscBajtow = sizeof(int);
	dana4->wartoscDana = &gcc;
	//AddElement(rootE, size, dana4);

	//Indexator(rootE);
	Deletor(rootE);
	return 0;
}
