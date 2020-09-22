using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain {
    public class Pais {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
       [JsonIgnore]
        public virtual IList<Estado> Estados { get; set; }
    }
    public class PaisResponse {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        public virtual IList<Estado> Estados { get; set; }
    }
}
