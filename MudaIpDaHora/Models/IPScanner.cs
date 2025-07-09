using System;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;

namespace MudaIpDahora.Models
{
    public class IPScanner
    {
        public delegate void ProgressChangedHandle(int progress);
        public delegate void FinishHandle();
        public delegate void DeviceFoundHandle(string ip, int index);

        public event ProgressChangedHandle OnProgressChanged;
        public event DeviceFoundHandle ONIPFound;
        public event FinishHandle OnFinish;
        public SemaphoreSlim Semaphore { get; set; } = new SemaphoreSlim(20);
        private List<Thread> threadPingList = new List<Thread>();
        private Thread mainTread;
        private int _progress;
        public int Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnProgressChanged?.Invoke(_progress);
                }
            }
        }
        public void Scan(IPAddress ip, int timeOut)
        {
            if (threadPingList.Count > 0) return;

            mainTread = new Thread(() =>
            {
                try
                {
                    Progress = 0;

                    for (int i = 0; i < 254; i++)
                    {
                        Semaphore.Wait();

                        Thread t = new Thread(() =>
                        {
                            try
                            {
                                string[] ipArray = ip.ToString().Split('.');
                                ipArray[3] = i.ToString();
                                string ipString = string.Join(".", ipArray);
                                if (PingIP(ipString, timeOut))
                                {
                                    ONIPFound?.Invoke(ipString, i);
                                }
                                Progress++;
                            }
                            finally
                            {
                                Semaphore.Release();
                            }
                        });
                        t.Start();
                        threadPingList.Add(t);
                    }

                    foreach (Thread t in threadPingList)
                    {
                        t.Join();
                    }
                    threadPingList.Clear();
                }
                finally
                {
                    Progress = 254;
                    OnFinish?.Invoke();
                }
            });

            mainTread.Start();
        }
        public bool PingIP(string ip, int timeOut)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(ip, timeOut);
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
