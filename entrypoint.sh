wait_time=15s
password=Intelitrader2022

echo importing data will start in $wait_time
sleep $wait_time
echo executing script...

/opt/mssql-tools/bin/sqlcmd -S 0.0.0.0 -U sa -P $password -i ./BD/DDL.sql