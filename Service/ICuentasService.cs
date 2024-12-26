using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using DTO;

namespace Banco_MVC.Service
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "ICuentasService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface ICuentasService
    {

        [OperationContract]
        string create_Cuenta(
            int ClienteID,
            string TipoCuenta,
            decimal Saldo,
            DateTime FechaApertura);

        [OperationContract]
        List<Cuentas_DTO> list_cuentas(int id);

        [OperationContract]
        string update_Cuenta(
            int CuentaID,
            int ClienteID,
            string TipoCuenta,
            decimal Saldo,
            DateTime FechaApertura);

        [OperationContract]
        string delete_Cuenta(int CuentaID);

    }
}