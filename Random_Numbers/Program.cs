using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll")]
    public static extern bool GetCursorPos(out POINT lpPoint);

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;
    }

    static void Main()
    {
        RunRandomNumberGenerator();
    }

    static void RunRandomNumberGenerator()
    {
        Random random = new Random();
        List<int> numbersGenerated = new List<int>();

        for (int i = 0; i < 100; i++)
        {
            POINT cursorPos;
            GetCursorPos(out cursorPos);

            long ticks = DateTime.UtcNow.Ticks; // Tiempo actual con mayor precisión

            int mouseSeed = cursorPos.X + cursorPos.Y + Environment.TickCount;

            random = new Random(mouseSeed);

            int randomNumber = GetRandomNumber(random, 1, 10);
            numbersGenerated.Add(randomNumber);

            Console.WriteLine($"Iteración {i + 1}: Número generado = {randomNumber}:  Valor Cursor = {cursorPos.X} | {cursorPos.Y} Seed = {mouseSeed}" );
            Thread.Sleep(100);
        }
        Console.WriteLine("-----------------------------------------");
        Console.WriteLine("\nFrecuencia de cada número entre 1 y 10:");
        for (int num = 1; num <= 10; num++)
        {
            int frequency = CountFrequency(numbersGenerated, num);
            Console.WriteLine($"Número {num}: {frequency} veces");
        }
    }
    static int GetRandomNumber(Random random, int min, int max)
    {
        return random.Next(min, max + 1);
    }
    static int CountFrequency(List<int> numbers, int numberToCount)
    {
        int count = 0;
        foreach (int num in numbers)
        {
            if (num == numberToCount)
            {
                count++;
            }
        }
        return count;
    }
}
