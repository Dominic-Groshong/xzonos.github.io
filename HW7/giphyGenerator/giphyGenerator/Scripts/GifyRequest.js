var xhr = $.get("http://api.giphy.com/v1/gifs/search?q=ryan+gosling&api_key=YOUR_API_KEY&limit=5");
xhr.done(function (data) { console.log("success got data", data); });


// Triger when the spacebar is pressed.
$('input:text').keypress(function (e) {

  // `0` works is for mozilla and `32` for other browsers
  if (e.keyCode == 0 || e.keyCode == 32) 
    console.log('space pressed');
});
