
# KNOWLEDGE CENTER - VERSION 2.0 <br/> A Terminal-Based Knowledge Tracker

Author: Matt Mortensen (@mttmortensen) <br/>
Built with: C#, SQL Server
<br/>

🎯 WHAT IS THIS?
----------------
Knowledge Center is a personal growth tracker built entirely in the terminal.
It helps you organize your learning by creating "Knowledge Nodes" (KNs) and attaching log entries to track your daily progress, projects, and breakthroughs.

This app is 100% terminal-driven and fully custom-built for personal knowledge building — not productivity buzzwords or dopamine buttons.

💡 KEY FEATURES
---------------
- Create, view, update, and delete Knowledge Nodes
- Log Entries tied to each Node with timestamp, tag, and progress flag
- Create and manage Domains (categories for your Knowledge Nodes)
- Structured terminal UI for ease of use and clarity
- Logs are immutable — no edits or deletes allowed for integrity
- Full CRUD support for KNs
- Connected to a SQL Server backend (NOT SQLite)
- Version 2.0 is feature-complete for terminal workflows and includes a full REST API

🧱 ENTITY STRUCTURE
-------------------
• KnowledgeNode
  - Title
  - Domain (General category like "Programming", "Music", etc.)
  - Description
  - Confidence Level (1–10)
  - Status (Exploring, Learning, Mastered)
  - Node Type (Concept or Project)
  - CreatedAt / LastUpdated timestamps

• LogEntry
  - NodeId (linked to KnowledgeNode)
  - EntryDate (auto-generated)
  - Content (what you did, learned, tried, etc.)
  - TagId (Links to Tags Model)
  - ContributesToProgress (true/false)

• Domain
  - Name (e.g., "Programming", "Music", "Fitness")
  - Description 
  - CreatedAt timestamp
  - Status (Active or not in use at the moment)

• Tag
  - TagId (auto-generated)
  - Name (Quick labeling e.g., "breakthrough", "confused", "research")


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
- Update the DB connection string in `Program.cs` to match your environment
- Build & run — the terminal is your home

🚀 ROADMAP IDEAS (POST-V1)
---------------------------
- Stats & analytics dashboard (log streaks, weekly log counts, etc.)
- WinForms read-only viewer
- Self-hosted dashboard with ASP.NET Core

🧠 WHY I BUILT THIS
--------------------
To organize the chaos of learning across life — from tech to hobbies to obsessions.  
This isn't a todo list. This is a log of becoming my best self.

