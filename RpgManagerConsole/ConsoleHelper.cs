using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpgManagerConsole
{
    static internal class ConsoleHelper
    {
        private static readonly BoxCharacters SingleLine = new(Horizontal: '─', Vertical: '│', UpperLeft: '┌', LowerLeft: '└', UpperRight: '┐', LowerRight: '┘');
        private static readonly BoxCharacters DoubleLine = new(Horizontal: '═', Vertical: '║', UpperLeft: '╔', LowerLeft: '╚', UpperRight: '╗', LowerRight: '╝');

        /// <summary>
        /// Width of the console window minus 2 (for box borders).
        /// </summary>
        private static readonly int MaxTitleLength = Console.WindowWidth - 2;

        /// <summary>
        /// Draws a box around the specified title, with an optional style for single or double lines. Fails silently if title is too long.
        /// </summary>
        /// <param name="title">The text to display inside the box.</param>
        /// <param name="doubleLines">Whether the box should use double-line characters or not. The default is false.</param>
        /// <param name="fullWidth">Whether the box should span the full width of the console window. The default is false. If true, <paramref name="padding"/> automatically becomes 0.</param>
        /// <param name="padding">The amount of padding (in spaces) to add on each side of the title text. The default is 7 (or 0 if <paramref name="fullWidth"/> = true).</param>
        public static void DrawBox(string title, bool doubleLines = false, bool fullWidth = false, int padding = 7)
        {
            string[] lines = title.Split(Environment.NewLine, StringSplitOptions.TrimEntries);

            foreach (string line in lines)
            {
                if (line.Length > MaxTitleLength - padding * 2)
                {
                    return;
                    //throw new ArgumentException($"A title line exceeds maximum length of {MaxTitleLength} characters.", nameof(title));
                }
            }

            int maxLineLength = MaxTitleLength;
            int totalWidth = maxLineLength;

            if (fullWidth)
            {
                padding = 0; // geen padding bij fullWidth
            }
            else
            {
                // bij multiline: maxLineLength = lengte van langste regel
                maxLineLength = lines.Length > 0 ? lines.Max(line => line.Length) : 0;
                totalWidth = maxLineLength + (padding * 2);
            }

            BoxCharacters chars = doubleLines ? DoubleLine : SingleLine;

            // bovenste lijn
            Console.Write(chars.UpperLeft);
            Console.Write(new string(chars.Horizontal, totalWidth));
            Console.WriteLine(chars.UpperRight);

            // inhoud
            foreach (string line in lines)
            {
                int leftPadding = padding + (maxLineLength - line.Length) / 2; // Center each line
                int rightPadding = padding + (maxLineLength - line.Length + 1) / 2; // Adjust for odd lengths
                Console.WriteLine($"{chars.Vertical}{new string(' ', leftPadding)}{line}{new string(' ', rightPadding)}{chars.Vertical}");
            }

            // onderste lijn
            Console.Write(chars.LowerLeft);
            Console.Write(new string(chars.Horizontal, totalWidth));
            Console.WriteLine(chars.LowerRight);
        }

        /// <summary>
        /// Print a menu with options and an exit option. Returns the chosen option as an integer.
        /// </summary>
        /// <param name="options">Options to display in the menu.</param>
        /// <param name="exitOption">Name for the exit option (always displayed on the bottom as option #0)</param>
        /// <returns></returns>
        public static int Menu(List<string> options, string exitOption)
        {
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {options[i]}");
            }
            Console.WriteLine("0 - " + exitOption);

            return ReadInt("Choose an option: ", 0, options.Count);
        }

        /// <summary>
        /// Safely reads an integer from the console within the specified range.
        /// </summary>
        /// <remarks>If the user enters an unparsable input or an integer outside of the specified range, 
        /// an error message is displayed, and the prompt is repeated until valid input is provided.</remarks>
        /// <param name="prompt">Displayed before input.</param>
        /// <param name="min">Minimum valid value. Defaults to <see cref="int.MinValue"/>.</param>
        /// <param name="max">Maximum valid value. Defaults to <see cref="int.MaxValue"/>.</param>
        /// <returns>The integer entered by the user.</returns>
        public static int ReadInt(string prompt, int min = int.MinValue, int max = int.MaxValue)
        {
            Console.Write(prompt);
            string? consoleInput = Console.ReadLine();
            int input;

            while (!int.TryParse(consoleInput, out input) || input < min || input > max)
            {
                ShowError("Invalid input!");
                Console.Write(prompt);
                consoleInput = Console.ReadLine();
            }

            return input;
        }

        /// <summary>
        /// Reads a non-empty string input.
        /// </summary>
        /// <remarks>If the user enters an empty or whitespace-only input, an error message is displayed,
        /// and the prompt is repeated until valid input is provided.</remarks>
        /// <param name="prompt">The prompt message to display to the user.</param>
        /// <returns>The string entered by the user. The returned string is guaranteed to be non-null and non-whitespace.</returns>
        public static string ReadString(string prompt)
        {
            Console.Write(prompt);
            string? consoleInput = Console.ReadLine();

            while (string.IsNullOrWhiteSpace(consoleInput))
            {
                ShowError("Invalid input!");
                Console.Write(prompt);
                consoleInput = Console.ReadLine();
            }

            return consoleInput;
        }

        /// <summary>
        /// Displays an error message in a specified color.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="errorColour">The <see cref="ConsoleColor"/> to display the message in (defaults to <see cref="ConsoleColor.Red"/>.</param>
        public static void ShowError(string message, ConsoleColor errorColour = ConsoleColor.Red)
        {
            Console.ForegroundColor = errorColour;
            Console.WriteLine(message);
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray;
        }

    }
}
