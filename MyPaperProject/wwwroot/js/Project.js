﻿

function LoadProjectsTable() {

    datatable = $('#kt_datatable').KTDatatable({

        data: {
            type: 'remote',
            source: {
                read: {
                    url: "/Project/GetAllProjects",
                    timeout: 240000,
                }
            },
            pageSize: 10,
            serverPaging: false,
            serverFiltering: false,
            serverSorting: false,
        },
        layout: {
            scroll: true,
            footer: false,
            spinner: {
                message: 'Procurando...'
            }
        },
        scrollX: true,
        translate: {
            records: {
                processing: 'Aguarde...',
                noRecords: 'Nenhum projeto encontrado.'
            },
            toolbar: {
                pagination: {
                    items: {
                        default: {
                            first: 'Primeiro',
                            prev: 'Anterior',
                            next: 'Próximo',
                            last: 'Último',
                            more: 'Mais Páginas',
                            input: 'Número da Página',
                            select: 'Selecione a quantidade por página'
                        },
                        info: 'Mostrando {{start}} - {{end}} de {{total}} registros'
                    }
                }
            }
        },
        sortable: true,
        pagination: true,
        search: {
            input: $('#kt_datatable_search_query'),
        },
        columns: [
            {
                field: 'name',
                title: 'NOME',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 200,
                template: function (row) {
                    return '<a href="/FinalClient/EditFinalClient?id=' + row.id + '" class="text-truncate text-dark-75 d-block font-size-md">' + row.name + '</a>';
                }
            },
            {
                field: 'creationDate',
                title: 'PUBLICADO EM',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 125,
                template: function (row) {

                    const splitDate = row.creationDate.split('T')[0].split("-");
                    const newDate = splitDate[2] + "/" + splitDate[1] + "/" + splitDate[0];

                    var output = '\<a class="text-truncate text-dark-75 d-block font-size-md">' + newDate + '</a>'

                    return output;
                }
            },
            {
                field: 'teachers',
                title: 'DOCENTE(S)',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                template: function (row) {

                    var output = '';

                    for (let i = 0; i < row.researchers.length; i++) {
                        if (row.researchers[i].type == "Teacher")
                            output += '<span class="label font-weight-bold label-lg label-rounded label-inline label-bold mt-2">' + row.researchers[i].name + '</span> '
                    }

                    return output;
                }
            },
            {
                field: 'researchers',
                title: 'PESQUISADORE(S)',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                template: function (row) {

                    var output = '';

                    for (let i = 0; i < row.researchers.length; i++) {
                        if (row.researchers[i].type != "Teacher")
                            output += '<span class="label font-weight-bold label-lg label-rounded label-inline label-bold mt-2">' + row.researchers[i].name + '</span> '
                    }

                    return output;
                }
            },
            {
                field: 'areas',
                title: 'ÁREA(S) DO CONHECIMENTO',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                template: function (row) {

                    var output = '';

                    for (let i = 0; i < row.areas.length; i++)
                        output += '<span class="label font-weight-bold label-lg label-light-primary label-inline label-bold mt-2">' + row.areas[i].name + '</span>'

                    return output;
                }
            },
            {
                field: 'funding',
                title: 'FINANCIAMENTO',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 125,
                template: function (row) {

                    if (!row.funded)
                        return '<a class="text-truncate text-dark-75 d-block font-size-md">Não financiado</a>'

                    var output = '<a class="text-truncate text-dark-75 d-block font-size-md">' + row.funding.name + '</a>'

                    return output;
                }
            },
            {
                field: 'ended',
                title: 'FINALIZADO EM',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 125,
                template: function (row) {

                    if (!row.ended)
                        return '<span class="label font-weight-bold label-lg label-light-info label-inline label-bold mt-2">Em andamento</span> '

                    const splitDate = row.endedDate.split('T')[0].split("-");
                    const newDate = splitDate[2] + "/" + splitDate[1] + "/" + splitDate[0];

                    var output = '<span class="label font-weight-bold label-lg label-rounded label-inline label-bold mt-2">' + newDate + '</span> '

                    return output;
                }
            },
            {
                field: 'Actions',
                title: 'AÇÕES',
                sortable: false,
                textAlign: 'right',
                overflow: 'visible',
                autoHide: false,
                width: 80,
                template: function (row) {

                    var output = '\
	                     <a href="/Project/Edit?id=' + row.id + '" class="btn btn-sm btn-icon btn-bg-light btn-icon-primary btn-hover-primary mx-1" title="Editar">\
		                    <i class="fas fa-edit"></i>\
	                    </a>';

                    return output;
                }
            },
        ]
    });
}

jQuery(document).ready(function () {
    LoadProjectsTable();
});
