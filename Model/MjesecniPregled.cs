using System;
namespace projekt
{
	public class MjesecniPregled
	{

		private string ime_i_prezime;
		private string redovni_rad;
		private string prekovremeni;
		private string ukupno_odradeno;


		public MjesecniPregled(string ime_i_prezime,string redovni_rad,string prekovremeni,string ukupno_odradeno)
		{
			this.ime_i_prezime = ime_i_prezime;
			this.redovni_rad = redovni_rad;
			this.prekovremeni = prekovremeni;
			this.ukupno_odradeno = ukupno_odradeno;
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

		public string Redovni_rad
		{
			get
			{
				return redovni_rad;
			}

			set
			{
				redovni_rad = value;
			}
		}

		public string Prekovremeni
		{
			get
			{
				return prekovremeni;
			}

			set
			{
				prekovremeni = value;
			}
		}



		public string Ukupno_odradeno
		{
			get
			{
				return ukupno_odradeno;
			}

			set
			{
				ukupno_odradeno = value;
			}
		}


	}
}
