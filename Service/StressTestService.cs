using System;
using System.Diagnostics;
using System.IO;
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
                Global.IsStartCPU = true;
                CPUStressTest();
            }

            if (testSettings.IsFPUTestEnabled)
            {
                Global.IsStartFPU = true;

                FPUStressTest();
            }

            if (testSettings.IsCasheTestEnabled)
            {
                Global.IsStartCashe= true;
                CasheStressTest();
            }

            if (testSettings.IsGPUTestEnabled)
            {
                Global.IsStartGPU = true;
                GPUStressTest();
            }

            if (testSettings.IsDiskTestEnabled)
            {
                Global.IsStartDisk = true;
                DiskStressTest();
            }

            if (testSettings.IsRAMTestEnabled)
            {
                Global.IsStartRAM = true;
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





        //Cashe
        static volatile bool stopThreadsCashe = false;
        static volatile int totalCalculationsCashe = 0;

        public void CasheStressTest()
        {
            static void Main()
            {
                Task.Run(() =>
                {
                    stopThreadsCashe = false;
                    int numThreads = 8;

                    // Створюємо масив для зберігання потоків
                    Thread[] threads = new Thread[numThreads];

                    // Створюємо та запускаємо кожен потік
                    for (int i = 0; i < numThreads; i++)
                    {
                        threads[i] = new Thread(CacheCPULoadWorker);
                        threads[i].Start();
                    }

                    while (Global.IsStartCashe)
                    {
                        Debug.WriteLine("Skalii3");
                        Thread.Sleep(1000);
                    }
                    stopThreadsCashe = true;

                    foreach (Thread thread in threads)
                    {
                        thread.Join();
                    }
                });
            }

            static void CacheCPULoadWorker()
            {

                while (!stopThreadsCashe)
                {
                    DoCalculations();
                    Interlocked.Increment(ref totalCalculationsCashe);
                }


            }
            static int DoCalculations()
            {
                int result = 0;

                for (int i = 0; i < 1000000; i++)
                {

                    result += i * i;
                }

                return result;
            }
            Main();
        }




        //RAM
        static volatile bool stopThreadsRAM = false;
        static volatile int totalCalculationsRAM = 0;
        public void RAMStressTest()
        {
            static void Main()
            {
                Task.Run(() =>
                {
                    stopThreadsRAM = false;
                    int numThreads = 8;

                    // Створюємо масив для зберігання потоків
                    Thread[] threads = new Thread[numThreads];

                    // Створюємо та запускаємо кожен потік
                    for (int i = 0; i < numThreads; i++)
                    {
                        threads[i] = new Thread(MemoryCPULoadWorker);
                        threads[i].Start();
                    }

                    while (Global.IsStartRAM)
                    {
                        Debug.WriteLine("Skalii3");
                        Thread.Sleep(1000);
                    }
                    stopThreadsRAM = true;
                });
            }
            static int DoMemoryCalculations()
            {
                try
                {
                    int[] array = new int[500000000];
                    int result = 0;

                    for (int i = 0; i < array.Length; i++)
                    {
                        if (stopThreadsRAM) return 0;
                        result += array[i];
                    }

                    return result;
                }
                catch
                {
                    throw;
                }
            }

            static void MemoryCPULoadWorker()
            {

                while (!stopThreadsRAM)
                {

                    DoMemoryCalculations();
                    Interlocked.Increment(ref totalCalculationsRAM);

                }
            }
            Main();

        }

        //Disk
        public void DiskStressTest()
        {
            static void Main()
            {
                Task.Run(() =>
                {
                    const long FileSizeInBytes = 1000000000; // Size of the file to write in bytes (adjust as needed)
                    const string FilePath = "disk_stress_test.bin"; // Path of the file to create

                    // Create a large file
                    using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
                    {
                        // Set the file size
                        fileStream.SetLength(FileSizeInBytes);

                        // Write data to the file
                        byte[] data = new byte[4096]; // Buffer size
                        long remainingBytes = FileSizeInBytes;
                        while (remainingBytes > 0 && Global.IsStartDisk)
                        {
                            int bytesToWrite = (int)Math.Min(data.Length, remainingBytes);
                            fileStream.Write(data, 0, bytesToWrite);
                            remainingBytes -= bytesToWrite;
                        }
                    }
                    File.Delete(FilePath);
                });
            }
            Main();
        }


        //GPU
        public void GPUStressTest()
        {

        }
    }

}




