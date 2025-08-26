<p align="center">
  <img src="https://readme-typing-svg.herokuapp.com?font=JetBrains+Mono&size=30&duration=1000&pause=50&color=FF2D55&center=true&vCenter=true&width=700&lines=NovaSentinel+-+Anti-DDoS+Fortress;Built+for+Servers;Powered+by+C%23+and+Arch+Linux" alt="Typing SVG">
</p>

<h1 align="center"> NovaSentinel: Multi-Layer Anti-DDoS System </h1>

<p align="center">
  <img src="https://img.shields.io/badge/Status-Active%20Development-FF2D55?style=plastic&logo=shield" alt="Active Development">
  <img src="https://img.shields.io/badge/Language-C%23-239120?style=plastic&logo=c-sharp&logoColor=white" alt="C#">
  <img src="https://img.shields.io/badge/Platform-Arch%20Linux-1793D1?style=plastic&logo=archlinux&logoColor=white" alt="Arch Linux">
  <img src="https://img.shields.io/badge/%F0%9F%9B%A0%EF%B8%8F-Security+Fortified-FF2D55?style=plastic&labelColor=1A1A1A" alt="Security Fortified">
</p>


## For Managers

NovaSentinel is a powerful, open-source Anti-DDoS system designed to protect **Space Station 14** (using RobustToolbox in C#) and other high-traffic applications from distributed denial-of-service attacks. With multi-layer (L3/L4/L7) defense, it ensures uptime and performance using cutting-edge tools like Redis, Envoy, and eBPF. Easy to deploy locally or via Docker, NovaSentinel is scalable, secure, and ideal for gaming servers, cloud infrastructure, and critical services.

---

## Overview

NovaSentinel is a state-of-the-art Anti-DDoS system built to safeguard servers from distributed denial-of-service attacks. Tailored for **Space Station 14** (using RobustToolbox in C#) and adaptable to other applications, it provides robust protection across network (L3/L4) and application (L7) layers. Developed on Arch Linux, NovaSentinel combines minimal latency, enterprise-grade security, and open-source flexibility for gaming platforms, data centers, and cloud environments.

### Purpose
- Shield servers from DDoS attacks, ensuring uninterrupted service.
- Optimize performance for Space Station 14 and other high-traffic applications.
- Deliver a lightweight, scalable, and community-driven security solution.

### Key Features
- **Multi-Layer Defense**: Blocks L3/L4 (network) and L7 (application) attacks with advanced filtering and rate-limiting.
- **C# Integration**: Seamlessly integrates with RobustToolbox for Space Station 14, leveraging C#’s .NET ecosystem.
- **Real-Time Threat Detection**: Identifies malicious traffic with sub-10ms response times.
- **Scalable Architecture**: Uses Redis for caching and Envoy for proxying to handle high-traffic loads.
- **eBPF-Powered Analysis**: Leverages kernel-level packet inspection for efficiency.
- **Flexible Deployment**: Supports local, Docker, and cloud setups.
- **Open Source**: MIT-licensed for community contributions and customization.

---

## Technical Architecture

NovaSentinel employs a multi-layer architecture to counter DDoS attacks, combining C# middleware, external tools, and kernel-level optimizations for comprehensive protection.

### L3/L4 Defense (Network Layer)
- **Packet Filtering**: Uses eBPF for kernel-level inspection and filtering of malicious traffic.
- **Rate Limiting**: Implements iptables and Envoy to throttle excessive connections at L3 (IP) and L4 (TCP/UDP).
- **Geo-Blocking**: Filters traffic by region to mitigate botnet-driven attacks.

### L7 Defense (Application Layer)
- **C# Middleware**: Integrates with RobustToolbox to detect and block HTTP floods and other application-level attacks.
- **Rate Limiting & Challenges**: Uses Redis for request tracking and CAPTCHA-like challenges for suspicious clients.
- **WAF (Web Application Firewall)**: Employs Envoy’s filtering to block malicious payloads.

### Key Components
- **C# (RobustToolbox)**: Core logic for Space Station 14, handling game-specific traffic analysis.
- **Redis**: In-memory caching for rapid rate-limiting and session tracking.
- **Envoy**: High-performance proxy for load balancing and L7 filtering.
- **eBPF**: Kernel-level packet analysis for low-latency filtering.
- **Arch Linux**: Optimized environment for development and deployment.

### Architecture Diagram

[Client Traffic] → [Envoy Proxy (L7 Filtering)] → [Redis (Rate Limiting)] → [C# Middleware (RobustToolbox)]                   ↓               [eBPF (L3/L4 Filtering)] → [iptables (Rate Limiting)] → [Server]

---

## Installation and Setup

NovaSentinel supports local, Docker, and RobustToolbox-integrated deployments. Below are detailed instructions.

### Prerequisites
- **OS**: Arch Linux (recommended) or any Linux distribution
- **Dependencies**:
  - .NET SDK 8.0+
  - Redis 7.0+
  - Envoy 1.31+
  - BCC (for eBPF)
  - Docker (optional)
- **Hardware**: 4GB RAM, 2-core CPU (minimum); 16GB RAM, 8-core CPU (recommended)

### Local Installation
1. **Clone the Repository**:
   ```bash
   git clone https://github.com/earnestaxis5546/NovaSentinel.git
   cd NovaSentinel


Install Dependencies (Arch Linux):sudo pacman -S dotnet-sdk redis envoy bcc


Configure Redis:
Start Redis: sudo systemctl start redis
Edit /etc/redis/redis.conf (e.g., set bind 127.0.0.1).


Configure Envoy:
Copy config/envoy.yaml to /etc/envoy/.
Adjust ports and filters as needed.


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
Update csproj:<ItemGroup>
  <ProjectReference Include="..\NovaSentinel\NovaSentinel.csproj" />
</ItemGroup>




Configure Middleware:
Add NovaSentinel middleware to Startup.cs (see Code Examples).


Test Integration:
Run Space Station 14: dotnet run
Verify DDoS protection logs in logs/novasentinel.log.




Code Examples
C# Middleware (RobustToolbox)
Integrate NovaSentinel into Space Station 14’s RobustToolbox pipeline for L7 protection.
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
                await context.Response.WriteAsync("Rate limit exceeded.");
                return;
            }
            await _next(context);
        }
    }
}

API Endpoint (Rate Limiting)
Expose an API to monitor and manage NovaSentinel’s rate-limiting rules.
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
        return Ok($"IP {ip} blocked successfully.");
    }
}


Testing
NovaSentinel includes testing suites for load and lab environments to ensure robust protection.
Load Testing

Tool: hping3 or Apache JMeter
Procedure:
Simulate L3/L4 traffic:sudo hping3 -S -p 80 --flood <server_ip>


Monitor logs: tail -f logs/novasentinel.log
Verify blocked IPs: redis-cli KEYS "blocked:*"


Expected Outcome: Malicious traffic is filtered, legitimate traffic passes with <10ms latency.

Lab Testing

Setup: Use an Arch Linux VM with Space Station 14 and NovaSentinel.
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

```
  "Defend with code, thrive with resilience." – Earnest Riivitse


