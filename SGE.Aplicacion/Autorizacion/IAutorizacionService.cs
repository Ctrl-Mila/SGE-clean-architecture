
public interface IAutorizacionService
{
    bool PoseeElPermiso (Guid idUsuario, Permiso permiso);
    
}