using Grupo404.Abstracciones.ModelosParaUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grupo404.Abstracciones.AccesoADatos
{
    public interface IListarAlertasDA
    {
        List<AlertasDto> Obtener();
    }
}
