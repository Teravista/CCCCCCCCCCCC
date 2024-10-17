import { expect } from 'chai'
export function Biblioteka()
{
	this.lista_wypozyczen = [];
	this.wypozycz = function(ksiazka) {this.lista_wypozyczen.push(ksiazka)};
	this.zwroc = function(zwracajacy) 
	{
		const index = this.lista_wypozyczen.findIndex((ksiazka)=> ksiazka.wypozyczajacy === zwracajacy);
		if ( index >-1)
		{
			this.lista_wypozyczen.splice(index,1);
		}
	};
	this.kto_wypozyczyl = function(tytul_szukany) 
	{ 
		const found = this.lista_wypozyczen.find((ksiazka)=> ksiazka.tytul === tytul_szukany);
		return found.wypozyczajacy;
	};
	this.szukaj = function(slowo_klucz) {
		var szukajSlowa = function(var1,var2) 
		{
			return var1.find((slowo)=> slowo === var2);
		}
		const found = this.lista_wypozyczen.find((ksiazka)=> szukajSlowa(ksiazka.slowa_kluczowe,slowo_klucz) === slowo_klucz);
		return found.tytul;
		};
}