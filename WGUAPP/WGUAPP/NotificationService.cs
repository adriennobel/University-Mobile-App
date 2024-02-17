using Plugin.LocalNotification;

namespace WGUAPP
{
    public static class NotificationService
    {
        private static int LastID = 0;

        public static int RegisterNotification(string title, string description, DateTime notifyTime)
        {
            LastID++;

            var request = new NotificationRequest
            {
                NotificationId = LastID,
                Title = title,
                Subtitle = "Degree Plan",
                Description = description,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime,
                    //NotifyRepeatInterval = TimeSpan.FromSeconds(15),
                    //RepeatType = NotificationRepeat.Daily,
                }
            };

            LocalNotificationCenter.Current.Show(request);
            return LastID;
        }

        public static void UpdateRegisteredNotification(int notificationID, string title, string description, DateTime notifyTime)
        {
            // Cancel and clear old notification request
            LocalNotificationCenter.Current.Cancel(notificationID);
            LocalNotificationCenter.Current.Clear(notificationID);

            // Create a new notification request
            var request = new NotificationRequest
            {
                NotificationId = notificationID,
                Title = title,
                Subtitle = "Degree Plan",
                Description = description,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = notifyTime,
                }
            };

            LocalNotificationCenter.Current.Show(request);
        }

        public static void ClearAndCancelNotification(int notificationID)
        {
            // Cancel and clear old notification request
            LocalNotificationCenter.Current.Cancel(notificationID);
            LocalNotificationCenter.Current.Clear(notificationID);
        }
    }
}