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




$('.collapse').collapse();

$('.panel-heading h4 a input[type=checkbox]').on('click', function(e) {
    e.stopPropagation();
    $(this).parent().trigger('click');   // <---  HERE
});

$('#collapseOne').on('show.bs.collapse', function(e) {
    if(!$('.panel-heading h4 a input[type=checkbox]').is(':checked'))
    {
        return false;
    }
});
