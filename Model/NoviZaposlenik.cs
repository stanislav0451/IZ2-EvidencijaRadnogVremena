using System;
namespace projekt
{
	public class NoviZaposlenik
	{
		private string id;
		private string ime_i_prezime;
		private string oib;
		private DateTime datum_zaposlenja;
		private string radno_mjesto;
		private string odjel;

		//preko konstruktora postavljamo vrijednosti ovih privatnih atributa , a preko gettera dohvaćamo te iste vrijednosti
		public NoviZaposlenik(string id,string ime_i_prezime,string oib,DateTime datum_zaposlenja,string radno_mjesto,string odjel)
		{
			this.Id = id;
			this.ime_i_prezime = ime_i_prezime;
			this.oib = oib;
			this.datum_zaposlenja = datum_zaposlenja;
			this.radno_mjesto = radno_mjesto;
			this.odjel = odjel;
		}

		public string Ime_i_prezime
		{
			get
			{
				return ime_i_prezime;
			}

			set
			{
				ime_i_prezime = value;
			}
		}

		public string OIB
		{
			get
			{
				return oib;
			}

			set
			{
				oib = value;
			}
		}

		public DateTime Datum_zaposlenja
		{
			get
			{
				return datum_zaposlenja;
			}

			set
			{
				datum_zaposlenja = value;
			}
		}

		public string Radno_mjesto
		{
			get
			{
				return radno_mjesto;
			}

			set
			{
				radno_mjesto = value;
			}
		}

		public string Odjel
		{
			get
			{
				return odjel;
			}

			set
			{
				odjel = value;
			}
		}

		public string Id
		{
			get
			{
				return id;
			}

			set
			{
				id = value;
			}
		}
	}
}
