using System;
using System.Collections.Generic;

namespace NotificationManager
{
    // интерфейс для контрвариантности
    public interface INotificationProvider<out T>
    {
        IEnumerable<T> GetNotifications();
    }

    // Обобщённый класс
    public class NotificationContainer<T> : INotificationProvider<T> where T : Notification, IComparable<T>
    {
        private List<T> notifications = new List<T>();
        public List<T> Notifications
        {
            get => notifications;
            private set => notifications = value;
        }

        public void AddNotification(T notification){
            notifications.Add(notification);
        }

        public void ShowAll(){
            foreach (var notification in notifications){
                Console.WriteLine(notification.ToString());
            }
        }

        public IEnumerable<T> GetNotifications()
        {
            return notifications;
        }

         // Сортировка уведомлений
        public void Sort()
        {
            notifications.Sort();
        }

        public bool HasNotifications(){
            return notifications.Count > 0; 
        }
    }

    // базовый класс Notification
    public abstract class Notification : IComparable<Notification>
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public Notification(string message, DateTime date)
        {
            Message = message;
            Date = date;
        }

        public int CompareTo(Notification other)
        {
            if (other == null) return 1;
            return Date.CompareTo(other.Date);
        }

        public override string ToString()
        {
            return $"{Date}: {Message}";
        }
    }

    // Класс для смс сообщений 
    public class SmsNotification : Notification
    {
        public string PhoneNumber { get; set; }

        public SmsNotification(string message, DateTime date, string phoneNumber) : base(message, date)
        {
            PhoneNumber = phoneNumber;
        }

        public override string ToString()
        {
            return $"SMS на номер {PhoneNumber}: {Message} - {Date}";
        }

        // Метод проверки корректности номера
        public bool ValidatePhoneNumber()
        {
            return PhoneNumber.Length == 11 && PhoneNumber.All(char.IsDigit);
        }
            }

    public class EmailNotification : Notification
    {
        public string Sender { get; set; }

        public EmailNotification(string sender, string message, DateTime date)
            : base(message, date)
        {
            Sender = sender;
        }

        public string GenerateEmailTemplate(string username)
        {
            return $"Здравствуйте, {username}! Спасибо, что пользуетесь нашими услугами.";
        }
       
        public override string ToString()
        {
            return $"[Email от {Sender}] {base.ToString()}";
        }
    }

    public class PushNotification : Notification
    {
        public string DeviceId { get; set; }

        public PushNotification(string message, DateTime date, string deviceId) : base(message, date)
        {
            DeviceId = deviceId;
        }

        // Специфичный метод для Push
        public void SendPush()
        {
            if (this.CheckDeviceConnection()){
                Console.WriteLine($"Отправка Push-уведомления на устройство {DeviceId}: {Message}");
            } else
            {
                this.CancelPush();
            }
        }

        // Специфичный метод для Push
        public bool CheckDeviceConnection()
        {
            // Предположим: если DeviceId не пустой, устройство подключено
            return !string.IsNullOrEmpty(DeviceId);
        }

        public void CancelPush()
        {
            Console.WriteLine($"PUSH-уведомление для {DeviceId} отменено.");
        }
    }
}