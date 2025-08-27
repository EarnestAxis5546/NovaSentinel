<p align="center">
  <img src="https://img.shields.io/badge/Contribute-to+NovaSentinel-FF3D7F?style=plastic&logo=git" alt="Contribute to NovaSentinel">
</p>

# Contributing to NovaSentinel

Thank you for joining the **NovaSentinel** community, a multi-layer Anti-DDoS system for Space Station 14 and beyond! Your contributions help fortify our defenses. This guide outlines how to collaborate effectively.

## How to Contribute

### Reporting Issues
- **Check Existing Issues**: Review [GitHub Issues](https://github.com/earnestaxis5546/NovaSentinel/issues) to avoid duplicates.
- **Submit an Issue**:
  - Use a clear title (e.g., "Redis rate-limiting fails under UDP flood").
  - Include steps to reproduce, environment (e.g., Arch Linux, .NET 8.0), and logs.
  - Suggest fixes if possible.
- **Template**:
  ```markdown
  **Description**: [Brief issue description]
  **Steps to Reproduce**: [1. Step 1, 2. Step 2, ...]
  **Environment**: [OS, .NET version, Redis version, etc.]
  **Logs**: [Paste relevant logs]
  **Expected Behavior**: [What should happen]
  **Actual Behavior**: [What happens]

Submitting Pull Requests (PRs)

Fork the Repository:git clone https://github.com/EarnestAxis5546/NovaSentinel.git
cd NovaSentinel


Create a Branch:git checkout -b feature/your-feature-name


Make Changes:
Follow the C# coding style below.
Update tests in src/NovaSentinel.Tests/.


Test Locally:dotnet test src/NovaSentinel.Tests


Commit Changes:
Use clear commit messages (e.g., "Add eBPF filter for TCP floods").
Sign off commits: git commit -s -m "Your message".


Push and Create PR:git push origin feature/your-feature-name


Open a PR on GitHub.
Reference related issues (e.g., "Fixes #123").


Review Process:
Address feedback promptly.
Ensure CI tests pass.



Coding Style (C#)

Formatting: Use .NET Core conventions (dotnet format).
Naming:
PascalCase for classes, methods, properties (e.g., DDoSProtectionMiddleware).
camelCase for local variables (e.g., requestCount).


Comments: Use XML comments for public methods:/// <summary>
/// Blocks an IP address for exceeding rate limits.
/// </summary>
/// <param name="ip">The IP to block.</param>
public void BlockIp(string ip) { ... }


Structure: Follow single-responsibility principle, use dependency injection.
Linting: Run dotnet format before committing.

Getting Help

Issues: Post on GitHub Issues.
Contact: Email wolkapoika@gmail.com or message on X.


  



  Let’s build a stronger, safer NovaSentinel together!