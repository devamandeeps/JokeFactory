// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //Random Joke at intial load
    getRandomJoke();
});

$("#menu-toggle").click(function (e) {
    e.preventDefault();
    $("#wrapper").toggleClass("toggled");
});

function findJokeByCategory(category) {
    var isRandomNamesOn = $("#chkRandomNames")[0].checked;
    $.ajax({
        url: '/Home/FindJokeByCategory/',
        data: { category: category, isRandomNamesOn: isRandomNamesOn },
        type: 'GET',
        success: function (joke) {
            $('#divJoke').html(joke);
        },
        error: function (error) {
            alert(error)
        }
    });
}

function getRandomJoke() {
    var isRandomNamesOn = $("#chkRandomNames")[0].checked;
    $.ajax({
        url: '/Home/GetRandomJoke/',
        data: { isRandomNamesOn: isRandomNamesOn },
        type: 'GET',
        success: function (joke) {
            $('#divJoke').html(joke);
        },
        error: function (error) {
            alert(error)
        }
    });
}