#include <stdio.h>
#include <stdlib.h>
#include "common.h"


// FYI, functions "should" be declared above main
// otherwise the compiler will complain
void doWork() {
    for (int i = 0; i < 10; i++) {
        printf("%i Doing Work\n", i);
    }
}


void userQuestions() {
    int stuff[256] = {0};
    int n;

    printf("select a box [0-255]: ");
    scanf("%d", &n);

    printf("You selected %d\n", stuff[n]);
}


int main() {
  printf("%s\n", HELLOWORLD);

  doWork();

  userQuestions();

  return 0;
}


