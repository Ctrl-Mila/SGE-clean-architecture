using SGE.Aplicacion.Usuarios.UsuarioDTOs;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class EliminarUsuarioUseCase (IUsuarioRepository repositorio, IAutorizacionService autorizacion, IUnidadDeTrabajo unidadDeTrabajo)
{
    public EliminarUsuarioResponse Ejecutar (EliminarUsuarioRequest request)
    {
        if (!autorizacion.EsAdmin(request.IdSolicitante))
        {
            throw new AutorizacionException ("Solo un administrador puede eliminar usuarios");
        }

        var usuario = repositorio.ObtenerPorId(request.IdBuscado) ?? throw new EntidadNoEncontradaException ("El usuario no fue encontrado");

        repositorio.Eliminar(usuario.Id);

        unidadDeTrabajo.GuardarCambios();

        return new EliminarUsuarioResponse();
    }
}