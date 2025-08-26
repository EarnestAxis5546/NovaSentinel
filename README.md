# NovaSentinel: Multi-Layer Anti-DDoS System

<p align="center">
  <img src="https://img.shields.io/badge/Status-Active%20Development-26A69A?style=plastic&logo=shield" alt="Active Development">
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=plastic&logo=c-sharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Platform-Arch%20Linux-1793D1?style=plastic&logo=archlinux&logoColor=white" alt="Arch Linux">
</p>

## For Managers

NovaSentinel is a robust, open-source Anti-DDoS system designed to protect servers, with a focus on **Space Station 14** (using RobustToolbox in C#) and other high-traffic applications. It provides multi-layer defense (L3/L4/L7) to ensure uptime and performance under attack, leveraging modern technologies like Redis, Envoy, and eBPF. Easy to deploy locally or via Docker, NovaSentinel is optimized for enterprise-grade security and scalability, making it ideal for gaming servers, cloud infrastructure, and critical services.

---

## Overview

NovaSentinel is a high-performance Anti-DDoS system built to safeguard servers from distributed denial-of-service (DDoS) attacks. Tailored for **Space Station 14** (built on RobustToolbox in C#) and adaptable to other applications, it provides comprehensive protection across network (L3/L4) and application (L7) layers. Developed on Arch Linux, NovaSentinel ensures minimal latency, robust security, and scalability for enterprise servers, gaming platforms, and cloud infrastructure.

### Purpose
- Protect servers from DDoS attacks, ensuring uninterrupted service.
- Optimize performance for Space Station 14 and other high-traffic applications.
- Provide a lightweight, scalable, and open-source solution for developers and IT teams.

### Key Features
- **Multi-Layer Defense**: Mitigates L3/L4 (network) and L7 (application) attacks using advanced filtering and rate-limiting.
- **C# Integration**: Seamlessly integrates with RobustToolbox for Space Station 14, leveraging C#’s performance and .NET ecosystem.
- **Real-Time Threat Detection**: Identifies and blocks malicious traffic with minimal latency.
- **Scalability**: Supports enterprise-grade deployments with Redis caching and Envoy proxy.
- **eBPF-Powered Analysis**: Uses eBPF for efficient, kernel-level packet inspection.
- **Flexible Deployment**: Supports local, Docker, and cloud environments.
- **Open Source**: MIT-licensed for community contributions and customization.

---

## Technical Architecture

NovaSentinel employs a multi-layer architecture to counter DDoS attacks at various levels, combining C# middleware, external tools, and kernel-level optimizations.

### L3/L4 Defense (Network Layer)
- **Packet Filtering**: Uses eBPF to inspect and filter malicious traffic at the kernel level, reducing overhead.
- **Rate Limiting**: Implements iptables and Envoy to throttle excessive connections at L3 (IP) and L4 (TCP/UDP).
- **Geo-Blocking**: Filters traffic by region to mitigate large-scale botnets.

### L7 Defense (Application Layer)
- **C# Middleware**: Integrates with RobustToolbox to detect and block application-level attacks (e.g., HTTP floods).
- **Rate Limiting & Challenge-Response**: Uses Redis to track request rates and enforce CAPTCHA-like challenges for suspicious clients.
- **WAF (Web Application Firewall)**: Leverages Envoy’s filtering capabilities to block malicious payloads.

### Key Components
- **C# (RobustToolbox)**: Core logic for Space Station 14 integration, handling game-specific traffic analysis.
- **Redis**: In-memory caching for fast rate-limiting and session tracking.
- **Envoy**: High-performance proxy for load balancing and L7 filtering.
- **eBPF**: Kernel-level packet analysis for low-latency, high-efficiency filtering.
- **Arch Linux**: Optimized development and deployment environment for performance.

### Architecture Diagram

[Client Traffic] → [Envoy Proxy (L7 Filtering)] → [Redis (Rate Limiting)] → [C# Middleware (RobustToolbox)]                   ↓               [eBPF (L3/L4 Filtering)] → [iptables (Rate Limiting)] → [Server]

---

## Installation and Setup

NovaSentinel supports local, Docker, and RobustToolbox-integrated deployments. Below are instructions for each.

### Prerequisites
- **OS**: Arch Linux (recommended) or any Linux distribution
- **Dependencies**:
  - .NET SDK 8.0+ (for C#)
  - Redis 7.0+
  - Envoy 1.31+
  - BCC (for eBPF)
  - Docker (optional)
- **Hardware**: 4GB RAM, 2-core CPU (minimum); 16GB RAM, 8-core CPU (recommended for enterprise)

### Local Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/earnestaxis5546/NovaSentinel.git
   cd NovaSentinel


Install Dependencies (Arch Linux):sudo pacman -S dotnet-sdk redis envoy bcc


Configure Redis:
Start Redis: sudo systemctl start redis
Update redis.conf for your environment (e.g., bind 127.0.0.1).


Configure Envoy:
Copy envoy.yaml from /config to /etc/envoy/.
Adjust listener ports and filters as needed.


Build and Run:dotnet build src/NovaSentinel.sln
dotnet run --project src/NovaSentinel


Enable eBPF:
Load eBPF program: sudo bpftool prog load ebpf/filter.o /sys/fs/bpf/nova_filter
Attach to interface: sudo bpftool net attach xdp prog /sys/fs/bpf/nova_filter dev eth0



Docker Installation

Pull Docker Image (when available):docker pull earnestaxis5546/novasentinel:latest


Run Container:docker run -d --name novasentinel -p 8080:8080 -v /etc/envoy:/etc/envoy -v /etc/redis:/etc/redis earnestaxis5546/novasentinel


Configure Environment:
Mount envoy.yaml and redis.conf to the container.
Set environment variables (e.g., REDIS_HOST=localhost).



RobustToolbox Integration (Space Station 14)

Add NovaSentinel to RobustToolbox:
Copy src/NovaSentinel to your Space Station 14 project’s RobustToolbox directory.
Update csproj to include NovaSentinel:<ItemGroup>
  <ProjectReference Include="..\NovaSentinel\NovaSentinel.csproj" />
</ItemGroup>




Configure Middleware:
Add NovaSentinel middleware to Startup.cs (see Code Examples below).


Test Integration:
Run Space Station 14 with dotnet run and verify DDoS protection logs.




Code Examples
C# Middleware (RobustToolbox)
Integrate NovaSentinel into Space Station 14’s RobustToolbox pipeline to filter L7 traffic.
using NovaSentinel.Filters;
using Robust.Server;

public class Startup
{
    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<DDoSProtectionMiddleware>();
        // Additional RobustToolbox configuration
    }
}

namespace NovaSentinel.Filters
{
    public class DDoSProtectionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RedisClient _redis;

        public DDoSProtectionMiddleware(RequestDelegate next, RedisClient redis)
        {
            _next = next;
            _redis = redis;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            if (await _redis.IsRateLimited(ip))
            {
                context.Response.StatusCode = 429; // Too Many Requests
                return;
            }
            await _next(context);
        }
    }
}

API Endpoint (Rate Limiting)
Expose an API to monitor and configure NovaSentinel’s rate-limiting rules.
using Microsoft.AspNetCore.Mvc;
using NovaSentinel.Services;

[Route("api/novasentinel")]
[ApiController]
public class DDoSController : ControllerBase
{
    private readonly DDoSService _ddosService;

    public DDoSController(DDoSService ddosService)
    {
        _ddosService = ddosService;
    }

    [HttpGet("status")]
    public IActionResult GetStatus()
    {
        var stats = _ddosService.GetTrafficStats();
        return Ok(stats);
    }

    [HttpPost("block/{ip}")]
    public IActionResult BlockIp(string ip)
    {
        _ddosService.BlockIp(ip);
        return Ok($"IP {ip} blocked.");
    }
}


Testing
NovaSentinel includes testing suites for load and lab environments to ensure reliability under attack.
Load Testing

Tool: hping3 or Apache JMeter
Procedure:
Simulate L3/L4 traffic:sudo hping3 -S -p 80 --flood <server_ip>


Monitor logs: tail -f logs/novasentinel.log
Verify blocked IPs in Redis: redis-cli KEYS "blocked:*"


Expected Outcome: Malicious traffic is filtered, legitimate traffic passes with <10ms latency.

Lab Testing

Setup: Use a local Arch Linux VM with Space Station 14 and NovaSentinel.
Procedure:
Run NovaSentinel: dotnet run --project src/NovaSentinel
Simulate HTTP floods using curl or custom scripts.
Check Envoy logs: tail -f /var/log/envoy.log


Expected Outcome: L7 attacks are blocked, and game server remains responsive.


Contributing
We welcome contributions to NovaSentinel! See CONTRIBUTING.md for guidelines on submitting pull requests, reporting issues, and following our C# coding style.
Contacts

Author: Earnest Riivitse (EarnestAxis5546)
Email: wolkapoika@gmail.com
X: earnestaxis5546
Issues: GitHub Issues


  



  "Defend with code, thrive with resilience." – Earnest Riivitse

https://capsule-render.vercel.app/api?type=waving&color=gradient&height=80&section=footer&text=NovaSentinel&fontSize=20&fontColor=FFFFFF&animation=scaleIn
