using System;
using System.Collections.Generic;
namespace projekt
{
	public class MjesecniPregledNodeStore: Gtk.NodeStore
	{
		public MjesecniPregledNodeStore(): base(typeof(MjesecniPregledNode))
		{
		}
		public void Add(MjesecniPregled m)
		{
			this.AddNode(new MjesecniPregledNode(m));
		}
		public void Dodaj(List<MjesecniPregled> pregled)
		{
			foreach (var p in pregled)
			{
				this.Add(p);
			}
		}
	}
}
