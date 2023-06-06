"use strict";

// Class Definition
var KTLogin = function () {
    var _login;

    var _showForm = function (form) {
        var cls = 'login-' + form + '-on';
        var form = 'kt_login_' + form + '_form';

        _login.removeClass('login-forgot-on');
        _login.removeClass('login-signin-on');
        _login.removeClass('login-signup-on');

        _login.addClass(cls);

        KTUtil.animateClass(KTUtil.getById(form), 'animate__animated animate__backInUp');
    }

    var _handleSignInForm = function () {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('kt_login_signin_form'),
            {
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o campo'
                            }
                        }
                    },
                    password: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o campo'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    submitButton: new FormValidation.plugins.SubmitButton(),
                    //defaultSubmit: new FormValidation.plugins.DefaultSubmit(), // Uncomment this line to enable normal button submit after form validation
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        $('#kt_login_signin_submit').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {

                    $('#kt_login_signin_submit').html('Login <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
                    $('#kt_login_signin_submit').prop("disabled", true);

                    $.ajax({
                        url: '/Home/Login',
                        type: 'POST',
                        data: $("#kt_login_signin_form").serialize(),
                        dataType: 'json',
                        success: function (data) {

                            if (data.mustchangepassword) {

                                (async () => {
                                    const { value: formValues } = await Swal.fire({

                                        title: 'Alterar Senha',
                                        showCancelButton: true,
                                        cancelButtonText: "Cancelar",
                                        confirmButtonText: "Enviar",
                                        focusConfirm: false,
                                        reverseButtons: true,
                                        allowOutsideClick: false,

                                        html:
                                            'Email:' +
                                            '<input id="EMAIL" class="swal2-input" disabled value="' + data.email + '">' +
                                            'Nova senha: ' +
                                            '<input id="PASSWORD" class="swal2-input" type="password">' +
                                            'Confirmar nova senha: ' +
                                            '<input id="NEWPASSWORD" class="swal2-input" type="password">',
                                        
                                        preConfirm: () => {

                                            const email = Swal.getPopup().querySelector('#EMAIL').value;
                                            const pass = Swal.getPopup().querySelector('#PASSWORD').value;
                                            const confirmPass = Swal.getPopup().querySelector('#NEWPASSWORD').value;

                                            if (!email)
                                                Swal.showValidationMessage('email não informado')

                                            else if (!pass)
                                                Swal.showValidationMessage('Senha não informada')

                                            else if (!confirmPass)
                                                Swal.showValidationMessage('Confirmação de senha não informada')

                                            else if (pass != confirmPass)
                                                Swal.showValidationMessage('Senha e confirmação de senha não conferem')

                                            return [
                                                email,
                                                pass,
                                                confirmPass
                                            ]
                                        },

                                    })

                                    if (formValues) {
                                        ChangePassword(formValues[0], formValues[1], formValues[2])
                                    }

                                    else {
                                        window.location.href = "/Main/Index";
                                    }

                                })();

                            }

                            else if (data.sucesso) {
                                window.location.href = "/Main/Index";
                            }

                            else {
                                var msg = data.msg;

                                if (msg == null || msg == "") {
                                    msg = "Parece que foram detectados alguns erros, por favor, tente novamente.";
                                }

                                swal.fire({
                                    text: msg,
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, entendi!",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    KTUtil.scrollTop();
                                });
                            }

                            $('#kt_login_signin_submit').html('Login');
                            $('#kt_login_signin_submit').prop("disabled", false);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(XMLHttpRequest);
                            console.log(XMLHttpRequest.statusText);
                            console.log(textStatus);
                            console.log(errorThrown);

                            swal.fire({
                                text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                                icon: "error",
                                buttonsStyling: false,
                                confirmButtonText: "Ok, entendi!",
                                customClass: {
                                    confirmButton: "btn font-weight-bold btn-light-primary"
                                }
                            }).then(function () {
                                KTUtil.scrollTop();
                            });

                            $('#kt_login_signin_submit').html('Login');
                            $('#kt_login_signin_submit').prop("disabled", false);
                        }
                    });

                } else {
                    swal.fire({
                        text: "Usuário ou senha inválidos!",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, entendi!",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        },
                    }).then(function () {
                        KTUtil.scrollTop();
                    });
                }
            });

        });

        function ChangePassword(email, password, newpassword) {

            var form_data = new FormData();
            form_data.append("Email", email);
            form_data.append("Password", password);
            form_data.append("NewPassword", newpassword);
            form_data.append("__RequestVerificationToken", $('input[name ="__RequestVerificationToken"]').val());

            $.ajax({
                url: '/Home/ChangePassword',
                type: 'POST',
                data: form_data,
                processData: false,
                contentType: false,
                success: function (data) {
                    if (data.sucesso) {
                        toastr.success("Senha alterada com Sucesso!");
                        window.location.href = "/Main/Index";

                    } else {
                        //toastr.error(data.message, 'Atenção!');
                    }
                },
                error: function (xhr, ajaxOptions, thrownError) {
                    console.error(xhr);
                    console.error(ajaxOptions);
                    console.error(thrownError);
                    toastr.error(thrownError, 'Atenção!');
                }
            });
        }


        // Handle forgot button
        $('#kt_login_forgot').on('click', function (e) {
            e.preventDefault();
            _showForm('forgot');
        });

        // Handle signup
        $('#kt_login_signup').on('click', function (e) {
            e.preventDefault();
            _showForm('signup');
        });

    }

    var _handleForgotForm = function (e) {
        var validation;

        // Init form validation rules. For more info check the FormValidation plugin's official documentation:https://formvalidation.io/
        validation = FormValidation.formValidation(
            KTUtil.getById('kt_login_forgot_form'),
            {
                fields: {
                    email: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o campo'
                            },
                            emailAddress: {
                                message: 'O valor n&atilde;o &eacute; um endere&ccedil;o de email v&aacute;lido'
                            }
                        }
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );

        // Handle submit button
        $('#kt_login_forgot_submit').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {
                    // Submit form
                    //KTUtil.scrollTop();
                    $('#kt_login_forgot_submit').html('Enviar <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
                    $('#kt_login_forgot_submit').prop("disabled", true);

                    $.ajax({
                        url: '/Home/ForgotPassword',
                        type: 'POST',
                        data: $("#kt_login_forgot_form").serialize(),
                        dataType: 'json',
                        success: function (data) {

                            if (data.sucesso) {
                                swal.fire({
                                    text: data.msg,
                                    icon: "success",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, entendi!",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    KTUtil.scrollTop();

                                    $("#kt_login_forgot_cancel").click();
                                });
                            }
                            else {
                                console.log(data.msg);
                                swal.fire({
                                    text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                                    icon: "error",
                                    buttonsStyling: false,
                                    confirmButtonText: "Ok, entendi!",
                                    customClass: {
                                        confirmButton: "btn font-weight-bold btn-light-primary"
                                    }
                                }).then(function () {
                                    KTUtil.scrollTop();
                                });
                            }

                            $('#kt_login_forgot_submit').html('Enviar');
                            $('#kt_login_forgot_submit').prop("disabled", false);
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            console.log(XMLHttpRequest);
                            console.log(XMLHttpRequest.statusText);
                            console.log(textStatus);
                            console.log(errorThrown);

                            swal.fire({
                                text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                                icon: "error",
                                buttonsStyling: false,
                                confirmButtonText: "Ok, entendi!",
                                customClass: {
                                    confirmButton: "btn font-weight-bold btn-light-primary"
                                }
                            }).then(function () {
                                KTUtil.scrollTop();
                            });

                            $('#kt_login_forgot_submit').html('Enviar');
                            $('#kt_login_forgot_submit').prop("disabled", false);
                        }
                    });
                } else {
                    swal.fire({
                        text: "Parece que foram detectados alguns erros, por favor, tente novamente.",
                        icon: "error",
                        buttonsStyling: false,
                        confirmButtonText: "Ok, entendi!",
                        customClass: {
                            confirmButton: "btn font-weight-bold btn-light-primary"
                        }
                    }).then(function () {
                        KTUtil.scrollTop();
                    });
                }
            });
        });

        // Handle cancel button
        $('#kt_login_forgot_cancel').on('click', function (e) {
            e.preventDefault();

            _showForm('signin');
        });
    }

    // Public Functions
    return {
        // public functions
        init: function () {
            _login = $('#kt_login');

            _handleSignInForm();
            //_handleSignUpForm();
            _handleForgotForm();
        }
    };
}();


// Class Initialization
jQuery(document).ready(function () {
    KTLogin.init();

    $(document).on('keypress', function (e) {
        if (e.which == 13) {
            $('#kt_login_signin_submit').click();
        }
    });
});
