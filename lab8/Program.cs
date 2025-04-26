namespace TranportNavigation
{
    public struct Point{
        public double CoordinateX;
        public double CoordinateY;

        public string GetPoint(){
            return $"{CoordinateX} {CoordinateY}";
        }
    }

    public abstract class Transport{
        public Point TransportPoint { get; set; }
        public Transport(Point point){
            TransportPoint = point;
        }
        public abstract string GetTransportType();
        public abstract string GetMassageGoToExit();
        public abstract void MoveTo(Point point);
        public abstract string GetInfo();
    }

    public class Car : Transport
    {
        public string Model { get; set; }
        public string RegisterNumber { get; set; }
        private int fuelLevel;
        public int FuelLevel 
        { 
            get => fuelLevel;
            set{
                if (value > 40){
                    Console.WriteLine("Уровень топлива не может быть больше 40");
                    fuelLevel = 40;
                } 
                if (value < 0){
                    Console.WriteLine("Уровень топлива не может быть отрицательным");
                    fuelLevel = 0;
                }
                else fuelLevel = value;
            }
        }

        public Car(Point carPoint, string model, string registerNumber, int fuelLevel)
        : base(carPoint){
            Model = model;
            RegisterNumber = registerNumber;
            FuelLevel = fuelLevel;
        }

        public override void MoveTo(Point point){
            if (FuelLevel - 1 > 0){
                TransportPoint = point;
                FuelLevel--;
            }
            else Console.WriteLine("Топлива нет, передвижение невозможно");
        }
        

        public override string GetTransportType(){
            return "Автомобиль";
        }

        public override string GetMassageGoToExit(){
            return "Вы прибыли к точке назначения, не забудьте закрыть автомобиль";
        }
        public override string GetInfo(){
            return $"Автомобиль номер: {RegisterNumber}\nМестоположение: {TransportPoint.GetPoint()}\nМодель: {Model}\nУровень топлива: {FuelLevel}%";
        }

        public void ToRefuel(int count){
            FuelLevel = FuelLevel + count;
        }

    }

    public class Bus : Transport
    {
        public string RegisterNumber { get; set; }
        public string RouteNumber { get; set; }

        private int batteryLevel;

        public int BatteryLevel
        {
            get => batteryLevel;
            set
            {
                if (value > 100)
                {
                    Console.WriteLine("Уровень заряда не может быть больше 100%");
                    batteryLevel = 100;
                }
                else if (value < 0)
                {
                    Console.WriteLine("Уровень заряда не может быть меньше 0%");
                    batteryLevel = 0;
                }
                else
                {
                    batteryLevel = value;
                }
            }
        }

        public Bus(Point busPoint, string registerNumber, string routeNumber, int batteryLevel)
            : base(busPoint)
        {
            RegisterNumber = registerNumber;
            RouteNumber = routeNumber;
            BatteryLevel = batteryLevel;
        }

        public override void MoveTo(Point point)
        {
            if (BatteryLevel - 5 > 0)
            {
                TransportPoint = point;
                BatteryLevel -= 5;
            }
            else
            {
                Console.WriteLine("Недостаточно заряда для передвижения.");
            }
        }
        public void ChargeBattery(int amount)
        {
            Console.WriteLine($"Зарядка автобуса на {amount}%...");
            BatteryLevel += amount;
        }

        public override string GetTransportType(){
            return "Автобус";
        }
        
        public override string GetMassageGoToExit(){
            return "Остановка Павла Корчагина, следующая остановка Московский Политех";
        }
        public override string GetInfo(){
            return $"Автобус номер: {RegisterNumber}\nМестоположение: {TransportPoint.GetPoint()}\nМаршрут: {RouteNumber}\nУровень заряда: {BatteryLevel}";
        }

        // Объявить следующую остановку
        public void AnnounceNextStop(string stopName)
        {
            Console.WriteLine($"Следующая остановка: {stopName}");
        }

        // Проверка количества пассажиров
        public void CheckPassengerCount(int count)
        {
            Console.WriteLine($"В автобусе сейчас {count} пассажиров.");
        }
    }

    public class Tram : Transport
    {
        public int InventNumber { get; set; }
        public string RouteNumber { get; set; }
        private bool isConnectedToRails;

        public bool IsConnectedToRails
        {
            get => isConnectedToRails;
            set => isConnectedToRails = value;
        }

        public Tram(Point tramPoint, int inventNumber, string routeNumber, bool isConnected)
            : base(tramPoint)
        {
            InventNumber = inventNumber;
            RouteNumber = routeNumber;
            IsConnectedToRails = isConnected;
        }

        public override void MoveTo(Point point)
        {
            if (IsConnectedToRails)
            {
                TransportPoint = point;
            }
            else
            {
                Console.WriteLine("Трамвай не подключён к рельсам, движение невозможно.");
            }
        }

        public void ConnectToRails()
        {
            IsConnectedToRails = true;
            Console.WriteLine("Трамвай подключен к рельсам и готов к движению.");
        }

        public void DisconnectFromRails()
        {
            IsConnectedToRails = false;
            Console.WriteLine("Трамвай отключён от рельсов.");
        }

        public override string GetTransportType(){
            return "Трамвай";
        }
        public override string GetMassageGoToExit(){
            return "Остановка Станция Юных натуралистов (по требованию), следующая остановка Московский Политех";
        }
        
        public override string GetInfo(){
            return $"Трамвай номер: {InventNumber}\nМестоположение: {TransportPoint.GetPoint()}\nМаршрут: {RouteNumber}\nПодключение к контактам: {(IsConnectedToRails ? "Да" : "Нет")}";
        }

        // Объявить следующую остановку
        public void AnnounceNextStop(string stopName)
        {
            Console.WriteLine($"Следующая остановка: {stopName}");
        }

        // Проверить напряжение контактной сети
        public void CheckElectricSupply()
        {   
            Console.WriteLine($"Трамвай {InventNumber}: напряжение в контактной сети в норме.");
        }
    }

    class Program
    {
        static void Main()
    {
        // Создаём точки
        Point startPoint = new Point { CoordinateX = 0, CoordinateY = 0 };
        Point destinationPoint = new Point { CoordinateX = 10, CoordinateY = 15 };

        // Создаём разные виды транспорта
        Car car = new Car(startPoint, "Toyota Camry", "A123BC", 30);
        Bus bus = new Bus(startPoint, "B456DE", "12A", 80);
        Tram tram = new Tram(startPoint, 1001, "5", true);

        // Вызываем функцию с разными экземплярами
        ShowTransportInfo(car);
        Console.WriteLine(new string('-', 50));
        ShowTransportInfo(bus);
        Console.WriteLine(new string('-', 50));
        ShowTransportInfo(tram);
    }

    static void ShowTransportInfo(Transport transport)
    {
        // Вывод общей информации о транспорте
        Console.WriteLine(transport.GetInfo());

        // Попробуем сдвинуть транспорт

        Console.WriteLine($"Координаты до перемещения: {transport.TransportPoint.GetPoint()}");
        Point newPoint = new Point { CoordinateX = transport.TransportPoint.CoordinateX + 5, CoordinateY = transport.TransportPoint.CoordinateY + 5 };
        transport.MoveTo(newPoint);

        Console.WriteLine($"Координаты после перемещения: {transport.TransportPoint.GetPoint()}");

    }
    }
}