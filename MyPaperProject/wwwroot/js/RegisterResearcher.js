var areas;
var subareas;

var KTSelect2 = function () {

    var demos = function () {

        $('#select-area').select2({
            placeholder: "Selecione a(s) área(s) de atuação do pesquisador",
        });

        $('#select-subarea').select2({
            placeholder: "Selecione a(s) subárea(s) de atuação do pesquisador",
        });

        $('#select-type').select2({
            placeholder: "Selecione o tipo de pesquisador",
            minimumResultsForSearch: Infinity
        });

    }

    return {
        init: function () {
            demos();
        }
    };
}();

var KTInputmask = function () {

    var demos = function () {
        $("#researcher-document").inputmask("999.999.999-99", {
            autoUnmask: true
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
    KTInputmask.init();

    $.ajax({
        url: "/Researcher/GetAllAreas",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            areas = data;
        }
    });

    $.ajax({
        url: "/Researcher/GetAllSubareas",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            subareas = data;
        }
    });
});

$("#select-area").change(function () {

    const selectArea = document.getElementById("select-area");
    const selectSubarea = document.getElementById("select-subarea");

    selectSubarea.innerHTML = "";

    var selectedOptions = selectArea.selectedOptions;

    var selectedValues = [];

    for (let i = 0; i < selectedOptions.length; i++)
        selectedValues.push(parseInt(selectedOptions[i].value))

    for (let i = 0; i < selectedValues.length; i++) {
        const optGroup = document.createElement("optgroup");
        optGroup.label = areas[areas.findIndex(item => item.id == selectedValues[i])].name;
        selectSubarea.appendChild(optGroup);

        for (let j = 0; j < subareas.length; j++) {
            if (subareas[j].idArea == selectedValues[i]) {
                const opt = document.createElement("option");
                opt.value = subareas[j].id;
                opt.innerHTML = subareas[j].name;

                optGroup.appendChild(opt);
            }
        }
    }
});

$("#btnSubmit").click(function () {

    var formData = {
        name: $("#researcher-name").val(),
        cpf: $("#researcher-document").val(),
        type: $("#select-type").val(),
        idAreas: $("#select-area").val(),
        idSubareas: $("#select-subarea").val(),
    };
    
    $.ajax({
        url: "/Researcher/RegisterResearcher",
        type: "POST",
        data: JSON.stringify(formData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            if (data.success) {
                toastr.success("Adicionado!", 'Sucesso!');
                window.location.href = document.referrer;
            } else {
                Swal.fire({
                    title: "Atenção",
                    html: data.message + '<br><p style="color: silver; margin-top:12px; font-weight: normal;">Clique em ok para fechar.</p>',
                    icon: 'error',
                    timerProgressBar: true,
                    confirmButtonText: "Ok",
                    customClass: {
                        confirmButton: "btn btn-primary",
                    }

                })
            }
        }
    });
});

