using Financas.DAO;
using Financas.Entidades;
using Financas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Financas.Controllers
{

    [Authorize]
    public class MovimentacaoController : Controller
    {
        private MovimentacaoDAO movimentacaoDAO;

        public UsuarioDAO UsuarioDAO { get; }

        public MovimentacaoController(MovimentacaoDAO movimentacaoDAO, UsuarioDAO usuarioDAO)
        {
            this.movimentacaoDAO = movimentacaoDAO;
            this.UsuarioDAO = usuarioDAO;
        }
         
        // GET: Movimentacao
        public ActionResult Form()
        {
            ViewBag.Usuarios = UsuarioDAO.Lista();
            return View();
        }

        public ActionResult Adiciona(Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                movimentacaoDAO.Adiciona(movimentacao);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Usuarios = UsuarioDAO.Lista();
                return View("Form", movimentacao);
            }
        }

        public ActionResult Index()
        {
            return View(movimentacaoDAO.Lista());
        }

        public ActionResult MovimentacoesPorUsuario(MovimentacoesPorUsuarioModel model)
        {
            model.Usuarios = UsuarioDAO.Lista();
            model.Movimentacoes = movimentacaoDAO.BuscaPorUsuario(model.UsuarioId);
            return View(model);
        }

        public ActionResult Busca(BuscaMovimentacoesModel model)
        {
            model.Usuarios = UsuarioDAO.Lista();
            model.Movimentacoes = movimentacaoDAO.Busca(model.ValorMinimo, model.ValorMaximo, model.DataMinima, model.DataMaxima, model.Tipo, model.UsuarioId);
            return View(model);
        }
    }
}