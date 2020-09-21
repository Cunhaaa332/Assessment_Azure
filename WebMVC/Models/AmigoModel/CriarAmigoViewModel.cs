using Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models.AmigoModel {
    public class CriarAmigoViewModel {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public IFormFile Fotoarq { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Birth { get; set; }
        public Pais Pais { get; set; }
        public Estado Estado { get; set; }

    }
}