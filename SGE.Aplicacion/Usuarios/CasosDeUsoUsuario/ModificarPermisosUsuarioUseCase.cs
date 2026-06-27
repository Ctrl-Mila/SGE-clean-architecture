using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;
using SGE.Aplicacion.Usuarios.UsuarioDTOs;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class ModificarPermisosUsuarioUseCase (IUsuarioRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public ModificarPermisosUsuarioResponse Ejecutar (ModificarPermisosUsuarioRequest request)
    {
        if (!autorizacion.EsAdmin(request.IdSolicitante))
        {
            throw new AutorizacionException ("Solo un administrador puede modificar permisos de usuario");
        }

        var usuario = repositorio.ObtenerPorId(request.IdBuscado) ?? throw new EntidadNoEncontradaException ("El usuario no fue encontrado");

        usuario.ModificarPermisos(request.Permisos);

        unidadDeTrabajo.GuardarCambios();

        return new ModificarPermisosUsuarioResponse();
    }
}