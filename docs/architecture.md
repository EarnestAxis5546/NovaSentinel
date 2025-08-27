# NovaSentinel Architecture

## Overview

NovaSentinel is a multi-layer Anti-DDoS system designed for Space Station 14 (RobustToolbox, C#) and other applications. It combines L3/L4 (network) and L7 (application) defenses using C# middleware, Redis, Envoy, and eBPF.

## Layers

### L3/L4 (Network)
- **eBPF**: Kernel-level packet filtering for low-latency IP/TCP/UDP analysis.
- **iptables**: Rate-limiting for excessive connections.
- **Geo-Blocking**: Filters traffic by region.

### L7 (Application)
- **C# Middleware**: Integrates with RobustToolbox to block HTTP floods.
- **Redis**: Tracks request rates and sessions.
- **Envoy**: Provides WAF and load balancing.

## Diagram

[Client Traffic] → [Envoy Proxy (L7)] → [Redis (Rate Limiting)] → [C# Middleware]                   ↓               [eBPF (L3/L4)] → [iptables] → [Server]

## Key Components
- **C# (RobustToolbox)**: Game-specific traffic analysis.
- **Redis**: In-memory caching for speed.
- **Envoy**: High-performance proxy.
- **eBPF**: Kernel-level efficiency.
- **Arch Linux**: Optimized environment.
