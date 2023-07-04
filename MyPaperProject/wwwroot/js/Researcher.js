
function LoadResearchersTable() {

    datatable = $('#kt_datatable').KTDatatable({

        data: {
            type: 'remote',
            source: {
                read: {
                    url: "/Researcher/GetAllResearchers",
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
                noRecords: 'Nenhum pesquisador encontrado.'
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
                width: 150,
                template: function (row) {
                    return '<a href="/FinalClient/EditFinalClient?id=' + row.id + '" class="text-truncate text-dark-75 d-block font-size-md">' + row.name + '</a>';
                }
            },
            {
                field: 'cpf',
                title: 'CPF',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 125,
                template: function (row) {

                    row.cpf = row.cpf.replace(/\D/g, '');
                    row.cpf = row.cpf.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4');

                    var output = '\<a class="text-truncate text-dark-75 d-block font-size-md">' + row.cpf + '</a>'

                    return output;
                }
            },
            {
                field: 'type',
                title: 'TIPO',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                width: 125,
                template: function (row) {

                    if (row.type == "Teacher") {
                        row.type = "Professor"
                    } else if (row.type == "Student") {
                        row.type = "Aluno"
                    } else if (row.type == "Employee") {
                        row.type = "Funcionário"
                    }

                    var output = '\<a class="text-truncate text-dark-75 d-block font-size-md">' + row.type + '</a>'

                    return output;
                }
            },
            {
                field: 'nameAreas',
                title: 'ÁREA(S) DO CONHECIMENTO',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                template: function (row) {

                    var output = '<span class="label label-inline label-light-primary font-weight-bold mt-2">' + row.nameAreas[0] + '</span> '

                    for (let i = 1; i < row.nameAreas.length; i++)
                        output += '<span class="label label-inline label-light-primary font-weight-bold mt-2">' + row.nameAreas[i] + '</span> '

                    return output;
                }
            },
            {
                field: 'nameSubareas',
                title: 'SUBÁREA(S) DO CONHECIMENTO',
                selector: false,
                overflow: 'visible',
                autoHide: false,
                template: function (row) {

                    var output = '<span class="label label-inline label-light-primary font-weight-bold mt-2">' + row.nameSubareas[0] + '</span> '

                    for (let i = 1; i < row.nameSubareas.length; i++)
                        output += '<span class="label label-inline label-light-primary font-weight-bold mt-2">' + row.nameSubareas[i] + '</span> '

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
	                     <a href="/Researcher/Edit?id=' + row.id + '" class="btn btn-sm btn-icon btn-bg-light btn-icon-primary btn-hover-primary mx-1" title="Editar">\
		                    <i class="fas fa-edit"></i>\
	                    </a>';

                    output += '\
                        <a class="btn btn-sm btn-icon btn-bg-light btn-icon-danger btn-hover-danger btn-delete" idResearcher = "' + row.id + '" title = "Excluir" >\
		                    <i class="flaticon-delete-1"></i>\
	                    </a>';

                    return output;
                }
            },
        ]
    });
}

jQuery(document).ready(function () {
    LoadResearchersTable();
});