using System;

namespace ETL.Nats.Shared
{
    [Serializable]
    public class Aluno
    {
        public Guid Codigo { get; init; }
        public string Nome { get; init; }
        public DateTime? DataNascimento { get; init; }
    }
}
