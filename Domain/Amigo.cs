using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Amigo {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Birth { get; set; }
        public List<Amigo> Amigos { get; set; }
        public int IdPais { get; set; }
        public string IdEstado { get; set; }
    }
}

