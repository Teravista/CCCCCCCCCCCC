// 2SUM_C.cpp : This file contains the 'main' function. Program execution begins and ends there.
#define INDEXER(x) ((x + 1000002161) % 20007)
#define MAPSIZE 20007
#define PRIMEOFFSET 1000002161

#include <stdlib.h>
#include <stdio.h>
#include <math.h>

int* twoSum(int* nums, int numsSize, int target, int* returnSize) {

    for (int x = 0; x < numsSize; x++) {
        for (int y = x+1; y < numsSize; y++)
        {
            if (nums[x] + nums[y] == target)
            {
                int* p = (int*)malloc(sizeof(int) * 2);
                if (p == 0)
                {
                    return 0;
                }
                p[0] = x;
                p[1] = y;
                return p;
            }
        }
    }
    return 0;
}

int indexer(int i)
{
    return (i + 1000002161) % 20007;
}

int* twoSum2(int* nums, int numsSize, int target, int* returnSize) {
    //we creatin a map
    int* map = calloc(20007, sizeof(int));
    if (map == 0)
    {
        return -1;
    }

    for (int i = 0; i < numsSize; i++)
    {
        int index_x = indexer(nums[i]);
        map[index_x] = i + 1;
    }


    for (int i = 0; i < numsSize; i++)
    {
        int t = target - nums[i];
        int index_x = indexer(t);
        if (map[index_x] != 0 && map[index_x] - 1 != i)
        {
            int* ret = (int*)malloc(sizeof(int) * 2);
            if (ret == 0)
            {
                return -1;
            }
            ret[0] = i;
            ret[1] = map[index_x] - 1;
            *returnSize = 2;
            return ret;
        }
    }
    free(map);
    return 0;
}


int main()
{
    int nums[] = {2, 5, 5, 11};
    int returnSize = 0;
    int* p = twoSum2(&nums,4,10, &returnSize);
    printf("1=: %d\n", p[0]);
    printf("2=: %d",p[1]);
    free(p);
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
