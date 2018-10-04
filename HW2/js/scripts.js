function findCash2() {
  $('.price').blur(
    function() {
      var total = 0;

		$('.price').each(
        function() {
          sum += Number($(this).val());
        });
    });​​​​​​​​​
};

$('#submit').click(findCash2());

$(document).ready(){
	$(function(){
    $('#submit').click(function() {
        alert("Hello");
    });
});
}


function findCash() {
  var arr = document.getElementsByName('cash');
  var total = 0;
  for (var i = 0; i < arr.length; i++) {
    if (parseInt(arr[i].value))
      total += parseInt(arr[i].value);
  }

  return total;
}

function findInvested() {
  var arr = document.getElementsByName('invested');
  var total = 0;
  for (var i = 0; i < arr.length; i++) {
    if (parseInt(arr[i].value))
      total += parseInt(arr[i].value);
  }

  return total;
}

function findRetirement() {
  var arr = document.getElementsByName('retirement');
  var total = 0;
  for (var i = 0; i < arr.length; i++) {
    if (parseInt(arr[i].value))
      total += parseInt(arr[i].value);
  }

  return total;
}

function findBusiness() {
  var arr = document.getElementsByName('business');
  var total = 0;
  for (var i = 0; i < arr.length; i++) {
    if (parseInt(arr[i].value))
      total += parseInt(arr[i].value);
  }

  return total;
}
<<<<<<< HEAD

function findUseAssets() {
  var arr = document.getElementsByName('useAsset');
  var total = 0;
  for (var i = 0; i < arr.length; i++) {
    if (parseInt(arr[i].value))
      total += parseInt(arr[i].value);
  }

  return total;
}

function computeNetWorth() {
  var cashTotal = findCash();
  var investTotal = findInvested();
  var retireTotal = findRetirement();
  var busTotal = findBusiness();
  var useTotal = findUseAssets();

  var totalAssets = cashTotal + investTotal + retireTotal + busTotal + useTotal;

  document.getElementById("netWorth").innerHTML = "<h3>Net Worth: " + totalAssets + "</h3>";
}
=======
>>>>>>> 69bfa88039ad1cdd6209c8dd5a518dec385f141f
