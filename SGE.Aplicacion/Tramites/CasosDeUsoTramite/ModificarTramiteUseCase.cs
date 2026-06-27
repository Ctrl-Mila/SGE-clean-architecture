using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.TramiteDTOs;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class ModificarTramiteUseCase (ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarTramiteResponse Ejecutar (ModificarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteModificacion))
        {
            throw new AutorizacionException ("El usuario no posee autorización para modificar el trámite");
        }

        var tramite = repositorio.ObtenerPorId(request.IdTramite) ?? throw new EntidadNoEncontradaException ("El trámite a modificar no fue encontrado en los registros");

        var contenido = new ContenidoTramite(request.Contenido);

        tramite.Modificar(request.IdUsuario, request.Etiqueta, contenido);

        servicio.RevisionDeActualizacion(tramite.ExpedienteId, request.IdUsuario);

        unidadDeTrabajo.GuardarCambios();

        return new ModificarTramiteResponse();
    }
}