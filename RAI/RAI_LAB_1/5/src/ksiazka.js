import { expect } from 'chai'
export class Ksiazka{
	#wydawnicstwo;
	constructor(autor,tytul,cena,wydawnicstwo,slowa_kluczowe,wypozyczajacy = "")
	{
		this.autor = autor;
		this.tytul = tytul;
		this.cena = cena;
		this.#wydawnicstwo = wydawnicstwo;
		this.slowa_kluczowe = slowa_kluczowe;
		this.wypozyczajacy = wypozyczajacy;
	}
}