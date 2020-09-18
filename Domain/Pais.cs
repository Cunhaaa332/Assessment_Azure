using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Pais {
        public int Id { get; set; }
        public string Bandeira { get; set; }
        public string Nome { get; set; }
        public List<Estado> Estados { get; set; }
    }
}
