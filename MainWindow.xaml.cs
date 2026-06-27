using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        // ========== GENERIC COLLECTIONS ==========
        private List<ChatMessage> _chatHistory;
        private Dictionary<string, List<string>> _keywordResponses;
        private List<string> _randomPhishingTips;
        private List<string> _randomPasswordTips;
        private List<string> _randomSafeBrowsingTips;

        // ========== TASK MANAGEMENT ==========
        private List<TaskItem> _tasks;
        private List<ReminderItem> _reminders;
        private int _taskCounter = 1;

        // ========== QUIZ ==========
        private List<QuizQuestion> _quizQuestions;
        private int _currentQuestionIndex;
        private int _quizScore;
        private bool _isQuizActive;

        // ========== ACTIVITY LOG ==========
        private List<ActivityLogEntry> _activityLog;
        private const int MAX_LOG_ENTRIES = 10;

        // ========== MEMORY & STATE ==========
        private string _userName;
        private string _currentTopic;
        private string _userSentiment;
        private Dictionary<string, string> _userPreferences;

        // ========== NLP KEYWORDS ==========
        private List<string> _taskKeywords = new List<string> { "task", "todo", "to-do", "add", "create", "new" };
        private List<string> _reminderKeywords = new List<string> { "remind", "reminder", "remember", "alert" };
        private List<string> _quizKeywords = new List<string> { "quiz", "test", "question", "challenge" };
        private List<string> _logKeywords = new List<string> { "log", "history", "what have you done", "activity" };
        private List<string> _showKeywords = new List<string> { "show", "view", "list", "display" };

        public MainWindow()
        {
            InitializeComponent();

            // Play voice greeting
            PlayVoiceGreeting();

            // Initialize collections
            InitializeCollections();

            // Add welcome message
            AddBotMessage("👋 Welcome to the Cybersecurity Awareness Bot!");
            AddBotMessage("I can help you with:");
            AddBotMessage("• 📝 Add and manage tasks");
            AddBotMessage("• ⏰ Set reminders");
            AddBotMessage("• 📊 Take a cybersecurity quiz");
            AddBotMessage("• 📜 View activity log");
            AddBotMessage("\nWhat's your name?");
        }

        private void PlayVoiceGreeting()
        {
            try
            {
                string audioPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (System.IO.File.Exists(audioPath))
                {
                    using (SoundPlayer player = new SoundPlayer(audioPath))
                    {
                        player.PlaySync();
                    }
                }
            }
            catch (Exception) { }
        }

        private void InitializeCollections()
        {
            _chatHistory = new List<ChatMessage>();
            _activityLog = new List<ActivityLogEntry>();
            _userPreferences = new Dictionary<string, string>();
            _currentTopic = "general";
            _userSentiment = "neutral";
            _isQuizActive = false;

            // Initialize tasks and reminders
            _tasks = new List<TaskItem>();
            _reminders = new List<ReminderItem>();

            // ===== Initialize keyword responses =====
            _keywordResponses = new Dictionary<string, List<string>>();

            _keywordResponses["password"] = new List<string>
            {
                "🔐 Use a different password for every account!",
                "🔐 Make passwords at least 12 characters long with mixed characters.",
                "🔐 Consider using a password manager like Bitwarden.",
                "🔐 Enable Two-Factor Authentication (2FA) whenever possible."
            };

            _keywordResponses["scam"] = new List<string>
            {
                "⚠️ Never click links in unsolicited emails or SMS.",
                "⚠️ Scammers create fake urgency - don't panic, verify first.",
                "⚠️ Check sender email addresses carefully.",
                "⚠️ If it seems too good to be true, it probably is."
            };

            _keywordResponses["phishing"] = _keywordResponses["scam"];

            _keywordResponses["privacy"] = new List<string>
            {
                "🛡️ Review your privacy settings on social media regularly.",
                "🛡️ Don't share personal information like ID numbers publicly.",
                "🛡️ Be careful what you post - scammers use that information.",
                "🛡️ Use a VPN on public WiFi to protect your data."
            };

            _keywordResponses["safe"] = new List<string>
            {
                "🌐 Look for 'https://' in website addresses.",
                "🌐 Don't download files from untrusted websites.",
                "🌐 Keep your browser and extensions updated.",
                "🌐 Use an ad-blocker to prevent malicious ads."
            };

            // ===== Initialize random tip arrays =====
            _randomPhishingTips = new List<string>
            {
                "📧 Be cautious of emails asking for personal information.",
                "📧 Hover over links before clicking to see where they go.",
                "📧 Check for spelling mistakes - common signs of phishing.",
                "📧 When in doubt, go directly to the website instead of clicking links."
            };

            _randomPasswordTips = new List<string>
            {
                "🔑 Never use 'password123' - hackers try that first!",
                "🔑 Use passphrases like 'BlueElephant$Running!92'.",
                "🔑 Change default passwords on new devices immediately.",
                "🔑 Don't write passwords on sticky notes on your monitor!"
            };

            _randomSafeBrowsingTips = new List<string>
            {
                "🛜 Avoid using public computers for banking.",
                "🛜 Clear your browser cache and cookies regularly.",
                "🛜 Be careful what you download - free software can contain malware.",
                "🛜 Use bookmarks for important sites instead of searching."
            };

            // ===== Initialize quiz questions =====
            _quizQuestions = new List<QuizQuestion>
            {
                new QuizQuestion { Question = "What does 'phishing' refer to?",
                    Options = new List<string> { "A type of fishing", "A cyber attack via fake emails", "A password manager", "A type of antivirus" },
                    CorrectAnswer = 1 },
                new QuizQuestion { Question = "Which is a strong password?",
                    Options = new List<string> { "password123", "12345678", "BlueElep$F#2k", "admin" },
                    CorrectAnswer = 2 },
                new QuizQuestion { Question = "What does 'https' indicate in a URL?",
                    Options = new List<string> { "It's a trusted website", "The connection is encrypted", "It's a social media site", "It's a government website" },
                    CorrectAnswer = 1 },
                new QuizQuestion { Question = "What is two-factor authentication?",
                    Options = new List<string> { "Using two passwords", "A second layer of security", "A type of malware", "A password manager" },
                    CorrectAnswer = 1 },
                new QuizQuestion { Question = "What should you do with suspicious emails?",
                    Options = new List<string> { "Reply to them", "Click the links", "Delete and report them", "Forward to friends" },
                    CorrectAnswer = 2 }
            };

            // Log initial entry
            AddToActivityLog("System", "Chatbot initialized and ready.");
        }

        // ========== NLP PARSING ==========
        private string ParseUserIntent(string input)
        {
            string lowerInput = input.ToLower();

            // Check for name first (if not set)
            if (_userName == null && !lowerInput.Contains("my name is") && input.Split(' ').Length < 5 && !lowerInput.Contains("?"))
            {
                _userName = input.Trim();
                _userPreferences["name"] = _userName;
                AddToActivityLog("NLP", $"User set name: {_userName}");
                return $"Nice to meet you, {_userName}! 🎉\n\nWhat would you like to do today?\n• Add a task\n• Set a reminder\n• Take a quiz\n• View activity log";
            }

            // ===== Check for Activity Log request =====
            foreach (var keyword in _logKeywords)
            {
                if (lowerInput.Contains(keyword) || (lowerInput.Contains("show") && lowerInput.Contains("log")))
                {
                    AddToActivityLog("NLP", "User requested activity log.");
                    return GetActivityLog();
                }
            }

            // ===== Check for Task requests =====
            bool hasTaskKeyword = _taskKeywords.Any(k => lowerInput.Contains(k));
            if (hasTaskKeyword || lowerInput.Contains("task") || lowerInput.Contains("to-do") || lowerInput.Contains("todo"))
            {
                AddToActivityLog("NLP", "User requested task operation.");
                return ProcessTaskRequest(input);
            }

            // ===== Check for Reminder requests =====
            if (_reminderKeywords.Any(k => lowerInput.Contains(k)))
            {
                AddToActivityLog("NLP", "User requested reminder operation.");
                return ProcessReminderRequest(input);
            }

            // ===== Check for Quiz requests =====
            if (_quizKeywords.Any(k => lowerInput.Contains(k)))
            {
                AddToActivityLog("NLP", "User requested quiz.");
                return ProcessQuizRequest(input);
            }

            // ===== Check for sentiment =====
            if (lowerInput.Contains("worried") || lowerInput.Contains("scared") || lowerInput.Contains("nervous"))
            {
                _userSentiment = "worried";
                AddToActivityLog("NLP", "User expressed worry.");
                return "I understand you're feeling worried. That's normal! The more you learn, the safer you'll be. Would you like a tip on staying safe online?";
            }

            if (lowerInput.Contains("confused") || lowerInput.Contains("difficult"))
            {
                _userSentiment = "confused";
                AddToActivityLog("NLP", "User expressed confusion.");
                return "I know cybersecurity can be confusing! Let's take it step by step. What would you like me to explain?";
            }

            // ===== Check for keywords (password, scam, privacy, etc.) =====
            foreach (var keyword in _keywordResponses.Keys)
            {
                if (lowerInput.Contains(keyword))
                {
                    _currentTopic = keyword;
                    AddToActivityLog("NLP", $"User asked about: {keyword}");
                    return GetRandomResponseForTopic(keyword);
                }
            }

            // ===== Check for "tip" or "advice" =====
            if (lowerInput.Contains("tip") || lowerInput.Contains("advice") || lowerInput.Contains("suggestion"))
            {
                AddToActivityLog("NLP", "User requested a tip.");
                return GetRandomTip("general");
            }

            // ===== Check for "remember" =====
            if (lowerInput.Contains("remember") || lowerInput.Contains("know about me"))
            {
                AddToActivityLog("NLP", "User asked what I remember.");
                return ShowMemory();
            }

            // ===== Default =====
            AddToActivityLog("NLP", "Unrecognized input: " + input);
            return "🤔 I'm not quite sure what you mean. You can ask me to:\n• Add a task\n• Set a reminder\n• Take a quiz\n• Show activity log\n• Ask about passwords, scams, or privacy";
        }

        // ========== TASK PROCESSING ==========
        private string ProcessTaskRequest(string input)
        {
            // Try to extract the task from the input
            string taskDescription = ExtractContent(input, new[] { "task", "add", "create", "new", "to-do", "todo" });

            if (string.IsNullOrWhiteSpace(taskDescription))
            {
                return "What task would you like me to add? Please describe it clearly.";
            }

            TaskItem newTask = new TaskItem
            {
                Id = _taskCounter++,
                Description = taskDescription,
                CreatedDate = DateTime.Now,
                IsCompleted = false
            };
            _tasks.Add(newTask);

            AddToActivityLog("Task", $"Added task: '{taskDescription}'");

            return $"✅ Task added: '{taskDescription}' (ID: {newTask.Id})\nWould you like to set a reminder for this task?";
        }

        // ========== REMINDER PROCESSING ==========
        private string ProcessReminderRequest(string input)
        {
            // Try to extract reminder content
            string reminderContent = ExtractContent(input, new[] { "remind", "reminder", "remember", "alert" });

            if (string.IsNullOrWhiteSpace(reminderContent))
            {
                return "What would you like me to remind you about?";
            }

            ReminderItem reminder = new ReminderItem
            {
                Id = _reminders.Count + 1,
                Content = reminderContent,
                CreatedDate = DateTime.Now,
                ReminderDate = DateTime.Now.AddDays(1) // Default: tomorrow
            };
            _reminders.Add(reminder);

            AddToActivityLog("Reminder", $"Set reminder: '{reminderContent}' for {reminder.ReminderDate:dd MMM yyyy}");

            return $"⏰ Reminder set for: '{reminderContent}'\nReminder date: {reminder.ReminderDate:dd MMM yyyy}";
        }

        // ========== QUIZ PROCESSING ==========
        private string ProcessQuizRequest(string input)
        {
            if (!_isQuizActive)
            {
                _currentQuestionIndex = 0;
                _quizScore = 0;
                _isQuizActive = true;
                AddToActivityLog("Quiz", "Quiz started.");
                return GetNextQuestion();
            }
            else
            {
                // Try to extract answer number or letter
                string lowerInput = input.ToLower();
                int answerIndex = -1;

                // Check for number
                if (int.TryParse(input, out int numAnswer))
                {
                    answerIndex = numAnswer - 1;
                }
                // Check for letter
                else if (lowerInput.Contains("a") || lowerInput.Contains("option 1"))
                    answerIndex = 0;
                else if (lowerInput.Contains("b") || lowerInput.Contains("option 2"))
                    answerIndex = 1;
                else if (lowerInput.Contains("c") || lowerInput.Contains("option 3"))
                    answerIndex = 2;
                else if (lowerInput.Contains("d") || lowerInput.Contains("option 4"))
                    answerIndex = 3;

                if (answerIndex >= 0 && answerIndex < _quizQuestions[_currentQuestionIndex - 1].Options.Count)
                {
                    int correctIndex = _quizQuestions[_currentQuestionIndex - 1].CorrectAnswer;
                    if (answerIndex == correctIndex)
                    {
                        _quizScore++;
                        AddBotMessage("✅ Correct! Great job!");
                    }
                    else
                    {
                        AddBotMessage($"❌ Incorrect. The correct answer was: {_quizQuestions[_currentQuestionIndex - 1].Options[correctIndex]}");
                    }
                }
                else if (input.ToLower() != "stop" && input.ToLower() != "end" && input.ToLower() != "exit")
                {
                    AddBotMessage("Please type the number of your answer (1, 2, 3, or 4) or say 'stop' to end the quiz.");
                }

                // Check if quiz is complete
                if (_currentQuestionIndex >= _quizQuestions.Count)
                {
                    _isQuizActive = false;
                    AddToActivityLog("Quiz", $"Quiz completed with score: {_quizScore}/{_quizQuestions.Count}");
                    return $"🏆 Quiz complete!\nYou scored: {_quizScore} out of {_quizQuestions.Count}\n\nType 'start quiz' to try again or ask me anything else!";
                }

                return GetNextQuestion();
            }
        }

        private string GetNextQuestion()
        {
            if (_currentQuestionIndex >= _quizQuestions.Count)
            {
                return "🏆 Quiz complete! You've answered all questions.";
            }

            var q = _quizQuestions[_currentQuestionIndex];
            string optionsText = "";
            for (int i = 0; i < q.Options.Count; i++)
            {
                optionsText += $"{i + 1}. {q.Options[i]}\n";
            }

            string message = $"📊 Question {_currentQuestionIndex + 1} of {_quizQuestions.Count}:\n{q.Question}\n\n{optionsText}\nType the number of your answer:";
            _currentQuestionIndex++;
            return message;
        }

        // ========== ACTIVITY LOG ==========
        private void AddToActivityLog(string category, string description)
        {
            var entry = new ActivityLogEntry
            {
                Timestamp = DateTime.Now,
                Category = category,
                Description = description
            };

            _activityLog.Add(entry);

            // Keep only the last MAX_LOG_ENTRIES
            if (_activityLog.Count > MAX_LOG_ENTRIES)
            {
                _activityLog = _activityLog.Skip(_activityLog.Count - MAX_LOG_ENTRIES).ToList();
            }
        }

        private string GetActivityLog()
        {
            if (_activityLog.Count == 0)
            {
                return "📜 No activity logged yet.";
            }

            string logText = "📜 Here's a summary of recent actions:\n\n";
            int count = 1;
            foreach (var entry in _activityLog.OrderByDescending(e => e.Timestamp).Take(MAX_LOG_ENTRIES))
            {
                logText += $"{count}. {entry.Timestamp:HH:mm} - [{entry.Category}] {entry.Description}\n";
                count++;
            }

            logText += "\nTip: Keep learning about cybersecurity to stay safe online!";
            return logText;
        }

        // ========== HELPER METHODS ==========
        private string ExtractContent(string input, string[] removeWords)
        {
            string result = input;
            foreach (var word in removeWords)
            {
                result = result.Replace(word, "", StringComparison.OrdinalIgnoreCase);
            }
            // Remove common filler words
            string[] fillerWords = { "to", "for", "a", "an", "the", "please", "can", "you", "would", "like", "could" };
            foreach (var word in fillerWords)
            {
                result = result.Replace($" {word} ", " ", StringComparison.OrdinalIgnoreCase);
            }
            result = result.Trim();

            // If result is too short, try to extract meaningful content
            if (result.Length < 3 && input.Contains("remind") || input.Contains("task"))
            {
                // Try to get what's after the keyword
                foreach (var word in removeWords)
                {
                    int index = input.IndexOf(word, StringComparison.OrdinalIgnoreCase);
                    if (index >= 0)
                    {
                        string afterWord = input.Substring(index + word.Length).Trim();
                        if (afterWord.Length > 0)
                        {
                            return afterWord;
                        }
                    }
                }
            }

            return result;
        }

        private string GetRandomResponseForTopic(string topic)
        {
            if (_keywordResponses.ContainsKey(topic) && _keywordResponses[topic].Count > 0)
            {
                Random rand = new Random();
                int index = rand.Next(_keywordResponses[topic].Count);
                return _keywordResponses[topic][index] + "\n\nWould you like another tip on this topic? Just ask!";
            }
            return "I have information on that topic! Try asking me about passwords, privacy, or safe browsing.";
        }

        private string GetRandomTip(string category)
        {
            Random rand = new Random();
            switch (category)
            {
                case "password":
                    return _randomPasswordTips[rand.Next(_randomPasswordTips.Count)];
                case "phishing":
                    return _randomPhishingTips[rand.Next(_randomPhishingTips.Count)];
                case "safe":
                    return _randomSafeBrowsingTips[rand.Next(_randomSafeBrowsingTips.Count)];
                default:
                    string[] allTips = {
                        _randomPasswordTips[rand.Next(_randomPasswordTips.Count)],
                        _randomPhishingTips[rand.Next(_randomPhishingTips.Count)],
                        _randomSafeBrowsingTips[rand.Next(_randomSafeBrowsingTips.Count)]
                    };
                    return allTips[rand.Next(allTips.Length)];
            }
        }

        private string ShowMemory()
        {
            string memoryText = "Here's what I remember about you:\n\n";
            if (_userName != null)
                memoryText += $"• Your name: {_userName}\n";
            else
                memoryText += "• You haven't told me your name yet!\n";

            memoryText += $"• Your mood: {_userSentiment}\n";
            memoryText += $"• Tasks added: {_tasks.Count}\n";
            memoryText += $"• Reminders set: {_reminders.Count}\n";
            memoryText += $"• Quiz attempts: {(int)Math.Ceiling((double)_quizScore / 5 * 100)}% average\n\n";

            if (_userName != null)
                memoryText += $"You're doing great, {_userName}! Keep learning about cybersecurity!";
            else
                memoryText += "Tell me your name so I can remember you better!";

            return memoryText;
        }

        // ========== UI METHODS ==========
        private void AddUserMessage(string message)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#e94560")),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(15, 10, 15, 10),
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Right,
                MaxWidth = 500
            };
            TextBlock textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14
            };
            bubble.Child = textBlock;
            ChatPanel.Children.Add(bubble);
            _chatHistory.Add(new ChatMessage { Sender = "User", Message = message, Timestamp = DateTime.Now });
            ScrollToBottom();
        }

        private void AddBotMessage(string message)
        {
            Border bubble = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0f3460")),
                CornerRadius = new CornerRadius(15),
                Padding = new Thickness(15, 10, 15, 10),
                Margin = new Thickness(5, 5, 5, 5),
                HorizontalAlignment = HorizontalAlignment.Left,
                MaxWidth = 500
            };
            TextBlock textBlock = new TextBlock
            {
                Text = message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 14
            };
            bubble.Child = textBlock;
            ChatPanel.Children.Add(bubble);
            _chatHistory.Add(new ChatMessage { Sender = "Bot", Message = message, Timestamp = DateTime.Now });
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToBottom();
        }

        // ========== EVENT HANDLERS ==========
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }

        private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string userInput = UserInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(userInput))
                return;

            AddUserMessage(userInput);
            UserInputTextBox.Clear();

            string botResponse = ParseUserIntent(userInput);
            AddBotMessage(botResponse);

            UserInputTextBox.Focus();
        }

        private void QuickAction_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null)
            {
                string tag = btn.Tag?.ToString() ?? "";
                UserInputTextBox.Text = tag;
                SendMessage();
            }
        }
    }

    // ========== DATA CLASSES ==========
    public class ChatMessage
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsCompleted { get; set; }
    }

    public class ReminderItem
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ReminderDate { get; set; }
    }

    public class ActivityLogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
    }

    public class QuizQuestion
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public int CorrectAnswer { get; set; }
    }
}