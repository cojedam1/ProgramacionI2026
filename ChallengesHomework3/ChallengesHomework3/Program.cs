// See https://aka.ms/new-console-template for more information
Console.WriteLine("Ingresa un numero entero positivo:");
int numero = int.Parse(Console.ReadLine());

Console.WriteLine($"Numeros pares de 1 hasta {numero}:");
for (int i = 0; i <= numero; i++)
{
    if (i % 2 == 0)
    {
        Console.WriteLine(i);
    }
}

Console.WriteLine("Presione Cualquier tecla para salir");
Console.ReadKey();