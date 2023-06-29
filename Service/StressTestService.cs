using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Kursak.Help;
using Kursak.Models;
using OpenHardwareMonitor.Hardware;

namespace Kursak.Service
{
    public class StressTestService
    {
        static volatile bool stopThreadsCPU = false;
        static volatile int totalCalculationsCPU = 0;

        public async Task RunHardwareTests(TestSettingsModel testSettings)
        {
            if (testSettings.IsCPUTestEnabled)
            {
                CPUStressTest();
            }

            if (testSettings.IsFPUTestEnabled)
            {
                FPUStressTest();
            }

            if (testSettings.IsCasheTestEnabled)
            {
                CasheStressTest();
            }

            if (testSettings.IsGPUTestEnabled)
            {
                GPUStressTest();
            }

            if (testSettings.IsDiskTestEnabled)
            {
                DiskStressTest();
            }

            if (testSettings.IsRAMTestEnabled)
            {
                RAMStressTest();
            }
        }


        //CPU



        public void CPUStressTest()
        {
            static void Main()
            {
                // Кількість потоків, які будуть використовуватись для стрес-тестування
                Task.Run(() =>
                {
                    stopThreadsCPU = false;
                    int numThreads = 8;

                    // Створюємо масив для зберігання потоків
                    Thread[] threads = new Thread[numThreads];

                    // Створюємо та запускаємо кожен потік
                    for (int i = 0; i < numThreads; i++)
                    {
                        threads[i] = new Thread(StressThread);
                        threads[i].Start();
                    }

                    // Запускаємо таймер на 30 секунд
                    while (Global.IsStartCPU)
                    {
                        Debug.WriteLine("Skalii1");
                        Thread.Sleep(1000);
                    }
                    // Змінюємо флаг, щоб зупинити потоки
                    stopThreadsCPU = true;

                    // Очікуємо завершення всіх потоків
                    foreach (Thread thread in threads)
                    {
                        thread.Join();
                    }
                });
            }

            static void StressThread()
            {
                while (!stopThreadsCPU)
                {
                    CalculateFactorial(100000);
                    Interlocked.Increment(ref totalCalculationsCPU);
                }
            }

            static int CalculateFactorial(int n)
            {
                int result = 1;
                for (int i = 1; i <= n; i++)
                {
                    if (stopThreadsCPU) return 0;
                    result *= i;
                }
                return result;
            }
            Main();
        }

        //FPU
        static volatile bool stopThreadsFPU = false;
        static long totalCalculationsFPU = 0;
        public void FPUStressTest()
        {
            static void Main()
            {
                // Кількість потоків, які будуть використовуватись для стрес-тестування
                Task.Run(() =>
                {
                    stopThreadsFPU = false;
                    int numThreads = 8;

                    // Створюємо масив для зберігання потоків
                    Thread[] threads = new Thread[numThreads];

                    // Створюємо та запускаємо кожен потік
                    for (int i = 0; i < numThreads; i++)
                    {
                        threads[i] = new Thread(FPULoadWorker);
                        threads[i].Start();
                    }

                    // Запускаємо таймер на 30 секунд
                    while (Global.IsStartFPU)
                    {
                        Debug.WriteLine("Skalii2");
                        Thread.Sleep(1000);
                    }
                    // Змінюємо флаг, щоб зупинити потоки
                    stopThreadsFPU = true;

                    // Очікуємо завершення всіх потоків
                    foreach (Thread thread in threads)
                    {
                        thread.Join();
                    }
                });
            }

            static void FPULoadWorker()
            {
                while (!stopThreadsFPU)
                {
                    DoCalculations();
                    Interlocked.Increment(ref totalCalculationsFPU);
                }
            }

            static double DoCalculations()
            {
                double result = 0;

                for (int i = 0; i < 10000000; i++)
                {
                    for (int j = 0; j < 100; j++)
                    {
                        if (stopThreadsFPU) return 0;
                        double calculation = Math.Exp(Math.Sqrt(Math.Sin(100000) + Math.Cos(100000)));
                        result += calculation;
                    }
                }

                return result;
            }

            Main();
        }






        //GPU

        public void GPUStressTest()
        {

        }


        //Cashe
        public void CasheStressTest()
        {

        }



        //RAM

        public void RAMStressTest()
        {

        }




        //Disk
        public void DiskStressTest()
        {

        }
    }
}




