using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain {
    public class Estado {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        [JsonIgnore]
        public virtual Pais Pais { get; set; }
    }
    public class EstadoResponse {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        public virtual Pais Pais { get; set; }
    }

}
