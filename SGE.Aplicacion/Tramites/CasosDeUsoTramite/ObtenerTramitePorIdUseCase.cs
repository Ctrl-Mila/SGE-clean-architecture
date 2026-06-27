using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Tramites.TramiteDTOs;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class ObtenerTramitePorIdUseCase (ITramiteRepository repositorio)
{
    public ObtenerTramitePorIdResponse Ejecutar (ObtenerTramitePorIdRequest request)
    {
        var t = repositorio.ObtenerPorId(request.Id) ?? throw new EntidadNoEncontradaException ("El trámite solicitado no se encuentra en los registros");
        
        var response = new TramiteResponse(t.Id, t.Contenido.Contenido, t.ExpedienteId, t.Etiqueta, t.FechaCreacion, t.FechaUltimaModificacion, t.UsuarioUltimoCambio);

        return new ObtenerTramitePorIdResponse(response);
    }

}