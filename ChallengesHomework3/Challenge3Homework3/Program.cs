using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        double sumaTotal = 0;
        int cantidadNotas = 0;
        string entrada;

        Console.WriteLine("Ingrese las notas de los estudiantes (0-10) o escriba 'fin' para calcular el promedio");
        int indiceConcepto = 1;
        while (true)
        {
            string conceptoActual = $"Tarea {indiceConcepto}";

            Console.WriteLine($"Ingrese nota para {conceptoActual}:");
            entrada = Console.ReadLine().ToLower();

            if (entrada == "fin") break;

            if (double.TryParse(entrada, out double nota) && nota >= 0 && nota <= 10)
            {
                sumaTotal += nota;
                cantidadNotas++;
                indiceConcepto++;
            }
            else
            {
                Console.WriteLine("Entrada no válida. Por favor, ingrese una nota entre 0 y 10 o 'fin' para terminar.");
            }
        }
        if (cantidadNotas > 0)
        {
            double promedio = sumaTotal / cantidadNotas;
            Console.WriteLine($"Cantidad de notas ingresadas: {cantidadNotas}");
            Console.WriteLine($"El promedio de las notas ingresadas es: {promedio:F2}");
        }
        else
        {
            Console.WriteLine("No se ingresaron notas válidas para calcular el promedio.");
        }
        Console.WriteLine("Presione cualquier tecla para salir.");
        Console.ReadKey();
    }
}