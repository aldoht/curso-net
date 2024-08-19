namespace Etapa1;

public class Views {
    private Cajero cajero = new Cajero();

    public int firstView() {
        int opcion = 0;
        do {
            Console.WriteLine("Banco");
            Console.WriteLine("1. Ingresar una cantidad de retiros.");
            Console.WriteLine("2. Revisar la cantidad ingresada de retiros.");
            Console.WriteLine("3. Salir.");
            Console.Write("Opción: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out opcion);
            if (opcion != 1 && opcion != 2 && opcion != 3) {
                Console.WriteLine("Esa no es una opción válida.");
            }
        } while (opcion != 1 && opcion != 2 && opcion != 3);

        return opcion;
    }

    public int[] firstOption() {
        int cantidadRetiros = 0;
        do {
            Console.WriteLine("¿Cuántos retiros fueron realizados?");
            Console.Write("Cantidad: ");
            string input = Console.ReadLine();
            Int32.TryParse(input, out cantidadRetiros);
            if (cantidadRetiros < 1 || cantidadRetiros > 10) {
                Console.WriteLine("Esa no es una opción válida.");
            }
        } while (cantidadRetiros < 1 || cantidadRetiros > 10);

        int[] retiros = new int[cantidadRetiros];

        for (int i = 0; i < cantidadRetiros; i++) {
            do {
                Console.Write($"Ingrese la cantidad del retiro {i+1}: $");
                string input = Console.ReadLine();
                Int32.TryParse(input, out retiros[i]);
                if (retiros[i] < 1 || retiros[i] > 50000) {
                    Console.WriteLine("Esa no es una opción válida.");
                }
            } while (retiros[i] < 1 || retiros[i] > 50000);
        }

        return retiros;
    }

    public void secondOption(int[] retirosHechos) {
        int actualIndex = 0;
        foreach (int retiro in retirosHechos) {
            int[] conteo = cajero.contarDinero(retiro);
            Console.WriteLine($"Para el retiro {actualIndex+1} se usaron {conteo[0]} billetes y {conteo[1]} monedas.");
            actualIndex++;
        }
    }

    public void finalView() {
        Console.WriteLine("Ha salido exitosamente del programa.");
    }
}