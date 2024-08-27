Views view = new Views();
int opt = 0;

do {
    opt = view.mainView();
    switch (opt) {
        case 1:
            view.firstOption();
            break;
        case 2:
            view.secondOption();
            break;
        case 3:
            Console.WriteLine("Ha salido del programa.");
            break;
    }
    view.lineSeparator();
} while (opt != 3);
