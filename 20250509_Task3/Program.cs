namespace _20250509_Task3
{
    /*
     Используя потоки создать метод, который находит в строке количество слов, 
    которые начинаются и оканчиваются на одну и ту же букву.
     */
    internal class Program
    {
        static int count = 0;
        // lockObject is a synchronization object used to safely manage access
        // to the shared variable sharedValue across multiple threads.
        static readonly object lockObject = new object();
        static void Main(string[] args)
        {
            string input = "Anna went to Alaska and met Otto who owns a civic car";

            Thread thread = new Thread(() => CountMatchingWords(input));
            thread.Start();

            thread.Join();

            Console.WriteLine($"\nTotal words starting and ending with the same letter: {count}");
        }
        static void CountMatchingWords(string sentence)
        {
            string[] words = sentence.Split(new[] { ' ', ',', '.', '!', '?' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                string cleanWord = word.Trim().ToLower();

                if (cleanWord.Length > 1 && cleanWord[0] == cleanWord[cleanWord.Length - 1])
                {
                    lock (lockObject)
                    {
                        count++;
                        Console.WriteLine($"[Thread] Matched word: {word}");
                    }
                }
            }
        }
    }
}
