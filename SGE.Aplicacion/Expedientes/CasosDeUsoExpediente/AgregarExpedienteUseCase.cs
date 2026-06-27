using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class AgregarExpedienteUseCase (IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public AgregarExpedienteResponse Ejecutar (AgregarExpedienteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteAlta))
        {
            throw new AutorizacionException ("El usuario no posee autorización para dar de alta un expediente");
        }
        
        var caratula = new Caratula(request.Caratula);
        var expediente = new Expediente(caratula, request.IdUsuario);

        repositorio.Agregar(expediente);
        unidadDeTrabajo.GuardarCambios();

        return new AgregarExpedienteResponse(expediente.Id);
    }
}