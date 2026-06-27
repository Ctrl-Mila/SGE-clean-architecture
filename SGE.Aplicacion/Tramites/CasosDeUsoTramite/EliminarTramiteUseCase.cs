using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.TramiteDTOs;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class EliminarTramiteUseCase (ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public EliminarTramiteResponse Ejecutar (EliminarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteBaja))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de baja un trámite");
        }

        var tramite = repositorio.ObtenerPorId(request.IdTramite) ?? throw new EntidadNoEncontradaException ("El trámite a eliminar no fue encontrado en los registros");

        repositorio.Eliminar(request.IdTramite);
        servicio.RevisionDeActualizacion(tramite.ExpedienteId, request.IdUsuario);
        unidadDeTrabajo.GuardarCambios();

        return new EliminarTramiteResponse();
    }
}