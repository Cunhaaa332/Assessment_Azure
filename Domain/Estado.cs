using System;
using System.Collections.Generic;
using System.Text;

namespace Domain {
    public class Estado {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Bandeira { get; set; }
        public virtual Pais Pais { get; set; }
    }
}
