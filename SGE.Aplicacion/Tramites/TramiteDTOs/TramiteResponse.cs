using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites.TramiteDTOs;
public record class TramiteResponse (Guid Id, string Contenido, Guid IdExpediente, EtiquetaTramite Etiqueta, DateTime FechaCreacion, DateTime FechaUltima, Guid IdUsuario);