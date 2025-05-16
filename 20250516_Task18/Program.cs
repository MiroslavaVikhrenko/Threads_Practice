namespace _20250516_Task18
{

      /*
     Создайте приложение для работы с массивом: 

■ Удаление из массива повторяющихся значений; 
■ Сортировка массива (стартует после удаления дублей); 
■ Бинарный поиск некоторого значения (стартует после сортировки). 

Используйте «Continuation Tasks» для решения поставленной задачи.
     */

    internal class Program
    {
        static void Main(string[] args)
        {
            // Initial array with duplicates
            int[] numbers = { 5, 3, 7, 2, 5, 7, 1, 9, 2, 4 };
            int valueToSearch = 7; // Value to search using binary search

            Console.WriteLine("Original array: " + string.Join(", ", numbers));

            // Task 1: Remove duplicates
            Task<int[]> removeDuplicatesTask = Task.Run(() =>
            {
                Console.WriteLine("\nRemoving duplicates...");
                int[] distinct = numbers.Distinct().ToArray();
                Console.WriteLine("After removing duplicates: " + string.Join(", ", distinct));
                return distinct; // Pass result to next task
            });

            // Task 2: Sort array (continuation of Task 1)
            Task<int[]> sortTask = removeDuplicatesTask.ContinueWith(previousTask =>
            {
                Console.WriteLine("\nSorting array...");
                int[] sorted = previousTask.Result.OrderBy(n => n).ToArray();
                Console.WriteLine("Sorted array: " + string.Join(", ", sorted));
                return sorted; // Pass result to next task
            });

            // Task 3: Binary search (continuation of Task 2)
            Task searchTask = sortTask.ContinueWith(previousTask =>
            {
                Console.WriteLine($"\nPerforming binary search for {valueToSearch}...");
                int[] sortedArray = previousTask.Result;
                int index = Array.BinarySearch(sortedArray, valueToSearch);
                if (index >= 0)
                {
                    Console.WriteLine($"Value {valueToSearch} found at index {index}.");
                }
                else
                {
                    Console.WriteLine($"Value {valueToSearch} not found.");
                }
            });

            // Wait for the final task to complete
            searchTask.Wait();

            Console.ReadLine(); // Pause to view output
        }
    }
}
