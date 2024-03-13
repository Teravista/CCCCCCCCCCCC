// ConsoleApplication1.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

//#include "stdafx.h"
#include "net.h"
#include <windows.h>
#include <math.h>
#include <time.h>
#include "objects.h"

unicast_net* uni_recv;         // wsk do obiektu zajmujacego sie odbiorem komunikatow
unicast_net* uni_send;          //   -||-  wysylaniem komunikatow
unsigned long IP_sender[2];
int cAmount = 0;

struct Frame                                       // The main structure of net communication between aplications. Homogenous for simpicity.
{
	int type;                                      // frame type  
	int iID;                                       // object identifier 
	ObjectState state;                             // object state values (see object module)
};

void Init()
{
	uni_recv = new unicast_net(10001);      // obiekt do odbioru ramek sieciowych
	uni_send = new unicast_net(10002);       // obiekt do wysy³ania ramek
}

int main()
{
	Init();

	while (true)
	{
		Frame frame;

		unsigned long IP;
		//char* buffer;
		short size;
		uni_recv->reciv((char*)&frame, &IP, sizeof(Frame));
		//printf("Rzla");
		bool added = false;
		for (int i = 0; i < cAmount; i++) {
			if (IP_sender[i] == IP) {
				added = true;
				break;
			}
		}
		if (!added)
			IP_sender[cAmount++] = IP;
		for (int i = 0; i < cAmount; i++) {
			if (IP_sender[i] != IP) {
				uni_send->send((char*)&frame, IP_sender[i], sizeof(Frame));
			}
		}


	}

	return 0;
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
