using System.Collections.Generic;

namespace LibreriaJose;
public class Operacion
{

    public List<int> NumerosImpares = new();

    public int SumarNumeros(int numero1, int numero2)
    {
        return numero1 + numero2;
    }

    public bool IsValorPar(int num)
    {
        return num % 2 == 0;
    }

    public double SumarDecimales(double decimal1, double decimal2)
    {
        return decimal1 + decimal2;
    }

    public List<int> GetListaNumerosImpares(int intervaloMinimo, int intervaloMaximo)
    {
        this.NumerosImpares.Clear();

        for (int i = intervaloMinimo; i <= intervaloMaximo; i++)
        {
            if (i % 2 != 0)
            {
                this.NumerosImpares.Add(i);
            }

        }

        return this.NumerosImpares;
    }
}

