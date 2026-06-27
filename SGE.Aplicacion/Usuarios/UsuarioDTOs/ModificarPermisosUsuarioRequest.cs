using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios.UsuarioDTOs;

public record class ModificarPermisosUsuarioRequest (Guid IdSolicitante, Guid IdBuscado, List<Permiso> Permisos);