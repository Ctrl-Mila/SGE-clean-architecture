namespace SGE.Aplicacion.Usuarios.UsuarioDTOs;

public record class ListarUsuariosResponse (IEnumerable<UsuarioResponse> Usuarios);