using System;
using Gtk;
using System.Data.SQLite;
using System.Collections.Generic;

namespace projekt
{

	public static class baza
	{
		static SQLiteConnection konekcija = new SQLiteConnection("Data Source=projekt.db");

		public static void Baza()
		{
			konekcija.Open();

			//UNOS U EVIDENCIJU
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = "CREATE TABLE IF NOT EXISTS unosEvidencija (" +
				"id integer primary key autoincrement," +
				"ime_i_prezime nvarchar(30)," +
				"datum datetime," +
				"pocetak_rada nvarchar(20), " +
				"kraj_rada nvarchar(20), " +
				"tip nvarchar(20), " +
				"prekovremeni_rad nvarchar(20)," +
				"ukupno_dnevno nvarchar(20))";

			cmd.ExecuteNonQuery();
			//KRAJ


			//UNOS RADNIKA
			SQLiteCommand cmd2 = konekcija.CreateCommand();
			cmd2.CommandText = "CREATE TABLE IF NOT EXISTS unosRadnika (" +
				"id integer primary key autoincrement," +
				"ime_i_prezime nvarchar(30)," +
				"oib nvarchar(30)," +
				"datum_zaposlenja datetime," +
				"radno_mjesto nvarchar(20), " +
				"odjel nvarchar(20))";

			cmd2.ExecuteNonQuery();
			//KRAJ


			//MJESEČNI PRGLED
			SQLiteCommand cmd3 = konekcija.CreateCommand();
			cmd3.CommandText = "CREATE TABLE IF NOT EXISTS mjesecniPregled (" +
				"id integer primary key autoincrement," +
				"ime_i_prezime nvarchar(30)," +
				"redovni_rad nvarchar(30)," +
				"prekovremeni nvarchar(20)," +
				"ukupno_odradeno nvarchar(20))";

			cmd3.ExecuteNonQuery();
			//KRAJ

			konekcija.Close();
			cmd.Dispose();//OTPUŠTANJE RESURSA
			cmd2.Dispose();
		}







		//UNOS U EVIDENCIJU

		public static void UnesiUEvidenciju(string imeZaposlenika, DateTime datum, string pocetakRada, string krajRada, string tip, string prekovremeno, string ukupno_dnevno)
		{//UNESI U TABLICU unosEvidencija

			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("INSERT INTO unosEvidencija (ime_i_prezime,datum,pocetak_rada,kraj_rada,tip,prekovremeni_rad,ukupno_dnevno) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", imeZaposlenika, datum.ToFileTime(), pocetakRada, krajRada, tip, prekovremeno, ukupno_dnevno);
			cmd.ExecuteNonQuery();

			konekcija.Close();
			cmd.Dispose();
		}

		public static List<string> VratiTip()
		{
			List<string> lista = new List<string>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT tip FROM unosEvidencija GROUP BY tip");
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string tip = reader.GetString(0);
				lista.Add(tip);
			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}


		public static List<EvidencijaRada> VratiZaposlenikeUEvidenciji()//vrati radnike koji će se prikazati u nodeview3
		{
			List<EvidencijaRada> lista = new List<EvidencijaRada>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT * FROM unosEvidencija");//String.Format se mora koristiti ako želimo koristiti placeholdere tj. 	'{0}'
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				EvidencijaRada zaposlenik = new EvidencijaRada(reader.GetString(1), DateTime.FromFileTime(reader.GetInt64(2)), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
				lista.Add(zaposlenik);
			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}
		//KRAJ



		//DNEVNA EVIDENCIJA
		public static List<EvidencijaRada> VratiDnevnuEvidenciju(DateTime datum){
			List<EvidencijaRada> lista = new List<EvidencijaRada>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT * FROM unosEvidencija WHERE datum='{0}'",datum.ToFileTime());//String.Format se mora koristiti ako želimo koristiti placeholdere tj. 	'{0}'
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				EvidencijaRada zaposlenik = new EvidencijaRada(reader.GetString(1), DateTime.FromFileTime(reader.GetInt64(2)), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
				lista.Add(zaposlenik);
			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}	

		//KRAJ







		//MJESECNA EVIDENCIJA

		public static void MjesecnaEvidencija(List<EvidencijaRada> lista,List<string> ImeIPrezime){
			
			MjesecniPregled zaposlenik;
			int prekovremeni_sati = 0, redovni_rad = 0, ukupno = 0;

			foreach (string radnik in ImeIPrezime) {
				foreach (var ime in lista) {
					if (radnik == ime.ImeZaposlenika) {
						
						prekovremeni_sati += Convert.ToInt32(ime.Prekovremeni_sati);
						ukupno += Convert.ToInt32(ime.Ukupno_dnevno);
						redovni_rad = (ukupno-prekovremeni_sati);

					}
				}
				zaposlenik = new MjesecniPregled(radnik, redovni_rad.ToString(),prekovremeni_sati.ToString(),ukupno.ToString());
				baza.SpremiZaMjesec(zaposlenik);

				prekovremeni_sati = 0;
				ukupno = 0;
				redovni_rad = 0;
			}
		}

		public static void SpremiZaMjesec(MjesecniPregled zaposlenik)
		{
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("INSERT INTO mjesecniPregled (ime_i_prezime,redovni_rad,prekovremeni,ukupno_odradeno) VALUES('{0}','{1}','{2}','{3}')",zaposlenik.Ime_i_prezime,zaposlenik.Redovni_rad,zaposlenik.Prekovremeni,zaposlenik.Ukupno_odradeno);
			cmd.ExecuteNonQuery();
			konekcija.Close();
			cmd.Dispose();
		}
		public static List<EvidencijaRada> VratiOdDo(DateTime Od,DateTime Do)
		{
			List<EvidencijaRada> lista = new List<EvidencijaRada>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT * FROM unosEvidencija WHERE datum >='{0}' AND datum <='{1}'", Od.ToFileTime(),Do.ToFileTime());
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				EvidencijaRada zaposlenik = new EvidencijaRada(reader.GetString(1), DateTime.FromFileTime(reader.GetInt64(2)), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6), reader.GetString(7));
				lista.Add(zaposlenik);


			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}
		public static List<MjesecniPregled> VratiMjesecnuEvidenciju() {
			List<MjesecniPregled> lista = new List<MjesecniPregled>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT * FROM mjesecniPregled");
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				MjesecniPregled zaposlenik = new MjesecniPregled(reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4));
				lista.Add(zaposlenik);
			}

			konekcija.Close();
			cmd.Dispose();
			return lista;
		}
		public static void DeleteMjesecnaEvidencija() {
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("DELETE FROM mjesecniPregled");
			cmd.ExecuteNonQuery();
			
			konekcija.Close();
			cmd.Dispose();
		}
		//KRAJ










		//METODE ZA TAB UNOS RADNIKA

		public static void UnosNovogRadnika(string ime_i_prezime,string oib,DateTime datum_zaposlenja,string radno_mjesto,string odjel){//UNOS U TABLICU unosRadnika

			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("INSERT INTO unosRadnika (ime_i_prezime,oib,datum_zaposlenja,radno_mjesto,odjel) VALUES('{0}','{1}','{2}','{3}','{4}')",ime_i_prezime,oib,datum_zaposlenja.ToFileTime(),radno_mjesto,odjel);//String.Format se mora koristiti ako želimo koristiti placeholdere tj. 	'{0}'
			cmd.ExecuteNonQuery();
			

			konekcija.Close();
			cmd.Dispose();
		}

		public static List<NoviZaposlenik> VratiRadnike()//vrati radnike koji će se prikazati u nodeview3
		{
			List<NoviZaposlenik> lista = new List<NoviZaposlenik>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT * FROM unosRadnika");//String.Format se mora koristiti ako želimo koristiti placeholdere tj. 	'{0}'
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				NoviZaposlenik noviZaposlenik = new NoviZaposlenik(Convert.ToString(reader.GetInt32(0)),reader.GetString(1), reader.GetString(2), DateTime.FromFileTime(reader.GetInt64(3)), reader.GetString(4), reader.GetString(5));
				lista.Add(noviZaposlenik);
			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}

		public static List<string> VratiImeIPrezimeRadnika()//ova metoda je potrebna jer treba vratit samo ime i prezime radnika a ne i ostale podatke radnika u comboboxOdaberiRadnika
		{
			List<string> lista = new List<string>();
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("SELECT ime_i_prezime FROM unosRadnika");
			SQLiteDataReader reader = cmd.ExecuteReader();

			while (reader.Read())
			{
				string noviZaposlenik = reader.GetString(0);
				lista.Add(noviZaposlenik);
			}


			konekcija.Close();
			cmd.Dispose();
			return lista;
		}


		public static void BrisanjegRadnika(string ime_i_prezime)
		{//UNOS U TABLICU unosRadnika

			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("DELETE FROM unosRadnika WHERE ime_i_prezime='{0}'",ime_i_prezime);
			cmd.ExecuteNonQuery();


			konekcija.Close();
			cmd.Dispose();
		}

		public static void IzmijeniPodatkeRadnika(string id,string ime_i_prezime, string oib, DateTime datum_zaposlenja, string radno_mjesto, string odjel)
		{
			
			konekcija.Open();
			SQLiteCommand cmd = konekcija.CreateCommand();
			cmd.CommandText = String.Format("UPDATE unosRadnika SET ime_i_prezime='{0}', oib='{1}', datum_zaposlenja='{2}', radno_mjesto='{3}', odjel='{4}' WHERE id='{5}'", ime_i_prezime, oib, datum_zaposlenja.ToFileTime(), radno_mjesto, odjel,id);
			cmd.ExecuteNonQuery();


			konekcija.Close();
			cmd.Dispose();
		}


		//KRAJ

	}
}
