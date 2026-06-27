using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Usuarios.UsuarioDTOs;
using SGE.Dominio.Comun;
using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class RegistrarUsuarioUseCase (IUsuarioRepository repositorio, IUnidadDeTrabajo unidadDeTrabajo, IContraseñaHasher hasher)
{
    public RegistrarUsuarioResponse Ejecutar (RegistrarUsuarioRequest request)
    {
        var correo = new Correo(request.Correo);
        
        if (repositorio.ObtenerPorCorreo(correo) != null)
        {
            throw new UsuarioExistenteException ("El correo ya se encuentra registrado en el sistema");
        }

        var contraHash = hasher.CalcularHash(request.Contraseña);
        
        var usuario = new Usuario (request.Nombre, correo, contraHash);

        repositorio.Agregar(usuario);

        unidadDeTrabajo.GuardarCambios();

        return new RegistrarUsuarioResponse(usuario.Id);
    }
}