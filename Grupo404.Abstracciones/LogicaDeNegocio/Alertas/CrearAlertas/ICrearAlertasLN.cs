using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.LogicaDeNegocio.Alertas.CrearAlertas
{
    public interface ICrearAlertasLN
    {
        Task<int> Guardar(AlertasDto LaAlertaAGuardar);
    }
}
