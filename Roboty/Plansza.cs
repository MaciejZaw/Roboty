using System;
using System.Threading;

namespace Roboty
{
	class Plansza
	{
		char puste_pole = '.';
		public char[,] ekran = new char[18, 76];
			
		public void Pusta_plansza ()
		{//Czyści plansze przed każdą turą.
			for (int i=0; i<18; i++) {
				for (int j=0; j<76; j++) {
					this.ekran [i, j] = this.puste_pole;
				}
			}
		}

		public bool Możliwa(char znak, int x, int y)
		{//Blokowanie możliwości wyjścia gracza poza obszar gry.
			Jednostka jednostka = new Jednostka();
			jednostka.Switch(znak, x, y, out x, out y);
			if ( x >= 0 & y >= 0 & x <= 17 & y <=  75)
				return true;
			return false;
		}
		public void Rysuj_plansze(string gracz, int poziom, int ilość_robotów, char[,] tablica)
		{//Rysuje cały ekran gry, wraz z planszą.
			Console.Clear ();
			Console.WriteLine("Gracz: {0}   Poziom: {1:D2}   Ilość robotów: {2:D3}", gracz, poziom, ilość_robotów);
			Console.WriteLine ("/----------------------------------------------------------------------------\\");
			for (int i=0; i<18; i++) {
				Console.Write("|");
				for (int j=0; j<76; j++){
					Console.Write(tablica[i,j]);
				}
				Console.WriteLine("|");
			}
			Console.WriteLine("\\----------------------------------------------------------------------------/");
		}
		public void Przywitanie ()
		{//Wyświetla ekran powitalny (tylko tekst).
			Console.WriteLine("Witam w grze Roboty.\n");
			Console.WriteLine("Twoim zadaniem jest poruszać się po planszy tak by prymitywne roboty, powpadały\nna siebie. Kiedy dwa lub więcej robotów będą chciały zająć jedno pole, następuje\nkolizja i roboty zostają zniszczone. Kiedy wszystkie roboty zostaną zniszczone,\nprzechodzisz do następnego poziomu.\n");
			Console.WriteLine("Plansza to prostokąt o wymiarach 18 na 76. Sterujesz używać klawiszy: 'q', 'w',\n'e', 'a', 's', 'd', 'z', 'x', 'c', klawisze te reprezentują kierunku ruchu,\ngdzie 's' jest klawiszem czekania.");
			Console.WriteLine("Klawiszem 't', możesz dokonać losowej teleportacji, która wrzuci Cię w losowe\nmiejsce na planszy. Uważaj kiedy prze teleportujesz się w to samo miejsce, które\nzajmuje robot lub miejsce zaraz koło tego miejsca, zginiesz!\n");
			Console.WriteLine("Pierwsze litera twojego imienia, znajdująca się na środku planszy to ty. Roboty\noznaczone są '$', zniszczone roboty oznaczone są '#'.\n");
			Console.WriteLine("Życzę powodzenia! ");
			Console.WriteLine("Wpisz swoje imię:");
		}
		public void KończGrę(int poziom)
		{
			Console.WriteLine("Walczyłeś dzielnie, ale poległeś. Osiągnąłeś poziom {0}. Gratulacje.", poziom);
			Thread.Sleep(512);
			Console.Read(); 
			//Environment.Exit(0);
		}
		public void KoniecPoziomu()
		{
			Console.WriteLine("Poziom ukończony.");
			Thread.Sleep(512);
			Console.Read();
		}
	}
}

