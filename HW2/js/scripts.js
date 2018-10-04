

$(document).ready(function() {
	$( function() {
		$( "#accordion" ).accordion();
	} );
});

var cashArray = new Array();

$('input[type=number]').each(fuction(){
	cashArray.push($(this).val())
});

function findCash(){
	var arr = document.getElementsByName('cash');
	var total = 0;
	for(var i=0; i<arr.length; i++){
		if(parseInt(arr[i].value))
			total += parseInt(arr[i].value);
	}

	return total;
}

function findInvested(){
	var arr = document.getElementsByName('invested');
	var total = 0;
	for(var i=0; i<arr.length; i++){
		if(parseInt(arr[i].value))
			total += parseInt(arr[i].value);
	}

	return total;
}

function findRetirement(){
	var arr = document.getElementsByName('retirement');
	var total = 0;
	for(var i=0; i<arr.length; i++){
		if(parseInt(arr[i].value))
			total += parseInt(arr[i].value);
	}

	return total;
}

function findBusiness(){
	var arr = document.getElementsByName('business');
	var total = 0;
	for(var i=0; i<arr.length; i++){
		if(parseInt(arr[i].value))
			total += parseInt(arr[i].value);
	}

	return total;
}

function findUseAssets(){
	var arr = document.getElementsByName('useAsset');
	var total = 0;
	for(var i=0; i<arr.length; i++){
		if(parseInt(arr[i].value))
			total += parseInt(arr[i].value);
	}

	return total;
}

function computeNetWorth(){
	var cashTotal = findCash();
	var investTotal = findInvested();
	var retireTotal = findRetirement();
	var busTotal = findBusiness();
	var useTotal = findUseAssets();

	var totalAssets = cashTotal + investTotal + retireTotal + busTotal + useTotal;

	document.getElementById("netWorth").innerHTML = "<h3>Net Worth: " + totalAssets + "</h3>";
}
