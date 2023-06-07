
$("#btnSubmit").click(function () {

    var formData = {
        name: $("#researcher-name").val()
    };
    
    $.ajax({
        url: "/Researcher/RegisterResearcher",
        type: "POST",
        data: JSON.stringify(formData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            alert(JSON.stringify(data));
        }
    });
});


