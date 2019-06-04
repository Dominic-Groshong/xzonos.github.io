$(document).ready(function () {

  var currentBid = parseInt($("#price").html());
  var url = window.location.href;
  var id = url.substr(url.lastIndexOf('/') + 1);
  var source = "/Items/Update/ " + id;

  // send the ajax call to the controller
  var ajax_call = function () {
    $.ajax({
      type: "GET",
      dataType: "json",
      url: source,
      success: displayData,
      error: errorOnAjax
    });
  };

  // Display the data that we've retrieved
  function displayData(recent) {
    if (recent.bid > currentBid){
       currentBid = recent.bid;
       $("#inner").prepend("<tr><td>" + recent.name + "</td>" + "<td>" + recent.bid + "</td></tr>");
    }
  }
  // something went wrong
  function errorOnAjax() {
    console.log("error");
  }


  // Call the function repeatedly
  var interval = 1000 * 5;
  window.setInterval(ajax_call, interval);
});




