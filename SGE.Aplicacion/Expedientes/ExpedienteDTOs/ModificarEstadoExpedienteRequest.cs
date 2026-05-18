using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes.ExpedienteDTOs;
public record class ModificarEstadoExpedienteRequest (Guid IdUsuario, EstadoExpediente NuevoEstado, Guid IdExpediente);