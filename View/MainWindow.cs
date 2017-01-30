using System;
using Gtk;
using projekt;
using System.Collections.Generic;

public partial class MainWindow : Gtk.Window
{
	NoviZaposlenikNodeStore NoviZaposlenikPresenter = new NoviZaposlenikNodeStore();
	ZaposlenikNodeStore zaposlenikPresenter = new ZaposlenikNodeStore();
	ZaposlenikNodeStore zaposlenikp = new ZaposlenikNodeStore();
	MjesecniPregledNodeStore mjesecniPresenter = new MjesecniPregledNodeStore();

	public MainWindow() : base(Gtk.WindowType.Toplevel)
	{
		Build();

		notebook1.CurrentPage = 0;//postavi prozor na tab 0 (imate 3 taba ,prvi je sa 0 ,drugi je 1 a treći je 2)
		NoviZaposlenikPresenter.Dodaj(baza.VratiRadnike());
		nodeview3.NodeStore = NoviZaposlenikPresenter;





		//UNOS U EVIDENCIJU
		zaposlenikPresenter.Dodaj(baza.VratiZaposlenikeUEvidenciji());
		nodeview1.NodeStore = zaposlenikPresenter;
		nodeview1.AppendColumn("Ime i prezime", new Gtk.CellRendererText(), "text", 0);
		nodeview1.AppendColumn("Datum", new Gtk.CellRendererText(), "text", 1);
		nodeview1.AppendColumn("Početak", new Gtk.CellRendererText(), "text", 2);
		nodeview1.AppendColumn("Završetak", new Gtk.CellRendererText(), "text", 3);
		nodeview1.AppendColumn("Tip", new Gtk.CellRendererText(), "text", 4);
		nodeview1.AppendColumn("Prekovremeni rad", new Gtk.CellRendererText(), "text", 5);
		nodeview1.AppendColumn("Ukupno dnevno", new Gtk.CellRendererText(), "text", 6);
		//KRAJ


		//DNEVNA EVIDENCIJA
		nodeview2.NodeStore = zaposlenikp;
		nodeview2.AppendColumn("Ime i prezime", new Gtk.CellRendererText(), "text", 0);
		nodeview2.AppendColumn("Datum", new Gtk.CellRendererText(), "text", 1);
		nodeview2.AppendColumn("Početak", new Gtk.CellRendererText(), "text", 2);
		nodeview2.AppendColumn("Završetak", new Gtk.CellRendererText(), "text", 3);
		nodeview2.AppendColumn("Tip", new Gtk.CellRendererText(), "text", 4);
		nodeview2.AppendColumn("Prekovremeni rad", new Gtk.CellRendererText(), "text", 5);
		nodeview2.AppendColumn("Ukupno dnevno", new Gtk.CellRendererText(), "text", 6);
		//KRAJ


		//MJESEČNA EVIDENCIJA

		nodeview4.NodeStore = mjesecniPresenter;
		nodeview4.AppendColumn("Ime i prezime", new Gtk.CellRendererText(), "text", 0);
		nodeview4.AppendColumn("Redovni rad", new Gtk.CellRendererText(), "text", 1);
		nodeview4.AppendColumn("Prekovremeni sati", new Gtk.CellRendererText(), "text", 2);
		nodeview4.AppendColumn("Ukupno sati odrađeno", new Gtk.CellRendererText(), "text", 3);

		baza.DeleteMjesecnaEvidencija();
		//KRAJ


		//UNOS RADNIKA

		nodeview3.AppendColumn("Ime i prezime", new Gtk.CellRendererText(), "text", 0);//parametar jedan je naziv stupca, parametar 2 neznam,parametar 3 prikazi podatke u obliku stringa,parametar 4 odredi o kojem se stupcu po redu radi unutar nodeview
		nodeview3.AppendColumn("OIB", new Gtk.CellRendererText(), "text", 1);
		nodeview3.AppendColumn("Datum zaposlenja", new Gtk.CellRendererText(), "text", 2);
		nodeview3.AppendColumn("Radno mjesto", new Gtk.CellRendererText(), "text", 3);
		nodeview3.AppendColumn("Odjel", new Gtk.CellRendererText(), "text", 4);
		nodeview3.NodeSelection.Changed += OnRowSelected;//pretplati metodu OnRowSelected koja će popuniti kučice za unos podataka o korisniku
		//KRAJ


		//ažuriraj odmah combobox nakon pokretanja programa
		Azuriraj(comboboxOdaberiRadnika, null);//UNOS RADNIKA
		Azuriraj(comboboxEvidencija, null);//UNOS EVIDENCIJE
		AzurirajTip(comboboxentryTip, null);//ažuriraj combobox ZA TIP u prozoru UNOS U EVIDENCIJU
		//KRAJ


	}

	protected void OnDeleteEvent(object sender, DeleteEventArgs a)//metoda koju napravi sam program ,kad klikneš X zatvori aplikaciju
	{
		Application.Quit();
		a.RetVal = true;
	}




	//UNOS U EVIDENCIJU

	protected void SpremiKorisnika(object sender, EventArgs e)
	{
		int prekovremeni_sati=0;
		int sati_ukupno=0;
		if (comboboxEvidencija.ActiveText != "" && entry1.Text != "" && entry2.Text != "" && entry3.Text != "" && comboboxentryTip.ActiveText != "")//provjeri dali su ona polja u koje korisnik treba upisati podatke prazna
		{//ako polja nisu prazna onda kreiraj objekt noviZaposlenik
			sati_ukupno = Convert.ToInt32(entry3.Text) - Convert.ToInt32(entry2.Text);
			if (sati_ukupno > 8) {
				prekovremeni_sati = sati_ukupno - 8;
			}

			baza.UnesiUEvidenciju(comboboxEvidencija.ActiveText, Convert.ToDateTime(entry1.Text), entry2.Text, entry3.Text, comboboxentryTip.ActiveText,prekovremeni_sati.ToString(),sati_ukupno.ToString());//kad si kreiro novi objekt onda tom objekt se treba prosljedit ono što je korisnik upisao jer svaki objekt klase Zaposlenik prima 5 parametra zato jer konstruktor u klasi Zaposlenik prima 5 parametara

			Dialog d = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Podaci su spremljeni!");
			d.Run();
			d.Destroy();

			//Ažuriraj nodeview 1 nakon što uneseš evidenciju
			zaposlenikPresenter.Clear();//isprazni nodeview tako da se podaci ne dupliciraju
			zaposlenikPresenter.Dodaj(baza.VratiZaposlenikeUEvidenciji());
			nodeview1.NodeStore = zaposlenikPresenter;


			AzurirajTip(comboboxentryTip, null);//ažuriraj combobox ZA TIP

		}
		else {//inače ako polja nisu popunjena otvori pomoćni prozorčić koji onda traži od korisnika da unsee podatke
			Dialog d = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Molim unesite tražene podatke");
			d.Run();
			d.Destroy();
		}


	}

	protected void AzurirajTip(object sender, EventArgs e)//MORAMO SAM NAPRAVITI JOŠ JEDNU METODU ZA AŽURIRANJE COMBOBOXA JER SE MORA POZVATI DRUGA METODA KOJA VRAĆA tip A NE ime_i_prezime 
	{
		ComboBox combo = sender as ComboBox;//objekt koji pošalješ je tipa ComboBox 


		ListStore model = new ListStore(typeof(string));
		combo.Model = model; // isprazni combobox kad pozivaš metodu koja će ažurirati combobox

		List<string> lista = new List<string>();
		lista = baza.VratiTip();//Ažuriraj combobox

		foreach (string tip in lista)
		{
			combo.AppendText(tip);
		}
	}

	//KRAJ




	//DNEVNA EVIDENCIJA

	protected void OnPretrazi(object sender, EventArgs e) {

		if (entry4.Text != ""){

			zaposlenikp.Clear();
			zaposlenikp.Dodaj(baza.VratiDnevnuEvidenciju(Convert.ToDateTime(entry4.Text)));
			nodeview2.NodeStore = zaposlenikp;
		}
		else { 
			Dialog d = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Molim unesite tražene podatke");
			d.Run();
			d.Destroy();
		}
	
	
	
	}
	//KRAJ


	//MJESEČNA EVIDENCIJA

	protected void OnOdaberi(object sender, EventArgs e) {
		List<EvidencijaRada> lista = new List<EvidencijaRada>();
		List<string> ImeIPrezime = new List<string>();
		mjesecniPresenter.Clear();//isprazni nodeview4 tako da nema duplikata kod novog vračanja podataka

		if (entry5.Text!="" && entry6.Text!="") {
			baza.DeleteMjesecnaEvidencija();
			lista = baza.VratiOdDo(Convert.ToDateTime(entry5.Text),Convert.ToDateTime(entry6.Text));
			ImeIPrezime = baza.VratiImeIPrezimeRadnika();
			baza.MjesecnaEvidencija(lista,ImeIPrezime);

			//Ažuriraj nodeview4

			mjesecniPresenter.Dodaj(baza.VratiMjesecnuEvidenciju());
			nodeview4.NodeStore = mjesecniPresenter;



		}
		else{ 
			Dialog d = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Molim unesite tražene podatke");
			d.Run();
			d.Destroy();
		}




	} 

	//KRAJ



	//UNOS RADNIKA

	protected void SpremiRadnika(object sender, EventArgs e) {

		if (entry7.Text!="" && entry8.Text!="" && entry9.Text!="" && entry10.Text!="" && entry11.Text!="")//provjeri dali su polja prazna
		{//ako polja nisu prazna prosljedi podatke koje je korisnik upisao metodi UnosNovogRadnika koja se nalazi u baza.cs datoteci
			baza.UnosNovogRadnika(entry7.Text,entry8.Text,Convert.ToDateTime(entry9.Text),entry10.Text,entry11.Text);
			//javi poruku da su podaci spremljeni
			Dialog dialog = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Podaci spremljeni! ");
			dialog.Run();
			dialog.Destroy();

			//ažuriraj odmah combobox nakon unosa korisnika
			Azuriraj(comboboxOdaberiRadnika,null);//UNOS RADNIKA
			Azuriraj(comboboxEvidencija, null);//UNOS EVIDENCIJE

			//odmah ažuriraj nodeview3 nakon što se unese novi korisnik

			UpdateNodeView(null, null);





		}
		else{//inače ako su polja prazna javi poruku da korisnik treba upisati podatke
			Dialog d = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Molim unesite tražene podatke");
			d.Run();
			d.Destroy();
		}

	}

	protected void IzbrisiRadnika(object sender, EventArgs e){//ove metoda je povezana sa gumbom "Briši iz baze"


		baza.BrisanjegRadnika(comboboxOdaberiRadnika.ActiveText);//prosljedi odabranog radnika metodi za brisanje

		//odmah ažuriraj nodeview3 nakon što se izbriše novi korisnik
		nodeview3.NodeSelection.Changed -= OnRowSelected;//makni metodu sa pretplate inače če se program srušiti
		UpdateNodeView(null, null);
		nodeview3.NodeSelection.Changed += OnRowSelected;//opet pretplati metodu OnRowSelected



		///ažuriraj odmah combobox nakon unosa korisnika
		Azuriraj(comboboxOdaberiRadnika, null);//UNOS RADNIKA
		Azuriraj(comboboxEvidencija, null);//UNOS EVIDENCIJE
	}

	protected void IzmijeniPodatkeRadnika(object sender, EventArgs e) { 
		//ISTO KAO I KOD UNOSA RADNIKA 
		if (entry7.Text != "" && entry8.Text != "" && entry9.Text != "" && entry10.Text != "" && entry11.Text != "")//provjeri dali su polja prazna
		{//ako polja nisu prazna prosljedi podatke koje je korisnik upisao metodi UnosNovogRadnika koja se nalazi u baza.cs datoteci

			baza.IzmijeniPodatkeRadnika(label22.Text,entry7.Text, entry8.Text, Convert.ToDateTime(entry9.Text), entry10.Text, entry11.Text);//SAMO JE OVA METODA DRUGAČIJA

			//javi poruku da su podaci spremljeni
			Dialog dialog = new Gtk.MessageDialog(this, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Podaci izmijenjeni! ");
			dialog.Run();
			dialog.Destroy();



			//odmah ažuriraj nodeview3 nakon što se izmijeni korisnik
			nodeview3.NodeSelection.Changed -= OnRowSelected;//makni metodu sa pretplate inače če se program srušiti
			UpdateNodeView(null,null);
			nodeview3.NodeSelection.Changed += OnRowSelected;//opet pretplati metodu OnRowSelected



			//ažuriraj odmah combobox nakon unosa korisnika
			Azuriraj(comboboxOdaberiRadnika, null);//UNOS RADNIKA
			Azuriraj(comboboxEvidencija, null);//UNOS EVIDENCIJE
		}

	}

	protected void OnRowSelected(object sender, EventArgs e)
	{
		var zaposlenik = (NoviZaposlenikNode)nodeview3.NodeSelection.SelectedNode;
		label22.Text = zaposlenik.id;
		entry7.Text = zaposlenik.ime_i_prezime;
		entry8.Text = zaposlenik.oib;
		entry9.Text = zaposlenik.datum_zaposlenja;
		entry10.Text = zaposlenik.radno_mjesto;
		entry11.Text = zaposlenik.odjel;
	}

	//KRAJ METODA KOJE SE POTREBNE ZA PROZOR : UNOS RADNIKA








	protected void Azuriraj(object sender, EventArgs e)//koristi se za combobox u prozoru UNOS RADNIKA i u prozoru UNOS U EVIDENCIJU
	{

		ComboBox combo = sender as ComboBox;//objekt koji pošalješ je tipa ComboBox 


		ListStore model = new ListStore(typeof(string));
		combo.Model = model; // isprazni combobox kad pozivaš metodu koja će ažurirati combobox

		List<string> lista = new List<string>();
		lista = baza.VratiImeIPrezimeRadnika();//Ažuriraj combobox

		foreach (var ime in lista)
		{
			combo.AppendText(ime);
		}
	}

	protected void UpdateNodeView(object sender, EventArgs e)//koristi se kako bi se ažurirao nodeview1 nakon ubacivanja radnika u bazu ili brisanja ili mijenjanja podataka o rdniku
	{
		NoviZaposlenikPresenter.Clear();
		NoviZaposlenikPresenter.Dodaj(baza.VratiRadnike());
		nodeview3.NodeStore = NoviZaposlenikPresenter;
	}

}
