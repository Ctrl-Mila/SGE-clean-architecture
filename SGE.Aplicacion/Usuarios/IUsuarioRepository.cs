using SGE.Dominio.Usuarios
namespace SGE.Aplicacion.Usuarios;

public interface IUsuarioRepository
{
    void Agregar (Usuario u);
    void Eliminar (Guid id);
    Usuario? ObtenerPorId (Guid id);
    IEnumerable<Usuario> ObtenerTodos();
    Usuario? ObtenerPorCorreo(Correo correo);

}