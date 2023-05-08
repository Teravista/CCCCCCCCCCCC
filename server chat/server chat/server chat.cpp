
#include <winsock.h>
#include <iostream>
#include <process.h>
using namespace std;

int socketCounter = 0;
SOCKET socketos[10];
unsigned __stdcall writer(void* data)
{
    SOCKET si = (SOCKET)data;
    char buf[80];
    char buf2[80];
    while (recv(si, buf, 80, 0) > 0)
    {
        if (strcmp(buf, " KONIEC ") == 0)
        {
            closesocket(si);
            WSACleanup();
            return 0;
        }
        for (int i = 0; i < socketCounter; i++)
        {
            if (si != socketos[i]) {
                int dlug = strlen(buf);
                for (int v = 0; v < 80; v++)
                {
                    buf2[v] = buf[v];
                }
                buf2[dlug] = '\0';
                send(socketos[i], buf2, dlug+1, 0);
            }
        }
    };
}
int main()
{
    WSADATA wsas;
    int result;
    WORD wersja;
    wersja = MAKEWORD(1, 1);
    result = WSAStartup(wersja, &wsas);
    //Nastepnie tworzone jest gniazdko za pomoca funkcji socket
    SOCKET s;
    s = socket(AF_INET, SOCK_STREAM, 0);


    struct sockaddr_in sa;
    memset((void*)(&sa), 0, sizeof(sa));
    sa.sin_family = AF_INET;
    sa.sin_port = htons(10000);
    sa.sin_addr.s_addr = htonl(INADDR_ANY);


    result = bind(s, (struct sockaddr FAR*) & sa, sizeof(sa));


    struct sockaddr {
        u_short sa_family;
        char sa_data[14];
    };
    result = listen(s, 5);
    SOCKET si;
    struct sockaddr_in sc;
    int lenc;
    for (;;)
    {
        lenc = sizeof(sc);
        si = accept(s, (SOCKADDR *) & sc, &lenc);
        socketos[socketCounter] = si;
        socketCounter++;
        printf("Client Connected: %i\n",socketCounter);
        unsigned threadID;
        HANDLE hThread = (HANDLE)_beginthreadex(NULL, 0, &writer, (void*)si, 0, &threadID);
    }
    closesocket(s);
    WSACleanup();
    return 0;
}


