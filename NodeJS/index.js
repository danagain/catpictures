var cheerio = require('cheerio');
var request = require('request');
var wallpaper = require('wallpaper');
var util = require('util');
var fs = require('fs');

function randomInt(min, max) {
  min = Math.ceil(min);
  max = Math.floor(max);
  return Math.floor(Math.random() * (max - min)) + min;
}

request('https://octodex.github.com/', function (error, response, body) {
  if (!error && response.statusCode == 200) {
    // create emtpy array to store the output in
    var urlArray = []

    // load in the body of the callback from the http request
    var $ = cheerio.load(body);

    // create vars for upcoming use
    var data, url, format_url, filename, random

    // filter starting at the .purchasable class in the DOM
    $('.preview-image').filter(function(){
      // the current data
      data = $(this);
      // push each of the children of the a class with the href attribute
      urlArray.push(data.children().attr('data-src'));

    })
    // get random url from the array
    random = randomInt(0, urlArray.length)

    // create formatted string with the random index from the array
    format_url = util.format('https://octodex.github.com%s', urlArray[random])

    request.head(format_url, function() {

      // set wallpaper using wallpaper module
      request(format_url).pipe(fs.createWriteStream(urlArray[random].replace('/images/', '')));
      wallpaper.set(urlArray[random].replace('/images/', '')).then(() => {
        console.log('Complete');
      });
    });

  }
})
