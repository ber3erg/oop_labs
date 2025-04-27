namespace NotificationManager
{
    class Program
    {
        static void Main(){
            // Контейнер для SMS уведомлений
            var smsContainer = new NotificationContainer<SmsNotification>();
            smsContainer.AddNotification(new SmsNotification("Ваш код: 1234", DateTime.Now.AddMinutes(-10), "89991234567"));
            smsContainer.AddNotification(new SmsNotification("Ваш баланс пополнен", DateTime.Now.AddMinutes(-5), "89991112233"));

            // Контейнер для Email уведомлений
            var emailContainer = new NotificationContainer<EmailNotification>();
            emailContainer.AddNotification(new EmailNotification("support@site.com", "Ваш заказ подтверждён", DateTime.Now.AddHours(-1)));
            emailContainer.AddNotification(new EmailNotification("noreply@service.com", "Аккаунт активирован", DateTime.Now.AddHours(-2)));

            // Контейнер для Push уведомлений
            var pushContainer = new NotificationContainer<PushNotification>();
            pushContainer.AddNotification(new PushNotification("Вам новое сообщение", DateTime.Now.AddMinutes(-30), "Device_123"));
            pushContainer.AddNotification(new PushNotification("Обновление приложения", DateTime.Now.AddMinutes(-15), "Device_456"));

            // Вывод содержимого контейнеров
            Console.WriteLine("=== СМС уведомления ===");
            smsContainer.ShowAll();
            Console.WriteLine("\n=== Email уведомления ===");
            emailContainer.ShowAll();
            Console.WriteLine("\n=== Push уведомления ===");
            pushContainer.ShowAll();

            // Проверка наличия уведомлений
            Console.WriteLine($"\nSMS уведомлений: {(smsContainer.HasNotifications() ? "есть" : "нет")}");
            Console.WriteLine($"Email уведомлений: {(emailContainer.HasNotifications() ? "есть" : "нет")}");
            Console.WriteLine($"Push уведомлений: {(pushContainer.HasNotifications() ? "есть" : "нет")}");

            // Сортировка уведомлений
            Console.WriteLine("\n=== Сортированные СМС уведомления ===");
            smsContainer.Sort();
            smsContainer.ShowAll();

            Console.WriteLine("\n=== Сортированные Email уведомления ===");
            emailContainer.Sort();
            emailContainer.ShowAll();

            Console.WriteLine("\n=== Сортированные Push уведомления ===");
            pushContainer.Sort();
            pushContainer.ShowAll();

            // ковариантность
            INotificationProvider<Notification> provider = smsContainer;

            // Теперь можно получить все уведомления через базовый тип Notification
            Console.WriteLine("\n=== Демонстрация ковариантности ===");
            foreach (var notification in provider.GetNotifications())
            {
                Console.WriteLine(notification);
            }
        }
    }
}