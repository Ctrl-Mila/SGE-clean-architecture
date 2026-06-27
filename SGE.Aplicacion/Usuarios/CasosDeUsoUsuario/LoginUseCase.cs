using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Usuarios.UsuarioDTOs;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Autorizacion;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class LoginUseCase (IUsuarioRepository repositorio, IContraseñaHasher hasher, ITokenProvider tokenProvider)
{
    public LoginResponse Ejecutar(LoginRequest request)
    {
        var correo = new Correo(request.Correo);

        var usuario = repositorio.ObtenerPorCorreo(correo) ?? throw new AutorizacionException ("Correo o contraseña incorrectos");

        var hash = hasher.CalcularHash(request.Contraseña);

        if (usuario.ContraseñaHash != hash)
        {
            throw new AutorizacionException("Correo o contraseña incorrectos");
        }

        var token = tokenProvider.GenerarToken(usuario);

        return new LoginResponse(token);
    }
}