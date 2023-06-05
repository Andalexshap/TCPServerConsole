using System.Net;
using System.Net.Sockets;
using System.Text;
using ServiceBySocket.Extensions;
using ServiceBySocket.Models;
using ServiceBySocket.Services;

//Консольное расширение для разного цвета
var console = new WriteConsoleExtend();

//ip адрес позже можно брать из конфига
var ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });

var tcpListener = new TcpListener(ipAddress, 8888);

var _carService = new CarService();

try
{
    tcpListener.Start();
    console.WriteMessage($"Сервер запущен. Адресс: {ipAddress.ToString()}");

    while (true)
    {
        var tcpClient = await tcpListener.AcceptTcpClientAsync();
        console.WriteMessage($"Клиент подключен: {tcpClient.Client.RemoteEndPoint}");

        Task.Run(async () =>
        {
            await ProcessClientAsync(tcpClient);
        });
    }
}
catch (Exception e)
{
    console.Error(e.Message);
}
finally
{
    tcpListener.Stop();
}

async Task ProcessClientAsync(TcpClient tcpClient)
{
    var stream = tcpClient.GetStream();

    var response = new List<byte>();
    int bytesRead = 10;
    while (true)
    {
        // считываем данные до конечного символа
        while ((bytesRead = stream.ReadByte()) != '\n')
        {
            // добавляем в буфер
            response.Add((byte)bytesRead);
        }

        var word = Encoding.UTF8.GetString(response.ToArray());
        console.WriteMessage($"Получено сообщение: {word}");

        switch (word)
        {
            case "END":
                console.Warning($"Клиент: {tcpClient.Client.RemoteEndPoint} завершил работу");
                break;
            case "cars":
                console.WriteMessage($"Запрос получения всех авто. Клиент: {tcpClient.Client.RemoteEndPoint}");
                var cars = GetSomeCars();//_carService.GetAllCart();
                var answer = cars.ConvertCarsToByteForSend();

                answer.Add(0x10);

                await stream.WriteAsync(answer.ToArray());
                response.Clear();
                break;
        }

        if (word.StartsWith("car:"))
        {
            var id = word.Split(':')[1];
            console.WriteMessage($"Запрос получения авто по id: {id}. Клиент: {tcpClient.Client.RemoteEndPoint}");
            var car = GetOneCar(0); //_carService.GetCarById(id);

            var answer = car.ConvertCarToByteForSend();

            answer.Add(0x10);

            await stream.WriteAsync(answer.ToArray());
            response.Clear();
        }

        // если прислан маркер окончания взаимодействия,
        // выходим из цикла и завершаем взаимодействие с клиентом
        if (word == "END")
        {
            console.Warning($"Клиент: {tcpClient.Client.RemoteEndPoint} завершил работу");
            break;
        }

        response.Clear();
    }
    tcpClient.Close();
}

Cars GetSomeCars()
{
    var cars = new Cars
    {
        ListCars = new List<Car>()
    };

    cars.ListCars.Add(GetOneCar(1));
    cars.ListCars.Add(GetOneCar(2));

    return cars;
}

Car GetOneCar(int count)
{
    var firstCar = new Car
    {
        Id = Guid.NewGuid(),
        Model = "Mazda",
        Year = 2007,
        DoorsCount = 5,
        EngineCapacity = 2.0F
    };

    var secondCar = new Car
    {
        Id = Guid.NewGuid(),
        Model = "Nissan",
        Year = 2008,
        EngineCapacity = 1.6F
    };

    var thirdCar = new Car();

    switch (count)
    {
        case 0: return thirdCar;
        case 1: return firstCar;
        case 2: return secondCar;
        default:
            break;
    }
    return thirdCar;
}


