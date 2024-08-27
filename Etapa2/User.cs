using System.Text.Json;

public class User {
    private int _id;
    public int id {
        get { return _id; }
        set { _id = value; }
    }
    private string _email;
    public string email {
        get { return _email; }
        set { _email = value; }
    }
    private double _balance;
    public double balance {
        get { return _balance; }
        set { _balance = value; }
    }
    private char _type;
    public char type {
        get { return _type; }
        set { _type = value; }
    }

    public void deleteUser(int ID) {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string newPath = Path.Combine(docPath, "Etapa2_Files");
        string userFile = Path.Combine(newPath, "users.json");

        if (!File.Exists(userFile)) {
            Console.WriteLine("El archivo de usuarios no existe.");
            return;
        }

        int counter = 0;
        try {
            string str = File.ReadAllText(userFile);
            string[] jsonObjects = str.Split('\n');
            foreach (string json in jsonObjects.SkipLast<string>(1)) {
                User? user = JsonSerializer.Deserialize<User>(json);
                if (user.id == ID) {
                    Console.WriteLine($"Se ha encontrado al usuario con id {ID}.");
                    break;
                }
                counter++;
            }
            
            Console.WriteLine($"No se ha encontrado el usuario con id {ID}.");
        } catch (Exception ex) {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
        }

        try {
            List<string> linesList = File.ReadAllLines(userFile).ToList();            
            linesList.RemoveAt(counter);
            File.WriteAllLines(userFile, linesList.ToArray());
            Console.WriteLine($"Se ha eliminado el usuario con id {ID}.");
            return;
        } catch (Exception ex) {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            return;
        }
    }

    public bool searchUser(int ID) {
        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string newPath = Path.Combine(docPath, "Etapa2_Files");
        string userFile = Path.Combine(newPath, "users.json");

        if (!File.Exists(userFile)) {
            Console.WriteLine("El archivo de usuarios no existe.");
            return false;
        }

        try {
            string str = File.ReadAllText(userFile);
            string[] jsonObjects = str.Split('\n');
            foreach (string json in jsonObjects.SkipLast<string>(1)) {
                User? user = JsonSerializer.Deserialize<User>(json);
                if (user.id == ID) {
                    Console.WriteLine($"Se ha encontrado al usuario con id {ID}.");
                    return true;
                }
            }
            
            Console.WriteLine($"No se ha encontrado el usuario con id {ID}.");
            return false;
        } catch (Exception ex) {
            Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            return false;
        }
    }

    public User() {}

    public User(int ID, string EMAIL, double BALANCE, char TYPE) {
        try {
            id = ID;
            email = EMAIL;
            balance = BALANCE;
            type = TYPE;
        } catch (InvalidOperationException e) {
            Console.WriteLine($"Se ha producido el siguiente error: {e.ToString()}");
            return;
        }


        string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string newPath = Path.Combine(docPath, "Etapa2_Files");
        string userFile = Path.Combine(newPath, "users.json");
        string newData = JsonSerializer.Serialize(this);
        string updatedData = newData + "\n";

        if (!Directory.Exists(newPath))
            Directory.CreateDirectory(newPath);

        File.AppendAllText(userFile, updatedData);
        Console.WriteLine($"El usuario con ID {_id} ha sido creado existosamente.");
    }

}