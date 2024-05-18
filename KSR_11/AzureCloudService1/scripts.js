var myGuid = 0;
function DoAjax(method, url, fn) {
	var rq;
	if(window.XMLHttpRequest) rq = new XMLHttpRequest();
	else rq = new ActiveXObject("Microsoft.XMLHTTP");
	rq.open(method, url, true);
	var handler = function(rq) {
		return function() {
			if(rq.readyState != 4) return;
			if(rq.status == 200) fn(rq);
			else alert(rq.readyState + " " + rq.status + " " + rq.statusText);
			};
		}
	rq.onreadystatechange = handler(rq);
	rq.send();
	}

function HandleClickCreateU() {
	var fn = function(rq) {
		//alert(rq.responseText);
		//		     tag           #text         zwartosc
		var e = document.getElementById("wynik");
		e.innerHTML = rq.responseXML.childNodes[0].childNodes[0].nodeValue;
		e = document.getElementById("resp");
		e.innerHTML = rq.responseText;
		};
	var a, b;
	a = document.getElementById("a").value;
	b = document.getElementById("b").value;

	DoAjax("POST", "CreateUser/" + encodeURI(a) + "/" + encodeURI(b), fn);
	}

function HandleClickLOGIN() {
	var fn = function(rq) {
		//alert(rq.responseText);
		//		     tag           #text         zwartosc
		var e = document.getElementById("WynikLogin");
		e.innerHTML = rq.responseXML.childNodes[0].childNodes[0].nodeValue;
		e = document.getElementById("resp");
		e.innerHTML = rq.responseText;
		myGuid = rq.responseText;
		};
	var a, b;
	a = document.getElementById("aa").value;
	b = document.getElementById("bb").value;

	DoAjax("POST", "LoginUser/" + encodeURI(a) + "/" + encodeURI(b), fn);
	}


function HandleClickLOGOUT() {

		if(myGuid != 0)
		{

		var fn = function(rq) {
			//alert(rq.responseText);
			//		     tag           #text         zwartosc
			var e = document.getElementById("WynikOutLogowanie");
			e.innerHTML = rq.responseXML.childNodes[0].childNodes[0].nodeValue;
			e = document.getElementById("resp");
			e.innerHTML = rq.responseText;
			};
		//var a, b;
		//a = document.getElementById("aa").value;
		//b = document.getElementById("bb").value;
		var e = document.getElementById("WynikOutLogowanie");
		 
		const myArray = myGuid.split(">");
		let word = myArray[1];
		e.innerHTML = String(myGuid);
		const myArray2 = word.split("<");
		let word2 = myArray2 [0];
		DoAjax("POST", "LogOut/" + encodeURI(String(word2)), fn);
		myGuid = 0
		}
}

function HandleClickPUT() {
if(myGuid != 0)
	{
	var fn = function(rq) {
		//alert(rq.responseText);
		//		     tag           #text         zwartosc
		var e = document.getElementById("WynikPUT");
		e.innerHTML = rq.responseXML.childNodes[0].childNodes[0].nodeValue;
		e = document.getElementById("resp");
		e.innerHTML = rq.responseText;
		};
	var e = document.getElementById("WynikPUT");
	e.innerHTML = "im in";
	var a, b,c;
	a = document.getElementById("nazwa").value;
	b = document.getElementById("tresc").value;

	const myArray = myGuid.split(">");
		let word = myArray[1];
		e.innerHTML = String(myGuid);
		const myArray2 = word.split("<");
		let word2 = myArray2 [0];
	c = word2;

	DoAjax("POST", "Put/" + encodeURI(a) + "/" + encodeURI(b)+ "/" + encodeURI(c), fn);
	}
}

function HandleClickGet() {
if(myGuid != 0)
	{
	var fn = function(rq) {
		//alert(rq.responseText);
		//		     tag           #text         zwartosc
		var e = document.getElementById("WynikGet");
		e.innerHTML = rq.responseXML.childNodes[0].childNodes[0].nodeValue;
		e = document.getElementById("resp");
		e.innerHTML = rq.responseText;
		};
	var e = document.getElementById("WynikGet");
	e.innerHTML = "im in";
	var a, c;
	a = document.getElementById("nazwaGet").value;

	const myArray = myGuid.split(">");
		let word = myArray[1];
		e.innerHTML = String(myGuid);
		const myArray2 = word.split("<");
		let word2 = myArray2 [0];
	c = word2;

	DoAjax("POST", "Get/" + encodeURI(a) + "/" + encodeURI(c), fn);
	}
}