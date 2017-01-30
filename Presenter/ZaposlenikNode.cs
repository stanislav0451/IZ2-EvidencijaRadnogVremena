using System;
namespace projekt
{
	public class ZaposlenikNode: Gtk.TreeNode
	{
		
		[Gtk.TreeNodeValue(Column = 0)]
		public String zaposlenik;

		[Gtk.TreeNodeValue(Column = 1)]
		public string datum;

		[Gtk.TreeNodeValue(Column = 2)]
		public String pocetakRada;

		[Gtk.TreeNodeValue(Column = 3)]
		public string krajRada;

		[Gtk.TreeNodeValue(Column = 4)]
		public String tip;

		[Gtk.TreeNodeValue(Column = 5)]
		public String prekovremeni_sati;

		[Gtk.TreeNodeValue(Column = 6)]
		public String ukupno_dnevno;


		public ZaposlenikNode(EvidencijaRada z)
		{
			this.zaposlenik = z.ImeZaposlenika;
			this.datum = z.Datum.ToShortDateString();
			this.pocetakRada = z.PocetakRada;
			this.krajRada = z.KrajRada;
			this.tip = z.Tip;
			this.prekovremeni_sati = z.Prekovremeni_sati;
			this.ukupno_dnevno = z.Ukupno_dnevno;

		}

	}
}
