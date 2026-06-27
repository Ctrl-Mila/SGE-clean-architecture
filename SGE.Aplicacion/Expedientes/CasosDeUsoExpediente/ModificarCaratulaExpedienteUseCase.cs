using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class ModificarCaratulaExpedienteUseCase (IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarCaratulaExpedienteResponse Ejecutar (ModificarCaratulaExpedienteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException ("El usuario no posee autorización para modificar un expediente");
        }

        var expediente = repositorio.ObtenerPorId(request.IdExpediente) ?? throw new EntidadNoEncontradaException("El expediente a modificar no fue encontrado en los registros");
        
        var caratula = new Caratula(request.Caratula);

        expediente.ModificarCaratula(caratula, request.IdUsuario);

        unidadDeTrabajo.GuardarCambios();

        return new ModificarCaratulaExpedienteResponse();
    }
}