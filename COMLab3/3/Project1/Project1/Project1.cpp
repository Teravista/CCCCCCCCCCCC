#include<windows.h>
#import "C:\Users\Robert\source\repos\COMLab3\3\Project1\Project1\klasa.tlb" no_namespace

int main() {

	CoInitializeEx(NULL, COINIT_MULTITHREADED);
	IKlasa* s;
	HRESULT rv;
	rv = CoCreateInstance(__uuidof(Klasa), NULL, CLSCTX_ALL, __uuidof(IKlasa), (void**)&s);
	s->Test("Testowanie, zadanie 3 ok!");
	s->Release();
	CoUninitialize();

	return 0;
};
