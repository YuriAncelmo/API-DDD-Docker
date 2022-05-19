docker image pull mysql:5.7
echo container_id | docker run -e MYSQL_ALLOW_EMPTY_PASSWORD=yes -v mysql:5.7
echo %container_id%
set container_ip=docker inspect -f '{{range.NetworkSettings.Networks}}{{.IPAddress}}{{end}}' container_id
echo %container_ip%
set /p DUMMY=Hit ENTER to continue...


