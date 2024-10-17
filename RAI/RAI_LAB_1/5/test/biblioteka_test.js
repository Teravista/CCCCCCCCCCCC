import { expect } from 'chai'
//import { Pojazd } from '../src/pojazd.js'
import { Biblioteka } from '../src/biblioteka.js'
import { Ksiazka } from '../src/ksiazka.js'
describe('pojazd-tests', function() 
{
	
	beforeEach(function(){
	
	});	
	
	it('adding books to wyporzyczenie', function() 
	{
    const ksiazka1 = new Ksiazka("Robert","Proza Norweska",3,"Brak Wydawnicstawaw",["ja","on","ona","ono"],"Robert");
	const ksiazka2 = new Ksiazka("Michal","c#tutorial",9999,"Mr Prof",["wskaznik","pointer","rzutowanie"],"Michal");
	var vivlioteka = new Biblioteka();
	vivlioteka.wypozycz(ksiazka1);
	vivlioteka.wypozycz(ksiazka2);
	expect(vivlioteka.lista_wypozyczen.length).to.eql(2);
	});
	
	it('testowanie kto wyporzyczyl', function() 
	{
    var ksiazka1 = new Ksiazka("Robert","Proza Norweska",3,"Brak Wydawnicstawaw",["ja","on","ona","ono"],"Robert");
	var ksiazka2 = new Ksiazka("Michal","c#tutorial",9999,"Mr Prof",["wskaznik","pointer","rzutowanie"],"Michal");
	var vivlioteka = new Biblioteka();
	vivlioteka.wypozycz(ksiazka1);
	vivlioteka.wypozycz(ksiazka2);
	expect(vivlioteka.kto_wypozyczyl("c#tutorial")).to.eql("Michal");
	});
	
	
	it('zwracanie ksiazki', function() 
	{
    var ksiazka1 = new Ksiazka("Robert","Proza Norweska",3,"Brak Wydawnicstawaw",["ja","on","ona","ono"],"Robert");
	var ksiazka2 = new Ksiazka("Michal","c#tutorial",9999,"Mr Prof",["wskaznik","pointer","rzutowanie"],"Michal");
	var vivlioteka = new Biblioteka();
	vivlioteka.wypozycz(ksiazka1);
	vivlioteka.wypozycz(ksiazka2);
	expect(vivlioteka.lista_wypozyczen.length).to.eql(2);
	vivlioteka.zwroc("Michal");
	expect(vivlioteka.lista_wypozyczen.length).to.eql(1);
	expect(vivlioteka.lista_wypozyczen[0].tytul).to.eql("Proza Norweska");
	
	});
	
	it('szukanie po slowach', function() 
	{
    var ksiazka1 = new Ksiazka("Robert","Proza Norweska",3,"Brak Wydawnicstawaw",["ja","on","ona","ono"],"Robert");
	var ksiazka2 = new Ksiazka("Michal","c#tutorial",9999,"Mr Prof",["wskaznik","pointer","rzutowanie"],"Michal");
	var vivlioteka = new Biblioteka();
	vivlioteka.wypozycz(ksiazka1);
	vivlioteka.wypozycz(ksiazka2);
	expect(vivlioteka.lista_wypozyczen.length).to.eql(2);
	
	expect(vivlioteka.szukaj("rzutowanie")).to.eql("c#tutorial");
	
	ksiazka2.tytul = ksiazka2.tytul.replaceAll("c#","c++");
	expect(ksiazka2.tytul).to.eql("c++tutorial");
	
	
	});
	

	
});

