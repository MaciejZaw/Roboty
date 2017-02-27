using System;
using System.Threading;

namespace Roboty
{
	class MainClass
	{
		public static void Main (string[] args)
		{

			Plansza plansza = new Plansza ();
			Gracz gracz = new Gracz ();
			Random rand = new Random ();
			bool czy_czekać; int ilość_robotów;	int poziom = 0; char znak_z_klawiatury;

			plansza.Przywitanie();
			gracz.Wybranie_imienia();

			PoczątekPoziomu: //Tu rozpoczyna się każdy kolejny poziom gry!
			czy_czekać = true;
			gracz.Pozycja_Startowa();
			poziom += 1;
			ilość_robotów = 5 + (poziom * 5);
			int[,] lista_liczb_losowych = new int[2, ilość_robotów];
			for (int i = 0; i < ilość_robotów; i++) {
				lista_liczb_losowych[0, i] = rand.Next(0, 18);
				lista_liczb_losowych[1, i] = rand.Next(1, 76);
				//Uniemożliwienie postawiania robota na miejscu startowym gracza.
				if ( lista_liczb_losowych[0, i] >= 8 && lista_liczb_losowych[0, i] <= 10 && lista_liczb_losowych[1, i] >= 36 && lista_liczb_losowych[1, i] <= 38 )
					i--;
			}
			Robot[] robot = new Robot[ilość_robotów];
			for (int i=0; i < robot.Length; i++) {
				robot [i] = new Robot(lista_liczb_losowych [0, i], lista_liczb_losowych [1, i]);
			}

			PoczątekSekwencji: //Tu rozpoczyna się każda tura!
			plansza.Pusta_plansza();
			//Rysuj roboty i zniszczone roboty, niszcz jeśli są kolizje.
			foreach (Robot roboty in robot) {
				if (plansza.ekran[roboty.x, roboty.y] == '.') {
					plansza.ekran[roboty.x, roboty.y] = '$';
				} else {
					if (roboty.czy_zniszczony == false) {
						roboty.czy_zniszczony = true;
						ilość_robotów--;
						plansza.ekran[roboty.x, roboty.y] = '#';
					} else {
						plansza.ekran[roboty.x, roboty.y] = '#'; 
					}
				}
			}
			foreach (Robot roboty in robot) {
				if (plansza.ekran[roboty.x, roboty.y] == '#') {
					if (roboty.czy_zniszczony == false) {
						roboty.czy_zniszczony = true;
						ilość_robotów--;
					}
					plansza.ekran[roboty.x, roboty.y] = '#';
				}
			}
			//Rysuj gracza lub śmierć gracza, jeśli ta nastąpiła.
			foreach (Robot roboty in robot) {
				if (roboty.x == gracz.x && roboty.y == gracz.y) {
					plansza.ekran[roboty.x, roboty.y] = '@';
					plansza.Rysuj_plansze(gracz.nazwa, poziom, ilość_robotów, plansza.ekran);
					plansza.KończGrę(poziom);
					poziom = 0;
					goto PoczątekPoziomu;
				}
				else
				{
					plansza.ekran[gracz.x, gracz.y] = gracz.litera_gracza;
				}
			}
			plansza.Rysuj_plansze(gracz.nazwa, poziom, ilość_robotów, plansza.ekran);

			//Jeżeli wszystkie roboty zniszczone.
			if (ilość_robotów == 0) {
				plansza.KoniecPoziomu();
				goto PoczątekPoziomu;
			}
			if (czy_czekać == true) {
				//Czeka na akcje gracza.
				znak_z_klawiatury = Console.ReadKey().KeyChar;
				Console.Write("\b");
				Thread.Sleep(255);
				switch (znak_z_klawiatury)
				{
				case 'q': case 'w': case 'e': case 'a': case 's': case 'd': case 'z': case 'x': case 'c':
					if (plansza.Możliwa(znak_z_klawiatury, gracz.x, gracz.y)) {
						foreach (Robot roboty in robot) {
							if (roboty.Sprawdz_kolizje(znak_z_klawiatury, gracz.x, gracz.y) == false && roboty.czy_zniszczony == false)
								goto PoczątekSekwencji;
							if (roboty.czy_zniszczony == true)
							if (gracz.Kolizja (znak_z_klawiatury, roboty.x, roboty.y))
								goto PoczątekSekwencji;
						}
						int x_temp = 0;	int y_temp = 0;
						gracz.Ruch (znak_z_klawiatury, out x_temp, out y_temp);
						gracz.x = x_temp; gracz.y = y_temp;
					} else
						goto PoczątekSekwencji;
					break;
				case 't':
					gracz.Teleportacja();
					break;
				case ' ':
					czy_czekać = false;
					break;
				default:
					goto PoczątekSekwencji;
				}
			} else {
				Thread.Sleep(128);
			}
			foreach (Robot roboty in robot) {
				if (roboty.czy_zniszczony == false)
					roboty.Ruch(gracz.x, gracz.y);
			}
			goto PoczątekSekwencji;
		}
	}
}
