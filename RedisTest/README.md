## Run Redis with Docker

```bash
# start redis
docker run --name redis-test -d -p 6379:6379 redis:latest

# on windows to interactive run command
docker exec -it redis-test sh # into shell
redis-cli  # then: into redis-cli

set age 11  # then do get and set:
get age  # came back: "11"

# use exit command to escape
exit  # exit form cli
exit # exit form shell

# in vscode, right click the container, attach shell, then:
redis-cli  # now its in the cli
```
