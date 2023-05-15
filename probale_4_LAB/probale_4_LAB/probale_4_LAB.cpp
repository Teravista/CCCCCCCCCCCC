// probale_4_LAB.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <iostream>

int main()
{
    std::cout << "Hello World!\n";
    srand(time(NULL));
    int liczby[5][5] = {(0,0,0,0,0),(0,0,0,0,0),(0,0,0,0,0),(0,0,0,0,0),(0,0,0,0,0)};
    float num = 0.0;
    for (int i = 0; i < 1000000; i++)
    {
        int x;
        int y;
        num = (float)(rand()) / (float)(RAND_MAX);
        if (num < 0.5)//x=1 0.5
        {
            x = 1;
            num = (float)(rand()) / (float)(RAND_MAX);
            if (num < (0.4 / 0.5))//y=3 P=0.4
            {
                y = 3;
            }
            else {//y=4 P=0.1
                y = 4;
            }
        }
        else if (num < 0.65) {//x=2 P=0.15
            x = 2;
            y = 4;
        }
        else if (num < 0.85)//x=3 P=0.2
        {
            x = 3;
            y = 1;
        }
        else //x=4 P=0.15
        {
            x = 4;
            num = (float)(rand()) / (float)(RAND_MAX);
            if (num < (0.1 / 0.15))
            {
                y = 1;
            }
            else {
                y = 3;
            }
        }
        liczby[x][y]++;
    }
    for (int x = 0; x < 4; x++)
    {
        for (int y = 0; y < 4; y++)
        {
            printf("[%i][%i]= %i,",x+1,y+1, liczby[x+1][y+1]);
        }
        printf("\n");
    }
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
