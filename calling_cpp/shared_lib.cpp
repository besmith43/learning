#define _CRT_SECURE_NO_WARNINGS

#include "shared_lib.h"

void StrCat(const char *str1, const char *str2, char *newstr)
{
    string str = string(str1) + string(str2);
    strcpy(newstr, str.c_str());
}

void StrCpy(char *str1, char *str2)
{
    strcpy(str1, str2);
}

void pipecommand(const char *strCmd)
{
    std::array<char,80> buffer;
    FILE *pipe = _popen(strCmd, "r");
    if (!pipe)
    {
        std::cerr << "cannot open pipe for reading" << endl;
    }

    int c=0;
    while(fgets(buffer.data(), 80, pipe) != NULL)
    {
        c++;
        std::cout << c << "\t" << buffer.data();
    }
    _pclose(pipe);
}

void SaySomething(const char *str)
{
    std::cout << "I typed:" << str << endl;
}

int add (int a, int b)
{
    return a + b;
}