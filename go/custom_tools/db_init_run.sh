#!/bin/bash



start_docker() {
	docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Yukon900' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge
}






### Main Script

if [ `docker ps | grep -i "azuresqledge" | wc -l` -gt 0 ]; then
	echo "Container is running"
else
	echo "Container is dead"
	start_docker
fi


echo "compiling go program..."
make db_init_build


echo "running go program"

# ./bin/db_init_cli init --dir ~/Temp/ --project testing
# ./bin/db_init_cli init --dir /tmp --project testing --bare
# ./bin/db_init_cli reset
./bin/db_init_cli test
# ./bin/db_init_cli


