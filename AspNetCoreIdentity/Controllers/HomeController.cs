using AspNetCoreIdentity.Extensions;
using AspNetCoreIdentity.Models;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }


        //permitir acesso
        [AllowAnonymous]
        public IActionResult Index()
        {
            _logger.Debug("Hello world from AspNetCore!");

            return View();
        }

        //[Authorize]
        public IActionResult Privacy()
        {
            throw new Exception(message: "Erro");
            return View();
        }

        [Authorize(Roles = "Admin, Gestor")]
        public IActionResult Secret()
        {
            return View();
        }

        //claim
        [Authorize(Policy = "PodeExcluir2")]
        public IActionResult SecretClaim()
        {
            return View("Secret");
        }

        //claim "economica"
        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretClaimGravar()
        {
            return View("Secret");
        }

        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Message = "Ocorreu um erro! tente novamente mais tarde ou contate o nosso suporte.";
                modelErro.Title = "Ocorreu um erro!";
                modelErro.ErrorCode = id;
            }
            else if (id == 404)
            {
                modelErro.Message = "A página que está procurando não existe <br /> Em caso de dúvidas, entre em contato com o nosso suporte.";
                modelErro.Title = "Ops! Página não encontrada.";
                modelErro.ErrorCode = id;
            }
            else if (id == 403)
            {
                modelErro.Message = "Você não tem permissão.";
                modelErro.Title = "Acesso negado";
                modelErro.ErrorCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
