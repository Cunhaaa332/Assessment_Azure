using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain {
    public class Amigo {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Birth { get; set; }
        //[JsonIgnore]
        public virtual Pais Pais { get; set; }
        //[JsonIgnore]
        public virtual Estado Estado { get; set; }
        [JsonIgnore]
        public virtual IList<Amigo> Amigos { get; set; }
    }
    public class AmigoResponse {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Foto { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public DateTime Birth { get; set; }
        public Pais Pais { get; set; }
        public Estado Estado { get; set; }
    }
}

