using System;

namespace Roboty
{
	public class Robot : Jednostka
	{
		public bool czy_zniszczony;

		public Robot(int x, int y)
		{
			this.czy_zniszczony = false;
			base.x = x;
			base.y = y;
		}
	}
}

