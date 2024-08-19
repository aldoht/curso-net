namespace Etapa1;

public class Cajero {
    public int[] contarDinero(int cantidad) {
        int[] dinero = {0, 0, 0, 0, 0, 0, 0, 0};
        int[] valores = {500, 200, 100, 50, 20, 10, 5, 1};
        int[] salida = new int[2];

        int actualIndex = 0;
        foreach (int elem in valores) {
            while (cantidad >= elem) {
                dinero[actualIndex]++;
                cantidad -= valores[actualIndex];
                if (actualIndex <= 4) {
                    salida[0]++;
                }
                else {
                    salida[1]++;
                }
            }
            actualIndex++;
        }


        return salida;
    }
}