import { expect } from 'chai'
export function Pojazd(id,max_predkosc,predkosc)
{
	"use strict";
	//this.id = id;
	//this.max_predkosc = max_predkosc;
	//this.predkosc = predkosc;
	Pojazd.prototype = {} ;
	Pojazd.prototype.status=function() {return predkosc;};
	Pojazd.prototype.start=function(nowaPredkosc) {predkosc=nowaPredkosc;};
	Pojazd.prototype.stopp=function() {predkosc=0;};
	Pojazd.prototype.getId=function() {return id};
		Pojazd.prototype.getMax=function() {return max_predkosc};

		Pojazd.prototype.setId=function(nowaPRd) { id = nowaPRd};

}