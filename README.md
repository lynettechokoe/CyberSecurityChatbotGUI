# CyberSecurityChatbotGUI

# 🛡️ Cybersecurity Awareness Chatbot

A comprehensive cybersecurity awareness application with console chatbot, WPF GUI, NLP simulation, task management, quiz, and activity logging features. Built for South African citizens to learn about online safety.

---

## 📋 Table of Contents

- [What I Built](#what-i-built)
- [Part 1 - Console Chatbot](#part-1---console-chatbot)
- [Part 2 - GUI Interface (WPF)](#part-2---gui-interface-wpf)
- [Part 3 - NLP Simulation & Activity Log](#part-3---nlp-simulation--activity-log)
- [Files in Project](#files-in-project)
- [How to Run](#how-to-run)
- [Test Values That Work](#test-values-that-work)
- [Stored Messages Menu Options](#stored-messages-menu-options)
- [Coding Constructs Used](#coding-constructs-used)
- [Unit Tests](#unit-tests)
- [GitHub Releases](#github-releases)
- [References](#references)
- [Conclusion](#conclusion)

---

## What I Built

| Part | Description |
|------|-------------|
| **Part 1** | Console-based chatbot with voice greeting, ASCII art, memory, and sentiment detection |
| **Part 2** | WPF GUI with chat bubbles, keyword recognition, random responses, and conversation flow |
| **Part 3** | NLP simulation, task management, reminders, quiz, and activity logging |

---

## Part 1 - Console Chatbot

### Features Implemented

- ✅ **Voice Greeting**: Plays `greeting.wav` on startup using `System.Media.SoundPlayer`
- ✅ **ASCII Art Logo**: Cybersecurity-themed logo displayed in console
- ✅ **User Input Validation**: Name validation (non-empty), command validation
- ✅ **Auto-Implemented Properties**: `UserData` class with `Name`, `FavoriteTopic`, `CurrentSentiment`
- ✅ **Memory Feature**: Stores and recalls user name and interests
- ✅ **Sentiment Detection**: Identifies "worried", "curious", "frustrated" moods
- ✅ **Cybersecurity Tips**: Phishing, passwords, and safe browsing advice
- ✅ **GitHub Actions CI**: Automated build verification with green check mark
- ✅ **6+ Commits**: Meaningful commit messages on GitHub

### Test Values That Work

| Field | Enter This |
|-------|-------------|
| Name | Thabo |
| Topic Interest | phishing, passwords, privacy |
| Sentiment Test | I'm worried about scams |
| Command | phishing, passwords, tip, remember, exit |

---

## Part 2 - GUI Interface (WPF)

### Features Implemented

- ✅ **WPF GUI**: Translated ALL Part 1 features to graphical interface
- ✅ **Chat Bubbles**: User messages on right (red), bot messages on left (blue)
- ✅ **Voice Greeting**: Plays `greeting.wav` on startup
- ✅ **Dark Theme**: Professional look with accent colors (`#e94560`, `#0f3460`)
- ✅ **Keyword Recognition**: Detects "password", "scam", "privacy", "safe"
- ✅ **Random Responses**: Multiple responses using `List<string>` arrays
- ✅ **Conversation Flow**: Handles "another tip", "tell me more"
- ✅ **Sentiment Detection**: Empathetic responses for worried/confused users
- ✅ **Memory Feature**: Remembers name, interests, and mood
- ✅ **Error Handling**: Default response for unknown inputs
- ✅ **Generic Collections**: `List<T>` for chat history, `Dictionary<string, List<string>>` for keywords
- ✅ **Delegates**: `ResponseGenerator` delegate for dynamic responses
- ✅ **2 Releases**: v1.0 and v1.1 on GitHub

### Test Values That Work

| Command | Expected Response |
|---------|-------------------|
| `Tell me about passwords` | Random password tip |
| `another tip` | Different password tip |
| `I'm worried about scams` | Empathetic response + scam tips |
| `phishing` | Random phishing tip |
| `privacy` | Privacy protection tips |
| `remember` | Shows what bot remembers |
| `asdfgh` (gibberish) | "I'm not sure I understand" |

---

## Part 3 - NLP Simulation & Activity Log

### Features Implemented

- ✅ **NLP Simulation**: Keyword detection for tasks, reminders, quiz
- ✅ **Task Management**: Add tasks with descriptions, track completion
- ✅ **Reminder System**: Set reminders with dates
- ✅ **Cybersecurity Quiz**: 5 multiple choice questions with score tracking
- ✅ **Activity Log**: Tracks ALL actions with timestamps
- ✅ **"Show Activity Log" Command**: Displays recent actions (last 5-10)
- ✅ **Log Categories**: Tasks, reminders, quiz, NLP interactions
- ✅ **Generic Collections**: `List<TaskItem>`, `List<ReminderItem>`, `List<ActivityLogEntry>`
- ✅ **3 Releases**: v1.0, v1.1, v1.2 on GitHub

### Test Values That Work

| Command | What It Does |
|---------|--------------|
| `Add task to enable 2FA` | Adds a task to the task list |
| `Remind me to update password tomorrow` | Sets a reminder |
| `Start quiz` | Begins cybersecurity quiz |
| `1` (during quiz) | Answers question 1 |
| `Show activity log` | Displays recent actions |
| `What have you done for me?` | Displays activity log |
| `Show tasks` | Displays all tasks |

### Quiz Questions

| # | Question | Correct Answer |
|---|----------|----------------|
| 1 | What does 'phishing' refer to? | A cyber attack via fake emails |
| 2 | Which is a strong password? | BlueElep$F#2k |
| 3 | What does 'https' indicate? | The connection is encrypted |
| 4 | What is two-factor authentication? | A second layer of security |
| 5 | What should you do with suspicious emails? | Delete and report them |

---

## 📁 Files in Project

| File | Description |
|------|-------------|
| `MainWindow.xaml` | WPF GUI design (chat bubbles, buttons, colors) |
| `MainWindow.xaml.cs` | All chatbot logic (NLP, tasks, quiz, activity log) |
| `greeting.wav` | Voice greeting audio file |
| `README.md` | Project documentation |
| `.github/workflows/main.yml` | GitHub Actions CI pipeline |

### Code Structure

---

## 🚀 How to Run

### Prerequisites
- Windows OS
- Visual Studio 2022
- .NET Framework 4.8

### Steps

1. **Clone the repository:**
   
   git clone https://github.com/YOUR-USERNAME/CybersecurityChatbotGUI.git