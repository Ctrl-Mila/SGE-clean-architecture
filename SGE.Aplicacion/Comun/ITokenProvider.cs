using SGE.Dominio.Usuarios;

namespace SGE.Aplicacion.Comun;

public interface ITokenProvider
{
    string GenerarToken (Usuario usuario);

}