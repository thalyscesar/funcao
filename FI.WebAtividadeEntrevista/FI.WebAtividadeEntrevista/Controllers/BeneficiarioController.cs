using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.AtividadeEntrevista.DML.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        // GET: Beneficiario
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Incluir()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            var mensagens = InconsistenciasMsgs(model);
            if (mensagens.Count > 0) return Json(string.Join(Environment.NewLine, mensagens));

            BoBeneficiario bo = new BoBeneficiario();
            //model.Id = bo.Incluir(new Beneficiario()
            //{
            //    Nome = model.Nome,
            //    CPF = model.CPF,
            //    Cliente = new Cliente() { Id  = model.ClienteModel.Id }
            //});

            return Json("Cadastro efetuado com sucesso");
        }

        private List<string> InconsistenciasMsgs(BeneficiarioModel model)
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
        public JsonResult Alterar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

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
                bo.Alterar(new Beneficiario()
                {
                    Id = model.Id,
                    //Cliente = new Cliente() { Id = model.ClienteModel.Id },
                    CPF = model.CPF,
                    Nome = model.Nome
                });

                return Json("Cadastro alterado com sucesso");
            }
        }

        [HttpGet]
        public ActionResult Alterar(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            Beneficiario beneficiario = bo.Consultar(id);
            BeneficiarioModel model = null;

            if (model != null)
            {
                model = new BeneficiarioModel()
                {
                    Id = beneficiario.Id,
                    CPF = beneficiario.CPF,
                    Nome = beneficiario.Nome,
                    //ClienteModel = new ClienteModel() { Id = beneficiario.Cliente.Id, Nome = beneficiario.Nome }
                };
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult BeneficiarioList(int id = 0,int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                int qtd = 0;
                string campo = string.Empty;
                string crescente = string.Empty;
                string[] array = "".Split(' ');

                if (array.Length > 0)
                    campo = array[0];

                if (array.Length > 1)
                    crescente = array[1];

                List<Beneficiario> beneficiarios = new BoBeneficiario().Pesquisa();


                //Return result to jTable
                return Json(new { Result = "OK", Records = beneficiarios, TotalRecordCount = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


    }
}