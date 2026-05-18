using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.TramiteDTOs;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class EliminarTramiteUseCase (ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public EliminarTramiteResponse Ejecutar (EliminarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteBaja))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de baja un trámite");
        }

        Tramite tramite = repositorio.ObtenerPorId(request.IdTramite) ?? throw new EntidadNoEncontradaException ("El trámite a eliminar no fue encontrado en los registros");

        repositorio.Eliminar(tramite.Id);
        servicio.RevisionDeActualizacion(tramite.ExpedienteId, request.IdUsuario);

        return new EliminarTramiteResponse();
    }
}