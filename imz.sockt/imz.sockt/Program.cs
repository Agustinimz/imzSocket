using imz.sockt.Comunicacion;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace imz.sockt
{
    public class Program
    {
        static void Main(string[] args)
        {
            int puerto = Convert.ToInt32(ConfigurationManager.AppSettings["puerto"]);

            Console.WriteLine("Inicializando el servidor en el puerto.... {0}", puerto);
            ServerSocket servidor = new ServerSocket(puerto);

            if (servidor.Iniciar())
            {
                //OK, puede conectar
                Console.WriteLine("Servidor iniciado");

                //Solicitiando clientes infinitamente

                //<! Establecer la conversacion
                
                while (true)
                {
                    Console.WriteLine("Esperando Cliente...");
                    Socket socketCliente = servidor.ObtenerCliente();

                    //construir el mecanismo para escribir y leeer cliente
                    ClienteCom cliente = new ClienteCom(socketCliente);
                    //aqui esta el protocolo de comunicacion
                    cliente.Escribir("Hola , dime tu nombre: ");
                    string respuesta = cliente.Leer();
                    Console.WriteLine("El cliente envio: {0}", respuesta);
                    cliente.Escribir("Hasta luego!!");
                    cliente.Desconectar();

                }

            }
            else
            {
                Console.WriteLine("ERRRORR!!!, el puerto {0} esta en uso", puerto);
            }
        }
    }
}