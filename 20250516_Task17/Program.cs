using System.Text;

/*
 Отправить редактируемый текст в консоль, чтобы пользователь мог стереть его нажатием на ← Backspace и написать там другой.
 */

namespace _20250516_Task17
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string initialText = "Edit me"; // Initial editable text shown to user
            StringBuilder inputBuffer = new StringBuilder(initialText); // Buffer for storing/editing text


            Console.Write("Input: ");
            Console.Write(inputBuffer); // Print the initial text ("Edit me")

            int cursorStart = Console.CursorLeft; // Store cursor position after initial text

            ConsoleKeyInfo key; // Will store each key press info

            while (true) // Infinite loop to handle user input
            {
                key = Console.ReadKey(intercept: true); // Read a key press without displaying it

                if (key.Key == ConsoleKey.Enter) // If Enter is pressed
                {
                    break; // Exit the loop and finish input
                }
                else if (key.Key == ConsoleKey.Backspace) // Handle Backspace
                {
                    // Ensure there's text to delete and the cursor isn't before start
                    if (inputBuffer.Length > 0 && Console.CursorLeft > cursorStart)
                    {
                        // Remove one character from buffer before the cursor
                        inputBuffer.Remove(Console.CursorLeft - cursorStart - 1, 1);

                        // Move the cursor left
                        Console.CursorLeft--;

                        // Redraw the text after deletion
                        RedrawInput(inputBuffer, cursorStart);
                    }
                }
                else if (!char.IsControl(key.KeyChar)) // If it's a printable character
                {
                    // Insert the character into the buffer at the cursor position
                    inputBuffer.Insert(Console.CursorLeft - cursorStart, key.KeyChar);

                    // Move cursor right after inserting character
                    Console.CursorLeft++;

                    // Redraw the input text
                    RedrawInput(inputBuffer, cursorStart);
                }
            }

            Console.WriteLine(); // Move to next line after input
            Console.WriteLine($"You entered: {inputBuffer}");
        }
        static void RedrawInput(StringBuilder buffer, int startLeft)
        {
            int currentLeft = Console.CursorLeft; // Store current cursor position
            Console.SetCursorPosition(startLeft, Console.CursorTop); // Move cursor to start of editable text
            Console.Write(buffer.ToString() + " "); // Reprint the text and clear leftovers
            Console.SetCursorPosition(currentLeft, Console.CursorTop); // Restore cursor to its position
        }
    }
}
