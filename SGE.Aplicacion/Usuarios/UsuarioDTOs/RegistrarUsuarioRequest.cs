namespace SGE.Aplicacion.Usuarios.UsuarioDTOs;

public record class RegistrarUsuarioRequest (string Nombre, string Correo, string Contraseña);