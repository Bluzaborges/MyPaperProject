
var areas;
var allResearchersAreas;

var KTSelect2 = function () {
    var demos = function () {

        $('#select-area').select2({
            placeholder: "Selecione a(s) área(s) de conhecimento",
            tags: true
        });

        $('#select-teacher').select2({
            placeholder: "Selecione o(s) docente(s)",
            tags: true
        });

        $('#select-researcher').select2({
            placeholder: "Selecione o(s) pesquisador(es)",
            tags: true
        });

        $('#select-funding').select2({
            placeholder: "Selecione o financiamento",
            minimumResultsForSearch: Infinity
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
    ConfigureDropzone();

    $.ajax({
        url: "/Area/GetAllAreas",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            areas = data;
        }
    });

    $.ajax({
        url: "/Researcher/GetAllResearchersAreas",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            allResearchersAreas = data;
        }
    });
});

$("#funded").change(function () {

    const fundedCheckbok = document.getElementById("funded");
    const selectFunding = document.getElementById("select-funding");

    if (fundedCheckbok.checked)
        selectFunding.removeAttribute("disabled");
    else
        selectFunding.setAttribute("disabled", "");
});

$("#ended").change(function () {

    const endedCheckbok = document.getElementById("ended");
    const endedDateInput = document.getElementById("ended-date");

    if (endedCheckbok.checked)
        endedDateInput.removeAttribute("disabled");
    else
        endedDateInput.setAttribute("disabled", "");
});

$("#select-area").change(function () {

    const selectArea = document.getElementById("select-area");

    var selectedAreaOptions = selectArea.selectedOptions;
    var selectedAreaValues = [];
    for (let i = 0; i < selectedAreaOptions.length; i++)
        selectedAreaValues.push(parseInt(selectedAreaOptions[i].value));

    addTeachers(selectedAreaValues);
    addStudents(selectedAreaValues);
});

function addTeachers(selectedAreaValues) {

    const selectTeacher = document.getElementById("select-teacher");
    selectTeacher.innerHTML = "";

    const filteredResearchers = [];
    const addedIds = new Set();

    allResearchersAreas.forEach(researcher => {
        if (
            selectedAreaValues.includes(researcher.idArea) &&
            researcher.type === "Teacher" &&
            !addedIds.has(researcher.id)
        ) {
            filteredResearchers.push(researcher);
            addedIds.add(researcher.id);
        }
    });

    for (let j = 0; j < filteredResearchers.length; j++) {
        const opt = document.createElement("option");
        opt.value = filteredResearchers[j].id;
        opt.innerHTML = filteredResearchers[j].name;
        selectTeacher.appendChild(opt);
    }
}

function addStudents(selectedAreaValues) {

    const selectResearcher = document.getElementById("select-researcher");
    selectResearcher.innerHTML = "";

    const filteredResearchers = [];
    const addedIds = new Set();

    allResearchersAreas.forEach(researcher => {
        if (
            selectedAreaValues.includes(researcher.idArea) &&
            (researcher.type === "Student" || researcher.type === "Employee") &&
            !addedIds.has(researcher.id)
        ) {
            filteredResearchers.push(researcher);
            addedIds.add(researcher.id);
        }
    });

    for (let j = 0; j < filteredResearchers.length; j++) {
        const opt = document.createElement("option");
        opt.value = filteredResearchers[j].id;
        opt.innerHTML = filteredResearchers[j].name;
        selectResearcher.appendChild(opt);
    }
}

$("#btnSubmit").click(function () {

    const selectedAreas = $("#select-area").val();
    var areasList = [];

    for (let i = 0; i < selectedAreas.length; i++) {
        let area = {};
        area.id = selectedAreas[i];
        areasList.push(area);
    }

    const selectedResearchers = $("#select-researcher").val();
    const selectedTeachers = $("#select-teacher").val();
    var researcherList = [];

    for (let i = 0; i < selectedResearchers.length; i++) {
        let researcher = {};
        researcher.id = selectedResearchers[i];
        researcherList.push(researcher);
    }

    for (let i = 0; i < selectedTeachers.length; i++) {
        let researcher = {};
        researcher.id = selectedTeachers[i];
        researcherList.push(researcher);
    }

    var selectedFunding = {
        id: $("#funded").prop('checked') == true ? $("#select-funding").val() : 0
    };

    const idResults = document.querySelectorAll(".accordion");
    var idResultList = [];

    for (let i = 0; i < idResults.length; i++)
        idResultList.push(idResults[i].id);

    var formData = {
        id: $("#project-id").val(),
        name: $("#project-name").val(),
        areas: areasList,
        researchers: researcherList,
        funded: $("#funded").prop('checked'),
        funding: selectedFunding,
        idFunding: $("#funded").prop('checked') == true ? $("#select-funding").val() : 0,
        ended: $("#ended").prop('checked'),
        endedDate: $("#ended").prop('checked') == false || $("#ended-date").val() == "" ? "0001-01-01" : $("#ended-date").val(),
        description: $("#project-description").val(),
        idResults: idResultList
    };

    $.ajax({
        url: "/Project/RegisterProject",
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