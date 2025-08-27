#!/bin/bash
docker build -t earnestaxis5546/novasentinel:latest .
docker run -d --name novasentinel -p 8080:8080 \
  -v $(pwd)/config/envoy.yaml:/etc/envoy/envoy.yaml \
  -v $(pwd)/config/redis.conf:/etc/redis/redis.conf \
  -v $(pwd)/lua:/app/lua \
  --cpuset-cpus="0-3" --memory="8g" \
  earnestaxis5546/novasentinel