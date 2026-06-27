using SGE.Aplicacion.Tramites.TramiteDTOs;

namespace SGE.Aplicacion.Expedientes.ExpedienteDTOs;

public record class ObtenerExpedientePorIdResponse (ExpedienteResponse Expediente, IEnumerable<TramiteResponse> Tramites);