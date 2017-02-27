using System;

namespace Roboty
{
	class Gracz : Jednostka
	{
		public string nazwa;
		public char litera_gracza;

		public void Pozycja_Startowa()
		{//Ustawia pozycje gracza na początku każdego poziomu.
			base.x = 9;	base.y = 37;
		}

		public void Teleportacja()
		{//Losowa teleportacja.
			Random rand = new Random();
			base.x = rand.Next(1,17);
			base.y = rand.Next(1,76);
		}

		public bool Kolizja(char znak, int robot_x, int robot_y)
		{//Sprawdza czy gracz po wykonaniu ruchu zderzy się z robotem.
			int x_gracza = base.x, y_gracza = base.y;
			Switch(znak, base.x, base.y, out x_gracza, out y_gracza);
			if ( x_gracza == robot_x && y_gracza == robot_y ) 
				return true; 
			else
				return false;
		}

		public void Wybranie_imienia ()
		{//Nadanie imienia gracz i wyznaczenie literki gracza na planszy.
			this.nazwa = Console.ReadLine ();
			if (this.nazwa.Length > 35) {
				this.nazwa = this.nazwa.Remove (35);
			} else {
				this.nazwa = this.nazwa.PadRight (35);
			}
			if (string.IsNullOrEmpty (this.nazwa))
				this.nazwa = "Nieznany";
			if ( nazwa[0] == '$' || nazwa[0] == '#' || nazwa[0] == '@' || nazwa[0] == '.' || nazwa[0] == ',' )
				this.nazwa = "NiewłaściwyPierwszyZnak";
			this.litera_gracza = nazwa[0];
		}
	}
}

