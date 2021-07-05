## Redis basic

1. 两个维度看Redis
   1. 应用维度：缓存使用、集群应用、数据结构的巧妙使用
   2. 系统维度：三高
      1. 高性能：线程模型、网络IO模型、数据结构、持久化
      2. 高可用：主从复制、集群哨兵、cluster分片集群
      3. 高拓展：负载均衡



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

## Redis cmd

```bash
# get object encoding
object encoding key
```