# Practice-docker
I will cover this application docker everything how to build, create image and container. docker volume, network, docker compose file. 

# Docker own image build command:
-> right click project file and open powershel
cd src
docker build -f .\DockerTutorial.API\Dockerfile -t dotnetcore-image .

## docker and git command:
----------------------------------------
Net start com.docker.service   > docker windows service start.Run this command administrator mode.
----------sql connection:-----port, password and user you are define when create container------------------
server: localhost,49433
user: sa
password: your password
-----------postgres sql------port,password and user you are define when create container----------------------
name: localhost
port:5432
user: admin

docker log containerId			--showing all log for this container if error or success.
docker images ls			--show docker all images.
docker ps 				--show only running container list.
docker ps -a 				--showing all active and inactive container.
docker start containerId 		-- start the container. 
docker restart containerId		-- ReStart the container. 
docker rm containerId          	 	-- remove container. 
docker rm -f containerName 		-- remove any running container.
docker run -d --name my-nginx -p 8080:80 nginx		
	-- run nginx image and open a container using this image. 
	-- -d means run on background. detatch mode.
	-- --name means container name. otherwise set default anonomous name.
	-- -p means port. 8080 is localhost port and 80 is inside the container port. 
docker container stop ContainerId	-- Stop the container.

docker volume create volumeName 	-- create a docker volume. 
docker volume ls			-- list docker volume.
docker volume inspect volumeName 	-- check where store the data sql server in windowns mechine. 
->if we don't use volume when build a container then we lost our data base & data when we remove our container. 

docker network create NetworkName
docker network ls			--showing list of networks.
docker inspect natworkName		--details checks of network. 
docker network rm networkName		--removed network if not associate any container.
docker rm -f asociateContainerName	--removed network if associate any container.

docker exec -it my-redis /bin/bash  	-- monitor redis container and we can modify container.
root@ee55e4f29489:/data# redis-cli
127.0.0.1:6379> monitor
ok
127.0.0.1:6379> flushall		-- remove all data from resis cash.
127.0.0.1:6379> exit			-- exits 2 time back to c:users

SQL server docker image pull:
----------------------------------------
docker pull mcr.microsoft.com/mssql/server:2022-latest

sQL Server Docker container
----------------------------------------
docker run \
-e "ACCEPT_EULA=Y" \
-e "MSSQL_SA_PASSWORD=Admin@123" \
-p 1499:1433 \
-d \
-v sql_data:/var/opt/mssql \
--name ms-sql-server \
mcr.microsoft.com/mssql/server:2022-latest

----------------------redus image---------------------------------
docker run \
--name my-redis \
-p 6379:6379 \
-d \
redis

----------------------nginx server image---------------------------------
docker run \
--name my-nginx-server \
-p 8081:80 \
-d \
-v my-nginx-data:/usr/ssare/nginx/html \
-network my-network1 \
nginx


----------------------docker compose command:----------------------------
docker-compose up -d
docker-compose down	-> if down command all container are remove and data loss
docker-compose stop	-> just stop not loss data.
docker compose file .yml
-----------------------
version: '3.3'

services:
  mssql:
    container_name: sql-server
    image: mcr.microsoft.com/mssql/server:2017-latest
    #image: mcr.microsoft.com/mssql/server:2017-CU11-ubuntu
    restart: always
    environment:
      ACCEPT_EULA: "Y"
      SA_PASSWORD: "Contraseña12345678"
    ports:
      - 1433:1433
    volumes:
      - my-volume:/var/opt/mssql
      
volumes:
  my-volume:

-------------------------------Docker setup for .net6 app------------------
docker network create my-postgres-net	-> create network.

postgres server image setup:
----------------------
docker run --name my-postgress -p 5432:5432 \
	--net my-postgres-net \
	-v my-postgress-data:/var/lib/postgresql/data \
	-e POSTGRES_PASSWORD=rasel001 -d postgres

pgAdmin setup: Management tool.
-------------------------------
docker run --name my-postgres-admin \
    -p 9080:80 \
    --net my-postgres-net \
    -e 'PGADMIN_DEFAULT_EMAIL=user@domain.com' \
    -e 'PGADMIN_DEFAULT_PASSWORD=rasel001' \
    -d dpage/pgadmin4


------------------------------------git command--------------------------------------------------------------
At first login git hub
create repository
copy below command and past git base cli to push your code to git repository.
git remote add origin <REPO URL>

branch manage command:
--------------------------------------------------
git branch newbranchName -> create but stay on current
git checkout -b branchName
git checkout master	  -> master is checked out
git pull		  -> update local for master.
git merge new-feature	  -> merge branch new-feature into master

git commit and push command:
------------------------------------------------------
git init
git log
git status
git add .
git commit -m "my first commit"
git push origin branchName.

git ignore file create for specific platform and ide:
-----------------------------------------------------
https://www.toptal.com/developers/gitignore

gir rebase command:
----------------------------------------
[In normal scnario first we pull the code then push but someone update same code then a new system generated
commit message create.if we avoid this system generated message then come git rebase.]

1.if conflict : when-> 
git push origin branchName -> if try to push but not push if update same file some code someone then below command
git pull origin branchName -r -> we try always must code pull with rebase command.
git status
git push origin branchName

2. if conflict :
resolve the conflict and after that (some code edit).
git add .
git rebase --continue  --no need new commit after the resolved conflict. code push with same commit.
git push origin branchName

git amend command:
---------------------------------------------------
if we change some code and commit that not push. if we realise something messing that commit.
so write some code. then came to amend command. both change marge with previous commit message.

git commit --amend -> edit the commit message
press esc key -> then
wq   ->write and quite.
git status
git push

git soft reset:
--------------------------------------------------------------
scnario: 
write some code and commit it not push yet, after that time you build code
but some error arise, now you want to remove this commit. 

git log
git reset --soft HEAD~1  ->Delete last one commit but code will come staging(local) area. you can again modify code and commit.
git status  

git hard reset:
--------------------------------------------------------------
scnario: when we need to delete commit permanantly then need hard reset command.
git reset --hard HEAD~1 ->Delete last one commit with code permanantly.

NB: Hard and Soft reset command only use when your code in local repository
not push your code on server. when code local repository only this time
use hard and soft reset command



