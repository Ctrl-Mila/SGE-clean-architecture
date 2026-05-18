using SGE.Dominio.Expedientes;

namespace SGE.Aplicacion.Expedientes.ExpedienteDTOs;

public record class ExpedienteResponse (Guid Id, string Caratula, DateTime FechaCreacion, EstadoExpediente Estado, DateTime FechaUltima, Guid UsuarioUltimoCambio);