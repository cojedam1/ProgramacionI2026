
Console.Clear();
Console.WriteLine("Verificador de Numeros Primos");
Console.WriteLine("Un número primo es un número natural mayor que 1 que tiene únicamente dos divisores positivos: él mismo y el 1.");
Console.WriteLine("Ingresa el numero que deseas verificar:");

if (int.TryParse(Console.ReadLine(), out int numero))
{
    if (numero <= 1)
    {
        Console.WriteLine($"{numero} no es posible verificar si es primo o no, ya que los numeros primos son mayores a 1");
    }
    else
    {
        bool esPrimo = true;
        for (int i = 2; i < numero; i++)
        {
            if (numero % i == 0)
            {
                esPrimo = false;
                esPrimo = false;
                break;
            }
        }
        if (esPrimo)
        {
            Console.WriteLine($"El numero {numero} es primo.");
        }
        else
        {
            Console.WriteLine($"El numero {numero} no es primo.");
        }
    }
}
Console.WriteLine("Por favor, ingresa un numero entero valido.");
Console.WriteLine("Presione Cualquier tecla para salir");
Console.ReadKey();