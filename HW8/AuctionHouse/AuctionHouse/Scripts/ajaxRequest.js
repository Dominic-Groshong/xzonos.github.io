$(document).ready(function () {

  // Get the last segment of the url
  var url = window.location.href;
  var id = url.substr(url.lastIndexOf('/') + 1);
  var source = "/Items/Update/ " + id;

  var ajax_call = function () {

    $.ajax({
      type: "GET",
      dataType: "json",
      url: source,
      success: displayData,
      error: errorOnAjax
    });
  };

  var interval = 1000 * 5;

  window.setInterval(ajax_call, interval);
});

// Display the data that we've retrieved
/*function displayData(data) {
  var recent = $("#price").html();
}*/
// something went wrong
function errorOnAjax() {
  console.log("error");
}



