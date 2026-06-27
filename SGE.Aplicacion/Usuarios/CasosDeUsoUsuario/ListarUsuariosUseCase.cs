using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Usuarios.UsuarioDTOs;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class ListarUsuariosUseCase (IUsuarioRepository repositorio, IAutorizacionService autorizacion)
{
    public ListarUsuariosResponse Ejecutar(ListarUsuariosRequest request)
    {
        if (!autorizacion.EsAdmin(request.IdSolicitante))
        {
            throw new AutorizacionException ("Solo un administrador puede consultar los usuarios");
        }

        var lista = new List<UsuarioResponse>();

        foreach (var u in repositorio.ObtenerTodos())
        {
            lista.Add (new UsuarioResponse(u.Id, u.Nombre, u.CorreoElectronico.ToString(), u.EsAdministrador, u.Permisos));
        }

        return new ListarUsuariosResponse(lista);
    }
}