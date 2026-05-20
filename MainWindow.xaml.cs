using System;
using System.Collections.Generic;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityChatbotGUI
{
    public partial class MainWindow : Window
    {
        // Voice greeting plays at startup using greeting.wav
        public MainWindow()
        {
            // This dictionary handles keyword recognition for cybersecurity topics
            InitializeComponent();
            PlayVoiceGreeting();
            AddBotMessage("Hello! 👋 Welcome to the Cybersecurity Awareness Bot!");
            AddBotMessage("What's your name?");
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
            catch (Exception)
            {
                // Voice greeting failed - continue without it
            }
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
            ScrollToBottom();
        }
        // Sentiment detection using keyword analysis for worried/confident moods

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
            ScrollToBottom();
        }

        private void ScrollToBottom()
        {
            ChatScrollViewer.ScrollToBottom();
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            SendMessage();
        }
        // Random responses using generic List<string> arrays

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

            // Simple response for now
            AddBotMessage($"You said: {userInput}");

            UserInputTextBox.Focus();
        }
    }
}
