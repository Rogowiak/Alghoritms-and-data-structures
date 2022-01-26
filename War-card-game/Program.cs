//The exercise purpose was to implement queue structure on your own, simple card game served great as an example

Karta[] talia = Talia();
Kolejka gracz1 = new Kolejka();
Kolejka gracz2 = new Kolejka();
potasuj(talia, gracz1, gracz2);
Wojna(gracz1, gracz2);

static void Wojna(Kolejka gracz1, Kolejka gracz2)
{
    Kolejka Stół = new Kolejka();
    bool CzyKoniec = gracz1.CzyPusta() || gracz2.CzyPusta();
    while (!CzyKoniec)
    {
        Console.WriteLine("gracz 1");
        gracz1.wypisz();
        Console.WriteLine("");
        Console.WriteLine("gracz 2");
        gracz2.wypisz();
        Console.WriteLine("");
        Karta k1 = gracz1.Usun();
        Karta k2 = gracz2.Usun();
        Stół.Dodaj(k1);
        Stół.Dodaj(k2);
        int wynik = Porównaj(k1, k2);
        if (wynik == 1)
        {
            while (!Stół.CzyPusta())
            {
                Karta tmp = Stół.Usun();
                gracz1.Dodaj(tmp);
            }
        }
        if (wynik == -1)
        {
            while (!Stół.CzyPusta())
            {
                Karta tmp = Stół.Usun();
                gracz2.Dodaj(tmp);
            }
        }
        if (wynik == 0)
        {
            k1 = gracz1.Usun();
            k2 = gracz2.Usun();
            Stół.Dodaj(k1);
            Stół.Dodaj(k2);
        }
        CzyKoniec = gracz1.CzyPusta() || gracz2.CzyPusta();
    }
    if (gracz1.CzyPusta())
    {
        Console.WriteLine("Grę wygrał gracz2!!!");
        return;
    }
    Console.WriteLine("Grę wygrał gracz1!!!");
    return;
}

static int Porównaj(Karta k1, Karta k2)
{
    k1.Wyswietl();
    Console.Write(" vs ");
    k2.Wyswietl();
    Console.Write(" ");

    if (k1.siła > k2.siła)
    {
        Console.WriteLine("Wygrał gracz1!");
        return 1;
        
    }
    if (k1.siła == k2.siła)
    {
        Console.WriteLine("Wojna!");
        Console.WriteLine("X vs X ");
        return 0;
    }
    if (k1.siła < k2.siła)
    {
        Console.WriteLine("Wygrał gracz2!");
        return -1;
    }
    return 0;
}

static Karta[] Usun(Karta[] talia, int n)
{
    Karta[] arr = new Karta[talia.Length - 1];
    for (int i = 0; i < n; i++)
    {
        arr[i] = talia[i];
    }
    for (int i = n; i < arr.Length; i++)
    {
        arr[i] = talia[i + 1];
    }
    return arr;
}

static Karta[] Talia()
{
    Karta[] talia = new Karta[52];
    int indeks = 0;
    for (int i = 3; i < 7; i++)
    {
        for (int j = 2; j <= 14; j++)
        {
            talia[indeks] = new Karta(j, Convert.ToChar(i));
            indeks++;
        }
    }

    return talia;
}


static void potasuj(Karta[] talia, Kolejka gracz1, Kolejka gracz2)
{
    Random rnd = new Random();    
    int liczba = 51;
    while (liczba > 0)
    {
        int a = rnd.Next(0, liczba);
        gracz1.Dodaj(talia[a]);
        talia = Usun(talia, a);
        liczba--;
        int b = rnd.Next(0, liczba);
        gracz2.Dodaj(talia[b]);
        talia = Usun(talia, b);
        liczba--;
    } 
}



public class Karta
{
    public int siła;
    public string nazwa = "";
    public char kolor;

    public Karta(int siła, char kolor)
    {
        this.siła = siła;
        this.kolor = kolor;
        if (siła < 11)
        {
            this.nazwa = Convert.ToString(siła);
        }
        if (siła == 11)
        {
            this.nazwa = "J";
        }
        if (siła == 12)
        {
            this.nazwa = "Q";
        }
        if (siła == 13)
        {
            this.nazwa = "K";
        }
        if (siła == 14)
        {
            this.nazwa = "A";
        }
        if (siła > 14)
        {
            this.nazwa = "NotACard";
        }
    }

    public void Wyswietl()
    {
        Console.Write(kolor + nazwa + " ");
    }

}


public class Węzeł
{
    public Karta karta;// w węźle przechowujemy liczbę
    public Węzeł następny; // oraz referencję do następnego

    public Węzeł (Karta k)
    {
        this.karta = k;
    }
}

public class Kolejka
{
    Węzeł głowa = null;
    Węzeł ogon = null;

    public void Dodaj(Karta karta)
    {
        Węzeł w = new Węzeł(karta);
        if (głowa == null)
        {
            głowa = ogon = w;
        }
        else
        {
            ogon.następny = w;
            ogon = w;
        }
    }

    public Karta Usun()
    {
        if (głowa == null)
        {
            return null;
        }
        else
        {
            Karta karta = głowa.karta;
            Węzeł tmp = głowa.następny;
            głowa = tmp;
            return karta;
        }
        
    }

    public bool CzyPusta()
    {
        if (głowa == null)
        {
            return true;
        }
        return false;
    }
    public void wypisz()
    {
        for (Węzeł tmp = głowa; tmp != null ; tmp = tmp.następny)
        {
            tmp.karta.Wyswietl();
        }
    }
}


