using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Threading;

class CyberBot
{
    private Dictionary<string, List<string>> responseMap;
    private string clientName;

    public void Start()
    {
        DisplayAsciiArt();
        PrintWithColor("\nWelcome to the Cybersecurity Awareness Bot!", ConsoleColor.Green);
        PlaySound("greeting.wav");
        PrintWithColor("Enter your name: ", ConsoleColor.Blue);
        PlaySoundSync("Name.wav");
        clientName = Console.ReadLine().Trim();
        if (string.IsNullOrWhiteSpace(clientName)) clientName = "User";
        PrintWithColor("Type 'exit' anytime to leave.", ConsoleColor.Red);

        while (true)
        {
            PrintWithColor($"\n{clientName}: ", ConsoleColor.Blue);
            string userMessage = Console.ReadLine().Trim().ToLower();

            if (string.IsNullOrWhiteSpace(userMessage))
            {
                PrintWithColor("Bot: Please enter a valid question.", ConsoleColor.Red);
                continue;
            }

            if (userMessage == "exit")
            {
                PrintWithColor("Bot: Goodbye! Stay safe online.", ConsoleColor.Green);
                break;
            }

            RespondToUser(userMessage);
        }
    }

    private void PrintWithColor(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }

    private void RespondToUser(string userMessage)
    {
        bool responseFound = false;
        foreach (var keyword in responseMap.Keys.OrderByDescending(k => k.Length))
        {
            if (userMessage.Contains(keyword))
            {
                PrintWithColor("Bot: " + GetRandomReply(keyword), ConsoleColor.Green);
                responseFound = true;
                break;
            }
        }

        if (!responseFound)
        {
            PrintWithColor("Bot: Sorry, I don't have an answer for that. Try asking about password security, phishing, two-factor authentication, or network safety.", ConsoleColor.Red);
        }
    }

    private void PlaySound(string fileName)
    {
        try
        {
            SoundPlayer player = new SoundPlayer(fileName);
            player.Load();
            player.Play();
        }
        catch
        {
            PrintWithColor("(Sound effect unavailable.)", ConsoleColor.Red);
        }
    }

    private void PlaySoundSync(string fileName)
    {
        try
        {
            SoundPlayer player = new SoundPlayer(fileName);
            player.Load();
            player.PlaySync();
        }
        catch
        {
            PrintWithColor("(Sound effect unavailable.)", ConsoleColor.Red);
        }
    }

    private void DisplayAsciiArt()
    {
        PrintWithColor(@"  ____      _          _                  \n / __|   _| |_   __| | __ _ ___ _ __   \n | |  | | | | '_ \ / _` |/ _` / _ \ '_| \n | || || | |) |  _/ || (| |  __/ |    \n \__, |\_,_| .__/\___|\__,_|\___|_|    \n |___/     |_|                          \n                                        \n    Cybersecurity Awareness Bot", ConsoleColor.Blue);
    }

    private void DisplayTyping(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }
        Console.WriteLine();
    }

    private string GetRandomReply(string keyword)
    {
        Random rand = new Random();
        return responseMap[keyword][rand.Next(responseMap[keyword].Count)];
    }

    static void Main(string[] args)
    {
        CyberBot bot = new CyberBot();
        bot.responseMap = new Dictionary<string, List<string>>
        {
            {"hi", new List<string>{"Hello! How can I assist you today?", "Hi there! Need help with cybersecurity?"} },
            {"password", new List<string>{"Use a strong mix of characters.", "Keep your passwords unique."} },
            {"vpn", new List<string>{"A VPN secures your internet connection.", "Use VPN on public networks."} },
            {"phishing", new List<string>{"Be cautious of unexpected emails and links.", "Look for spelling errors and suspicious URLs."} },
            {"malware", new List<string>{"Keep your antivirus software updated.", "Avoid downloading from untrusted sites."} },
            {"firewall", new List<string>{"Firewalls protect your network by filtering traffic.", "Always enable the firewall on your devices."} },
            {"ransomware", new List<string>{"Back up your data regularly.", "Avoid clicking on unknown attachments."} },
            {"encryption", new List<string>{"Encryption scrambles data to protect it from unauthorized access.", "Always encrypt sensitive files before sharing."} },
            {"two-factor", new List<string>{"2FA adds an extra layer of security.", "Use 2FA to protect important accounts."} }
        };
        bot.Start();
    }
}
