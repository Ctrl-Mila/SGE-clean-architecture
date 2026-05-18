using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class ListarTodosLosExpedientesUseCase (IExpedienteRepository repositorio)
{
    public ListarExpedientesResponse Ejecutar(ListarExpedientesRequest request)
    {
        IEnumerable<Expediente> expedientes = repositorio.ObtenerTodos();

        var lista = new List<ExpedienteResponse>();

        foreach (Expediente e in expedientes)
        {
            lista.Add(new ExpedienteResponse(e.Id, e.Carat.NombreCaratula, e.FechaCreacion, e.Estado, e.FechaUltimaModificacion, e.UsuarioUltimoCambio));
        }

        return new ListarExpedientesResponse(lista);
    }
}