var KTSelect2 = function () {

    var demos = function () {

        $('#select-area').select2({
            placeholder: "Selecione a(s) área(s) de atuação do pesquisador",
        });

        $('#select-subarea').select2({
            placeholder: "Selecione a(s) subárea(s) de atuação do pesquisador",
        });

    }

    return {
        init: function () {
            demos();
        }
    };
}();

jQuery(document).ready(function () {
    KTSelect2.init();
});

$("#select-area").change(function () {

    const selectArea = document.getElementById("select-area");
    const selectSubarea = document.getElementById("select-subarea");

    selectSubarea.innerHTML = "";

    var selectedOptions = selectArea.selectedOptions;
    var values = [];
    var contents = [];

    for (var i = 0; i < selectedOptions.length; i++) {
        values.push(parseInt(selectedOptions[i].value));
        contents.push(selectedOptions[i].textContent);
    }

    $.ajax({
        url: "/Researcher/GetAllSubareas",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {

            for (let i = 0; i < data.length; i++) {

                if (values.includes(data[i].idArea)) {
                    const newOptgroup = document.createElement("optgroup");
                    newOptgroup.label = data[i].name;

                    selectSubarea.appendChild(newOptgroup);
                }
            }

        }
    });
});


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

