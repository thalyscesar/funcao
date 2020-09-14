$(document).ready(function () {
    $("#divModal").find("#CPF").mask("000.000.000-00");
})

function CadBeneficiario() {

    var modalBeneficiario;
    var cpf;
    var nome;
    var modalAltBeneficiario;
    var gridBeneficiario;

    (function inicialize() {
        modalBeneficiario = $("#divModal");
        cpf = modalBeneficiario.find("#CPF").val();
        nome = modalBeneficiario.find("#Nome").val();
        modalAltBeneficiario = $("#modalAltBeneficiario");
        gridBeneficiario = $("#gridBeneficiarios");
    })();

    function incluir() {

        if (cpf == "") {
            ModalDialog("Ocorreu um erro", "Cpf deve ser informado");
            return;
        }
        if (nome == "") {
            ModalDialog("Ocorreu um erro", "Nome deve ser informado");
            return;
        }

        if (linhaJaExiste(cpf, nome)) {
            ModalDialog("Ocorreu um erro", "Cpf já adicionado anteriormente");
            return;
        }

        verificarCpf();
    }

    function linhaJaExiste(cpf) {

        if ($("#gridBeneficiarios tbody tr").length == 0) return false;

        var linhaJaExiste = false;
        $("#gridBeneficiarios tbody tr").each(function (i, value) {

            var tdCPF = $(this).children()[1];
            if (tdCPF.innerHTML.trim() == cpf.trim())
                linhaJaExiste = true;
        })

        return linhaJaExiste;
    }

    function limparGridBeneficiarios() {

      var linha =  $("#gridBeneficiarios tbody tr")

        if (linha) $(linha).remove();
    }

    function verificarCpf(cpfalt, nomealt) {
        var novoCpf = cpfalt || cpf;
        var novoNome = nomealt || nome;

        $.get(urlGetVerificacaoCPF + "?cpf=" + novoCpf, function (data) {

            if (data.Valido) {

                var linha = $('<tr>');
                var cols = "";

                cols += "<td> " + novoNome + " </td>"
                cols += "<td> " + novoCpf + " </td>"
                cols += "<td width:160px style='text-align:center;'>" +
                    "<button style='margin-right:10px' class='btn btn-danger btn-sm' type='button' onclick='cadBeneficiario.excluir(this)'>Excluir</button>" +
                    "<button class='btn btn-primary btn-sm' type='button' onclick='cadBeneficiario.abrirModalAlteracao(this)'>Alterar</button>"
                "</td>"

                linha.append(cols);
                gridBeneficiario.append(linha)
                limparCampos();

            }
            else {

                ModalDialog("Ocorreu um erro", data.Message);
            }
        })
    }

    function limparCampos() {
        modalBeneficiario.find("#CPF").val('');
        modalBeneficiario.find("#Nome").val('');
    }

    function excluir(data) {
        $(data).parent().parent().remove();
    }

    var elementoAcao = null;
    function abrirModalAlteracao(data) {

        elementoAcao = data
        modalAltBeneficiario.modal('show');

        var linhaSelecionada = $(data).parent().parent();

        $(linhaSelecionada).addClass('active');
        var tds = linhaSelecionada.children();

        tds.each(function (i, value) {
            var texto = $(this)[0];
            if (i == 0) {
                modalAltBeneficiario.find("#Nome").val(texto.innerText);
            }
            else if (i == 1) {
                modalAltBeneficiario.find("#CPF").val(texto.innerText);
            }
        })
    }

    function salvar() {
       var linhaAtivada = $("#gridBeneficiarios tbody tr.active");
            linhaAtivada[0].remove();
            incluirAlt();
    }
        

    function incluirAlt() {

        var cpfalt = modalAltBeneficiario.find("#CPF").val();
        var nomealt = modalAltBeneficiario.find("#Nome").val();

        if (cpfalt == "") {
            ModalDialog("Ocorreu um erro", "Cpf deve ser informado");
            return;
        }
        if (nomealt == "") {
            ModalDialog("Ocorreu um erro", "Nome deve ser informado");
            return;
        }

        if (linhaJaExiste(cpfalt, nomealt)) {
            ModalDialog("Ocorreu um erro", "Cpf já adicionado anteriormente");
            return;
        }
        verificarCpf(cpfalt, nomealt);
    }

    function ModalDialog(titulo, texto) {
        var random = Math.random().toString().replace('.', '');
        var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
            '        <div class="modal-dialog">                                                                                 ' +
            '            <div class="modal-content">                                                                            ' +
            '                <div class="modal-header">                                                                         ' +
            '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
            '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
            '                </div>                                                                                             ' +
            '                <div class="modal-body">                                                                           ' +
            '                    <p>' + texto + '</p>                                                                           ' +
            '                </div>                                                                                             ' +
            '                <div class="modal-footer">                                                                         ' +
            '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
            '                                                                                                                   ' +
            '                </div>                                                                                             ' +
            '            </div><!-- /.modal-content -->                                                                         ' +
            '  </div><!-- /.modal-dialog -->                                                                                    ' +
            '</div> <!-- /.modal -->                                                                                        ';

        $('body').append(texto);
        $('#' + random).modal('show');
    }

    function ListBeneficiarios() {

       var linhas = $("#gridBeneficiarios tbody tr")

        var beneficiarios = [];
        if (linhas.length == 0) return beneficiarios;

        $(linhas).each(function (i, value) {

            var objeto = {};
            $(this).children().each(function (i, value) {

                if (i === 0) {
                    objeto.Nome = this.innerHTML.trim();
                }

                if (i === 1) {
                    objeto.CPF = this.innerHTML.trim();
                }
            });
            beneficiarios.push(objeto)
        });

        return beneficiarios;
    }

    return {
        incluir: incluir,
        limparGridBeneficiarios: limparGridBeneficiarios,
        excluir: excluir,
        salvar: salvar,
        abrirModalAlteracao: abrirModalAlteracao,
        ListBeneficiarios: ListBeneficiarios
    }
}



