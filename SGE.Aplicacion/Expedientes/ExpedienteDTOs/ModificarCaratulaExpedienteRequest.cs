
namespace SGE.Aplicacion.Expedientes.ExpedienteDTOs;
public record class ModificarCaratulaExpedienteRequest (Guid IdUsuario, string Caratula, Guid IdExpediente);