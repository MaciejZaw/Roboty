using System;

namespace Roboty
{
	public class Jednostka
	{
		public int x {get;set;}
		public int y {get;set;}

		public void Ruch(char znak, out int out_x, out int out_y)
		{//Gracz wykonuje ruch.
			Switch(znak, this.x, this.y, out out_x, out out_y);
		}
		public void Ruch( int gracz_x, int gracz_y )
		{//Robot wykonuje ruch.
			if ( this.x > gracz_x )
			{
				this.x--;
			}
			else if ( this.x < gracz_x )
			{
				this.x++;
			}
			if ( this.y > gracz_y )
			{
				this.y--;
			}
			else if ( this.y < gracz_y )
			{
				this.y++;
			}
		}
		public bool Sprawdz_kolizje(char znak, int gracz_x, int gracz_y)
		{//Sprawdza czy gracz będzie bezpieczny po wykonaniu ruchu.
			int x_gracza, y_gracza;
			int x_robota = this.x, y_robota = this.y;
			Switch(znak,gracz_x,gracz_y,out x_gracza, out y_gracza);
			if ( x_robota > x_gracza )
			{
				x_robota--;
			}
			else if ( this.x < x_gracza )
			{
				x_robota++;
			}
			if ( y_robota > y_gracza )
			{
				y_robota--;
			}
			else if ( this.y < y_gracza )
			{
				y_robota++;
			}
			if ( x_robota == x_gracza && y_robota == y_gracza )
				return false;
			else
				return true;
		}

		public void Switch(char znak, int x, int y, out int out_x, out int out_y)
		{//Zamienia informacje z klawiatury na kierunki na planszy.
			out_x = x; out_y = y;
			switch (znak) {
			case 'q': out_x -= 1; 	out_y -= 1;		break;
			case 'w': out_x -= 1;					break;
			case 'e': out_x -= 1; 	out_y += 1; 	break;
			case 'a':				out_y -= 1; 	break;
			case 's': 								break;
			case 'd':				out_y += 1; 	break;
			case 'z': out_x += 1; 	out_y -= 1; 	break;
			case 'x': out_x += 1; 		 			break;
			case 'c': out_x += 1; 	out_y += 1; 	break;
			}
		}
	}
}

