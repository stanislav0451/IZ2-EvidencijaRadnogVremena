using System;
namespace projekt
{
	public class EvidencijaRada
	{
		private string imeZaposlenika;
		private DateTime datum;
		private string pocetakRada;
		private string krajRada;
		private string tip;
		private string prekovremeni_sati;
		private string ukupno_dnevno;

		public EvidencijaRada(string imeZaposlenika,DateTime datum,string pocetakRada,string krajRada,string tip,string prekovremeni_sati,string ukupno_dnevno)
		{
			this.imeZaposlenika = imeZaposlenika;
			this.datum = datum;
			this.pocetakRada = pocetakRada;
			this.krajRada = krajRada;
			this.tip = tip;
			this.Prekovremeni_sati = prekovremeni_sati;
			this.Ukupno_dnevno = ukupno_dnevno;
		}



		public string ImeZaposlenika
		{
			get
			{
				return imeZaposlenika;
			}

			set
			{
				imeZaposlenika = value;
			}
		}

		public DateTime Datum
		{
			get
			{
				return datum;
			}

			set
			{
				datum = value;
			}
		}

		public string PocetakRada
		{
			get
			{
				return pocetakRada;
			}

			set
			{
				pocetakRada = value;
			}
		}

		public string KrajRada
		{
			get
			{
				return krajRada;
			}

			set
			{
				krajRada = value;
			}
		}

		public string Tip
		{
			get
			{
				return tip;
			}

			set
			{
				tip = value;
			}
		}

		public string Prekovremeni_sati
		{
			get
			{
				return prekovremeni_sati;
			}

			set
			{
				prekovremeni_sati = value;
			}
		}

		public string Ukupno_dnevno
		{
			get
			{
				return ukupno_dnevno;
			}

			set
			{
				ukupno_dnevno = value;
			}
		}


	}
}
