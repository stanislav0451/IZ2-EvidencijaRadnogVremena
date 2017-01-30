using System;
using System.Collections.Generic;
namespace projekt
{
	public class NoviZaposlenikNodeStore: Gtk.NodeStore
	{
		public NoviZaposlenikNodeStore(): base(typeof(NoviZaposlenikNode))
		{
		}
		public void Add(NoviZaposlenik z)//metoda Add() će prosljediti dobiveni objekt metodi AddNode()
		{
			this.AddNode(new NoviZaposlenikNode(z));//ako možete primjetiti,svaki objekt klase NoviZaposlenikNode ima iste parametre kao i 
													//objekt klase NoviZaposlenik ,pogledajte konstuktor klase NoviZaposlenik i klase NoviZaposlenikNode
													//zbog toga se mogu prosljediti vrijednosti jednog objekta drugom objektu iako su ti objekti iz drugačijih klasa
		}
		public void Dodaj(List<NoviZaposlenik> zaposlenik)
		{
			foreach (var z in zaposlenik)//pročitaj objekt iz liste 
			{
				this.Add(z);//pročitani objekt prosljedi metodi Add()
			}
		}
	}
}
