using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios.UsuarioDTOs;

public record class UsuarioResponse (Guid Id, string Nombre, string Correo, bool EsAdmin, IEnumerable<Permiso> Permisos);