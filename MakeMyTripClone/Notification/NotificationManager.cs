
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MakeMyTripClone
{
    public enum FromNotificationAlignment
    {
        LeftUp,
        LeftDown,
        RightUp,
        RightDown
    }

    public enum ReArrangeType
    {
        None,
        AutoClose,
        ManualClose,
        PrevNotification
    }

    class NotificationManager:Component
    {
        public FromNotificationAlignment NotificationAlignment
        {
            get
            {
                return notificationAlignment;
            }
            set
            {
                notificationAlignment = value;
                InitialisePositions();
            }
        }
        public int NotificationScreenTime
        {
            get
            {
                return notificationScreenTime;
            }

            set
            {
                notificationScreenTime = value;
            }
        }
        public int BorderRadius { get; set; }

        public NotificationManager()
        {
            notificationWidth = 400;
            notificationHeight = 175;

            screenWidth = Screen.PrimaryScreen.Bounds.Width;
            screenHeight = Screen.PrimaryScreen.Bounds.Height;
            remainingHeight = screenHeight;

            NotifyManagerTimer = new Timer();
            NotifyManagerTimer.Interval = 1000;
            NotifyManagerTimer.Tick += OnNotifyScreenTimerChecker;
            NotifyManagerTimer.Start();
        }

        private void OnNotifyScreenTimerChecker(object sender, EventArgs e)
        {
            if (!NotificationTemplate.isFullContentDisplayed)
            {
                for (int ctr = 0; ctr < NotificationOnScreenList.Count; ctr++)
                {
                    NotificationTemplate Iter = NotificationOnScreenList[ctr];
                    Iter.TickCount += 1;

                    if (Iter.TickCount > notificationScreenTime)
                    {
                        Iter.Close();
                    }
                }
            }
        }

        private bool flag = false;
        private int startX, startY, prevX, prevY, sX, sY;
        private int notificationWidth, notificationHeight;
        private int remainingHeight;
        private int screenWidth, screenHeight;
        private int notificationScreenTime = 5;
        private FromNotificationAlignment notificationAlignment;
        private List<NotificationTemplate> NotificationOnScreenList = new List<NotificationTemplate>();
        private Queue<NotificationTemplate> NotificationQueue = new Queue<NotificationTemplate>();
        private Timer NotifyManagerTimer;

        public void AddNotification(string header, string content)
        {
            NotificationTemplate newNotification = new NotificationTemplate();
            newNotification.BorderRadius = BorderRadius;
            newNotification.Padding = new Padding(BorderRadius / 10);
            newNotification.NotificationHeader = header;
            newNotification.ContentMessage = content;
            newNotification.NotificationTime = DateTime.Now;
            newNotification.Size = newNotification.SetNotificationSize();
            newNotification.NotificationClosed += OnNotificationClosed;
            newNotification.TopMost = true;

            if(newNotification.isContentOutOfHeight)
            {
                if((notificationAlignment == FromNotificationAlignment.LeftDown || notificationAlignment == FromNotificationAlignment.RightDown) && (prevY - 10 > 175))
                {
                    newNotification.Size = new Size(400, prevY - 20);
                }
                else if(NotificationOnScreenList.Count>0 && screenHeight - (prevY + 10 + NotificationOnScreenList[NotificationOnScreenList.Count - 1].Height) - 100 > 175)
                {
                    newNotification.Size = new Size(400, screenHeight - (prevY + 10 + NotificationOnScreenList[NotificationOnScreenList.Count-1].Height) - 10);
                }
                else
                {
                    newNotification.Size = new Size(400, screenHeight - 20);
                }

            }
            
            if (((prevY + (10 + newNotification.Height) * sY) <= 0) && (notificationAlignment == FromNotificationAlignment.LeftDown || notificationAlignment == FromNotificationAlignment.RightDown))
            {
                NotificationQueue.Enqueue(newNotification);
            }
            else if ((NotificationOnScreenList.Count > 0) && ((prevY + newNotification.Height + (10 + NotificationOnScreenList[NotificationOnScreenList.Count - 1].Height) * sY) >= screenHeight) && (notificationAlignment == FromNotificationAlignment.LeftUp || notificationAlignment == FromNotificationAlignment.RightUp))
            {
                NotificationQueue.Enqueue(newNotification);
            }
            else if(NotificationQueue.Count>0)
            {
                NotificationQueue.Enqueue(newNotification);
            }
            else
            {
                SetStartLocation(newNotification);
                newNotification.Location = new Point(startX, startY); /*startY += stepY; startX += stepX;*/
                prevX = startX; prevY = startY;
                newNotification.TickCount = 0;
                NotificationOnScreenList.Add(newNotification);
                newNotification.Show();
            }
        }

        private void OnNotificationClosed(object sender, EventArgs e)
        {
            int Iter = NotificationOnScreenList.Count - 1;
            InitialisePositions();
            for (int ctr = 0; ctr < NotificationOnScreenList.Count; ctr++)
            {
                if(NotificationOnScreenList[ctr] == (sender as NotificationTemplate))
                {
                    Iter = ctr;
                    break;
                }
                else
                {
                    prevX = NotificationOnScreenList[ctr].Location.X;
                    prevY = NotificationOnScreenList[ctr].Location.Y;
                }
            }

            if (Iter == 0)
            {
                SetStartLocation(NotificationOnScreenList[Iter], ReArrangeType.AutoClose);
            }
            else
            {
                if (Iter == NotificationOnScreenList.Count-1)
                    flag = true;
                SetStartLocation(NotificationOnScreenList[Iter], ReArrangeType.ManualClose);
            }

            InitialisePositions();

            for (int ctr = Iter + 1; ctr < NotificationOnScreenList.Count; ctr++)
            {
                NotificationOnScreenList[ctr].Location = new Point(startX, startY);
                prevX = startX;
                prevY = startY;
                SetStartLocation(NotificationOnScreenList[ctr], ReArrangeType.PrevNotification);
            }
            NotificationTemplate temp = sender as NotificationTemplate;
            NotificationOnScreenList.Remove(sender as NotificationTemplate);
            while (NotificationQueue.Count>0 && isValidQueuePop(temp))
            {
                NotificationTemplate newNotification  = NotificationQueue.Peek();
                newNotification.TickCount = 0;
                if(!flag)
                {
                    SetStartLocation(newNotification);
                }
                newNotification.Location = new Point(startX, startY);
                prevX = startX;
                prevY = startY;
                NotificationOnScreenList.Add(newNotification);
                NotificationQueue.Dequeue();
                newNotification.Show();
                flag = false;
            }

        }

        private bool isValidQueuePop(NotificationTemplate temp)
        {
            if((notificationAlignment == FromNotificationAlignment.LeftDown || notificationAlignment == FromNotificationAlignment.RightDown) && (prevY + (10 + NotificationQueue.Peek().Height) * sY) > 0)
            {
                return true;
            }

            if ((notificationAlignment == FromNotificationAlignment.LeftUp || notificationAlignment == FromNotificationAlignment.RightUp) && (NotificationOnScreenList.Count>0) && (prevY + NotificationOnScreenList[NotificationOnScreenList.Count-1].Height + (10 + NotificationQueue.Peek().Height) * sY) <= screenHeight)
            {
                return true;
            }

            if ((notificationAlignment == FromNotificationAlignment.LeftUp || notificationAlignment == FromNotificationAlignment.RightUp) && (NotificationOnScreenList.Count == 0) && (prevY  + (10 + NotificationQueue.Peek().Height) * sY) <= screenHeight)
            {
                return true;
            }

            return false;
        }

        private void SetStartLocation(NotificationTemplate newNotification, ReArrangeType Type = ReArrangeType.None)
        {
            if ((notificationAlignment == FromNotificationAlignment.RightUp || notificationAlignment == FromNotificationAlignment.LeftUp))
            {
                if (NotificationOnScreenList.Count == 0 || Type== ReArrangeType.AutoClose)
                {
                    prevY = -newNotification.Height;
                    startY = prevY + (10 + newNotification.Height) * sY;
                }
                else
                {
                    if (Type == ReArrangeType.PrevNotification)
                    {
                        startY = prevY + (10 + newNotification.Height) * sY;
                    }
                    else if(Type == ReArrangeType.ManualClose)
                    {
                        startY = newNotification.Location.Y;
                    }
                    else
                    {
                        startY = prevY + (10 + NotificationOnScreenList[NotificationOnScreenList.Count - 1].Height) * sY;
                    }
                }
            }
            else
            {
                if (NotificationOnScreenList.Count > 0)
                {
                    if (Type == ReArrangeType.AutoClose || Type == ReArrangeType.ManualClose || Type == ReArrangeType.PrevNotification)
                    {
                        int Iter = NotificationOnScreenList.FindIndex(x => x == newNotification);
                        if(Iter+1<NotificationOnScreenList.Count)
                        startY = prevY - 10 - NotificationOnScreenList[Iter+1].Height;
                    }
                    else
                    {
                        startY = prevY + (10 + newNotification.Height) * sY;
                    }
                }
                else
                {
                    startY = prevY + (10 + newNotification.Height) * sY;
                }
            }

            startX = prevX + (10 + newNotification.Width) * sX;
        }

        private void InitialisePositions()
        {
            if (notificationAlignment == FromNotificationAlignment.RightDown)
            {
                prevX = screenWidth - notificationWidth - 10; prevY = screenHeight; sY = -1; sX = 0;
            }
            else if (notificationAlignment == FromNotificationAlignment.RightUp)
            {
                prevX = screenWidth - notificationWidth - 10; prevY = 10; sY = 1; sX = 0;
            }
            else if (notificationAlignment == FromNotificationAlignment.LeftDown)
            {
                prevX = 10; prevY = screenHeight;  sY = -1; sX = 0;
            }
            else if (notificationAlignment == FromNotificationAlignment.LeftUp)
            {
                prevX = 10; prevY = 10;  sY = 1; sX = 0;
            }
        }
    }
}
