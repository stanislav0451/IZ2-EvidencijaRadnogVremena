using System;
namespace projekt
{
	public class NoviZaposlenikNode: Gtk.TreeNode
	{
		public string id;

		[Gtk.TreeNodeValue(Column = 0)]
		public string ime_i_prezime;

		[Gtk.TreeNodeValue(Column = 1)]
		public string oib;

		[Gtk.TreeNodeValue(Column = 2)]
		public string datum_zaposlenja;

		[Gtk.TreeNodeValue(Column = 3)]
		public string radno_mjesto;

		[Gtk.TreeNodeValue(Column = 4)]
		public string odjel;

		public NoviZaposlenikNode(NoviZaposlenik n)
		{// KAO ŠTO MOŽETE VIDJETI u klasi NoviZaposlenikNodeStore metoda Dodaj() prima za parametar nekakvu listu di je svaki tip, tj.
		 //svaki objekt unutar te liste je tipa klase NoviZaposlenik i onda mi prolazimo kroz tu listu i učitavamo objekt po objekt i svaki taj učitani objekt 
		 //prosljedimo našoj metodi Add() a ta naša metoda prosljedi taj objekt "ugrađenoj" metodi AddNode ,e sada ta metoda za parametar prima 
		 //objekt ove klase NoviZaposlenikNode a to znači da vrijednosti onog učitanog objekta iz liste preko metode Add() i metode AddNode prosljedimo 
		 //ovom konstuktoru jer u metodi AddNode instanciramo objekt ove klase , i tako prikazujemo učitani red u nodeview-u ,znači ova klasa predstavlja
		 //svaki red unutar tog nodeview widgeta
			this.id = n.Id;
			this.ime_i_prezime = n.Ime_i_prezime;
			this.oib = n.OIB;
			this.datum_zaposlenja = n.Datum_zaposlenja.ToShortDateString();
			this.radno_mjesto = n.Radno_mjesto;
			this.odjel = n.Odjel;
		}
	}
}
