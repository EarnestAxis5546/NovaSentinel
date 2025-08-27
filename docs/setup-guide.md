# NovaSentinel Setup Guide

## Local Setup
1. Clone: `git clone https://github.com/earnestaxis5546/NovaSentinel.git`
2. Install dependencies (Arch Linux):
   ```bash
   sudo pacman -S dotnet-sdk redis envoy bcc


Configure Redis: Edit /etc/redis/redis.conf.
Configure Envoy: Copy config/envoy.yaml to /etc/envoy/.
Build: dotnet build src/NovaSentinel.sln
Run: dotnet run --project src/NovaSentinel
Enable eBPF:sudo bpftool prog load ebpf/filter.o /sys/fs/bpf/nova_filter
sudo bpftool net attach xdp prog /sys/fs/bpf/nova_filter dev eth0



Docker Setup

Pull: docker pull earnestaxis5546/novasentinel:latest
Run:docker run -d --name novasentinel -p 8080:8080 -v /etc/envoy:/etc/envoy -v /etc/redis:/etc/redis earnestaxis5546/novasentinel



RobustToolbox Integration

Copy src/NovaSentinel to your Space Station 14 project.
Update csproj:<ProjectReference Include="..\NovaSentinel\NovaSentinel.csproj" />


Add middleware to Startup.cs.