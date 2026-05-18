using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.TramiteDTOs;
using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class AgregarTramiteUseCase (ITramiteRepository repositorio, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio)
{
    public AgregarTramiteResponse Ejecutar (AgregarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de alta un trámite");
        }
        
        var contenido = new ContenidoTramite(request.Contenido);
        var tramite = new Tramite(contenido, request.IdExpediente, request.IdUsuario, request.Etiqueta);

        repositorio.Agregar(tramite);
        servicio.RevisionDeActualizacion(request.IdExpediente, request.IdUsuario);

        return new AgregarTramiteResponse(tramite.Id);
    }
}