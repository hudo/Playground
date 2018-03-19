Create new persisted volume and start: 

```
docker volume create pgdata
docker-compose -f docker-compose.yml up
```

Inspect files in volume:

```
docker run --rm -it -v=pgdata:/tmp/pgdata busybox
```