#!/bin/bash
echo "Simulating 35,000 RPS UDP/TCP traffic..."
hping3 -S -p 80 --flood --rand-source localhost &
hping3 -2 -p 80 --flood --rand-source localhost &
sleep 10
pkill hping3
echo "Test complete. Check logs/novasentinel.log for results."