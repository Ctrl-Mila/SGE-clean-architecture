using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites.TramiteDTOs;
public record class ModificarTramiteRequest (Guid IdUsuario, Guid IdTramite, EtiquetaTramite Etiqueta, string Contenido);