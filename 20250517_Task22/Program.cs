namespace _20250517_Task22
{
    /*
     Создайте приложение, которое получает данные о погоде из нескольких API. 
    Вы хотите получать данные из всех API одновременно, чтобы повысить производительность приложения. 
Информацию о погоде можно получать из следующих сайтов:
https://www.weatherapi.com/
https://openweathermap.org/

Предварительно необходимо пройти регистрацию и получить Api-ключ.
Для того что бы отправить запрос на сайт (с целью получения ответа в виде Json), можно использовать класс – HttpClient. 
    Вот полный пример отправки запроса и получение ответа в виде строки:

var httpClient = new HttpClient();
var task1 = httpClient.GetStringAsync("https://api.weatherapi.com/v1/current.json?key=YOUR_API_KEY&q=London");
var weatherData1 = task1.Result;
     */
    internal class Program
    {
        static async Task Main(string[] args)
        {
            // Create a single shared HttpClient instance
            using HttpClient httpClient = new HttpClient();

            // Replace with your actual API keys
            string weatherApiKey = "3a5d5ac5484248ee900194702251705";
            string openWeatherMapKey = "b5675c313bb9cbde03ef888a46644b3d";

            // City to fetch weather for
            string city = "London";

            // Build the full URLs for each API request
            string weatherApiUrl = $"https://api.weatherapi.com/v1/current.json?key={weatherApiKey}&q={city}";
            string openWeatherMapUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={openWeatherMapKey}&units=metric";

            // Start both HTTP GET requests concurrently
            Task<string> weatherApiTask = httpClient.GetStringAsync(weatherApiUrl);
            Task<string> openWeatherMapTask = httpClient.GetStringAsync(openWeatherMapUrl);

            // Await both tasks to complete
            string weatherApiResponse = await weatherApiTask;
            //string openWeatherMapResponse = await openWeatherMapTask;

            // Output the raw JSON responses (you can parse and format later)
            Console.WriteLine("WeatherAPI.com Response:");
            Console.WriteLine(weatherApiResponse);
            Console.WriteLine();

            Console.WriteLine("OpenWeatherMap.org Response:");
            //Console.WriteLine(openWeatherMapResponse);
        }
    }
}
