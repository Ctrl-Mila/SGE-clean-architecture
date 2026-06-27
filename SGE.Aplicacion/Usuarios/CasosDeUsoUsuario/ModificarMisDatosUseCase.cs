using SGE.Aplicacion.Usuarios.UsuarioDTOs;
using SGE.Dominio.Usuarios;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class ModificarMisDatosUseCase(IUsuarioRepository repositorio, IContraseñaHasher hasher, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarMisdatosResponse Ejecutar(ModificarMisDatosRequest request)
    {
        var usuario = repositorio.ObtenerPorId(request.IdUsuario) ?? throw new EntidadNoEncontradaException("El usuario no fue encontrado");

        usuario.CambiarNombre(request.Nombre);

        usuario.CambiarCorreo(new Correo(request.Correo));

        usuario.CambiarContraseñaHash(hasher.CalcularHash(request.Contraseña));

        unidadDeTrabajo.GuardarCambios();

        return new ModificarMisdatosResponse();
    }
}