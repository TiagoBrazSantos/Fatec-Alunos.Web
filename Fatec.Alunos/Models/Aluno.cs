using System;
using System.ComponentModel;

namespace Fatec.Alunos.Models
{
    public class Aluno
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }

        [DisplayName("IP")]
        public string Ip { get; set; }

        [DisplayName("Computador")]
        public string NomeComputador { get; set; }
    }
}
