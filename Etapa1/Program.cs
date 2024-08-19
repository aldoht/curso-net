using Etapa1;

Views view = new Views();

int opt = 0;
bool datosIngresados = false;
int[] retirosHechos = null;
do {
    opt = view.firstView();
    switch (opt) {
        case 1:
            retirosHechos = view.firstOption();
            datosIngresados = true;
            Console.Write("Presione enter para continuar... ");
            Console.ReadLine();
            Console.Clear();
            break;
        case 2:
            if (!datosIngresados) {
                Console.WriteLine("Ingrese datos primero.");
                break;
            }
            view.secondOption(retirosHechos);
            Console.Write("Presione enter para continuar... ");
            Console.ReadLine();
            Console.Clear();
            break;
        case 3:
            view.finalView();
            break;
    }
} while (opt != 3);