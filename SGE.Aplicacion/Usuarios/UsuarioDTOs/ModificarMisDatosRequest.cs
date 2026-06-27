namespace SGE.Aplicacion.Usuarios.UsuarioDTOs;

public record class ModificarMisDatosRequest (Guid IdUsuario, string Nombre, string Correo, string Contraseña);