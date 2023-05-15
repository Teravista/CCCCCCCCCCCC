// Numeryczne.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>
#include <algorithm>
#include <iterator>
void kombinacje(int liczby[],int wyswietlone[], int dlugoscLiczb,int aktualnie,int m,int start)
{
	if (aktualnie == m)
	{
		for  ( int i=0;i<m;i++)
		{
			printf("%d ",wyswietlone[i]);
		}
		printf("\n");
	}
	else {
		for (int i = start; i < dlugoscLiczb; i++)
		{
			wyswietlone[aktualnie] = liczby[i];
			kombinacje(liczby, wyswietlone, dlugoscLiczb, aktualnie + 1,m,i+1);
		}
	}
}
void Warjacje(int liczby[], int wyswietlone[], int dlugoscLiczb, int aktualnie, int m, int start)
{
	if (aktualnie == m)
	{
		for (int i = 0; i < m; i++)
		{
			printf("%d ", wyswietlone[i]);
		}
		printf("\n");
	}
	else {
		for (int i = 0; i < dlugoscLiczb; i++)
		{
			wyswietlone[aktualnie] = liczby[i];
			Warjacje(liczby, wyswietlone, dlugoscLiczb, aktualnie + 1, m, i + 1);
		}
	}
}
void Permutacje(int liczby[], int* wyswietlone, int dlugoscLiczb, int aktualnie, int m, int start)
{
	if (aktualnie == m)
	{
		for (int i = 0; i < m; i++)
		{
			printf("%d ", wyswietlone[i]);
		}
		printf("\n");
	}
	else {
		for (int i = 0; i < dlugoscLiczb; i++)
		{
			int powtorzenie = 0;
			for (int j = 0;j < aktualnie; j++)
			{
				if (liczby[i] == wyswietlone[j])
					powtorzenie = 1;
			}
			if (powtorzenie == 0) {
				wyswietlone[aktualnie] = liczby[i];
				Permutacje(liczby, wyswietlone, dlugoscLiczb, aktualnie + 1, m, i);
			}
			
		}
	}
}
int main()
{
	int liczby[] = {1,2,3,4,5};
	int wyswietlone[6] = {};
	//kombinacje(liczby,wyswietlone, 6, 0, 4, 0);
	//Warjacje(liczby, wyswietlone, 5, 0, 5, 0);
	Permutacje(liczby, wyswietlone, 3, 0, 3, 0);
}

// Run program: Ctrl + F5 or Debug > Start Without Debugging menu
// Debug program: F5 or Debug > Start Debugging menu

// Tips for Getting Started: 
//   1. Use the Solution Explorer window to add/manage files
//   2. Use the Team Explorer window to connect to source control
//   3. Use the Output window to see build output and other messages
//   4. Use the Error List window to view errors
//   5. Go to Project > Add New Item to create new code files, or Project > Add Existing Item to add existing code files to the project
//   6. In the future, to open this project again, go to File > Open > Project and select the .sln file
