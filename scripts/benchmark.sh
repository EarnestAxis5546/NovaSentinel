#!/bin/bash
echo "Running NovaSentinel benchmark for 35,000 RPS..."
docker-compose up -d
sleep 5
bash benchmarks/high_rps_test.sh
dotnet run --project src/NovaSentinel -c Release -- --benchmark
tail -f logs/novasentinel.log