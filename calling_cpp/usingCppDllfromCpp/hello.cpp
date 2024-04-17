#include "shared_lib.h"

int main()
{
    string cmd="";
    while(1)
    {
        std:cout <<"Enter command in DOS: ";
        std::getline(std::cin, cmd);
        pipecommand(cmd.c_str());
        SaySomething(cmd.c_str());
    }
    return 0;
}