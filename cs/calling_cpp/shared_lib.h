#ifndef SHARED_LIB_H
#define SHARED_LIB_H

#include <stdio.h>
#include <string>
#include <iostream>
#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <array>

using namespace std;

#ifdef __cplusplus
    extern "C" {
#endif

#ifdef BUILD_MY_DLL
    #define SHARED_LIB __declspec(dllexport)
#else
    #define SHARED_LIB __declspec(dllimport)
#endif

void SHARED_LIB StrCat(const char *str1, const char *str2, char *newstr);
void SHARED_LIB StrCpy(char *str1, char *str2);
int SHARED_LIB add(int a, int b);
void SHARED_LIB pipecommand(const char *strCmd);
void SHARED_LIB SaySomething(const char *str);

#ifdef __cplusplus
    }
#endif

#endif //end of DLL