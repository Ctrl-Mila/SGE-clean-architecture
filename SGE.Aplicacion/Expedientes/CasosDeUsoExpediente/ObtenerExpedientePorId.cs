using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites;
using SGE.Aplicacion.Tramites.TramiteDTOs;
namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class ObtenerExpedientePorIdUseCase(IExpedienteRepository repoExpediente, ITramiteRepository repoTramite)
{
    public ObtenerExpedientePorIdResponse Ejecutar(ObtenerExpedientePorIdRequest request)
    {
        var expediente = repoExpediente.ObtenerPorId(request.Id) ?? throw new EntidadNoEncontradaException ("El expediente solicitado no fue encontrado en los registros");

        var expresponse = new ExpedienteResponse(expediente.Id, expediente.Carat.NombreCaratula, expediente.FechaCreacion, expediente.Estado, expediente.FechaUltimaModificacion, expediente.UsuarioUltimoCambio);

        var tramites = repoTramite.ObtenerPorExpedienteId(expediente.Id);

        var traresponse = new List<TramiteResponse>();

        foreach (var t in tramites)
        {
            traresponse.Add(new TramiteResponse(t.Id, t.Contenido.Contenido, t.ExpedienteId, t.Etiqueta, t.FechaCreacion, t.FechaUltimaModificacion, t.UsuarioUltimoCambio));
        }
        

        return new ObtenerExpedientePorIdResponse(expresponse, traresponse);
    }
}