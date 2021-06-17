using System;
using NATS.Client;
using ETL.Nats.Shared;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace ETL.Nats.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            using IEncodedConnection connection = new ConnectionFactory().CreateEncodedConnection();
            
            int count = 0;
            connection.OnSerialize = SerializeToXML;

            while (count < 1000)
            {
                connection.Publish("aluno-channel", new Aluno()
                {
                    Codigo = Guid.NewGuid(),
                    Nome = $"Aluno {++count}",
                    DataNascimento = new DateTime(2000 + count, 1, 1)
                });

                Thread.Sleep(100);
            }
        }

        public static byte[] SerializeToXML(Object obj)
        {
            MemoryStream ms = new();
            XmlSerializer x = new(((Aluno)obj).GetType());

            x.Serialize(ms, obj);

            byte[] content = new byte[ms.Position];
            Array.Copy(ms.GetBuffer(), content, ms.Position);

            return content;
        }
    }
}
