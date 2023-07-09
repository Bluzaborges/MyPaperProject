
Dropzone.autoDiscover = false;
jQuery(document).ready(function () {
    ConfigureResults();
    ConfigureDropzone();
});

function ConfigureDropzone() {
    var id = '#kt_dropzone';

    var previewNode = $(id + " .dropzone-item");
    previewNode.id = "";
    var previewTemplate = previewNode.parent('.dropzone-items').html();
    previewNode.remove();

    myDropzone = new Dropzone(id, {
        url: "/Result/UploadFile",
        parallelUploads: 10,
        maxFilesize: 10240,
        maxFiles: 10,
        previewTemplate: previewTemplate,
        previewsContainer: id + " .dropzone-items",
        clickable: id + " .dropzone-select",
        dictMaxFilesExceeded: "O numero máximo de {{maxFiles}} arquivos foi excedido!",
        dictFileTooBig: "O tamanho do arquivo é de {{filesize}}mb, o tamanho máximo que é permitido é de {{maxFilesize}}mb."
    });

    myDropzone.on("addedfile", function (file) {
        $(document).find(id + ' .dropzone-item').css('display', '');
    });

    myDropzone.on("totaluploadprogress", function (progress) {
        $(id + " .progress-bar").css('width', progress + "%");
    });

    myDropzone.on("sending", function (file) {
        $(id + " .progress-bar").css('opacity', "1");
    });

    myDropzone.on("complete", function (progress) {
        var thisProgressBar = id + " .dz-complete";
        setTimeout(function () {
            $(thisProgressBar + " .progress-bar, " + thisProgressBar + " .progress").css('opacity', '0');
        }, 600)
    });

    myDropzone.on("error", function (file, message, xhr) {
        this.removeFile(file);
        toastr.error("Erro ao carregar o arquivo: " + message, 'Atenção!');
    });
}

$("#btnSubmitResult").click(function () {

    var files = myDropzone.getAcceptedFiles();
    var attachmentsList = [];

    for (let i = 0; i < files.length; i++) {
        let attachment = {};
        attachment.name = files[i].name;
        attachmentsList.push(attachment);
    }

    var formData = {
        name: $("#result-name").val(),
        description: $("#result-description").val(),
        attachments: attachmentsList
    };

    $.ajax({
        url: "/Result/RegisterResult",
        type: "POST",
        data: JSON.stringify(formData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (data) {
            if (data.success) {
                toastr.success("Adicionado!", 'Sucesso!');
                LoadResults(data.message);
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

function ConfigureResults() {

    var idResult = $("#project-id").val();

    $.ajax({
        url: "/Result/GetAllResultsByIdProject",
        type: "POST",
        data: JSON.stringify(idResult),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            for (let i = 0; i < data.length; i++)
                LoadResults(data[i]);
        }
    });
}

function LoadResults(idResult) {

    const resultsSeparator = document.getElementById("results-separator");
    const resultsTitle = document.getElementById("results-title");
    const resultsDiv = document.getElementById("results-list");

    resultsSeparator.removeAttribute("hidden");
    resultsTitle.removeAttribute("hidden");
    resultsDiv.removeAttribute("hidden");

    $.ajax({
        url: "/Result/RenderResultSection",
        type: "POST",
        data: JSON.stringify(idResult),
        contentType: "application/json; charset=utf-8",
        dataType: "html",
        success: function (data) {
            resultsDiv.innerHTML += data;
        }
    });
}