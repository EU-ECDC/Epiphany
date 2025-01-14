$(document).ready(function () {
    initPage();
}); 

function initPage() {
}

$.urlParam = function (name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    return decodeURI(results[1]) || 0;
}

function doAsk() {
    var prompt = $('#prompt').val();
    var modelType = $.urlParam('model') && ['Epi', 'Generic'].map(function (value) {
        return value.toLowerCase();
    }).includes($.urlParam('model').toLowerCase()) ? $.urlParam('model') : 'Generic'

    modelType = modelType.charAt(0).toUpperCase() + modelType.slice(1).toLowerCase();

    if (!prompt) {
        alert('No prompt provided!');
        return false;
    }

    doWaitStart();

    var askData = {
        prompt: prompt,
        modelType: modelType
    }

    $.ajax({
        url: "ai/ask",
        data: JSON.stringify(askData),
        type: "POST",
        contentType: "application/json",
        success: function (result) {
            showResult(result);
            return false;
        },
        error: function (errormessage) {
            doWaitStop();
            alert(errormessage.responseText);
        }
    });  

    return false;
}

function doWaitStart() {
    $('#ask').prop("disabled", true);
    $("body").css("cursor", "progress");
}

function doWaitStop() {
    $('#ask').prop("disabled", false);
    $("body").css("cursor", "default");
}

function showResult(data) {
    doWaitStop();

    $('#responseMain').removeClass("d-none");

    $('#response').html(data.answer.replace(/(?:\r\n|\r|\n)/g, '</br>'));
}
