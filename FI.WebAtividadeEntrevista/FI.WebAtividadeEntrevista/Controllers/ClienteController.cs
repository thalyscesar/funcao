using FI.AtividadeEntrevista.BLL;
using WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Utilidades;

namespace WebAtividadeEntrevista.Controllers
{
    public class ClienteController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(ClienteModel model)
        {
            var mensagens = InconsistenciasMsgs(model);
            if (mensagens.Count > 0) return Json(string.Join(Environment.NewLine, mensagens));

            BoCliente bo = new BoCliente();
            Cliente cliente = new Cliente()
            {
                CEP = model.CEP,
                CPF = model.CPF,
                Cidade = model.Cidade,
                Email = model.Email,
                Estado = model.Estado,
                Logradouro = model.Logradouro,
                Nacionalidade = model.Nacionalidade,
                Nome = model.Nome,
                Sobrenome = model.Sobrenome,
                Telefone = model.Telefone,
            };

            model.Beneficiarios.ForEach(b => cliente.Beneficiarios.Add(new Beneficiario() { CPF = b.CPF, Nome = b.Nome }));

            model.Id = bo.Incluir(cliente);
            return Json("Cadastro efetuado com sucesso");
        }

        private List<string> InconsistenciasMsgs(ClienteModel model)
        {
            List<string> erros = new List<string>();

            if (!this.ModelState.IsValid)
            {
                erros = (from item in ModelState.Values
                         from error in item.Errors
                         select error.ErrorMessage).ToList();
            }
            if (!UtilidadesValidadacao.CPFEhValido(model.CPF))
            {
                erros.Add("Informe um CPF valido!");
            }
            if (new BoCliente().VerificarExistencia(model.CPF))
            {
                erros.Add("CPF ja existe em nossa base. Não permitida a existência de um CPF duplicado!");
            }

            return erros;
        }

        [HttpPost]
        public JsonResult Alterar(ClienteModel model)
        {
            BoCliente bo = new BoCliente();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                var cliente = new Cliente()
                {
                    Id = model.Id,
                    CEP = model.CEP,
                    CPF = model.CPF,
                    Cidade = model.Cidade,
                    Email = model.Email,
                    Estado = model.Estado,
                    Logradouro = model.Logradouro,
                    Nacionalidade = model.Nacionalidade,
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    Telefone = model.Telefone
                };

                model.Beneficiarios.ForEach(b => cliente.Beneficiarios.Add(new Beneficiario() 
                { 
                    CPF = b.CPF, 
                    Nome = b.Nome, 
                    Cliente = new Cliente() { Id = model.Id } }));

                bo.Alterar(cliente);

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoCliente bo = new BoCliente();
            Cliente cliente = bo.Consultar(id);
            Models.ClienteModel model = null;

            if (cliente != null)
            {
                model = new ClienteModel()
                {
                    Id = cliente.Id,
                    CPF = cliente.CPF,
                    CEP = cliente.CEP,
                    Cidade = cliente.Cidade,
                    Email = cliente.Email,
                    Estado = cliente.Estado,
                    Logradouro = cliente.Logradouro,
                    Nacionalidade = cliente.Nacionalidade,
                    Nome = cliente.Nome,
                    Sobrenome = cliente.Sobrenome,
                    Telefone = cliente.Telefone
                };

                cliente.Beneficiarios.ForEach(b =>
                {
                    model.Beneficiarios.Add(new BeneficiarioModel() { Id = b.Id, CPF = b.CPF, Nome = b.Nome });
                });


            }

            return View(model);
        }

        [HttpPost]
        public JsonResult ClienteList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = jtSorting.Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Cliente> clientes = new BoCliente().Pesquisa(jtStartIndex, jtPageSize, campo, crescente.Equals("ASC", StringComparison.InvariantCultureIgnoreCase), out qtd);

                //Return result to jTable
                return Json(new { Result = "OK", Records = clientes, TotalRecordCount = qtd });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult VerifiqueCPf(string cpf)
        {
            if (UtilidadesValidadacao.CPFEhValido(cpf))
            {
                return Json(new { Valido = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { Valido = false, Message = "Informe um CPF valido" },JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}