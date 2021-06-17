using System;
using System.IO;
using NATS.Client;
using ETL.Nats.Shared;
using System.Xml.Serialization;

namespace ETL.Nats.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            using IEncodedConnection connection = new ConnectionFactory().CreateEncodedConnection();
            EventHandler<EncodedMessageEventArgs> evt = (sender, args) =>
            {
                Aluno aluno = (Aluno)args.ReceivedObject;
                Console.WriteLine("Código: {0}, Nome: {1}, Nascimento: {2}", aluno.Codigo, aluno.Nome, aluno.DataNascimento?.ToString("dd/MM/yyyy"));
            };

            connection.OnDeserialize = DeserializeFromXML;

            using (IAsyncSubscription subscribe = connection.SubscribeAsync("aluno-channel", evt))
            {
                Console.WriteLine("Aguardando mensagens..");
                
                while (true)
                {

                }
            }
        }

        public static Object DeserializeFromXML(byte[] data)
        {
            XmlSerializer x = new XmlSerializer(new Aluno().GetType());
            MemoryStream ms = new MemoryStream(data);
            return x.Deserialize(ms);
        }
    }
}
