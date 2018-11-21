FROM microsoft/mssql-server-linux:2017-latest

ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=Sa.123456
ENV MSSQL_PID=Express

WORKDIR /src

COPY ["./comandos.sql", "./"]
COPY ["./setup-database.sh", "./"]

CMD ./setup-database.sh & /opt/mssql/bin/sqlservr