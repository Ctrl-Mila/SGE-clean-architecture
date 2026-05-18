
public interface ITramiteRepository
{
    void Agregar (Tramite t);
    void Modificar (Tramite t);
    void Eliminar (Guid id);
    Tramite? ObtenerPorId (Guid id);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expid);
}