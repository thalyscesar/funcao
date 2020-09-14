$(document).ready(function () {

    (function loadBeneficiarios() {

        var beneficiarios = obj.Beneficiarios;

        $(beneficiarios).each(function () {

            incluirBeneficiario(this)
        });
    })();

});

function incluirBeneficiario(beneficiario) {
    
    var tabela = $("#gridBeneficiarios");

    var linha = $('<tr>');
    var cols = "";

    cols += "<td> " + beneficiario.Nome + " </td>"
    cols += "<td> " + beneficiario.CPF + " </td>"
    cols += "<td width:160px style='text-align:center;'>" +
        "<button style='margin-right:10px' class='btn btn-danger btn-sm' type='button' onclick='cadBeneficiario.excluir(this)'>Excluir</button>" +
        "<button class='btn btn-primary btn-sm' type='button' onclick='cadBeneficiario.abrirModalAlteracao(this)'>Alterar</button>"
    "</td>"

    linha.append(cols);
    tabela.append(linha)

}

