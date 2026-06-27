using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes;
using SGE.Aplicacion.Servicios;
using SGE.Aplicacion.Tramites.TramiteDTOs;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Tramites.CasosDeUsoTramite;

public class AgregarTramiteUseCase (ITramiteRepository repoTramite, IExpedienteRepository repoExpediente, IAutorizacionService autorizacion, ActualizacionEstadoExpedienteService servicio, IUnidadDeTrabajo unidadDeTrabajo)
{
    public AgregarTramiteResponse Ejecutar (AgregarTramiteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.TramiteAlta))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de alta un trámite");
        }
        var expediente = repoExpediente.ObtenerPorId(request.IdExpediente) ?? throw new EntidadNoEncontradaException ("El expediente indicado no fue encontrado");
        
        var contenido = new ContenidoTramite(request.Contenido);
        var tramite = new Tramite(contenido, expediente.Id, request.IdUsuario, request.Etiqueta);

        repoTramite.Agregar(tramite);
        
        servicio.RevisionDeActualizacion(expediente.Id, request.IdUsuario);
        unidadDeTrabajo.GuardarCambios();

        return new AgregarTramiteResponse(tramite.Id);
    }
}