using SGE.Dominio.Tramites;

namespace SGE.Aplicacion.Tramites;
public interface ITramiteRepository
{
    void Agregar (Tramite t);
    void Eliminar (Guid id);
    Tramite? ObtenerPorId (Guid id);
    IEnumerable<Tramite> ObtenerPorExpedienteId(Guid expid);
}