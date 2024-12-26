using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;

namespace Banco_MVC.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IClienteService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IClienteService
    {
        [OperationContract]
        string create_Cliente(
            string Nombre,
            string Apellido,
            DateTime FechaNacimineto,
            string Direccion,
            string Telefono,
            string Email
            );

        [OperationContract]
        List<Cliente_DTO> list_clientes(int id);

        [OperationContract]
        string update_Cliente(
            int ClienteID,
            string Nombre,
            string Apellido,
            DateTime FechaNacimineto,
            string Direccion,
            string Telefono,
            string Email);

        [OperationContract]
        string delete_Cliente(int ClienteID);
    }
}
