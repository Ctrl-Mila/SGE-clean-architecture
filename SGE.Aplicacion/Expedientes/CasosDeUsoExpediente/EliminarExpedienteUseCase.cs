using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Aplicacion.Tramites;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Tramites;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class EliminarExpedienteUseCase (IExpedienteRepository repoExpediente, IAutorizacionService autorizacion, ITramiteRepository repoTramite, IUnidadDeTrabajo unidadDeTrabajo)
{
    public EliminarExpedienteResponse Ejecutar (EliminarExpedienteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteBaja))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de baja un expediente");
        }

        var expediente = repoExpediente.ObtenerPorId(request.IdExpediente) ?? throw new EntidadNoEncontradaException("El expediente a eliminar no fue encontrado en los registros");
        
        var tramites = repoTramite.ObtenerPorExpedienteId(expediente.Id);
        foreach (Tramite t in tramites)
        {
            repoTramite.Eliminar(t.Id);
        }
        
        repoExpediente.Eliminar(expediente.Id);
        unidadDeTrabajo.GuardarCambios();
        
        return new EliminarExpedienteResponse();
    }
}