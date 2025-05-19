====================================
  KNOWLEDGE CENTER - VERSION 1.0
  Terminal-Based Knowledge Tracker
====================================

Author: Matt Mortensen (@mttmortensen)
Built with: C#, SQL Server
-------------------------------

🎯 WHAT IS THIS?
----------------
Knowledge Center is a personal growth tracker built entirely in the terminal.
It helps you organize your learning by creating "Knowledge Nodes" (KNs) and attaching log entries to track your daily progress, projects, and breakthroughs.

This app is 100% terminal-driven and fully custom-built for personal knowledge building — not productivity buzzwords or dopamine buttons.

💡 KEY FEATURES
---------------
- Create, view, update, and delete Knowledge Nodes
- Log Entries tied to each Node with timestamp, tag, and progress flag
- Structured terminal UI for ease of use and clarity
- Logs are immutable — no edits or deletes allowed for integrity
- Full CRUD support for KNs
- Connected to a SQL Server backend (NOT SQLite)
- Version 1.0 is feature-complete for terminal workflows

🧱 ENTITY STRUCTURE
-------------------
• KnowledgeNode
  - Title
  - Domain
  - Description
  - Confidence Level (1–10)
  - Status (Exploring, Learning, Mastered)
  - Node Type (Concept or Project)
  - CreatedAt / LastUpdated timestamps

• LogEntry
  - NodeId (linked to KnowledgeNode)
  - EntryDate (auto-generated)
  - Content (what you did, learned, tried, etc.)
  - Tag (Quick label like "breakthrough", "confused", "research")
  - ContributesToProgress (true/false)

🚫 DESIGN PHILOSOPHY
---------------------
Logs are permanent. No edits. No deletes. You either log it or you don't.
Knowledge is messy and human — this app reflects that.

⚙️ TECH STACK
-------------
- Language: C#
- Data: SQL Server (local)
- Terminal-only UI
- Architecture: Raw C# classes, no LINQ, no .NET Core
- Separation of concerns: Database layer, Service layer, UI layer

💻 RUNNING THE APP
-------------------
- Make sure SQL Server is installed and your KnowledgeCenterDB is configured
- Clone the repo
- Open solution in Visual Studio
- Update the DB connection string in `Database.cs` to match your environment
- Build & run — the terminal is your home

🚀 ROADMAP IDEAS (POST-V1)
---------------------------
- Domains as first-class objects (CRUD support, filtering, domain descriptions)
- Stats & analytics dashboard (log streaks, weekly log counts, etc.)
- Markdown export for GitHub-style logging
- WinForms read-only viewer
- Self-hosted dashboard with Spectrum Console or ASP.NET Core
- Sync/backup tooling for long-term archive

🧠 WHY I BUILT THIS
--------------------
To organize the chaos of learning across life — from tech to hobbies to obsessions.
This isn't a todo list. This is a log of what you *become*.

