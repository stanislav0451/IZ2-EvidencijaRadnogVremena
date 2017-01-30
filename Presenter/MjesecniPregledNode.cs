using System;
namespace projekt
{
	public class MjesecniPregledNode: Gtk.TreeNode
	{
		[Gtk.TreeNodeValue(Column = 0)]
		public string ime_i_prezime;

		[Gtk.TreeNodeValue(Column = 1)]
		public string redovni_rad;

		[Gtk.TreeNodeValue(Column = 2)]
		public string prekovremeni;

		[Gtk.TreeNodeValue(Column = 3)]
		public string ukupno_odradeno;
		
		public MjesecniPregledNode(MjesecniPregled m)
		{
			this.ime_i_prezime = m.Ime_i_prezime;
			this.redovni_rad = m.Redovni_rad;
			this.prekovremeni = m.Prekovremeni;
			this.ukupno_odradeno = m.Ukupno_odradeno;
		}
	}
}
