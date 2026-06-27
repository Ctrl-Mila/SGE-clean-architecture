using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Expedientes.ExpedienteDTOs;
using SGE.Dominio.Expedientes;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Expedientes.CasosDeUsoExpediente;

public class ModificarEstadoExpedienteUseCase (IExpedienteRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarEstadoExpedienteResponse Ejecutar (ModificarEstadoExpedienteRequest request)
    {
        if (!autorizacion.PoseeElPermiso(request.IdUsuario, Permiso.ExpedienteModificacion))
        {
            throw new AutorizacionException ("El usuario no posee autorización para modificar un expediente");
        }

        Expediente expediente = repositorio.ObtenerPorId(request.IdExpediente) ?? throw new EntidadNoEncontradaException ("El expediente a modificar no fue encontrado en los registros");

        expediente.CambiarEstado(request.NuevoEstado, request.IdUsuario);

        unidadDeTrabajo.GuardarCambios();

        return new ModificarEstadoExpedienteResponse();
    }
}