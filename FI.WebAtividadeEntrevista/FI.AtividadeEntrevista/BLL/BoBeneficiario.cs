using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public long Incluir(Beneficiario beneficiario)
        {
            DaoBeneficiario bene = new DaoBeneficiario();
            return bene.Incluir(beneficiario);
        }

        /// <summary>
        /// Altera um cliente
        /// </summary>
        /// <param name="cliente">Objeto de cliente</param>
        public void Alterar(Beneficiario beneficiario)
        {
            //DaoBeneficiario bene = new DaoBeneficiario();
            //bene.Alterar(beneficiario);
        }

        /// <summary>
        /// Consulta o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public Beneficiario Consultar(long id)
        {
            return null;
            //DaoBeneficiario bene = new DaoBeneficiario();
            //return bene.Consultar(id);
        }

        /// <summary>
        /// Excluir o cliente pelo id
        /// </summary>
        /// <param name="id">id do cliente</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            //DaoBeneficiario bene = new DaoBeneficiario();
            //bene.Excluir(id);
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Beneficiario> Listar()
        {
            return null;
            //DaoBeneficiario bene = new DaoBeneficiario();
            //return bene.Listar();
        }

        /// <summary>
        /// Lista os clientes
        /// </summary>
        public List<Beneficiario> Pesquisa()
        {
            return null;
            //DaoBeneficiario bene = new DaoBeneficiario();
            //return bene.Pesquisa();
        }
    }
}
