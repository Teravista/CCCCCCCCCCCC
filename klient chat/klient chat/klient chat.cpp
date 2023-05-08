
#include <iostream>
#include <winsock.h>
#include <process.h>

unsigned __stdcall writer(void* data)
{
    SOCKET si = (SOCKET)data;
    char buf[80];
    printf("got in\n");
    while (recv(si, buf, 80, 0) > 0)
    {
        if (strcmp(buf, " KONIEC ") == 0)
        {
            closesocket(si);
            WSACleanup();
            return 0;
        }
        printf("%s\n", buf);
    };
}

int main(int argc, char* argv[])
{
    SOCKET s;
    struct sockaddr_in sa;
    WSADATA wsas;
    WORD wersja;
    wersja = MAKEWORD(2, 0);
    WSAStartup(wersja, &wsas);
    s = socket(AF_INET, SOCK_STREAM, 0);
    memset((void*)(&sa), 0, sizeof(sa));
    sa.sin_family = AF_INET;
    sa.sin_port = htons(10000);
    sa.sin_addr.s_addr = inet_addr("127.0.0.1");

    int result;
    result = connect(s, (struct sockaddr FAR*) & sa, sizeof(sa));
    if (result == SOCKET_ERROR)
    {
        printf("\ nBlad polaczenia !");
        return 0;
    }
    unsigned threadID;
    HANDLE hThread = (HANDLE)_beginthreadex(NULL, 0, &writer, (void*)s, 0, &threadID);
    int dlug;
    char buf[80];
    for (;;)
    {
        fgets(buf, 80, stdin);
        dlug = strlen(buf); buf[dlug - 1] = '\0';
        send(s, buf, dlug, 0);
        if (strcmp(buf, " KONIEC ") == 0) break;
    }
    closesocket(s);
    WSACleanup();
    return 0;
}

