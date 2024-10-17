import { expect } from 'chai'
import { Pojazd } from '../src/pojazd.js'
describe('pojazd-tests', function() 
{
	
	var pojazd;
	
	beforeEach(function(){
	pojazd = new Pojazd(5,15,2);
	});	

	it('zmiana predkosci', function() 
	{
		pojazd = new Pojazd(5,15,2);
		pojazd.start(11);
		expect(pojazd.status()).to.eql(11);
	});
	
	it('pola odczyt prywatne', function() 
	{
		pojazd = new Pojazd(5,15,2);
		expect(pojazd.id).to.eql(undefined);
		expect(pojazd.max_predkosc).to.eql(undefined);
		expect(pojazd.predkosc).to.eql(undefined);
	});
	
    it('pola zapis prywatne', function() 
	{
		pojazd = new Pojazd(5,15,2);
		pojazd.id = 10
		pojazd.max_predkosc = 10
		pojazd.predkosc = 10
		expect(pojazd.getId()).to.eql(5);
		expect(pojazd.getMax()).to.eql(15);
		expect(pojazd.status()).to.eql(2);
	});
	
	it('stop predkosci', function() 
	{
		pojazd = new Pojazd(5,15,2);
		pojazd.start(11);
		pojazd.stopp();
		expect(pojazd.status()).to.eql(0);
	});
	
    it('test zmian ID', function() 
	{
		pojazd = new Pojazd(5,15,2);
		pojazd.setId(11);
		expect(pojazd.getId()).to.eql(11);
	});
	
	it('test inicjalizacji zmiennych', function() 
	{
		pojazd = new Pojazd(5,15,2);
		expect(pojazd.getId()).to.eql(5);
        expect(pojazd.getMax()).to.eql(15);
		expect(pojazd.status()).to.eql(2);
	});
	
	it('pola prototype construct _prototype', function() 
	{
		pojazd = new Pojazd(5,15,2);
	    expect(function(){pojazd.prototype.x = 10}).to.throw();
		pojazd.constructor.x = 10
		expect(function(){pojazd._prototype.x = 10}).to.throw();
	});
	
    it('Dodanie funckji', function() 
	{
		pojazd = new Pojazd(5,15,2);
	expect(function(){pojazd.prototype.nowaFunkcja=function(){}}).to.throw(TypeError);
	});
	

	
});