using SB.Core.Domain;
using SB.Core.ValueObjects;
using System;

namespace SB.Credito.Domain.Entities
{
    public class Credito : Entity, IAggregateRoot
    {
        public Credito(int usuarioCod,
                       string usuarioNome,
                       string cpf,
                       DateTime dataNascimento,
                       decimal salario,
                       string observacao)
        {
            UsuarioCod = usuarioCod;
            UsuarioNome = usuarioNome;
            Cpf = new CPF(cpf);
            DataNascimento = dataNascimento;
            Salario = salario;
            Observacao = observacao;
        }

        protected Credito() { }

        public int UsuarioCod { get; set; }
        public string UsuarioNome { get; private set; }
        public CPF Cpf { get; private set; }
        public DateTime DataNascimento { get; private set; }
        public decimal Salario { get; private set; }
        public string Observacao { get; private set; }
    }
}
