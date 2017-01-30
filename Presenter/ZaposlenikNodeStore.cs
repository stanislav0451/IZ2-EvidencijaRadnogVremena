using System;
using System.Collections.Generic;
namespace projekt
{
	public class ZaposlenikNodeStore: Gtk.NodeStore
	{
		public ZaposlenikNodeStore(): base(typeof(ZaposlenikNode))
		{

		}
		public void Add(EvidencijaRada z)
		{
			this.AddNode(new ZaposlenikNode(z));
		}
		public void Dodaj(List<EvidencijaRada> zaposlenik)
		{
			foreach (var z in zaposlenik)
			{
				this.Add(z);
			}
		}

	}
}
