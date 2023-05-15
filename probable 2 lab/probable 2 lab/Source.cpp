#include<cmath>
long long Res(int x) {long long M = pow(2, 31)-1;return (397204094 * x + 0) % M;}
long long numerable(bool fars[]){long long out = 1;for (int i = 0; i < 32; i++)out = out+pow(fars[i] * 2, i);return out;}
int main(){long long results[10] = {Res(15)};for (int i = 0; i < 9; i++)results[i + 1] = Res(results[i]);bool fars[] = {0,0,0,1,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,0};for (int j = 0; j < 10; j++) {for (int i = 31; i > 2; i--)fars[i] = fars[i - 2]!=fars[i - 1];results[j] = numerable(fars);}return 0;}