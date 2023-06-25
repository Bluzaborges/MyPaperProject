var KTSelect2 = function () {
    var demos = function () {
        $('#select-area').select2({
            placeholder: "Selecione a(s) área(s) de conhecimento",
        });
    }

    return {
        init: function () {
            demos();
        }
    };
}();

$("#ended").change(function (ev) {
    console.log({ ev });
    console.log('logica para mostrar ou nao o campo #endedDate');
});

jQuery(document).ready(function () {
    KTSelect2.init();
});

$("#btnSubmit").click(function () {

    var formData = {
        name: $("#project-name").val(),
        idAreas: $("#select-area").val(),
        idResearchers: $("#select-researchers").val(),
        funded: $("#funded").val(),
        ended: $("#ended").val(),
        endedDate: $("#endedDate").val(),
    };

    console.log(formData);
});
