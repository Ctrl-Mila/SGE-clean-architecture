using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites.TramiteDTOs;

public record class AgregarTramiteRequest (string Contenido, Guid IdExpediente, Guid IdUsuario, EtiquetaTramite Etiqueta);