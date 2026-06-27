using SGE.Aplicacion.Usuarios.UsuarioDTOs;
using SGE.Aplicacion.Autorizacion;
using SGE.Aplicacion.Comun;

namespace SGE.Aplicacion.Usuarios.CasosDeUsoUsuario;

public class ObtenerUsuarioPorIdUseCase (IUsuarioRepository repositorio, IAutorizacionService autorizacion)
{
    public ObtenerUsuarioPorIdResponse Ejecutar (ObtenerUsuarioPorIdRequest request)
    {
        if (!autorizacion.EsAdmin(request.IdSolicitante))
        {
            throw new AutorizacionException ("Solo un administrador puede consultar usuarios");
        }

        var usuario = repositorio.ObtenerPorId(request.IdBuscado) ?? throw new EntidadNoEncontradaException ("El usuario no fue encontrado");

        var response = new UsuarioResponse (usuario.Id, usuario.Nombre, usuario.CorreoElectronico.ToString(), usuario.EsAdministrador, usuario.Permisos);

        return new ObtenerUsuarioPorIdResponse(response);
    }
}