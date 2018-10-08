// Enables accordion functionality while allowing more than one accordion "tab" open at the same time.
$(document).ready(function() {

  $('#accordion').accordion({
    collapsible: true,
    active: false,
    autoHeight: false,
    heightStyle: "content",
    disabled: true
  });

  $('#accordion h3.ui-accordion-header').click(function() {
    $(this).next().slideToggle();
  });

  $('.accordion-expand-all').click(function() {
    $('#accordion h3.ui-accordion-header').next().slideDown();
  });

});


// Get the values from the input fields and add them to the sum.
function findCash() {
  var sum = 0;
  $("input[name='cash']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

function findInvested() {
  var sum = 0;
  $("input[name='invested']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

function findRetirement() {
  var sum = 0;
  $("input[name='retirement']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

function findBusiness() {
  var sum = 0;
  $("input[name='business']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

function findUseAssets() {
  var sum = 0;
  $("input[name='useAsset']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}


// Calculates current liabilities
function findCurrentLiability() {
  var sum = 0;
  $("input[name='currentLib']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

// Calculates long-term liabilities
function findLongLiability() {
  var sum = 0;
  $("input[name='longLib']").each(function() {
    sum += +$(this).val();
  });
  return sum;
}

// Calculate each section and output net worth plus add them to the list.
function computeNetWorth() {
  $("ul#netWorth > li").remove();
  // Current Assets
  var cashTotal = findCash();
  var investTotal = findInvested();
  var retireTotal = findRetirement();
  var busTotal = findBusiness();
  var useTotal = findUseAssets();

  // Current liabilities
  var currentLibTotal = findCurrentLiability();
  var longLibTotal = findLongLiability();

  var totalAssets = cashTotal + investTotal + retireTotal + busTotal + useTotal;
  var totalLibabilities = currentLibTotal + longLibTotal;

  var netWorth = totalAssets - totalLibabilities;

  if (cashTotal != 0) {
    $("#netWorth").append("<li>Cash and Cash Equivilent Assets: $" + cashTotal);
  }
  if (investTotal != 0) {
     $("#netWorth").append("<li>Invested Assets: $" + investTotal);
  }
  if (retireTotal != 0) {
    $("#netWorth").append("<li>Retirement Accounts: $" + retireTotal);
  }
  if (busTotal != 0) {
    $("#netWorth").append("<li>Business Ownership Intrests: $" + busTotal);
  }
  if (useTotal != 0) {
    $("#netWorth").append("<li>Use Assets: $" + useTotal);
  }
  if (currentLibTotal != 0) {
    $("#netWorth").append("<li class='negitive'>Current Liabilities: $" + currentLibTotal);
  }

  if (longLibTotal != 0) {
    $("#netWorth").append("<li class='negitive'>Long-Term Liabilities: $" + longLibTotal);
  }

  $("#netWorth").append("<li class='total'>Total Net Worth: $" + netWorth);

  if (netWorth > 0) {
    $('.total').addClass('positive');
  } else if (netWorth < 0) {
    $(".total").addClass('negitive')
  } else {
    $(".total").addClass('thanos')
  }
}
