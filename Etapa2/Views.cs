using System.Text.RegularExpressions;

public class Views {
    public int mainView() {
        int option = 0;
        do {
            Console.WriteLine("Seleccione una opción: ");
            Console.WriteLine("1. Crear un usuario nuevo.");
            Console.WriteLine("2. Eliminar un usuario existente.");
            Console.WriteLine("3. Salir.");
            Console.Write("Opción: ");
            Int32.TryParse(Console.ReadLine(), out option);
            if (option < 1 || option > 3) {
                Console.WriteLine("Esa opción no es válida.");
            }
        } while (option < 1 || option > 3);
        
        lineSeparator();
        return option;
    }

    public void lineSeparator() { Console.WriteLine("------------------------------"); }

    public void firstOption() {
        int id = 0;
        string email = "";
        double balance = 0.0;
        char type = 'a';

        User user = new User();
        bool exists = true;
        do {
            Console.Write("Ingrese el ID para el usuario: ");
            Int32.TryParse(Console.ReadLine(), out id);
            if (id <= 0)
                Console.WriteLine("El ID no puede ser negativo ni cero.");
            else if (exists = user.searchUser(id))
                Console.WriteLine("El usuario con esa ID ya existe.");
        } while (id <= 0 || exists);

        bool validEmail;
        do
        {
            Console.Write("Ingrese su correo: ");
            email = Console.ReadLine();
            string strRegex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            Regex re = new Regex(strRegex);
            if (validEmail = !re.IsMatch(email)) Console.WriteLine("Correo no válido.");
        } while (validEmail);

        do {
            Console.Write("Ingrese el balance del usuario: ");
            Double.TryParse(Console.ReadLine(), out balance);
            if (balance <= 0.0) Console.WriteLine("El balance no puede ser cero ni negativo.");
        } while (balance <= 0.0);

        do {
            Console.Write("Ingrese el tipo de usuario (c si es cliente o e si es empleado): ");
            Char.TryParse(Console.ReadLine(), out type);
            if (type != 'c' && type != 'e') Console.WriteLine("Carácter no válido.");
        } while (type != 'c' && type != 'e');

        User newUser = new(id, email, balance, type);
    }

    public void secondOption() {
        int id = 0;
        User user = new();

        do {
            Console.Write("Ingrese el ID del usuario a eliminar: ");
            Int32.TryParse(Console.ReadLine(), out id);
        } while (!user.searchUser(id));

        user.deleteUser(id);
    }
}