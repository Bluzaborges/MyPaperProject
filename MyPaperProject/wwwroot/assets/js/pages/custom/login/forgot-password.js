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
                    },
                    newpassword: {
                        validators: {
                            notEmpty: {
                                message: 'Preencha o campo'
                            },
                            identical: {
                                compare: function () {
                                    return KTUtil.getById('kt_login_signin_form').querySelector('[name="password"]').value;
                                },
                                message: 'Senhas n&atilde;o conferem'
                            }
                        }
                    }
                },
                rules: {
                    newpassword: {
                        equalTo: '[name="password"]'
                    }
                },
                plugins: {
                    trigger: new FormValidation.plugins.Trigger(),
                    submitButton: new FormValidation.plugins.SubmitButton(),
                    bootstrap: new FormValidation.plugins.Bootstrap()
                }
            }
        );



        $('#kt_login_signin_submit').on('click', function (e) {
            e.preventDefault();

            validation.validate().then(function (status) {
                if (status == 'Valid') {

                    $('#kt_login_signin_submit').html('Enviar <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>');
                    $('#kt_login_signin_submit').prop("disabled", true);

                    $.ajax({
                        url: '/Home/ForgotPasswordChange',
                        type: 'POST',
                        data: $("#kt_login_signin_form").serialize(),
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

                                    window.location.href = "/Home/Index";
                                });
                            }
                            else {
                                swal.fire({
                                    text: data.msg,
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

                            $('#kt_login_signin_submit').html('Enviar');
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

                            $('#kt_login_signin_submit').html('Enviar');
                            $('#kt_login_signin_submit').prop("disabled", false);
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
    // Public Functions
    return {
        // public functions
        init: function () {
            _login = $('#kt_login');
            _handleSignInForm();
        }
    };
}();

// Class Initialization
jQuery(document).ready(function () {
    KTLogin.init();


});